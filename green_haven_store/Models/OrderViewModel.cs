namespace green_haven_store.Models
{
    public class OrderViewModel
    {
        public int Order_Id { get; set; }

        public int? Order_Quantity { get; set; }

        public DateTime Order_Date { get; set; }

        public double Order_Tax { get; set; }

        public double Order_PayableAmount { get; set; }

        public bool Order_IsPaymentDone { get; set; }
    }
}
