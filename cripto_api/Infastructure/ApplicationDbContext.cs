using Domain.Domains.IdentityDomain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infastructure;

public class ApplicationDbContext : IdentityDbContext<UserIdentity, RoleIdentity, int, UserClaim, UserHasRole, UserLogin, RoleClaim, UserToken>
{
    public DbSet<Crypto> Cryptos { get; set; }
    public DbSet<Ticker> Tickers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public ApplicationDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Crypto>()
       .HasIndex(e => e.Name)
       .IsUnique();

        // Crypto ve Ticker arasındaki ilişkiyi tanımla
        modelBuilder.Entity<Ticker>()
            .HasOne(t => t.Crypto)
            .WithMany(c => c.Tickers)
            .HasForeignKey(t => t.CryptoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserIdentity>()
        .Ignore(u => u.NormalizedEmail)
        .Ignore(u => u.NormalizedUserName)
        .Ignore(u => u.EmailOptional)
        .Ignore(x => x.UserName)
        .Ignore(x => x.TwoFactorEnabled)
        .Ignore(x => x.LastActiveDate)
        .Ignore(x => x.LockoutEnabled)
        .Ignore(x => x.LockoutEnd)
        .Ignore(x => x.AccessFailedCount)
        .Ignore(x => x.EmailConfirmed)
        .Ignore(x => x.PhoneNumberConfirmed);
    }
}