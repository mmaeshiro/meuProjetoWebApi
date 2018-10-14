namespace MeuProjeto.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basedados_v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        enderecoId = c.Int(nullable: false, identity: true),
                        cep = c.String(nullable: false, maxLength: 8, unicode: false),
                        logradouro = c.String(maxLength: 100, unicode: false),
                        numero = c.String(maxLength: 100, unicode: false),
                        complemento = c.String(maxLength: 100, unicode: false),
                        bairro = c.String(maxLength: 100, unicode: false),
                        cidade = c.String(maxLength: 100, unicode: false),
                        uf = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.enderecoId);
            
            CreateTable(
                "dbo.PessoaFisica",
                c => new
                    {
                        pessoaFisicaId = c.Int(nullable: false, identity: true),
                        cpf = c.String(nullable: false, maxLength: 150, unicode: false),
                        dataNascimento = c.DateTime(nullable: false),
                        nome = c.String(nullable: false, maxLength: 300, unicode: false),
                        sobreNome = c.String(nullable: false, maxLength: 15, unicode: false),
                        enderecoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.pessoaFisicaId)
                .ForeignKey("dbo.Endereco", t => t.enderecoId)
                .Index(t => t.enderecoId);
            
            CreateTable(
                "dbo.PessoaJuridica",
                c => new
                    {
                        pessoJuridicaId = c.Int(nullable: false, identity: true),
                        cnpj = c.String(nullable: false, maxLength: 150, unicode: false),
                        razaoSocial = c.String(maxLength: 100, unicode: false),
                        nomeFantasia = c.String(maxLength: 100, unicode: false),
                        enderecoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.pessoJuridicaId)
                .ForeignKey("dbo.Endereco", t => t.enderecoId)
                .Index(t => t.enderecoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PessoaJuridica", "enderecoId", "dbo.Endereco");
            DropForeignKey("dbo.PessoaFisica", "enderecoId", "dbo.Endereco");
            DropIndex("dbo.PessoaJuridica", new[] { "enderecoId" });
            DropIndex("dbo.PessoaFisica", new[] { "enderecoId" });
            DropTable("dbo.PessoaJuridica");
            DropTable("dbo.PessoaFisica");
            DropTable("dbo.Endereco");
        }
    }
}
