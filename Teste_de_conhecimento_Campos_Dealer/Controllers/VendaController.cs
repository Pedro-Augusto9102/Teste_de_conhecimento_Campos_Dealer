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
            return View(mymodel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVendaViewModel viewModel)
        {
            var venda = new Venda
            {
                idCliente = viewModel.idCliente,
                idProduto = viewModel.idProduto,
                qtdVenda = viewModel.qtdVenda,
                vlrUnitarioVenda = viewModel.vlrUnitarioVenda,
                dathVenda = viewModel.dathVenda,
                vlrTotalVenda = viewModel.vlrTotalVenda,
            };
            await bdContext.Venda.AddAsync(venda);
            await bdContext.SaveChangesAsync();
            return View();
        }
    }
}
