using System.ComponentModel.DataAnnotations;

namespace ExcursionistasApp.Models
{
    public class Elemento
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public int Peso { get; set; }
        [Required]
        public int Calorias { get; set; }
    }

    public class SeleccionParametros
    {
        [Required]
        public int MinimoCalorias { get; set; }
        [Required]
        public int PesoMaximo { get; set; }
    }

    public class SeleccionResultado
    {
        public List<Elemento> ElementosSeleccionados { get; set; } = new List<Elemento>();
        public int CaloriasTotales { get; set; }
        public int PesoTotal { get; set; }
    }
}
