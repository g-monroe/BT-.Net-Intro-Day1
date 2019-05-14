using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JsonApp
{
    public class QuestionAdder
    {
        public static String[] incorrectAnswers;
        public static _Question newQuestion;
        public static List<_Question> Questions;
        public static Boolean view = false;
        public static void Main(String[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = System.IO.Path.Combine(currentDirectory, "Files", "Questions.json");
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                Questions = JsonConvert.DeserializeObject<List<_Question>>(json); //Uses JSON.net Library
            }
            Console.WriteLine("View JSON after done? [Y/N]");
            String val = Console.ReadLine();
            if (val.ToLower() == "y")
            {
                view = true;
            }
            Ask();
        }
        public static void AskQuestion()
        {
            newQuestion = new _Question();
            incorrectAnswers = new String[0];
            Console.WriteLine("Question: ");
            String QuestionText = Console.ReadLine();
            newQuestion.Question = QuestionText;
            Console.WriteLine("Answer: ");
            String Answer = Console.ReadLine();
            newQuestion.CorrectAnswer = Answer;
            AskIncorrect();
        }
        public static void AskIncorrect()
        {
            Console.WriteLine("Incorrect Answer: ");
            String incorrect = Console.ReadLine();
            Array.Resize(ref incorrectAnswers, incorrectAnswers.Length + 1);
            incorrectAnswers[incorrectAnswers.Length - 1] = incorrect;
            AskMore();
        }
        public static void AskMore()
        {
            Console.WriteLine("Add more incorrect answers? [Y/N]");
            String val = Console.ReadLine();
            if (val.ToLower() == "y")
            {
                AskIncorrect();
            }
            else
            {
                newQuestion.IncorrectAnswers = incorrectAnswers;
                Questions.Add(newQuestion);
                Ask();  
            }
        }
        public static void writeJson()
        {
            string json = JsonConvert.SerializeObject(Questions);
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(currentDirectory, "Files", "Questions.json");
            if (view)
            {
              Console.WriteLine(json);
            }
            File.WriteAllText(filePath, json);
        }
        public static void Ask()
        {
            Console.WriteLine("Would you like to add Json to the Questions File? [Y/N]");
            String val = Console.ReadLine();
            if (val.ToLower() == "y")
            {
                AskQuestion();
            }
            else
            {
                writeJson();
            }
        }
    }
}
