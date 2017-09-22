/*
 * 由于使用了NI Visa，调试时使用NI I/O Trace才能捕获到信息 
 */

#define DEBUG_TheVariablesOfTheControl //控件值 调试开关
//#define DEBUG_Interface  //调试各接口类型   Gpib  Serial
//#define Debug_CommandListAttribute    //获取commandSet中的仪器命令属性 调试开关
//#define Debug_ControlSetPrase  //解析命令集时调试开关
//#define Debug_KeyPress   //按键 调试开关
//#define Debug_RunCMD                    //调用cmd时的输出 调试开关
//#define Debug_FileAbout   //文件操作 调试开关

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.VisaNS;

namespace VISA_CommonUnit
{
    public partial class VISA_CommonUnit : Form
    {
        private MessageBasedSession mbSession;
        public VISA_CommonUnit()
        {
            InitializeComponent();
            SynchronizeVariableWithControls(); //同步变量到控件
            ComboBoxInit();
            AddToolTipsToControl();
        }

        //每当ComboBox中的内容改变或窗口尺寸改变时,ComboBox中的内容会被全部选中,此函数将所有的ComboBox的选择起始点移到最后去
        public void SelectComboBoxTextLast()
        {
            this.commandManipulate_Write_CommandStr_comboBox.SelectionStart = this.commandManipulate_Write_CommandStr_comboBox.SelectedText.Length;  //仪器命令
            this.commandManipulate_Response_Format_RegexExpression_comboBox.SelectionStart = this.commandManipulate_Response_Format_RegexExpression_comboBox.SelectedText.Length; //正则表达式
            this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.SelectionStart = this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.SelectedText.Length; //gnu 命令
        }

        /// <summary>
        /// 在指定的目录下搜索 命令集设置文件并添加到 命令集列表
        /// </summary>
        /// <param name="theSpeccifyDirectory"></param>
        /// <param name="theComboBox"></param>
        private void AddAllcommandSetsFileToCommandSetComboBox(String theSpeccifyDirectory, ComboBox theComboBox)
        {
            String cmdProcessToExecuteCommandSync;
            cmdProcessToExecuteCommandSync = "cd /d \"" + theSpeccifyDirectory + "\" &&  dir /b | grep -E \"\\" + GlobalVars.commandSetFileExt + "$\" | awk -F. \"{ print $1}\" "; //获取指定目录(theSpeccifyDirectory) 后缀名为 GlobalVars.commandSetFileExt 的所有文件 的命令行
            String FileListWithOutExtTmp = ExecuteCommandSync(cmdProcessToExecuteCommandSync); // 执行命令,返回的所有文件以整个字符串的形式储存
            String[] FileListWithOutExtList = FileListWithOutExtTmp.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries); // 将返回的文件分割为文件列表 https://stackoverflow.com/questions/7393119/c-splitting-a-string-and-not-returning-empty-string
            foreach (String s in FileListWithOutExtList)
            {
                theComboBox.Items.Add(s.Replace("\n", "").Replace("\r", "")); //去除换行符再添加
            }
        }
        private void AddToolTipsToControl()
        {
            ToolTip myToolTip = new ToolTip();
            myToolTip.SetToolTip(this.openSessionButton_button, "开启一个基于消息的(Message Based)新会话");
            myToolTip.SetToolTip(this.Session_CurrentSelectDevice_textBox, "指示当前打开的设备地址");
            myToolTip.SetToolTip(this.closeSessionButton_button, "关闭当前已经打开的会话");
            myToolTip.SetToolTip(this.commandManipulate_Write_CommandStr_label, "要发送到仪器的命令,可以手动输入,也可以从下拉列表中选择,\n输入完或选定命令后可以按 回车键(ENTER) 直接发送命令，也可以点击右边发送按钮发送\n多条命令可以队列式发送 例如 *CLS;:*IDN?;  :TST?  需要注意的是分号 与冒号之间可以是任意间隔,但冒号必须紧接下一条要发送的命令  \n命令发送完成后，如果命令有返回值,点击右边读取按钮进行读取操作,也可直接按 Alt 键 读取内容.\n如果需要循环发送并读取，选中下方 \"双向命令\" 复选框后,点击  \"发送&读取\" 按钮");
            myToolTip.SetToolTip(this.commandManipulate_Write_CommandStr_comboBox, myToolTip.GetToolTip(this.commandManipulate_Write_CommandStr_label));
            myToolTip.SetToolTip(this.commandManipulate_Write_button, "发送左边命令输入框中的命令");
            myToolTip.SetToolTip(this.commandManipulate_Read_button, "如果点击过 \"发送命令\" 按钮且发送的命令有返回值,则可点击此按钮取回仪器返回信息");
            myToolTip.SetToolTip(this.commandManipulate_Write_isBiodirection_checkBox, "命令输入框中的命令是否是有返回值的命令\n如果勾选此框,则 \"读取\" 按钮 和 \"发送&读取\"按钮可用");
            myToolTip.SetToolTip(this.commandManipulate_Write_isCycle_checkBox, "勾选此框后,若点击  \"发送&读取\"按钮 将重复发送命令输入框中\n的内容并读取返回结果,循环发送时间间隔为右边输入框中指定的值,单位为ms");
            myToolTip.SetToolTip(this.commandManipulate_Write_isCycle_Internal_textBox, "循环执行时每次 发送&读取 操作的时间间隔,单位为ms");
            myToolTip.SetToolTip(this.MeasureCountLimit_label, "测量次数限制,0为不限制测量次数");
            myToolTip.SetToolTip(this.MeasureCountLimit_textBox, myToolTip.GetToolTip(this.MeasureCountLimit_label));
            myToolTip.SetToolTip(this.SaveTheResponseToFile_checkBox, "是否将\"发送&读取\" 取回的内容保存到文件");
            myToolTip.SetToolTip(this.SaveTheResponseToFile_Path_textBox, "文件路径");
            myToolTip.SetToolTip(this.SaveTheResponseToFile_Path_BrowserButton, "选择将文件保存到什么地方/选择文件名");
            myToolTip.SetToolTip(this.commandManipulate_Response_Format_label, "是否将取回的内容进行格式化(即只保留返回内容的一部分或以另外指定的格式来显示)\n 以下三个选项，从上到下依次执行选定的项,\n每次操作将会在上一个选项作用后的结果后继续进行");
            myToolTip.SetToolTip(this.commandManipulate_Response_Format_Substring_panel, myToolTip.GetToolTip(this.commandManipulate_Response_Format_label));
            myToolTip.SetToolTip(this.commandManipulate_Response_Format_Substring_checkBox, "截取仪器返回结果中指定区域的内容,例如连接到Agilent34401A,在命令输入框中输入 *IDN?  点击 \"发送&读取\"  按钮后，将会得到返回结果 HEWLETT-PACKARD,34401A,0,11-5-52 \n,选定此项后，在右边输入框中分别输入2和18,点击 \"发送&读取\"  按钮后会得到 \n WLETT-PACKARD,3440 \n 即 2 和18 表示取出 第2个字符之后的连续18个字符 ");
            myToolTip.SetToolTip(this.commandManipulate_Response_Format_RegexExpression_checkBox, "使用正则表达式格式化返回内容,如果返回内容为  123456-78-9 \n使用表达式 ([0-9]{6})|([0-9]{2})|([0-9]) 后将得到\n 123456  78  9");
            myToolTip.SetToolTip(this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox, "使用GNU核心工具链格式化输出,像在shell里面输入命令一样在右边输入框中输入命令即可,例如,如果返回内容为  123456-78-9 \n使用表达式  awk -F- \"{print $1 $2 $3}\" | rev 或 sed \"s@-@@g\" | rev  \n 都能得到同样的结果 987654321");
        }


