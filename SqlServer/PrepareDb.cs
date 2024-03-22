using Microsoft.EntityFrameworkCore;
using NCG.HR.Data;
namespace NCG.HR.SqlServer;

public static class PrepareDb
{
    public static void PreparePopulation(IApplicationBuilder app, bool isProduction)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            SeedData(scope.ServiceProvider.GetService<ApplicationDbContext>(), isProduction);
        }
    }

    private static void SeedData(ApplicationDbContext? appDbContext, bool isProduction)
    {
        if (isProduction)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try { appDbContext.Database.Migrate(); }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }
        }
        if (false)
        {
            Console.WriteLine("--> We are seeding data...");
            // appDbContext.Units.AddRange(
            //     new Unit { UnitName = "人次", UnitNumber = "00001" },
            //     new Unit { UnitName = "项次", UnitNumber = "00003" },
            //     new Unit { UnitName = "剂次", UnitNumber = "00002" }
            // );

            // appDbContext.Categories.AddRange(
            //     new Category { CategoryName = "公卫", CategoryNumber = "00001" },
            //     new Category { CategoryName = "基础公卫", CategoryNumber = "00003" },
            //     new Category { CategoryName = "医疗", CategoryNumber = "00002" }
            //     );

            // appDbContext.Classifications.AddRange(
            //     new Classification { ClassificationName = "门诊", ClassificationNumber = "00001" },
            //     new Classification { ClassificationName = "计免", ClassificationNumber = "00003" },
            //     new Classification { ClassificationName = "儿科门诊", ClassificationNumber = "00002" }
            //     );
            // appDbContext.SaveChanges();
        }
        else Console.WriteLine("--> We already have data");
    }

    // public static bool HasRecord<T>(IReadOnlyCollection<T> items) where T : class, IEntity
    // {
    //     if (null == items) return false;
    //     if (items.Count == 0) return false;
    //     return true;
    // }

    // public static bool Exists<TR, T>(TR repository, T item)
    //  where T : class, IEntity
    //  where TR : class, IRepository<T>
    // {
    //     return (repository.GetAsync(r => r.Id == item.Id) != null);
    // }
}