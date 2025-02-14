namespace Shared.Domain.Models
{
    public class HolidayDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public int CountryId { get; set; }
    }
}
