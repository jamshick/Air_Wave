using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestureRecognition.Model;
using GestureRecognition.Services;
using Leap;

namespace UI_Test.Training
{
    public partial class TrainingForm : Form
    {
        private readonly List<string> alfabet = Letter.Alph.ToList();
        private const int NumberOfSamples = 100;
        private string currLetter;
        private Controller controller;
        private MainForm mainForm;

        public TrainingForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TrainingForm_Load(object sender, EventArgs e)
        {
            controller = new Controller();
            step2.Hide();
            step3.Hide();
            currLetter = alfabet.First();
            FirstStep();
        }

        private void FirstStep()
        {
            picture.ImageLocation = ConfigurationManager.AppSettings["imageLocation"] + currLetter + ".jpg";
            picture.Load();
            backgroundWorker2.RunWorkerAsync();
        }
        
        private void SecondStep()
        {
            step2.Show();

            backgroundWorker1.RunWorkerAsync();
        }

        private void ThirdStep()
        {
            if (currLetter == alfabet.Last())
            {
                Finish();
            }
            step3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var index = alfabet.IndexOf(currLetter);
            currLetter = alfabet.ElementAt(index + 1);
            step2.Hide();
            step3.Hide();
            FirstStep();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Finish()
        {
            this.Dispose();
            mainForm = new MainForm();
            mainForm.timer1.Start();
            mainForm.Activate();
            mainForm.Show();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var letter = PatternService.GetLetter(currLetter);
            if (letter == null)
            {
                letter = new Letter { Name = currLetter };
                PatternService.SaveLetter(letter);
            }

            for (var j = 0; j < NumberOfSamples; j++)
            {
                var timer = new System.Diagnostics.Stopwatch();
                timer.Start();
                var foundHand = false;
                while (timer.Elapsed.TotalSeconds < 10)
                {
                    if (controller.Frame().Hands == null || !controller.Frame().Hands.Any(r => r.IsRight)) continue;
                    var sample = new GestureRecognition.Model.Features(controller.Frame());
                    PatternService.SaveSample(sample.serialisedData, letter.Id);
                    backgroundWorker1.ReportProgress((j*100)/NumberOfSamples);
                    System.Threading.Thread.Sleep(100);
                    foundHand = true;
                    break;
                }
                timer.Stop();
                if (!foundHand)
                    throw new Exception("No right hand found");
            }
        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            ThirdStep();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            while (controller.Frame().Hands == null || !controller.Frame().Hands.Any(r => r.IsRight))
            {

            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SecondStep();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}
