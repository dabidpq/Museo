using System.Data.Entity;

namespace Museo.Models
{
    public class Service : DbContext
    {


        public DbSet<Arte> artes { get; set; }

        public Service() : base("Museo") { }


        #region Metodos Museo

        public List<Arte> MostrarObras()
        {
            return artes.ToList();
        }

        public Arte ObtenerObraPorId(int id)
        {
            return artes.Find(id);
        }

        public void AgregarObra(Arte obra)
        {
            
            if (obra == null)
            {
                throw new ArgumentNullException(nameof(obra));
            }

            // Generar código si no esta presente
            if (string.IsNullOrEmpty(obra.Codigo))
            {
                obra.Codigo = obra.GenerarCodigo();
            }

            
            if (obra.FechaRegistro == default)
            {
                obra.FechaRegistro = DateOnly.FromDateTime(DateTime.Now);
            }

            if (obra.PrecioFinal == 0)
            {
                obra.PrecioFinal = obra.CalcularPrecioFinal();
            }

            artes.Add(obra);
            SaveChanges();
        }
        public void EditarObra(Arte obra)
        {
            var obraExistente = artes.Find(obra.Id);
            if (obraExistente != null)
            {
                // Actualizar propiedades 
                obraExistente.Nombre = obra.Nombre;
                obraExistente.Tipo = obra.Tipo;
                obraExistente.Precio = obra.Precio;
                obraExistente.Autor = obra.Autor;
                obraExistente.Continente = obra.Continente;
                obraExistente.Mantenimiento = obra.Mantenimiento;
                obraExistente.PrecioFinal = obraExistente.CalcularPrecioFinal();

                SaveChanges();
            }
        }

        public void EliminarObra(int id)
        {
            var obra = artes.Find(id);
            if (obra != null)
            {
                artes.Remove(obra);
                SaveChanges();
            }
        }

        #endregion

    }
}
