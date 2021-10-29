using Microsoft.EntityFrameworkCore;

namespace CSharp.Lessons.DbData;

public class DatabaseContext : DbContext
{
    private const string ConnectionString = "Data Source=localhost;Initial Catalog=Example;Integrated Security=SSPI";

    public DbSet<EFEmployee> EmployeeSet { get; set; } = null!;
    public DbSet<EFEmployeeData> EmployeeDataSet { get; set; } = null!;
    public DbSet<EFEmployeeDataType> EmployeeDataTypeSet { get; set; } = null!;

    public static DbContextOptions<DatabaseContext> GetDbContextOptions(string connectionString) =>
        new DbContextOptionsBuilder<DatabaseContext>()
            .UseSqlServer(connectionString)
            .Options;

    public DatabaseContext() : base(GetDbContextOptions(ConnectionString))
    {
    }
}
