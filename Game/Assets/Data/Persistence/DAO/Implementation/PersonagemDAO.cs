using Assets.Scripts.Persistence.DAO.Specification;
using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Persistence.DAO.Implementation
{
    public class PersonagemDAO : IPersonagemDAO

    {
        public ISQliteConnectionProvider ConnectionProvider { get; set; }
        public PersonagemDAO(ISQliteConnectionProvider connectionProvider) => ConnectionProvider = connectionProvider;


        public bool DeletePersonagem(int id)
        {
            using (var connection = ConnectionProvider.Connection)
            {
                var commandText = "DELETE FROM personagem WHERE Id = @id;";
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



        public Personagem GetPersonagem(int id)
        {

            var commandText = "SELECT * FROM Personagem where Id = @id;";
            Personagem personagem = null;

            //using (IDataReader reader = command.ExecuteReader())

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
                         personagem = new Personagem();
                         personagem.Id = reader.GetInt32(0);
                         personagem.Nome = reader.GetString(1);
                         personagem.Ataque = reader.GetInt32(2);
                         personagem.Defesa = reader.GetInt32(3);
                         personagem.Agilidade = reader.GetInt32(4);
                         personagem.Health = reader.GetInt32(5);
                         personagem.ArmaId = reader.GetInt32(6);

                        Debug.Log("\tid:" + reader["id"] + "nome:" + reader["nome"] + "\tataque:" + reader["ataque"] + "\tdefesa:" + reader["defesa"] + "\tagilidade:" + reader["agilidade"] + "\thealth:" + reader["health"] + "\tArmaId:" + reader["armaId"]);

                    }
                }
                return personagem;
            }
        }


        public bool SetPersonagem(Personagem personagem)
        {

            var commandText = "PRAGMA foreign_keys=true;" + "INSERT INTO Personagem (Nome, Ataque, Defesa, Agilidade, Health, ArmaId) VALUES(@nome,@ataque, @defesa, @agilidade, @health, @armaId);Select cast(Scope_identy) AS int";

            using (var connection = ConnectionProvider.Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;

                    command.Parameters.AddWithValue("@nome", personagem.Nome);
                    command.Parameters.AddWithValue("@ataque", personagem.Ataque);
                    command.Parameters.AddWithValue("@defesa", personagem.Defesa);
                    command.Parameters.AddWithValue("@agilidade", personagem.Agilidade);
                    command.Parameters.AddWithValue("@health", personagem.Health);
                    command.Parameters.AddWithValue("@armaId", personagem.ArmaId);
                    return command.ExecuteNoQueryWithFK() > 0;
                    //personagem.Id = (int)command.ExecuteScalar();

                }
            }
        }


        public bool UpdatePersonagem(Personagem personagem)
        {
            var commandText = "UPDATE Personagem  SET " +
             "Nome = @nome," +
             "Ataque = @ataque," +
             "Defesa = @defesa," +
             "Agilidade = agilidade," +
             "Health = @health," +
             "ArmaId = armaId " +
             "WHERE Id=@id;";

            using (var connection = ConnectionProvider.Connection)
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.Parameters.AddWithValue("@id", personagem.Id);
                    command.Parameters.AddWithValue("@nome", personagem.Nome);
                    command.Parameters.AddWithValue("@ataque", personagem.Ataque);
                    command.Parameters.AddWithValue("@defesa", personagem.Defesa);
                    command.Parameters.AddWithValue("@agilidade", personagem.Agilidade);
                    command.Parameters.AddWithValue("@health", personagem.Health);
                    command.Parameters.AddWithValue("@armaId", personagem.ArmaId);
                    return command.ExecuteNoQueryWithFK() > 0;
                    //Debug.Log("UPDATE PERSONAGEM) 


                }
            }
        }


    }
}