using System.Drawing;

namespace PMBUSQueryTool
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Debug = new System.Windows.Forms.TextBox();
            this.Query = new System.Windows.Forms.Button();
            this.txextBox_Address = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_interval = new System.Windows.Forms.TextBox();
            this.Poll = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.checkBox1_all = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.35714F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.64286F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 850F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Debug, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.Query, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.txextBox_Address, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBox_interval, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Poll, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.button2, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkedListBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.checkBox1_all, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 10);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.112149F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.88785F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1180, 457);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 381);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 30);
            this.label1.TabIndex = 6;
            this.label1.Text = "Device Address:";
            // 
            // textBox_Debug
            // 
            this.textBox_Debug.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Debug.BackColor = System.Drawing.Color.Black;
            this.textBox_Debug.ForeColor = System.Drawing.Color.Lime;
            this.textBox_Debug.Location = new System.Drawing.Point(333, 3);
            this.textBox_Debug.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox_Debug.Name = "textBox_Debug";
            this.textBox_Debug.Size = new System.Drawing.Size(843, 25);
            this.textBox_Debug.TabIndex = 5;
            // 
            // Query
            // 
            this.Query.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Query.Location = new System.Drawing.Point(237, 380);
            this.Query.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(89, 33);
            this.Query.TabIndex = 1;
            this.Query.Text = "Query";
            this.Query.UseVisualStyleBackColor = true;
            this.Query.Click += new System.EventHandler(this.Query_Click_1);
            // 
            // txextBox_Address
            // 
            this.txextBox_Address.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txextBox_Address.Location = new System.Drawing.Point(72, 384);
            this.txextBox_Address.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txextBox_Address.Name = "txextBox_Address";
            this.txextBox_Address.Size = new System.Drawing.Size(152, 25);
            this.txextBox_Address.TabIndex = 3;
            this.txextBox_Address.Text = "B4";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 430);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Interval";
            // 
            // textBox_interval
            // 
            this.textBox_interval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_interval.ForeColor = System.Drawing.Color.Black;
            this.textBox_interval.Location = new System.Drawing.Point(72, 425);
            this.textBox_interval.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_interval.Name = "textBox_interval";
            this.textBox_interval.Size = new System.Drawing.Size(152, 25);
            this.textBox_interval.TabIndex = 8;
            this.textBox_interval.Text = "1";
            // 
            // Poll
            // 
            this.Poll.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Poll.Location = new System.Drawing.Point(237, 421);
            this.Poll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Poll.Name = "Poll";
            this.Poll.Size = new System.Drawing.Size(89, 33);
            this.Poll.TabIndex = 9;
            this.Poll.Text = "Poll";
            this.Poll.UseVisualStyleBackColor = true;
            this.Poll.Click += new System.EventHandler(this.polling_button_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button2.Location = new System.Drawing.Point(1088, 421);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 33);
            this.button2.TabIndex = 10;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkedListBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.checkedListBox1, 3);
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(3, 36);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(319, 304);
            this.checkedListBox1.TabIndex = 0;
            // 
            // checkBox1_all
            // 
            this.checkBox1_all.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1_all.AutoSize = true;
            this.checkBox1_all.Location = new System.Drawing.Point(7, 13);
            this.checkBox1_all.Margin = new System.Windows.Forms.Padding(7, 2, 3, 2);
            this.checkBox1_all.Name = "checkBox1_all";
            this.checkBox1_all.Size = new System.Drawing.Size(47, 19);
            this.checkBox1_all.TabIndex = 11;
            this.checkBox1_all.Text = "All";
            this.checkBox1_all.UseVisualStyleBackColor = true;
            this.checkBox1_all.CheckedChanged += new System.EventHandler(this.checkBox1_all_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(332, 36);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 31;
            this.dataGridView1.Size = new System.Drawing.Size(845, 318);
            this.dataGridView1.TabIndex = 4;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 478);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        public System.Windows.Forms.Button Query;
        public System.Windows.Forms.TextBox txextBox_Address;
        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.TextBox textBox_Debug;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBox_interval;
        public System.Windows.Forms.Button Poll;
        public System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox1_all;
    }
}