using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIDP.SAA;

namespace PMBUSQueryTool
{
    public partial class PMBUSQueryTool : Form
    {
        
        private static bool IsAdapterOK = false;
        public static int Active_adapter_num { get; set; }
        private QueryRange query = new QueryRange();
        private int defaultInterval = 1000;
        private int delayInterval = 3000;
        public bool pollingflag = true;
        public bool timerStopFlag = false;
        public System.Windows.Forms.Timer myTimer;

        public static int RESPONSE_CASE_CHECKADAPTER = -1;
        public static int RESPONSE_CASE_OK = 0;
        public static int RESPONSE_CASE_POLLING_STOP = 1;
        public static int RESPONSE_CASE_TEST = 2;

        public string pollinglogmsg = string.Empty;
        public bool selectedAllFlag = false;
        

        public PMBUSQueryTool()
        {
            InitializeComponent();
            InitCheckBoxList();
        }
        private void InitCheckBoxList()
        {
            string checkBoxItemStr = String.Empty;
            List<string> commandList = query.AddressList;
            List<string> descriptions = query.DesciprtionList;

            for(int i= 0;i< commandList.Count;i++)
            {
                checkBoxItemStr = String.Empty;
                checkBoxItemStr += commandList[i] + " "+ descriptions[i];
                this.checkedListBox1.Items.Add(checkBoxItemStr);
            }
            
        }

        private void ClearTextBox()
        {
            this.textBox_Debug.Text = string.Empty;
        }
        private void updateDebugTextBox(int caseNum)
        {
            switch (caseNum)
            {
                case -1:
                    this.textBox_Debug.Text = "Check Adpter";
                    break;
                case 0:
                    this.textBox_Debug.Text = "Adpter is ok.";
                    break;
                case 1:
                    this.textBox_Debug.Text = "Polling stop.";
                    break;
                case 2:
                    this.textBox_Debug.Text = "Test data.";
                    break;
            }
        }
        private static void AddTextToFile(string value)
        {
            
            DateTime myDate = DateTime.Now;
            string filename = "PMBUSQueryTool_" + myDate.ToString("yyyy-MM-dd_HHmmss");            
            string path = @".\Log\" + filename;
           
            try
            {
                path += ".txt";
                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write);
                fs.Close();
                StreamWriter sw = new StreamWriter(path, true, Encoding.ASCII);
                sw.Write(value);
                sw.Close();
            }
            catch
            {

            }
        }
        public string AssembleLog(List<QueryResultObject> objList)
        {
            string msg = string.Empty;
            foreach (QueryResultObject obj in objList)    
            {
                msg += obj.command+"  "+obj.description +"  "
                      +obj.queryCommandResponse+"  "+obj.result+"  "+ obj.displayResult+"\r\n";
            }

            return msg;
            
        }
        
        private List<QueryResultObject> doQueryTask()
        {
            List<PMBusQueryObject> queryItemList = new List<PMBusQueryObject>();
            List<QueryResultObject> resultObjList = new List<QueryResultObject>();
           
            QueryRange query = new QueryRange();

            foreach (int index in checkedListBox1.CheckedIndices)
            {
                PMBusQueryObject obj = new PMBusQueryObject();
                obj.slaveaddress = this.txextBox_Address.Text;
                obj.description = query.DesciprtionList[index];
                //obj.transactiontype = query.TransactionResponseList[index];
                obj.command = query.AddressList[index];
                queryItemList.Add(obj);
               
            }
            
            resultObjList = query.doQuery(queryItemList);
            if(resultObjList.Count == 0)
            {
                updateDebugTextBox(RESPONSE_CASE_CHECKADAPTER);
            }
            else 
            {
                updateDebugTextBox(RESPONSE_CASE_OK);
            }
            return resultObjList;  

        }
        private void packetDatagridDataSource(List<QueryResultObject> objList)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Address");
            dataTable.Columns.Add("Description");
            dataTable.Columns.Add("Query");
            dataTable.Columns.Add("Raw data");
            dataTable.Columns.Add("Display  Value");


            foreach (QueryResultObject obj in objList)
            {
                dataTable.Rows.Add(obj.command, obj.description, obj.queryCommandResponse, obj.result, obj.displayResult);

            }
            dataGridView1.DataSource = dataTable;
        }
        private void settingDatagridFieldWidth()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                // Store Auto Sized Widths:
                int colw = dataGridView1.Columns[i].Width;
                // Remove AutoSizing:
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                // Set Width to calculated AutoSize value:
                int width = 50;
                switch (i)
                {
                    case 0:
                        width = 60;
                        break;
                    case 2:
                        width = 70;
                        break;
                    case 1:
                        width = 130;
                        break;
                    case 3:
                        width = 140;
                        break;
                    case 4:
                        width = 150;
                        break;

                }
                dataGridView1.Columns[i].Width = width;
            }
        }
        private void queryAndProcessDataValue()
        {
            GPIB gpib = new GPIB();
            gpib.settingTestEnviroment(true, this.txextBox_Address.Text);
            List<QueryResultObject> objList = doQueryTask();

            //Log
            string msg = AssembleLog(objList);
            AddTextToFile(msg + "\r\n");

            packetDatagridDataSource(objList);
            settingDatagridFieldWidth();
            
            //gpib.settingTestEnviroment(false, this.txextBox_Address.Text);
        }
        private void Query_Click_1(object sender, EventArgs e)
        {
            queryAndProcessDataValue();
        }

        private void updateDataTable(object sender, EventArgs e)
        {
            if (timerStopFlag)
            {
                myTimer.Stop();
                return;
            }
            else
            {
                pollinglogmsg += "----------------------------------------------------";
                pollinglogmsg += "----------------------------------------------------";
                pollinglogmsg += "\r\n";

                List<QueryResultObject> objList = doQueryTask();
                pollinglogmsg += AssembleLog(objList);
                packetDatagridDataSource(objList);
                settingDatagridFieldWidth();
            }


        }
        private void polling_button_Click(object sender, EventArgs e)
        {
            int sleeptimeinterval = defaultInterval; //s
            sleeptimeinterval = int.Parse(this.textBox_interval.Text) * 1000;

            ClearTextBox();

            GPIB gpib = new GPIB();
            gpib.settingTestEnviroment(true, this.txextBox_Address.Text);

            List<QueryResultObject> objList = doQueryTask();
            pollinglogmsg += AssembleLog(objList);

            //setting timer
            timerStopFlag = false;
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Interval = sleeptimeinterval; // 設置間隔為 1 秒
            myTimer.Tick += new EventHandler(updateDataTable);            
            myTimer.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timerStopFlag = true;
            AddTextToFile(pollinglogmsg + "\r\n");                             
            updateDebugTextBox(RESPONSE_CASE_POLLING_STOP);

            GPIB gpib = new GPIB();
            gpib.settingTestEnviroment(false, this.txextBox_Address.Text);
        }

        private void checkBox1_all_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedAllFlag)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                selectedAllFlag = false;
            }
            else
            {            

                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
                selectedAllFlag = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
