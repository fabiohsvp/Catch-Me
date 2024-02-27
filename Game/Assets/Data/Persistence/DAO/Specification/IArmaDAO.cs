using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Persistence.DAO.Specification

{

    public interface IArmaDAO
    {
        ISQliteConnectionProvider ConnectionProvider { get; }
        bool SetArma(Arma arma);
        bool UpdateArma(Arma arma);
        bool DeleteArma(int id);
        Arma GetArma(int id);

    }
}
