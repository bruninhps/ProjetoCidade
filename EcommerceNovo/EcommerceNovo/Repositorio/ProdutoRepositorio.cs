using EcommerceNovo.Models;
using MySql.Data.MySqlClient;
using System.Data;


namespace EcommerceNovo.Repositorio
{
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");


        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("insert into produto (Id, Nome, Descricao, Quantidade, Preco) values (@id, @nome, @descricao, @quantidade, @preco )", conexao);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = produto.Id;
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.Quantidade;
                cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.Preco;
                cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }

        public bool Atualizar(Produto produto)
        {

            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();
                    MySqlCommand cmd = new MySqlCommand("Update produto set Nome=@nome, Descricao=@descricao, Quantidade=@quantidade, Preco=@preco " + " where Id=@id", conexao);
                    cmd.Parameters.Add("@Id", MySqlDbType.Int32).Value = produto.Id;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = produto.Nome;
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.Descricao;
                    cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.Quantidade;
                    cmd.Parameters.Add("@preco", MySqlDbType.Decimal).Value = produto.Preco;
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }
            catch (MySqlException ex)
            {

                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Produto> TodosProduto()
        {
            List<Produto> ProdutoList = new List<Produto>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("select * from produto", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ProdutoList.Add(
                        new Produto

                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = ((string)dr["Nome"]),
                            Descricao = ((string)dr["Descricao"]),
                            Quantidade = Convert.ToInt32(dr["Quantidade"]),
                            Preco = ((decimal)dr["Preco"]),
                        });
                }
                return ProdutoList;
            }
        }

        public Produto ObterProduto(int Codigo)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * from produto where Id=@id ", conexao);

                cmd.Parameters.AddWithValue("@id", Codigo);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Produto produto = new Produto();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    produto.Id = Convert.ToInt32(dr["id"]);
                    produto.Nome = (string)(dr["Nome"]);
                    produto.Descricao = (string)(dr["Descricao"]);
                    produto.Quantidade = Convert.ToInt32(dr["Quantidade"]);
                    produto.Preco = (decimal)(dr["Preco"]);
                }
                return produto;
            }

        }

        public void ExcluirProduto(int id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("delete from produto where Id=@id", conexao);
                cmd.Parameters.AddWithValue("@id", id);

                int i = cmd.ExecuteNonQuery();
                conexao.Close();
            }

        }
    }

}