using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Mayando.Web.Properties;

namespace Mayando.Web.Models
{


public class LogOnModel
{
    [Required]
    [Display(Name = "AccountLogonUserLabelText", ResourceType = typeof(Resources))]
    public string UserName { get; set; }

    [Required]    
    [DataType(DataType.Password)]
    [Display(Name = "AccountLogonPasswordLabelText", ResourceType = typeof(Resources))]
    public string Password { get; set; }

    [Display(Name = "AccountLogonRememberMeCheckBoxText", ResourceType = typeof(Resources))]
    public bool RememberMe { get; set; }
}}
