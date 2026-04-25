using PortalPagos.Models;
using PortalPagos.Repositories;

namespace PortalPagos.Services
{
    // CAPA 3 — SERVICE
    // Responsabilidad: lógica de negocio (validar servicios, montos).
    // NO accede directamente a la BD, delega al Repository.
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepo;

        public PagoService(IPagoRepository pagoRepo)
        {
            _pagoRepo = pagoRepo;
        }

        public async Task<List<Pago>> ObtenerPagosByUserAsync(Guid userId)
        {
            return await _pagoRepo.ObtenerPorUsuarioAsync(userId);
        }

        public async Task<Pago?> ObtenerPagoPorIdAsync(Guid id)
        {
            return await _pagoRepo.ObtenerPorIdAsync(id);
        }

        public async Task<Pago> RegistrarPagoAsync(Pago pago)
        {
            // Regla de negocio: solo servicios válidos
            var serviciosValidos = new[] { "agua", "electricidad", "streming", "internet", "gas" };
            if (!serviciosValidos.Contains(pago.Servicio))
                throw new Exception("Servicio no valido");

            // Regla de negocio: rango de montos
            if (pago.Monto <= 0 || pago.Monto > 5000)
                throw new Exception("Monto fuera de rango (1 - 5000)");

            return await _pagoRepo.CrearAsync(pago);
        }

        public async Task<Pago> ActualizarPagoAsync(Guid id, Pago pago)
        {
            var existente = await _pagoRepo.ObtenerPorIdAsync(id);
            if (existente == null)
                throw new Exception("Pago no encontrado");

            // Mismas reglas de negocio que al registrar
            var serviciosValidos = new[] { "agua", "electricidad", "streming", "internet", "gas" };
            if (!serviciosValidos.Contains(pago.Servicio))
                throw new Exception("Servicio no valido");

            if (pago.Monto <= 0 || pago.Monto > 5000)
                throw new Exception("Monto fuera de rango (1 - 5000)");

            existente.Servicio = pago.Servicio;
            existente.NumeroContrato = pago.NumeroContrato;
            existente.Monto = pago.Monto;
            existente.Estado = pago.Estado;

            return await _pagoRepo.ActualizarAsync(existente);
        }

        public async Task<bool> EliminarPagoAsync(Guid id)
        {
            return await _pagoRepo.EliminarAsync(id);
        }

        public async Task<decimal> ObtenerTotalPagadoAsync(Guid userId)
        {
            return await _pagoRepo.ObtenerTotalPagadoAsync(userId);
        }
    }
}