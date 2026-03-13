using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    internal class Program
    {
        public const bool _DEBUG_MODE_ = false;
        static void Main(string[] args)
        {
            //if(Program._DEBUG_MODE_)TestMain();

            DbApp db = new DbApp(); //there will only ever be one of these
            db.GoDataBase();
        }
    }
}
