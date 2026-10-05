using System;
using System.Collections.Generic;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit;
namespace CRUDTests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;
        // constructor

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry() Tests

        // when CountryAddRequest is null, then AddCountry() should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            // Arrange
            CountryAddRequest? request = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //act
                _countriesService.AddCountry(request);
            });

        }
        // when CountryName is null, then AddCountry() should throw ArgumentException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest
            {
                CountryName = null
            };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //act
                _countriesService.AddCountry(request);
            });

        }
        // when CountryName is duplicate, then AddCountry() should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            // Arrange
            CountryAddRequest? request1 = new CountryAddRequest
            {
                CountryName = "USA"
            };
            // Arrange
            CountryAddRequest? request2 = new CountryAddRequest
            {
                CountryName = "USA"
            };
            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }
        // when you supply proper CountryAddRequest, then AddCountry() should return CountryResponse with proper values
        [Fact]
        public void AddCountry_ProperCountryDetails()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest
            {
                CountryName = "Canada"
            };
            // Act
            CountryResponse response = _countriesService.AddCountry(request);
            List<CountryResponse> countries_from_GetAllCountries = _countriesService.GetAllCountries();
            // Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, countries_from_GetAllCountries);

        }
        #endregion

        #region GetAllCountries() Tests

        [Fact]
        // when there are no countries in the in-memory list, then GetAllCountries() should return an empty list
        public void GetAllCountries_EmptyList()
        {
            // Act
            List<CountryResponse> countries =
            _countriesService.GetAllCountries();
            // Assert
            Assert.Empty(countries);
        }

        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            // Arrange
            List<CountryAddRequest> requests = new List<CountryAddRequest>
            {
                new CountryAddRequest
                {
                    CountryName = "USA"
                },
                new CountryAddRequest
                {
                    CountryName = "Canada"
                }
            };

            List<CountryResponse> responses = new List<CountryResponse>();

            foreach (var request in requests)
            {
               responses.Add(_countriesService.AddCountry(request));
            }
            // Act
            List<CountryResponse> countries =
            _countriesService.GetAllCountries();

            // read each element from countries in responses and check whether it is present in countries or not
            foreach (var response in responses)
            {
                Assert.Contains(response, countries);
            }
            // Assert
            Assert.Equal(2, countries.Count);
        }
        #endregion
    }
}
