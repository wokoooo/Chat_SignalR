using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class MessageViewModel
    {
        [Required]
        [Display(Name = "消息")]
        public string Message { get; set; }
    }
    
}