        /// <summary>
        /// 清空所有全局变量中的 commandSet List
        /// </summary>
        private static void ClearGlobalVarscommandSet()
        {
            GlobalVars.commandSetCommandList.Clear();
            GlobalVars.commandSetCommandListAttribute.Clear();
            GlobalVars.commandSetCommandListToolTipsStrings.Clear();
            GlobalVars.commandSetRelativeRegexExpressionList.Clear();
            GlobalVars.commandSetRelativeRegexExpressionListToolTipsStrings.Clear();
            GlobalVars.commandSetRelativeGNUCoreutilsExpressionList.Clear();
            GlobalVars.commandSetRelativeGNUCoreutilsExpressionListToolTipsStrings.Clear();
        }
        /// <summary>
        /// 将命令集文件解析到全局变量中
        /// </summary>
        /// <param name="theControlSetFileName"></param>
        /// <returns></returns>
        public static bool ApplyControlSetsAndTipsFromControlSetFileToGlobalVars(String theControlSetFileName)
        {
            if (!String.IsNullOrEmpty(theControlSetFileName))
            {
                ClearGlobalVarscommandSet();

                GlobalVars.commandSetFileName = theControlSetFileName;
                GlobalVars.commandSetFileWhole = GlobalVars.commandSetFilePath + "\\" + GlobalVars.commandSetFileName + GlobalVars.commandSetFileExt;

                /////////////////////////////////////////////////////////////关于命令的部分
                //前一部分  命令                                          使用 cut -d# -f1 不如使用  awk -F# \"{print $1}\"  效果好   特别注意 cat 文件名中有空格的情况
                String commandSetCommandListTmp = ExecuteCommandSync(" cat  \"" + GlobalVars.commandSetFileWhole + " \"   | iconv -f utf-8 -t gbk | grep -E \"^!1\" | awk -F# \"{print $1}\"  | sed \"s@^!1@@g\" | sed \"s@^[[:space:]]\\+@@g\" |sed \"s@[[:space:]]\\+$@@g\"  | sed \"s@^$@NULL@g\"");

                //中间部分  命令的属性  有些行可能没有写属性部分,那么此时就要将这些行插入字符，这里选择插入字符串 NULL 方便后面判断
                String commandSetCommandListAttributeTmp = ExecuteCommandSync(" cat  \"" + GlobalVars.commandSetFileWhole + " \"   | iconv -f utf-8 -t gbk | grep -E \"^!1\" | awk -F# \"{print $2}\"  | sed \"s@^!1@@g\" | sed \"s@^[[:space:]]\\+@@g\" |sed \"s@[[:space:]]\\+$@@g\"  | sed \"s@^$@NULL@g\"");

                //后一部分,  命令说明 有些行可能没有注释部分,那么此时就要将这些行插入字符，这里选择插入字符串 NULL 方便后面判断
                String commandSetCommandListToolTipsStringsTmp = ExecuteCommandSync(" cat  \"" + GlobalVars.commandSetFileWhole + " \"   | iconv -f utf-8 -t gbk | grep -E \"^!1\" | awk -F# \"{print $3}\"  | sed \"s@^!1@@g\" | sed \"s@^[[:space:]]\\+@@g\" | sed \"s@[[:space:]]\\+$@@g\" | sed \"s@^$@NULL@g\"");

                String[] commandSetCommandListArrayTmp = commandSetCommandListTmp.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                String[] commandSetCommandListAttributeArrayTmp = commandSetCommandListAttributeTmp.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                String[] commandSetCommandListToolTipsStringsArrayTmp = commandSetCommandListToolTipsStringsTmp.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < commandSetCommandListArrayTmp.Length; ++i)
                {
                    GlobalVars.commandSetCommandList.Add(commandSetCommandListArrayTmp[i]);
                    GlobalVars.commandSetCommandListAttribute.Add(commandSetCommandListAttributeArrayTmp[i]);
                    GlobalVars.commandSetCommandListToolTipsStrings.Add(commandSetCommandListToolTipsStringsArrayTmp[i].Replace("\\n", "\n")); // 解析说明部分换行符

#if Debug_ControlSetPrase
                MessageBox.Show(GlobalVars.commandSetCommandList[i] + "\n" + GlobalVars.commandSetCommandListAttribute[i]+"\n" + GlobalVars.commandSetCommandListToolTipsStrings[i],
                  "命令集信息", MessageBoxButtons.OK, MessageBoxIcon.Information
                  );
#endif
                }
                /////////////////////////////////////////////////////////////关于命令的部分完
                /////////////////////////////////////////////////////////////关于正则表达式的部分
                //正则表达式
                //前一部分  表达式                                          使用 cut -d# -f1 不如使用  awk -F# \"{print $1}\"  效果好
                String commandSetRelativeRegexExpressionListTmp = ExecuteCommandSync(" cat  \"" + GlobalVars.commandSetFileWhole + " \"   | iconv -f utf-8 -t gbk | grep -E \"^!2\" | awk -F# \"{print $1}\"  | sed \"s@^!2@@g\" | sed \"s@^[[:space:]]\\+@@g\" |sed \"s@[[:space:]]\\+$@@g\"  | sed \"s@^$@NULL@g\"");
                //后一部分  表达式说明
                String commandSetRelativeRegexExpressionListToolTipsStringsTmp = ExecuteCommandSync(" cat  \"" + GlobalVars.commandSetFileWhole + " \"   | iconv -f utf-8 -t gbk | grep -E \"^!2\" | awk -F# \"{print $2}\"  | sed \"s@^!2@@g\" | sed \"s@^[[:space:]]\\+@@g\" |sed \"s@[[:space:]]\\+$@@g\"  | sed \"s@^$@NULL@g\"");

                String[] commandSetRelativeRegexExpressionListArrayTmp = commandSetRelativeRegexExpressionListTmp.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                String[] commandSetRelativeRegexExpressionListToolTipsStringsArrayTmp = commandSetRelativeRegexExpressionListToolTipsStringsTmp.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < commandSetRelativeRegexExpressionListArrayTmp.Length; ++i)
                {
                    GlobalVars.commandSetRelativeRegexExpressionList.Add(commandSetRelativeRegexExpressionListArrayTmp[i]);
                    GlobalVars.commandSetRelativeRegexExpressionListToolTipsStrings.Add(commandSetRelativeRegexExpressionListToolTipsStringsArrayTmp[i].Replace("\\n", "\n")); // 解析说明部分换行符

#if Debug_ControlSetPrase
                MessageBox.Show(GlobalVars.commandSetRelativeRegexExpressionList[i] + "\n" + GlobalVars.commandSetRelativeRegexExpressionListToolTipsStrings[i],
                  "正则表达式信息", MessageBoxButtons.OK, MessageBoxIcon.Information
                  );
#endif
                }

                /////////////////////////////////////////////////////////////关正则表达式的部分完
                /////////////////////////////////////////////////////////////关于GNUCoreutils表达式的部分
                //正则表达式
                //前一部分  表达式                                          使用 cut -d# -f1 不如使用  awk -F# \"{print $1}\"  效果好
                String commandSetRelativeGNUCoreutilsExpressionListTmp = ExecuteCommandSync(" cat  \"" + GlobalVars.commandSetFileWhole + " \"   | iconv -f utf-8 -t gbk | grep -E \"^!3\" | awk -F# \"{print $1}\"  | sed \"s@^!3@@g\" | sed \"s@^[[:space:]]\\+@@g\" |sed \"s@[[:space:]]\\+$@@g\"  | sed \"s@^$@NULL@g\"");
                //后一部分  表达式说明
                String commandSetRelativeGNUCoreutilsExpressionListToolTipsStringsTmp = ExecuteCommandSync(" cat  \"" + GlobalVars.commandSetFileWhole + " \"   | iconv -f utf-8 -t gbk | grep -E \"^!3\" | awk -F# \"{print $2}\"  | sed \"s@^!3@@g\" | sed \"s@^[[:space:]]\\+@@g\" |sed \"s@[[:space:]]\\+$@@g\"  | sed \"s@^$@NULL@g\"");

                String[] commandSetRelativeGNUCoreutilsExpressionListArrayTmp = commandSetRelativeGNUCoreutilsExpressionListTmp.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                String[] commandSetRelativeGNUCoreutilsExpressionListToolTipsStringsArrayTmp = commandSetRelativeGNUCoreutilsExpressionListToolTipsStringsTmp.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < commandSetRelativeGNUCoreutilsExpressionListArrayTmp.Length; ++i)
                {
                    GlobalVars.commandSetRelativeGNUCoreutilsExpressionList.Add(commandSetRelativeGNUCoreutilsExpressionListArrayTmp[i]);
                    GlobalVars.commandSetRelativeGNUCoreutilsExpressionListToolTipsStrings.Add(commandSetRelativeGNUCoreutilsExpressionListToolTipsStringsArrayTmp[i].Replace("\\n", "\n")); // 解析说明部分换行符

#if Debug_ControlSetPrase
                MessageBox.Show(GlobalVars.commandSetRelativeGNUCoreutilsExpressionList[i] + "\n" + GlobalVars.commandSetRelativeGNUCoreutilsExpressionListToolTipsStrings[i],
                  "GNUCoreutils表达式信息", MessageBoxButtons.OK, MessageBoxIcon.Information
                  );
#endif
                    /////////////////////////////////////////////////////////////关于GNUCoreutils表达式的部分完
                }
                return true;
            }else
            {
                return false;
            }
            
            
        }


        /// <summary>
        /// 下拉列表收回后隐藏提示信息 https://stackoverflow.com/questions/680373/tooltip-for-each-items-in-a-combo-box
        /// </summary>
        /// <param name="theToolTipForHidden"></param>
        /// <param name="relatedComboBox"></param>
        public void ComboBox_DropDownClosed_HiddenRelatedToolTip(ToolTip theToolTipForHidden, ComboBox relatedComboBox)
        {
            theToolTipForHidden.Hide(relatedComboBox);
        }

        /// <summary>
        /// 当鼠标悬停到下拉列表的选项上时,显示与该选项相关的说明 https://stackoverflow.com/questions/680373/tooltip-for-each-items-in-a-combo-box
        /// </summary>
        /// <param name="e">DrawItemEventArgs </param>
        /// <param name="commandSetTip">要使用来显示提示的Tooltip</param>
        /// <param name="theComboBox">指定ComboBox对象</param>
        /// <param name="relatedListString">相关说明的List</param>
        public void ShowTipsWhen_ComboBoxItemDraw(DrawItemEventArgs e, ToolTip commandSetTip, ComboBox theComboBox, List<String> relatedListString)
        {
            if (e.Index < 0) { return; } // added this line thanks to Andrew's comment

            String comboboxText = theComboBox.GetItemText(theComboBox.Items[e.Index]);
            String Tiptext = relatedListString[e.Index];
            if (String.IsNullOrEmpty(Tiptext) || Tiptext == "NULL\r") //如果要显示的提示信息 为空 或 NULL(书写commandSet时没有写提示信息 NULL 定义在 函数ApplyControlSetsAndTipsFromControlSetFileToGlobalVars 中),则直接显示 item 内容
            {
                Tiptext = comboboxText;
            }
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            { e.Graphics.DrawString(comboboxText, e.Font, br, e.Bounds); }//显示原来的内容
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            { commandSetTip.Show(Tiptext, theComboBox, e.Bounds.Right, e.Bounds.Bottom); } //显示提示
            e.DrawFocusRectangle();

        }

        /// <summary>
        /// 清空下拉列表框中所有内容并 添加所有 List 内容到下拉列表中
        /// </summary>
        /// <param name="theComboBox"></param>
        /// <param name="relatedCommandSetStringList"></param>
        public void AddCommandSetToComboBoxs(ComboBox theComboBox, List<String> relatedCommandSetStringList)
        {
            //先清空列表
            theComboBox.Items.Clear();
            //添加列表
            foreach (String s in relatedCommandSetStringList)
            {
                theComboBox.Items.Add(s.Replace("\n", "").Replace("\r", ""));
            }


        }


        private void FreshComboBoxItems()
        {
            
            //更新命令集到全局变量中去
            ApplyControlSetsAndTipsFromControlSetFileToGlobalVars(this.InstrumentsCommand_Set_comboBox.SelectedItem.ToString());
            //添加全局变量中的各命令集到相应选框
            //添加 仪器命令
            AddCommandSetToComboBoxs(this.commandManipulate_Write_CommandStr_comboBox, GlobalVars.commandSetCommandList);
            //添加  正则表达式 
            AddCommandSetToComboBoxs(this.commandManipulate_Response_Format_RegexExpression_comboBox, GlobalVars.commandSetRelativeRegexExpressionList);
            //添加GNUCoreutils 表达式
            AddCommandSetToComboBoxs(this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox, GlobalVars.commandSetRelativeGNUCoreutilsExpressionList);
            }

        private void ComboBoxInit()
        {

            //在指定的目录下搜索 命令集设置文件并添加到 命令集列表
            AddAllcommandSetsFileToCommandSetComboBox(GlobalVars.commandSetFilePath, InstrumentsCommand_Set_comboBox);
            //默认选定第一个 命令集 --如果存在
            if (this.InstrumentsCommand_Set_comboBox.Items.Count != 0)
            {
                this.InstrumentsCommand_Set_comboBox.SelectedIndex = 0;
            }
            //初始化命令集到各组件ComboBox 
            FreshComboBoxItems();
            //默认选择第一个 --如果存在
            if (this.commandManipulate_Response_Format_RegexExpression_comboBox.Items.Count != 0)
            {
                this.commandManipulate_Response_Format_RegexExpression_comboBox.SelectedIndex = 0; //正则表达式默认选定第一个
            }
            if (this.commandManipulate_Response_Format_RegexExpression_comboBox.Items.Count != 0)
            {
                this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.SelectedIndex = 0; //GNUCoreutils 表达式选定默认选定第一个
            }
        }
        private void VISAGPIBCommonUnit_Load(object sender, EventArgs e)
        {


        }

        /// <span class="code-SummaryComment"><summary></span>
        /// Executes a shell command synchronously.
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name="command">string command</param></span>
        /// <span class="code-SummaryComment"><returns>string, as output of the command.</returns></span>
        public static String ExecuteCommandSync(String command) //-----https://www.codeproject.com/Articles/25983/How-to-Execute-a-Command-in-C
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.RedirectStandardError = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;

                //环境变量------https://stackoverflow.com/questions/28023011/start-cmd-exe-and-setting-environment-variables-does-not-show-window
                String env = System.Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
                env = env + ";" + GlobalVars.theGNUCoreutilsExecutePath;
                procStartInfo.EnvironmentVariables["PATH"] = env;

                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
