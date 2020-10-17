using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsBus
{
    class AutovoksalCollection
    {
        readonly Dictionary<string, Autovoksal<EasyBus>> autovoksalStages;

        public List<string> Keys => autovoksalStages.Keys.ToList();

        private readonly int pictureWidth;

        private readonly int pictureHeight;

        public AutovoksalCollection(int pictureWidth, int pictureHeight)
        {
            autovoksalStages = new Dictionary<string, Autovoksal<EasyBus>>();
            this.pictureWidth = pictureWidth;
            this.pictureHeight = pictureHeight;
        }

        public void AddAutovoksal(string name)
        {
            if (autovoksalStages.ContainsKey(name))
            {
                return;
            }
            autovoksalStages.Add(name, new Autovoksal<EasyBus>(pictureWidth, pictureHeight));
        }

        public void DelAutovoksal(string name)
        {
            if (autovoksalStages.ContainsKey(name))
            {
                autovoksalStages.Remove(name);
            }
        }

        public Autovoksal<EasyBus> this[string index]
        {
            get
            {
                if (autovoksalStages.ContainsKey(index))
                {
                    return autovoksalStages[index];
                }
                else
                {
                    return null;
                }
            }
        }
    }
}


