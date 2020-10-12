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
        /// <param name="weight">Вес автобуса</param>
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
            Pen luke = new Pen(Color.Black);
            Brush white = new SolidBrush(Color.White);     
            Brush red = new SolidBrush(MainColor);
            Brush okno = new SolidBrush(Color.DarkGray);
            
            base.DrawTransport(g);

            //отрисуем гармошку
            if (Garmoshka)
            {
                //отрисуем кузов автобуса
                g.FillRectangle(red, _startPosX + 136, _startPosY + 5, 80, 53);

                //правый люк
                g.DrawRectangle(luke, _startPosX + 150, _startPosY, 29, 6);
                g.FillRectangle(white, _startPosX + 151, _startPosY + 1, 28, 5);

                //окно автобуса
                g.DrawRectangle(luke, _startPosX + 137, _startPosY + 14, 78, 29);
                g.FillRectangle(okno, _startPosX + 138, _startPosY + 15, 77, 28);

                //гармошка
                g.FillRectangle(brDop, _startPosX + 118, _startPosY + 8, 19, 47);
            }
            //отрисуем третью ось колес
            if (ThirdOs)
            {
                g.DrawEllipse(os, _startPosX + 148, _startPosY + 50, 17, 17);
                g.FillEllipse(white, _startPosX + 150, _startPosY + 52, 13, 13);
            }
        }
    }
}