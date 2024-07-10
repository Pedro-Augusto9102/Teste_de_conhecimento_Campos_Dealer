using Teste_de_conhecimento_Campos_Dealer.Models.Entities;

namespace Teste_de_conhecimento_Campos_Dealer.Models
{
    public class AddVendaViewModel
    {
        public Cliente idCliente { get; set; }
        public Produto idProduto { get; set; }
        public int qtdVenda { get; set; }
        public int vlrUnitarioVenda { get; set; }
        public DateTime dathVenda { get; set; }
        public float vlrTotalVenda { get; set; }
    }
}
