using Teste_de_conhecimento_Campos_Dealer.Data;
using Teste_de_conhecimento_Campos_Dealer.Models.Entities;

public static class DataInitializer
{
    public static void Initialize(AppBdContext bdContext)
    {
        bdContext.Database.EnsureCreated();

        // Verifique se já existem clientes no banco de dados
        if (bdContext.Clientes.Any() || bdContext.Produto.Any() || bdContext.Venda.Any())
        {
            return; // O banco de dados já foi populado
        }

        var clientes = new Cliente[]
        {
            new Cliente { Name = "Cli1", City = "Cidade1",},
            new Cliente { Name = "Cli2", City = "Cidade2",},
            new Cliente { Name = "Cli3", City = "Cidade3"},
            new Cliente { Name = "Cli4", City = "Cidade4"},
        };
        foreach (Cliente c in clientes)
        {
            bdContext.Clientes.AddAsync(c);
            bdContext.SaveChanges();
        }

        var produtos = new Produto[]
        {
            new Produto { dscProduto = "Produto 1", vlrUnitário = 5,},
            new Produto { dscProduto = "Produto 2", vlrUnitário = 10,},
            new Produto { dscProduto = "Produto 3", vlrUnitário = 15},
            new Produto { dscProduto = "Produto 4", vlrUnitário = 20},
        };

        foreach (Produto p in produtos)
        {
            bdContext.Produto.AddAsync(p);
            bdContext.SaveChanges();
        }

        var vendas = new Venda[]
        {
            new Venda { clienteId = clientes[0].Id, produtoId = produtos[0].Id, qtdVenda = 5, vlrUnitarioVenda = 5.00F, vlrTotalVenda = 25.00F},
            new Venda { clienteId = clientes[0].Id, produtoId = produtos[1].Id, qtdVenda = 1, vlrUnitarioVenda = 10.00F, vlrTotalVenda = 10.00F},
            new Venda { clienteId = clientes[0].Id, produtoId = produtos[2].Id, qtdVenda = 1, vlrUnitarioVenda = 15.00F, vlrTotalVenda = 15.00F},
            new Venda { clienteId = clientes[1].Id, produtoId = produtos[0].Id, qtdVenda = 5, vlrUnitarioVenda = 5.00F, vlrTotalVenda = 25.00F},
            new Venda { clienteId = clientes[1].Id, produtoId = produtos[1].Id, qtdVenda = 1, vlrUnitarioVenda = 10.00F, vlrTotalVenda = 10.00F},
            new Venda { clienteId = clientes[2].Id, produtoId = produtos[0].Id, qtdVenda = 10, vlrUnitarioVenda = 6.00F, vlrTotalVenda = 60.00F},
            new Venda { clienteId = clientes[2].Id, produtoId = produtos[2].Id, qtdVenda = 2, vlrUnitarioVenda = 15.00F, vlrTotalVenda = 30.00F},
        };        
        
        foreach (Venda v in vendas)
        {
            bdContext.Venda.AddAsync(v);
            bdContext.SaveChanges();
        }

    }
}
/*    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppBdContext>();
        var apiService = scope.ServiceProvider.GetRequiredService<IApiService>();

        if (!context.Clientes.Any())
        {
            var clientesViewModel = await apiService.GetClientesAsync();
            var clientes = clientesViewModel.Select(vm => new Cliente
            {
                Id = Guid.NewGuid(),
                Name = vm.Name,
                City = vm.City,
                dathVenda = vm.dathVenda
            }).ToList();

            context.Clientes.AddRange(clientes);
        }

        if (!context.Produto.Any())
        {
            var produtosViewModel = await apiService.GetProdutosAsync();
            var produtos = produtosViewModel.Select(vm => new Produto
            {
                Id = Guid.NewGuid(),
                dscProduto = vm.dscProduto,
                vlrUnitário = vm.vlrUnitário
            }).ToList();

            context.Produto.AddRange(produtos);
        }

        if (!context.Venda.Any())
        {
            var vendasViewModel = await apiService.GetVendasAsync();
            var vendas = vendasViewModel.Select(vm => new Venda
            {
                Id = Guid.NewGuid(),
                clienteId = vm.idClienteId,
                produtoId = vm.idProdutoId,
                qtdVenda = vm.qtdVenda,
                vlrUnitarioVenda = vm.vlrUnitarioVenda,
                dathVenda = vm.dathVenda,
                vlrTotalVenda = vm.vlrTotalVenda
            }).ToList();

            context.Venda.AddRange(vendas);
        }

        await context.SaveChangesAsync();
    
     }
}*/
