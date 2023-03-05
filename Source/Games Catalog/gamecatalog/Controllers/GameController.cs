using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using gamecatalog.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace gamecatalog.Controllers
{
    public class GameController : Controller
    {

        static BlobContainerClient blobContainer;

        private readonly IConfiguration _configuration;

        public GameController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET: Game
        public ActionResult Index(int _console)
        {
            var model = new List<Game>();

            //string connectionString = "Server=localhost,1433;Initial Catalog=games;Persist Security Info=False;User ID=sa;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            
            string connectionString = _configuration.GetValue<string>("ConnectionStrings:AzureSQL");

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT game.id, game.name, consoles.console, game.descricao, game.data_compra, game.finalizado, game.data_finalizacao FROM game INNER JOIN consoles on game.id_console = consoles.id where consoles.id = " + _console.ToString());
                    cmd.Connection = con;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Game games = new Game();

                        games.id = Convert.ToInt32(rdr["id"]);
                        games.console = rdr["console"].ToString();
                        games.nome = rdr["name"].ToString();
                        games.descricao = rdr["descricao"].ToString();
                        games.datacompra = Convert.ToDateTime(rdr["data_compra"]);
                        games.finalizado = Convert.ToBoolean(rdr["finalizado"]);
                        games.datafinalizacao = Convert.ToDateTime(rdr["data_finalizacao"]);

                        model.Add(games);
                    }
                    con.Close();
                }
            }
            catch (SqlException)
            {
                return View("Houve um erro ao tentar acessar o Banco de dados"); //tratamento em caso de erro
            }

            return View(model);
        }

        // GET: Game/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = new List<Game>();

            //string connectionString = "Server=localhost,1433;Initial Catalog=games;Persist Security Info=False;User ID=sa;Password=P@ssw0rd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            string connectionString = _configuration.GetValue<string>("ConnectionStrings:AzureSQL");  

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT game.id, game.name, consoles.console, game.descricao, game.data_compra, game.finalizado, game.data_finalizacao, game.thumb FROM game INNER JOIN consoles on game.id_console = consoles.id where game.id = " + id.ToString());
                    cmd.Connection = con;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Game games = new Game();

                        games.id = Convert.ToInt32(rdr["id"]);
                        games.console = rdr["console"].ToString();
                        games.nome = rdr["name"].ToString();
                        games.descricao = rdr["descricao"].ToString();
                        games.datacompra = Convert.ToDateTime(rdr["data_compra"]);
                        games.finalizado = Convert.ToBoolean(rdr["finalizado"]);
                        games.datafinalizacao = Convert.ToDateTime(rdr["data_finalizacao"]);
                        games.thumb = rdr["thumb"].ToString();

                        var blobs = new List<Blobs>();

                        blobs = await getcontainer();

                        foreach(Blobs blob in blobs)
                        {
                            if (blob.name == games.thumb)
                            {
                                games.URLthumb = blob.URL;
                            }
                        }

                        model.Add(games);
                    }
                    con.Close();
                }
            }
            catch (SqlException)
            {
                return View("Houve um erro ao tentar acessar o Banco de dados"); //tratamento em caso de erro
            }

            return View(model);
        }

        //Get Container
        public async Task<List<Blobs>> getcontainer()
        {
            string storageconnectionstring = _configuration.GetValue<string>("ConnectionStrings:AzureBlobStorage");
            string blobContainername = _configuration.GetValue<string>("Container:name");


            BlobServiceClient blobServiceClient = new BlobServiceClient(storageconnectionstring);

            blobContainer = blobServiceClient.GetBlobContainerClient(blobContainername);
            await blobContainer.CreateIfNotExistsAsync(PublicAccessType.None);

            var blobs = new List<Blobs>();
            foreach (BlobItem blob in blobContainer.GetBlobs())
            {
                if (blob.Properties.BlobType == BlobType.Block)

                    blobs.Add(new Blobs { URL = blobContainer.GetBlobClient(blob.Name).Uri.ToString(), name = blob.Name });
            }

            return blobs;
        }
    }
}