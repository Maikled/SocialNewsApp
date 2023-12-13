namespace SocialNewsApp.Sources.VK_Source
{
    class AccountInfoResponce
    {
        public AccountResponse response { get; set; }
    }

    public class AccountResponse
    {
        public int id { get; set; }
        public string home_town { get; set; }
        public string status { get; set; }
        public string photo_200 { get; set; }
        public bool is_service_account { get; set; }
        public string bdate { get; set; }
        public bool is_verified { get; set; }
        public string mail { get; set; }
        public string verification_status { get; set; }
        public object[] promo_verifications { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int bdate_visibility { get; set; }
        public City city { get; set; }
        public Country country { get; set; }
        public string phone { get; set; }
        public int relation { get; set; }
        public string screen_name { get; set; }
        public int sex { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string title { get; set; }
    }

    public class Country
    {
        public int id { get; set; }
        public string title { get; set; }
    }
}
