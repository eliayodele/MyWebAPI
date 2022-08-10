
using Microsoft.EntityFrameworkCore;
using MyWebAPI.Model;

namespace MyWebAPI.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
    }

}
