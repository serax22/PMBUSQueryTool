using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMBUSQueryTool
{
    public class QueryResultObject
    {
        //query process paramter
        public bool queryResponseStatus;
        public string queryType;
        public string debugMsg;
        //query result
        public string command;
        public string description;        
        public string result;
        public string queryCommandResponse;
        public string displayResult;
    }
}
