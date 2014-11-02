using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace HelloWorld
{
    class SampleListener : Listener
    {
        private readonly Object thisLock = new Object();

        private void SafeWriteLine(String line)
        {
            lock (thisLock)
            {
                Console.WriteLine(line);
            }
        }

        public override void OnConnect(Controller controller)
        {
            SafeWriteLine("Connected");
            controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
            controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
            controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
            controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
        }

        public override void OnFrame(Controller controller)
        {
            var frame = controller.Frame();

            if (frame.Hands.Any(r => r.IsLeft))
            {
                var hand = frame.Hands.First(r => r.IsLeft);
                foreach (var finger in hand.Fingers.Where(r=>r.Type() == Finger.FingerType.TYPE_INDEX))
                {
                    SafeWriteLine("Finger " + finger.Type() + ", x: " + finger.Direction.Normalized.x + ", y: " + finger.Direction.Normalized.y + ", z: " + finger.Direction.Normalized.z);
                }
            }

            //SafeWriteLine("Frame id: " + frame.Id + ", timestamp: " + frame.Timestamp + ", hands: " + frame.Hands.Count + ", fingers: " + frame.Fingers.Count + ", tools: " + frame.Tools.Count + 
            //    ", gestures: " + frame.Gestures().Count);
        }
    }
}
