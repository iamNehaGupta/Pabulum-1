using System.Reflection;
using TYProject.Core.ContributorAggregate;
using TYProject.Core.ProjectAggregate;
using TYProject.SharedKernel;
using TYProject.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using TYProject.Core.CompanyAggregate;
using TYProject.Core.PersonAggregate;
using TYProject.Core.ContactAggregate;
using TYProject.Core.CouponAggregate;
using TYProject.Core.LoginAggregate;

namespace TYProject.Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher)
      : base(options)
  {
    _dispatcher = dispatcher;
  }

  public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
  public DbSet<Project> Projects => Set<Project>();
  public DbSet<Contributor> Contributors => Set<Contributor>();

  public virtual DbSet<Company> Companies { get; set; }

  public virtual DbSet<Contact> Contacts { get; set; }

  public virtual DbSet<Coupon> Coupons { get; set; }

  public virtual DbSet<Login> Logins { get; set; }

  public virtual DbSet<Person> People { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
