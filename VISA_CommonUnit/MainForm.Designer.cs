namespace VISA_CommonUnit
{
    partial class VISA_CommonUnit
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VISA_CommonUnit));
            this.openSessionButton_button = new System.Windows.Forms.Button();
            this.closeSessionButton_button = new System.Windows.Forms.Button();
            this.SessionButton_panel = new System.Windows.Forms.Panel();
            this.Session_CurrentSelectDevice_Label = new System.Windows.Forms.Label();
            this.Session_CurrentSelectDevice_textBox = new System.Windows.Forms.TextBox();
            this.SessionButton_label = new System.Windows.Forms.Label();
            this.commandManipulate_Write_button = new System.Windows.Forms.Button();
            this.commandManipulate_Write_isCycle_checkBox = new System.Windows.Forms.CheckBox();
            this.commandManipulate_Write_isBiodirection_checkBox = new System.Windows.Forms.CheckBox();
            this.commandManipulate_Read_button = new System.Windows.Forms.Button();
            this.commandManipulate_Response_richTextBox = new System.Windows.Forms.RichTextBox();
            this.commandManipulate_Write_isCycle_Internal_textBox = new System.Windows.Forms.TextBox();
            this.commandManipulate_Write_isCycle_Internal_label = new System.Windows.Forms.Label();
            this.commandManipulate_Response_Format_label = new System.Windows.Forms.Label();
            this.commandManipulate_Response_Format_Substring_StartIndex_textBox = new System.Windows.Forms.TextBox();
            this.commandManipulate_Response_Format_Substring_EndIndex_textBox = new System.Windows.Forms.TextBox();
            this.commandManipulate_Response_Format_Substring_checkBox = new System.Windows.Forms.CheckBox();
            this.commandManipulate_Response_Format_RegexExpression_checkBox = new System.Windows.Forms.CheckBox();
            this.SaveTheResponseToFile_checkBox = new System.Windows.Forms.CheckBox();
            this.SaveTheResponseToFile_Path_textBox = new System.Windows.Forms.TextBox();
            this.SaveTheResponseToFile_Path_BrowserButton = new System.Windows.Forms.Button();
            this.InstrumentsCommand_Set_comboBox = new System.Windows.Forms.ComboBox();
            this.InstrumentsCommandSet_label = new System.Windows.Forms.Label();
            this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox = new System.Windows.Forms.CheckBox();
            this.commandManipulate_Write_CommandStr_label = new System.Windows.Forms.Label();
            this.commandManipulate_Response_Format_Substring_label_ToChar = new System.Windows.Forms.Label();
            this.commandManipulate_WriteAndRead_button = new System.Windows.Forms.Button();
            this.commandManipulate_Write_CommandStr_comboBox = new System.Windows.Forms.ComboBox();
            this.Debug_textBox = new System.Windows.Forms.TextBox();
            this.Debug_label = new System.Windows.Forms.Label();
            this.commandManipulate_Response_Format_Substring_panel = new System.Windows.Forms.Panel();
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox = new System.Windows.Forms.ComboBox();
            this.commandManipulate_Response_Format_RegexExpression_comboBox = new System.Windows.Forms.ComboBox();
            this.commandManipulate_panel = new System.Windows.Forms.Panel();
            this.MeasureCountLimit_label = new System.Windows.Forms.Label();
            this.MeasureCountLimit_textBox = new System.Windows.Forms.TextBox();
            this.commandManipulate_Response_richTextBox_label2 = new System.Windows.Forms.Label();
            this.commandManipulate_Response_richTextBox_Clean_button = new System.Windows.Forms.Button();
            this.commandManipulate_Response_richTextBox_panel = new System.Windows.Forms.Panel();
            this.SessionButton_panel.SuspendLayout();
            this.commandManipulate_Response_Format_Substring_panel.SuspendLayout();
            this.commandManipulate_panel.SuspendLayout();
            this.commandManipulate_Response_richTextBox_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // openSessionButton_button
            // 
            this.openSessionButton_button.Location = new System.Drawing.Point(7, 5);
            this.openSessionButton_button.Name = "openSessionButton_button";
            this.openSessionButton_button.Size = new System.Drawing.Size(128, 47);
            this.openSessionButton_button.TabIndex = 0;
            this.openSessionButton_button.Text = "开启会话";
            this.openSessionButton_button.UseVisualStyleBackColor = true;
            this.openSessionButton_button.Click += new System.EventHandler(this.openSessionButton_button_Click);
            // 
            // closeSessionButton_button
            // 
            this.closeSessionButton_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeSessionButton_button.Location = new System.Drawing.Point(529, 5);
            this.closeSessionButton_button.Name = "closeSessionButton_button";
            this.closeSessionButton_button.Size = new System.Drawing.Size(121, 47);
            this.closeSessionButton_button.TabIndex = 1;
            this.closeSessionButton_button.Text = "关闭会话";
            this.closeSessionButton_button.UseVisualStyleBackColor = true;
            this.closeSessionButton_button.Click += new System.EventHandler(this.closeSessionButton_button_Click);
            // 
            // SessionButton_panel
            // 
            this.SessionButton_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SessionButton_panel.BackColor = System.Drawing.SystemColors.Highlight;
            this.SessionButton_panel.Controls.Add(this.Session_CurrentSelectDevice_Label);
            this.SessionButton_panel.Controls.Add(this.Session_CurrentSelectDevice_textBox);
            this.SessionButton_panel.Controls.Add(this.closeSessionButton_button);
            this.SessionButton_panel.Controls.Add(this.openSessionButton_button);
            this.SessionButton_panel.Location = new System.Drawing.Point(172, 32);
            this.SessionButton_panel.Name = "SessionButton_panel";
            this.SessionButton_panel.Size = new System.Drawing.Size(653, 58);
            this.SessionButton_panel.TabIndex = 2;
            // 
            // Session_CurrentSelectDevice_Label
            // 
            this.Session_CurrentSelectDevice_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Session_CurrentSelectDevice_Label.AutoSize = true;
            this.Session_CurrentSelectDevice_Label.Location = new System.Drawing.Point(218, 0);
            this.Session_CurrentSelectDevice_Label.Name = "Session_CurrentSelectDevice_Label";
            this.Session_CurrentSelectDevice_Label.Size = new System.Drawing.Size(67, 15);
            this.Session_CurrentSelectDevice_Label.TabIndex = 3;
            this.Session_CurrentSelectDevice_Label.Text = "当前设备";
            this.Session_CurrentSelectDevice_Label.Visible = false;
            // 
            // Session_CurrentSelectDevice_textBox
            // 
            this.Session_CurrentSelectDevice_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Session_CurrentSelectDevice_textBox.Location = new System.Drawing.Point(171, 18);
            this.Session_CurrentSelectDevice_textBox.Name = "Session_CurrentSelectDevice_textBox";
            this.Session_CurrentSelectDevice_textBox.ReadOnly = true;
            this.Session_CurrentSelectDevice_textBox.Size = new System.Drawing.Size(324, 25);
            this.Session_CurrentSelectDevice_textBox.TabIndex = 2;
            this.Session_CurrentSelectDevice_textBox.Text = "请先选择设备";
            this.Session_CurrentSelectDevice_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Session_CurrentSelectDevice_textBox.TextChanged += new System.EventHandler(this.Session_CurrentSelectDevice_textBox_TextChanged);
            // 
            // SessionButton_label
            // 
            this.SessionButton_label.AutoSize = true;
            this.SessionButton_label.BackColor = System.Drawing.SystemColors.Highlight;
            this.SessionButton_label.Font = new System.Drawing.Font("宋体", 12F);
            this.SessionButton_label.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SessionButton_label.Location = new System.Drawing.Point(172, 14);
            this.SessionButton_label.Name = "SessionButton_label";
            this.SessionButton_label.Size = new System.Drawing.Size(89, 20);
            this.SessionButton_label.TabIndex = 3;
            this.SessionButton_label.Text = "会话管理";
            // 
            // commandManipulate_Write_button
            // 
            this.commandManipulate_Write_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManipulate_Write_button.Location = new System.Drawing.Point(842, 42);
            this.commandManipulate_Write_button.Name = "commandManipulate_Write_button";
            this.commandManipulate_Write_button.Size = new System.Drawing.Size(158, 28);
            this.commandManipulate_Write_button.TabIndex = 5;
            this.commandManipulate_Write_button.Text = "发送命令";
            this.commandManipulate_Write_button.UseVisualStyleBackColor = true;
            this.commandManipulate_Write_button.Click += new System.EventHandler(this.commandManipulate_Write_button_Click);
            // 
            // commandManipulate_Write_isCycle_checkBox
            // 
            this.commandManipulate_Write_isCycle_checkBox.AutoSize = true;
            this.commandManipulate_Write_isCycle_checkBox.Location = new System.Drawing.Point(199, 85);
            this.commandManipulate_Write_isCycle_checkBox.Name = "commandManipulate_Write_isCycle_checkBox";
            this.commandManipulate_Write_isCycle_checkBox.Size = new System.Drawing.Size(59, 19);
            this.commandManipulate_Write_isCycle_checkBox.TabIndex = 6;
            this.commandManipulate_Write_isCycle_checkBox.Text = "循环";
            this.commandManipulate_Write_isCycle_checkBox.UseVisualStyleBackColor = true;
            this.commandManipulate_Write_isCycle_checkBox.CheckedChanged += new System.EventHandler(this.commandManipulate_Write_isCycle_checkBox_CheckedChanged);
            // 
            // commandManipulate_Write_isBiodirection_checkBox
            // 
            this.commandManipulate_Write_isBiodirection_checkBox.AutoSize = true;
            this.commandManipulate_Write_isBiodirection_checkBox.Location = new System.Drawing.Point(104, 85);
            this.commandManipulate_Write_isBiodirection_checkBox.Name = "commandManipulate_Write_isBiodirection_checkBox";
            this.commandManipulate_Write_isBiodirection_checkBox.Size = new System.Drawing.Size(89, 19);
            this.commandManipulate_Write_isBiodirection_checkBox.TabIndex = 7;
            this.commandManipulate_Write_isBiodirection_checkBox.Text = "双向命令";
            this.commandManipulate_Write_isBiodirection_checkBox.UseVisualStyleBackColor = true;
            this.commandManipulate_Write_isBiodirection_checkBox.CheckedChanged += new System.EventHandler(this.commandManipulate_Write_isBiodirection_checkBox_CheckedChanged);
            // 
            // commandManipulate_Read_button
            // 
            this.commandManipulate_Read_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManipulate_Read_button.Location = new System.Drawing.Point(842, 76);
            this.commandManipulate_Read_button.Name = "commandManipulate_Read_button";
            this.commandManipulate_Read_button.Size = new System.Drawing.Size(158, 28);
            this.commandManipulate_Read_button.TabIndex = 8;
            this.commandManipulate_Read_button.Text = "读取";
            this.commandManipulate_Read_button.UseVisualStyleBackColor = true;
            this.commandManipulate_Read_button.Click += new System.EventHandler(this.commandManipulate_Read_button_Click);
            // 
            // commandManipulate_Response_richTextBox
            // 
            this.commandManipulate_Response_richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManipulate_Response_richTextBox.BackColor = System.Drawing.Color.PeachPuff;
            this.commandManipulate_Response_richTextBox.Location = new System.Drawing.Point(6, 40);
            this.commandManipulate_Response_richTextBox.Name = "commandManipulate_Response_richTextBox";
            this.commandManipulate_Response_richTextBox.Size = new System.Drawing.Size(989, 219);
            this.commandManipulate_Response_richTextBox.TabIndex = 10;
            this.commandManipulate_Response_richTextBox.Text = "";
            this.commandManipulate_Response_richTextBox.TextChanged += new System.EventHandler(this.commandManipulate_Response_richTextBox_TextChanged);
            this.commandManipulate_Response_richTextBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.commandManipulate_Response_richTextBox_MouseUp);
            // 
            // commandManipulate_Write_isCycle_Internal_textBox
            // 
            this.commandManipulate_Write_isCycle_Internal_textBox.Location = new System.Drawing.Point(264, 80);
            this.commandManipulate_Write_isCycle_Internal_textBox.Name = "commandManipulate_Write_isCycle_Internal_textBox";
            this.commandManipulate_Write_isCycle_Internal_textBox.Size = new System.Drawing.Size(121, 25);
            this.commandManipulate_Write_isCycle_Internal_textBox.TabIndex = 11;
            this.commandManipulate_Write_isCycle_Internal_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.commandManipulate_Write_isCycle_Internal_textBox_KeyPress);
            this.commandManipulate_Write_isCycle_Internal_textBox.Leave += new System.EventHandler(this.commandManipulate_Write_isCycle_Internal_textBox_Leave);
            // 
            // commandManipulate_Write_isCycle_Internal_label
            // 
            this.commandManipulate_Write_isCycle_Internal_label.AutoSize = true;
            this.commandManipulate_Write_isCycle_Internal_label.Location = new System.Drawing.Point(391, 86);
            this.commandManipulate_Write_isCycle_Internal_label.Name = "commandManipulate_Write_isCycle_Internal_label";
            this.commandManipulate_Write_isCycle_Internal_label.Size = new System.Drawing.Size(99, 15);
            this.commandManipulate_Write_isCycle_Internal_label.TabIndex = 12;
            this.commandManipulate_Write_isCycle_Internal_label.Text = "发送间隔(ms)";
            // 
            // commandManipulate_Response_Format_label
            // 
            this.commandManipulate_Response_Format_label.AutoSize = true;
            this.commandManipulate_Response_Format_label.BackColor = System.Drawing.Color.BurlyWood;
            this.commandManipulate_Response_Format_label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.commandManipulate_Response_Format_label.Location = new System.Drawing.Point(6, 328);
            this.commandManipulate_Response_Format_label.Name = "commandManipulate_Response_Format_label";
            this.commandManipulate_Response_Format_label.Size = new System.Drawing.Size(69, 20);
            this.commandManipulate_Response_Format_label.TabIndex = 17;
            this.commandManipulate_Response_Format_label.Text = "格式化";
            // 
            // commandManipulate_Response_Format_Substring_StartIndex_textBox
            // 
            this.commandManipulate_Response_Format_Substring_StartIndex_textBox.Location = new System.Drawing.Point(153, 16);
            this.commandManipulate_Response_Format_Substring_StartIndex_textBox.Name = "commandManipulate_Response_Format_Substring_StartIndex_textBox";
            this.commandManipulate_Response_Format_Substring_StartIndex_textBox.Size = new System.Drawing.Size(64, 25);
            this.commandManipulate_Response_Format_Substring_StartIndex_textBox.TabIndex = 19;
            this.commandManipulate_Response_Format_Substring_StartIndex_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.commandManipulate_Response_Format_Substring_StartIndex_textBox_KeyPress);
            this.commandManipulate_Response_Format_Substring_StartIndex_textBox.Leave += new System.EventHandler(this.commandManipulate_Response_Format_Substring_StartIndex_textBox_Leave);
            // 
            // commandManipulate_Response_Format_Substring_EndIndex_textBox
            // 
            this.commandManipulate_Response_Format_Substring_EndIndex_textBox.Location = new System.Drawing.Point(244, 16);
            this.commandManipulate_Response_Format_Substring_EndIndex_textBox.Name = "commandManipulate_Response_Format_Substring_EndIndex_textBox";
            this.commandManipulate_Response_Format_Substring_EndIndex_textBox.Size = new System.Drawing.Size(64, 25);
            this.commandManipulate_Response_Format_Substring_EndIndex_textBox.TabIndex = 20;
            this.commandManipulate_Response_Format_Substring_EndIndex_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.commandManipulate_Response_Format_Substring_EndIndex_textBox_KeyPress);
            this.commandManipulate_Response_Format_Substring_EndIndex_textBox.Leave += new System.EventHandler(this.commandManipulate_Response_Format_Substring_EndIndex_textBox_Leave);
            // 
            // commandManipulate_Response_Format_Substring_checkBox
            // 
            this.commandManipulate_Response_Format_Substring_checkBox.AutoSize = true;
            this.commandManipulate_Response_Format_Substring_checkBox.BackColor = System.Drawing.Color.Beige;
            this.commandManipulate_Response_Format_Substring_checkBox.Location = new System.Drawing.Point(17, 22);
            this.commandManipulate_Response_Format_Substring_checkBox.Name = "commandManipulate_Response_Format_Substring_checkBox";
            this.commandManipulate_Response_Format_Substring_checkBox.Size = new System.Drawing.Size(119, 19);
            this.commandManipulate_Response_Format_Substring_checkBox.TabIndex = 21;
            this.commandManipulate_Response_Format_Substring_checkBox.Text = "截取指定范围";
            this.commandManipulate_Response_Format_Substring_checkBox.UseVisualStyleBackColor = false;
            this.commandManipulate_Response_Format_Substring_checkBox.CheckedChanged += new System.EventHandler(this.commandManipulate_Response_Format_Substring_checkBox_CheckedChanged);
            // 
            // commandManipulate_Response_Format_RegexExpression_checkBox
            // 
            this.commandManipulate_Response_Format_RegexExpression_checkBox.AutoSize = true;
            this.commandManipulate_Response_Format_RegexExpression_checkBox.Location = new System.Drawing.Point(17, 56);
            this.commandManipulate_Response_Format_RegexExpression_checkBox.Name = "commandManipulate_Response_Format_RegexExpression_checkBox";
            this.commandManipulate_Response_Format_RegexExpression_checkBox.Size = new System.Drawing.Size(104, 19);
            this.commandManipulate_Response_Format_RegexExpression_checkBox.TabIndex = 24;
            this.commandManipulate_Response_Format_RegexExpression_checkBox.Text = "正则表达式";
            this.commandManipulate_Response_Format_RegexExpression_checkBox.UseVisualStyleBackColor = true;
            this.commandManipulate_Response_Format_RegexExpression_checkBox.CheckedChanged += new System.EventHandler(this.commandManipulate_Response_Format_RegexExpression_checkBox_CheckedChanged);
            // 
            // SaveTheResponseToFile_checkBox
            // 
            this.SaveTheResponseToFile_checkBox.AutoSize = true;
            this.SaveTheResponseToFile_checkBox.Location = new System.Drawing.Point(90, 164);
            this.SaveTheResponseToFile_checkBox.Name = "SaveTheResponseToFile_checkBox";
            this.SaveTheResponseToFile_checkBox.Size = new System.Drawing.Size(134, 19);
            this.SaveTheResponseToFile_checkBox.TabIndex = 26;
            this.SaveTheResponseToFile_checkBox.Text = "保存输出到文件";
            this.SaveTheResponseToFile_checkBox.UseVisualStyleBackColor = true;
            this.SaveTheResponseToFile_checkBox.CheckedChanged += new System.EventHandler(this.SaveTheResponseToFile_checkBox_CheckedChanged);
            // 
            // SaveTheResponseToFile_Path_textBox
            // 
            this.SaveTheResponseToFile_Path_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveTheResponseToFile_Path_textBox.Location = new System.Drawing.Point(230, 158);
            this.SaveTheResponseToFile_Path_textBox.Name = "SaveTheResponseToFile_Path_textBox";
            this.SaveTheResponseToFile_Path_textBox.Size = new System.Drawing.Size(483, 25);
            this.SaveTheResponseToFile_Path_textBox.TabIndex = 25;
            this.SaveTheResponseToFile_Path_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SaveTheResponseToFile_Path_textBox_KeyPress);
            this.SaveTheResponseToFile_Path_textBox.Leave += new System.EventHandler(this.SaveTheResponseToFile_Path_textBox_Leave);
            // 
            // SaveTheResponseToFile_Path_BrowserButton
            // 
            this.SaveTheResponseToFile_Path_BrowserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveTheResponseToFile_Path_BrowserButton.Location = new System.Drawing.Point(719, 155);
            this.SaveTheResponseToFile_Path_BrowserButton.Name = "SaveTheResponseToFile_Path_BrowserButton";
            this.SaveTheResponseToFile_Path_BrowserButton.Size = new System.Drawing.Size(57, 28);
            this.SaveTheResponseToFile_Path_BrowserButton.TabIndex = 27;
            this.SaveTheResponseToFile_Path_BrowserButton.Text = "浏览";
            this.SaveTheResponseToFile_Path_BrowserButton.UseVisualStyleBackColor = true;
            this.SaveTheResponseToFile_Path_BrowserButton.Click += new System.EventHandler(this.SaveTheResponseToFile_Path_BrowserButton_Click);
            // 
            // InstrumentsCommand_Set_comboBox
            // 
            this.InstrumentsCommand_Set_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InstrumentsCommand_Set_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InstrumentsCommand_Set_comboBox.FormattingEnabled = true;
            this.InstrumentsCommand_Set_comboBox.Location = new System.Drawing.Point(832, 5);
            this.InstrumentsCommand_Set_comboBox.Name = "InstrumentsCommand_Set_comboBox";
            this.InstrumentsCommand_Set_comboBox.Size = new System.Drawing.Size(163, 23);
            this.InstrumentsCommand_Set_comboBox.TabIndex = 28;
            this.InstrumentsCommand_Set_comboBox.SelectedIndexChanged += new System.EventHandler(this.InstrumentsCommand_Set_comboBox_SelectedIndexChanged);
            // 
            // InstrumentsCommandSet_label
            // 
            this.InstrumentsCommandSet_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InstrumentsCommandSet_label.AutoSize = true;
            this.InstrumentsCommandSet_label.Location = new System.Drawing.Point(744, 8);
            this.InstrumentsCommandSet_label.Name = "InstrumentsCommandSet_label";
            this.InstrumentsCommandSet_label.Size = new System.Drawing.Size(82, 15);
            this.InstrumentsCommandSet_label.TabIndex = 29;
            this.InstrumentsCommandSet_label.Text = "仪器命令集";
            // 
            // commandManipulate_Response_Format_UseGNUCoreutils_checkBox
            // 
            this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox.AutoSize = true;
            this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox.Location = new System.Drawing.Point(17, 89);
            this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox.Name = "commandManipulate_Response_Format_UseGNUCoreutils_checkBox";
            this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox.Size = new System.Drawing.Size(113, 19);
            this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox.TabIndex = 31;
            this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox.Text = "GNU核心工具";
            this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox.UseVisualStyleBackColor = true;
            this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox.CheckedChanged += new System.EventHandler(this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox_CheckedChanged);
            // 
            // commandManipulate_Write_CommandStr_label
            // 
            this.commandManipulate_Write_CommandStr_label.AutoSize = true;
            this.commandManipulate_Write_CommandStr_label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.commandManipulate_Write_CommandStr_label.Location = new System.Drawing.Point(40, 49);
            this.commandManipulate_Write_CommandStr_label.Name = "commandManipulate_Write_CommandStr_label";
            this.commandManipulate_Write_CommandStr_label.Size = new System.Drawing.Size(49, 20);
            this.commandManipulate_Write_CommandStr_label.TabIndex = 32;
            this.commandManipulate_Write_CommandStr_label.Text = "命令";
            // 
            // commandManipulate_Response_Format_Substring_label_ToChar
            // 
            this.commandManipulate_Response_Format_Substring_label_ToChar.AutoSize = true;
            this.commandManipulate_Response_Format_Substring_label_ToChar.Location = new System.Drawing.Point(223, 23);
            this.commandManipulate_Response_Format_Substring_label_ToChar.Name = "commandManipulate_Response_Format_Substring_label_ToChar";
            this.commandManipulate_Response_Format_Substring_label_ToChar.Size = new System.Drawing.Size(15, 15);
            this.commandManipulate_Response_Format_Substring_label_ToChar.TabIndex = 33;
            this.commandManipulate_Response_Format_Substring_label_ToChar.Text = "-";
            // 
            // commandManipulate_WriteAndRead_button
            // 
            this.commandManipulate_WriteAndRead_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManipulate_WriteAndRead_button.BackColor = System.Drawing.SystemColors.Control;
            this.commandManipulate_WriteAndRead_button.Font = new System.Drawing.Font("宋体", 15F);
            this.commandManipulate_WriteAndRead_button.Location = new System.Drawing.Point(90, 110);
            this.commandManipulate_WriteAndRead_button.Name = "commandManipulate_WriteAndRead_button";
            this.commandManipulate_WriteAndRead_button.Size = new System.Drawing.Size(692, 39);
            this.commandManipulate_WriteAndRead_button.TabIndex = 34;
            this.commandManipulate_WriteAndRead_button.Text = "发送&&读取";
            this.commandManipulate_WriteAndRead_button.UseVisualStyleBackColor = false;
            this.commandManipulate_WriteAndRead_button.Click += new System.EventHandler(this.commandManipulate_WriteAndRead_button_Click);
            // 
            // commandManipulate_Write_CommandStr_comboBox
            // 
            this.commandManipulate_Write_CommandStr_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManipulate_Write_CommandStr_comboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.commandManipulate_Write_CommandStr_comboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.commandManipulate_Write_CommandStr_comboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.commandManipulate_Write_CommandStr_comboBox.FormattingEnabled = true;
            this.commandManipulate_Write_CommandStr_comboBox.Items.AddRange(new object[] {
            "*IDN?",
            "*CLS",
            "*RST",
            "*TST?",
            "*STB?",
            "MEASure:VOLTage:DC?",
            "SENSe:FUNCtion \"FRESistance\"",
            "SENSe:FUNCtion \"RESistance\"",
            "MEASure:CONTinuity?"});
            this.commandManipulate_Write_CommandStr_comboBox.Location = new System.Drawing.Point(90, 46);
            this.commandManipulate_Write_CommandStr_comboBox.Name = "commandManipulate_Write_CommandStr_comboBox";
            this.commandManipulate_Write_CommandStr_comboBox.Size = new System.Drawing.Size(736, 26);
            this.commandManipulate_Write_CommandStr_comboBox.TabIndex = 35;
            this.commandManipulate_Write_CommandStr_comboBox.Text = "*IDN?";
            this.commandManipulate_Write_CommandStr_comboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.commandManipulate_Write_CommandStr_comboBox_DrawItem);
            this.commandManipulate_Write_CommandStr_comboBox.SelectedIndexChanged += new System.EventHandler(this.commandManipulate_Write_CommandStr_comboBox_SelectedIndexChanged);
            this.commandManipulate_Write_CommandStr_comboBox.DropDownClosed += new System.EventHandler(this.commandManipulate_Write_CommandStr_comboBox_DropDownClosed);
            this.commandManipulate_Write_CommandStr_comboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandManipulate_Write_CommandStr_comboBox_KeyDown);
            // 
            // Debug_textBox
            // 
            this.Debug_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Debug_textBox.BackColor = System.Drawing.Color.Gray;
            this.Debug_textBox.Location = new System.Drawing.Point(832, 164);
            this.Debug_textBox.Name = "Debug_textBox";
            this.Debug_textBox.Size = new System.Drawing.Size(168, 25);
            this.Debug_textBox.TabIndex = 36;
            // 
            // Debug_label
            // 
            this.Debug_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Debug_label.AutoSize = true;
            this.Debug_label.ForeColor = System.Drawing.Color.Red;
            this.Debug_label.Location = new System.Drawing.Point(835, 149);
            this.Debug_label.Name = "Debug_label";
            this.Debug_label.Size = new System.Drawing.Size(67, 15);
            this.Debug_label.TabIndex = 37;
            this.Debug_label.Text = "实时调试";
            // 
            // commandManipulate_Response_Format_Substring_panel
            // 
            this.commandManipulate_Response_Format_Substring_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManipulate_Response_Format_Substring_panel.BackColor = System.Drawing.Color.BurlyWood;
            this.commandManipulate_Response_Format_Substring_panel.Controls.Add(this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox);
            this.commandManipulate_Response_Format_Substring_panel.Controls.Add(this.commandManipulate_Response_Format_Substring_EndIndex_textBox);
            this.commandManipulate_Response_Format_Substring_panel.Controls.Add(this.commandManipulate_Response_Format_Substring_label_ToChar);
            this.commandManipulate_Response_Format_Substring_panel.Controls.Add(this.commandManipulate_Response_Format_Substring_checkBox);
            this.commandManipulate_Response_Format_Substring_panel.Controls.Add(this.commandManipulate_Response_Format_Substring_StartIndex_textBox);
            this.commandManipulate_Response_Format_Substring_panel.Controls.Add(this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox);
            this.commandManipulate_Response_Format_Substring_panel.Controls.Add(this.commandManipulate_Response_Format_RegexExpression_comboBox);
            this.commandManipulate_Response_Format_Substring_panel.Controls.Add(this.commandManipulate_Response_Format_RegexExpression_checkBox);
            this.commandManipulate_Response_Format_Substring_panel.Location = new System.Drawing.Point(5, 348);
            this.commandManipulate_Response_Format_Substring_panel.Name = "commandManipulate_Response_Format_Substring_panel";
            this.commandManipulate_Response_Format_Substring_panel.Size = new System.Drawing.Size(1008, 113);
            this.commandManipulate_Response_Format_Substring_panel.TabIndex = 38;
            // 
            // commandManipulate_Response_Format_UseGNUCoreutils_comboBox
            // 
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.FormattingEnabled = true;
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.Location = new System.Drawing.Point(150, 81);
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.Name = "commandManipulate_Response_Format_UseGNUCoreutils_comboBox";
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.Size = new System.Drawing.Size(849, 26);
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.TabIndex = 40;
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox_DrawItem);
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.DropDownClosed += new System.EventHandler(this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox_DropDownClosed);
            // 
            // commandManipulate_Response_Format_RegexExpression_comboBox
            // 
            this.commandManipulate_Response_Format_RegexExpression_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManipulate_Response_Format_RegexExpression_comboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.commandManipulate_Response_Format_RegexExpression_comboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.commandManipulate_Response_Format_RegexExpression_comboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.commandManipulate_Response_Format_RegexExpression_comboBox.FormattingEnabled = true;
            this.commandManipulate_Response_Format_RegexExpression_comboBox.Location = new System.Drawing.Point(150, 52);
            this.commandManipulate_Response_Format_RegexExpression_comboBox.Name = "commandManipulate_Response_Format_RegexExpression_comboBox";
            this.commandManipulate_Response_Format_RegexExpression_comboBox.Size = new System.Drawing.Size(849, 26);
            this.commandManipulate_Response_Format_RegexExpression_comboBox.TabIndex = 39;
            this.commandManipulate_Response_Format_RegexExpression_comboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.commandManipulate_Response_Format_RegexExpression_comboBox_DrawItem);
            this.commandManipulate_Response_Format_RegexExpression_comboBox.DropDownClosed += new System.EventHandler(this.commandManipulate_Response_Format_RegexExpression_comboBox_DropDownClosed);
            // 
            // commandManipulate_panel
            // 
            this.commandManipulate_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManipulate_panel.BackColor = System.Drawing.Color.DarkKhaki;
            this.commandManipulate_panel.Controls.Add(this.MeasureCountLimit_label);
            this.commandManipulate_panel.Controls.Add(this.MeasureCountLimit_textBox);
            this.commandManipulate_panel.Controls.Add(this.Debug_label);
            this.commandManipulate_panel.Controls.Add(this.Debug_textBox);
            this.commandManipulate_panel.Controls.Add(this.commandManipulate_Write_CommandStr_comboBox);
            this.commandManipulate_panel.Controls.Add(this.commandManipulate_WriteAndRead_button);
            this.commandManipulate_panel.Controls.Add(this.commandManipulate_Write_CommandStr_label);
            this.commandManipulate_panel.Controls.Add(this.InstrumentsCommandSet_label);
            this.commandManipulate_panel.Controls.Add(this.SaveTheResponseToFile_Path_BrowserButton);
            this.commandManipulate_panel.Controls.Add(this.SaveTheResponseToFile_Path_textBox);
            this.commandManipulate_panel.Controls.Add(this.InstrumentsCommand_Set_comboBox);
            this.commandManipulate_panel.Controls.Add(this.commandManipulate_Write_isCycle_Internal_label);
            this.commandManipulate_panel.Controls.Add(this.SaveTheResponseToFile_checkBox);
            this.commandManipulate_panel.Controls.Add(this.commandManipulate_Write_isCycle_Internal_textBox);
            this.commandManipulate_panel.Controls.Add(this.commandManipulate_Read_button);
            this.commandManipulate_panel.Controls.Add(this.commandManipulate_Write_isBiodirection_checkBox);
            this.commandManipulate_panel.Controls.Add(this.commandManipulate_Write_isCycle_checkBox);
            this.commandManipulate_panel.Controls.Add(this.commandManipulate_Write_button);
            this.commandManipulate_panel.Location = new System.Drawing.Point(9, 116);
            this.commandManipulate_panel.Name = "commandManipulate_panel";
            this.commandManipulate_panel.Size = new System.Drawing.Size(1004, 192);
            this.commandManipulate_panel.TabIndex = 39;
            // 
            // MeasureCountLimit_label
            // 
            this.MeasureCountLimit_label.AutoSize = true;
            this.MeasureCountLimit_label.Location = new System.Drawing.Point(496, 86);
            this.MeasureCountLimit_label.Name = "MeasureCountLimit_label";
            this.MeasureCountLimit_label.Size = new System.Drawing.Size(67, 15);
            this.MeasureCountLimit_label.TabIndex = 40;
            this.MeasureCountLimit_label.Text = "次数限制";
            // 
            // MeasureCountLimit_textBox
            // 
            this.MeasureCountLimit_textBox.Location = new System.Drawing.Point(569, 78);
            this.MeasureCountLimit_textBox.Name = "MeasureCountLimit_textBox";
            this.MeasureCountLimit_textBox.Size = new System.Drawing.Size(121, 25);
            this.MeasureCountLimit_textBox.TabIndex = 39;
            this.MeasureCountLimit_textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MeasureCountLimit_textBox_KeyPress);
            this.MeasureCountLimit_textBox.Leave += new System.EventHandler(this.MeasureCountLimit_textBox_Leave);
            // 
            // commandManipulate_Response_richTextBox_label2
            // 
            this.commandManipulate_Response_richTextBox_label2.AutoSize = true;
            this.commandManipulate_Response_richTextBox_label2.BackColor = System.Drawing.Color.PeachPuff;
            this.commandManipulate_Response_richTextBox_label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.commandManipulate_Response_richTextBox_label2.Location = new System.Drawing.Point(7, 20);
            this.commandManipulate_Response_richTextBox_label2.Name = "commandManipulate_Response_richTextBox_label2";
            this.commandManipulate_Response_richTextBox_label2.Size = new System.Drawing.Size(89, 20);
            this.commandManipulate_Response_richTextBox_label2.TabIndex = 40;
            this.commandManipulate_Response_richTextBox_label2.Text = "实时输出";
            // 
            // commandManipulate_Response_richTextBox_Clean_button
            // 
            this.commandManipulate_Response_richTextBox_Clean_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManipulate_Response_richTextBox_Clean_button.BackColor = System.Drawing.Color.AntiqueWhite;
            this.commandManipulate_Response_richTextBox_Clean_button.Location = new System.Drawing.Point(907, 15);
            this.commandManipulate_Response_richTextBox_Clean_button.Name = "commandManipulate_Response_richTextBox_Clean_button";
            this.commandManipulate_Response_richTextBox_Clean_button.Size = new System.Drawing.Size(88, 25);
            this.commandManipulate_Response_richTextBox_Clean_button.TabIndex = 4;
            this.commandManipulate_Response_richTextBox_Clean_button.Text = "清空";
            this.commandManipulate_Response_richTextBox_Clean_button.UseVisualStyleBackColor = false;
            this.commandManipulate_Response_richTextBox_Clean_button.Click += new System.EventHandler(this.commandManipulate_Response_richTextBox_Clean_button_Click);
            // 
            // commandManipulate_Response_richTextBox_panel
            // 
            this.commandManipulate_Response_richTextBox_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.commandManipulate_Response_richTextBox_panel.Controls.Add(this.commandManipulate_Response_richTextBox_label2);
            this.commandManipulate_Response_richTextBox_panel.Controls.Add(this.commandManipulate_Response_richTextBox_Clean_button);
            this.commandManipulate_Response_richTextBox_panel.Controls.Add(this.commandManipulate_Response_richTextBox);
            this.commandManipulate_Response_richTextBox_panel.Location = new System.Drawing.Point(9, 475);
            this.commandManipulate_Response_richTextBox_panel.Name = "commandManipulate_Response_richTextBox_panel";
            this.commandManipulate_Response_richTextBox_panel.Size = new System.Drawing.Size(1003, 260);
            this.commandManipulate_Response_richTextBox_panel.TabIndex = 41;
            // 
            // VISA_CommonUnit
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1025, 739);
            this.Controls.Add(this.commandManipulate_panel);
            this.Controls.Add(this.SessionButton_label);
            this.Controls.Add(this.commandManipulate_Response_Format_label);
            this.Controls.Add(this.SessionButton_panel);
            this.Controls.Add(this.commandManipulate_Response_Format_Substring_panel);
            this.Controls.Add(this.commandManipulate_Response_richTextBox_panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VISA_CommonUnit";
            this.Text = "VISA--GPIB--CommonUnit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VISAGPIBCommonUnit_FormClosing);
            this.Load += new System.EventHandler(this.VISAGPIBCommonUnit_Load);
            this.Resize += new System.EventHandler(this.VISAGPIBCommonUnit_Resize);
            this.SessionButton_panel.ResumeLayout(false);
            this.SessionButton_panel.PerformLayout();
            this.commandManipulate_Response_Format_Substring_panel.ResumeLayout(false);
            this.commandManipulate_Response_Format_Substring_panel.PerformLayout();
            this.commandManipulate_panel.ResumeLayout(false);
            this.commandManipulate_panel.PerformLayout();
            this.commandManipulate_Response_richTextBox_panel.ResumeLayout(false);
            this.commandManipulate_Response_richTextBox_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openSessionButton_button;
        private System.Windows.Forms.Button closeSessionButton_button;
        private System.Windows.Forms.Panel SessionButton_panel;
        private System.Windows.Forms.Label SessionButton_label;
        private System.Windows.Forms.Button commandManipulate_Write_button;
        private System.Windows.Forms.CheckBox commandManipulate_Write_isCycle_checkBox;
        private System.Windows.Forms.CheckBox commandManipulate_Write_isBiodirection_checkBox;
        private System.Windows.Forms.Button commandManipulate_Read_button;
        private System.Windows.Forms.RichTextBox commandManipulate_Response_richTextBox;
        private System.Windows.Forms.TextBox commandManipulate_Write_isCycle_Internal_textBox;
        private System.Windows.Forms.Label commandManipulate_Write_isCycle_Internal_label;
        private System.Windows.Forms.Label commandManipulate_Response_Format_label;
        private System.Windows.Forms.TextBox commandManipulate_Response_Format_Substring_StartIndex_textBox;
        private System.Windows.Forms.TextBox commandManipulate_Response_Format_Substring_EndIndex_textBox;
        private System.Windows.Forms.CheckBox commandManipulate_Response_Format_Substring_checkBox;
        private System.Windows.Forms.CheckBox commandManipulate_Response_Format_RegexExpression_checkBox;
        private System.Windows.Forms.CheckBox SaveTheResponseToFile_checkBox;
        private System.Windows.Forms.TextBox SaveTheResponseToFile_Path_textBox;
        private System.Windows.Forms.Button SaveTheResponseToFile_Path_BrowserButton;
        private System.Windows.Forms.ComboBox InstrumentsCommand_Set_comboBox;
        private System.Windows.Forms.Label InstrumentsCommandSet_label;
        private System.Windows.Forms.CheckBox commandManipulate_Response_Format_UseGNUCoreutils_checkBox;
        private System.Windows.Forms.Label commandManipulate_Write_CommandStr_label;
        private System.Windows.Forms.Label commandManipulate_Response_Format_Substring_label_ToChar;
        private System.Windows.Forms.Button commandManipulate_WriteAndRead_button;
        private System.Windows.Forms.ComboBox commandManipulate_Write_CommandStr_comboBox;
        private System.Windows.Forms.Label Session_CurrentSelectDevice_Label;
        public System.Windows.Forms.TextBox Session_CurrentSelectDevice_textBox;
        private System.Windows.Forms.TextBox Debug_textBox;
        private System.Windows.Forms.Label Debug_label;
        private System.Windows.Forms.Panel commandManipulate_Response_Format_Substring_panel;
        private System.Windows.Forms.ComboBox commandManipulate_Response_Format_RegexExpression_comboBox;
        private System.Windows.Forms.ComboBox commandManipulate_Response_Format_UseGNUCoreutils_comboBox;
        private System.Windows.Forms.Panel commandManipulate_panel;
        private System.Windows.Forms.Label commandManipulate_Response_richTextBox_label2;
        private System.Windows.Forms.Button commandManipulate_Response_richTextBox_Clean_button;
        private System.Windows.Forms.Panel commandManipulate_Response_richTextBox_panel;
        private System.Windows.Forms.TextBox MeasureCountLimit_textBox;
        private System.Windows.Forms.Label MeasureCountLimit_label;
    }
}

