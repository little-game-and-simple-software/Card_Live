using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Card_Live
{

    public partial class Form1 : Form
    {

        #region 声明字段
        int blood = 100; //生命
        int water = 0; //缺水
        int hungry = 0; //饥饿
        #region 临时字段
        int now_blood = 100; //生命
        int now_water = 0; //缺水
        int now_hungry = 0; //饥饿
        #endregion
        int money; //金钱
        int day = 1; //生存时间
        List<int> cards = new List<int>(); //六类卡
        List<int> uses = new List<int>(); //食物和水
        #endregion

        public Form1() => InitializeComponent();

        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化
            #region 初始化
            this.button7.Visible = false;
            for (int i = 0; i < 6; i++)
            {
                cards.Add(0);
            }
            listBox1.Items.Add("你的生活开始啦!");
            #endregion
            #region 拓展包检测
            if(File.Exists(Application.StartupPath + "\\Farm.exe"))
            {
                this.button7.Visible = true;

            }
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region 抽卡
            if (money >= 0)
            {
                //减少金钱
                Random rd = new Random();
                money = money - day * rd.Next(1, 6);
                //获取随机卡牌
                GetRanCard gc = new GetRanCard();
                int result = gc.Get();
                //解析
                ParsingCard pc = new ParsingCard();
                cards = pc.Parsing(cards, result);
                #region 回显
                switch (result)
                {
                    case 1:
                        listBox1.Items.Add("抽卡事件:抽到了甲等卡1张。");
                        break;
                    case 2:
                    case 3:
                        listBox1.Items.Add("抽卡事件:抽到了乙等卡1张。");
                        break;
                    case 4:
                    case 5:
                    case 6:
                        listBox1.Items.Add("抽卡事件:抽到了丙等卡1张。");
                        break;
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        listBox1.Items.Add("抽卡事件:抽到了丁等卡1张。");
                        break;
                }
                #endregion
            }
            else
            {
                listBox1.Items.Add("提示事件:金钱不够了，去往下一年吧");
            }
            #endregion

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            #region 回显数据
            //实时回显
            label2.Text = blood.ToString();
            label3.Text = hungry.ToString();
            label5.Text = water.ToString();
            label7.Text = cards[0].ToString();
            label9.Text = cards[1].ToString();
            label11.Text = cards[2].ToString();
            label13.Text = cards[3].ToString();
            label19.Text = cards[4].ToString();
            label21.Text = cards[5].ToString();
            label15.Text = (day+2020).ToString();
            label17.Text = money.ToString();
            #endregion
            #region 自动滚动
            this.listBox1.SelectedIndex = this.listBox1.Items.Count - 1;
            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            day++;
            #region 减少属性
            UpdateAttributes ua = new UpdateAttributes();
            List<int> results = ua.Update(blood, water, hungry, day);
            blood = results[0];
            water = results[1];
            hungry = results[2];
            listBox1.Items.Add("自然事件:饥饿度增加了" + (hungry - now_hungry).ToString() + "点。");
            listBox1.Items.Add("自然事件:脱水度增加了" + (water - now_water).ToString() + "点。");
            now_blood = results[0];
            now_water = results[1];
            now_hungry = results[2];
            #endregion
            #region 加钱
            GiveMoney gm = new GiveMoney();
            int give = gm.give(day);
            money = money + give;
            listBox1.Items.Add("自然事件:获得了" + give + "元。");
            listBox1.Items.Add("--------------------------");
            listBox1.Items.Add("公元" + (2020+day).ToString() + "年开始!");
            #endregion
            #region 判断挂掉和属性
            if (blood <= 0)
            {
                listBox1.Items.Add(">你去世了,享年" + day.ToString() + "岁。<");
                button1.Enabled = false;
                button2.Enabled = false;
            }
            if (water > 20)
            {
                listBox1.Items.Add("提醒:你的缺水度已经大于20％了，" + "总共减血已达"+ (100 - ua.blood_now_inwater).ToString()+"点，请注意补水！");
            }
            if (hungry > 20)
            {
                listBox1.Items.Add("提醒:你的饥饿度已经大于20％了，总共减血已达" + (100-ua.blood_now_inhungry).ToString() +"点，请注意补充食物！<");
            }
            #endregion
            #region 存储生存天数
            StreamWriter sw = new StreamWriter(Application.StartupPath + "temp\\day.txt");
            sw.Write(day.ToString());
            sw.Flush();
            sw.Close();
            #endregion
        }

        private void button3_Click(object sender, EventArgs e)
        {
            #region 兑换食物
            ExangeCard ec = new ExangeCard();
            List<int> bak = new List<int>();
            bak = cards;
            cards = ec.Foods(cards);
            if(cards.Count != 0)
            {
                //兑换成功
                //分配cards[4]为食物卡
                cards[4] = cards[4] + 1;
                listBox1.Items.Add("兑换事件:成功兑换了一份食物");

            }
            else
            {
                //兑换失败，恢复备份
                cards = bak;
                MessageBox.Show("兑换失败!须最少|1张甲卡|或3张乙卡|或5张丙卡|或8张丁卡","食物兑换失败",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            #endregion
        }

        private void button4_Click(object sender, EventArgs e)
        {
            #region 吃饭
            if (cards[4] > 0)
            {
                Random rd = new Random();
                int ran = rd.Next(5, 40);
                hungry = hungry - ran;
                listBox1.Items.Add("日常事件:我吃饭了，饥饿度减少了" + ran.ToString() + "点。");
                //记得扣除卡!
                cards[4] = cards[4] - 1;
            }
            else
            {
                MessageBox.Show("没有饭你怎么吃饭？吃西北风吗？","余粮不足",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            #endregion
        }

        private void button6_Click(object sender, EventArgs e)
        {
            #region 兑换水
            ExangeCard ec = new ExangeCard();
            List<int> bak = new List<int>();
            bak = cards;
            cards = ec.Water(cards);
            if (cards.Count != 0)
            {
                //兑换成功
                cards[5] = cards[5] + 1;
                listBox1.Items.Add("兑换事件:成功兑换了一杯水");
            }
            else
            {
                //兑换失败，恢复备份
                cards = bak;
                MessageBox.Show("兑换失败!须最少|1张甲卡|或2张乙卡|或3张丙卡|或5张丁卡", "水兑换失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion 
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            #region 喝水
            if (cards[5] > 0)
            {
                Random rd = new Random();
                int ran = rd.Next(5, 40);
                water = water - ran;
                listBox1.Items.Add("日常事件:我喝水了，缺水度减少了" + ran.ToString() + "点。");
                cards[5] = cards[5] - 1;
            }
            else
            {
                MessageBox.Show("没有水你怎么喝水？喝尿吗？", "水不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + "\\Farm.exe");
        }
    }
}
