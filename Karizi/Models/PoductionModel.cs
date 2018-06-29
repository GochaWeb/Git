using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karizi.Models
{
    public class PoductionModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "შეიყვანეთ პროდუქციის დასახელება!")]
        [StringLength(100, ErrorMessage = "მაქსიმუმ, 100 სიმბოლო!")]
        public string Name { get; set; }
    }
}