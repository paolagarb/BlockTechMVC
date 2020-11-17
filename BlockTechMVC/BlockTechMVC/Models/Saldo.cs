using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlockTechMVC.Models
{
    public class Saldo
    {
        public int Id { get; set; }

        [Display(Name = "Saldo Atual")]
        [Column(TypeName = "decimal(20,2)")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double SaldoAtualRS { get; set; } = 0;

        public ContaCliente ContaCliente { get; set; }
        public int ContaClienteId { get; set; }

        public Saldo()
        {

        }

        public Saldo(double saldoAtualRS, ContaCliente contaCliente)
        {
            ContaCliente = contaCliente;
            SaldoAtualRS = saldoAtualRS;
        }
    }
}
