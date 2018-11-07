using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using MySql.Data.MySqlClient;

namespace LostOrderUtils
{
    public partial class Percona2BpmOrdersSync : Form
    {
        #region Global data
        ModelReader operations = new ModelReader();
        static List<JsonEntity> jsonEntities = new List<JsonEntity>();
        DateTime startDate;
        DateTime dueDate;
        #endregion
        #region Properties
        public Percona2BpmOrdersSync()
        {
            InitializeComponent();
            perconaPurchaseInfoList = new List<OrderAnswerInfo>();
            perconaOrderList = new List<Order>();
            bpmOrderInfoList = new List<OrderAnswerInfo>();
            bpmOrderList = new List<Order>();
            //ModelInit();
        }
        public UtilProperties utilPropertiesUpdate()
        {
            UtilProperties utilProperties = new UtilProperties
            {
                MsDbName = inputMsName.Text,
                MsServer = inputMsServer.Text,
                MsLogin = inputMsLogin.Text,
                MsPassword = inputMsPassword.Text,
                MyDbName = inputMyName.Text,
                MyServer = inputMyServer.Text,
                MyLogin = inputMyLogin.Text,
                MyPassword = inputMyPassword.Text,
                startDate = Convert.ToDateTime(textBoxDateFrom.Value),
                dueDate = Convert.ToDateTime(textBoxDateTo.Value)
            };
            return utilProperties;
        }

        public List<OrderAnswerInfo> bpmOrderInfoList { get; set; }

        public List<OrderAnswerInfo> perconaPurchaseInfoList { get; set; } 

        public List<Order> bpmOrderList { get; set; }

        public List<Order> perconaOrderList { get; set; }
        #endregion

