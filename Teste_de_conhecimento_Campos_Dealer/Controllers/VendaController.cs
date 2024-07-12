using Microsoft.AspNetCore.Mvc;
using Teste_de_conhecimento_Campos_Dealer.Data;
using Teste_de_conhecimento_Campos_Dealer.Models.Entities;
using Teste_de_conhecimento_Campos_Dealer.Models;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;

namespace Teste_de_conhecimento_Campos_Dealer.Controllers
{
    public class VendaController : Controller
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
        public VendaController(AppBdContext bdContext)
        {
            this.bdContext = bdContext;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.cliente = await bdContext.Clientes.ToListAsync();
            mymodel.produto = await bdContext.Produto.ToListAsync();
            AddVendaViewModel addVendaViewModel = new AddVendaViewModel();
            mymodel.venda = addVendaViewModel;
            return View(mymodel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVendaViewModel viewModel)
        {
            float valorTotalFloat = ConverterValorMonetarioParaFloat(viewModel.vlrTotalVenda);
            float valorFloat = ConverterValorMonetarioParaFloat(viewModel.vlrUnitarioVenda);
            var venda = new Venda
            {
                Id = Guid.NewGuid(),
                clienteId = viewModel.idClienteId,
                produtoId = viewModel.idProdutoId,
                qtdVenda = viewModel.qtdVenda,
                vlrUnitarioVenda = valorFloat,
                dathVenda = DateTime.Now,
                vlrTotalVenda = valorTotalFloat,
            };
            await bdContext.Venda.AddAsync(venda);
            await bdContext.SaveChangesAsync();
            return RedirectToAction("List", "Cliente");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var vendas = await bdContext.Venda
                .Include(x => x.produto)
                .Include(x => x.cliente)
                .ToListAsync();
        
            return View(vendas);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            dynamic mymodel = new ExpandoObject();
            var vendas = await bdContext.Venda
                    .Include(x => x.produto)
                    .Include(x => x.cliente)
                    .Where(v => v.Id == id)
                    .ToListAsync();
            mymodel.venda = vendas;
            mymodel.cliente = await bdContext.Clientes.ToListAsync();
            mymodel.produto = await bdContext.Produto.ToListAsync();
            return View(mymodel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddVendaViewModel viewModel)
        {
            float valorTotalFloat = ConverterValorMonetarioParaFloat(viewModel.vlrTotalVenda);
            float valorFloat = ConverterValorMonetarioParaFloat(viewModel.vlrUnitarioVenda);
            var venda = await bdContext.Venda.FindAsync(viewModel.Id);
            if (venda is not null)
            {
                venda.clienteId = viewModel.idClienteId;
                venda.produtoId = viewModel.idProdutoId;
                venda.qtdVenda = viewModel.qtdVenda;
                venda.vlrUnitarioVenda = valorFloat;
                venda.dathVenda = DateTime.Now;
                venda.vlrTotalVenda = valorTotalFloat;
                await bdContext.SaveChangesAsync();
            }
            TempData["AlertMessage"] = "Operação realizada com Sucesso";
            return RedirectToAction("List", "Venda");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Venda viewModel)
        {
            var venda = await bdContext.Venda.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (venda is not null)
            {
                bdContext.Venda.Remove(viewModel);
                await bdContext.SaveChangesAsync();
                TempData["AlertMessage"] = "Operação realizada com Sucesso";
            }
            TempData["AlertMessage"] = "Operação realizada com Sucesso";
            return RedirectToAction("List", "Venda");
        }
    }

    
}
