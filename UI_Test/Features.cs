using System;
using Leap;

namespace UI_Test
{
	public class Features
	{
		public string trueLable {get; set;}
		public float midAngle0 {get; set;}
		public float midAngle1 {get; set;}
		public float midAngle2 {get; set;}
		public float midAngle3 {get; set;}
		public float midAngle4 {get; set;}
		public float proxAngle0 {get; set;}
		public float proxAngle1 {get; set;}
		public float proxAngle2 {get; set;}
		public float proxAngle3 {get; set;}
		public float proxAngle4 {get; set;}
		public float yawDiff01 {get; set;}
		public float yawDiff12 {get; set;}
		public float yawDiff23 {get; set;}
		public float yawDiff34 {get; set;}
		public float dis01 {get; set;}
		public float dis02 {get; set;}
		public float dis03 {get; set;}
		public float dis04 {get; set;}
		public string serialisedData {get; set;}
		
		public Features(Frame frame)
		{
			if(frame.Fingers.Count > 0)
			{
				foreach(Finger f in frame.Fingers)
				{
					Bone b;
					switch(f.Id % 10)
					{
						case 0:
							b = f.Bone(Bone.BoneType.TYPE_INTERMEDIATE);
							midAngle0 = b.PrevJoint.AngleTo(b.NextJoint);
							b = f.Bone(Bone.BoneType.TYPE_PROXIMAL);
							proxAngle0 = b.PrevJoint.AngleTo(b.NextJoint);
							Bone b2;
							foreach(Finger f2 in frame.Fingers)
							{
								switch(f2.Id % 10)
								{
									case 1:
										yawDiff01 = f.Direction.Yaw - f2.Direction.Yaw;
										b2 = f2.Bone(Bone.BoneType.TYPE_INTERMEDIATE);
										b = f.Bone(Bone.BoneType.TYPE_DISTAL);
										dis01 = b.NextJoint.DistanceTo(b2.PrevJoint);
										break;
									case 2:
										b2 = f2.Bone(Bone.BoneType.TYPE_INTERMEDIATE);
										b = f.Bone(Bone.BoneType.TYPE_DISTAL);
										dis02 = b.NextJoint.DistanceTo(b2.PrevJoint);
										break;
									case 3:
										b2 = f2.Bone(Bone.BoneType.TYPE_INTERMEDIATE);
										b = f.Bone(Bone.BoneType.TYPE_DISTAL);
										dis03 = b.NextJoint.DistanceTo(b2.PrevJoint);
										break;
									case 4:
										b2 = f2.Bone(Bone.BoneType.TYPE_INTERMEDIATE);
										b = f.Bone(Bone.BoneType.TYPE_DISTAL);
										dis04 = b.NextJoint.DistanceTo(b2.PrevJoint);
										break;
								}
							}
							break;
						case 1:
							b = f.Bone(Bone.BoneType.TYPE_INTERMEDIATE);
							midAngle1 = b.PrevJoint.AngleTo(b.NextJoint);
							b = f.Bone(Bone.BoneType.TYPE_PROXIMAL);
							proxAngle1 = b.PrevJoint.AngleTo(b.NextJoint);
							foreach(Finger f2 in frame.Fingers)
								if(f2.Id % 10 == 2)
									yawDiff12 = f.Direction.Yaw - f2.Direction.Yaw;
							break;
						case 2:
							b = f.Bone(Bone.BoneType.TYPE_INTERMEDIATE);
							midAngle2 = b.PrevJoint.AngleTo(b.NextJoint);
							b = f.Bone(Bone.BoneType.TYPE_PROXIMAL);
							proxAngle2 = b.PrevJoint.AngleTo(b.NextJoint);
							foreach(Finger f2 in frame.Fingers)
								if(f2.Id % 10 == 3)
									yawDiff23 = f.Direction.Yaw - f2.Direction.Yaw;
							break;
						case 3:
							b = f.Bone(Bone.BoneType.TYPE_INTERMEDIATE);
							midAngle3 = b.PrevJoint.AngleTo(b.NextJoint);
							b = f.Bone(Bone.BoneType.TYPE_PROXIMAL);
							proxAngle3 = b.PrevJoint.AngleTo(b.NextJoint);
							foreach(Finger f2 in frame.Fingers)
								if(f2.Id % 10 == 3)
									yawDiff34 = f.Direction.Yaw - f2.Direction.Yaw;
							break;
						case 4:
							b = f.Bone(Bone.BoneType.TYPE_INTERMEDIATE);
							midAngle4 = b.PrevJoint.AngleTo(b.NextJoint);
							b = f.Bone(Bone.BoneType.TYPE_PROXIMAL);
							proxAngle4 = b.PrevJoint.AngleTo(b.NextJoint);
							break;
					}
				}
			}
			serialisedData = "" + midAngle0 + "_"+ midAngle1 + "_" + midAngle2 + "_" + midAngle3 + "_" + midAngle4 +
				"_" + proxAngle0 + "_" + proxAngle1 + "_" + proxAngle2 + "_" + proxAngle3 + "_" + proxAngle4 +
				"_" + yawDiff01 + "_" + yawDiff12 + "_" + yawDiff23 + "_" + yawDiff34 +
				"_" + dis01 + "_" + dis02 + "_" + dis03 + "_" + dis04;
		}
		
