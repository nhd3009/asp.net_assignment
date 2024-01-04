namespace BigAssignment.Models
{
    public class ListOrder
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public double SAmount { get; set; }
        public int? Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ExportDate { get; set; }
    }
}
