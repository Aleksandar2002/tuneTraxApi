using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IApplicationActor
    {
        string Username{ get; }
        string Email{ get; }
        int Id{ get; }
        IEnumerable<int> AllowedUseCases{ get; }
    }
}
