using PortalPagos.Models;

namespace PortalPagos.Repositories
{
    public interface IPagoRepository
    {
        Task<List<Pago>> ObtenerPorUsuarioAsync(Guid userId);
        Task<Pago?> ObtenerPorIdAsync(Guid id);
        Task<Pago> CrearAsync(Pago pago);
        Task<Pago> ActualizarAsync(Pago pago);
        Task<bool> EliminarAsync(Guid id);
        Task<decimal> ObtenerTotalPagadoAsync(Guid userId);
    }
}