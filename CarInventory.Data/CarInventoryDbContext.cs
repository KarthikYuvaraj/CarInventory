﻿using CarInventory.Core.Entities;
using CarInventory.Core.Logging;
using CarInventory.Data.Configurations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace CarInventory.Data
{
    public class CarInventoryDbContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        private ObjectContext _objectContext;
        private DbTransaction _transaction;
        private static readonly object Lock = new object();
        private static bool _databaseInitialized;

        public CarInventoryDbContext()
            : base("CarInventoryDbConnection")
        {

        }

        public CarInventoryDbContext(string nameOrConnectionString, ILogger logger)
            : base(nameOrConnectionString)
        {
            if (logger != null)
            {
                Database.Log = logger.Log;
            }

            if (_databaseInitialized)
            {
                return;
            }
            lock (Lock)
            {
                if (!_databaseInitialized)
                {
                    Database.SetInitializer(new BigBangInitializer());
                    _databaseInitialized = true;
                }
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CarConfiguration());
        }

        public static CarInventoryDbContext Create()
        {
            return new CarInventoryDbContext(nameOrConnectionString: "CarInventoryDbConnection", logger: null);
        }

        #region DbSets
        public virtual IDbSet<Car> Cars { get; set; }
        #endregion

        #region IDbContext

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public void SetAsAdded<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Added);
        }

        public void SetAsModified<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Modified);
        }

        public void SetAsDeleted<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            UpdateEntityState(entity, EntityState.Deleted);
        }

        public void BeginTransaction()
        {
            this._objectContext = ((IObjectContextAdapter)this).ObjectContext;
            if (_objectContext.Connection.State == ConnectionState.Open)
            {
                return;
            }
            _objectContext.Connection.Open();
            _transaction = _objectContext.Connection.BeginTransaction();
        }

        public int Commit()
        {
            try
            {
                BeginTransaction();
                var saveChanges = SaveChanges();
                _transaction.Commit();

                return saveChanges;
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                BeginTransaction();
                var saveChangesAsync = await SaveChangesAsync();
                _transaction.Commit();

                return saveChangesAsync;
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        private void UpdateEntityState<TEntity>(TEntity entity, EntityState entityState) where TEntity : BaseEntity
        {
            var dbEntityEntry = GetDbEntityEntrySafely(entity);
            dbEntityEntry.State = entityState;
        }

        private DbEntityEntry GetDbEntityEntrySafely<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var dbEntityEntry = Entry<TEntity>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
            {
                Set<TEntity>().Attach(entity);
            }
            return dbEntityEntry;
        }

        #endregion
    }
}
