using MeuProjeto.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuProjeto.Infra.Mapeamento
{
    public class EnderecoMap: EntityTypeConfiguration<Endereco>
    {
        public EnderecoMap()
        {
            ToTable("Endereco");

            HasKey(e => e.enderecoId).Property(e => e.enderecoId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(e => e.cep).HasMaxLength(8).IsRequired();

        }

    }
}
