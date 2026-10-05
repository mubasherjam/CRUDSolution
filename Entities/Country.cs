namespace Entities
{
    public class Country
    {
        /// <summary>
        /// Domain Model for country entity. This class represents a country with its unique identifier and name.
        /// </summary>
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }
    }
}
