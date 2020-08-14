using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DArrays
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public List<string> dagLijst = new List<string>() { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag" };
        private string[,] mijnAgenda = new string[24 ,7];
        public int uur, dag;
        public void lb_Click(object sender,EventArgs e)
        {
            string[] separators = { ","};
            string value = (sender as Label).Name;
            string[] split = value.Split(separators,StringSplitOptions.RemoveEmptyEntries);
            uur = Convert.ToInt32(split[0]);
            dag = Convert.ToInt32(split[1]);
            Zichtbaar();
        }
        private void btnToevoegen_Click(object sender, EventArgs e)
        {
            mijnAgenda[uur,dag] = tbToevoegen.Text;
            Label actiefLabel = this.Controls.Find($"{uur},{dag}",true).FirstOrDefault() as Label;
            actiefLabel.Text = mijnAgenda[uur, dag];
            Onzichtbaar();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            mijnAgenda[17,2 ] = "test";
            VulAgenda();
            Onzichtbaar();
        }
        private void Onzichtbaar() 
        {
            lblMessage.Location = new Point(10, 10);
            lblMessage.Text = "Selecteer een uur en dag.";
            tbToevoegen.Clear();
            btnToevoegen.Hide();
            tbToevoegen.Hide();
            btnToevoegen.Enabled = false;
            tbToevoegen.Enabled = false;
        }
        private void Zichtbaar() 
        {
            lblMessage.Text = ($"Geef een nieuwe taak voor {dagLijst[dag]} om {uur.ToString()} uur: ");
            btnToevoegen.Show();
            tbToevoegen.Show();
            btnToevoegen.Enabled = true;
            tbToevoegen.Enabled = true;
            tbToevoegen.Location = new Point(10 + lblMessage.Width+10,10);
            btnToevoegen.Location = new Point(10 + lblMessage.Width + 10 + tbToevoegen.Width + 10, 10);
        }
        private void VulAgenda()
        {
            for (int i = 0; i < 24; i++)
            {
                var newTextBox = new TextBox();
                newTextBox.Width = 70;
                newTextBox.Height = 20;
                newTextBox.Enabled = false;
                newTextBox.Location = new Point(50, 50 + (i+1) * newTextBox.Height);
                newTextBox.Text = $"{i.ToString()}:00";
                Controls.Add(newTextBox);
            }

            for (int i = 0; i < 7; i++)
            {
                var newTextBox = new TextBox();
                newTextBox.Width = 70;
                newTextBox.Height = 20;
                newTextBox.Enabled = false;
                newTextBox.Location = new Point(50 + (i + 1) * newTextBox.Width,50);
                newTextBox.Text = dagLijst[i];
                Controls.Add(newTextBox);
            }
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    var newLabel = new Label();
                    newLabel.Width = 70;
                    newLabel.Height = 20;
                    newLabel.Location = new Point(120 + newLabel.Width * i, 70 + newLabel.Height * j);
                    newLabel.Text = mijnAgenda[j, i];
                    newLabel.Name = $"{j},{i}";
                    newLabel.IsAccessible = false;
                    newLabel.BorderStyle = BorderStyle.FixedSingle;
                    newLabel.Click += new EventHandler(lb_Click);
                    Controls.Add(newLabel);
                }
            }
        }
    }
}
