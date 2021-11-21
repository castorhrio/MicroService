using System.ComponentModel.DataAnnotations;

namespace  PlatformService.Data{
    public class PlatformCreatDto
    {
        [Required]
        public string Name{get;set;}

        [Required]
        public string Publisher{get;set;}

        [Required]
        public string Cost{get;set;}
    }
}