using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.UseCases
{
    public abstract class EFUseCase
    {
        protected EFUseCase(TuneTraxContext context)
        {
            Context = context;
        }

        protected TuneTraxContext Context { get; }

    }
}
