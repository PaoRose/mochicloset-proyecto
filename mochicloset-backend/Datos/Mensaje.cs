namespace mochi_closet.Datos;

public class Mensaje
{
    public int Id { get; set; }
    public int ConversacionId { get; set; }
    public int RemitenteId { get; set; }
    public string? Texto { get; set; }
    public DateTime FechaEnvio { get; set; }
    public bool Leido { get; set; }
}