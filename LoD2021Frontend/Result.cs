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
                    FlowLayoutPanel kbkPanel = new FlowLayoutPanel();
                    kbkPanelInit(kbkPanel, kbksDict, pair.Key);
                    if (j == 0)
                    {
                        Label separator = new Label();
                        separator.Text = "Меры, которые также могут быть полезны: ";
                        separator.AutoSize = true;
                        separator.Font = new Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                        kbksPanel.Controls.Add(separator);
                        kbkPanel.Paint += panel_Paint;
                    }
                    j++;
                }
                catch (Exception) { }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        void kbkPanelInit(FlowLayoutPanel kbkPanel, Dictionary<int, kbkNote> kbksDict, int id)
        {
            Label depLabelText = new Label();
            Label depLabelValue = new Label();
            Label descriptLabelText = new Label();
            RichTextBox descriptLabelValue = new RichTextBox();
            Label kbkLabelText = new Label();
            Label kbkLabelValue = new Label();

            int parts = 8;

            kbkPanel.FlowDirection = FlowDirection.TopDown;
            kbkPanel.AutoSize = true;
            kbkPanel.BorderStyle = BorderStyle.FixedSingle;
            kbkPanel.WrapContents = false;
            kbkPanel.Padding = new Padding(10);

            depLabelText.Text = "Код департамента: ";
            depLabelText.AutoSize = true;
            //depLabelText.Size = new Size(kbksPanel.Size.Width - 100, panelHeight / parts);
            //depLabelText.Location = new Point(5, 5 + 0 * (panelHeight / parts));
            depLabelText.Font = new Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            kbkPanel.Controls.Add(depLabelText);

            depLabelValue.Text = kbksDict[id].department_code;
            depLabelValue.AutoSize = true;
            //depLabelValue.Size = new Size(kbksPanel.Size.Width - 50, panelHeight / parts);
            //depLabelValue.Location = new Point(5, 5 + 1 * (panelHeight / parts));
            depLabelValue.Font = new Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            kbkPanel.Controls.Add(depLabelValue);

            descriptLabelText.Text = "Описание меры поддержки:";
            descriptLabelText.AutoSize = true;
            //descriptLabelText.Size = new Size(kbksPanel.Size.Width - 15, panelHeight / parts );
            //descriptLabelText.Location = new Point(5, 5 + 2 * (panelHeight / parts));
            descriptLabelText.Font = new Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            kbkPanel.Controls.Add(descriptLabelText);

            descriptLabelValue.Text = kbksDict[id].description;
            descriptLabelValue.AutoSize = false;
            descriptLabelValue.ReadOnly = true;
            descriptLabelValue.ScrollBars = RichTextBoxScrollBars.Vertical;
            descriptLabelValue.Size = new Size(700, 80 );
            //descriptLabelValue.Location = new Point(5, 5 + 3 * (panelHeight / parts));
            descriptLabelValue.Font = new Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            kbkPanel.Controls.Add(descriptLabelValue);

            kbkLabelText.Text = "КБК меры поддержки:";
            //kbkLabelText.AutoSize = false;
            //kbkLabelText.Size = new Size(kbksPanel.Size.Width - 15, panelHeight / parts);
            //kbkLabelText.Location = new Point(5, 5 + 6 * (panelHeight / parts));
            kbkLabelText.Font = new Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            kbkPanel.Controls.Add(kbkLabelText);

            kbkLabelValue.Text = kbksDict[id].kbk;
            kbkLabelValue.AutoSize = true;
            //kbkLabelValue.Size = new Size(kbksPanel.Size.Width - 15, panelHeight / parts);
            //kbkLabelValue.Location = new Point(5, 5 + 7 * (panelHeight / parts));
            kbkLabelValue.Font = new Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            kbkPanel.Controls.Add(kbkLabelValue);

            kbksPanel.Controls.Add(kbkPanel);
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ((Panel)sender).ClientRectangle,
            Color.Red, 2, ButtonBorderStyle.Solid, // left
            Color.Red, 2, ButtonBorderStyle.Solid, // top
            Color.Red, 2, ButtonBorderStyle.Solid, // right
            Color.Red, 2, ButtonBorderStyle.Solid);// bottom
        }
    }
}
