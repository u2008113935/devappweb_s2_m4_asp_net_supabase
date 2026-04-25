using Microsoft.AspNetCore.Mvc;
using PortalPagos.Models;
using PortalPagos.Services;

namespace PortalPagos.Controllers
{
    // CAPA 1 + 2 — ROUTER + CONTROLLER
    // En ASP.NET el enrutamiento va en atributos dentro del Controller.
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagosController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        // GET /api/pagos?userId=uuid
        [HttpGet]
        public async Task<IActionResult> GetPagos([FromQuery] Guid userId)
        {
            var pagos = await _pagoService.ObtenerPagosByUserAsync(userId);
            return Ok(new { success = true, data = pagos });
        }

        // POST /api/pagos
        [HttpPost]
        public async Task<IActionResult> RegistrarPago([FromBody] Pago pago)
        {
            try
            {
                var resultado = await _pagoService.RegistrarPagoAsync(pago);
                return Created("", new { success = true, data = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // GET /api/pagos/total?userId=uuid
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalPagado([FromQuery] Guid userId)
        {
            var total = await _pagoService.ObtenerTotalPagadoAsync(userId);
            return Ok(new { success = true, data = new { total_pagado = total } });
        }

        // GET /api/pagos/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPagoPorId(Guid id)
        {
            var pago = await _pagoService.ObtenerPagoPorIdAsync(id);
            if (pago == null)
                return NotFound(new { success = false, message = "Pago no encontrado" });

            return Ok(new { success = true, data = pago });
        }

        // PUT /api/pagos/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> ActualizarPago(Guid id, [FromBody] Pago pago)
        {
            try
            {
                var resultado = await _pagoService.ActualizarPagoAsync(id, pago);
                return Ok(new { success = true, data = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // DELETE /api/pagos/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> EliminarPago(Guid id)
        {
            var eliminado = await _pagoService.EliminarPagoAsync(id);
            if (!eliminado)
                return NotFound(new { success = false, message = "Pago no encontrado" });

            return Ok(new { success = true, message = "Pago eliminado" });
        }
    }
}