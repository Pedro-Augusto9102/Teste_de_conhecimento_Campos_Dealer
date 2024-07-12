using Microsoft.AspNetCore.Mvc;
using Teste_de_conhecimento_Campos_Dealer.Data;
using Teste_de_conhecimento_Campos_Dealer.Models.Entities;
using Teste_de_conhecimento_Campos_Dealer.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Teste_de_conhecimento_Campos_Dealer.Controllers
{
    public class ProdutoController : Controller
    {
        public float ConverterValorMonetarioParaFloat(string valorMonetario)
        {
            // Remove caracteres não numéricos
            string valorLimpo = valorMonetario
                .Replace("R$", "")
                .Replace(".", "")
                .Replace(",", "")
                .Trim(); // Remove espaços em branco extras, se houver

            // Converte para float
            if (float.TryParse(valorLimpo, out float valorConvertido))
            {
                return valorConvertido / 100; // Divide por 100 se o valor estiver em centavos
            }
            else
            {
                throw new ArgumentException("Valor monetário inválido.");
            }
        }
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
            float valorFloat = ConverterValorMonetarioParaFloat(viewModel.vlrUnitário);
            var produto = new Produto
            {
                dscProduto = viewModel.dscProduto,
                vlrUnitário = valorFloat,
            };
            await bdContext.Produto.AddAsync(produto);
            await bdContext.SaveChangesAsync();
            TempData["AlertMessage"] = "Operação realizada com Sucesso";
            return RedirectToAction("List", "Produto");
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
            return RedirectToAction("List", "Produto");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await bdContext.Produto.FindAsync(id);
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddProdutoViewModel viewModel)
        {
            float valorFloat = ConverterValorMonetarioParaFloat(viewModel.vlrUnitário);
            var produto = await bdContext.Produto.FindAsync(viewModel.Id);
            if (produto is not null)
            {
                produto.dscProduto = viewModel.dscProduto;
                produto.vlrUnitário = valorFloat;
                await bdContext.SaveChangesAsync();
            }
            TempData["AlertMessage"] = "Operação realizada com Sucesso";
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
