using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIDP.SAA;
using static Free.RemotingConfigurationHelper;


namespace PMBUSQueryTool
{
    public class QueryRange
    {        
        private static bool IsAdapterOK = false;
        public static int Active_adapter_num { get; set; }       
       
        private static string TI_PMBus_Address { get; set; }
        TIBusAdapters Adapter = new TIBusAdapters();
        
        public string queryCommandCode = "1A";
        public string queryBuffer = "88";
        //commandtype
        //"Linear Data Format";
        public string dataformatType_Linear = "000";
        //"16 bit signed number";
        public string dataformatType_16BitSignedNumber = "001";
        //"Reserved";
        public string dataformatType_Reserved = "010";
        //"Direct Mode Format
        public string dataformatType_DirectMode = "011";
        //"8 bit unsigned number";
        public string dataformatType_8BitUnsignedNumber = "100";
        //"VID Mode Format";
        public string dataformatType_VIDModeFormat = "101";
        //"Manufacturer specific format ";
        public string dataformatType_ManufacturerSpecificFormat = "110";
        //Command does not return numeric data.  This is also used for commands that return blocks of data
        public string dataformatType_BlockOfData = "111";

        public string ADAPTER_IS_READY = "Ready.";
        public string RESULT_IS_EMPTY = "N/A";
        private static int LINEAR_SIGNED_UNSIGNED_MIDDLEPOINT = Convert.ToInt32("01111", 2);
        private static string VOUT_MODE_ADDRESS = "20";
        private static string READ_VOUT_ADDRESS = "8B";


        //Query range:86~FC
        public List<string> AddressList = new List<string>() 
        {"20","86","87","88","89","8B","8C","8D","8E","8F","90","91","96","97","98","9A","9B","9E","9F",
         "A0","A1","A3","A6","A7","A8","A9","C0","C1","D0","D4","D5","D6","D7","D8","D9",
         "DD","DE","DF","E0","F0","F1","FC"};

        public List<string> DesciprtionList = new List<string>()
        {
            "VOUT_MODE","READ_EIN","READ_EOUT","READ_VIN","READ_IIN","READ_VOUT","READ_IOUT",
            "READ_TEMPERATURE_1",//(Ambient)
            "READ_TEMPERATURE_2",// (Hot Spot)"
            "READ_TEMPERATURE_3", //(PFC Heatsink)"
            "READ_FAN_SPEED_1",
            "READ_FAN_SPEED_2", //(if available)"
            "READ_POUT","READ_PIN","PMBUS_REVISION","MFR_MODEL","MFR_REVISION","MFR_SERIAL",
            "APP_PROFILE_SUPPORT","MFR_VIN_MIN","MFR_VIN_MAX","MFR_PIN_MAX","MFR_IOUT_MAX",
            "MFR_POUT_MAX","MFR_TAMBIENT_MAX","MFR_TAMBIENT_MIN",
            "MFR_MAX_TEMP_1",// (Ambient)"
            "MFR_MAX_TEMP_2",// (hot Spot)
            "MFR_WAKEUP_REDUNDANCY","MFR_HW_COMPATIBILITY",
            "MFR_FWUPLOAD_CAPABILITY","MFR_FWUPLOAD_MODE","MFR_FWUPLOAD","MFR_FWUPLOAD_STATUS",
            "MFR_FW_REVISION","MFR_REAL_TIME_BLACK_BOX","MFR_SYSTEM_BLACK_BOX",
            "MFR_BLACKBOX_CONFIG","MFR_CLEAR_BLACKBOX","MFR_PWOK_WARNING_Time",
            "MFR_MAX_IOUT_CAPABILITY","Current_Sharing_Control"};

