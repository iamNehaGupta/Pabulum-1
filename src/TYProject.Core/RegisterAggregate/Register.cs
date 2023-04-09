using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYProject.SharedKernel;
using TYProject.SharedKernel.Interfaces;

namespace TYProject.Core.RegisterAggregate;
public class Register : EntityBase, IAggregateRoot
{
  public Register(string name, string email, string password, string retypepassword) 
  { 
    Name = name;
    Email = email;
    Password = password;
    ReTypePassword = retypepassword;  
  }
  [Required(ErrorMessage ="Please Enter Your Name")]
  public string Name { get; set; } 

  public string Email { get; set; } 
  public string Password { get; set; }  
  public string ReTypePassword { get; set; }

}