        #region Get Properties
        public bool GetProperties()
        {
            #region MsSql Connection String
            var utilProperties = utilPropertiesUpdate();
            if (String.IsNullOrEmpty(inputMsServer.Text))
            {
                if (String.IsNullOrEmpty(utilProperties.MsServer))
                {
                    MessageBox.Show("Enter database server!", "Error", 0);
                    LogInfo("Reading failed. Enter MsSQL database server!");
                    return false;
                }
                inputMsServer.Text = utilProperties.MsServer;
            }
            else
            {
                utilProperties.MsServer = inputMsServer.Text;
            }

            if (String.IsNullOrEmpty(inputMsName.Text))
            {
                if (String.IsNullOrEmpty(utilProperties.MsDbName))
                {
                    MessageBox.Show("Enter database name!", "Error", 0);
                    LogInfo("Reading failed. Enter MsSQL database name!");
                    return false;
                }
                inputMsName.Text = utilProperties.MsDbName;
            }
            else
            {
                utilProperties.MsDbName = inputMsName.Text;
            }

            if (String.IsNullOrEmpty(inputMsLogin.Text))
            {
                if (String.IsNullOrEmpty(utilProperties.MsLogin))
                {
                    MessageBox.Show("Enter database name!", "Error", 0);
                    LogInfo("Reading failed. Enter MsSQL database user login!");
                    return false;
                }
                inputMsLogin.Text = utilProperties.MsLogin;
            }
            else
            {
                utilProperties.MsLogin = inputMsLogin.Text;
            }

            if (String.IsNullOrEmpty(inputMsPassword.Text))
            {
                if (String.IsNullOrEmpty(utilProperties.MsPassword))
                {
                    MessageBox.Show("Enter database name!", "Error", 0);
                    LogInfo("Reading failed. Enter MsSQL database password!");
                    return false;
                }
                inputMsPassword.Text = utilProperties.MsPassword;
            }
            else
            {
                utilProperties.MsPassword = inputMsPassword.Text;
            }
            #endregion

            #region Percona Connection String
            if (String.IsNullOrEmpty(inputMyServer.Text))
            {
                if (String.IsNullOrEmpty(utilProperties.MyServer))
                {
                    MessageBox.Show("Enter database server!", "Error", 0);
                    LogInfo("Reading failed. Enter Percona database server!");
                    return false;
                }
                inputMyServer.Text = utilProperties.MyServer;
            }
            else
            {
                utilProperties.MyServer = inputMyServer.Text;
            }
            if (String.IsNullOrEmpty(inputMyName.Text))
            {
                if (String.IsNullOrEmpty(utilProperties.MyDbName))
                {
                    MessageBox.Show("Enter database name!", "Error", 0);
                    LogInfo("Reading failed. Enter Percona database name!");
                    return false;
                }
                inputMyName.Text = utilProperties.MyDbName;
            }
            else
            {
                utilProperties.MyDbName = inputMyName.Text;
            }

            if (String.IsNullOrEmpty(inputMyLogin.Text))
            {
                if (String.IsNullOrEmpty(utilProperties.MyLogin))
                {
                    MessageBox.Show("Enter database name!", "Error", 0);
                    LogInfo("Reading failed. Enter Percona user login!");
                    return false;
                }
                inputMyLogin.Text = utilProperties.MyLogin;
            }
            else
            {
                utilProperties.MyLogin = inputMyLogin.Text;
            }
            if (String.IsNullOrEmpty(inputMyPassword.Text))
            {
                if (String.IsNullOrEmpty(utilProperties.MyPassword))
                {
                    MessageBox.Show("Enter database name!", "Error", 0);
                    LogInfo("Reading failed. Enter Percona user password!");
                    return false;
                }
                inputMyPassword.Text = utilProperties.MyPassword;
            }
            else
            {
                utilProperties.MyPassword = inputMyPassword.Text;
            }
            #endregion

            #region Dates
            if (String.IsNullOrEmpty(textBoxDateFrom.Text))
            {
                textBoxDateFrom.BackColor = Color.LightYellow;
                textBoxDateFrom.Text = utilProperties.startDate.ToString("yyyy-MM-dd");
            }
            else
            {
                try
                {
                    utilProperties.startDate = Convert.ToDateTime(textBoxDateFrom.Text);
                }
                catch(Exception ex)
                {
                    textBoxDateFrom.BackColor = Color.Tomato;
                    LogInfo(ex);
                    MessageBox.Show("Wrong Data format!");
                    return false;
                }
                textBoxDateFrom.BackColor = Color.White;
            }

            if (String.IsNullOrEmpty(textBoxDateTo.Text))
            {
                textBoxDateTo.BackColor = Color.LightYellow;
                textBoxDateTo.Text = utilProperties.dueDate.ToString("yyyy-MM-dd");
            }
            else
            {
                try
                {
                    utilProperties.dueDate = Convert.ToDateTime(textBoxDateTo.Text);
                }
                catch (Exception ex)
                {
                    textBoxDateTo.BackColor = Color.Tomato;
                    LogInfo(ex);
                    MessageBox.Show("Wrong Data format!");
                    return false;
                }
                textBoxDateFrom.BackColor = Color.White;
            }
            #endregion

            #region Models
            ModelRead();
            #endregion
            return true;
        }

        public void GetEntityChecked()
        {
            
        }

        public void ModelInit()
        {
           
        }

        public void ModelRead()
        {
           
        }
        #endregion

        #region HelpMethods
        private void Cleaner_Click(object sender, EventArgs e)
        {
            bpmOrderInfoList.Clear();
            perconaOrderList.Clear();
            bpmOrderList.Clear();
            perconaPurchaseInfoList.Clear();
        }

        public void LogInfo(string Message)
        {
            logInfoDataGrid.Rows.Add(DateTime.UtcNow, Message); 
        }

        public void LogInfo(Exception ex)
        {
            logInfoDataGrid.Rows.Add(DateTime.UtcNow, String.Format("Error. Message: {0}; Source: {1}", ex.Message, ex.Source));
        }

