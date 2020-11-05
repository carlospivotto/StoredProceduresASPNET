using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoredProceduresApp.Models;

namespace StoredProceduresApp.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: ProdutoController
        public ActionResult Index()
        {
            var produtos = new List<Produto>();
            
            //I. Etapa de configuração:
            //1. Cadeia de conexão com o banco
            var connectionString = "<COLOCAR SUA CONNECTION STRING AQUI!>";
            //2. O objeto de conexão com o banco de dados (construído com a cadeia de conexão)
            using var connection = new SqlConnection(connectionString);
            //3. O nome da Stored Procedure
            var sp = "FiltrarProdutosPorNome";
            //4. O comando (a(s) instrução(ões)) a executar
            var sqlCommand = new SqlCommand(sp, connection);
            //5. Definir que o que será executado é uma Stored Procedure
            sqlCommand.CommandType = CommandType.StoredProcedure;
            //6. Parâmetro da Stored Procedure
            sqlCommand.Parameters.AddWithValue("@Nome", "%");

            //II. Etapa de conexão e execução:
            try
            {
                //1. Abrir a conexão (pode gerar erros — banco não existir, senha errada etc.)
                connection.Open();
                //2. Executar a leitura da tabela e armazenar resultados em um objeto reader
                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                //3. Enquanto for possível ler novas ocorrências (resultados) dentro de reader...
                while (reader.Read())
                {
                    //3.1. Criar um novo objeto produto.
                    var produto = new Produto
                    {
                        Nome = reader["Nome"].ToString()
                    };

                    //3.2. Adicionar este produto à coleção de produtos.
                    produtos.Add(produto);
                }
            }
            finally
            {
                connection.Close();
            }

            return View(produtos);
        }

        // GET: ProdutoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProdutoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProdutoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProdutoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProdutoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProdutoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProdutoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
