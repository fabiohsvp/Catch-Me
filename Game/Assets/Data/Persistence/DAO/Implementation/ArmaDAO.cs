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
    public class ArmaDAO : IArmaDAO
    {
        public ArmaDAO(ISQliteConnectionProvider connectionProvider) => ConnectionProvider = connectionProvider;

        public ISQliteConnectionProvider ConnectionProvider { get; protected set; }
        public bool DeleteArma(int id)
        {

            using (var connection = ConnectionProvider.Connection)
            {
                var commandText = "DELETE FROM Arma WHERE Id = @id;";
                connection.Open();

                using (var command = connection.CreateCommand())
                {


                    {
                        command.CommandText = commandText;
                        command.Parameters.AddWithValue("@id", id);
                        return command.ExecuteNoQueryWithFK() > 0;


                    }


                }
            }
        }

        public Arma GetArma(int id)
        {
            var commandText = "SELECT * FROM Arma where Id = @id;";
            Arma arma = null;

            using (var connection = ConnectionProvider.Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())

                {
                    command.CommandText = commandText;
                    command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        /*arma = new Arma();
                        arma.Id = reader.GetInt32(0);
                        arma.Nome = reader.GetString(1);
                        arma.Ataque = reader.GetInt32(2);
                        arma.Preco = reader.GetDouble(3); */
                        Debug.Log("\tid:" + reader["id"] + "nome:" + reader["nome"] + "\tataque:" + reader["ataque"] + "\tpreco:" + reader["preco"]);

                    }
                }
                return arma;
            }
        }



        public bool SetArma(Arma arma)

        {
            var commandText = "INSERT INTO Arma (Nome, Ataque, Preco) VALUES (@nome,@ataque,@preco);";

            using (var connection = ConnectionProvider.Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;

                    command.Parameters.AddWithValue("@nome", arma.Nome);
                    command.Parameters.AddWithValue("@ataque", arma.Ataque);
                    command.Parameters.AddWithValue("@defesa", arma.Preco);
                    return command.ExecuteNoQueryWithFK() > 0;

                }
            }
        }



        public bool UpdateArma(Arma arma)
        {
            var commandText = "UPDATE Arma SET " +
             "Nome = @nome," +
             "Ataque = @ataque," +
             "Preco = @preco" +
             "WHERE Id=@id;";

            using (var connection = ConnectionProvider.Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {


                    command.CommandText = commandText;
                    command.Parameters.AddWithValue("@id", arma.Id);
                    command.Parameters.AddWithValue("@nome", arma.Nome);
                    command.Parameters.AddWithValue("@ataque", arma.Ataque);
                    command.Parameters.AddWithValue("@Preco", arma.Preco);
                    return command.ExecuteNoQueryWithFK() > 0;
                    //Debug.Log("UPDATE PERSONAGEM"); 
                }
            }
        }
    }

}