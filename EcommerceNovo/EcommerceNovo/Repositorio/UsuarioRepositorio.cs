using EcommerceNovo.Models;
using MySql.Data.MySqlClient;
using System.Data;


namespace EcommerceNovo.Repositorio
{
    public class UsuarioRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");


        public Usuario ObterLogin(string email)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new("SELECT * FROM Usuario WHERE Email = @email", conexao);
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    Usuario usuario = null;
                    if (dr.Read())
                    {
                        usuario = new Usuario
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = dr["Nome"].ToString(),
                            Email = dr["Email"].ToString(),
                            Senha = dr["Senha"].ToString()
                        };
                    }
                    return usuario;
                }
            }
        }
    }
}