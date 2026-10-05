using System;
using System.Collections.Generic;
using Entities;

namespace ServiceContracts.DTO
{
    public class CountryAddRequest
    {
        /// <summary>
        /// DTO for adding a new country. This class contains the necessary information to create a new country entity.
        /// </summary>
        public string? CountryName { get; set; }

        public Country ToCountry()
        {
            return new Country()
            {
                CountryName = CountryName
            };
        }
    }
}