		public Features(string theTrueLable, float theMidAngle0, float theMidAngle1, float theMidAngle2, float theMidAngle3, float theMidAngle4,
		                float theProxAngle0, float theProxAngle1, float theProxAngle2, float theProxAngle3, float theProxAngle4,
		                float theYawDiff01, float theYawDiff12, float theYawDiff23, float theYawDiff34,
		                float theDis01, float theDis02, float theDis03, float theDis04)
		{
			trueLable = theTrueLable;
			midAngle0 = theMidAngle0;
			midAngle1 = theMidAngle1;
			midAngle2 = theMidAngle2;
			midAngle3 = theMidAngle3;
			midAngle4 = theMidAngle4;
			proxAngle0 = theProxAngle0;
			proxAngle1 = theProxAngle1;
			proxAngle2 = theProxAngle2;
			proxAngle3 = theProxAngle3;
			proxAngle4 = theProxAngle4;
			yawDiff01 = theYawDiff01;
			yawDiff12 = theYawDiff12;
			yawDiff23 = theYawDiff23;
			yawDiff34 = theYawDiff34;
			dis01 = theDis01;
			dis02 = theDis02;
			dis03 = theDis03;
			dis04 = theDis04;
			
			serialisedData = "" + midAngle0 + "_"+ midAngle1 + "_" + midAngle2 + "_" + midAngle3 + "_" + midAngle4 +
				"_" + proxAngle0 + "_" + proxAngle1 + "_" + proxAngle2 + "_" + proxAngle3 + "_" + proxAngle4 +
				"_" + yawDiff01 + "_" + yawDiff12 + "_" + yawDiff23 + "_" + yawDiff34 +
				"_" + dis01 + "_" + dis02 + "_" + dis03 + "_" + dis04;
		}
		
		public Features(string theSearialiedData, string theTrueLable)
		{
			trueLable = theTrueLable;
			serialisedData = theSearialiedData;
			string[] data = theSearialiedData.Split('_');
			midAngle0 = float.Parse(data[0], System.Globalization.CultureInfo.InvariantCulture);
			midAngle1 = float.Parse(data[1], System.Globalization.CultureInfo.InvariantCulture);
			midAngle2 = float.Parse(data[2], System.Globalization.CultureInfo.InvariantCulture);
			midAngle3 = float.Parse(data[3], System.Globalization.CultureInfo.InvariantCulture);
			midAngle4 = float.Parse(data[4], System.Globalization.CultureInfo.InvariantCulture);
			proxAngle0 = float.Parse(data[5], System.Globalization.CultureInfo.InvariantCulture);
			proxAngle1 = float.Parse(data[6], System.Globalization.CultureInfo.InvariantCulture);
			proxAngle2 = float.Parse(data[7], System.Globalization.CultureInfo.InvariantCulture);
			proxAngle3 = float.Parse(data[8], System.Globalization.CultureInfo.InvariantCulture);
			proxAngle4 = float.Parse(data[9], System.Globalization.CultureInfo.InvariantCulture);
			yawDiff01 = float.Parse(data[10], System.Globalization.CultureInfo.InvariantCulture);
			yawDiff12 = float.Parse(data[11], System.Globalization.CultureInfo.InvariantCulture);
			yawDiff23 = float.Parse(data[12], System.Globalization.CultureInfo.InvariantCulture);
			yawDiff34 = float.Parse(data[13], System.Globalization.CultureInfo.InvariantCulture);
			dis01 = float.Parse(data[14], System.Globalization.CultureInfo.InvariantCulture);
			dis02 = float.Parse(data[15], System.Globalization.CultureInfo.InvariantCulture);
			dis03 = float.Parse(data[16], System.Globalization.CultureInfo.InvariantCulture);
			dis04 = float.Parse(data[17], System.Globalization.CultureInfo.InvariantCulture);
		}
	}
}
