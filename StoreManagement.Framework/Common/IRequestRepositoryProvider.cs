using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Framework.Common
{
    public interface IRequestRepositoryProvider
    {
        Repository Repository { get; }
    }
}
