using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsBus
{
    /// <summary>
    /// Параметризованный класс для хранения набора объектов от интерфейса ITransport
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Autovoksal<T> where T : class, ITransport
    {
        /// <summary>
        /// Массив объектов, которые храним
        /// </summary>
        private readonly T[] _places;
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private readonly int pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private readonly int pictureHeight;
        /// <summary>
        /// Размер парковочного места (ширина)
        /// </summary>
        private readonly int _placeSizeWidth = 300;
        /// <summary>
        /// Размер парковочного места (высота)
        /// </summary>
        private readonly int _placeSizeHeight = 80;

        public Autovoksal(int picWidth, int picHeight)
        {
            int width = picWidth / _placeSizeWidth;
            int height = picHeight / _placeSizeHeight;
            _places = new T[width * height];
            pictureWidth = picWidth;
            pictureHeight = picHeight;
        }

        public static bool operator +(Autovoksal<T> p, T bus)
        {
            for (int i = 0; i < p._places.Length; i++)
            {
                if (p._places[i] == null)
                {
                    bus.SetPosition(10 + p._placeSizeWidth * (int)(i / (int)(p.pictureHeight / p._placeSizeHeight)), 10 + p._placeSizeHeight * (int)(i % (int)(p.pictureHeight / p._placeSizeHeight)), p.pictureWidth, p.pictureHeight);
                    p._places[i] = bus;
                    return true;
                }
            }
            return false;
        }

        public static T operator -(Autovoksal<T> p, int index)
        {
            if ((index < p._places.Length) && (index >= 0))
            {
                T bus = p._places[index];
                p._places[index] = null;
                return bus;
            }
            return null;
        }
        /// <summary>
        /// Метод отрисовки парковки
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {
            DrawMarking(g);
            for (int i = 0; i < _places.Length; i++)
            {
                _places[i]?.DrawTransport(g);
            }
        }
        /// <summary>
        /// Метод отрисовки разметки парковочных мест
        /// </summary>
        /// <param name="g"></param>
        private void DrawMarking(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            for (int i = 0; i < pictureWidth / _placeSizeWidth; i++)
            {
                for (int j = 0; j < pictureHeight / _placeSizeHeight + 1; ++j)
                {
                    g.DrawLine(pen, i * _placeSizeWidth + 3, j * _placeSizeHeight, i * _placeSizeWidth + _placeSizeWidth / 2 + 110, j * _placeSizeHeight);
                }
                g.DrawLine(pen, i * _placeSizeWidth + 3, 0, i * _placeSizeWidth + 3, (pictureHeight / _placeSizeHeight) * _placeSizeHeight);
            }
        }
    }
}
