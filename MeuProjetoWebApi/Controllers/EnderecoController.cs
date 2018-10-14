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
    public class EnderecoController : ApiController
    {
        private MeuProjetoDataContext db = new MeuProjetoDataContext();

        [Route("ObterPorCepEndereco/{cep}")]
        public HttpResponseMessage GetEndereco(string cep)
        {
            try
            {
               
                var resultado = db.enderecos.Find(cep);               

                if (resultado == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);


                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao obter id Pessoa Física");
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