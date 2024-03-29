﻿using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace FamousRestaurant.Domain.Configurations
{
    public class SigningConfigurations
    {
        public SecurityKey SecurityKey { get; set; }

        public SigningCredentials SigningCredentials { get; set; }

        public SigningConfigurations()
        {
            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048))
            {
                SecurityKey = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
