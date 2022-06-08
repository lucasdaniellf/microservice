namespace Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public Rating ClassificacaoESBR { get; set; }
        //public DateTime DataCompra { get; set; }
        //Decimal PrecoCompra { get; set; }
        public int? IdEstudio { get; set; }
    }
}
