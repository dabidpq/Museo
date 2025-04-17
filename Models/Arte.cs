using System.ComponentModel.DataAnnotations;

namespace Museo.Models
{
    public class Arte
    {
        private int id;
        private string nombre;
        private string tipo;
        private decimal precio;
        private string autor;
        private string continente;
        private bool mantenimiento;
        private DateOnly fechaRegistro;
        private string codigo;
        private decimal precioFinal;


        public Arte(int id, string nombre, string tipo, decimal precio, string autor,
                   string continente, bool mantenimiento, DateOnly fechaRegistro, string codigo)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Tipo = tipo;
            this.Precio = precio;
            this.Autor = autor;
            this.Continente = continente;
            this.Mantenimiento = mantenimiento;
            this.FechaRegistro = fechaRegistro;
            this.Codigo = codigo;
            this.PrecioFinal = CalcularPrecioFinal(); 

        }

        public Arte()
        {
            this.Id = 0;
            this.Nombre = "";
            this.Tipo = "";
            this.Precio = 0;
            this.Autor = "";
            this.Continente = "";
            this.Mantenimiento = false;
            this.Codigo = GenerarCodigo(); 
            this.PrecioFinal = 0;
        }

        
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public string Autor { get => autor; set => autor = value; }
        public string Continente { get => continente; set => continente = value; }
        public bool Mantenimiento { get => mantenimiento; set => mantenimiento = value; }

        public DateOnly FechaRegistro { get => fechaRegistro; set => fechaRegistro = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public decimal PrecioFinal { get => precioFinal; set => precioFinal = value; }


 
 public decimal CalcularPrecioFinal()
{
   
    if (Precio <= 0) return 0m;

    
    decimal precioFinal = Precio;

    //porcentaje fijo segun el tipo de obra
    decimal porcentajeTipo = Tipo.ToLower() switch
    {
        "escultura" => 0.40m,
        "pintura" => 0.10m,
        "reliquia" => 0.20m,
        "restauracion" => 0.35m,
        _ => 0m
    };
    precioFinal += Precio * porcentajeTipo;

    //costo fijo segun el continente
    decimal costoContinente = Continente.ToLower() switch
    {
        "america" => 5000m,
        "europa" => 7000m,
        "asia" => 4000m,
        "africa" or "oceania" => 3000m,
        "antartida" => 2000m,
        _ => 0m
    };
    precioFinal += costoContinente;

    if (Mantenimiento) precioFinal += Precio * 0.15m;

    return precioFinal;
}

      
        public string GenerarCodigo()
        {
            try
            {
                // Obtener los ultimos 2 dígitos del año
                string año = DateTime.Now.Year.ToString().Substring(2);

                // Obtener la primera letra del tipo de obra
                char letraTipo = Tipo?.FirstOrDefault() switch
                {
                    'E' => 'E', 
                    'P' => 'P',
                    'R' => 'R', 
                    _ => 'X' 
                };

          
                char letraContinente = Continente?.ToLower() switch
                {
                    "europa" => 'E',
                    "oceania" or "oceanía" => 'O',
                    _ when Continente?.StartsWith("A", StringComparison.OrdinalIgnoreCase) == true => 'A', 
                    _ => 'X' 
                };

          
                return $"{año}{letraTipo}{letraContinente}{new Random().Next(100, 1000)}";
            }
            catch
            {
                return "ERR-GEN-CODE";
            }
        }
    }
    }
    
