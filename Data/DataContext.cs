using Microsoft.EntityFrameworkCore;
using VerticalSliceArch.Domain;

namespace VerticalSliceArch.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<GameConsole> Consoles { get; set; }
        public DbSet<Game> Games { get; set; }
        

        ///Department
       public DbSet<Departments> Departments { get; set; }
        ///User
        public DbSet<Users> User { get; set; }
        
        ///UserRoles
        public DbSet<UserRole> UserRoles { get; set; }


    }
}
