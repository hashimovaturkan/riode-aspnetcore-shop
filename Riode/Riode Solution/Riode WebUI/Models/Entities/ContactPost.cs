using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Riode_WebUI.Models.Entities
{
    public class ContactPost : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Comment { get; set; }
        public string Answer { get; set; }
        public DateTime? AnswerDate{ get; set; }
        public int? AnswerByUserId { get; set; }
        public bool IsMarked { get; set; } = false;
    }
}
