using System.Collections;
using System.Windows.Forms;

namespace Snake
{
    class Input
    {
        //wgrywa liste przyciskow
        private static Hashtable keyTable = new Hashtable();

        //sprawdza czy dany przycisk jest wcisniety
        public static bool KeyPressed(Keys key)
        {
            if(keyTable[key] == null)
            {
                return false;
            }
            
            return (bool) keyTable[key];
        }

        //spr czy przycisk z klawiatury zostal wcisniety
        public static void ChangeState(Keys key, bool state)
        {

            keyTable[key] = state;
        }
    }
}
