using System;
using Entities;

namespace ServiceContracts.DTO
{
    public class CountryResponse
    {
        /// <summary>
        /// DTO for representing a country in the response. This class contains the information about a country to be returned by the API.
        /// </summary>
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(CountryResponse))
            {
                return false;
            }
            CountryResponse country_to_compare = (CountryResponse)obj;

            return CountryID == country_to_compare.CountryID && CountryName == country_to_compare.CountryName;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    public static class  CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse()
            {
                CountryID = country.CountryID,
                CountryName = country.CountryName
            };
        }
    }
}
