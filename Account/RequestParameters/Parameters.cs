using System.ComponentModel.DataAnnotations;

namespace BankLibrary.RequestParameters
{
    public class Parameters
    {
        const int maxPageSize = 50;
        [Required]
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        [Required]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
