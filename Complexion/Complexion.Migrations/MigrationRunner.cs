using DbUp;
using System.Reflection;

namespace Complexion.Migrations;

public class MigrationRunner
{
    public static void Run(string connectionString)
    {
        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
            throw new Exception("Migration failed", result.Error);
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Migration successful!");
        Console.ResetColor();
    }
}