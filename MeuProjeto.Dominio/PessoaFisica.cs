using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuProjeto.Dominio
{
    public class PessoaFisica
    {

        public PessoaFisica()
        {
            this.dataNascimento = DateTime.Now;
        }

        public int pessoaFisicaId { get; set; }

        public string cpf { get; set; }

        public DateTime dataNascimento { get; set; }

        public string nome { get; set; }

        public string sobreNome { get; set; }

        public int enderecoId { get; set; }

        public virtual Endereco Endereco { get; set; }
    }
}
