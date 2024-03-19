using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    public class Holiday : UserActivity
    {

        [Display(Name="假期标题")]
        public string Title { get; set; }
        [Display(Name="假期开始")]
        public DateTime StartDate { get; set; }
        [Display(Name="假期结束")]
        public DateTime EndDate { get; set; }
    }
}
