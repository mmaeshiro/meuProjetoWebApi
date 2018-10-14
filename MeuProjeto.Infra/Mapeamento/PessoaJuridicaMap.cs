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
    public class PessoaJuridicaMap : EntityTypeConfiguration<PessoaJuridica>
    {

        public PessoaJuridicaMap()
        {
            ToTable("PessoaJuridica");

            HasKey(pj => pj.pessoJuridicaId).Property(pj => pj.pessoJuridicaId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(pj => pj.cnpj).HasMaxLength(150).IsRequired();

            HasRequired(pj => pj.Endereco);

        }

    }
}
