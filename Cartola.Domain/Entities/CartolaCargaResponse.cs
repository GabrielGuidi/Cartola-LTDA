namespace Cartola.Domain.Entities
{
    public class CartolaCargaResponse
    {
        public string Errors { get; set; }
        public int QuantidadeUpdates { get; set; }
        public int QuantidadeInserts { get; set; }
        public string Mensagem { get; set; }
    }

    public class CartolaCargaResponse<T> where T : class
    {
        public string Errors { get; set; }
        public int QuantidadeUpdates { get; set; }
        public int QuantidadeInserts { get; set; }
        public string Mensagem { get; set; }
        public T Dados { get; set; }
    }
}
