using Assets.Scripts.Persistence.DAO.Specification;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.TextCore.Text;
using System;

namespace Assets.Scripts.Persistence.DAO.Implementation
{
    public class QueryDAO : IQueryDAO
    {
        public QueryDAO()
        {
        }

        public QueryDAO(ISQliteConnectionProvider connectionProvider) => ConnectionProvider = connectionProvider;

        public ISQliteConnectionProvider ConnectionProvider { get; protected set; }

        public void ArmaFULLPersonagem()
        {
            throw new NotImplementedException();
        }

        public void ArmaIdPersonagem(int id)
        {
            throw new NotImplementedException();
        }

        // ARMAS E RESPECTIVOS PERSONAGEMES QUE A UTILIZAM
        public void ArmaINNERPersonagem()
        {
            var commandText = "SELECT * FROM Arma INNER JOIN Personagem on Arma.Id=Personagem.ArmaId";
            //Arma arma = null;

            using (var connection = ConnectionProvider.Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())

                {
                    command.CommandText = commandText;
                    // command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        /*arma = new Arma();
                        
                        arma.Id = reader.GetInt32(0);
                        arma.Nome = reader.GetString(1);
                        arma.Ataque = reader.GetInt32(2);
                        arma.Preco = reader.GetDouble(3); */
                        Debug.Log("\tid:" + reader["id"] + "nome:" + reader["nome"] + "\tataque:" + reader["ataque"] + "\tpreco:" + reader["preco"] + "\tarmaId:" + reader["armaId"] + "\thealth:" + reader["health"]);

                    }
                    reader.Close();
                }
                connection.Close();
            }
        }

        // AQUI IMPLEMENTA-SE O LEFT SUPONDO QUE HAJA ALGUMA ARMA SEM PERSONAGEM
        public void ArmaLEFTPersonagem()
        {
            var commandText = "SELECT * FROM Arma LEFT JOIN Personagem on Arma.Id=Personagem.ArmaId";
            //Arma arma = null;

            using (var connection = ConnectionProvider.Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())

                {
                    command.CommandText = commandText;
                    // command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        /*arma = new Arma();
                        
                        arma.Id = reader.GetInt32(0);
                        arma.Nome = reader.GetString(1);
                        arma.Ataque = reader.GetInt32(2);
                        arma.Preco = reader.GetDouble(3); */
                        Debug.Log("\tid:" + reader["id"] + "nome:" + reader["nome"] + "\tataque:" + reader["ataque"] + "\tpreco:" + reader["preco"] + "\tarmaId:" + reader["armaId"] + "\thealth:" + reader["health"]);

                    }
                    reader.Close();
                }
                connection.Close();
            }
        }


    }
}