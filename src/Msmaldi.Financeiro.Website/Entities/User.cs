using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Msmaldi.AspNetCore.GuIdentity;

namespace Msmaldi.Financeiro.Website.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : GuIdentityUser
    {
        public List<CDBComCDI> CDBsComCDI { get; protected set; }
        public List<SwingTrade> SwingTrades { get; protected set; }
        public List<CryptoWallet> CryptoWallets { get; protected set; }
    }
}
