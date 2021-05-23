using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoD2021Frontend
{
    public partial class Start : Form
    {
        public class InputInfo
        {
            public String kpp;
            public int livetime;
            public int okved;
            public String region;
            public int want_solutions;
        }

        public Start()
        {
            InitializeComponent();
        }

        private void Start_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Guest guestForm = new Guest();
            this.Hide();
            if (guestForm.ShowDialog() == DialogResult.OK)
            {
                InputInfo input = new InputInfo();
                input.kpp = guestForm.kppInput.Text;
                input.livetime = Convert.ToInt32(guestForm.livetimeInput.Text);
                input.okved = Convert.ToInt32(guestForm.okvedInput.Text);
                input.region = guestForm.regionInput.Text;
                input.want_solutions = 5;
                List<InputInfo> list = new List<InputInfo>();
                list.Add(input);
                String str = JsonConvert.SerializeObject(list);
                for (int i = 0; i < str.Length; i++)
                    str = str.Insert(i + 1, str[i] switch
                    {
                        '{' => "\n",
                        '[' => "\n",
                        ',' => "\n",
                        '}' => "\n",
                        ']' => "\n",
                        ':' => " ",
                        _ => ""
                    });
                File.WriteAllText("inputs.json", str);
                ProcessStartInfo psi = new ProcessStartInfo(@"WorkNN.exe");
                var proc = Process.Start(psi);
                while (!proc.HasExited) ;

                Result resForm = new Result();
                resForm.ShowDialog();
                this.Show();
                resForm.Close();
            }
            this.Show();
            guestForm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.Delete("inputs.json");
            File.Copy("inputs_test.json", "inputs.json");

            Result resForm = new Result();
            this.Hide();
            resForm.ShowDialog();
            this.Show();
            resForm.Close();
        }
    }
}
