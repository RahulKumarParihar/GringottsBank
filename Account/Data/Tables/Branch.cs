using System.ComponentModel.DataAnnotations;

namespace BankLibrary.Data.Tables
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(50)]
        public string Code { get; set; }
        [Required, StringLength(200)]
        public string Address { get; set; }
        public virtual Account Account { get; set; }
    }
}
