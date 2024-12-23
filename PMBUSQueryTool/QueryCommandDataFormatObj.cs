using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMBUSQueryTool
{
    //SPEC:PMBus™ Power SysteSem Management PrSpecification Part II –Protocol Command Language
    /// <summary>
    /// Table 8. QUERY Command Returned Data Byte Format
    /// </summary>
    public class QueryCommandDataFormatObj 
    {
        public string result=string.Empty;
        public bool commandIsSupport = false;
        public bool commandSupportWrite = false;
        public bool commandSupportRead = false;
        public string dataformatType=string.Empty; //2-4 bit        
    }
}
