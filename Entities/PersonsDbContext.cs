using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Entities
{
    public class PersonsDbContext : DbContext
    {
        public PersonsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");

            // Seed from JSON files placed in the application's output directory
            string basePath = AppContext.BaseDirectory;

            //Seed to Countries
            string countriesJsonPath = System.IO.Path.Combine(basePath, "countries.json");
            if (System.IO.File.Exists(countriesJsonPath))
            {
                string countriesJson = System.IO.File.ReadAllText(countriesJsonPath);
                List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson) ?? new List<Country>();
                foreach (Country country in countries)
                    modelBuilder.Entity<Country>().HasData(country);
            }

            //Seed to Persons
            string personsJsonPath = System.IO.Path.Combine(basePath, "persons.json");
            if (System.IO.File.Exists(personsJsonPath))
            {
                string personsJson = System.IO.File.ReadAllText(personsJsonPath);
                List<Person> persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJson) ?? new List<Person>();
                foreach (Person person in persons)
                    modelBuilder.Entity<Person>().HasData(person);
            }


            //Fluent API
            modelBuilder.Entity<Person>().Property(temp => temp.TIN)
              .HasColumnName("TaxIdentificationNumber")
              .HasColumnType("varchar(8)")
              .HasDefaultValue("ABC12345");

            //modelBuilder.Entity<Person>()
            //  .HasIndex(temp => temp.TIN).IsUnique();

            modelBuilder.Entity<Person>()
              .ToTable(t => t.HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 8"));

            //Table Relations
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasOne<Country>(c => c.Country)
                .WithMany(p => p.Persons)
                .HasForeignKey(p => p.CountryID);
            });
        }

        public List<Person> sp_GetAllPersons()
        {
            return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
        }

        public int sp_InsertPerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[] {
        new SqlParameter("@PersonID", person.PersonID),
        new SqlParameter("@PersonName", person.PersonName),
        new SqlParameter("@Email", person.Email),
        new SqlParameter("@DateOfBirth", person.DateOfBirth),
        new SqlParameter("@Gender", person.Gender),
        new SqlParameter("@CountryID", person.CountryID),
        new SqlParameter("@Address", person.Address),
        new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters)
      };

            return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertPerson] @PersonID, @PersonName, @Email, @DateOfBirth, @Gender, @CountryID, @Address, @ReceiveNewsLetters", parameters);
        }
    }
}