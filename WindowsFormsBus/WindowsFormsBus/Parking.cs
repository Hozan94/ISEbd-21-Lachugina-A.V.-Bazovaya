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
    public class Parking<T> where T : class, ITransport
    {
        private readonly List<T> _places;

        private readonly int _maxCount;

        private readonly int pictureWidth;

        private readonly int pictureHeight;

        private readonly int _placeSizeWidth = 300;

        private readonly int _placeSizeHeight = 80;

        public Parking(int picWidth, int picHeight)
        {
            int width = picWidth / _placeSizeWidth;
            int height = picHeight / _placeSizeHeight;
            _maxCount = width * height;
            pictureWidth = picWidth;
            pictureHeight = picHeight;
            _places = new List<T>();
        }

        public static bool operator +(Parking<T> p, T bus)
        {
            if (p._places.Count >= p._maxCount)
                return false;

            p._places.Add(bus);
            return true;
        }

        public static T operator -(Parking<T> p, int index)
        {
            if (index < -1 || index > p._places.Count)
                return null;

            T bus = p._places[index];
            p._places.RemoveAt(index);
            return bus;
        }

        public void Draw(Graphics g)
        {
            DrawMarking(g);
            for (int i = 0; i < _places.Count; ++i)
            {
                int x = i / (pictureHeight / _placeSizeHeight);
                int y = i - x * (pictureHeight / _placeSizeHeight);
                _places[i].SetPosition(x * _placeSizeWidth + 10, y * _placeSizeHeight + 5, pictureWidth, pictureHeight);
                _places[i].DrawTransport(g);
            }
        }

        private void DrawMarking(Graphics g)
        {
            Pen pen = new Pen(Color.Black, 3);
            for (int i = 0; i < pictureWidth / _placeSizeWidth; i++)
            {
                for (int j = 0; j < pictureHeight / _placeSizeHeight + 1; j++)
                {
                    g.DrawLine(pen, i * _placeSizeWidth + 3, j * _placeSizeHeight, i * _placeSizeWidth + _placeSizeWidth / 2 + 110, j * _placeSizeHeight);
                }
                g.DrawLine(pen, i * _placeSizeWidth + 3, 0, i * _placeSizeWidth + 3, (pictureHeight / _placeSizeHeight) * _placeSizeHeight);
            }
        }
    }
}

