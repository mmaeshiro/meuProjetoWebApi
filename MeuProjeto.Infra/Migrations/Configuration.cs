using System.Data.Entity.Migrations;

namespace MeuProjeto.Infra.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MeuProjeto.Infra.DataContext.MeuProjetoDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MeuProjeto.Infra.DataContext.MeuProjetoDataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