        public static void CompareModel(Dictionary<string, bool> config, DataGridViewRowCollection rows)
        {
            foreach (DataGridViewRow row in rows)
            {
                string key = row.Cells[0].FormattedValue.ToString();
                bool value = Convert.ToBoolean(row.Cells[1].FormattedValue);
                config[key] = value;
            }
        }
        #endregion

        #region Events
        private void getMsSQLInfo_Click(object sender, EventArgs e)
        {
            var utilProperties = utilPropertiesUpdate();
            string bpmConnection = String.Format(ModelReader.bpmConnectionStringFormat, utilProperties.MsServer, utilProperties.MsDbName,
                    utilProperties.MsLogin, utilProperties.MsPassword);
            using (SqlConnection sqlConnection = new SqlConnection(bpmConnection))
            {
                sqlConnection.Open();
                if (sqlConnection.State == ConnectionState.Closed)
                {
                    LogInfo("MsSQL connection error!");
                }
                else if (sqlConnection.State == ConnectionState.Open)
                {
                    LogInfo("MsSQL connection passed!");
                }
            }
        }

        private void getPerconaInfo_Click(object sender, EventArgs e)
        {
            var utilProperties = utilPropertiesUpdate();
            string perconaConnection = String.Format(ModelReader.perconaConnectionStringFormat, utilProperties.MyServer, utilProperties.MyDbName,
                utilProperties.MyLogin, utilProperties.MyPassword);
            using (MySqlConnection MyConnection = new MySqlConnection(perconaConnection))
            {
                MyConnection.Open();
                if (MyConnection.State == ConnectionState.Closed)
                {
                    LogInfo("MySQL connection error!");
                }
                else if (MyConnection.State == ConnectionState.Open)
                {
                    LogInfo("MySQL connection passed!");
                }
            }
        }

