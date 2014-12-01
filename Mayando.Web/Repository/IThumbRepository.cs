using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Myembro.Infrastructure;
using Myembro.Interfaces;
using Myembro.Models;

namespace Myembro.Repository
{
    public interface IThumbRepository
    {
        EmbroNavigationContext GetNavigationContextByCountPage(EmbroNavigationContext context);
        IWritePng2Stream GetEmbroByID(int embroID);
    }
}
