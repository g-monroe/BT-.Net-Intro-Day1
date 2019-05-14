using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
namespace Core
{
    static class ExtensionMethods
    {
        public static void WriteQuestionsAnsweredIncorrectly(this List<_Question> questions, StreamWriter str)
        {
            IEnumerable<_Question> testQuery = questions.Where(q => !q.correct).ToList();
            string json = JsonConvert.SerializeObject(testQuery);
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "Files", "Questions.json");
            File.WriteAllText(filePath, json);
        }
        public static void WriteQuestionsAnsweredCorrectly(this List<_Question> questions, StreamWriter str)
        {
            IEnumerable<_Question> testQuery = questions.Where(q => q.correct).ToList();
            string json = JsonConvert.SerializeObject(testQuery);
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "Files", "Questions.json");
            File.WriteAllText(filePath, json);
        }
    }
}
