using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Authentification
{
    public class Credentials : ICredentials
    {
        public Credentials(string publicKey, string secretKey)
        {
            PublicKey = publicKey ?? throw new ArgumentNullException(nameof(publicKey));
            SecretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
        }

        public string PublicKey { get; }
        public string SecretKey { get; }
    }
}
