using Microsoft.EntityFrameworkCore;

namespace UMS.Persistence
{
    public class UmsDbContextFactory : DesignTimeDbContextFactoryBase<umsContext>
    {
        protected override umsContext CreateNewInstance(DbContextOptions<umsContext> options)
        {
            return new umsContext(options);
        }
    }
}
