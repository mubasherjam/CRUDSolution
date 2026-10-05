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

        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

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
            // Assert
            Assert.True(response.CountryID != Guid.Empty);
            
        }
    }
}
