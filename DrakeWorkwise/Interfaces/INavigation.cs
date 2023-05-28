using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.Interfaces
{
    public interface INavigationMMR
    {
        Task NavigateTo(string name, Dictionary<string, object> parameters = null);

        T DecodeParameter<T>(string key, IDictionary<string, object> query);
    }
}
