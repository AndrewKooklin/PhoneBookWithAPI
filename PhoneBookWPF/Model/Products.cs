namespace PhoneBookWPF.Model
{
    public partial class Products
    {
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Email { get; set; }
    
        public virtual Clients Clients { get; set; }

        public Products(int clientId, int productCode, string productName, string email)
        {
            ClientId = clientId;
            ProductCode = productCode;
            ProductName = productName;
            Email = email;
        }

        public Products()
        {
        }
    }
}
