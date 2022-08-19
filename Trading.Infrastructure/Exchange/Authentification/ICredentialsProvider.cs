using System;
using System.Collections.Generic;
using System.Text;
using Trading.Exchange.Connections;

namespace Trading.Infrastructure.Exchange.Authentification
{
    public interface ICredentialsProvider
    {
        ICredentials GetCredentials();
    }
}
