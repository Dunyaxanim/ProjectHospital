using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class HekimDetail
    {
        public int Id { get; set; }
       
        [Required(ErrorMessage = "Adress daxil edin.")]
        public bool Sex { get; set; }
        [Required(ErrorMessage = "Qan qrupunu daxil edin.")]
        public string BloodGroup { get; set; }
        
        [Required(ErrorMessage = "Doğum tarixini qeyd edin.")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "İşə qəbul olunma tarixini qeyd edin.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Doğulduğu yeri qeyd edin.")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Hal hazırda yaşadığı ünvanı qeyd edin.")]
        public string Destination { get; set; }
        [Required(ErrorMessage = "İxtisas daxil edin.")]
        public string Proprofession { get; set; }
        [Required(ErrorMessage = "Vəzifə qeyd edin.")]
        public string Position { get; set; }
        [Required(ErrorMessage = "Dil biliklərini qeyd edin.")]
        public string ForeignLanguages { get; set; }
        [Required(ErrorMessage = "Əlaqə nömrəsini daxil edin.")]
        public int PhoneNumber { get; set; }
        public Hekim Hekim { get; set; }

        [ForeignKey("Hekim")]
        public int HekimId { get; set; }
    }
}
