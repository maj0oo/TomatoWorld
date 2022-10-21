using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Characters.Character;

namespace Assets.Characters
{
    class CharactersManager
    {
        public Panel panel;
        public static List<Character> characters = new List<Character>();
        
        public void RefreshPanel()
        {
            if(panel == null)
            {
                return;
            }
            //TODO add on input change selected option
        }
    }
    public enum CharacterType
    {
        dealer = 1,
    }
    class SeedPrices
    {
        public const float malinowySeedPrice = 0.5f;
        public const float koktajlowySeedPrice = 2f;
        public const float daktylowySeedPrice = 5f;
        public const float podluznySeedPrice = 10f;
    }
}
