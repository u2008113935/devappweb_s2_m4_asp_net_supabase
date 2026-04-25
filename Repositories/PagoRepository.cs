using Microsoft.EntityFrameworkCore;
using PortalPagos.Data;
using PortalPagos.Models;

namespace PortalPagos.Repositories
{
    // CAPA 4 — REPOSITORY
    // Único punto de acceso a la base de datos.
    // Todas las consultas Entity Framework van aquí.
    public class PagoRepository : IPagoRepository
    {
        private readonly AppDbContext _context;

        public PagoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pago>> ObtenerPorUsuarioAsync(Guid userId)
        {
            return await _context.Pagos
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Fecha)
                .Take(20)
                .ToListAsync();
        }

        public async Task<Pago?> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Pagos.FindAsync(id);
        }

        public async Task<Pago> CrearAsync(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return pago;
        }

        public async Task<Pago> ActualizarAsync(Pago pago)
        {
            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync();
            return pago;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return false;

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> ObtenerTotalPagadoAsync(Guid userId)
        {
            return await _context.Pagos
                .Where(p => p.UserId == userId)
                .SumAsync(p => p.Monto);
        }
    }
}