using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsBus
{
    public partial class FormBusConfig : Form
    {
        EasyBus bus = null;
        public event Action<EasyBus> addBus;
        public FormBusConfig()
        {
            InitializeComponent();
            buttonFalse.Click += (object sender, EventArgs e) => { Close(); };
        }
        private void DrawBus()
        {
            if (bus != null)
            {
                Bitmap bmp = new Bitmap(pictureBoxBusDraw.Width, pictureBoxBusDraw.Height);
                Graphics gr = Graphics.FromImage(bmp);
                bus.SetPosition(5, 15, pictureBoxBusDraw.Width, pictureBoxBusDraw.Height);
                bus.DrawTransport(gr);
                pictureBoxBusDraw.Image = bmp;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            addBus?.Invoke(bus);
            Close();
        }

        private void labelBus_MouseDown(object sender, MouseEventArgs e)
        {
            labelBus.DoDragDrop(labelBus.Text, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void labelBusGarm_MouseDown(object sender, MouseEventArgs e)
        {
            labelBusGarm.DoDragDrop(labelBusGarm.Text, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void panelBus_DragDrop(object sender, DragEventArgs e)
        {
            switch (e.Data.GetData(DataFormats.Text).ToString())
            {
                case "Автобус":
                    bus = new Bus((int)numericUpDownSpeed.Value, (int)numericUpDownWeight.Value, Color.White);
                    break;
                case "Автобус с гармошкой":
                    bus = new BusGarm((int)numericUpDownSpeed.Value, (int)numericUpDownWeight.Value, Color.White, Color.Black, checkBoxGarm.Checked, checkBoxOs.Checked);
                    break;
            }
            DrawBus();
        }

        private void panelBus_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void panelColor_MouseDown(object sender, MouseEventArgs e)
        {
            ((Panel)sender).DoDragDrop(((Panel)sender).BackColor.Name, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void labelMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text.ToString()))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void labelMain_DragDrop(object sender, DragEventArgs e)
        {
            if (bus != null)
            {
                bus.SetMainColor(Color.FromName(e.Data.GetData(DataFormats.Text).ToString()));
                DrawBus();
            }
        }

        private void labelDop_DragDrop(object sender, DragEventArgs e)
        {
            if (bus is BusGarm && bus != null)
            {
                (bus as BusGarm).SetDopColor(Color.FromName(e.Data.GetData(DataFormats.Text).ToString()));
                DrawBus();
            }
        }



    }
}
