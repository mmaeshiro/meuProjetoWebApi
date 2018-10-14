using MeuProjeto.Dominio;
using MeuProjeto.Infra.Mapeamento;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MeuProjeto.Infra.DataContext
{
    public class MeuProjetoDataContext : DbContext
    {
        public MeuProjetoDataContext()
            : base("MinhaConnectionString")
        {            
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Endereco> enderecos { get; set; }
        public DbSet<PessoaFisica> pessoaFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoaJuridicas { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties()
             .Where(p => p.Name == p.ReflectedType.Name + "Id")
             .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new EnderecoMap());
            modelBuilder.Configurations.Add(new PessoaFisicaMap());
            modelBuilder.Configurations.Add(new PessoaJuridicaMap());

        }

    }

}
