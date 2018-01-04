using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace Vancluysen.Carl.Collision
{
  static  class EventChecker
    {
        public static bool EventCheck(this Rectangle r1, Rectangle r2, bool checkbool)
        {
            return r1.Intersects(r2) && checkbool == true;
        }
       
    }
}
