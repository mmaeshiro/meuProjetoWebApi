using MeuProjeto.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeuProjetoWeb.Models
{
    public class PessoaJurFisViewModel
    {  
        public int id { get; set; }

        public int enderecoId { get; set; }

        public string tipoPessoa { get; set; }

        public string cpfCnpj { get; set; }
        public string razaoSocial { get; set; }

        public string nome { get; set; }       

        public string sobreNome { get; set; }

        public string dataNascimento { get; set; }

        public string cep { get; set; }

        public string logradouro { get; set; }

        public string complemento { get; set; }

        public string numero { get; set; }

        public string cidade { get; set; }

        public string bairro { get; set; }

        public string uf { get; set; }

        public PessoaFisica PessoaFisica { get; set; }

        public PessoaJuridica PessoaJuridica { get; set; }

        public Endereco Endereco { get; set; }

    }
}