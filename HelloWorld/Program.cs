using System;
using GestureRecognition.Services;

namespace HelloWorld
{
    class Program
    {
        static void Main()
        {
            string numberOfGesturesStr;
            var numberOfGestures = 0;

            var train = new Training();
            Console.WriteLine();

            do
            {
                Console.Write("Enter number of gestures: ");
                numberOfGesturesStr = Console.ReadLine();
            } while (!int.TryParse(numberOfGesturesStr, out numberOfGestures));

            Console.WriteLine("Start training");
            train.Train(numberOfGestures);
            Console.WriteLine("Training finished!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
