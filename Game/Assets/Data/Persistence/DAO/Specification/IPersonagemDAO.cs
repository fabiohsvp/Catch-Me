using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Persistence.DAO.Specification


{

    public interface IPersonagemDAO

    {

        ISQliteConnectionProvider ConnectionProvider { get; }

        bool SetPersonagem(Personagem personagem);
        bool UpdatePersonagem(Personagem personagem);
        bool DeletePersonagem(int id);
        Personagem GetPersonagem(int id);



    }
}