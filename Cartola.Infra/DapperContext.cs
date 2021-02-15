using Microsoft.EntityFrameworkCore;

namespace Cartola.Infra
{
    public class DapperContext : DbContext
    {
        public DapperContext() { }
        public DapperContext(DbContextOptions<DapperContext> options) : base(options) { }
    }
}
