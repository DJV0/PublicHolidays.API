using Moq;
using PublicHolidays.Domain.Abstractions;
using PublicHolidays.Services.Abstractions;
using PublicHolidays.Services.Services;
using Shared.Domain.Models;

namespace PublicHolidays.Tests.Services
{
    public class HolidayServiceTests
    {
        private readonly Mock<IHolidayRepository> _holidayRepositoryMock;
        private readonly Mock<IDataHelper> _dataHelperMock;
        private readonly HolidayService _holidayService;

        public HolidayServiceTests()
        {
            _holidayRepositoryMock = new Mock<IHolidayRepository>();
            _dataHelperMock = new Mock<IDataHelper>();
            _holidayService = new HolidayService(_holidayRepositoryMock.Object, _dataHelperMock.Object);
        }

        [Fact]
        public async Task GetMaxNumberOfFreeDaysInRowAsync_Returns_CorrectValue()
        {
            // Arrange
            int year = 2025;
            string countryCode = "US";

            var holidays = new List<HolidayDomain>
        {
            new HolidayDomain { Date = new DateOnly(2025, 1, 1) },
            new HolidayDomain { Date = new DateOnly(2025, 1, 20) },
            new HolidayDomain { Date = new DateOnly(2025, 7, 3) },
            new HolidayDomain { Date = new DateOnly(2025, 7, 4) },
            new HolidayDomain { Date = new DateOnly(2025, 12, 25) }
        };

            _holidayRepositoryMock
                .Setup(repo => repo.GetHolidaysByYearAsync(year, countryCode))
                .ReturnsAsync(holidays);

            _dataHelperMock
                .Setup(helper => helper.FetchCountryHolidaysAsync(countryCode))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _holidayService.GetMaxNumberOfFreeDaysInRowAsync(year, countryCode);

            // Assert
            Assert.Equal(4, result);
        }
    }
}
