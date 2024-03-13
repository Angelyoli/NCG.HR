using System.ComponentModel.DataAnnotations;

namespace NCG.HR.Models
{
    public class SystemCode : UserActivity
    {
     
        public string Code { get; set; }
        public string Description { get;set; }
    }
}
