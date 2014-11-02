using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace UI_Test
{
    public delegate void HandAppearEventHandler(object sender, EventArgs e);

    class LeapEvent
    {
        public event HandAppearEventHandler HandAppear;
        private Controller controller;

        public LeapEvent()
        {
            controller = new Controller();
        }

        public void IsRightHandPresent()
        {
            while(controller.Frame().Hands == null || !controller.Frame().Hands.Any(r => r.IsRight))
            {
                
            }
            HandAppear(this, null);
        }
    }
}
