using mochi_closet.Datos;

namespace mochi_closet.Negocio;

public class GestionCompras
{
    private static List<Compra> _dbCompras = new();
    private static GestionPublicaciones _gestionPublicaciones = new();
 
    public List<Compra> ListaCompras()
    {
        return _dbCompras;
    }
 
    public Compra? ObtenerCompra(int id)
    {
        return _dbCompras.FirstOrDefault(c => c.Id == id);
    }
 
    public string RegistrarCompra(Compra compra)
    {
        if (string.IsNullOrWhiteSpace(compra.MetodoPago))
            return "El método de pago es obligatorio";
 
        if (string.IsNullOrWhiteSpace(compra.DireccionEntrega))
            return "La dirección de entrega es obligatoria";
 
        var publicacion = _gestionPublicaciones.ObtenerPublicacion(compra.PublicacionId);
 
        if (publicacion == null)
            return "La publicación no existe";
 
        if (publicacion.Estado != "Disponible")
            return "Esta publicación ya fue vendida";
 
        compra.Id = _dbCompras.Count == 0 ? 1 : _dbCompras.Max(c => c.Id) + 1;
        compra.FechaCompra = DateTime.Now;
        compra.Estado = "Completada";
        compra.MontoTotal = (decimal)publicacion.Precio;
        _dbCompras.Add(compra);
 
        publicacion.Estado = "Vendido";
 
        return "ok";
    }
}