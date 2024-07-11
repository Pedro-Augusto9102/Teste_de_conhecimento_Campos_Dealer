using Teste_de_conhecimento_Campos_Dealer.Data;
using Teste_de_conhecimento_Campos_Dealer.Models.Entities;

public static class DataInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
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
}
