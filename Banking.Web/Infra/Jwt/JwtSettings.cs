namespace Banking.Web.Infra.Jwt
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }

        /// <summary>
        /// Seconds
        /// </summary>
        public int Expires { get; set; }
    }
}