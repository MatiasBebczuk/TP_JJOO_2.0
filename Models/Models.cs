namespace TP_JJOO_2.Models
{
    public class Deporte
    {
        public int IdDeporte { get; set; }
        public string Nombre { get; set; }
        public string Foto { get; set; }
    }

    public class Deportista
    {
        public int IdDeportista { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdDeporte { get; set; }
        public int IdPais { get; set; }
        public string Imagen { get; set; }
    }

    public class Pais
    {
        public int IdPais { get; set; }
        public string Nombre { get; set; }
        public string Bandera { get; set; }
    }
}
