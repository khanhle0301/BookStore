namespace BookStore.Web.Models
{
    public class ContactDetailViewModel
    {
        public string ID { set; get; }

        public string CompanyName { set; get; }

        public string CompanyPhone { set; get; }

        public string CompanyEmail { set; get; }

        public string CompanyWebsite { set; get; }

        public string CompanyAddress { set; get; }

        public string CompanyFax { set; get; }

        public double? Lat { set; get; }

        public double? Lng { set; get; }
    }
}