using Microsoft.EntityFrameworkCore;
namespace NCG.HR.SqlServer
{
    public static class Extensions
    {
        public static IServiceCollection AddSqlServerDbContext<T>(this IServiceCollection services, bool isProduction, string connectionString) where T : DbContext
        {
            if (isProduction)
            {
                Console.WriteLine($"--> Using SqlServer DB");
                services.AddDbContext<T>(opt =>
                opt.UseSqlServer(
                    connectionString,
                b => b.MigrationsAssembly(typeof(T).Assembly.FullName)));
            }
            else
            {
                Console.WriteLine($"--> Using In Memory DB");
                services.AddDbContext<T>(opt => opt.UseInMemoryDatabase("InMem"));
            }
            return services;
        }
        //public static IServiceCollection AddSqlRepository<TDB, T>(this IServiceCollection services)
        //where TDB : DbContext
        //where T : class, IEntity
        //{
        //    services.AddTransient<IRepository<T>, Repository<TDB, T>>();
        //    return services;
        //}
    }
}
