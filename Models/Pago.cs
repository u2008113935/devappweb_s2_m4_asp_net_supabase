using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalPagos.Models
{
    [Table("pagos")]
    public class Pago
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, Column("user_id")]
        public Guid UserId { get; set; }

        [Required]
        public string Servicio { get; set; } = string.Empty;

        [Column("numero_contrato")]
        public string NumeroContrato { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Monto { get; set; }

        public string Estado { get; set; } = "completado";

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
