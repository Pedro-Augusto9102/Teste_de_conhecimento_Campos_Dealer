namespace Teste_de_conhecimento_Campos_Dealer.Models.Entities
{
    public class Venda
    {
        public Guid Id { get; set; }
        public Cliente idCliente { get; set; }
        public Produto idProduto { get; set; }
        public int qtdVenda { get; set; }
        public int vlrUnitarioVenda { get; set; }
        public DateTime dathVenda { get; set; }
        public float vlrTotalVenda { get; set; }
    }
}
