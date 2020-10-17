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
    public partial class FormParking : Form
    {
        private readonly ParkingCollection parkingCollection;
        public FormParking()
        {
            InitializeComponent();
            parkingCollection = new ParkingCollection(pictureBoxParking.Width, pictureBoxParking.Height);
        }
        /// <summary>
        /// Заполнение listBoxLevels
        /// </summary>
        private void ReloadLevels()
        {
            int index = listBoxAutovoksal.SelectedIndex;
            listBoxAutovoksal.Items.Clear();
            for (int i = 0; i < parkingCollection.Keys.Count; i++)
            {
                listBoxAutovoksal.Items.Add(parkingCollection.Keys[i]);
            }
            if (listBoxAutovoksal.Items.Count > 0 && (index == -1 || index >= listBoxAutovoksal.Items.Count))
            {
                listBoxAutovoksal.SelectedIndex = 0;
            }
            else if (listBoxAutovoksal.Items.Count > 0 && index > -1 && index < listBoxAutovoksal.Items.Count)
            {
                listBoxAutovoksal.SelectedIndex = index;
            }
        }

        private void Draw()
        {
            if (listBoxAutovoksal.SelectedIndex > -1)
            {
                Bitmap bmp = new Bitmap(pictureBoxParking.Width, pictureBoxParking.Height);
                Graphics gr = Graphics.FromImage(bmp);
                parkingCollection[listBoxAutovoksal.SelectedItem.ToString()].Draw(gr);
                pictureBoxParking.Image = bmp;
            }
        }
        private void buttonAddAutovoksal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAutovoksalName.Text))
            {
                MessageBox.Show("Введите название парковки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            parkingCollection.AddAutovoksal(textBoxAutovoksalName.Text);
            ReloadLevels();
        }
       
        private void buttonDeleteAutovoksal_Click_1(object sender, EventArgs e)
        {
            if (listBoxAutovoksal.SelectedIndex > -1)
            {
                if (MessageBox.Show($"Удалить автовокзал {listBoxAutovoksal.SelectedItem.ToString()}?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    parkingCollection.DelAutovoksal(listBoxAutovoksal.SelectedItem.ToString());
                    ReloadLevels();
                    Draw();
                }
            }
        }
        
        private void buttonSetBus_Click(object sender, EventArgs e)
        {
            if (listBoxAutovoksal.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var bus = new Bus(100, 1000, dialog.Color);
                    if (parkingCollection[listBoxAutovoksal.SelectedItem.ToString()] + bus)
                    { 
                        Draw();
                    }
                else
                    {
                        MessageBox.Show("Парковка переполнена");
                    }
                }
            }
        }

        private void buttonSetGarmBus_Click_1(object sender, EventArgs e)
        {
            if (listBoxAutovoksal.SelectedIndex > -1)
            {
                ColorDialog dialog = new ColorDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ColorDialog dialogDop = new ColorDialog();
                    if (dialogDop.ShowDialog() == DialogResult.OK)
                    {
                        var bus = new BusGarm(100, 10000, dialog.Color, dialogDop.Color, true, true);
                        if (parkingCollection[listBoxAutovoksal.SelectedItem.ToString()] + bus)
                        {
                            Draw();
                        }
                        else
                        {
                            MessageBox.Show("Автовокзал переполнен");
                        }
                    }
                }
            }
        }
        private void buttonTakeBus_Click_1(object sender, EventArgs e)
        {
            if (listBoxAutovoksal.SelectedIndex > -1 && maskedTextBoxNumber.Text != "")
            {
                var bus = parkingCollection[listBoxAutovoksal.SelectedItem.ToString()] - Convert.ToInt32(maskedTextBoxNumber.Text);
                if (bus != null)
                {
                    FormBus form = new FormBus();
                    form.SetBus(bus);
                    form.ShowDialog();
                }
                Draw();
            }
        }

        private void listBoxAutovoksal_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw();
        }
    }
}
    
