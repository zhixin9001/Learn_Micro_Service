using RuPeng.ThriftTest1.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThriftTest1.Server
{
    class CalcServiceImp : CalcService.Iface
    {
        public int Add(int i1, int i2)
        {
            return i1 + i2;
        }
    }
}
