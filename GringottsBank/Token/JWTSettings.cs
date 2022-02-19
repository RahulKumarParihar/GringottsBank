namespace GringottsBank.Token
{
    public class JWTSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int ExpirationTimeInMinutes { get; set; }
    }
}
