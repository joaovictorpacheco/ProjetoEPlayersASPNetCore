using System.Collections.Generic;
using System.IO;

namespace EPlayersAspNetCore.Models
{
    public class EplayersBase
    {
        public void CreateFolderAndFile(string _path)
        {
            //Database/Equipe.csv
            string folder = _path.Split("/")[0];
            

            if(!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if(!File.Exists(_path))
            {
                File.Create(_path);
            }
        }

        public List<string> ReadAllLinesCSV(string path)
        {
            List<string> linhas = new List<string>();
            //responsavel por abrir e fechar o arquivo automaticamente
            // StramReader -> Ler dados de um arquivo
            using(StreamReader file = new StreamReader(path))
            {
                string linha;

                //Percorrer as linhas com um laço while
                while( (linha = file.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

            return linhas;
        }

        public void RewriteCSV(string path, List<string> linhas)
        {
            //SteamWriter -> Escrever dados em um arquivo
            using(StreamWriter output = new StreamWriter(path))
            {
                foreach (var item in linhas)
                {
                    output.Write(item + '\n');
                }
            }
        }
    }
}