        public List<string> Unit = new List<string>()
        {
            " V"," V"," V"," V"," A"," V"," A",
            " ℃"," ℃"," ℃",
            " RPM"," RPM",
            " W"," W",""," Model","","",
            ""," V"," V"," W"," A",
            " P"," ℃"," ℃",
            " ℃",
            " ℃",
            "","",
            "","","","",
            "","","",
            "","","",
            " A","" };

        
        public List<string> TransactionResponseList = new List<string>()
        {
            "Read Byte w/PEC","Read Byte w/PEC","Block Read w/ PEC","Block Read w/ PEC",
            "Read Word w/PEC","Read Word w/PEC","Read Word w/PEC","Read Word w/PEC","Read Word w/PEC",
            "Read Word w/PEC","Read Word w/PEC","Read Word w/PEC","Read Word w/PEC","Read Word w/PEC",
            "Read Word w/PEC","Read Byte w/PEC","Block Read","Block Read","Block Read",
            "Read Byte w/PEC","Read Word w/PEC","Read Word w/PEC","Read Word w/PEC","Read Word w/PEC",
            "Read Word w/PEC","Read Word w/PEC","Read Word w/PEC","Read Word w/PEC","Read Word w/PEC",
            "Read/Write Byte w/PEC","Read Word w/PEC","Read Byte w/PEC","Read/Write Byte w/PEC",
            "Bloack Write w/ PEC",// (size = block size from image header)"
            "Read Word w/PEC","Block Read w/PEC",//(3 bytes)
            "Block Read w/ PEC",// (230 byes)
            "Block Write/Read w/ PEC",// (4 bytes)
            "Block Write/Read w/ PEC",// (40 bytes)
            "Read/Write Byte with PEC",// (1 byte)
            "Send Byte with PEC",
            "Read/Write Word with PEC",// (2 bytes)
            "Block Read w/ PEC",   
            "Read/Write Byte with PEC"// (1 byte)"                           
        };
        
        
        //ReadBuf,resultvalue
        //WriteBuf,""
        public QueryResultObject ReadByte(string slaveAddress,string commandcode)
        {
            QueryResultObject obj = new QueryResultObject();
            obj.queryType = "ReadByte";

            if (IsAdapterOK == false)
            {                                
                obj.debugMsg = "Check adapter.";
                obj.result = "N/A";                
                return obj;
            }
            else
            {
                IsAdapterOK = true;
            }

            IReadByteResult result;
            TI_PMBus_Address = slaveAddress;    
            byte _addr = 0;
            if (TI_PMBus_Address.Length > 0)
            {
                _addr = Convert.ToByte(Convert.ToInt32(slaveAddress, 16) >> 1);
            }
            else
            {
                _addr = Convert.ToByte(Convert.ToInt32(slaveAddress, 16) >> 1);
            }
            byte _cmdcode = Convert.ToByte(commandcode, 16);

            result = Adapter.Read_Byte(Active_adapter_num, _addr, _cmdcode);

            if (result.Success)
            {
                obj.queryResponseStatus = true;
                string resultvalue = result.Byte.ToString("X");                
                obj.result = resultvalue;
                obj.debugMsg = "Success.";
                return obj;
            }
            else
            {
                obj.queryResponseStatus = false;
                string resultvalue = "N/A";                
                obj.result = resultvalue;
                obj.debugMsg = "Read byte fail";
                return obj;
            }
        }


        public QueryResultObject ReadWord(string slaveAddress,string commandcode)
        {
            QueryResultObject obj = new QueryResultObject();
            obj.queryType = "ReadWord";
           
            if (IsAdapterOK == false)
            {               
                obj.debugMsg = "Check adapter.";
                return obj;
            }
            else
            {
                IsAdapterOK = true;
            }

            IReadWordResult result;
           
            byte _addr = 0;
            if (slaveAddress.Length > 0)
            {
                _addr = Convert.ToByte(Convert.ToInt32(slaveAddress, 16) >> 1);
            }
            else
            {
                _addr = Convert.ToByte(Convert.ToInt32(slaveAddress, 16) >> 1);
            }
            byte _cmdcode = Convert.ToByte(commandcode, 16);

            result = Adapter.Read_Word(Active_adapter_num, _addr, _cmdcode);

            if (result.Success)
            {               
                obj.queryResponseStatus = true;
                obj.result = result.Word.Hex.Substring(2);
                obj.debugMsg = "Success.";                              
                return obj;
            }
            else
            {
                obj.queryResponseStatus = false;
                obj.debugMsg = "Read word fail.";
                obj.result = "N/A";               
                return obj;
            }
        }
        
