using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Myembro.Infrastructure;
using Myembro.Properties;

namespace Myembro.Models
{


public class LogOnModel
{
    [Required]
    [LocalizedDisplayName("AccountLogonUserLabelText")]
    public string UserName { get; set; }

    [Required]    
    [DataType(DataType.Password)]
    [Display(Name = "AccountLogonPasswordLabelText", ResourceType = typeof(Resources))]
    public string Password { get; set; }

    [Display(Name = "AccountLogonRememberMeCheckBoxText", ResourceType = typeof(Resources))]
    public bool RememberMe { get; set; }
}}
