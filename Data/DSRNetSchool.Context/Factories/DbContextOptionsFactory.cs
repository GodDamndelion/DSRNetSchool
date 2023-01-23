namespace DSRNetSchool.Context;

using Microsoft.EntityFrameworkCore;

public static class DbContextOptionsFactory
{
    private const string migrationProjctPrefix = "DSRNetSchool.Context.Migrations"; //Название проекта, в который будут складываться миграции

    public static DbContextOptions<MainDbContext> Create(string connStr, DbType dbType)
    {
        var bldr = new DbContextOptionsBuilder<MainDbContext>();

        Configure(connStr, dbType).Invoke(bldr);

        return bldr.Options;
    }

    public static Action<DbContextOptionsBuilder> Configure(string connStr, DbType dbType)
    {
        return (bldr) =>
        {
            switch (dbType)
            {
                case DbType.MSSQL:
                    bldr.UseSqlServer(connStr,
                        opts => opts
                                .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                                .MigrationsHistoryTable("_EFMigrationsHistory", "public")
                                .MigrationsAssembly($"{migrationProjctPrefix}{DbType.MSSQL}") //Путь для складывания миграций
                        );
                    break;

                case DbType.PostgreSQL:
                    bldr.UseNpgsql(connStr,
                        opts => opts
                                .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                                .MigrationsHistoryTable("_EFMigrationsHistory", "public")
                                .MigrationsAssembly($"{migrationProjctPrefix}{DbType.PostgreSQL}") //И тут путь для складывания миграций
                        );
                    break;
            }

            bldr.EnableSensitiveDataLogging(); //Будут проскакивать параметры в запросах, чтобы удобнее отлаживать (А не просто что-то выполнилось)
            //bldr.UseLazyLoadingProxies(); //Не подружились
            bldr.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking); //Чтобы ничего лишнего не хранить в памяти
        };
    }
}
