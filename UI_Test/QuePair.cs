using System;

namespace UI_Test
{
	public class QuePair
	{
		public string letter {get; set;}
		public double diff {get; set;}
		public QuePair()
		{
			letter = null;
			diff = Double.PositiveInfinity;
		}
		public QuePair(string theLetter, double theDiff)
		{
			letter = theLetter;
			diff = theDiff;
		}
	}
}
