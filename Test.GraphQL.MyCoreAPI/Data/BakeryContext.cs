using Microsoft.EntityFrameworkCore;
using Test.GraphQL.MyCoreAPI.Models;

namespace Test.GraphQL.MyCoreAPI.Data
{
    public class BakeryContext:DbContext
    {
        public BakeryContext(DbContextOptions<BakeryContext> options) : base(options)
        {

        }
        public DbSet<Cake> Cake { get; set; }
    }
}
