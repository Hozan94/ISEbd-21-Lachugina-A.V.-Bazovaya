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
    public partial class FormAutovoksal : Form
    {
        private readonly AutovoksalCollection autovoksalCollection;
        public FormAutovoksal()
        {
            InitializeComponent();
            autovoksalCollection = new AutovoksalCollection(pictureBoxAutovoksal.Width, pictureBoxAutovoksal.Height);
        }
        /// <summary>
        /// Заполнение listBoxLevels
        /// </summary>
        private void ReloadLevels()
        {
            int index = listBoxAutovoksal.SelectedIndex;
            listBoxAutovoksal.Items.Clear();
            for (int i = 0; i < autovoksalCollection.Keys.Count; i++)
            {
                listBoxAutovoksal.Items.Add(autovoksalCollection.Keys[i]);
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
                Bitmap bmp = new Bitmap(pictureBoxAutovoksal.Width, pictureBoxAutovoksal.Height);
                Graphics gr = Graphics.FromImage(bmp);
                autovoksalCollection[listBoxAutovoksal.SelectedItem.ToString()].Draw(gr);
                pictureBoxAutovoksal.Image = bmp;
            }
        }
        private void buttonAddAutovoksal_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAutovoksalName.Text))
            {
                MessageBox.Show("Введите название парковки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            autovoksalCollection.AddAutovoksal(textBoxAutovoksalName.Text);
            ReloadLevels();
        }

        private void buttonDeleteAutovoksal_Click_1(object sender, EventArgs e)
        {
            if (listBoxAutovoksal.SelectedIndex > -1)
            {
                if (MessageBox.Show($"Удалить автовокзал {listBoxAutovoksal.SelectedItem.ToString()}?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    autovoksalCollection.DelAutovoksal(listBoxAutovoksal.SelectedItem.ToString());
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
                    if (autovoksalCollection[listBoxAutovoksal.SelectedItem.ToString()] + bus)
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
                        if (autovoksalCollection[listBoxAutovoksal.SelectedItem.ToString()] + bus)
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
                var bus = autovoksalCollection[listBoxAutovoksal.SelectedItem.ToString()] - Convert.ToInt32(maskedTextBoxNumber.Text);
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
        private void AddBus(EasyBus bus)
        {
            if (bus != null && listBoxAutovoksal.SelectedIndex > -1)
            {
                if ((autovoksalCollection[listBoxAutovoksal.SelectedItem.ToString()]) + bus)
                {
                    Draw();
                }
                else
                {
                    MessageBox.Show("Автобус не удалось поставить");
                }
            }
        }

        private void buttonAddBus_Click_1(object sender, EventArgs e)
        {
            var formBusConfig = new FormBusConfig();
            formBusConfig.AddEvent(AddBus);
            formBusConfig.Show();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (autovoksalCollection.SaveData(saveFileDialog.FileName))
                {
                    MessageBox.Show("Сохранение прошло успешно", "Результат",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Не сохранилось", "Результат",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (autovoksalCollection.LoadData(openFileDialog.FileName))
                {
                    MessageBox.Show("Загрузили", "Результат", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);
                    ReloadLevels();
                    Draw();
                }
                else
                {
                    MessageBox.Show("Не загрузили", "Результат", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
    }
}
    
