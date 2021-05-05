using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Add
{
    public partial  class Form1 : Form
    {
        public List<string> tools = new List<string>();
        public int count;
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="spath">插件文档</param>
        /// <param name="cpath">计数文档</param>
        public void Write(string spath,string cpath) {

            StreamReader sr = new StreamReader(cpath);
            var counta = sr.ReadToEnd();
            count = Convert.ToInt32(counta);
            sr.Close();
            StreamReader sr2 = new StreamReader(spath);
            for (int i = 0; i < Convert.ToInt32(count); i++)
            {
                tools.Add(sr2.ReadLine());
            }
        }
        public int Getcount() {
            return count;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //获取路径
            StreamReader ready = new StreamReader("D:\\CLPATH.txt");
            string path = ready.ReadToEnd();
            ready.Close();
            //写
            Write(path + "API\\CONFIG\\Start.txt", path + "API\\CONFIG\\Count.txt");
            int i = -1;
            foreach(var item in tools)
            {
                i++;
                if (tools[i] == string.Empty)
                {
                    tools.RemoveAt(i);
                }
            }
            foreach (var item in tools)
            {
                MessageBox.Show(item);
            }
           
        }

        public static List<string> GetReturn() {

            Form1 f1 = new Form1();
            return f1.tools;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
