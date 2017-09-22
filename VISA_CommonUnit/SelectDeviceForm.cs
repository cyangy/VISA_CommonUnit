//#define Debug_DeviceName

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

namespace VISA_CommonUnit
{
    public partial class SelectDeviceForm : Form
    {
        public SelectDeviceForm()
        {
            InitializeComponent();
        }
                

        private void SelectDevice_Load(object sender, EventArgs e)
        {
            try
            {
                //获取所有接口 也可以使用正则表达式过滤需要的接口//参考NI VISA 帮助文档 Finding VISA Resources Using Regular Expressions
                String[] resources = ResourceManager.GetLocalManager().FindResources("?*");//GPIB[0-9]::[0-9]*::?*
                                                                                         //添加接口到接口列表
                foreach (String res in resources)
                    {
                        this.AvilableDeviceList_listBox.Items.Add(res);
                    }
            }catch(Exception exp)
            {
                this.AvilableDeviceList_listBox.Items.Add("未发现任何设备");
                this.AvilableDeviceList_listBox.Enabled = false;
                MessageBox.Show(exp.Message,
                    "异常发生",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
            //availableResourcesListBox.Items.Add("TCPIP[board]::host address[::LAN device name][::INSTR]");
            //availableResourcesListBox.Items.Add("TCPIP[board]::host address::port::SOCKET");
        }
        // 双击直接选定设备
        private void AvilableDeviceList_listBox_DoubleClick(object sender, EventArgs e)
        {
            String selectedString = ((String)this.AvilableDeviceList_listBox.SelectedItem);
            if (String.IsNullOrEmpty(selectedString))
            {
                MessageBox.Show("未选定任何设备","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
            {
                GlobalVars.selectedDeviceName = selectedString;
#if Debug_DeviceName
            MessageBox.Show(GlobalVars.selectedDeviceName,
                    "当前选定设备",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information // for Error 
                     );
#endif
                this.DialogResult = DialogResult.OK;//设备已选定
                this.Close();
            }
        }

        private void AvilableDeviceList_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedString = (String)this.AvilableDeviceList_listBox.SelectedItem;
            this.SelectedDevice_textBox.Text = selectedString;
        }

        private void SelectedDevice_OK_button_Click(object sender, EventArgs e)
        {
            String selectedString = SelectedDevice_textBox.Text;
            //先判断输入框中是否有内容
            if (String.IsNullOrEmpty(selectedString)) //如果输入框中有内容 则优先使用输入框中的
            {
                selectedString = ((String)this.AvilableDeviceList_listBox.SelectedItem);
            }            
            if (String.IsNullOrEmpty(selectedString))
            {
                MessageBox.Show("未选定任何设备", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                GlobalVars.selectedDeviceName = selectedString;
#if Debug_DeviceName
            MessageBox.Show(GlobalVars.selectedDeviceName,
                    "当前选定设备",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information // for Error 
                     );
#endif
                this.DialogResult = DialogResult.OK;//设备已选定
                this.Close();
            }

        }

        private void SelectedDevice_Cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //要使FORM 响应 KeyDown ,Form 的KeyPreview 属性要设为True
        private void SelectDevice_KeyDown(object sender, KeyEventArgs e)
        {
#if Debug_KeyPress
            MessageBox.Show(e.KeyCode.ToString(), "按键按下", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
            if (e.KeyCode == Keys.Enter) // Enter 按下
            {
                SelectedDevice_OK_button_Click(sender, e);
            }

        }

        
    }
}
