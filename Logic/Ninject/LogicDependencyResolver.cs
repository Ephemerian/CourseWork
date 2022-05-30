using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace Logic
{
    public static class LogicDependencyResolver
    {
        static UnitOfWork UoW;

        static LogicDependencyResolver()
        {
            UoW = new UnitOfWork();
        }

        public static IUnitOfWork ResolveUoW()
        {
            return UoW;
        }
    }
}
