namespace Teste_de_conhecimento_Campos_Dealer.Models.Entities
{
    public class Venda
    {
        public Guid Id { get; set; }
        public string idCliente { get; set; }
        public Cliente cliente { get; set; }
        public string idProduto { get; set; }
        public Produto produto { get; set; }
        public int qtdVenda { get; set; }
        public int vlrUnitarioVenda { get; set; }
        public DateTime dathVenda { get; set; }
        public float vlrTotalVenda { get; set; }
    }
}
