using Teste_de_conhecimento_Campos_Dealer.Models.Entities;

namespace Teste_de_conhecimento_Campos_Dealer.Models
{
    public class AddVendaViewModel
    {
        public Guid Id { get; set; }
        public Guid idClienteId { get; set; }
        public Guid idProdutoId { get; set; }
        public int qtdVenda { get; set; }
        public string vlrUnitarioVenda { get; set; }
        public DateTime dathVenda { get; set; }
        public string vlrTotalVenda { get; set; }
    }
}
