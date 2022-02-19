using System.ComponentModel.DataAnnotations;

namespace BankLibrary.RequestParameters
{
    public class TransactionParameter : Parameters
    {
        [Required]
        public int AccountId { get; set; }
    }
}
