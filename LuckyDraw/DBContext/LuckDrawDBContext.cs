using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyDraw.DBContexts
{
    public class LuckDrawDBContext : DbContext
    {
        public LuckDrawDBContext()
            : base("name=LuckyDrawEntities")
        {
        }

        public LuckDrawDBContext(DbConnection connection) : base(connection, true)
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    throw new UnintentionalCodeFirstException();
        //}

        public virtual DbSet<Prize> Prizes { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Result> Results { get; set; }
    }
}
