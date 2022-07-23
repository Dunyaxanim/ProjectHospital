using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class PatientDetail
    {
        public int Id { get; set; }
       
       
        public string EmailAdress { get; set; }
        [Required(ErrorMessage = "Ata adını qeyd edin.")]
        public string FatherName { get; set; }
        
        public string File { get; set; }
        [NotMapped]
        public IFormFile Document { get; set; }
        //[Required(ErrorMessage = "Ana adını qeyd edin.")]
        public string MotherName { get; set; }
        [Required(ErrorMessage = "Mobil nömrəsini daxil edin.")]
        public int MobileNumber { get; set; }
        [Required(ErrorMessage = "Ev nömrəsini daxil edin.")]
        public int HomeNumber { get; set; }
        [Required(ErrorMessage = "Şəxsiyyət vəsiqəsinin FİN kodunu qeyd edin.")]
        public int CodeOfIdentityCard { get; set; }
        [Required(ErrorMessage = "Qan qurpunu qeyd edin.")]
        public string BloodGroup { get; set; }
        [Required(ErrorMessage = "Cinsiyyət qeyd olunmalıdır.")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Doğum tarixini qeyd edin.")]
        public DateTime DateOfBrith { get; set; }
        [Required(ErrorMessage = "Hal hazırda yaşadığı ünvanı qeyd edin.")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Doğulu ünvanı qeyd edin.")]
        public string Destination { get; set; }
        
        public bool Insurance { get; set; }

        
        public bool IsReferredPatient { get; set; }
        
        public Patient Patient { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        
    }
}
