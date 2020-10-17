using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsBus
{
    class ParkingCollection
    {
        readonly Dictionary<string, Parking<EasyBus>> parkingStages;

        public List<string> Keys => parkingStages.Keys.ToList();

        private readonly int pictureWidth;

        private readonly int pictureHeight;

        public ParkingCollection(int pictureWidth, int pictureHeight)
        {
            parkingStages = new Dictionary<string, Parking<EasyBus>>();
            this.pictureWidth = pictureWidth;
            this.pictureHeight = pictureHeight;
        }

        public void AddAutovoksal(string name)
        {
            if (parkingStages.ContainsKey(name))
            {
                return;
            }
            parkingStages.Add(name, new Parking<EasyBus>(pictureWidth, pictureHeight));
        }

        public void DelAutovoksal(string name)
        {
            if (parkingStages.ContainsKey(name))
            {
                parkingStages.Remove(name);
            }
        }

        public Parking<EasyBus> this[string index]
        {
            get
            {
                if (parkingStages.ContainsKey(index))
                {
                    return parkingStages[index];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}


