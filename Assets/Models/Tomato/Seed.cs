using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models.Tomato
{
    class Seed
    {
        public TomatoType type;
        public string name { 
            get
            {
                return GetSeedName(type);
            }
        }
        public Seed(TomatoType type)
        {
            this.type = type;
        }
        private string GetSeedName(TomatoType type)
        {
            switch (type)
            {
                case TomatoType.malinowy:
                    {
                        return Consts.Translations.seedMalinowy;
                    }
                case TomatoType.podluzny:
                    {
                        return Consts.Translations.seedPodluzny;
                    }
                case TomatoType.daktylowy:
                    {
                        return Consts.Translations.seedDaktylowy;
                    }
                case TomatoType.koktajlowy:
                    {
                        return Consts.Translations.seedKoktajlowy;
                    }
                default:
                    {
                        return "";
                    }
            }
        }
    }
}
