using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYProject.SharedKernel;
using TYProject.SharedKernel.Interfaces;

#nullable disable

namespace TYProject.Core.ContactAggregate;

public partial class Contact: IAggregateRoot  
{
  public long ID { get; set; }
  public string Name { get; set; }

  public string Email { get; set; }

  public string Description { get; set; }

}
