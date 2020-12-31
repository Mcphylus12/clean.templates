using Business;
using Microsoft.EntityFrameworkCore;
using System;

namespace DB.Tests.DBHelpers
{
    public class InMemoryDb : ExampleContext
    {
        private static Lazy<InMemoryDb> DB => new Lazy<InMemoryDb>(() =>
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();

            builder.UseInMemoryDatabase("test");

            return new InMemoryDb(builder.Options);
        });

        private InMemoryDb(DbContextOptions options)
            : base(options)
        {
        }

        public static IRepository<T> GetRepository<T>()
            where T : class
        {
            return new Repository<T>(DB.Value);
        }
    }
}
