using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LoD2021Frontend
{
    public partial class Result : Form
    {
        public Result()
        {
            InitializeComponent();
        }

        class kbkNote
        {
            public String department_code = "";
            public String description = "";
            public String kbk = "";
        }

        private void Result_Load_1(object sender, EventArgs e)
        {
            String kbksString = File.ReadAllText("given_kbks.json", Encoding.UTF8);
            List<kbkNote> kbksList = JsonConvert.DeserializeObject<List<kbkNote>>(kbksString);

            Dictionary<int, kbkNote> kbksDict = new Dictionary<int, kbkNote>();
            for (int i = 0; i < kbksList.Count; i++)
            {
                kbksDict.Add(i, kbksList[i]);
            }

            Dictionary<int, double> kbksChance = new Dictionary<int, double>();

            foreach (String str in File.ReadAllLines("Out.txt"))
            {
                try
                {
                    String[] pair = str.Split(' ');
                    pair[1] = pair[1].Replace('.',',');
                    kbksChance.Add(Convert.ToInt32(pair[0]), Convert.ToDouble(pair[1]));
                }
                catch (Exception ex) { }
            }

            int j = 0;
            foreach (var pair in kbksChance)
            {
                try
                {
                    Panel kbkPanel = new Panel();
                    Label depLabelText = new Label();
                    Label depLabelValue = new Label();
                    Label descriptLabelText = new Label();
                    Label descriptLabelValue = new Label();
                    Label kbkLabelText = new Label();
                    Label kbkLabelValue = new Label();

                    int panelHeight = 180;
                    kbkPanel.Size = new Size(kbksPanel.Size.Width - 40, panelHeight);
                    kbkPanel.Location = new Point(10, j * (panelHeight + 10) + 10);
                    kbkPanel.BorderStyle = BorderStyle.FixedSingle;

                    depLabelText.Text = "Код департамента: ";
                    depLabelText.AutoSize = false;
                    depLabelText.Size = new Size(kbksPanel.Size.Width, panelHeight / 7);
                    depLabelText.Location = new Point(0, 0 * (panelHeight / 7));
                    depLabelText.Font = new Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                    kbkPanel.Controls.Add(depLabelText);

                    depLabelValue.Text = kbksDict[pair.Key].department_code;
                    depLabelValue.AutoSize = false;
                    depLabelValue.Size = new Size(kbksPanel.Size.Width, panelHeight / 7);
                    depLabelValue.Location = new Point(0, 1 * (panelHeight / 7));
                    depLabelValue.Font = new Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                    kbkPanel.Controls.Add(depLabelValue);

                    descriptLabelText.Text = "Описание меры поддержки:";
                    descriptLabelText.AutoSize = false;
                    descriptLabelText.Size = new Size(kbksPanel.Size.Width, panelHeight / 7);
                    descriptLabelText.Location = new Point(0, 2 * (panelHeight / 7));
                    descriptLabelText.Font = new Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                    kbkPanel.Controls.Add(descriptLabelText);

                    descriptLabelValue.Text = kbksDict[pair.Key].description;
                    descriptLabelValue.AutoSize = false;
                    descriptLabelValue.Size = new Size(kbksPanel.Size.Width, panelHeight / 7 * 2);
                    descriptLabelValue.Location = new Point(0, 3 * (panelHeight / 7));
                    descriptLabelValue.Font = new Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                    kbkPanel.Controls.Add(descriptLabelValue);

                    kbkLabelText.Text = "КБК меры поддержки:";
                    kbkLabelText.AutoSize = false;
                    kbkLabelText.Size = new Size(kbksPanel.Size.Width, panelHeight / 7);
                    kbkLabelText.Location = new Point(0, 5 * (panelHeight / 7));
                    kbkLabelText.Font = new Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                    kbkPanel.Controls.Add(kbkLabelText);

                    kbkLabelValue.Text = kbksDict[pair.Key].kbk;
                    kbkLabelValue.AutoSize = false;
                    kbkLabelValue.Size = new Size(kbksPanel.Size.Width, panelHeight / 7);
                    kbkLabelValue.Location = new Point(0, 6 * (panelHeight / 7));
                    kbkLabelValue.Font = new Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                    kbkPanel.Controls.Add(kbkLabelValue);

                    kbksPanel.Controls.Add(kbkPanel);
                    j++;
                }
                catch (Exception) { }
            }
        }
    }
}
