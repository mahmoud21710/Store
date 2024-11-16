using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Services.Contract
{
    public interface ICacheService
    {
        Task SetCacheKeyAsync(string key , object response ,TimeSpan expiretime); //GEt DAta from db  and store in ram
        Task<string> GetCachekeyAsync(string Key);

    }
}
