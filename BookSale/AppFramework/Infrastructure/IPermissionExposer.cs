using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Infrastructure
{
    public interface IPermissionExposer
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }
}
