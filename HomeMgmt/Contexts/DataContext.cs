﻿using HomeMgmt.Models.NotificationModels;
using HomeMgmt.Models.UserModels;
using HomeMgmt.Utils;
using Microsoft.EntityFrameworkCore;

namespace HomeMgmt.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // TABLES

        #region USER TABLES

        public DbSet<Permissions> Permissions { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<BannedAccount> BannedAccounts { get; set; }

        #endregion

        #region NOTIFICATION TABLES

        public DbSet<Notification> Notifications { get; set; }

        #endregion

        // OVERRIDE SAVE CHANGES ASYNC TO ADD AND UPDATE TIMESTAMPS
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var added = ChangeTracker.Entries<IAuditableEntity>().Where(E => E.State == EntityState.Added).ToList();
            var now = DateTime.Now;
            added.ForEach(E =>
            {
                E.Property(x => x.CreatedAt).CurrentValue = now;
                E.Property(x => x.UpdatedAt).CurrentValue = now;

            });

            var modified = ChangeTracker.Entries<IAuditableEntity>().Where(E => E.State == EntityState.Modified).ToList();

            modified.ForEach(E =>
            {
                E.Property(x => x.UpdatedAt).CurrentValue = now;

            });

            return base.SaveChangesAsync();
        }
    }
}