        private async void buttomFromMsToMy_Click(object sender, EventArgs e)
        {
            #region Properties
            //if (!GetProperties())
            //{
            //    return;
            //}
            //GetEntityChecked();
            //if (!(utilProperties.PushOrder || utilProperties.PushOrderProduct || utilProperties.PushTransaction))
            //{
            //    MessageBox.Show("Check Entity!");
            //    return;
            //}

            //if (bpmOrderInfoList.Count < 1)
            //{
            //    MessageBox.Show("Bpm diff collection is empty!", "Error", 0);
            //    LogInfo("Bpm diff is empty");
            //    return;
            //}
            //if (bpmOrderInfoList.Count > 1000)
            //{
            //    DialogResult result = MessageBox.Show("Bpm diff collection count > 1000. Continue?", "Error", MessageBoxButtons.YesNo);
            //    LogInfo("Bpm diff collection count > 1000");
            //    if (result == DialogResult.No)
            //    {
            //        LogInfo("Canseled");
            //        return;
            //    }
            //    else
            //    {
            //        LogInfo("Continued");
            //    }
            //}
            #endregion
            var utilProperties = utilPropertiesUpdate();
            string bpmConnection = String.Format(ModelReader.bpmConnectionStringFormat, utilProperties.MsServer, utilProperties.MsDbName,
                utilProperties.MsLogin, utilProperties.MsPassword);
            string perconaConnection = String.Format(ModelReader.perconaConnectionStringFormat, utilProperties.MyServer, utilProperties.MyDbName,
                utilProperties.MyLogin, utilProperties.MyPassword);

            startDate = Convert.ToDateTime(textBoxDateFrom.Text);
            dueDate = Convert.ToDateTime(textBoxDateTo.Text);
            if (jsonEntities.Count == 0)
            {
                LogInfo("Configuration file is empty or is not checked!");
                return;
            }

            int count = operations.ProgressBarDataFromMsSql(bpmConnection, jsonEntities, startDate, dueDate);
            label3.Text = count.ToString();
            progressBar1.Maximum = count;
            if (count == 0)
            {
                LogInfo("----- Data is not available (STOP)");
                return;
            }
            try
            {
                for (int i = 0; i < jsonEntities.Count; i++)
                {
                    List<string> insertData = new List<string>();
                    int offset = 0, limit = 100;
                    do
                    {
                        insertData = await Task.Run(() => operations.GetMsSqlData(bpmConnection, jsonEntities[i], startDate, dueDate, offset, limit));
                        await Task.Run(() => operations.PushMsSqlToMySql(perconaConnection, jsonEntities[i], insertData));
                        offset += limit;
                        progressBar1.PerformStep();
                    }
                    while (insertData.Count == limit);
                }
                progressBar1.Value = 0;
                label3.Text = "0";
                MessageBox.Show("----- Process is complete -----");
            }
            catch (Exception ex)
            {
                LogInfo(ex);
            }
            #region Comments
            //perconaOrderList.Clear();
            //if (utilProperties.PushOrder)
            //{
            //    if (!(utilProperties.PushOrderProduct || utilProperties.PushTransaction))
            //    {
            //        DialogResult result = MessageBox.Show("Are u sure about push Orders with out Products and Transactions?");
            //        if (result == DialogResult.No)
            //        {
            //            LogInfo("Canseled");
            //            return;
            //        }
            //    }
            //    try
            //    {
            //        #region Read Data
            //        bpmOrderList = UtilityHelper.GetOrderList(bpmConnection, bpmOrderInfoList, utilProperties.OrderModel);
            //        if (utilProperties.PushOrderProduct)
            //        {
            //            UtilityHelper.GetOrderProductList(bpmConnection, bpmOrderInfoList, utilProperties.OrderProductModel, bpmOrderList);
            //        }
            //        if (utilProperties.PushTransaction)
            //        {
            //            UtilityHelper.GetMsTransactionsList(bpmConnection, bpmOrderInfoList, utilProperties.TransactionModel, bpmOrderList);
            //        }
            //        #endregion

            //        #region Push Data
            //        UtilityHelper.PushOrderToPercona(perconaConnection, bpmOrderList, utilProperties.OrderModel, utilProperties.OrderProductModel,
            //            utilProperties.TransactionModel);
            //        #endregion
            //    }
            //    catch(Exception ex)
            //    {
            //        LogInfo(ex);
            //    }
            //}
            #endregion
        }

