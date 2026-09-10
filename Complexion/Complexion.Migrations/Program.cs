var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
    ?? throw new InvalidOperationException("No connection string provided.");

Complexion.Migrations.MigrationRunner.Run(connectionString);