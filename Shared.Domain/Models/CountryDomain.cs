namespace Shared.Domain.Models
{
    public class CountryDomain
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }
    }
}
