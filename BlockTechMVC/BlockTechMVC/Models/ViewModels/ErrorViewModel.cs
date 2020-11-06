using System;

namespace BlockTechMVC.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; } //Para poder adicionar mensagens de erro
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
