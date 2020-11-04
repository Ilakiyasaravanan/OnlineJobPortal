namespace JobPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JobPortal.DAL.DBUtills>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "JobPortal.DAL.DBUtills";
        }

        protected override void Seed(JobPortal.DAL.DBUtills context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
