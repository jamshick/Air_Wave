using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestureRecognition.Model;

namespace GestureRecognition.DAL
{
    internal class PatternDao
    {
        private const string LetterTable = "letter";
        private const string SampleTable = "sample";

        internal static void SaveSample(string pattern, int idLetter)
        {
            var query = "INSERT INTO " + SampleTable + "(idLetter, data) VALUES(" + idLetter + ", '" + pattern + "')";
            var db = new DbConnect();
            db.ExecuteNonQuery(query);
        }

        internal static int SaveLetter(Letter letter)
        {
            var query = "INSERT INTO " + LetterTable + "(name) VALUES('" + letter.Name.ToLower() + "'); SELECT LAST_INSERT_ID();";
            var db = new DbConnect();
            letter.Id = db.ExecuteScalar(query);
            return letter.Id;
        }

        

        internal static void DeleteLetter(string name)
        {
            var query = "DELETE FROM " + LetterTable + " WHERE name = '" + name.ToLower() + "'";
            var db = new DbConnect();
            db.ExecuteNonQuery(query);
        }

        internal static void DeleteSamples(string name)
        {
            var query = "DELETE FROM " + SampleTable + " WHERE idLetter = (SELECT idLetter FROM " + LetterTable + " WHERE name = '" + name.ToLower() + "')";
            var db = new DbConnect();
            db.ExecuteNonQuery(query);
        }

        public static List<Letter> GetLetter(LetterRequest letterRequest)
        {
            var constrains = new List<string>();
            var sql = "SELECT idLetter, name FROM " + LetterTable + " ";

            if (!letterRequest.RetrieveAll)
            {
                if (letterRequest.Id != 0)
                {
                    constrains.Add("idLetter = " + letterRequest.Id);
                }
                if (letterRequest.Name != null)
                {
                    constrains.Add("name = '" + letterRequest.Name.ToLower() + "'");
                }
            }

            if (constrains.Count > 0)
            {
                sql += " WHERE ";
                sql = constrains.Aggregate(sql, (current, constrain) => current + (constrain + " "));
            }
            var db = new DbConnect();
            return db.ExecuteReader(sql, r => new Letter
                {
                    Id = (int) r["idLetter"],
                    Name = (string) r["name"]
                });
        }

        public static List<Features> GetSamples(FeatureRequest featureRequest)
        {
            var sql = "SELECT " + SampleTable + ".data, " + SampleTable + ".idLetter FROM " + SampleTable;
            var constrains = new List<string>();
            if (!featureRequest.RetrieveAll)
            {
                if (featureRequest.IdLetter != 0)
                {
                    constrains.Add("idLetter = " + featureRequest.IdLetter);
                }
                if (featureRequest.Name != null)
                {
                    sql += " INNER JOIN " + LetterTable + " ON " + LetterTable + ".idLetter = " + SampleTable + ".idLetter ";
                    constrains.Add("name = '" + featureRequest.Name + "'");
                }
            }

            if (constrains.Count > 0)
            {
                sql += " WHERE ";
                sql = constrains.Aggregate(sql, (current, constrain) => current + (constrain + " "));
            }

            var db = new DbConnect();
            return db.ExecuteReader(sql, r => new Features((string) r["data"]));
        }
    }
}
