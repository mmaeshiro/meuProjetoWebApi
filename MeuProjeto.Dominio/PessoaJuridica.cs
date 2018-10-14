using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuProjeto.Dominio
{
    public class PessoaJuridica
    {
        public int pessoJuridicaId { get; set; }

        public string cnpj { get; set; }

        public string razaoSocial { get; set; }

        public string nomeFantasia { get; set; }

        public int enderecoId { get; set; }

        public virtual Endereco Endereco {get;set;}
    }
}
