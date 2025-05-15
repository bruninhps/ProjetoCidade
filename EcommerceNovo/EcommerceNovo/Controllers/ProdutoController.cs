
using EcommerceNovo.Models;
using EcommerceNovo.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceNovo.Controllers
{

    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        public ProdutoController(ProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public IActionResult Index()
        {
            return View(_produtoRepositorio.TodosProduto());
        }

        public IActionResult CadastrarProduto()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            _produtoRepositorio.Cadastrar(produto);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditarProduto(int id)
        {
            var produto = _produtoRepositorio.ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditarProduto(int id, [Bind("Id,Nome,Descricao,Quantidade,Preco")] Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_produtoRepositorio.Atualizar(produto))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao Editar");
                    return View(produto);
                }
            }

            return View(produto);
        }


        public IActionResult ExcluirProduto(int id)
        {
            _produtoRepositorio.ExcluirProduto(id);
            return RedirectToAction(nameof(Index));

        }

    }
}