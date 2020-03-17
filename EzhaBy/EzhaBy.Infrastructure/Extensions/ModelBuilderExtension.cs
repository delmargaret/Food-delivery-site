using EzhaBy.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EzhaBy.Infrastructure.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasData(
                new Tag[]
                {
                    new Tag{ Id = Guid.NewGuid(), TagName = "First" },
                    new Tag{ Id = Guid.NewGuid(), TagName = "Second" },
                }
            );
        }
    }
}
