namespace Cartola.Domain.Entities
{
    public class CartolaCargaResponse
    {
        public string Errors { get; set; }
        public int QuantidadeUpdates { get; set; }
        public int QuantidadeInserts { get; set; }
        public string Mensagem { get; set; }
    }
}
