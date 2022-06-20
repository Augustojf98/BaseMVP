using BaseMVP.Entities.Classes;
using Microsoft.EntityFrameworkCore;

namespace BaseMVP.Data
{
    public class MyDBContext : DbContext
    {
        public DbSet<UserDTO> Users { get; set; }

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }
    }
}
