using System.ComponentModel.DataAnnotations;

namespace TestApp.Models
{
    public class R_CURRENCY
    {
        public int Id { get; set; }
        [MaxLength(60)]
        public string Title { get; set; }
        [MaxLength(3)]
        public string Code { get; set; }
        [MaxLength(18)]
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
