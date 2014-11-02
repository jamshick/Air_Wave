using System;
using System.Linq;
using GestureRecognition.Model;
using Leap;

namespace GestureRecognition.Services
{
    public class Training
    {
        private const int NumberOfSamples = 100;
        private const int NumberOfPeople = 4;

        public void Train(int numberOfGestures)
        {
            var controller = new Controller();

            for (var i = 0; i < numberOfGestures; i++)
            {
                string name;
                do
                {
                    Console.Write("Type the letter to save: ");
                    name = Console.ReadLine();
                } while (name != null && name.Length != 1);

                var letter = PatternService.GetLetter(name);
                if (letter == null)
                {
                    letter = new Letter {Name = name};
                    PatternService.SaveLetter(letter);
                }

                for (var s = 1; s <= NumberOfPeople; s++)
                {
                    Console.WriteLine("10 seconds for start next person");
                    for (var j = 10; j > 0; j--)
                    {
                        Console.Write(j + "...");
                        System.Threading.Thread.Sleep(1000);
                    }
                    Console.WriteLine("Take samples for person " + s);
                    Console.WriteLine("Start to take samples for gesture " + name);
                    for (var j = 0; j < NumberOfSamples; j++)
                    {
                        Console.Write("[" + j + "] Searching for right hand");
                        var timer = new System.Diagnostics.Stopwatch();
                        timer.Start();
                        var foundHand = false;
                        while (timer.Elapsed.TotalSeconds < 10)
                        {
                            if (controller.Frame().Hands == null || !controller.Frame().Hands.Any(r => r.IsRight)) continue;
                            var sample = new Features(controller.Frame());
                            PatternService.SaveSample(sample.serialisedData, letter.Id);
                            Console.WriteLine("...Saved");
                            System.Threading.Thread.Sleep(500);
                            foundHand = true;
                            break;
                        }
                        timer.Stop();
                        if (!foundHand)
                            throw new Exception("No right hand found");
                    }
                    Console.WriteLine("Finish with person " + s);
                }
            }
        }
    }
}
