using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karizi.Models
{
    public class LoginModel
    {
        [AllowHtml]
        [Required(ErrorMessage = "შეავსეთ ელ-ფოსტა!")]
        [StringLength(50, ErrorMessage = "მაქსიმუმ, 50")]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
  + "@"
  + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "გთხოვთ, ჩაწერეთ ელ-ფოსტა!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "შეავსეთ პაროლი!")]
        public string Password { get; set; }
    }
}