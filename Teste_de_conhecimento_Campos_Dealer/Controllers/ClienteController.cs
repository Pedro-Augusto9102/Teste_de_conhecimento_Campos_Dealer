using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Teste_de_conhecimento_Campos_Dealer.Data;
using Teste_de_conhecimento_Campos_Dealer.Models;
using Teste_de_conhecimento_Campos_Dealer.Models.Entities;

namespace Teste_de_conhecimento_Campos_Dealer.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AppBdContext bdContext;
        public ClienteController(AppBdContext bdContext) 
        {
            this.bdContext = bdContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddClienteViewModel viewModel)
        {
            var cliente = new Cliente
            {
                Name = viewModel.Name,
                City = viewModel.City,
            };
            await bdContext.Clientes.AddAsync(cliente);
            await bdContext.SaveChangesAsync();
            TempData["AlertMessage"] = "Operação realizada com Sucesso";        
            return RedirectToAction("List", "Cliente");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var cliente = await bdContext.Clientes.ToListAsync();
            return View(cliente);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var cliente = await bdContext.Clientes.FindAsync(id);
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cliente viewModel)
        {
            var cliente = await bdContext.Clientes.FindAsync(viewModel.Id);
            if(cliente is not null)
            {
                cliente.Name = viewModel.Name;
                cliente.City = viewModel.City;
                await bdContext.SaveChangesAsync();
                TempData["AlertMessage"] = "Operação realizada com Sucesso";
            }
            
            return RedirectToAction("List", "Cliente");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Cliente viewModel)
        {
            var cliente = await bdContext.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);  
            
            if (cliente is not null)
            {
                bdContext.Clientes.Remove(viewModel);
                await bdContext.SaveChangesAsync();
                TempData["AlertMessage"] = "Operação realizada com Sucesso";
            }
            return RedirectToAction("List", "Cliente");
        }
    }
}
