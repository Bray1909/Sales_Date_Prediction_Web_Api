namespace Application.DTOs
{
    public class CustomerOrderPredictionDto
    {
        public int custid { get; set; }
        public string CustomerName { get; set; }
        public DateTime LastOrderDate { get; set; }
        public DateTime NextPredictedOrder { get; set; }
    }
}
