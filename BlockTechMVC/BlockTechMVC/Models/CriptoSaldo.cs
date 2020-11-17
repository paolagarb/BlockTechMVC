using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlockTechMVC.Models
{
    public class CriptoSaldo
    {
        public int Id { get; set; }
        public string Criptomoeda { get; set; } //Nome

        //[Display(Name = "Saldo em Criptomoeda")]
        [Display(Name = "Quantidade")]
        [Column(TypeName = "decimal(20,6)")]
        [DisplayFormat(DataFormatString = "{0:F6}")]

        public double Quantidade { get; set; } //Quantidade de Criptomoedas

        public ContaCliente ContaCliente { get; set; }
        public int ContaClienteId { get; set; }

        public CriptoSaldo()
        {

        }

        public CriptoSaldo(string criptomoeda, double quantidade)
        {
            Criptomoeda = criptomoeda;
            Quantidade = quantidade;
        }
    }
}
