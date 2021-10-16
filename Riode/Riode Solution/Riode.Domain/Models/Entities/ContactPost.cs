using Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Domain.Models.Entities
{
    public class ContactPost : BaseEntity
    {
        [Display(ResourceType =typeof(ContactResource),Name ="Name")]
        [Required(ErrorMessageResourceType =typeof(ContactResource),ErrorMessageResourceName = "CantBeEmpty")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(ContactResource), Name = "Email")]
        [Required(ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "CantBeEmpty")]
        [EmailAddress(ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "İnvalidEmailAddress")]
        public string Email { get; set; }

        [Display(ResourceType = typeof(ContactResource), Name = "Comment")]
        [Required(ErrorMessageResourceType = typeof(ContactResource), ErrorMessageResourceName = "CantBeEmpty")]
        public string Comment { get; set; }
        public string Answer { get; set; }
        public DateTime? AnswerDate{ get; set; }
        public int? AnswerByUserId { get; set; }
        public bool IsMarked { get; set; } = false;
    }
}
