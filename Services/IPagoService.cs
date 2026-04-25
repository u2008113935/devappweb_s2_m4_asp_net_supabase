using PortalPagos.Models;

namespace PortalPagos.Services
{
    public interface IPagoService
    {
        Task<List<Pago>> ObtenerPagosByUserAsync(Guid userId);
        Task<Pago?> ObtenerPagoPorIdAsync(Guid id);
        Task<Pago> RegistrarPagoAsync(Pago pago);
        Task<Pago> ActualizarPagoAsync(Guid id, Pago pago);
        Task<bool> EliminarPagoAsync(Guid id);
        Task<decimal> ObtenerTotalPagadoAsync(Guid userId);
    }
}