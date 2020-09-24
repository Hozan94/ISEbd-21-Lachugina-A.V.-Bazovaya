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
    public class Bus
    {
        /// <summary>
        /// Левая координата отрисовки автомобиля
        /// </summary>
        private float _startPosX;

        /// <summary>
        /// Правая координата отрисовки автомобиля
        /// </summary>
        private float _startPosY;

        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private int _pictureWidth;

        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private int _pictureHeight;

        /// <summary>
        /// Ширина отрисовки автобуса
        /// </summary>
        private readonly int busWidth = 194;

        /// <summary>
        /// Высота отрисовки автобуса
        /// </summary>
        private readonly int busHeight = 68;

        /// <summary>
        /// Максимальная скорость
        /// </summary>
        public int MaxSpeed { private set; get; }

        /// <summary>
        /// Вес автомобиля
        /// </summary>
        public float Weight { private set; get; }

        /// <summary>
        /// Основной цвет кузова
        /// </summary>
        public Color MainColor { private set; get; }

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
        public Bus(int maxSpeed, float weight, Color mainColor, Color dopColor, bool garmoshka, bool thirdOs)
        {
            MaxSpeed = maxSpeed;
            Weight = weight;
            MainColor = mainColor;
            DopColor = dopColor;
            Garmoshka = garmoshka;
            ThirdOs = thirdOs;
        }

        /// <summary>
        /// Установка позиции автомобиля
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="width">Ширина картинки</param>
        /// <param name="height">Высота картинки</param>
        public void SetPosition(int x, int y, int width, int height)
        {
            _startPosX = x;
            _startPosY = y;
            _pictureWidth = width;
            _pictureHeight = height;
        }

        /// <summary>
        /// Изменение направления перемещения
        /// </summary>
        /// <param name="direction">Направление</param>
        public void MoveTransport(Direction direction)
        {
            float step = MaxSpeed * 100 / Weight;
            switch (direction)
            {
                //вправо
                case Direction.Right:
                    if (_startPosX + step < _pictureWidth - busWidth)
                    {
                        _startPosX += step;
                    }
                    break;
                //влево
                case Direction.Left:
                    if (_startPosX - step >= 0)
                    {
                        _startPosX -= step;
                    }
                    break;
                //вверх
                case Direction.Up:
                    if (_startPosY - step >= 0)
                    {
                        _startPosY -= step;
                    }
                    break;
                //вниз
                case Direction.Down:
                    if (_startPosY + step < _pictureHeight - busHeight)
                    {
                        _startPosY += step;
                    }
                    break;
            }
        }

        /// <summary>
        /// Отрисовка автомобиля
        /// </summary>
        /// <param name="g"></param>
        public void DrawTransport(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            Brush br = new SolidBrush(MainColor);
            Brush brDop = new SolidBrush(DopColor);
            Brush black = new SolidBrush(Color.Black);
            Pen os = new Pen(Color.Black, 4);
            Brush white = new SolidBrush(Color.White);
            Brush fara = new SolidBrush(Color.Yellow);
            Brush oknoVod = new SolidBrush(Color.Gray);
            Brush okno = new SolidBrush(Color.DarkGray);
            Brush fon = new SolidBrush(Color.FromArgb(240, 240, 240));

            //отрисуем зеркало
            g.FillRectangle(black, _startPosX + 20, _startPosY + 9, 10, 5);
            g.FillEllipse(black, _startPosX + 12, _startPosY + 8, 11, 11);

            //отрисуем кузов автобуса
            g.FillRectangle(br, _startPosX + 29, _startPosY + 5, 164, 53);

            //отрисуем колеса
            //колесо впереди
            g.DrawEllipse(os, _startPosX + 45, _startPosY + 50, 17, 17);
            g.FillEllipse(white, _startPosX + 47, _startPosY + 52, 13, 13);
            //колесо сзади
            g.DrawEllipse(os, _startPosX + 148, _startPosY + 50, 17, 17);
            g.FillEllipse(white, _startPosX + 150, _startPosY + 52, 13, 13);

            //отрисуем люки
            //левый люк
            g.DrawRectangle(pen, _startPosX + 56, _startPosY, 29, 6);
            g.FillRectangle(white, _startPosX + 57, _startPosY + 1, 28, 5);
            //правый люк
            g.DrawRectangle(pen, _startPosX + 140, _startPosY, 29, 6);
            g.FillRectangle(white, _startPosX + 141, _startPosY + 1, 28, 5);

            //отрисуем третью ось колес
            if (ThirdOs)
            {
                g.DrawEllipse(os, _startPosX + 78, _startPosY + 50, 17, 17);
                g.FillEllipse(white, _startPosX + 80, _startPosY + 52, 13, 13);
            }

            //отрисуем окно
            g.FillRectangle(black, _startPosX + 29, _startPosY + 13, 163, 32);
            //окно водителя
            g.FillRectangle(oknoVod, _startPosX + 29, _startPosY + 15, 14, 28);
            //окно всего автобуса
            g.FillRectangle(okno, _startPosX + 46, _startPosY + 15, 145, 28);
            //разделения окон
            g.DrawLine(pen, _startPosX + 69, _startPosY + 15, _startPosX + 69, _startPosY + 44);
            g.DrawLine(pen, _startPosX + 90, _startPosY + 15, _startPosX + 90, _startPosY + 44);
            g.DrawLine(pen, _startPosX + 144, _startPosY + 15, _startPosX + 144, _startPosY + 44);
            g.DrawLine(pen, _startPosX + 170, _startPosY + 15, _startPosX + 170, _startPosY + 44);

            //отрисуем гармошку
            if (Garmoshka)
            {
                g.FillRectangle(brDop, _startPosX + 100, _startPosY + 8, 19, 47);
                g.FillRectangle(fon, _startPosX + 100, _startPosY + 4, 19, 4);
                g.FillRectangle(fon, _startPosX + 100, _startPosY + 54, 19, 4);
            }

            //отрисуем фару
            g.FillRectangle(fara, _startPosX + 30, _startPosY + 48, 9, 5);
        }
    }
}