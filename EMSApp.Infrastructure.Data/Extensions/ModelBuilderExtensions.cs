using EMSApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Action = EMSApp.Core.Entities.Action;

namespace EMSApp.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedDatabase(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Module>().HasData(

                new Module
                {
                    Id = 1,
                    Name = "Dashboard",
                    CreatedOn = DateTime.Now
                },
                new Module
                {
                    Id = 2,
                    Name = "Fair",
                    CreatedOn = DateTime.Now,

                },
                new Module
                {
                    Id = 3,
                    Name = "Firm",
                    CreatedOn = DateTime.Now,

                }
            );

            modelBuilder.Entity<Page>().HasData(
                 new Page
                 {
                     Id = 1,
                     Name = "view",
                     Url = "dashboard",
                     Icon="Home",
                     Component = "dashboard",
                     FileUrl = "./views/dashboard/analytics/AnalyticsDashboard",
                     ModuleId = 1,
                     CreatedOn = DateTime.Now
                 },
                new Page
                {
                    Id = 2,
                    Name = "list",
                    Url = "fair",
                    Icon = "Layout",
                    Component = "fair",
                    FileUrl = "./views/fair/Fairs",
                    ModuleId = 2,
                    CreatedOn = DateTime.Now
                },
                new Page
                {
                    Id = 3,
                    Name = "list",
                    Url = "firm",
                    Icon="Briefcase",
                    Component = "firm",
                    FileUrl = "./views/fair/Firms",
                    ModuleId = 3,
                    CreatedOn = DateTime.Now
                }
            );

            modelBuilder.Entity<Action>().HasData(
                new Action
                {
                    Id = 7,
                    Name = "view",
                    Url = null,
                    CreatedOn = DateTime.Now,
                    PageId = 1
                },
                new Action
                {
                    Id = 1,
                    Name = "create",
                    Url = "create",
                    CreatedOn = DateTime.Now,
                    PageId = 2
                },
                new Action
                {
                    Id = 2,
                    Name = "edit",
                    Url = "edit",
                    CreatedOn = DateTime.Now,
                    PageId = 2
                },
                new Action
                {
                    Id = 3,
                    Name = "delete",
                    Url = null,
                    CreatedOn = DateTime.Now,
                    PageId = 2
                },
                new Action
                {
                    Id = 4,
                    Name = "create",
                    Url = "create",
                    CreatedOn = DateTime.Now,
                    PageId = 3
                },
                new Action
                {
                    Id = 5,
                    Name = "edit",
                    Url = "edit",
                    CreatedOn = DateTime.Now,
                    PageId = 3
                },
                new Action
                {
                    Id = 6,
                    Name = "delete",
                    Url = null,
                    CreatedOn = DateTime.Now,
                    PageId = 3
                }
            );

        }
    }
}