        private async void buttonFromMyToMs_Click(object sender, EventArgs e)
        {
            #region Properties
            //if (!GetProperties())
            //{
            //    return;
            //}
            //GetEntityChecked();
            //if (!(utilProperties.PushOrder || utilProperties.PushOrderProduct || utilProperties.PushTransaction))
            //{
            //    MessageBox.Show("Check Entity!");
            //    return;
            //}

            //if (perconaPurchaseInfoList.Count < 1)
            //{
            //    MessageBox.Show("Bpm diff collection is empty!", "Error", 0);
            //    LogInfo("Bpm diff is empty");
            //    return;
            //}
            //if (perconaPurchaseInfoList.Count > 1000)
            //{
            //    DialogResult result = MessageBox.Show("Bpm diff collection count > 1000. Continue?", "Error", MessageBoxButtons.YesNo);
            //    LogInfo("Bpm diff collection count > 1000");
            //    if (result == DialogResult.No)
            //    {
            //        LogInfo("Canseled");
            //        return;
            //    }
            //    else
            //    {
            //        LogInfo("Continued");
            //    }
            //}

            //string bpmConnection = String.Format(ModelReader.bpmConnectionStringFormat, utilProperties.MsServer, utilProperties.MsDbName,
            //    utilProperties.MsLogin, utilProperties.MsPassword);
            //string perconaConnection = String.Format(ModelReader.perconaConnectionStringFormat, utilProperties.MyServer, utilProperties.MyDbName,
            //    utilProperties.MyLogin, utilProperties.MyPassword);
            #endregion
            var utilProperties = utilPropertiesUpdate();
            string bpmConnection = String.Format(ModelReader.bpmConnectionStringFormat, utilProperties.MsServer, utilProperties.MsDbName,
                    utilProperties.MsLogin, utilProperties.MsPassword);
            string perconaConnection = String.Format(ModelReader.perconaConnectionStringFormat, utilProperties.MyServer, utilProperties.MyDbName,
                utilProperties.MyLogin, utilProperties.MyPassword);

            startDate = Convert.ToDateTime(textBoxDateFrom.Text);
            dueDate = Convert.ToDateTime(textBoxDateTo.Text);

            if (jsonEntities.Count == 0)
            {
                LogInfo("Configuration file is empty or is not checked!");
                return;
            }
            int count = operations.ProgressBarDataFromMySql(perconaConnection, jsonEntities, startDate, dueDate);
            label4.Text = count.ToString();
            progressBar1.Maximum = count;
            if (count == 0)
            {
                LogInfo("----- Data is not available (STOP)");
                return;
            }
            try
            {
                for (int i = 0; i < jsonEntities.Count; i++)
                {
                    List<string> insertData = new List<string>();
                    int offset = 0, limit = 100;
                    do
                    {
                        insertData = await Task.Run(() => operations.GetMySqlData(perconaConnection, jsonEntities[i], startDate, dueDate, offset, limit));
                        await Task.Run(() => operations.PushMySqlToMsSql(bpmConnection, jsonEntities[i], insertData));
                        offset += limit;
                        progressBar1.PerformStep();
                    }
                    while (insertData.Count == limit);
                }
                progressBar1.Value = 0;
                label4.Text = "0";
                MessageBox.Show("----- Process is complete -----");
            }
            catch (Exception ex)
            {
                LogInfo(ex);
            }
            #region Comments
            //if (utilProperties.PushOrder)
            //{
            //    if (!(utilProperties.PushOrderProduct || utilProperties.PushTransaction))
            //    {
            //        DialogResult result = MessageBox.Show("Are u sure about push Orders with out Products and Transactions?");
            //        if (result == DialogResult.No)
            //        {
            //            LogInfo("Canseled");
            //            return;
            //        }
            //    }
            //    try
            //    {
            //        #region Read Data
            //        perconaOrderList = UtilityHelper.GetPurchaseList(perconaConnection, perconaPurchaseInfoList, utilProperties.OrderModel);
            //        if (utilProperties.PushOrderProduct)
            //        {
            //            UtilityHelper.GetProductInPurchaseList(perconaConnection, perconaPurchaseInfoList, utilProperties.OrderProductModel, perconaOrderList);
            //        }
            //        if (utilProperties.PushTransaction)
            //        {
            //            UtilityHelper.GetMyTransactionsList(perconaConnection, perconaPurchaseInfoList, utilProperties.TransactionModel, perconaOrderList);
            //        }
            //        #endregion

            //        #region Push Data
            //        UtilityHelper.PushOrderToMsSQL(bpmConnection, perconaOrderList, utilProperties.OrderModel, utilProperties.OrderProductModel,
            //            utilProperties.TransactionModel);
            //        #endregion
            //    }
            //    catch (Exception ex)
            //    {
            //        LogInfo(ex);
            //    }
            //}
            #endregion
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            LogInfo(SqlBuilder.BuildInsertSql());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //----- Checked files with .json format
            openFileDialog1.Filter = "json files (*.json)|*.json|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxDllAddress.Text = openFileDialog1.FileName;
                ModelReader.importConfig = textBoxDllAddress.Text;
                try
                {
                    //----- Reading config.JSON
                    jsonEntities = operations.LoadJson(ModelReader.importConfig);
                } catch (Exception ex)
                {
                    MessageBox.Show("Incorrect file directory!");
                    throw ex;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LogInfo("This function in progress of developing");
        }

        private void inputMsName_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelDateFrom_Click(object sender, EventArgs e)
        {

        }
    }
}