#if Debug_RunCMD
                MessageBox.Show(command, "传入CMD的参数", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show(env, "当前环境变量 PATH=", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
                // Get the output into a string
                String result = proc.StandardOutput.ReadToEnd(); //读取stdout 
                if (result == String.Empty) result = proc.StandardError.ReadToEnd(); //如果返回的stdout信息为空,说明信息在 stderr
                // Display the command output.                
#if Debug_RunCMD
                 MessageBox.Show(result + "是否有空行换行?", "CMD执行后返回信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // String env = System.Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine);
#endif
                //proc.CloseMainWindow();
                proc.WaitForExit(); //等待退出
                proc.Close();//释放资源
                return result;
            }
            catch (Exception objException)
            {
                MessageBox.Show(objException.Message, "异常发生", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "Fail"; //有异常发生
        }

        public delegate void SetTextCallback(Control richTextBox, string argText);
        private BackgroundWorker _worker = null; //background worker---https://stackoverflow.com/questions/27580241/breaking-from-a-loop-with-button-click-c-sharp
        private void SetrichTextBoxShowTextSafly(Control richTextBox, string argText) //线程安全回调 循环测量在另一单独线程执行,不至于阻断当前窗口的运行
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (richTextBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetrichTextBoxShowTextSafly);
                this.Invoke(d, new object[] { richTextBox, argText });
            }
            else
            {
                richTextBox.Text += argText;
            }
        }

        /// <summary>
        /// 所有需要随时刷新窗口 状态的放到这里面 总体来说是与 连接状态相关的
        /// </summary>
        private void TheElementreFresh() // 所有需要随时刷新窗口 状态的放到这里面 总体来说是与 连接状态相关的
        {
            //命令为单向时   读取 和 发送&读取 按钮不得启用  
            //this.commandManipulate_Read_button.Enabled = GlobalVars.isCommandBiodirection; 
            this.commandManipulate_WriteAndRead_button.Enabled = GlobalVars.isCommandBiodirection;

            //测量相关
            this.commandManipulate_Write_isBiodirection_checkBox.Enabled = !GlobalVars.isMeasuring; //双向命令选项
            this.commandManipulate_Write_isCycle_checkBox.Enabled = !GlobalVars.isMeasuring; //循环发送 选框
            //this.MeasureCountLimit_textBox.Enabled = this.commandManipulate_Write_isCycle_checkBox.Enabled;  //限制测量次数
            this.commandManipulate_Write_button.Enabled = !GlobalVars.isMeasuring;//单次发送按钮
            this.commandManipulate_Read_button.Enabled = (GlobalVars.isCommandBiodirection && !GlobalVars.isMeasuring); // 在 测量 进行中或  命令为单向时 读取按钮 均 不得启用
            this.commandManipulate_Write_CommandStr_comboBox.Enabled = !(GlobalVars.isMeasuring && GlobalVars.isSendcommandCyclically); //如果双向命令在测量中,锁定命令输入框以免无意中更改了命令导致异常
            //文件操作
            this.SaveTheResponseToFile_Path_textBox.Text = GlobalVars.SaveMeasureRealustToFile_Whole;//文件路径到文本框
            this.CheckBoxStatuAdjust(this.SaveTheResponseToFile_checkBox, GlobalVars.isSaveToFile, SaveTheResponseToFile_Path_textBox, false); //是否输出到文件 textBox 和 checkBox同步
            if (GlobalVars.isSaveToFile) SaveTheResponseToFile_Path_textBox.Enabled = true; else SaveTheResponseToFile_Path_textBox.Enabled = false;

            //选框
            this.CheckBoxStatuAdjust(this.commandManipulate_Write_isCycle_checkBox, GlobalVars.isSendcommandCyclically, this.commandManipulate_Write_isCycle_Internal_textBox, false); //循环发送
            //this.MeasureCountLimit_textBox.Enabled = this.commandManipulate_Write_isCycle_checkBox.Checked;           //测量次数限制 与是否循环同步    中途可以随时更改测量次数/   取消注释(只有选中循环才能限制次数) 
            this.CheckBoxStatuAdjust(this.commandManipulate_Write_isBiodirection_checkBox, GlobalVars.isCommandBiodirection,null, false);

            //字符串格式化选项
            this.CheckBoxStatuAdjust(this.commandManipulate_Response_Format_Substring_checkBox, GlobalVars.isUsingSubstringFormat, this.commandManipulate_Response_Format_Substring_StartIndex_textBox, false);//截取字符串 开始
            this.CheckBoxStatuAdjust(this.commandManipulate_Response_Format_Substring_checkBox, GlobalVars.isUsingSubstringFormat, this.commandManipulate_Response_Format_Substring_EndIndex_textBox, false);//截取字符串 结束
            this.CheckBoxStatuAdjust(this.commandManipulate_Response_Format_RegexExpression_checkBox, GlobalVars.isUsingRegeExpression, this.commandManipulate_Response_Format_RegexExpression_comboBox, false);//字符串处理用正则表达式
            this.CheckBoxStatuAdjust(this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox, GlobalVars.isUsingGNUCoreutils, this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox, false);//字符串处理用GNU核心工具

            //文件选择按钮 与文件名显示
            if (!GlobalVars.isSaveToFile || GlobalVars.isMeasuring) //不要保存到文件,或 测量进行中 不得更改文件路径
            {
                this.SaveTheResponseToFile_Path_textBox.Enabled = false;
                this.SaveTheResponseToFile_Path_BrowserButton.Enabled = false;
            }
            else if (GlobalVars.isSaveToFile && !GlobalVars.isMeasuring) //要保存到文件 且 测量未进行 且要保存文件  可以更改文件名及路径 
            {
                this.SaveTheResponseToFile_Path_textBox.Enabled = true;
                this.SaveTheResponseToFile_Path_BrowserButton.Enabled = true;
            }

            //最高优先级-------------------会话相关
            //this.SessionButton_panel.Enabled = GlobalVars.isSessionOpened; //会话窗口
            this.closeSessionButton_button.Enabled = (GlobalVars.isSessionOpened && !GlobalVars.isMeasuring);//测量过程中不得关闭会话
            this.openSessionButton_button.Enabled = !GlobalVars.isSessionOpened; //开启会话窗口
            this.commandManipulate_Response_Format_Substring_panel.Enabled = GlobalVars.isSessionOpened; //格式化 栏
            this.commandManipulate_panel.Enabled = GlobalVars.isSessionOpened; //命令栏
            //this.commandManipulate_Response_richTextBox_panel.Enabled = GlobalVars.isSessionOpened;//实时显示

            //最后获取设备名显示在主窗口 ---可能不会成功
            if (String.IsNullOrEmpty(GlobalVars.selectedAndConnectedInstrumentName))
            {
                this.Text = GlobalVars.theFormTitleWhenNoSessionOpened;
            }
            else
            {
                this.Text = GlobalVars.theFormTitleWhenSessionOpened;
            }

            //如果不是循环发送命令则不能将测量结果保存到文件    但在测量过程中可以随时选择是否将测量结果保存到文件
            this.SaveTheResponseToFile_checkBox.Enabled = this.commandManipulate_Write_isCycle_checkBox.Checked;
            if (!this.commandManipulate_Write_isCycle_checkBox.Checked) { this.SaveTheResponseToFile_checkBox.Checked = this.commandManipulate_Write_isCycle_checkBox.Checked; }



        }
        /// <summary>
        /// 同步选框关联的值到选框状态,如果该选框与对应文本框相关联(互斥或同步),与文本框关系应用
        /// </summary>
        /// <param name="chkBox"> 选框</param>
        /// <param name="ValuecheckOrUncheck">与选框关联的值</param>
        /// <param name="mutuallyExclusiveBox">与选框关联的文本框</param>
        /// <param name="reverse">与选框关联的文本框是互斥(默认) 的还是同状态</param>
        private void CheckBoxStatuAdjust(CheckBox chkBox, bool ValuecheckOrUncheck, Control mutuallyExclusiveBox = null, bool reverse = true) //同时设置与其互斥的文本输入框
        {

                if (!ValuecheckOrUncheck)
                {
                    chkBox.Checked = false;

                }
                else
                {
                    chkBox.Checked = true;
                }
            if (mutuallyExclusiveBox != null)
            {
                if (reverse)
                {

                    mutuallyExclusiveBox.Enabled = !chkBox.Checked; //checkBox 与文本框互斥
                }
                else if (!reverse)
                {
                    mutuallyExclusiveBox.Enabled = chkBox.Checked; //checkBox 与文本框同步
                }
            }
        }

        /// <summary>
        /// checkbox 改变时将状态应用到变量,同时设置与其关联的的textbox
        /// </summary>
        /// <param name="chkBox"> 选框</param>
        /// <param name="checkBoxRelateVariablesVar">与选框关联的值</param>
        /// <param name="mutuallyExclusiveBox">与选框关联的文本框</param>
        /// <param name="reverse">与选框关联的文本框是互斥(默认) 的还是同状态</param>
        private void CheckBoxApplyToVariables(CheckBox chkBox, ref bool checkBoxRelateVariablesVar, Control mutuallyExclusiveBox = null, bool reverse = true)//checkbox 改变时将状态应用到变量,同时设置与其互斥的textbox
        {
            if (chkBox.Checked) //选中
            {
                checkBoxRelateVariablesVar = true;
            }
            else               // 未选中
            {
                checkBoxRelateVariablesVar = false;
            }
            if (mutuallyExclusiveBox != null)
            {
                if (reverse)
                {
                    mutuallyExclusiveBox.Enabled = !chkBox.Checked;
                }
                else if (!reverse)
                {
                    mutuallyExclusiveBox.Enabled = chkBox.Checked;
                }
            }
        }

        /// <summary>
        ///页面加载时初始化控件 只在窗口加载时运行的代码,只运行一次
        /// </summary>
        private void SynchronizeVariableWithControls() //更新变量到控件显示 只在窗口加载时运行的代码,只运行一次
        {
#if DEBUG_TheVariablesOfTheControl  //Debug 文本框
            this.Debug_textBox.Visible = true;
            this.Debug_label.Visible = true;
            this.Debug_textBox.Enabled = true;
#else
            this.Debug_textBox.Visible = false;
            this.Debug_label.Visible = false;
            this.Debug_textBox.Enabled = false;

#endif
            this.SyncVariablesToTextBox(ref commandManipulate_Write_isCycle_Internal_textBox, ref GlobalVars.commandSendAndWriteQueryInternal, 0);
            //this.commandManipulate_Write_isCycle_Internal_textBox.Text = System.String.Format("{0:F0}", GlobalVars.commandSendAndWriteQueryInternal); //连续发送时的时间间隔  
            this.SyncVariablesToTextBox(ref MeasureCountLimit_textBox, ref GlobalVars.MeasureCountLimit, 0);
            //this.MeasureCountLimit_textBox.Text = System.String.Format("{0:F0}", GlobalVars.MeasureCountLimit); //测量次数限制  
            this.CheckBoxStatuAdjust(this.commandManipulate_Write_isBiodirection_checkBox, GlobalVars.isCommandBiodirection);//双向 命令

            this.SyncVariablesToTextBox(ref commandManipulate_Response_Format_Substring_StartIndex_textBox, ref GlobalVars.theSubstringStart, 0);
            this.SyncVariablesToTextBox(ref commandManipulate_Response_Format_Substring_EndIndex_textBox, ref GlobalVars.theSubstringEnd, 0);
            this.Text = GlobalVars.theFormTitleWhenNoSessionOpened;//窗口名称

            //发送&读取按钮
            this.commandManipulate_WriteAndRead_button.Text = "发送&&读取";//变换按钮文字
            this.commandManipulate_WriteAndRead_button.BackColor = Color.Green;//更改背景

            this.TheElementreFresh();

        }
        /// <summary>
        /// 同步文本框输入内容并检查合法性到变量
        /// </summary>
        /// <param name="txtBox"></param>
        /// <param name="txtBoxRelateVariablesVar"></param>
        /// <param name="digitalNumbersAferDecimalPoint"></param>
        /// <returns></returns>
        private bool SyncTextBoxAndVariables(ref TextBox txtBox, ref Int32 txtBoxRelateVariablesVar, int digitalNumbersAferDecimalPoint = 0) //同步文本框输入的在合法范围的值到变量
        {
            String stringFormatExpression = "{0:F" + digitalNumbersAferDecimalPoint.ToString() + "}"; //trick for 让String.Format接受参数
            //String previousValueString = System.String.Format(stringFormatExpression, txtBoxRelateVariablesVar);
            //正则表达式先检查输入合法性
            if (System.Text.RegularExpressions.Regex.IsMatch(txtBox.Text, "^[0-9]*[.]{0,1}[0-9]*$") ||  // 任意小数  任意整数
                System.Text.RegularExpressions.Regex.IsMatch(txtBox.Text, "^[0-9]*[.]{0,1}[0-9]*[eE][+-]{0,1}[0-9]*$")     //科学记数法 1.25e+3  1e3  
                )
            {   //如果输入合法 立即解析为double 类型  //主要是为了使用科学计数法
                if (txtBox.Text == String.Empty) txtBox.Text = "0";
                double praseTmp = double.Parse(txtBox.Text);
                txtBoxRelateVariablesVar = (Int32)praseTmp;
                SyncVariablesToTextBox(ref txtBox, ref txtBoxRelateVariablesVar, 0); //更新显示

            }
            else
            {   //如果输入不合法
                MessageBox.Show("请输入正确值,不能含数字和小数外的其他字符 支持的输入形式为\n 10\n 5.2365 \n 1e+2 \n5.2365e+2 \n 5.2365e-2 \n 5.2365e+002 \n5.2365e-0002 \n 等",
                    "输入不合法",
                        MessageBoxButtons.OK,
                               //MessageBoxIcon.Warning // for Warning  
                               MessageBoxIcon.Error // for Error 
                                                    //MessageBoxIcon.Information  // for Information
                                                    //MessageBoxIcon.Question // for Question
                                );
                txtBox.Text = System.String.Format(stringFormatExpression, txtBoxRelateVariablesVar);
                return false;
            }

            return true;
        }

        private bool SyncVariablesToTextBox(ref TextBox txtBox, ref Int32 txtBoxRelateVariablesVar, int digitalNumbersAferDecimalPoint = 11) //同步文本框输入的在合法范围将变量的值显示到文本输入框
        {
            String stringFormatExpression = "{0:F" + digitalNumbersAferDecimalPoint.ToString() + "}"; //trick for 让String.Format接受参数
                                                                                                      //String previousValueString = System.String.Format(stringFormatExpression, txtBoxRelateVariablesVar);
                                                                                                      //格式化到文本框中           
            txtBox.Text = System.String.Format(stringFormatExpression, txtBoxRelateVariablesVar); //更新当前文本框显示

            return true;
        }

        /// <summary>
        /// 选择文件保存位置
        /// </summary>
        /// <returns></returns>
        public bool ChoseWhichFileToSave() //--http://blog.csdn.net/techzero/article/details/27709981
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //打开的文件选择对话框上的标题  
            saveFileDialog.Title = "请选择文件";
            //初始文件夹
            saveFileDialog.InitialDirectory = GlobalVars.SaveMeasureRealustToFile_Directory;
            //默认文件名
            saveFileDialog.FileName = GlobalVars.SaveMeasureRealustToFile_FileName;
            //设置文件类型  
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|逗号分隔文件(*.csv)|*.csv|所有文件(*.*)|*.*";
            //设置默认文件类型显示顺序  
            saveFileDialog.FilterIndex = 2;
            //保存对话框是否记忆上次打开的目录  
            saveFileDialog.RestoreDirectory = true;
            //按下确定选择的按钮  
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径  
                String localFilePath = saveFileDialog.FileName.ToString();
                //获取文件路径，不带文件名  
                String FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                //获取文件名，带后缀名，不带路径  
                String fileNameWithSuffix = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                //去除文件后缀名  
                //String  fileNameWithoutSuffix = fileNameWithSuffix.Substring(0, fileNameWithSuffix.LastIndexOf("."));
                GlobalVars.SaveMeasureRealustToFile_Directory = FilePath;
                GlobalVars.SaveMeasureRealustToFile_FileName = fileNameWithSuffix;
                GlobalVars.SaveMeasureRealustToFile_Whole = GlobalVars.SaveMeasureRealustToFile_Directory + "\\" + GlobalVars.SaveMeasureRealustToFile_FileName;
            }
            return true;
        }
        /// <summary>
        /// 检查文件名和路径是否合法,如果合法,更新到变量
        /// </summary>
        /// <param name="txtBox"></param>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public bool isTextBoxFileNameValid(ref TextBox txtBox, ref String inputString)
        {
            bool isPathValid = true;
            bool isFileNameValid = true;
            String tmpStr = txtBox.Text;
            String tmpFilePath = tmpStr.Substring(0, tmpStr.LastIndexOf("\\")); //路径
            String tmpFileName = tmpStr.Substring(tmpFilePath.Length + 1);//文件名
#if Debug_FileAbout
            MessageBox.Show("输入的路径:tmpFilePath=" + tmpFilePath, "Debug信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show("输入的文件名:tmpFileName=" + tmpFileName, "Debug信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
            Regex regex = new Regex(@"^([a-zA-Z]:\\)?[^\/\:\*\?\""\<\>\|\,]*$");
            Match m = regex.Match(tmpFilePath);
            if (!m.Success) //路径不合法
            {
                isPathValid = false;
                MessageBox.Show("非法的文件保存路径，请重新选择或输入！", "路径错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            regex = new Regex(@"^[^\/\:\*\?\""\<\>\|\,]+$");
            m = regex.Match(tmpFileName);
            if (!m.Success)//文件名不合法
            {
                isFileNameValid = false;
                MessageBox.Show("请勿在文件名中包含\\ / : * ？ \" < > |等字符，请重新输入有效文件名！", "文件名错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (isPathValid && isFileNameValid)
            {
                inputString = txtBox.Text;//合法则将更改应用到变量
            }
            else
            {
                txtBox.Text = inputString;//不合法则显示回原来合法的
            }
            return true;
        }
        public static String GetInstrumentName(MessageBasedSession mbs)
        {
            try
            {
                //System.Threading.Thread.Sleep(300); //等待300ms
                if (GlobalVars.isSessionOpened)
                {
                    String nativeInstrumentName = mbs.Query(GlobalVars.theCommandForQueryInstrumentsInfomation); //询问仪器信息
                    if (!String.IsNullOrEmpty(nativeInstrumentName))
                    {
                        nativeInstrumentName = nativeInstrumentName.Replace("\n", String.Empty).Replace("\r", String.Empty); //替换文末结束符 便于处理
                        GlobalVars.selectedAndConnectedInstrumentName = ExecuteCommandSync("echo " + nativeInstrumentName + " | awk -F, \"{print $1\\\" \\\"$2}\" | sed \"s@-@ @g\" | sed \"s@  \\+@ @g\" | tr [:upper:]  [:lower:] | sed -e \"s@\\b\\(.\\)@\\u\\1@g\" | sed  \"s@[^a-z][a-z]$@\\U&@g\" |  sed  \"s@[^a-z][a-z][^a-z]@\\U&@g\" | sed  \"s@Quadtech@QuadTech@g\" "); //转换仪器名的所有单词只首字母大写，其余单个字母均大写
                        GlobalVars.theFormTitleWhenSessionOpened = GlobalVars.selectedAndConnectedInstrumentName + "-----已连接----" + GlobalVars.currentInterfaceType;
                    }                    

                }
            }
            catch (Exception exp) //显示异常
            {
                MessageBox.Show(exp.Message,
                                "异常发生",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error // for Error 
                        );
                return null;
            }

            return GlobalVars.selectedAndConnectedInstrumentName;
        }

        private void GetInterfaceTypeAndSetItToGlobalVars()
        {
            GlobalVars.currentInterfaceType = mbSession.HardwareInterfaceType.ToString().ToUpper();
#if DEBUG_Interface
            MessageBox.Show(GlobalVars.currentInterfaceType, "当前使用硬件接口类型", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessageBox.Show(mbSession.HardwareInterfaceName, "当前使用硬件接口名", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif

        }
        private void SetSerialAttribute(ref MessageBasedSession mbs)
        {
            SerialSession ser = (SerialSession)mbs;
            ser.BaudRate = GlobalVars.SerialBaudRate; //设置速率
            ser.DataBits = GlobalVars.SerialDataBits;      //数据位
            ser.StopBits = GlobalVars.SerialStopBits; //停止位 
            ser.Parity = GlobalVars.SerialParity; //校验     NONE 0  Odd 1  Even 2 Mark 3 Space 4
            ser.FlowControl = GlobalVars.SerialFlowControl; //Flow Control  NONE 0  XON/XOFF 1   使用 NI I/O Trace 监视   VISA Test Panel 设置时得到
            MessageBox.Show("当前正使用COM通信,当前设置为:\n速率"+GlobalVars.SerialBaudRate.ToString()+"\n数据位:"+GlobalVars.SerialDataBits.ToString()+"\n停止位:"+(((int) GlobalVars.SerialStopBits)/10).ToString()+"\n校验方式(Parity):" + GlobalVars.SerialParityEnumList[((int)GlobalVars.SerialParity)]+"\nFlowControl:"+ GlobalVars.SerialFlowControlEnumList[((int)GlobalVars.SerialFlowControl)]+"\n\n请确保仪器设置与本设置相符",
                "当前通讯设置",MessageBoxButtons.OK,MessageBoxIcon.Information
                );
        }
        private void openSessionButton_button_Click(object sender, EventArgs e)
        {
            SelectDeviceForm myDevice = new SelectDeviceForm();

            DialogResult result = myDevice.ShowDialog(this);
            if (result == DialogResult.OK)
            {

                try
                {
                    //this.Session_CurrentSelectDevice_textBox.Text = GlobalVars.selectedDeviceName; //更新选定的设备到指示框
                    mbSession = (MessageBasedSession)ResourceManager.GetLocalManager().Open(GlobalVars.selectedDeviceName); //打开设备
                    GlobalVars.selectedAndConnectedInstrumentName=null;//清空已连接设备记录
                    GlobalVars.isSessionOpened = true;
                    Session_CurrentSelectDevice_textBox.Text = GlobalVars.selectedDeviceName;
                    Session_CurrentSelectDevice_textBox.BackColor = Color.Green;

                    GetInterfaceTypeAndSetItToGlobalVars();  //取得接口类型信息

                    //Serial
                    // 如何设置 Serial https://forums.ni.com/t5/Instrument-Control-GPIB-Serial/How-do-you-set-the-number-of-start-bits-for-a-VISA-serial/td-p/325520
                    if (GlobalVars.currentInterfaceType == "SERIAL")//SERIAL 接口 
                    {
                        SetSerialAttribute(ref mbSession);  //  设置SERIAL
                        GlobalVars.theCommandForQueryInstrumentsInfomation = "*IDN?" + GlobalVars.theTerminationCharactersOfRS232; // 更新询问仪器信息的命令    不要使用 *CLS;:*IDN?
                    }

                    // GPIB
                    if (GlobalVars.currentInterfaceType == "GPIB")//GPIB
                    { 
                        GlobalVars.theCommandForQueryInstrumentsInfomation = "*IDN?"; // 更新询问仪器信息的命令 不要使用 *CLS;:*IDN?

                    }

                    GetInstrumentName(mbSession);//更新选择的接口上的仪器名称
                     //更新要保存的文件的文件名
                    GlobalVars.SaveMeasureRealustToFile_FileName = GlobalVars.GenerateNewFileNameToSave();
                    GlobalVars.SaveMeasureRealustToFile_Whole = GlobalVars.GenerateNewFileNameWholeToSave();
                    //MessageBox.Show(GlobalVars.SaveMeasureRealustToFile_Whole); //调试输出                  

                }
                catch (InvalidCastException) //打开了不支持的设备
                {
                    MessageBox.Show("会话必须是基于消息的会话,请选择正确的设备\n Resource selected must be a message-based session!",
                                    "会话错误",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error // for Error 
                                     );
                }
                catch (Exception exp)//其他异常
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    this.TheElementreFresh();//刷新窗口
                }
            }
        }
		
        private void closeSessionButton_button_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(e.ToString());
            if (GlobalVars.isSessionOpened)
            {
                try
                {
                    mbSession.Dispose(); //释放资源
                }catch(ObjectDisposedException ex)
                {
                    MessageBox.Show(ex.Message, "mbSession.Dispose()异常发生", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                GlobalVars.isSessionOpened = false; //会话开启标志置 0
            }

            if (e.ToString() == "System.Windows.Forms.FormClosingEventArgs")
            {
                //关闭窗口时调用了该函数故在此判断 是否是  System.Windows.Forms.FormClosingEventArgs ,说明窗口正在关闭,不必更新显示了
            }
            else
            {
                Session_CurrentSelectDevice_textBox.Text = "未选定设备";
                Session_CurrentSelectDevice_textBox.BackColor = Color.Gray;
                this.TheElementreFresh();//刷新窗口

                this.Text = GlobalVars.theFormTitleWhenNoSessionOpened; //更新窗口标题
            }
        }


        /// <summary>
        /// 返回格式化后的字符串
        /// </summary>
        /// <param name="stringForFormat"></param>
        /// <returns></returns>
        public String CustomFormatString(String stringForFormat)
        {
            /*
             * 根据情况格式化字符串
             */

            if (GlobalVars.isUsingSubstringFormat)
            {
                stringForFormat = stringForFormat.Substring(GlobalVars.theSubstringStart, GlobalVars.theSubstringEnd);
            }
            if (GlobalVars.isUsingRegeExpression)
            {
                Match m;
                InvokeOnFormThread(() =>   //预防在循环测量时产生从不是创建该线程的窗口访问的错误
                {
                    GlobalVars.theRegexExpressionPattern = commandManipulate_Response_Format_RegexExpression_comboBox.Text; //获取正则表达式
                });
                try
                {
                    m = Regex.Match(stringForFormat, GlobalVars.theRegexExpressionPattern);
                    if (m.Success) stringForFormat = String.Empty; //如果第一次就匹配成功，那么先清空 stringForFormat 等待装入已匹配的串
                    while (m.Success)
                    {
                        stringForFormat += m.Groups[0].ToString() + " "; //组合找出的匹配项
                        m = m.NextMatch();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (GlobalVars.isUsingGNUCoreutils)
            {
                InvokeOnFormThread(() =>   //预防在循环测量时产生从不是创建该线程的窗口访问的错误
                {
                    GlobalVars.theGNUCoreutilsExpression = this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox.Text; //输入的命令
                });
                String ThePipe = null;
                if (String.IsNullOrEmpty(GlobalVars.theGNUCoreutilsExpression)) //没有输入表达式
                {
                    ThePipe = String.Empty;
                }
                else
                {
                    ThePipe = " | ";
                }
                GlobalVars.theGNUCoreutilsExpression = "echo " + stringForFormat + ThePipe + GlobalVars.theGNUCoreutilsExpression; //组合后命令 
                stringForFormat = ExecuteCommandSync(GlobalVars.theGNUCoreutilsExpression);
            }
            stringForFormat = stringForFormat.Replace("\n", String.Empty).Replace("\r", String.Empty); //替换文末结束符 便于处理
            return GlobalVars.CurrentMeasuretimes.ToString().PadLeft(5, ' ') + DateTime.Now.ToString("   yyyy-MM-dd###HH:mm:ss.fff    ") + stringForFormat; //为方便Excel绘图  yyyy-MM-dd 和 HH:mm:ss.fff 应该在一个单元  此处在 两者之间添加 三个# 方便后面格式化
        }

        /// <summary>
        ///   //更新输入的命令到全局变量
        /// </summary>
        private void SyncCommandInComboBoxToGlobalVars()
        {



            InvokeOnFormThread(() =>   //预防在循环测量时产生从不是创建该线程的窗口访问的错误
            {
                GlobalVars.commandTextToWrite = commandManipulate_Write_CommandStr_comboBox.Text;
                if (GlobalVars.currentInterfaceType == "GPIB")//什么也不做
                { 
                    #if DEBUG_Interface
                    MessageBox.Show(GlobalVars.currentInterfaceType, "当前使用硬件接口类型(GPIB)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    #endif
                } 
                if (GlobalVars.currentInterfaceType == "SERIAL")//SERIAL 接口 加终止符
                {
                    #if DEBUG_Interface 
                    MessageBox.Show(GlobalVars.currentInterfaceType, "当前使用硬件接口类型(Serial)", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
                    GlobalVars.commandTextToWrite = GlobalVars.commandTextToWrite + GlobalVars.theTerminationCharactersOfRS232;
                } 
            });

        }


        /// <summary>
        /// 发送命令的状态,更新到窗口底部显示
        /// </summary>
        /// <param name="result"></param>
        private void commandManipulate_Write_button_Click(object sender, EventArgs e)
        {
            try
            {
                SyncCommandInComboBoxToGlobalVars();  //更新输入的命令到全局变量
                GlobalVars.isMeasuring = true;
                mbSession.Write(GlobalVars.commandTextToWrite); //发送命令
                GlobalVars.isMeasuring = false;
            }
            catch (Exception exp) //显示异常
            {
                MessageBox.Show(exp.Message,
                                "异常发生",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error // for Error 
                        );
            }
            finally
            {
                TheElementreFresh();
            }
        }

        private void commandManipulate_Read_button_Click(object sender, EventArgs e)
        {
            try
            {
                ++GlobalVars.CurrentMeasuretimes;
                GlobalVars.isMeasuring = true;
                this.QueryOrRead(false);
                GlobalVars.isMeasuring = false;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message,
                                "异常发生",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error // for Error 
                                );
            }
            finally
            {
                TheElementreFresh();
            }

        }

        /// <summary>
        /// //线程间安全访问--访问不是该线程创建的窗口的控件--https://stackoverflow.com/questions/1110458/winforms-interthread-modification
        /// </summary>
        /// <param name="behavior"></param>
        private void InvokeOnFormThread(Action behavior)
        {
            if (IsHandleCreated && InvokeRequired)
            {
                Invoke(behavior);
            }
            else
            {
                behavior();
            }
        }

        /// <summary>
        /// 当命令不是双向命令却点击了读取按钮 显示警告
        /// </summary>
        private void ShowError_NONBiodirectionCommandButReadButtonClicked() {

            MessageBox.Show("只有有返回状态的命令才可以使用 发送&读取命令 例如 *CLS 命令只可以发送不可以读取,\n 请确认要发送的命令是双向命令且选中 \"双向命令\" 复选框",
                "错误",
                    MessageBoxButtons.OK,
                MessageBoxIcon.Error // for Error 
        ); }
        /// <summary>
        /// 根据传入的参数执行 Query 或 Read 命令
        /// </summary>
        /// <param name="ifQueryThenTrue"></param>
        public void QueryOrRead(bool ifQueryThenTrue)
        {
            try
            {
                if (ifQueryThenTrue) //query
                {
                    SyncCommandInComboBoxToGlobalVars();  //更新输入的命令到全局变量
                    GlobalVars.nativeResponseString = mbSession.Query(GlobalVars.commandTextToWrite); //发送后立即返回
                }
                else if (!ifQueryThenTrue) //only read 
                {
                    if (GlobalVars.isCommandBiodirection)
                    {

                        GlobalVars.nativeResponseString = mbSession.ReadString(); //只读取返回值
                    }else
                    {
                        ShowError_NONBiodirectionCommandButReadButtonClicked();
                    }
                }
                GlobalVars.nativeResponseString = GlobalVars.nativeResponseString.Replace("\n", String.Empty).Replace("\r", String.Empty); //替换文末结束符 便于处理
                GlobalVars.formatedResponseString = CustomFormatString(GlobalVars.nativeResponseString) + "\n"; //格式化后的字符串 加回换行符
                                                                                                                //commandManipulate_Response_richTextBox.Text += GlobalVars.formatedResponseString;//显示到 richtextbox
                SetrichTextBoxShowTextSafly(commandManipulate_Response_richTextBox, Regex.Replace(GlobalVars.formatedResponseString, @"###", "\t", RegexOptions.IgnoreCase));//显示到 richtextbox  //为方便Excel绘图   public String CustomFormatString(String stringForFormat) 中在处理时在 日期和时间之间加入了3个#,现在将#替换为Tab 方便显示对齐
            }
            catch (Exception exp) //显示异常
            {
                MessageBox.Show(exp.Message,
                                "异常发生",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error // for Error 
                        );
            }

        }
        private void commandManipulate_WriteAndRead_button_Click(object sender, EventArgs e)
        {
            if (GlobalVars.isCommandBiodirection) //只有双向命令才可以发送&读取
            {
                if (!GlobalVars.isSendcommandCyclically) //如果是单次发送
                {
                    ++GlobalVars.CurrentMeasuretimes;
                    GlobalVars.isMeasuring = true; //正在进行
                    this.commandManipulate_WriteAndRead_button.Enabled = false;//点击 发送&读取 时等待命令完成后再接受下次点击
                    this.QueryOrRead(true);
                    this.commandManipulate_WriteAndRead_button.Enabled = true; //点击 发送&读取 时等待命令完成后再接受下次点击
                    GlobalVars.isMeasuring = false; //没有进行
                }
                else if (GlobalVars.isSendcommandCyclically) //多次发送
                {

                    if (commandManipulate_WriteAndRead_button.Text == "发送&&读取") // 循环发送  按钮刚被按下
                    {
                        //记数器先清零               
                        GlobalVars.CurrentMeasuretimes = 0;

                        _worker = new BackgroundWorker();
                        _worker.WorkerSupportsCancellation = true;
                        _worker.DoWork += new DoWorkEventHandler((state, args) =>
                        {

                            while (true) //循环发送
                            {
                                if (_worker.CancellationPending)
                                {
                                    if (GlobalVars.isNewStreamWriterCreated || GlobalVars.isSaveToFile) //
                                    {
                                        GlobalVars.sw.Close();  //如果选择了保存到文件或者创建了文件流 停止测量时都关闭文件
                                        GlobalVars.isNewStreamWriterCreated = false;
                                        //File.Delete(GlobalVars.SaveMeasureRealustToFile_Whole); 
                                    }
                                    //记数器清零
                                    GlobalVars.CurrentMeasuretimes = 0;
                                    break; //终止循环
                                }
                                //--http://hovertree.com/hvtart/bjae/jbj59xoe.htm
                                //--http://www.cnblogs.com/infly123/archive/2013/05/18/3085872.html
                                if (GlobalVars.isSaveToFile && !GlobalVars.isNewStreamWriterCreated) //根据情况(文件流还没被创建 且 需要保存数据到文件)创建文件,在循环内判断是很低效的方式，但为了能随时在测量过程中保存到文件只能用这种方法
                                {
                                    //GlobalVars.sw = new StreamWriter(GlobalVars.SaveMeasureRealustToFile_Whole, true, Encoding.Default);//不指定编码或制定为其它编码 用Excel打开文件后不能正确排行
                                    bool isFileLocked = false; //先判断文件是否被锁定
                                    try
                                    {
                                        GlobalVars.sw = new StreamWriter(GlobalVars.SaveMeasureRealustToFile_Whole, true, Encoding.Default);//不指定编码或制定为其它编码 用Excel打开文件后不能正确排行
                                    }
                                    catch (IOException) //异常发生,文件被锁定
                                    {
                                        isFileLocked = true;
                                    }
                                    if (isFileLocked)
                                    {
                                        GlobalVars.isMeasuring = false;//解除测量标识

                                        //this.TheElementreFresh();
                                        //ControlModifyAndRefresh();
                                        //显示提示信息
                                        MessageBox.Show("其他程序正在使用文件" + GlobalVars.SaveMeasureRealustToFile_Whole + "\n不能打开文件,请关闭文件后再试",
                                                        "文件被占用",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error
                                                        );
                                        _worker.CancelAsync();
                                        break;//终止执行

                                    }
                                    GlobalVars.isNewStreamWriterCreated = !GlobalVars.isNewStreamWriterCreated;//文件流已创建
                                    System.Threading.Thread.Sleep(200); //等待200ms 完成文件创建
                                }
                                ++GlobalVars.CurrentMeasuretimes; //开始计数
                                QueryOrRead(true); //测量一次
                                if (GlobalVars.isSaveToFile && GlobalVars.isNewStreamWriterCreated) //如果确认要保存文件,保存文件
                                {
                                    //先判断扩展名 (csv 是逗号分隔符,text直接原始输入即可)
                                    String fileExtension = Path.GetExtension(GlobalVars.SaveMeasureRealustToFile_Whole);
                                    if (fileExtension == ".csv") //csv 文件
                                    {
                                        String StringTmp = Regex.Replace(GlobalVars.formatedResponseString, @"^\s+", String.Empty, RegexOptions.IgnoreCase);//删除行首空格
                                        StringTmp = Regex.Replace(StringTmp, @"\s+$", String.Empty, RegexOptions.IgnoreCase);//删除行尾空格
                                        StringTmp = Regex.Replace(StringTmp, @"\s+", ",", RegexOptions.IgnoreCase);//替换行间空格为逗号
                                        StringTmp = Regex.Replace(StringTmp, @"###", "  ", RegexOptions.IgnoreCase);//为方便Excel绘图   public String CustomFormatString(String stringForFormat) 中在处理时在 日期和时间之间加入了3个#,现在将#替换为空格
                                        GlobalVars.sw.WriteLine(StringTmp); 
                                    }
                                    else // txt 文件 或其他文件 直接写入
                                    {
                                        GlobalVars.sw.WriteLine(GlobalVars.formatedResponseString);
                                    }
                                }

                                if (GlobalVars.MeasureCountLimit != 0 && GlobalVars.CurrentMeasuretimes >= GlobalVars.MeasureCountLimit) //测量次数到达限制 调用自己 便 停止
                                {
                                    commandManipulate_WriteAndRead_button_Click(sender, e);
                                }

                                System.Threading.Thread.Sleep(((GlobalVars.commandSendAndWriteQueryInternal - 200) > 0) ? (GlobalVars.commandSendAndWriteQueryInternal - 200) : 0); //两次测量间隔  减去操作可能导致的延时 200ms 如果为负 置为零
                                
                            }
                        }
                        );
                        _worker.RunWorkerAsync();
                        //确认正常运转起来后再更改控件显示
                        this.commandManipulate_WriteAndRead_button.BackColor = Color.Red;
                        this.commandManipulate_WriteAndRead_button.Text = "停止";//变换按钮文字
                        this.commandManipulate_Response_richTextBox.Text = String.Empty; //清空显示文本窗口
                        GlobalVars.isMeasuring = true;
                        this.TheElementreFresh();

                    }
                    else if (commandManipulate_WriteAndRead_button.Text == "停止")
                    {
                        DialogResult result = DialogResult.Yes;

                        if (GlobalVars.MeasureCountLimit == 0)     //如果未限制测量次数,停止时要提示                       {
                        {
                            result = MessageBox.Show("是否停止测量?", "提示",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);                           

                        }
                         else                                     //如果限制了测量次数 停止时不要提示
                        {
                            result = DialogResult.Yes;

                        }
                        if (result == DialogResult.Yes)
                        {

                            _worker.CancelAsync();
                            this.commandManipulate_WriteAndRead_button.BackColor = Color.Green;       //要先设背景再 改文字不然测量次数达到限制时更新按钮 只会更改文字,背景该改不了
                            commandManipulate_WriteAndRead_button.Text = "发送&&读取";//变换按钮文字

                            //测量已停止
                            GlobalVars.isMeasuring = false;

                            this.TheElementreFresh(); //刷新控件
                        }


                    }
                }
            }
            else if (!GlobalVars.isCommandBiodirection)
            {
                ShowError_NONBiodirectionCommandButReadButtonClicked();

            }
        }

        private void commandManipulate_Write_isBiodirection_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckBoxApplyToVariables(this.commandManipulate_Write_isBiodirection_checkBox, ref GlobalVars.isCommandBiodirection);
            this.Debug_textBox.Text = GlobalVars.isCommandBiodirection.ToString(); //调试
            //刷新状态
            TheElementreFresh();
        }

        private void commandManipulate_Write_isCycle_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckBoxApplyToVariables(this.commandManipulate_Write_isCycle_checkBox, ref GlobalVars.isSendcommandCyclically, this.commandManipulate_Write_isCycle_Internal_textBox, false);
            this.Debug_textBox.Text = GlobalVars.isSendcommandCyclically.ToString(); //调试

            TheElementreFresh(); //刷新显示
        }

        private void SaveTheResponseToFile_Path_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // 
            {
                isTextBoxFileNameValid(ref SaveTheResponseToFile_Path_textBox, ref GlobalVars.SaveMeasureRealustToFile_Whole);
            }
        }

        private void SaveTheResponseToFile_Path_textBox_Leave(object sender, EventArgs e)
        {
            isTextBoxFileNameValid(ref SaveTheResponseToFile_Path_textBox, ref GlobalVars.SaveMeasureRealustToFile_Whole);
        }

        private void SaveTheResponseToFile_Path_BrowserButton_Click(object sender, EventArgs e)
        {
            GlobalVars.SaveMeasureRealustToFile_FileName = GlobalVars.GenerateNewFileNameToSave(); //每次点击浏览都重新生成一个文件名
            ChoseWhichFileToSave(); //选择文件保存位置
            this.SaveTheResponseToFile_Path_textBox.Text = GlobalVars.SaveMeasureRealustToFile_Whole;
        }

        private void SaveTheResponseToFile_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            isTextBoxFileNameValid(ref SaveTheResponseToFile_Path_textBox, ref GlobalVars.SaveMeasureRealustToFile_Whole);
            this.CheckBoxApplyToVariables(SaveTheResponseToFile_checkBox, ref GlobalVars.isSaveToFile, SaveTheResponseToFile_Path_textBox, false);
            this.Debug_textBox.Text = GlobalVars.isSaveToFile.ToString();
            TheElementreFresh();
        }

        private void commandManipulate_Write_isCycle_Internal_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // 
            {
                SyncTextBoxAndVariables(ref commandManipulate_Write_isCycle_Internal_textBox, ref GlobalVars.commandSendAndWriteQueryInternal, 0);
            }
            this.Debug_textBox.Text = GlobalVars.commandSendAndWriteQueryInternal.ToString();
        }

        private void commandManipulate_Write_isCycle_Internal_textBox_Leave(object sender, EventArgs e)
        {
            SyncTextBoxAndVariables(ref commandManipulate_Write_isCycle_Internal_textBox, ref GlobalVars.commandSendAndWriteQueryInternal, 0);
            this.Debug_textBox.Text = GlobalVars.commandSendAndWriteQueryInternal.ToString();
        }

        private void VISAGPIBCommonUnit_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show(e.ToString());
            //如果正在测量或richTextBox中有内容,关闭窗口时提示
            if (GlobalVars.isMeasuring || this.commandManipulate_Response_richTextBox.Text != String.Empty)
            {
                DialogResult result = MessageBox.Show("是否确认关闭?", "关闭窗口",
                          MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    closeSessionButton_button_Click(sender, e); //关闭窗口,释放资源
                }
                else
                {
                    e.Cancel = true; //取消关闭窗口
                }
            }
            else
            {
                closeSessionButton_button_Click(sender, e); //关闭窗口,释放资源
            }
        }

        private void commandManipulate_Response_richTextBox_TextChanged(object sender, EventArgs e)
        {


            //只在显示窗口最多保留2*GlobalVars.MaxLinesInRichTextBox-1个值,使用正则表达式将前面的和后面分割，只保留后面
            if ((GlobalVars.CurrentMeasuretimes > GlobalVars.MaxLinesInRichTextBox) && (GlobalVars.CurrentMeasuretimes % GlobalVars.MaxLinesInRichTextBox) == 0) // GlobalVars.MaxLinesInRichTextBox 行(Lines)为一组
            {

                String splitDelimeter = "\n[ ]*" + (GlobalVars.CurrentMeasuretimes - GlobalVars.MaxLinesInRichTextBox).ToString();
                String[] richTextBoxShowTextParts = Regex.Split(this.commandManipulate_Response_richTextBox.Text, splitDelimeter);
                if (richTextBoxShowTextParts.Length < 2) //如果用户在分割过程执行前执行了清空显示的操作,则原始字符串可能不满足分割条件而没有被分割为两部分,先检查该情况以免数组越界
                {
                    this.commandManipulate_Response_richTextBox.Text = richTextBoxShowTextParts.First(); //保持不变,取前一部分
                }else
                {
                    this.commandManipulate_Response_richTextBox.Text = (((GlobalVars.CurrentMeasuretimes / GlobalVars.MaxLinesInRichTextBox) - 1) * GlobalVars.MaxLinesInRichTextBox).ToString().PadLeft(5, ' ') + richTextBoxShowTextParts.Last(); //取后一部分 补上用作正则表达式分隔符的 GlobalVars.CurrentMeasuretimes.ToString()
                }
            }
            //---https://stackoverflow.com/questions/9416608/rich-text-box-scroll-to-the-bottom-when-new-data-is-written-to-it
            // set the current caret position to the end
            this.commandManipulate_Response_richTextBox.SelectionStart = this.commandManipulate_Response_richTextBox.Text.Length;
            // scroll it automatically
            this.commandManipulate_Response_richTextBox.ScrollToCaret();
        }

        private void commandManipulate_Response_Format_Substring_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckBoxApplyToVariables(this.commandManipulate_Response_Format_Substring_checkBox, ref GlobalVars.isUsingSubstringFormat, this.commandManipulate_Response_Format_Substring_StartIndex_textBox, false);
            this.CheckBoxApplyToVariables(this.commandManipulate_Response_Format_Substring_checkBox, ref GlobalVars.isUsingSubstringFormat, this.commandManipulate_Response_Format_Substring_EndIndex_textBox, false);
            //this.TheElementreFresh();
            this.Debug_textBox.Text = GlobalVars.isUsingSubstringFormat.ToString(); //调试
        }

        private void commandManipulate_Response_Format_RegexExpression_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckBoxApplyToVariables(this.commandManipulate_Response_Format_RegexExpression_checkBox, ref GlobalVars.isUsingRegeExpression, this.commandManipulate_Response_Format_RegexExpression_comboBox, false);
            //this.TheElementreFresh();
            this.Debug_textBox.Text = GlobalVars.isUsingRegeExpression.ToString(); //调试
        }

        private void commandManipulate_Response_Format_UseGNUCoreutils_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckBoxApplyToVariables(this.commandManipulate_Response_Format_UseGNUCoreutils_checkBox, ref GlobalVars.isUsingGNUCoreutils, this.commandManipulate_Response_Format_UseGNUCoreutils_comboBox, false);
            //this.TheElementreFresh();
            this.Debug_textBox.Text = GlobalVars.isUsingGNUCoreutils.ToString(); //调试
        }

        private void commandManipulate_Response_Format_Substring_StartIndex_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // 
            {

                try
                {
                    if (int.Parse(commandManipulate_Response_Format_Substring_StartIndex_textBox.Text) < GlobalVars.theSubstringEnd && !(int.Parse(commandManipulate_Response_Format_Substring_StartIndex_textBox.Text) < 0))
                    {
                        GlobalVars.theSubstringStart = int.Parse(commandManipulate_Response_Format_Substring_StartIndex_textBox.Text);
                    }
                    else
                    {
                        MessageBox.Show("截取字符串起始位置必须小于结束位置，且不能小于0", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                commandManipulate_Response_Format_Substring_StartIndex_textBox.Text = GlobalVars.theSubstringStart.ToString();
            }
        }

        private void commandManipulate_Response_Format_Substring_EndIndex_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // 
            {


                try
                {
                    if (int.Parse(commandManipulate_Response_Format_Substring_EndIndex_textBox.Text) > GlobalVars.theSubstringStart && !(int.Parse(commandManipulate_Response_Format_Substring_EndIndex_textBox.Text) < 0))
                    {
                        GlobalVars.theSubstringEnd = int.Parse(commandManipulate_Response_Format_Substring_EndIndex_textBox.Text);
                    }
                    else
                    {
                        MessageBox.Show("截取字符串起始位置必须小于结束位置,且不能小于0", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                commandManipulate_Response_Format_Substring_EndIndex_textBox.Text = GlobalVars.theSubstringEnd.ToString();
            }
        }

        private void commandManipulate_Response_Format_Substring_StartIndex_textBox_Leave(object sender, EventArgs e)
        {

            try
            {
                if (int.Parse(commandManipulate_Response_Format_Substring_StartIndex_textBox.Text) < GlobalVars.theSubstringEnd && !(int.Parse(commandManipulate_Response_Format_Substring_StartIndex_textBox.Text) < 0))
                {
                    GlobalVars.theSubstringStart = int.Parse(commandManipulate_Response_Format_Substring_StartIndex_textBox.Text);
                }
                else
                {
                    MessageBox.Show("截取字符串起始位置必须小于结束位置，且不能小于0", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            commandManipulate_Response_Format_Substring_StartIndex_textBox.Text = GlobalVars.theSubstringStart.ToString();
        }

        private void commandManipulate_Response_Format_Substring_EndIndex_textBox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(commandManipulate_Response_Format_Substring_EndIndex_textBox.Text) > GlobalVars.theSubstringStart && !(int.Parse(commandManipulate_Response_Format_Substring_EndIndex_textBox.Text) < 0))
                {
                    GlobalVars.theSubstringEnd = int.Parse(commandManipulate_Response_Format_Substring_EndIndex_textBox.Text);
                }
                else
                {
                    MessageBox.Show("截取字符串起始位置必须小于结束位置,且不能小于0", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            commandManipulate_Response_Format_Substring_EndIndex_textBox.Text = GlobalVars.theSubstringEnd.ToString();
        }

        private void commandManipulate_Response_richTextBox_Clean_button_Click(object sender, EventArgs e)
        {
            this.commandManipulate_Response_richTextBox.Text = String.Empty;
        }


        private void commandManipulate_Write_CommandStr_comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void commandManipulate_Write_CommandStr_comboBox_KeyDown(object sender, KeyEventArgs e)
        {
#if Debug_KeyPress
            MessageBox.Show(e.KeyCode.ToString(), "按键按下", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
            if (e.KeyCode == Keys.Enter) // Enter 按下
            {
                commandManipulate_Write_button_Click(sender, e);
            }

            //只有这个能工作 https://stackoverflow.com/questions/1107492/detect-combination-key-event
            if (e.Control && e.KeyCode == Keys.Enter) //  Ctrl + Enter  读取操作,本来应该是Ctrl ，但Ctrl有时候可能需要 Ctrl+C/V/X
            {
                commandManipulate_Read_button_Click(sender, e);
            }
        }

        private void commandManipulate_Write_CommandStr_comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ShowTipsWhen_ComboBoxItemDraw(e, GlobalVars.commandSetToolTip, commandManipulate_Write_CommandStr_comboBox, GlobalVars.commandSetCommandListToolTipsStrings);
        }

        private void commandManipulate_Write_CommandStr_comboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox_DropDownClosed_HiddenRelatedToolTip(GlobalVars.commandSetToolTip, commandManipulate_Write_CommandStr_comboBox);

        }

        private void commandManipulate_Response_Format_RegexExpression_comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ShowTipsWhen_ComboBoxItemDraw(e, GlobalVars.commandSetToolTip, commandManipulate_Response_Format_RegexExpression_comboBox, GlobalVars.commandSetRelativeRegexExpressionListToolTipsStrings);
        }

        private void commandManipulate_Response_Format_RegexExpression_comboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox_DropDownClosed_HiddenRelatedToolTip(GlobalVars.commandSetToolTip, commandManipulate_Response_Format_RegexExpression_comboBox);
        }

        private void commandManipulate_Response_Format_UseGNUCoreutils_comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            
           ShowTipsWhen_ComboBoxItemDraw(e, GlobalVars.commandSetToolTip, commandManipulate_Response_Format_UseGNUCoreutils_comboBox, GlobalVars.commandSetRelativeGNUCoreutilsExpressionListToolTipsStrings);

        }

        private void commandManipulate_Response_Format_UseGNUCoreutils_comboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox_DropDownClosed_HiddenRelatedToolTip(GlobalVars.commandSetToolTip, commandManipulate_Response_Format_UseGNUCoreutils_comboBox);
        }

        private void InstrumentsCommand_Set_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FreshComboBoxItems();
        }

        private void commandManipulate_Write_CommandStr_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据选择的仪器命令 是否是双向命令来调整 “双向命令” 复选框 和全局变量 GlobalVars.isCommandBiodirection ,同时刷新窗口

            String commandAttrString = GlobalVars.commandSetCommandListAttribute[commandManipulate_Write_CommandStr_comboBox.SelectedIndex].Replace("\n","").Replace("\r",""); //获取当前选定的命令 对应的属性值 
#if Debug_CommandListAttribute
            MessageBox.Show(commandAttrString, "当前仪器命令的属性", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
            if (commandAttrString == "1") //  除非被设置为了1 ，其他情况比如 0,NULL(没有写) 等均默认为单向命令
            {
                GlobalVars.isCommandBiodirection = true;
            }
            else
            {
                GlobalVars.isCommandBiodirection = false;
            }
            this.commandManipulate_Write_CommandStr_comboBox.Select(0, 0);
            //this.commandManipulate_Write_CommandStr_comboBox.Parent.Focus();
            //this.commandManipulate_Write_CommandStr_comboBox.SelectionStart = this.commandManipulate_Write_CommandStr_comboBox.SelectedText.ToString().Length;
            //刷新窗口控件
            TheElementreFresh(); 
        }

        private void MeasureCountLimit_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) // 
            {
                SyncTextBoxAndVariables(ref MeasureCountLimit_textBox, ref GlobalVars.MeasureCountLimit, 0);
            }
            this.Debug_textBox.Text = GlobalVars.MeasureCountLimit.ToString();
        }

        private void MeasureCountLimit_textBox_Leave(object sender, EventArgs e)
        {
            SyncTextBoxAndVariables(ref MeasureCountLimit_textBox, ref GlobalVars.MeasureCountLimit, 0);
             this.Debug_textBox.Text = GlobalVars.MeasureCountLimit.ToString();
        }

        /// <summary>
        /// 启用richTextBox 鼠标右键菜单  https://stackoverflow.com/questions/18966407/enable-copy-cut-past-window-in-a-rich-text-box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void commandManipulate_Response_richTextBox_CutAction(object sender, EventArgs e)
        {
            commandManipulate_Response_richTextBox.Cut();
        }

        /// <summary>
        /// 启用richTextBox 鼠标右键菜单  https://stackoverflow.com/questions/18966407/enable-copy-cut-past-window-in-a-rich-text-box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void commandManipulate_Response_richTextBox_CopyAction(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Rtf, commandManipulate_Response_richTextBox.SelectedRtf);
            Clipboard.Clear();
        }

        /// <summary>
        /// 启用richTextBox 鼠标右键菜单  https://stackoverflow.com/questions/18966407/enable-copy-cut-past-window-in-a-rich-text-box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void commandManipulate_Response_richTextBox_PasteAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                commandManipulate_Response_richTextBox.SelectedRtf
                    = Clipboard.GetData(DataFormats.Rtf).ToString();
            }
        }

        /// <summary>
        /// 启用richTextBox 鼠标右键菜单  https://stackoverflow.com/questions/18966407/enable-copy-cut-past-window-in-a-rich-text-box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandManipulate_Response_richTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {   //click event
                //MessageBox.Show("you got it!");
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                MenuItem menuItem = new MenuItem("剪切");
                menuItem.Click += new EventHandler(commandManipulate_Response_richTextBox_CutAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("复制");
                menuItem.Click += new EventHandler(commandManipulate_Response_richTextBox_CopyAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("粘贴");
                menuItem.Click += new EventHandler(commandManipulate_Response_richTextBox_PasteAction);
                contextMenu.MenuItems.Add(menuItem);

                commandManipulate_Response_richTextBox.ContextMenu = contextMenu;
            }
        }

        private void VISAGPIBCommonUnit_Resize(object sender, EventArgs e)
        {
            //Resize只会由于主窗口Resize导致，因此统一在主窗口resize 事件中进行
            SelectComboBoxTextLast();  //不要全部选中Combox的内容 (ComboBox 光标移动到最后去)
        }

    }

    public static class GlobalVars
    {
        public static String theFormTitleWhenNoSessionOpened = System.AppDomain.CurrentDomain.FriendlyName; //程序窗口名 会话未开启 -----https://stackoverflow.com/questions/616584/how-do-i-get-the-name-of-the-current-executable-in-c
        public static String theFormTitleWhenSessionOpened = null; //程序窗口名    会话开启
        public static bool isSendcommandCyclically = false; //是否循环发送命令
        public static bool isCommandBiodirection = true; //命令 是否是双向的 （即发送之后是否必须读回再发送下一个命令）

        public static String theCommandForQueryInstrumentsInfomation = null; // 询问仪器信息的命令
        public static String selectedDeviceName = null;//设备名
        public static String selectedAndConnectedInstrumentName = null; //仪器名
        public static bool isSessionOpened = false;//会话是否已经开启
        public static Int32 commandSendAndWriteQueryInternal = 300;//重复  发送&读取  命令的时间间隔 ms
        public static String commandTextToWrite = null; //发送 的命令内容

        public static bool isUsingSubstringFormat = false;// 使用Substring 截取字符串
        public static Int32 theSubstringStart = 0;// 使用Substring 截取字符串 起始位置
        public static Int32 theSubstringEnd = 1;// 使用Substring 截取字符串 终止位置

        public static bool isUsingRegeExpression = false; //// 使用正则表达式处理字符串
        public static String theRegexExpressionPattern = null;//正则表达式字符串

        public static bool isUsingGNUCoreutils = false; //使用GNU核心工具
        public static String theGNUCoreutilsExecutePath = AppDomain.CurrentDomain.BaseDirectory + "GNUCoreutils"; //使用GNU核心工具位置，要加入环境变量的
        public static String theGNUCoreutilsExpression = null; //使用GNU核心工具时表达式

        public static String formatedResponseString = null;// 返回的 已格式化的内容
        public static String nativeResponseString = null;//返回的  原始字符串

        public static String commandSetFilePath = AppDomain.CurrentDomain.BaseDirectory + "CommandSets";//命令集文件所在文件夹
        public static String commandSetFileName = null;//命令集文件名不含扩展名
        public static String commandSetFileExt = ".commandset";//命令集文件 扩展名
        public static String commandSetFileWhole = null;//命令集全路径

        public static List<String> commandSetCommandList = new List<String> { }; //控制命令集 命令
        public static List<String> commandSetCommandListAttribute = new List<String> { }; //控制命令集 命令 的属性 有返回信息 1 无返回信息0
        public static List<String> commandSetCommandListToolTipsStrings = new List<String> { }; //控制命令集 命令 相关提示说明

        public static ToolTip commandSetToolTip = new ToolTip(); //命令窗口的相关提示，同一时刻只会一个，只保持一个实例即可

        public static List<String> commandSetRelativeRegexExpressionList = new List<String> { }; //控制命令集 相关的格式化 正则表达式
        public static List<String> commandSetRelativeRegexExpressionListToolTipsStrings = new List<String> { }; //控制命令集 相关的格式化 正则表达式  相关提示说明

        public static List<String> commandSetRelativeGNUCoreutilsExpressionList = new List<String> { }; //控制命令集 相关的格式化  GNUCoreutils表达式
        public static List<String> commandSetRelativeGNUCoreutilsExpressionListToolTipsStrings = new List<String> { }; //控制命令集 相关的格式化  GNUCoreutils表达式 相关提示说明


        public static bool isSaveToFile = false;//是否保存测量结果到文件
        public static Int32 MaxLinesInRichTextBox = 100;//显示框中最多显示多少行  只在显示窗口最多保留2*GlobalVars.MaxLinesInRichTextBox-1 行
        public static Int32 CurrentMeasuretimes = 0; //第几次测量
        public static Int32 MeasureCountLimit = 0; //限制测量次数  0为不限制
        public static bool isMeasuring = false; //是否正在测量
        public static String SaveMeasureRealustToFile_Directory = AppDomain.CurrentDomain.BaseDirectory; //保存文件文件夹--https://stackoverflow.com/questions/97312/how-do-i-find-out-what-directory-my-console-app-is-running-in-with-c

        /// <summary>
        /// 生成保存测量结果的文件的文件名
        /// </summary>
        /// <returns></returns>
        public static String GenerateNewFileNameToSave()
        {
            return DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss_") + ((String.IsNullOrEmpty(GlobalVars.selectedAndConnectedInstrumentName)) ? ("NoInstrumentsConnected") : (GlobalVars.selectedAndConnectedInstrumentName)).Replace("\n", String.Empty).Replace("\r", String.Empty) + "_VISA_Data.csv";
        }
        public static String SaveMeasureRealustToFile_FileName = GlobalVars.GenerateNewFileNameToSave(); //保存文件文件名
                                                                                                         /// <summary>
                                                                                                         /// 生成保存测量结果的文件的完整路径
                                                                                                         /// </summary>
                                                                                                         /// <returns></returns>
        public static String GenerateNewFileNameWholeToSave()
        {
            return SaveMeasureRealustToFile_Directory + SaveMeasureRealustToFile_FileName;//完整包含路径文件名
        }
        public static String SaveMeasureRealustToFile_Whole = GenerateNewFileNameWholeToSave();//完整包含路径文件名
        public static StreamWriter sw = null;//保存文件时使用的文件流,不确定是否使用,默认初始化为null，等使用时再实例化
        public static bool isNewStreamWriterCreated = false;//是否已创建文件流

        public static String currentInterfaceType = null;  //接口类型  
        //对RS232接口,命令后必须加 LF  作为结束  0x0A
        public static String theTerminationCharactersOfRS232 = "\n";
        //RS232设置用

        public static Int32 SerialBaudRate = 19200; //Serial速率
        public static short SerialDataBits = 8; //数据位
        public static StopBitType SerialStopBits = (StopBitType)10; //停止位
        public static List<String>  SerialParityEnumList = new List<String>{ "NONE", "Odd", "Even", "Mark ", "Space" }; //校验方式列表
        public static Parity SerialParity=(Parity)0; //校验     NONE 0  Odd 1  Even 2 Mark 3 Space 4
        public static List<String> SerialFlowControlEnumList = new List<String>(new String[] { "NONE", "XON/XOFF"}); //FlowControl方式列表
        public static FlowControlTypes SerialFlowControl=(FlowControlTypes)0; ////Flow Control  NONE 0  XON/XOFF 1   使用 NI I/O Trace 监视   VISA Test Panel 设置时得到

    }
}
