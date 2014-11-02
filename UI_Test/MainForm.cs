using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Net;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GestureRecognition.Model;
using Twilio;
using Leap;
using UI_Test.Training;

namespace UI_Test
{
	public partial class MainForm : Form
	{
		[DllImportAttribute("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
		[DllImportAttribute("user32.dll")]
		public static extern bool ReleaseCapture();
		public Controller controller;
		static String tmpFilePath= @"C:\Temp\testagain.xml";
		static String domain= "ftp://user2693720@www2.subdomain.com/www/testagain.xml";
		static String user= "user2693720";
		static String pass="uGwjCHCA";
	    private TrainingForm trainingForm;
		public MainForm()
		{
			InitializeComponent();
			controller = new Controller();
            List<string> dataSource = new List<string>();
            dataSource.Add("+447784343777");
            dataSource.Add("+447533054675");
		    comboBox1.DataSource = dataSource;
		}
		void RichTextBox1TextChanged(object sender, EventArgs e)
		{
		}
		void Button1Click(object sender, EventArgs e)
		{
            this.Dispose();
			this.Close();
		}
		void MainFormMouseDown(object sender, MouseEventArgs e)
		{
			if(e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, 0xA1, 0x2, 0);
			}
		}
		void Label1MouseDown(object sender, MouseEventArgs e)
		{
			if(e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				ReleaseCapture();
				SendMessage(Handle, 0xA1, 0x2, 0);
			}
		}
		void Button2Click(object sender, EventArgs e)
		{
			var twilio = new TwilioRestClient("AC96b05336ac0aa303766ec8735ba18a47","d44e79589bb4d1f484c01085a0785237");
		    var message = twilio.SendMessage("+441133202370", comboBox1.SelectedItem.ToString(), richTextBox1.Text);
			MessageBox.Show("Message Sent");
		}
		void Timer1Tick(object sender, EventArgs e)
		{
			Frame frame = controller.Frame();
			System.Windows.Forms.Application.DoEvents();
			if(frame.Fingers.Count > 0)
			{
				timer1.Stop();
				for(int time = 1; time <=5; time++)
				{
					Thread.Sleep(100);
					System.Windows.Forms.Application.DoEvents();
				}
				Frame oldFrame = null;
				Frame currentFrame = controller.Frame();
				while(!isSimilarFrame(oldFrame, currentFrame))
				{
					Thread.Sleep(100);
					oldFrame = currentFrame;
					currentFrame = controller.Frame();
					System.Windows.Forms.Application.DoEvents();
				}
				
				List<GestureRecognition.Model.Features> features = new List<GestureRecognition.Model.Features>();
				for(int i = 0; i < 10; i++)
				{
					features.Add(new GestureRecognition.Model.Features(controller.Frame()));
					Thread.Sleep(15);
					System.Windows.Forms.Application.DoEvents();
				}
				GestureRecognition.Model.Features inputFeature = features[0];
				for(int i = 1; i < 10; i++)
				{
					inputFeature.dis01 += features[i].dis01;
					inputFeature.dis02 += features[i].dis02;
					inputFeature.dis03 += features[i].dis03;
					inputFeature.dis04 += features[i].dis04;
					inputFeature.midAngle0 += features[i].midAngle0;
					inputFeature.midAngle1 += features[i].midAngle1;
					inputFeature.midAngle2 += features[i].midAngle2;
					inputFeature.midAngle3 += features[i].midAngle3;
					inputFeature.midAngle4 += features[i].midAngle4;
					inputFeature.proxAngle0 += features[i].proxAngle0;
					inputFeature.proxAngle1 += features[i].proxAngle1;
					inputFeature.proxAngle2 += features[i].proxAngle2;
					inputFeature.proxAngle3 += features[i].proxAngle3;
					inputFeature.proxAngle4 += features[i].proxAngle4;
					inputFeature.yawDiff01 += features[i].yawDiff01;
					inputFeature.yawDiff12 += features[i].yawDiff12;
					inputFeature.yawDiff23 += features[i].yawDiff23;
					inputFeature.yawDiff34 += features[i].yawDiff34;
					System.Windows.Forms.Application.DoEvents();
				}
				inputFeature.dis01 = inputFeature.dis01 / 10;
				inputFeature.dis02 = inputFeature.dis02 / 10;
				inputFeature.dis03 = inputFeature.dis03 / 10;
				inputFeature.dis04 = inputFeature.dis04 / 10;
				inputFeature.midAngle0 = inputFeature.midAngle0 / 10;
				inputFeature.midAngle1 = inputFeature.midAngle1 / 10;
				inputFeature.midAngle2 = inputFeature.midAngle2 / 10;
				inputFeature.midAngle3 = inputFeature.midAngle3 / 10;
				inputFeature.midAngle4 = inputFeature.midAngle4 / 10;
				inputFeature.proxAngle0 = inputFeature.proxAngle0 / 10;
				inputFeature.proxAngle1 = inputFeature.proxAngle1 / 10;
				inputFeature.proxAngle2 = inputFeature.proxAngle2 / 10;
				inputFeature.proxAngle3 = inputFeature.proxAngle3 / 10;
				inputFeature.proxAngle4 = inputFeature.proxAngle4 / 10;
				inputFeature.yawDiff01 = inputFeature.yawDiff01 / 10;
				inputFeature.yawDiff12 = inputFeature.yawDiff12 / 10;
				inputFeature.yawDiff23 = inputFeature.yawDiff23 / 10;
				inputFeature.yawDiff34 = inputFeature.yawDiff34 / 10;
				//Create Average Feature
				
				List<QuePair> que = new List<QuePair>();
				int k = 3;

				for (int i = 0; i < k; i++)
					que.Add(new QuePair());

				foreach (string letter in Letter.Alph)
				{
					System.Windows.Forms.Application.DoEvents();
					List<GestureRecognition.Model.Features> featureList = GestureRecognition.Services.PatternService.GetSamples(letter);
					foreach (GestureRecognition.Model.Features f in featureList)
					{
						que.Add(new QuePair(letter, getDifference(inputFeature, f)));
						que = que.OrderBy(QuePair => QuePair.diff).ToList();
						que.RemoveAt(k);
						System.Windows.Forms.Application.DoEvents();
					}
				}
				Dictionary<string, int> letterCount = new Dictionary<string, int>();
				
				foreach(QuePair qp in que)
				{
					if(letterCount.Keys.Contains(qp.letter))
						letterCount[qp.letter]++;
					else
						letterCount.Add(qp.letter, 1);
				}
				
				string l = null;
				int heigestCount = 1;
				foreach(string s in letterCount.Keys)
				{
					if(heigestCount < letterCount[s])
					{
						l = s;
						heigestCount = letterCount[s];
					}
				}
                if (!string.IsNullOrWhiteSpace(l) && l != "yo")
                {
                    richTextBox1.Text += l;
                }
                else if (!string.IsNullOrWhiteSpace(l))
                {
                    using (var wb = new WebClient())
                    {
                        try
                        {
                            var data = new System.Collections.Specialized.NameValueCollection();
                            string token = "333b1990-ce94-4038-8f2a-3829c05b7a75";
                            string url = "http://api.justyo.co/yoall/";
                            data["api_token"] = token;
                            var response = wb.UploadValues(url, "POST", data);
                            richTextBox1.Text += " YO! ";
                        }
                        catch
                        {
                            
                        }
                    }
                }
				timer1.Start();
			}
		}

		public double getDifference(GestureRecognition.Model.Features oldFeature, GestureRecognition.Model.Features newFeature)
		{
			double value = 0;
			value += Math.Pow(oldFeature.dis01 - newFeature.dis01, 2);
			value += Math.Pow(oldFeature.dis02 - newFeature.dis02, 2);
			value += Math.Pow(oldFeature.dis03 - newFeature.dis03, 2);
			value += Math.Pow(oldFeature.dis04 - newFeature.dis04, 2);
			value += Math.Pow(oldFeature.midAngle0 - newFeature.midAngle0, 2);
			value += Math.Pow(oldFeature.midAngle1 - newFeature.midAngle1, 2);
			value += Math.Pow(oldFeature.midAngle2 - newFeature.midAngle2, 2);
			value += Math.Pow(oldFeature.midAngle3 - newFeature.midAngle3, 2);
			value += Math.Pow(oldFeature.midAngle4 - newFeature.midAngle4, 2);
			value += Math.Pow(oldFeature.proxAngle0 - newFeature.proxAngle0, 2);
			value += Math.Pow(oldFeature.proxAngle1 - newFeature.proxAngle1, 2);
			value += Math.Pow(oldFeature.proxAngle2 - newFeature.proxAngle2, 2);
			value += Math.Pow(oldFeature.proxAngle3 - newFeature.proxAngle3, 2);
			value += Math.Pow(oldFeature.proxAngle4 - newFeature.proxAngle4, 2);
			value += Math.Pow(oldFeature.yawDiff01 - newFeature.yawDiff01, 2);
			value += Math.Pow(oldFeature.yawDiff12 - newFeature.yawDiff12, 2);
			value += Math.Pow(oldFeature.yawDiff23 - newFeature.yawDiff23, 2);
			value += Math.Pow(oldFeature.yawDiff34 - newFeature.yawDiff34, 2);
			return Math.Sqrt(value);
		}

		void Label1Click(object sender, EventArgs e)
		{
			
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			timer1.Start();
		}
		
		private bool isSimilarFrame(Frame oldFrame, Frame newFrame)
		{
			if(oldFrame == null || newFrame == null)
				return false;
			Features oldFeature = new Features(oldFrame);
			Features newFeature = new Features(newFrame);
			double values = 0;
			int n = 0;
			if(newFeature.dis01 != 0)
			{
				values +=  Math.Abs(oldFeature.dis01 - newFeature.dis01)/newFeature.dis01;
				n++;
			}
			if(newFeature.dis02 != 0)
			{
				values += Math.Abs(oldFeature.dis02 - newFeature.dis02) / newFeature.dis02;
				n++;
			}
			if(newFeature.dis03 != 0)
			{
				values += Math.Abs(oldFeature.dis03 - newFeature.dis03) / newFeature.dis03;
				n++;
			}
			if(newFeature.dis04 != 0)
			{
				values += Math.Abs(oldFeature.dis04 - newFeature .dis04) / newFeature.dis04;
				n++;
			}
			if(newFeature.yawDiff01 != 0)
			{
				values += Math.Abs(oldFeature.yawDiff01 - newFeature.yawDiff01) / newFeature.yawDiff01;
				n++;
			}
			if(newFeature.yawDiff12 != 0)
			{
				values += Math.Abs(oldFeature.yawDiff12 - newFeature.yawDiff12) / newFeature.yawDiff12;
				n++;
			}
			if(newFeature.yawDiff23 != 0)
			{
				values += Math.Abs(oldFeature.yawDiff23 - newFeature.yawDiff23) / newFeature.yawDiff23;
				n++;
			}
			if(newFeature.yawDiff34 != 0)
			{
				values += Math.Abs(oldFeature.yawDiff34 - newFeature.yawDiff34) / newFeature.yawDiff34;
				n++;
			}
			if(newFeature.proxAngle0 != 0)
			{
				values += Math.Abs(oldFeature.proxAngle0 - newFeature.proxAngle0) / newFeature.proxAngle0;
				n++;
			}
			if(newFeature.proxAngle1 != 0)
			{
				values += Math.Abs(oldFeature.proxAngle1 - newFeature.proxAngle1) / newFeature.proxAngle1;
				n++;
			}
			if(newFeature.proxAngle2 != 0)
			{
				values += Math.Abs(oldFeature.proxAngle2 - newFeature.proxAngle2) / newFeature.proxAngle2;
				n++;
			}
			if(newFeature.proxAngle3 != 0)
			{
				values += Math.Abs(oldFeature.proxAngle3 - newFeature.proxAngle3) / newFeature.proxAngle3;
				n++;
			}
			if(newFeature.proxAngle4 != 0)
			{
				values += Math.Abs(oldFeature.proxAngle4 - newFeature.proxAngle4) / newFeature.proxAngle4;
				n++;
			}
			if(newFeature.midAngle0 != 0)
			{
				values += Math.Abs(oldFeature.midAngle0 - newFeature.midAngle0) / newFeature.midAngle0;
				n++;
			}
			if(newFeature.midAngle1 != 0)
			{
				values += Math.Abs(oldFeature.midAngle1 - newFeature.midAngle1) / newFeature.midAngle1;
				n++;
			}
			if(newFeature.midAngle2 != 0)
			{
				values += Math.Abs(oldFeature.midAngle2 - newFeature.midAngle2) / newFeature.midAngle2;
				n++;
			}
			if(newFeature.midAngle3 != 0)
			{
				values += Math.Abs(oldFeature.midAngle3 - newFeature.midAngle3) / newFeature.midAngle3;
				n++;
			}
			if(newFeature.midAngle4 != 0)
			{
				values += Math.Abs(oldFeature.midAngle4 - newFeature.midAngle4) / newFeature.midAngle4;
				n++;
			}
			if (((values / n) * 100) < 20)
				return true;
			return false;
		}
		
		void writeToFtp()
		{
			FtpWebRequest request = (FtpWebRequest)WebRequest.Create(domain);
			request.Method = WebRequestMethods.Ftp.UploadFile;

			request.Credentials = new NetworkCredential (user,pass);
			
			// Copy the contents of the file to the request stream.
			StreamReader sourceStream = new StreamReader(tmpFilePath);
			byte [] fileContents = System.Text.Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
			sourceStream.Close();
			request.ContentLength = fileContents.Length;

			Stream requestStream = request.GetRequestStream();
			requestStream.Write(fileContents, 0, fileContents.Length);
			requestStream.Close();

			FtpWebResponse response = (FtpWebResponse)request.GetResponse();
			
			Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
			
			response.Close();
		}
		
		void writeToXml()
		{
			XmlWriter xmlWriter = XmlWriter.Create(tmpFilePath);

			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("Response");

			xmlWriter.WriteStartElement("Say");
			xmlWriter.WriteAttributeString("voice", "alice");
			xmlWriter.WriteString(richTextBox1.Text);
			xmlWriter.WriteEndElement();
			

			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();
			xmlWriter.Close();
		}
		void Button3Click(object sender, EventArgs e)
		{
			writeToXml();
			writeToFtp();
			
			string AccountSid = "ACd84eb2269773eac96d0b5dc697aaf57e";
			string AuthToken = "0bab8bdf7419b2e5dda907e624f83c33";
			var twilio = new TwilioRestClient(AccountSid, AuthToken);
			
			var options = new CallOptions();
			//options.Url=domain;
			options.Url="http://leap.1x.biz/testagain.xml";
			
			options.To = comboBox1.SelectedItem.ToString();
			options.From = "+441133202368";
			var call = twilio.InitiateOutboundCall(options);
			
			Console.WriteLine(call.Sid);
		}

        private void btTrainingMode_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (trainingForm == null || trainingForm.IsDisposed)
            {
                trainingForm = new TrainingForm();
                trainingForm.Show();
            }
            if (trainingForm == null || trainingForm.IsDisposed) return;
            if (trainingForm.WindowState == FormWindowState.Minimized)
                trainingForm.WindowState = FormWindowState.Normal;
            this.Hide();
            trainingForm.Activate();
        }
		
	}
}