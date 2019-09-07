using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostCard.Models
{
    /// <summary>
    /// The DbContext class is the class that we use in our application code to interact with the underlying database.
    /// This is the class that manages the database connection and is used to retrieve and save data in the database. 
    /// The link below provide more detials
    /// https://docs.microsoft.com/en-us/dotnet/api/system.data.entity.dbcontext?view=entity-framework-6.2.0
    /// </summary>
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        /// <summary>
        /// We will use this DbSet property UploadedDatas to query and save instances of the UploadedData class.
        ///The LINQ queries against the DbSet<TEntity> will be translated into queries against the underlying database.
        /// </summary>
        public DbSet<UploadedData> UploadedDatas { get; set; }
    }
}
