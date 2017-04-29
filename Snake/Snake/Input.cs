using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class Input
    {
        //Incarcam liste cu posibilele kayboard buttons
        private static Hashtable keyTable = new Hashtable();

        //Perform a check to see if   a particular  button  is pressed
        public static bool KeyPressed(Keys key)
        {
            if(keyTable[key] == null)
            {
                return false;
            }
            return (bool)keyTable[key];
        }

        //Detect if a keyboard is pressed
        public  static void ChangeState(Keys key, bool state)
        {
            keyTable[key] = state;
        }

        internal static bool Pressed(Keys right)
        {
            throw new NotImplementedException();
        }
    }
}
