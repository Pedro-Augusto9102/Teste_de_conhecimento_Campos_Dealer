using Microsoft.AspNetCore.Mvc;
using Teste_de_conhecimento_Campos_Dealer.Data;
using Teste_de_conhecimento_Campos_Dealer.Models.Entities;
using Teste_de_conhecimento_Campos_Dealer.Models;
using Microsoft.EntityFrameworkCore;

namespace Teste_de_conhecimento_Campos_Dealer.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly AppBdContext bdContext;
        public ProdutoController(AppBdContext bdContext)
        {
            this.bdContext = bdContext;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProdutoViewModel viewModel)
        {
            var produto = new Produto
            {
                dscProduto = viewModel.dscProduto,
                vlrUnitário = viewModel.vlrUnitário,
            };
            await bdContext.Produto.AddAsync(produto);
            await bdContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var produto = await bdContext.Produto.ToListAsync();
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> Search(Guid id)
        {

            var produto = await bdContext.Produto.FindAsync(id);

            if (produto is not null)
            {
                return new JsonResult(Ok(produto));
            }
            return RedirectToAction("List", "Cliente");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await bdContext.Produto.FindAsync(id);
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Produto viewModel)
        {
            var produto = await bdContext.Produto.FindAsync(viewModel.Id);
            if (produto is not null)
            {
                produto.dscProduto = viewModel.dscProduto;
                produto.vlrUnitário = viewModel.vlrUnitário;
                await bdContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Produto");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Produto viewModel)
        {
            var produto = await bdContext.Produto.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (produto is not null)
            {
                bdContext.Produto.Remove(viewModel);
                await bdContext.SaveChangesAsync();
                TempData["AlertMessage"] = "Operação realizada com Sucesso";
            }
            return RedirectToAction("List", "Produto");
        }
    }
}
