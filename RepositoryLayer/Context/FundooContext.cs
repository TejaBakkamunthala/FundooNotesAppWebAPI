using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {

        public FundooContext(DbContextOptions options) : base(options) 
        { 

        }

        public DbSet<UserEntity> UserTablee { get; set; }

        public DbSet<CustomerEntity> CustomerTablee { get; set; }

        public DbSet<ProductEntity> ProductTablee { get; set; }

        public DbSet<NotesEntity> NotesTablee { get; set;}

        public DbSet<LabelEntity> LabelTablee { get; set; }

        public DbSet<CollaboratorEntity> CollaboratorTablee { get;set; }
 

    }
}
