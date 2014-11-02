using Leap;
using System;

namespace UI_Test
{
	/// <summary>
	/// Description of SampleListener.
	/// </summary>
	class SampleListener : Listener
	{
		private Object thisLock = new Object ();

		private void SafeWriteLine (String line)
		{
			lock (thisLock) {
				Console.WriteLine (line);
			}
		}

		public override void OnConnect (Controller controller)
		{
			SafeWriteLine ("Connected");
		}


		public override void OnFrame (Controller controller)
		{
			SafeWriteLine ("Frame available");
		}
	}
}
