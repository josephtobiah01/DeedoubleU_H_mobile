using DrakeWorkwise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.Interfaces
{
    public interface IApiManager
    {
        public Task<IList<AuthenticationResponse>> GetAuthKey(Authentication authentication);
        public Task<T> PostForResponseAsync<T>(string uriRequest, Dictionary<string, string> parameters, CancellationToken cancellationToken = default);
    }
}
