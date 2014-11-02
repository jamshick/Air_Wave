using System.Collections.Generic;
using System.Linq;
using GestureRecognition.DAL;
using GestureRecognition.Model;

namespace GestureRecognition.Services
{
    public class PatternService
    {
        public static void SaveSample(string sample, int idLetter)
        {
            PatternDao.SaveSample(sample, idLetter);
        }

        public static int SaveLetter(Letter letter)
        {
            return PatternDao.SaveLetter(letter);
        }

        public static Letter GetLetter(string name)
        {
            return PatternDao.GetLetter(new LetterRequest {Name = name}).FirstOrDefault();
        }

        public static Letter GetLetter(LetterRequest request)
        {
            return PatternDao.GetLetter(request).FirstOrDefault();
        }

        public static List<Features> GetSamples(string name)
        {
            return PatternDao.GetSamples(new FeatureRequest {Name = name});
        }

        public static List<Features> GetSamples(FeatureRequest request)
        {
            return PatternDao.GetSamples(request);
        }

        public static void DeleteLetter(string name)
        {
            PatternDao.DeleteLetter(name);
        }

        public static void DeleteSamples(string name)
        {
            PatternDao.DeleteSamples(name);
        }
    }
}
