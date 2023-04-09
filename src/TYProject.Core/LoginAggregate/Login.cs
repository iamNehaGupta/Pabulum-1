using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TYProject.Core.CompanyAggregate;
using TYProject.Core.PersonAggregate;
using TYProject.SharedKernel;
using TYProject.SharedKernel.Interfaces;

#nullable disable

namespace TYProject.Core.LoginAggregate;
public partial class Login : EntityBase<long>, IAggregateRoot
{
  public string UserName { get; set; }

  public string Password { get; set; }

  public long CreatedBy { get; set; }

  public DateTime CreatedOnDate { get; set; }

  public long? LastModifiedBy { get; set; }

  public DateTime? LastModifiedOnDate { get; set; }

  public byte[] RowVer { get; set; }

  public virtual ICollection<Company> CompanyCreatedByNavigations { get; } = new List<Company>();

  public virtual ICollection<Company> CompanyLastModifiedByNavigations { get; } = new List<Company>();

  public virtual Login CreatedByNavigation { get; set; }

  public virtual ICollection<Login> InverseCreatedByNavigation { get; } = new List<Login>();

  public virtual ICollection<Login> InverseLastModifiedByNavigation { get; } = new List<Login>();

  public virtual Login LastModifiedByNavigation { get; set; }

  public virtual ICollection<Person> PersonCreatedByNavigations { get; } = new List<Person>();

  public virtual ICollection<Person> PersonLastModifiedByNavigations { get; } = new List<Person>();
}
