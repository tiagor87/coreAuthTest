using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace api
{
    public class TokenOptions
    {
        public static string Audience { get; } = "Nibo";
        public static string Issuer { get; } = "Nibo";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromDays(1);
        public static string TokenType { get; } = "Bearer";

        private static RSAParameters GenerateKey()
        {
            using (var key = new RSACryptoServiceProvider(2048))
            {
                return key.ExportParameters(true);
            }
        }
    }
}