        public QueryResultObject ReadBlock(string slaveAddress, string commancode)
        {

            QueryResultObject obj = new QueryResultObject();
            obj.queryType = "ReadBlock";

            if (IsAdapterOK == false)
            {                
                obj.result="N/A";
                obj.debugMsg = "Check adapter.";               
                return obj;
            }
            else
            {
                IsAdapterOK = true;
            }

            IReadBlockResult result;

            byte _addr = 0;
            if (slaveAddress.Length > 0)
            {
                _addr = Convert.ToByte(Convert.ToInt32(slaveAddress, 16) >> 1);
            }
            else
            {
                _addr = Convert.ToByte(Convert.ToInt32(slaveAddress, 16) >> 1);
            }
            byte _cmdcode = Convert.ToByte(commancode, 16);

            result = Adapter.Read_Block(Active_adapter_num, _addr, _cmdcode);
            
            if (result.Success)
            {
                obj.queryResponseStatus = true;
                string resultvalue = result.Block.Hex.Substring(2);                
                obj.result = resultvalue;                
                obj.debugMsg = "Success.";
                return obj;                
            }
            else
            {
                obj.queryResponseStatus = false;
                obj.result = "N/A";
                obj.debugMsg = "Read block fail.";
                return obj; 
            }
            return obj;
        }
        public QueryResultObject BlockWriteBlockRead(string slaveAddress, string commandcode,string writeBuf)
        {
            QueryResultObject obj = new QueryResultObject();
            if (IsAdapterOK == false)
            {   
                obj.result = "N/A";
                obj.debugMsg = "Check adapter.";                              
            }
            else
            {
                IsAdapterOK = true;
            }

            IReadWordResult result;

            byte _addr = 0;
            if (slaveAddress.Length > 0)
            {
                _addr = Convert.ToByte(Convert.ToInt32(slaveAddress, 16) >> 1);
            }
            else
            {
                _addr = Convert.ToByte(Convert.ToInt32(slaveAddress, 16) >> 1);
            }
            byte _cmdcode = Convert.ToByte(commandcode, 16);
            

            if (writeBuf == "") // this tool only support read
            { 
                obj.result = "N/A";
                obj.debugMsg = "Empty write string.";
                return obj;
            }

            if (writeBuf.Length > 2)
            {  
                obj.result = "N/A";
                obj.debugMsg = "Only 1 byte is allowed.";                
                return obj;
            }

            int _value = Convert.ToInt32(writeBuf, 16);
            byte _wrbuf = Convert.ToByte(_value);

            result = Adapter.Process_Call(1, _addr, _cmdcode, 0x01, _wrbuf);

            if (result.Success)
            {
                obj.queryResponseStatus = true;
                obj.debugMsg = "Success.";
                obj.result = result.Bytes[1].ToString("X2");                
                return obj;
            }
            else
            {
                obj.queryResponseStatus = false;
                obj.debugMsg = "Read byte fail.";
                obj.result = "N/A";                
                return obj;                
            }
        }
        public QueryResultObject Init()
        {
            QueryResultObject obj = new QueryResultObject();
            string result = string.Empty;
            if (Adapter.Discover() == 0)
            {
                //form1.textBox_Debug.Text = "No TI USB adapter found.";
                obj.debugMsg = "No TI USB adapter found.";
                IsAdapterOK = false;

            }
            else if (Adapter.Discover() > 1)
            {                
                obj.debugMsg = "More than 1 TI USB adapter is found.";
                IsAdapterOK = false;
            }
            else
            {                
                obj.debugMsg = "Ready.";
                IsAdapterOK = true;                
                Active_adapter_num = 1;
            }
            return obj;
        }
        

        public bool checkCommandIsSupportStatus(string commandIsSupport)
        {
            bool result = false;
            if (commandIsSupport == "1")
                result = true;
            
            return result;
        }
        

