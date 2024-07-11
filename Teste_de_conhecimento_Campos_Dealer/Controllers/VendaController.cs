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
            var venda = new Venda
            {
                Id = Guid.NewGuid(),
                clienteId = viewModel.idClienteId,
                produtoId = viewModel.idProdutoId,
                qtdVenda = viewModel.qtdVenda,
                vlrUnitarioVenda = viewModel.vlrUnitarioVenda,
                dathVenda = DateTime.Now,
                vlrTotalVenda = viewModel.vlrTotalVenda,
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
        public async Task<IActionResult> Edit(Venda viewModel)
        {
            var venda = await bdContext.Venda.FindAsync(viewModel.Id);
            if (venda is not null)
            {
                venda.clienteId = viewModel.clienteId;
                venda.produtoId = viewModel.produtoId;
                venda.qtdVenda = viewModel.qtdVenda;
                venda.vlrUnitarioVenda = viewModel.vlrUnitarioVenda;
                venda.dathVenda = DateTime.Now;
                venda.vlrTotalVenda = viewModel.vlrTotalVenda;
                await bdContext.SaveChangesAsync();
            }
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
            return RedirectToAction("List", "Cliente");
        }
    }

    
}
