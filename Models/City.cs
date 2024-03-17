namespace NCG.HR.Models
{
    public class City : UserActivity
    {        
        public string Code { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
