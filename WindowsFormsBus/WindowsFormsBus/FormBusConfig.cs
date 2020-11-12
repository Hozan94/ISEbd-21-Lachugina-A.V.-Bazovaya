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
        private event Action<EasyBus> AddBus;
        public FormBusConfig()
        {
            InitializeComponent();
            this.panelBlue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelDarkViolet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelPink.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelLightBlue.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelGreen.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelYellow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelDarkOrange.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
            this.panelRed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelColor_MouseDown);
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

        /// <param name="ev"></param>
        public void AddEvent(Action<EasyBus> ev)
        {
            if (AddBus == null)
            {
                AddBus = new Action<EasyBus>(ev);
            }
            else
            {
                AddBus += ev;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddBus?.Invoke(bus);
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
            Color color = (sender as Panel).BackColor;
            (sender as Panel).DoDragDrop(color, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void labelMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Color)))
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
                bus.SetMainColor((Color)e.Data.GetData(typeof(Color)));
                DrawBus();         
        }

        private void labelDop_DragDrop(object sender, DragEventArgs e)
        {
            if (bus is BusGarm)
            {
                (bus as BusGarm).SetDopColor((Color)e.Data.GetData(typeof(Color)));
                DrawBus();
            }
        }
    }
}
