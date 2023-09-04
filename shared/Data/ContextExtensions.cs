using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared.Data
{
    public static class ContextExtensions
    {
        public static void EnsureDatabaseMigration(this DbContext context)
        {
            Policy retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetry(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, ctx) =>
                    {
                        Console.WriteLine($"Database is currently not available. Exception: {exception}");
                    }
                );

            retryPolicy.Execute(() =>
            {
                Console.WriteLine("Check database migration.");
                context.Database.Migrate();
            });
        }
    }
}
