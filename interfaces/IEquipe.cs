using System.Collections.Generic;
using EPlayersAspNetCore.Models;

namespace EPlayersAspNetCore.interfaces
{
    public interface IEquipe
    {
         //Metodos de CRUD - Contrato
         void Create(Equipe e);

         List<Equipe> ReadAll();
         void  Update(Equipe e);
         void Delete(int id);
    }
}