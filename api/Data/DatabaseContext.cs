using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext (DbContextOptions<DatabaseContext> options): base(options)
        {
        }

        public DbSet<EmailList> EmailLists { get; set; }

        public DbSet<UrlToken> UrlTokens { get; set; }
    }
}