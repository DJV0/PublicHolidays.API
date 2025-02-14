namespace Shared.Contracts.Holidays
{
    public class HolidaysByYearResult
    {
        public int Month { get; set; }

        public List<HolidayContract> Holidays { get; set; }
    }
}
