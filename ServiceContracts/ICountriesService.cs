using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Interface for country-related services. represent business logic operations
    /// related to countries, such as adding, retrieving, updating, and deleting country information.
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a new country based on the provided CountryAddRequest and returns a CountryResponse.
        /// </summary>
        /// <param name="countryAddRequest"></param>
        /// <returns>the country object</returns>
        CountryResponse AddCountry(CountryAddRequest?
        countryAddRequest);
    }
}
