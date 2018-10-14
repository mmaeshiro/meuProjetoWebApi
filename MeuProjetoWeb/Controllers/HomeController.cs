using MeuProjeto.Dominio;
using MeuProjetoWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MeuProjetoWeb.Controllers
{
    public class HomeController : Controller
    {

        const string url = "http://localhost:59970";

        // GET: Home
        public ActionResult Index()
        {
            var listaPessoaJuridica = new List<PessoaJuridica>();

            var listaPessoaFisica = new List<PessoaFisica>();

            var ListaPessoaFisJurViewModel = new List<PessoaJurFisViewModel>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("Aplication/json");

                client.DefaultRequestHeaders.Accept.Add(contentType);

                HttpResponseMessage response = client.GetAsync("api/v1/public/ObterPessoaFisica").Result;

                if (response.IsSuccessStatusCode)
                {
                    var retorno = response.Content.ReadAsStringAsync().Result;

                    listaPessoaFisica = JsonConvert.DeserializeObject<List<PessoaFisica>>(retorno);
                };

                foreach (var pessoaFisica in listaPessoaFisica)
                {
                    var PessoaFisJurViewModel = new PessoaJurFisViewModel();

                    PessoaFisJurViewModel.id = pessoaFisica.pessoaFisicaId;
                    PessoaFisJurViewModel.tipoPessoa = "física";
                    PessoaFisJurViewModel.cpfCnpj = pessoaFisica.cpf;
                    PessoaFisJurViewModel.nome = string.Format("{0} {1}", pessoaFisica.nome, pessoaFisica.sobreNome);
                    PessoaFisJurViewModel.cep = pessoaFisica.Endereco.cep.Replace("-", "");
                    PessoaFisJurViewModel.logradouro = pessoaFisica.Endereco.logradouro;
                    PessoaFisJurViewModel.numero = pessoaFisica.Endereco.numero;
                    PessoaFisJurViewModel.bairro = pessoaFisica.Endereco.bairro;
                    PessoaFisJurViewModel.cidade = pessoaFisica.Endereco.cidade;
                    PessoaFisJurViewModel.uf = pessoaFisica.Endereco.uf;

                    ListaPessoaFisJurViewModel.Add(PessoaFisJurViewModel);
                };

                HttpResponseMessage response1 = client.GetAsync("api/v1/public/ObterPessoaJuridica").Result;

                if (response1.IsSuccessStatusCode)
                {
                    var retorno = response1.Content.ReadAsStringAsync().Result;

                    listaPessoaJuridica = JsonConvert.DeserializeObject<List<PessoaJuridica>>(retorno);
                };

                foreach (var pessoaJuridica in listaPessoaJuridica)
                {
                    var PessoaFisJurViewModel = new PessoaJurFisViewModel();

                    PessoaFisJurViewModel.id = pessoaJuridica.pessoJuridicaId;
                    PessoaFisJurViewModel.tipoPessoa = "jurídica";
                    PessoaFisJurViewModel.cpfCnpj = pessoaJuridica.cnpj;
                    PessoaFisJurViewModel.nome = string.Format("{0}/{1}", pessoaJuridica.nomeFantasia, pessoaJuridica.razaoSocial);
                    PessoaFisJurViewModel.cep = pessoaJuridica.Endereco.cep.Replace("-", "");
                    PessoaFisJurViewModel.logradouro = pessoaJuridica.Endereco.logradouro;
                    PessoaFisJurViewModel.numero = pessoaJuridica.Endereco.numero;
                    PessoaFisJurViewModel.bairro = pessoaJuridica.Endereco.bairro;
                    PessoaFisJurViewModel.cidade = pessoaJuridica.Endereco.cidade;
                    PessoaFisJurViewModel.uf = pessoaJuridica.Endereco.uf;

                    ListaPessoaFisJurViewModel.Add(PessoaFisJurViewModel);
                };

                ListaPessoaFisJurViewModel.OrderBy(x => x.id).ToList();

                return View(ListaPessoaFisJurViewModel);
            }

        }

        // GET: Home/Create
        public ActionResult Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem
            {
                Text = "Selecione",
                Value = "0",
                Selected = true
            });
            items.Add(new SelectListItem
            { Text = "Física", Value = "1" });
            items.Add(new SelectListItem
            { Text = "Jurídica", Value = "2" });

            ViewBag.TipoPessoa = items;

            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("Aplication/json");

                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    PessoaFisica pessoaFisica = new PessoaFisica();

                    if (formCollection["tipoPessoa"] == "1")
                    {

                        pessoaFisica.cpf = formCollection["PessoaFisica.cpf"];
                        pessoaFisica.nome = formCollection["PessoaFisica.nome"];
                        pessoaFisica.sobreNome = formCollection["PessoaFisica.sobreNome"];
                        pessoaFisica.dataNascimento = Convert.ToDateTime(formCollection["PessoaFisica.dataNascimento"]);
                        pessoaFisica.Endereco = new Endereco
                        {
                            cep = formCollection["Endereco.cep"].Replace("-", ""),
                            bairro = formCollection["Endereco.bairro"],
                            cidade = formCollection["Endereco.cidade"],
                            logradouro = formCollection["Endereco.logradouro"],
                            numero = formCollection["Endereco.numero"],
                            complemento = formCollection["Endereco.complemento"],
                            uf = formCollection["Endereco.uf"],
                        };

                        var stringContent = new StringContent(JsonConvert.SerializeObject(pessoaFisica), Encoding.UTF8, "application/json");

                        HttpResponseMessage response = client.PostAsync("api/v1/public/GravarPessoaFisica", stringContent).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var retorno = response.Content.ReadAsStringAsync().Result;
                        };

                    };

                    var pessoaJuridica = new PessoaJuridica();

                    if (formCollection["tipoPessoa"] == "2")
                    {

                        pessoaJuridica.cnpj = formCollection["PessoaJuridica.cnpj"];
                        pessoaJuridica.nomeFantasia = formCollection["PessoaJuridica.nomeFantasia"];
                        pessoaJuridica.razaoSocial = formCollection["PessoaJuridica.razaoSocial"];
                        pessoaJuridica.Endereco = new Endereco
                        {
                            cep = formCollection["Endereco.cep"].Replace("-", ""),
                            bairro = formCollection["Endereco.bairro"],
                            cidade = formCollection["Endereco.cidade"],
                            logradouro = formCollection["Endereco.logradouro"],
                            numero = formCollection["Endereco.numero"],
                            complemento = formCollection["Endereco.complemento"],
                            uf = formCollection["Endereco.uf"],
                        };

                        var stringContent1 = new StringContent(JsonConvert.SerializeObject(pessoaJuridica), Encoding.UTF8, "application/json");

                        HttpResponseMessage response1 = client.PostAsync("api/v1/public/GravarPessoaJuridica", stringContent1).Result;

                        if (response1.IsSuccessStatusCode)
                        {
                            var retorno = response1.Content.ReadAsStringAsync().Result;
                        };

                    };

                };

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id, string tipoPessoa)
        {

            var PessoaFisJurViewModel = new PessoaJurFisViewModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("Aplication/json");

                if (tipoPessoa.ToLower() == "física")
                {

                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    HttpResponseMessage response = client.GetAsync("api/v1/public/ObterPorIdPessoaFisica/" + JsonConvert.SerializeObject(id)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var retorno = response.Content.ReadAsStringAsync().Result;

                        PessoaFisica pessoaFisica = JsonConvert.DeserializeObject<PessoaFisica>(retorno);

                        PessoaFisJurViewModel.id = pessoaFisica.pessoaFisicaId;
                        PessoaFisJurViewModel.enderecoId = pessoaFisica.enderecoId;
                        PessoaFisJurViewModel.tipoPessoa = "física";
                        PessoaFisJurViewModel.cpfCnpj = pessoaFisica.cpf;
                        PessoaFisJurViewModel.nome = pessoaFisica.nome;
                        PessoaFisJurViewModel.sobreNome = pessoaFisica.sobreNome;
                        PessoaFisJurViewModel.dataNascimento = pessoaFisica.dataNascimento.ToShortDateString();
                        PessoaFisJurViewModel.cep = pessoaFisica.Endereco.cep.Replace("-", "");
                        PessoaFisJurViewModel.logradouro = pessoaFisica.Endereco.logradouro;
                        PessoaFisJurViewModel.complemento = pessoaFisica.Endereco.complemento;
                        PessoaFisJurViewModel.numero = pessoaFisica.Endereco.numero;
                        PessoaFisJurViewModel.bairro = pessoaFisica.Endereco.bairro;
                        PessoaFisJurViewModel.cidade = pessoaFisica.Endereco.cidade;
                        PessoaFisJurViewModel.uf = pessoaFisica.Endereco.uf;

                    };

                };

                if (tipoPessoa.ToLower() == "jurídica")
                {
                    var pessoaJuridica = new PessoaJuridica();

                    HttpResponseMessage response1 = client.GetAsync("api/v1/public/ObterPorIdPessoaJuridica/" + JsonConvert.SerializeObject(id)).Result;

                    if (response1.IsSuccessStatusCode)
                    {
                        var retorno = response1.Content.ReadAsStringAsync().Result;

                        pessoaJuridica = JsonConvert.DeserializeObject<PessoaJuridica>(retorno);

                        PessoaFisJurViewModel.id = pessoaJuridica.pessoJuridicaId;
                        PessoaFisJurViewModel.enderecoId = pessoaJuridica.enderecoId;
                        PessoaFisJurViewModel.tipoPessoa = "jurídica";
                        PessoaFisJurViewModel.cpfCnpj = pessoaJuridica.cnpj;
                        PessoaFisJurViewModel.nome = pessoaJuridica.nomeFantasia;
                        PessoaFisJurViewModel.razaoSocial = pessoaJuridica.razaoSocial;
                        PessoaFisJurViewModel.cep = pessoaJuridica.Endereco.cep.Replace("-", "");
                        PessoaFisJurViewModel.logradouro = pessoaJuridica.Endereco.logradouro;
                        PessoaFisJurViewModel.complemento = pessoaJuridica.Endereco.complemento;
                        PessoaFisJurViewModel.numero = pessoaJuridica.Endereco.numero;
                        PessoaFisJurViewModel.bairro = pessoaJuridica.Endereco.bairro;
                        PessoaFisJurViewModel.cidade = pessoaJuridica.Endereco.cidade;
                        PessoaFisJurViewModel.uf = pessoaJuridica.Endereco.uf;
                    };
                };

            };

            return View(PessoaFisJurViewModel);

        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection)
        {

            try
            {

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("Aplication/json");

                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    PessoaFisica pessoaFisica = new PessoaFisica();

                    if (formCollection["tipoPessoa"].ToLower() == "física")
                    {

                        pessoaFisica.pessoaFisicaId = Convert.ToInt32(formCollection["id"]);
                        pessoaFisica.cpf = formCollection["cpfCnpj"];
                        pessoaFisica.nome = formCollection["nome"];
                        pessoaFisica.sobreNome = formCollection["sobreNome"];
                        pessoaFisica.dataNascimento = Convert.ToDateTime(formCollection["dataNascimento"]);
                        pessoaFisica.enderecoId = Convert.ToInt32(formCollection["enderecoId"]);
                        pessoaFisica.Endereco = new Endereco
                        {
                            enderecoId = Convert.ToInt32(formCollection["enderecoId"]),
                            cep = formCollection["cep"].Replace("-", ""),
                            bairro = formCollection["bairro"],
                            cidade = formCollection["cidade"],
                            logradouro = formCollection["logradouro"],
                            numero = formCollection["numero"],
                            complemento = formCollection["complemento"],
                            uf = formCollection["uf"],
                        };


                        var stringContent = new StringContent(JsonConvert.SerializeObject(pessoaFisica), Encoding.UTF8, "application/json");

                        HttpResponseMessage response = client.PutAsync("api/v1/public/AlterarPessoaFisica2", stringContent).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var retorno = response.Content.ReadAsStringAsync().Result;
                        };

                    };

                    var pessoaJuridica = new PessoaJuridica();

                    if (formCollection["tipoPessoa"].ToLower() == "jurídica")
                    {
                        pessoaJuridica.pessoJuridicaId = Convert.ToInt32(formCollection["id"]);
                        pessoaJuridica.cnpj = formCollection["cpfCnpj"];
                        pessoaJuridica.nomeFantasia = formCollection["nome"];
                        pessoaJuridica.razaoSocial = formCollection["razaoSocial"];
                        pessoaJuridica.enderecoId = Convert.ToInt32(formCollection["enderecoId"]);
                        pessoaJuridica.Endereco = new Endereco
                        {
                            enderecoId = Convert.ToInt32(formCollection["enderecoId"]),
                            cep = formCollection["cep"].Replace("-", ""),
                            bairro = formCollection["bairro"],
                            cidade = formCollection["cidade"],
                            logradouro = formCollection["logradouro"],
                            numero = formCollection["numero"],
                            complemento = formCollection["complemento"],
                            uf = formCollection["uf"],
                        };

                        var stringContent1 = new StringContent(JsonConvert.SerializeObject(pessoaJuridica), Encoding.UTF8, "application/json");

                        HttpResponseMessage response1 = client.PutAsync("api/v1/public/AlterarPessoaJuridica2", stringContent1).Result;

                        if (response1.IsSuccessStatusCode)
                        {
                            var retorno = response1.Content.ReadAsStringAsync().Result;
                        };

                    };

                };

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        public ActionResult Delete(int id, string tipoPessoa)
        {
            var PessoaFisJurViewModel = new PessoaJurFisViewModel();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("Aplication/json");

                if (tipoPessoa.ToLower() == "física")
                {
                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(contentType);

                    HttpResponseMessage response = client.DeleteAsync("api/v1/public/ExcluirPessoaFisica/"+ JsonConvert.SerializeObject(id)).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var retorno = response.Content.ReadAsStringAsync().Result;                       
                    };

                };

                if (tipoPessoa.ToLower() == "jurídica")
                {
                    var pessoaJuridica = new PessoaJuridica();

                    HttpResponseMessage response1 = client.DeleteAsync("api/v1/public/ExcluirPessoaJuridica/" + JsonConvert.SerializeObject(id)).Result;

                    if (response1.IsSuccessStatusCode)
                    {
                        var retorno = response1.Content.ReadAsStringAsync().Result;                     
                    };
                };

            };

            return RedirectToAction("Index");

        }

        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