        /*getQueryTypeInformation
        query type(read byte/read word/block read/block read write)
        => query command address(1A)=>get dataframe
        =>parse query type bit=>get correct query command response type : AC/A0/BC/FC/DC/E0/F8*/
        
        public string getQueryCommandTypeByAddress(string command)
        {
            string queryType = string.Empty;
            switch (command)
            {
                case "86":
                case "87":
                case "9A":
                case "9B":
                case "9E":
                case "D9":
                case "DC":
                case "F1":
                case "DD":
                case "DE":
                    queryType = "ReadBlock";
                    break;
                case "88":
                case "89":
                case "8B":
                case "8C":
                case "8D":
                case "8E":
                case "8F":
                case "90":
                case "91":
                case "96":
                case "97":
                case "A0":
                case "A1":
                case "A3":
                case "A6":
                case "A7":
                case "A8":
                case "A9":
                case "C0":
                case "C1":
                case "D4":
                case "D8":
                case "F0":
                    queryType = "ReadWord";
                    break;
                case "20":
                case "98":
                case "9F":
                case "D0":
                case "D5":
                case "D6":
                case "DF":
                case "FC":
                    queryType = "ReadByte";
                    break;                    
                case "D7":
                    queryType = "BlockWriteBlockRead";
                    break;
                default:
                    break;
            }
            return queryType;
        }
       
            
        public List<string> getQueryTable(List<PMBusQueryObject> addressList)
        {
            
            string commandcode = "1A";
            List<string> resultList = new List<string>();
            List<QueryCommandDataFormatObj>dataformatObjList = new List<QueryCommandDataFormatObj>();
            string devieAddress = addressList[0].slaveaddress;

            for (int i =0;i< addressList.Count;i++)
            {                
                string queryBuffer = addressList[i].command;
                QueryCommandDataFormatObj queryCommandResponse = new QueryCommandDataFormatObj();
                QueryResultObject resultObj = new QueryResultObject();
                resultObj = BlockWriteBlockRead(devieAddress, commandcode, queryBuffer);
                string result = resultObj.result;
                resultList.Add(result);               
            }
            
            return resultList;    

        }
            
        
        public List<QueryCommandDataFormatObj> packetQueryCommandResponse(List<string> dataformatInfoList)
        {
            List<QueryCommandDataFormatObj> returnDataFormObjList = new List<QueryCommandDataFormatObj>();

            for (int i = 0; i < dataformatInfoList.Count; i++)
            {
                string result = dataformatInfoList[i];
                if (result.Contains("N/A") || result.Contains("00"))
                {
                    QueryCommandDataFormatObj dataformatObj = new QueryCommandDataFormatObj();
                    dataformatObj.result = "N/A";
                    returnDataFormObjList.Add(dataformatObj);
                }
                else
                {
                    string processResultStr = result;
                    int intResult = Convert.ToInt32(processResultStr, 16);
                    string intResultStr = Convert.ToString(intResult, 2);
                    
                    string tmp1 = intResultStr;
                    string tmp2 = intResultStr;
                    string tmp3 = intResultStr;

                    string dataformatType = intResultStr.Substring(3, 3);
                    string readStatus = tmp1.Substring(2, 1);
                    string writeStatus = tmp2.Substring(1, 1);
                    string supportStatus = tmp3.Substring(0, 1);

                    bool commandIssupportForReadStatus = checkCommandIsSupportStatus(readStatus);
                    bool commandIssupportForWriteStatus = checkCommandIsSupportStatus(writeStatus);
                    bool commandIssupportStatus = checkCommandIsSupportStatus(supportStatus);

                    QueryCommandDataFormatObj dataformatObj = new QueryCommandDataFormatObj();
                    dataformatObj.result = result;
                    dataformatObj.dataformatType = dataformatType;
                    dataformatObj.commandIsSupport = commandIssupportStatus;
                    dataformatObj.commandSupportRead = commandIssupportForReadStatus;
                    dataformatObj.commandSupportWrite = commandIssupportForWriteStatus;                    
                    returnDataFormObjList.Add(dataformatObj);
     
                }

            }
            return returnDataFormObjList;
        }
        
        
        public List<QueryResultObject> doQuery(List<PMBusQueryObject> objList)
        {
            List<QueryResultObject> returnObjList = new List<QueryResultObject>();
            List<QueryResultObject> returnParseObjList = new List<QueryResultObject>();

            QueryResultObject initReturnObj = new QueryResultObject();
            initReturnObj = Init();
            
            if (initReturnObj.debugMsg != ADAPTER_IS_READY)
                return returnObjList;

            List<string> dataformatObjList = getQueryTable(objList);
            List<QueryCommandDataFormatObj> queryCommandDataformatObjList = packetQueryCommandResponse(dataformatObjList);

            for (int i = 0; i < objList.Count; i++)
            {
                PMBusQueryObject obj = objList[i];
                string address = obj.slaveaddress;                              
                string command = obj.command;
                string writeBuf = obj.writeBuf;

                QueryResultObject finalReturnObj = new QueryResultObject();                 
                
                string commandType = getQueryCommandTypeByAddress(command);
                switch (commandType)
                {
                    case "ReadByte":
                        finalReturnObj = ReadByte(address, command);
                        break;
                    case "ReadWord":
                        finalReturnObj = ReadWord(address, command);
                        break;
                    case "ReadBlock":
                        finalReturnObj = ReadBlock(address, command);
                        break;
                    case "BlockWriteBlockRead":
                        finalReturnObj = BlockWriteBlockRead(address, command, queryBuffer);
                        break;
                    default:
                        break;
                }

                finalReturnObj.queryCommandResponse = queryCommandDataformatObjList[i].result;   
                finalReturnObj.command = command;
                finalReturnObj.description = objList[i].description;
                returnObjList.Add(finalReturnObj);
                
            }
            returnParseObjList = processDataDisplay(returnObjList, queryCommandDataformatObjList);
            returnParseObjList = addCustomizedResult(returnParseObjList);
            return returnParseObjList;
        }
        private string signedUnsingedTransfer(string signedValueStr)
        {
            int valueInt = Convert.ToInt32(signedValueStr, 2);
            string unsignedValueStr = string.Empty;
            
            //signed or unsigned value transfer
            if (valueInt > LINEAR_SIGNED_UNSIGNED_MIDDLEPOINT)
            {
                
                for (int i = 0; i < signedValueStr.Length; i++)
                {
                    string target = signedValueStr.Substring(i, 1);
                    if (target == "1")
                    {
                        unsignedValueStr += "0";
                    }
                    else if (target == "0")
                    {
                        unsignedValueStr += "1";
                    }
                }
               
            }
            else
            {
                unsignedValueStr = signedValueStr;
            }
                        
            return unsignedValueStr;
        }
        private int transferSingnedValueToUnsigned(string trnasferBinaryStr,int unsignedMiddlePoint)
        {
            int result = 0;
            int valueInt = Convert.ToInt32(trnasferBinaryStr, 2);
            string unsignedBinaryStr = string.Empty;

            if (valueInt > LINEAR_SIGNED_UNSIGNED_MIDDLEPOINT)
            {
                unsignedBinaryStr = signedUnsingedTransfer(trnasferBinaryStr);
                result = (Convert.ToInt32(unsignedBinaryStr, 2) + 1) * -1;
            }
            else
            {
                result = valueInt;
            }
            return result;
        }
        private double linearDataProcess(string hexString)
        {
            string binaryString = string.Join(string.Empty, hexString.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            
            string binaryStringLinear1 = binaryString;
            string binaryStringLinear2 = binaryString;
            string linear1BinaryStr = binaryStringLinear1.Substring(0, 5);
            string linear2BionaryStr = binaryStringLinear2.Substring(5, 11);
            int power = transferSingnedValueToUnsigned(linear1BinaryStr, LINEAR_SIGNED_UNSIGNED_MIDDLEPOINT);

            int baseNum = 2;            
            int linear2 = Convert.ToInt32(linear2BionaryStr, 2);            
            double result = Math.Pow(baseNum, power) * linear2;            
            return result;
        }
        private string hex2Int2Binary(string hexString)
        {
            int valueInt = Convert.ToInt32(hexString, 16);
            string strBinary = Convert.ToString(valueInt, 2);
            return strBinary;
        }
        private List<QueryResultObject> addCustomizedResult(List<QueryResultObject> objList)
        {
            List<QueryResultObject> resultList = new List<QueryResultObject>();
            bool vout_mode_exist = false;
            bool read_vout_exist = false;
            int read_vout_index = -1;
            for (int index = 0; index < objList.Count;index++)
            {
                if(index == 0)
                {
                    if(objList[index].command == VOUT_MODE_ADDRESS)
                    {
                        vout_mode_exist = true;    
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                if (objList[index].command == READ_VOUT_ADDRESS)
                {
                    read_vout_exist = true;
                    read_vout_index = index;
                    break;
                }

            }
            if(vout_mode_exist && read_vout_exist)
            {
                string vout_mode_hex = objList[0].result;
                string vout_mode_binary = hex2Int2Binary(vout_mode_hex);
                string read_vout = objList[read_vout_index].result;

                int vout_mode_int = transferSingnedValueToUnsigned(vout_mode_binary, LINEAR_SIGNED_UNSIGNED_MIDDLEPOINT);
                int read_vout_int = Convert.ToInt32(read_vout, 16);
                double baseNum = 2;
                double powNum = Math.Pow(baseNum, vout_mode_int);
                double customized_vout_value = powNum* read_vout_int;
                objList[read_vout_index].displayResult = customized_vout_value.ToString() + Unit[read_vout_index];

            }
            
            resultList = objList;
            return resultList;
        }
        public List<QueryResultObject> processDataDisplay(List<QueryResultObject> objList,List<QueryCommandDataFormatObj>dataFormatObjList)
        {
            List<QueryResultObject> displayObjList = new List<QueryResultObject>();
            for(int index=0;index<objList.Count;index++)
            {
                string dataFormatType = dataFormatObjList[index].dataformatType;
                string resultValue = objList[index].result;

                if (resultValue == null||resultValue.Contains("N/A") || resultValue == string.Empty)    
                    objList[index].displayResult = RESULT_IS_EMPTY;
                else
                {
                    string unit = Unit[index];
                    string valueProcess = string.Empty;
                    string displayResult = string.Empty;
                    switch (dataFormatType)
                    {
                        case "000": // Linear data format(16 bit),A0
                            valueProcess = resultValue;                            
                            displayResult = linearDataProcess(valueProcess).ToString() + unit;
                            objList[index].displayResult = displayResult;                            
                            break;
                        case "001": // 16 bit signed number                             
                            displayResult = Convert.ToInt32(resultValue, 2).ToString() + unit;
                            objList[index].displayResult = displayResult;
                            break;
                        case "011": // Direct Mode Format used,AC
                            displayResult = resultValue;
                            objList[index].displayResult = displayResult;
                            break;
                        case "100": // 8 bit unsigned number 
                            displayResult = Convert.ToInt32(resultValue, 2).ToString() + unit;
                            objList[index].displayResult = displayResult;
                            break;
                        // VID Mode Format used 
                        // temporarily no requirement fo this command type
                        case "101":
                            //displayResult = resultValue;                           
                            //objList[index].displayResult = displayResult;
                            break;
                        //Manufacturer specific format used                        
                        case "110"://F8
                            displayResult = resultValue;                            
                            objList[index].displayResult = displayResult+unit;
                            break;
                        //Command does not return numeric data.
                        //This is also  used for commands that return blocks of data.                        
                        case "111"://BC
                            displayResult = resultValue;                            
                            objList[index].displayResult = displayResult+unit;
                            break;
                    }
                }
            }
            displayObjList = objList;
            return displayObjList;

        }
    }
}
