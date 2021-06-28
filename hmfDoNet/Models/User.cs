using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace hmfDoNet.Models
{
    public class User
    {
        [Required]
        [Display(Name ="用户名")]
        public String Username { get; set; }

        [Required]
        [Display(Name = "密码")]
        [StringLength(maximumLength:10,MinimumLength =6,ErrorMessage ="密码最少不低于6个字符，最大不超过10个字符")]
        public String Password { get; set; }

        [Required]
        [Display(Name ="重复密码")]
        [Compare("Password")]
        public String RepeatPassword { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "不是合法的邮箱地址")]
        public String Email { get; set; }

        public String ObjectId { get; set; }

    }
}