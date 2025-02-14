using AutoMapper;
using FluentAssertions;
using Moq;
using PublicHolidays.Domain.Abstractions;
using PublicHolidays.Domain.Entities;
using PublicHolidays.Services.Models;
using PublicHolidays.Services.Services;

namespace PublicHolidays.Tests.Services
{
    public class CountryServiceTests
    {
        private readonly Mock<ICountryRepository> _countryRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CountryService _countryService;

        public CountryServiceTests()
        {
            _countryRepositoryMock = new Mock<ICountryRepository>();
            _mapperMock = new Mock<IMapper>();
            _countryService = new CountryService(_countryRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllCountriesAsync_ShouldReturnMappedCountries()
        {
            // Arrange
            var countries = new List<Country>
        {
            new() { Code = "US", Name = "United States" },
            new() { Code = "GB", Name = "United Kingdom" }
        };

            var countryDtos = new List<CountryDomain>
        {
            new() { Code = "US", Name = "United States" },
            new() { Code = "GB", Name = "United Kingdom" }
        };

            _countryRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(countries);

            _mapperMock.Setup(m => m.Map<IEnumerable<CountryDomain>>(countries))
                .Returns(countryDtos);

            // Act
            var result = await _countryService.GetAllCountriesAsync();

            // Assert
            result.Should().BeEquivalentTo(countryDtos);
            _countryRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<CountryDomain>>(countries), Times.Once);
        }
    }
}
