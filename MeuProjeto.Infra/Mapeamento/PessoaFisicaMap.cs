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
    public class PessoaFisicaMap : EntityTypeConfiguration<PessoaFisica>
    {
        public PessoaFisicaMap()
        {
            ToTable("PessoaFisica");

            HasKey(pf => pf.pessoaFisicaId).Property(pf => pf.pessoaFisicaId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(pf => pf.cpf).HasMaxLength(150).IsRequired();

            Property(pf => pf.nome).HasMaxLength(300).IsRequired();

            Property(pf => pf.sobreNome).HasMaxLength(15).IsRequired();

            HasRequired(pf => pf.Endereco);
        }

    }
}
