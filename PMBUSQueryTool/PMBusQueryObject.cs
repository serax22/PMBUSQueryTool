using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PMBUSQueryTool
{   
    public class PMBusQueryObject
    {
        public string slaveaddress; //actual is the same slave address
        public string querytype;
        public string description;
        public string transactiontype;
        public string command;
        public string querystatus;
        public string queryresult;
        public string writeBuf;
    }
}
