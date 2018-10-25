namespace LostOrderUtils
{
    partial class Percona2BpmOrdersSync
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Percona2BpmOrdersSync));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button3 = new System.Windows.Forms.Button();
            this.textBoxDllAddress = new System.Windows.Forms.TextBox();
            this.logInfoDataGrid = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Info = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.inputMyPassword = new System.Windows.Forms.TextBox();
            this.inputMyLogin = new System.Windows.Forms.TextBox();
            this.inputMyServer = new System.Windows.Forms.TextBox();
            this.inputMyName = new System.Windows.Forms.TextBox();
            this.labelMyDBName = new System.Windows.Forms.Label();
            this.labelMyServer = new System.Windows.Forms.Label();
            this.inputMsPassword = new System.Windows.Forms.TextBox();
            this.inputMsLogin = new System.Windows.Forms.TextBox();
            this.textBoxDateTo = new System.Windows.Forms.TextBox();
            this.textBoxDateFrom = new System.Windows.Forms.TextBox();
            this.labelDateTo = new System.Windows.Forms.Label();
            this.labelDateFrom = new System.Windows.Forms.Label();
            this.inputMsServer = new System.Windows.Forms.TextBox();
            this.inputMsName = new System.Windows.Forms.TextBox();
            this.labelMsDBName = new System.Windows.Forms.Label();
            this.labelMsSQLServer = new System.Windows.Forms.Label();
            this.getMsSQLInfo = new System.Windows.Forms.Button();
            this.buttonFromMyToMs = new System.Windows.Forms.Button();
            this.TableCaption = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttomFromMsToMy = new System.Windows.Forms.Button();
            this.getPerconaInfo = new System.Windows.Forms.Button();
            this.dataDiff = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logInfoDataGrid)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataDiff)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.textBoxDllAddress);
            this.groupBox1.Controls.Add(this.logInfoDataGrid);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.inputMyPassword);
            this.groupBox1.Controls.Add(this.inputMyLogin);
            this.groupBox1.Controls.Add(this.inputMyServer);
            this.groupBox1.Controls.Add(this.inputMyName);
            this.groupBox1.Controls.Add(this.labelMyDBName);
            this.groupBox1.Controls.Add(this.labelMyServer);
            this.groupBox1.Controls.Add(this.inputMsPassword);
            this.groupBox1.Controls.Add(this.inputMsLogin);
            this.groupBox1.Controls.Add(this.textBoxDateTo);
            this.groupBox1.Controls.Add(this.textBoxDateFrom);
            this.groupBox1.Controls.Add(this.labelDateTo);
            this.groupBox1.Controls.Add(this.labelDateFrom);
            this.groupBox1.Controls.Add(this.inputMsServer);
            this.groupBox1.Controls.Add(this.inputMsName);
            this.groupBox1.Controls.Add(this.labelMsDBName);
            this.groupBox1.Controls.Add(this.labelMsSQLServer);
            this.groupBox1.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(669, 387);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Properties";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 200);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(645, 23);
            this.progressBar1.Step = 100;
            this.progressBar1.TabIndex = 3;
            // 
            // button3
            // 
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.Enabled = false;
            this.button3.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(546, 159);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(102, 25);
            this.button3.TabIndex = 19;
            this.button3.Text = "Get entities";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBoxDllAddress
            // 
            this.textBoxDllAddress.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDllAddress.Location = new System.Drawing.Point(6, 159);
            this.textBoxDllAddress.Name = "textBoxDllAddress";
            this.textBoxDllAddress.ReadOnly = true;
            this.textBoxDllAddress.Size = new System.Drawing.Size(417, 25);
            this.textBoxDllAddress.TabIndex = 18;
            this.textBoxDllAddress.Text = "Address of configuration";
            // 
            // logInfoDataGrid
            // 
            this.logInfoDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logInfoDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Info});
            this.logInfoDataGrid.Location = new System.Drawing.Point(3, 240);
            this.logInfoDataGrid.Name = "logInfoDataGrid";
            this.logInfoDataGrid.Size = new System.Drawing.Size(645, 113);
            this.logInfoDataGrid.TabIndex = 2;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 110;
            // 
            // Info
            // 
            this.Info.HeaderText = "Info";
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            this.Info.Width = 400;
            // 
            // button2
            // 
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button2.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(429, 159);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 25);
            this.button2.TabIndex = 17;
            this.button2.Text = "Get file";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // inputMyPassword
            // 
            this.inputMyPassword.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputMyPassword.ForeColor = System.Drawing.SystemColors.WindowText;
            this.inputMyPassword.Location = new System.Drawing.Point(465, 92);
            this.inputMyPassword.Name = "inputMyPassword";
            this.inputMyPassword.Size = new System.Drawing.Size(183, 25);
            this.inputMyPassword.TabIndex = 15;
            this.inputMyPassword.Text = "3250877";
            // 
            // inputMyLogin
            // 
            this.inputMyLogin.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputMyLogin.Location = new System.Drawing.Point(349, 92);
            this.inputMyLogin.Name = "inputMyLogin";
            this.inputMyLogin.Size = new System.Drawing.Size(110, 25);
            this.inputMyLogin.TabIndex = 14;
            this.inputMyLogin.Text = "cns";
            // 
            // inputMyServer
            // 
            this.inputMyServer.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputMyServer.Location = new System.Drawing.Point(465, 32);
            this.inputMyServer.Name = "inputMyServer";
            this.inputMyServer.Size = new System.Drawing.Size(183, 25);
            this.inputMyServer.TabIndex = 13;
            this.inputMyServer.Text = "192.168.0.202";
            // 
            // inputMyName
            // 
            this.inputMyName.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputMyName.Location = new System.Drawing.Point(465, 62);
            this.inputMyName.Name = "inputMyName";
            this.inputMyName.Size = new System.Drawing.Size(183, 25);
            this.inputMyName.TabIndex = 12;
            this.inputMyName.Text = "LegelbakhP";
            // 
            // labelMyDBName
            // 
            this.labelMyDBName.AutoSize = true;
            this.labelMyDBName.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMyDBName.ForeColor = System.Drawing.Color.Red;
            this.labelMyDBName.Location = new System.Drawing.Point(346, 65);
            this.labelMyDBName.Name = "labelMyDBName";
            this.labelMyDBName.Size = new System.Drawing.Size(87, 18);
            this.labelMyDBName.TabIndex = 11;
            this.labelMyDBName.Text = "Percona db:";
            // 
            // labelMyServer
            // 
            this.labelMyServer.AutoSize = true;
            this.labelMyServer.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMyServer.ForeColor = System.Drawing.Color.Red;
            this.labelMyServer.Location = new System.Drawing.Point(346, 35);
            this.labelMyServer.Name = "labelMyServer";
            this.labelMyServer.Size = new System.Drawing.Size(113, 18);
            this.labelMyServer.TabIndex = 10;
            this.labelMyServer.Text = "Percona Server:";
            // 
            // inputMsPassword
            // 
            this.inputMsPassword.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputMsPassword.ForeColor = System.Drawing.SystemColors.WindowText;
            this.inputMsPassword.Location = new System.Drawing.Point(126, 92);
            this.inputMsPassword.Name = "inputMsPassword";
            this.inputMsPassword.Size = new System.Drawing.Size(183, 25);
            this.inputMsPassword.TabIndex = 9;
            this.inputMsPassword.Text = "Supervisor";
            // 
            // inputMsLogin
            // 
            this.inputMsLogin.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputMsLogin.Location = new System.Drawing.Point(10, 92);
            this.inputMsLogin.Name = "inputMsLogin";
            this.inputMsLogin.Size = new System.Drawing.Size(110, 25);
            this.inputMsLogin.TabIndex = 8;
            this.inputMsLogin.Text = "Supervisor";
            // 
            // textBoxDateTo
            // 
            this.textBoxDateTo.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDateTo.Location = new System.Drawing.Point(349, 128);
            this.textBoxDateTo.Name = "textBoxDateTo";
            this.textBoxDateTo.Size = new System.Drawing.Size(93, 25);
            this.textBoxDateTo.TabIndex = 7;
            this.textBoxDateTo.Text = "2018-10-30";
            // 
            // textBoxDateFrom
            // 
            this.textBoxDateFrom.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDateFrom.Location = new System.Drawing.Point(216, 128);
            this.textBoxDateFrom.Name = "textBoxDateFrom";
            this.textBoxDateFrom.Size = new System.Drawing.Size(93, 25);
            this.textBoxDateFrom.TabIndex = 6;
            this.textBoxDateFrom.Text = "2018-10-20";
            // 
            // labelDateTo
            // 
            this.labelDateTo.AutoSize = true;
            this.labelDateTo.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateTo.Location = new System.Drawing.Point(322, 131);
            this.labelDateTo.Name = "labelDateTo";
            this.labelDateTo.Size = new System.Drawing.Size(21, 18);
            this.labelDateTo.TabIndex = 5;
            this.labelDateTo.Text = "to";
            // 
            // labelDateFrom
            // 
            this.labelDateFrom.AutoSize = true;
            this.labelDateFrom.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateFrom.Location = new System.Drawing.Point(7, 131);
            this.labelDateFrom.Name = "labelDateFrom";
            this.labelDateFrom.Size = new System.Drawing.Size(203, 18);
            this.labelDateFrom.TabIndex = 4;
            this.labelDateFrom.Text = "Date (YYYY-MM-DD) from:";
            // 
            // inputMsServer
            // 
            this.inputMsServer.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputMsServer.Location = new System.Drawing.Point(126, 32);
            this.inputMsServer.Name = "inputMsServer";
            this.inputMsServer.Size = new System.Drawing.Size(183, 22);
            this.inputMsServer.TabIndex = 3;
            this.inputMsServer.Text = "WIN-BEAA7MI3J3O\\MSSQLSERVER2016";
            // 
            // inputMsName
            // 
            this.inputMsName.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputMsName.Location = new System.Drawing.Point(126, 62);
            this.inputMsName.Name = "inputMsName";
            this.inputMsName.Size = new System.Drawing.Size(183, 25);
            this.inputMsName.TabIndex = 2;
            this.inputMsName.Text = "LegelbakhTest7122Marketing";
            // 
            // labelMsDBName
            // 
            this.labelMsDBName.AutoSize = true;
            this.labelMsDBName.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMsDBName.ForeColor = System.Drawing.Color.Red;
            this.labelMsDBName.Location = new System.Drawing.Point(7, 65);
            this.labelMsDBName.Name = "labelMsDBName";
            this.labelMsDBName.Size = new System.Drawing.Size(82, 18);
            this.labelMsDBName.TabIndex = 1;
            this.labelMsDBName.Text = "MsSQL db:";
            // 
            // labelMsSQLServer
            // 
            this.labelMsSQLServer.AutoSize = true;
            this.labelMsSQLServer.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMsSQLServer.ForeColor = System.Drawing.Color.Red;
            this.labelMsSQLServer.Location = new System.Drawing.Point(7, 35);
            this.labelMsSQLServer.Name = "labelMsSQLServer";
            this.labelMsSQLServer.Size = new System.Drawing.Size(108, 18);
            this.labelMsSQLServer.TabIndex = 0;
            this.labelMsSQLServer.Text = "MsSQL Server:";
            // 
            // getMsSQLInfo
            // 
            this.getMsSQLInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.getMsSQLInfo.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.getMsSQLInfo.Location = new System.Drawing.Point(6, 24);
            this.getMsSQLInfo.Name = "getMsSQLInfo";
            this.getMsSQLInfo.Size = new System.Drawing.Size(224, 35);
            this.getMsSQLInfo.TabIndex = 3;
            this.getMsSQLInfo.Text = "Get MsSQL Info";
            this.getMsSQLInfo.UseVisualStyleBackColor = true;
            this.getMsSQLInfo.Click += new System.EventHandler(this.getMsSQLInfo_Click);
            // 
            // buttonFromMyToMs
            // 
            this.buttonFromMyToMs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFromMyToMs.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFromMyToMs.Location = new System.Drawing.Point(6, 147);
            this.buttonFromMyToMs.Name = "buttonFromMyToMs";
            this.buttonFromMyToMs.Size = new System.Drawing.Size(224, 35);
            this.buttonFromMyToMs.TabIndex = 6;
            this.buttonFromMyToMs.Text = "Push from Percona to MsSQL";
            this.buttonFromMyToMs.UseVisualStyleBackColor = true;
            this.buttonFromMyToMs.Click += new System.EventHandler(this.buttonFromMyToMs_Click);
            // 
            // TableCaption
            // 
            this.TableCaption.AutoSize = true;
            this.TableCaption.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TableCaption.Location = new System.Drawing.Point(2, 398);
            this.TableCaption.Name = "TableCaption";
            this.TableCaption.Size = new System.Drawing.Size(91, 18);
            this.TableCaption.TabIndex = 8;
            this.TableCaption.Text = "Orders info:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttomFromMsToMy);
            this.groupBox2.Controls.Add(this.getPerconaInfo);
            this.groupBox2.Controls.Add(this.getMsSQLInfo);
            this.groupBox2.Controls.Add(this.buttonFromMyToMs);
            this.groupBox2.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(680, 199);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 196);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control";
            // 
            // buttomFromMsToMy
            // 
            this.buttomFromMsToMy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttomFromMsToMy.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttomFromMsToMy.Location = new System.Drawing.Point(6, 106);
            this.buttomFromMsToMy.Name = "buttomFromMsToMy";
            this.buttomFromMsToMy.Size = new System.Drawing.Size(224, 35);
            this.buttomFromMsToMy.TabIndex = 8;
            this.buttomFromMsToMy.Text = "Push from MsSQL to Percona";
            this.buttomFromMsToMy.UseVisualStyleBackColor = true;
            this.buttomFromMsToMy.Click += new System.EventHandler(this.buttomFromMsToMy_Click);
            // 
            // getPerconaInfo
            // 
            this.getPerconaInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.getPerconaInfo.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.getPerconaInfo.Location = new System.Drawing.Point(6, 65);
            this.getPerconaInfo.Name = "getPerconaInfo";
            this.getPerconaInfo.Size = new System.Drawing.Size(224, 35);
            this.getPerconaInfo.TabIndex = 7;
            this.getPerconaInfo.Text = "Get Percona Info";
            this.getPerconaInfo.UseVisualStyleBackColor = true;
            this.getPerconaInfo.Click += new System.EventHandler(this.getPerconaInfo_Click);
            // 
            // dataDiff
            // 
            this.dataDiff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataDiff.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Number,
            this.TotalAmount,
            this.CardId,
            this.ProductsCount,
            this.TransactionsCount});
            this.dataDiff.Location = new System.Drawing.Point(5, 419);
            this.dataDiff.Name = "dataDiff";
            this.dataDiff.Size = new System.Drawing.Size(905, 121);
            this.dataDiff.TabIndex = 14;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 170;
            // 
            // Number
            // 
            this.Number.HeaderText = "Number";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            // 
            // TotalAmount
            // 
            this.TotalAmount.HeaderText = "TotalAmount";
            this.TotalAmount.Name = "TotalAmount";
            this.TotalAmount.ReadOnly = true;
            // 
            // CardId
            // 
            this.CardId.HeaderText = "CardId";
            this.CardId.Name = "CardId";
            this.CardId.ReadOnly = true;
            this.CardId.Width = 170;
            // 
            // ProductsCount
            // 
            this.ProductsCount.HeaderText = "Products";
            this.ProductsCount.Name = "ProductsCount";
            this.ProductsCount.ReadOnly = true;
            // 
            // TransactionsCount
            // 
            this.TransactionsCount.HeaderText = "Transactions";
            this.TransactionsCount.Name = "TransactionsCount";
            this.TransactionsCount.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(683, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 18);
            this.label1.TabIndex = 15;
            this.label1.Text = "MsSQL detected data:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(683, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 18);
            this.label2.TabIndex = 16;
            this.label2.Text = "MySQL detected data:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(842, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(842, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "0";
            // 
            // Percona2BpmOrdersSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 556);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataDiff);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.TableCaption);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Percona2BpmOrdersSync";
            this.Text = "Percona2MsSql Orders Sync";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logInfoDataGrid)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataDiff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox inputMsServer;
        private System.Windows.Forms.TextBox inputMsName;
        private System.Windows.Forms.Label labelMsDBName;
        private System.Windows.Forms.Label labelMsSQLServer;
        private System.Windows.Forms.DataGridView logInfoDataGrid;
        private System.Windows.Forms.Button getMsSQLInfo;
        private System.Windows.Forms.Label labelDateFrom;
        private System.Windows.Forms.TextBox textBoxDateTo;
        private System.Windows.Forms.TextBox textBoxDateFrom;
        private System.Windows.Forms.Label labelDateTo;
        private System.Windows.Forms.Button buttonFromMyToMs;
        private System.Windows.Forms.Label TableCaption;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox inputMsPassword;
        private System.Windows.Forms.TextBox inputMsLogin;
        private System.Windows.Forms.TextBox inputMyPassword;
        private System.Windows.Forms.TextBox inputMyLogin;
        private System.Windows.Forms.TextBox inputMyServer;
        private System.Windows.Forms.TextBox inputMyName;
        private System.Windows.Forms.Label labelMyDBName;
        private System.Windows.Forms.Label labelMyServer;
        private System.Windows.Forms.Button getPerconaInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Info;
        private System.Windows.Forms.Button buttomFromMsToMy;
        private System.Windows.Forms.DataGridView dataDiff;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductsCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionsCount;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxDllAddress;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

