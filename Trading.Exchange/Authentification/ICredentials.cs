using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Exchange.Authentification
{
    public interface ICredentials
    {
        public string PublicKey { get; }
        public string SecretKey { get; }
    }
}
