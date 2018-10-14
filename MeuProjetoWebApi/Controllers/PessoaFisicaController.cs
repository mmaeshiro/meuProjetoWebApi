using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using MeuProjeto.Dominio;
using MeuProjeto.Infra.DataContext;

namespace MeuProjetoWebApi.Controllers
{

    [EnableCors(origins:"*", headers:"*", methods:"*")]
    [RoutePrefix("api/v1/public")]
    public class PessoaFisicaController : ApiController
    {
        private MeuProjetoDataContext db = new MeuProjetoDataContext();

        // PessoaFisica/ObterTodos
        [Route("ObterPessoaFisica")]
        public HttpResponseMessage GetPessoaFisica()
        {
            try
            {
                var resultado = db.pessoaFisicas.Include("Endereco").ToList();

                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter todas Pessoa Física");
            }

        }

        [Route("ObterPorIdPessoaFisica/{id}")]
        public HttpResponseMessage GetPessoaFisica(int id)
        {
            try
            {
                var resultado = db.pessoaFisicas.Include("Endereco").Where(x => x.pessoaFisicaId == id).FirstOrDefault();

                if (resultado == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);


                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter id Pessoa Física");
            }
        }

        [HttpPost]
        [Route("GravarPessoaFisica")]
        public HttpResponseMessage PostPessoaFisica(PessoaFisica pessoaFisica)
        {

            if (pessoaFisica == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.pessoaFisicas.Add(pessoaFisica);
                db.SaveChanges();

                var resultado = pessoaFisica;

                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,"Falha ao incluir Pessoa Física");
            }
        }

        [HttpPatch]
        [Route("AlterarPessoaFisica")]
        public HttpResponseMessage PatchPessoaFisica(PessoaFisica pessoaFisica)
        {

            if (pessoaFisica == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.Entry<PessoaFisica>(pessoaFisica).State = EntityState.Modified;

                db.Entry<Endereco>(pessoaFisica.Endereco).State = EntityState.Modified;

                db.SaveChanges();

                var resultado = pessoaFisica;

                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir Pessoa Física");
            }
        }

        [HttpPut]
        [Route("AlterarPessoaFisica2")]
        public HttpResponseMessage PutPessoaFisica(PessoaFisica pessoaFisica)
        {

            if (pessoaFisica == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                db.Entry<PessoaFisica>(pessoaFisica).State = EntityState.Modified;

                db.Entry<Endereco>(pessoaFisica.Endereco).State = EntityState.Modified;

                db.SaveChanges();

                var resultado = pessoaFisica;

                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir Pessoa Física");
            }
        }

        [HttpDelete]
        [Route("ExcluirPessoaFisica/{id}")]
        public HttpResponseMessage DeletePessoaFisica(int id)
        {

            if (id <= 0)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {

                //db.pessoaFisicas.Remove(db.pessoaFisicas.Find(pessoaFisicaId));     

                var pessoaFisicaDb = db.pessoaFisicas.Include("Endereco").Where(x => x.pessoaFisicaId == id).FirstOrDefault();

                db.enderecos.Remove(pessoaFisicaDb.Endereco);

                db.pessoaFisicas.Remove(pessoaFisicaDb);              

                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Pessoa Física Excluida");
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao Excluida Pessoa Física");
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