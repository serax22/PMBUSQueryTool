using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.VisaNS;
using System.Threading;

namespace PMBUSQueryTool
{
    public class GPIB
    {        
        public string AC_GPIBADDRESS { set; get; }// GPIB address
        public string LOAD_GPIBADDRESS { set; get; }// GPIB address
        public static string ModelNameInitial_ACSource = "615"; 
        public static string ModelNameInitial_DCLoad = "632"; 
        //public static string ModelNameInitial_PowerMeter = "66";

        private void InitGpib()
        {
            AC_GPIBADDRESS = Get_Gpib(ModelNameInitial_ACSource);
            LOAD_GPIBADDRESS = Get_Gpib(ModelNameInitial_DCLoad);
        }
        private string GPIB_QUERY(string _Gpib, string _Command)
        {
            //throw new NotImplementedException();

            MessageBasedSession GpibInstrument = (MessageBasedSession)NationalInstruments.VisaNS.ResourceManager.GetLocalManager().Open(_Gpib);
            string _returnString;

            try
            {
                _returnString = GpibInstrument.Query(_Command);

                if (GpibInstrument.LastStatus == VisaStatusCode.Success)
                {
                    //GpibInstrument.Dispose();
                    return _returnString;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        private void GoToLocal(string _Gpib)
        {
            GpibSession vi = (GpibSession)NationalInstruments.VisaNS.ResourceManager.GetLocalManager().Open(_Gpib);
            vi.ControlRen(RenMode.AddressAndGtl);
        }
        private bool GPIB_Write(string _Gpib, string _Command)
        {            
            try
            {
                MessageBasedSession GpibInstrument = (MessageBasedSession)NationalInstruments.VisaNS.ResourceManager.GetLocalManager().Open(_Gpib);
                GpibInstrument.Write(_Command);

                if (GpibInstrument.LastStatus == VisaStatusCode.Success)
                {                    
                    return true;
                }
                else
                {                    
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }
        public string Get_Gpib(string modelName)
        {
            string GPIBStr = string.Empty;
            string[] resources = NationalInstruments.VisaNS.ResourceManager.GetLocalManager().FindResources("GPIB0::?*::INSTR");
            for (int i = 0; i < resources.Length; i++)  // Find the instrument
            {
                string responseString = GPIB_QUERY(resources[i], "*IDN?");
                GPIB_Write(resources[i], "*RST");
                GoToLocal(resources[i]);
                if (responseString != null)
                {
                    if (responseString.Split(',')[1].StartsWith(modelName))
                    {
                        GPIBStr = resources[i];                       
                    }
                }
            }
                        
            return GPIBStr;
            
        }

        //Load
        public bool Load_On()
        {
            return GPIB_Write(LOAD_GPIBADDRESS, "LOAD ON");
        }
        public bool Load_Off()
        {
            return GPIB_Write(LOAD_GPIBADDRESS, "LOAD OFF");
        }
        public bool Load_Set_Value(string _Iout)
        {
            return GPIB_Write(LOAD_GPIBADDRESS, "CURR:STAT:L1 " + _Iout);
        }

        //AC
        public bool AC_Output_On()
        {
            return GPIB_Write(AC_GPIBADDRESS, "OUTP ON");
        }

        public bool AC_Output_Off()
        {
            return GPIB_Write(AC_GPIBADDRESS, "OUTP OFF");
        }
       
        public bool setValue(string _Vin, string _Freq)
        {
            if (!GPIB_Write(AC_GPIBADDRESS, "RANG HIGH"))  // Set range
            {
                return false;
            }

            
            if (!GPIB_Write(AC_GPIBADDRESS, "SOUR:VOLT " + _Vin + ";:FREQ " + _Freq)) // Set values
            {
                return false;
            }

           
            return true;
        }
        public bool AC_Set_Value(double _Vin, double _Freq)
        {
            if (setValue(_Vin.ToString(), _Freq.ToString()))
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public void settingTestEnviroment(bool onOFF, string deviceAddress)
        {
            InitGpib();
            Console.WriteLine(AC_GPIBADDRESS,LOAD_GPIBADDRESS);
            if (onOFF)
            {                
                AC_Set_Value(230,50);
                Load_Set_Value(5.ToString());
                AC_Output_On();
                Load_On();
                Thread.Sleep(3000);
            }
            else
            {                
                AC_Output_Off();
                Load_Off();
                AC_Set_Value(0,0);
                Load_Set_Value(0.ToString());              
                Thread.Sleep(200);
            }

        }

    }
}
