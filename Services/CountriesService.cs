using ServiceContracts;
using ServiceContracts.DTO;
using Entities;
namespace Services
{
    public class CountriesService : ICountriesService
    {
        // private field for storing countries in-memory
        private readonly List<Country> _countries;

        // constructor to initialize the in-memory list of countries
        public CountriesService()
        {
            _countries = new List<Country>();
        }
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // validation: check if countryAddRequest is null
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }
            // validation: check if country name is null or empty
            if (countryAddRequest.CountryName == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.CountryName));
            }
            // duplicate check: check if a country with the same name already exists in the in-memory list
            if (_countries.Where(temp => temp.CountryName == countryAddRequest.CountryName).Count() > 0)
            {
                throw new ArgumentException("country name already exists");
            }
            // validate countryAddRequest
            Country country = countryAddRequest.ToCountry();

            // generate countryID for the new country
            country.CountryID = Guid.NewGuid();
            // Add the new country to the in-memory list
            _countries.Add(country);
            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(country => country.ToCountryResponse()).ToList();

        }
    }
}
