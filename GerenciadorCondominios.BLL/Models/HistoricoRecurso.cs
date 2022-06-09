namespace GerenciadorCondominios.BLL.Models
{
    public class HistoricoRecurso
    {
        public int HistoricoRecursoId { get; set; }

        public decimal Valor { get; set; }

        public Tipos Tipo { get; set; }

        public int Dia { get; set; }

        public int MesId { get; set; }

        public Mes Mes { get; set; }

        public int Ano { get; set; }
    }

    public enum Tipos
    {
        Entrada ,Saida
    }
}