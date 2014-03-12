using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mayando.Web.Infrastructure;

namespace Mayando.Web.Repository
{
    public interface IThumbRepository
    {
        EmbroNavigationContext GetNavigationContextByCountPage(EmbroNavigationContext context);
    }
}
