using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Karizi.Models
{
    public class MessageModel
    {
        [AllowHtml]
        [Required(ErrorMessage = "შეიყვანეთ სახელი!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "შეიყვანეთ ელ-ფოსტა!")]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
   + "@"
   + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "გთხოვთ, ჩაწერეთ ელ-ფოსტა!")]
        public string Email { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "შეიყვანეთ ტექსტი!")]
        public string Text { get; set; }
    }
}