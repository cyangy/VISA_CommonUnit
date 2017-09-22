namespace VISA_CommonUnit
{
    partial class SelectDeviceForm
    {

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AvilableDeviceList_listBox = new System.Windows.Forms.ListBox();
            this.AvilableDeviceList_listBox_label = new System.Windows.Forms.Label();
            this.SelectedDevice_textBox = new System.Windows.Forms.TextBox();
            this.SelectedDevice_label = new System.Windows.Forms.Label();
            this.SelectedDevice_OK_button = new System.Windows.Forms.Button();
            this.SelectedDevice_Cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AvilableDeviceList_listBox
            // 
            this.AvilableDeviceList_listBox.FormattingEnabled = true;
            this.AvilableDeviceList_listBox.HorizontalScrollbar = true;
            this.AvilableDeviceList_listBox.ItemHeight = 15;
            this.AvilableDeviceList_listBox.Location = new System.Drawing.Point(12, 25);
            this.AvilableDeviceList_listBox.Name = "AvilableDeviceList_listBox";
            this.AvilableDeviceList_listBox.Size = new System.Drawing.Size(294, 124);
            this.AvilableDeviceList_listBox.Sorted = true;
            this.AvilableDeviceList_listBox.TabIndex = 0;
            this.AvilableDeviceList_listBox.SelectedIndexChanged += new System.EventHandler(this.AvilableDeviceList_listBox_SelectedIndexChanged);
            this.AvilableDeviceList_listBox.DoubleClick += new System.EventHandler(this.AvilableDeviceList_listBox_DoubleClick);
            // 
            // AvilableDeviceList_listBox_label
            // 
            this.AvilableDeviceList_listBox_label.AutoSize = true;
            this.AvilableDeviceList_listBox_label.Location = new System.Drawing.Point(12, 9);
            this.AvilableDeviceList_listBox_label.Name = "AvilableDeviceList_listBox_label";
            this.AvilableDeviceList_listBox_label.Size = new System.Drawing.Size(67, 15);
            this.AvilableDeviceList_listBox_label.TabIndex = 1;
            this.AvilableDeviceList_listBox_label.Text = "设备列表";
            // 
            // SelectedDevice_textBox
            // 
            this.SelectedDevice_textBox.Location = new System.Drawing.Point(12, 192);
            this.SelectedDevice_textBox.Name = "SelectedDevice_textBox";
            this.SelectedDevice_textBox.Size = new System.Drawing.Size(294, 25);
            this.SelectedDevice_textBox.TabIndex = 2;
            // 
            // SelectedDevice_label
            // 
            this.SelectedDevice_label.AutoSize = true;
            this.SelectedDevice_label.Location = new System.Drawing.Point(20, 174);
            this.SelectedDevice_label.Name = "SelectedDevice_label";
            this.SelectedDevice_label.Size = new System.Drawing.Size(67, 15);
            this.SelectedDevice_label.TabIndex = 3;
            this.SelectedDevice_label.Text = "当前设备";
            // 
            // SelectedDevice_OK_button
            // 
            this.SelectedDevice_OK_button.Location = new System.Drawing.Point(39, 238);
            this.SelectedDevice_OK_button.Name = "SelectedDevice_OK_button";
            this.SelectedDevice_OK_button.Size = new System.Drawing.Size(89, 37);
            this.SelectedDevice_OK_button.TabIndex = 4;
            this.SelectedDevice_OK_button.Text = "确认";
            this.SelectedDevice_OK_button.UseVisualStyleBackColor = true;
            this.SelectedDevice_OK_button.Click += new System.EventHandler(this.SelectedDevice_OK_button_Click);
            // 
            // SelectedDevice_Cancel_button
            // 
            this.SelectedDevice_Cancel_button.Location = new System.Drawing.Point(178, 239);
            this.SelectedDevice_Cancel_button.Name = "SelectedDevice_Cancel_button";
            this.SelectedDevice_Cancel_button.Size = new System.Drawing.Size(99, 36);
            this.SelectedDevice_Cancel_button.TabIndex = 5;
            this.SelectedDevice_Cancel_button.Text = "取消";
            this.SelectedDevice_Cancel_button.UseVisualStyleBackColor = true;
            this.SelectedDevice_Cancel_button.Click += new System.EventHandler(this.SelectedDevice_Cancel_button_Click);
            // 
            // SelectDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 281);
            this.Controls.Add(this.SelectedDevice_Cancel_button);
            this.Controls.Add(this.SelectedDevice_OK_button);
            this.Controls.Add(this.SelectedDevice_label);
            this.Controls.Add(this.SelectedDevice_textBox);
            this.Controls.Add(this.AvilableDeviceList_listBox_label);
            this.Controls.Add(this.AvilableDeviceList_listBox);
            this.KeyPreview = true;
            this.Name = "SelectDevice";
            this.Text = "选择设备";
            this.Load += new System.EventHandler(this.SelectDevice_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectDevice_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox AvilableDeviceList_listBox;
        private System.Windows.Forms.Label AvilableDeviceList_listBox_label;
        private System.Windows.Forms.TextBox SelectedDevice_textBox;
        private System.Windows.Forms.Label SelectedDevice_label;
        private System.Windows.Forms.Button SelectedDevice_OK_button;
        private System.Windows.Forms.Button SelectedDevice_Cancel_button;
    }
}