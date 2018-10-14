using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using MeuProjeto.Dominio;
using MeuProjeto.Infra.DataContext;

namespace MeuProjetoWebApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/v1/public")]
    public class PessoaJuridicaController : ApiController
    {
        private MeuProjetoDataContext db = new MeuProjetoDataContext();

        // PessoaFisica/ObterTodos
        [Route("ObterPessoaJuridica")]
        public HttpResponseMessage GetPessoaJuridica()
        {
            try
            {
                var resultado = db.PessoaJuridicas.Include("Endereco").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter todas Pessoa Física");
            }

        }

        [Route("ObterPorIdPessoaJuridica/{id}")]
        public HttpResponseMessage GetPessoaJuridica(int id)
        {
            try
            {
                var resultado = db.PessoaJuridicas.Include("Endereco").Where(x => x.pessoJuridicaId == id).FirstOrDefault();

                if (resultado == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, resultado);               
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir Pessoa Jurídica");
            }
        }

        [HttpPost]
        [Route("GravarPessoaJuridica")]
        public HttpResponseMessage PostPessoaJuridica(PessoaJuridica pessoaJuridica)
        {

            if (pessoaJuridica == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.PessoaJuridicas.Add(pessoaJuridica);
                db.SaveChanges();

                var resultado = pessoaJuridica;

                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir Pessoa Jurídica");
            }
        }

        [HttpPatch]
        [Route("AlterarPessoaJuridica")]
        public HttpResponseMessage PatchPessoaJuridica(PessoaJuridica pessoaJuridica)
        {

            if (pessoaJuridica == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.Entry<PessoaJuridica>(pessoaJuridica).State = EntityState.Modified;

                db.Entry<Endereco>(pessoaJuridica.Endereco).State = EntityState.Modified;

                db.SaveChanges();

                var resultado = pessoaJuridica;

                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir Pessoa Juridica");
            }
        }

        [HttpPut]
        [Route("AlterarPessoaJuridica2")]
        public HttpResponseMessage PutPessoaJuridica(PessoaJuridica pessoaJuridica)
        {

            if (pessoaJuridica == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.Entry<PessoaJuridica>(pessoaJuridica).State = EntityState.Modified;

                db.Entry<Endereco>(pessoaJuridica.Endereco).State = EntityState.Modified;

                db.SaveChanges();

                var resultado = pessoaJuridica;

                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir Pessoa Jurídica");
            }
        }

        [HttpDelete]
        [Route("ExcluirPessoaJuridica/{id}")]
        public HttpResponseMessage DeletePessoaJuridica(int id)
        {

            if (id <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                //db.PessoaJuridicas.Remove(db.PessoaJuridicas.Find(pessoaJuridicaId));

                var pessoaJuridicaDb = db.PessoaJuridicas.Include("Endereco").Where(x => x.pessoJuridicaId == id).FirstOrDefault();

                db.enderecos.Remove(pessoaJuridicaDb.Endereco);

                db.PessoaJuridicas.Remove(pessoaJuridicaDb);

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Pessoa Jurídica Excluida");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao Excluida Pessoa Jurídica");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}