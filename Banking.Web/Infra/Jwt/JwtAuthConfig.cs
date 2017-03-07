using System.Collections.Specialized;

namespace Banking.Web.Infra.Jwt
{
    public static class JwtAuthConfig
    {
        public static void Configure(NameValueCollection appSettings)
        {
            JwtManager.Settings = new JwtSettings
            {
                Expires = int.Parse(appSettings["expire"]),
                Issuer = appSettings["issuer"],
                Secret = appSettings["secret"]
            };
        }
    }
}