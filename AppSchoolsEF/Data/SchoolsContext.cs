using Microsoft.EntityFrameworkCore;
using AppSchoolsEF.Models;

namespace AppSchoolsEF.Data
{
    public class SchoolsContext : DbContext
    {
        public SchoolsContext(DbContextOptions<SchoolsContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
