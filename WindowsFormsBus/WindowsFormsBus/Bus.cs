using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsBus
{
    /// <summary>
    /// Класс отрисовки автобуса
    /// </summary>
    public class Bus : Busik
    {
        /// <summary>
        /// Дополнительный цвет
        /// </summary>
        public Color DopColor { private set; get; }

        /// <summary>
        /// Признак наличия гармошки
        /// </summary>
        public bool Garmoshka { private set; get; }

        /// <summary>
        /// Признак наличия третьей оси колес
        /// </summary>
        public bool ThirdOs { private set; get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="maxSpeed">Максимальная скорость</param>
        /// <param name="weight">Вес автомобиля</param>
        /// <param name="mainColor">Основной цвет кузова</param>
        /// <param name="dopColor">Дополнительный цвет</param>
        /// <param name="garmoshka">Признак наличия гармошки</param>
        /// <param name="thirdOs">Признак наличия третьей оси</param>
        public Bus(int maxSpeed, float weight, Color mainColor, Color dopColor, bool garmoshka, bool thirdOs) : base(maxSpeed, weight, mainColor, 194, 68)
        {
            DopColor = dopColor;
            Garmoshka = garmoshka;
            ThirdOs = thirdOs;
        }

        public override void DrawTransport(Graphics g)
        {
            Brush brDop = new SolidBrush(DopColor);
            Pen os = new Pen(Color.Black, 4);
            Brush white = new SolidBrush(Color.White);
            Brush fon = new SolidBrush(Color.FromArgb(240, 240, 240));
            base.DrawTransport(g);
            //отрисуем третью ось колес
            if (ThirdOs)
            {
                g.DrawEllipse(os, _startPosX + 78, _startPosY + 50, 17, 17);
                g.FillEllipse(white, _startPosX + 80, _startPosY + 52, 13, 13);
            }

            //отрисуем гармошку
            if (Garmoshka)
            {
                g.FillRectangle(brDop, _startPosX + 100, _startPosY + 8, 19, 47);
                g.FillRectangle(fon, _startPosX + 100, _startPosY + 4, 19, 4);
                g.FillRectangle(fon, _startPosX + 100, _startPosY + 54, 19, 4);
            }
        }
    }
}