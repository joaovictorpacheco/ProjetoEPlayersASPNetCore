using System;
using System.IO;
using EPlayersAspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPlayersAspNetCore.Controllers
{
    [Route("Equipe")]

    // http://localhost:5000;Equipe
    public class EquipeController : Controller
    {
        //Criamos uma instancia de equipeModel
        Equipe equipeModel = new Equipe();
        
        // http://localhost:5000;Equipe/Listar
        [Route("Listar")]
        public IActionResult Index()
        {
            // Listamos todas as equipes e enviamos para aView, através da viewBag
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }
        
        // http://localhost:5000;Equipe/Cadastrar
        [Route("Cadastrar")]

        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe   = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse( form["IdEquipe"]);
            novaEquipe.Nome     = form ["Nome"];
            
            //Upload início
            
            //Verificamos se o usuário selecionou o arquivo
            if (form.Files.Count > 0)
            {
                // Recebemos o arquivo que o usuário enviou e armazenamos na variável file
                var file = form.Files[0];
                var folder = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                //Verificamos se o diretorio (pasta) já existe, se não, a criamos
                if (Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                                                  // localhost:5001                      Equipes  imagem.jpg
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                novaEquipe.Imagem  = file.FileName;
            }
            else
            {
                novaEquipe.Imagem = "padrao.png";
            }


            //Chamamos o metodo Create para salvar a nova euipe csv
            equipeModel.Create(novaEquipe);
            //Atualiza a lista de equipes na View
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar");
        }
        
        // http://localhost:5000;Equipe/1
        [Route("(id)")]
        public IActionResult Excluir(int id)
        {
            equipeModel.Delete(id);
            ViewBag.Equipes = equipeModel.ReadAll();
            
            return LocalRedirect("~/Equipe/Listar");
        }
    }
}