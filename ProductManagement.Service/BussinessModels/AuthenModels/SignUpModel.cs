﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Service.BussinessModels.AuthenModels
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email."), EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        [Display(Name = "Email address")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [Display(Name = "Full name")]
        public string FullName { get; set; } = "";


        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Mật khẩu phải có từ 4 đến 20 kí tự.")]
        public string Password { get; set; } = "";
    }
}
