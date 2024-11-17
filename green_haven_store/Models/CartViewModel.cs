namespace green_haven_store.Models
{
    public class CartViewModel
    {
        public int Cart_Id { get; set; }

        public int? Cart_Quantity { get; set; }

        public DateTime Cart_Date { get; set; }

        public int P_Id { get; set; }

        public string? P_Name { get; set; }

        public string? P_Price { get; set; }

        public string? P_Description { get; set; }

        public string? P_PictureUri { get; set; }
    }
}
