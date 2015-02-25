using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myembro.DataAccess
{
     [CLSCompliant(false)]
    public class StuffFactory:IDisposable
    {
        private readonly EmbroDBManager _manager;
        static readonly DataTablesFormer colMapFiller = new DataTablesFormer();
         CommandFormer commandFormer = null;

        public StuffFactory()
        {
            _manager = new EmbroDBManager();

        }

        public EmbroDBManager Manager { get { return _manager; } }
        public DataTablesFormer TablesHolder { get { return colMapFiller; } }
        public CommandFormer Commands { get
        {
            if (commandFormer == null) commandFormer = new CommandFormer(_manager.Connection);

            return commandFormer;
        } }

         public void Dispose()
         {
             if (_manager != null) _manager.Dispose();
         }
    }
}