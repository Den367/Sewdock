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
    [Display(Name = "AccountLogonUserLabelText", ResourceType = typeof(Mayando.Web.Properties.Resources))]
    public string UserName { get; set; }

    [Required]    
    [DataType(DataType.Password)]
    [Display(Name = "AccountLogonPasswordLabelText", ResourceType = typeof(Mayando.Web.Properties.Resources))]
    public string Password { get; set; }

    [Display(Name = "AccountLogonRememberMeCheckBoxText", ResourceType = typeof(Mayando.Web.Properties.Resources))]
    public bool RememberMe { get; set; }
}}
