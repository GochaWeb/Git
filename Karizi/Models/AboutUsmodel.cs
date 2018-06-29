using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karizi.Models
{
    public class AboutUsmodel
    {
        [AllowHtml]
        [Required(ErrorMessage = "შეიყვანეთ სიახლის ტექსტი!")]
        [StringLength(4000, ErrorMessage = "მაქსიმუმ, 4000 სიმბოლო!")]
        public string Text { get; set; }
    }
}