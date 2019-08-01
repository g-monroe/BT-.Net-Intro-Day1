using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace Core
{
    public class Game : IDisposable
    {
        public class Item
        {
            public String QuestionText { get; set; }
            public String CorrectResult { get; set; }
            public String IncorrectResult { get; set; }
            public Boolean correct { get; set; }
    }
        public List<_Question> Questions { get; set; }
        public int GameMode { get; set; } //1 = type the input, 0 = input but index
        public int CorrectNum { get; set; }
        public int IncorrectNum { get; set; }
        public int Answered { get; set; }
        public int NumQuestions { get; set; }
        public _Question selectedQuestion { get; set; }
        public List<Item> results { get; set; }

        public void InitQuestions(int gameMode = 0)
        {
            Console.ForegroundColor = ConsoleColor.White;
            results = new List<Item>();
            string currentDirectory = Directory.GetCurrentDirectory();
            string filePath = System.IO.Path.Combine(currentDirectory, "Files", "Questions.json");
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                Questions = JsonConvert.DeserializeObject<List<_Question>>(json); //Uses JSON.net Library
            }
            NumQuestions = Questions.Count;
            Answered = 1;
            CorrectNum = 0;
            IncorrectNum = 0;
            GameMode = gameMode;
            
        }
        public void ShowPage()
        {
            _Question temp = runGameQuestion();
            if (temp == null)
            {
                Environment.Exit(0);
            }
            Console.WriteLine("[" + Answered.ToString() + "/" + NumQuestions.ToString() + "] " + selectedQuestion.Question);
            String[] answers = selectedQuestion.IncorrectAnswers;
            int i = 0;
            foreach (String _item in answers)
            { //Loop through Questions
                Console.WriteLine("[" + i.ToString() + "] " + _item);
                i++;
            }
            AskInput();
        }
        private void AskInput()
        {
            if (GameMode == 1)
            {
                Console.WriteLine("Type your Answer: ");
            }
            else
            {
                Console.WriteLine("Input Index of your Answer: ");
            }
            ReadInput();
        }
        private void ReadInput()
        {
            String val = Console.ReadLine();
            Boolean correct = false;
            String _input = "";
            if (GameMode == 1)
            {
                if (val.ToLower() == selectedQuestion.CorrectAnswer.ToLower())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You were right!");
                    _input = val;
                    correct = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You were Wrong! The correct answer was: " + selectedQuestion.CorrectAnswer);
                    _input = val;
                }
                AskAgain(selectedQuestion.Question, correct, _input, selectedQuestion.CorrectAnswer);
            }
            else
            {
                int input = Convert.ToInt32(val);
                int max = selectedQuestion.IncorrectAnswers.Length;
                if (max <= input)
                {
                    Console.WriteLine("Answer out of Index Try Again!");
                    AskInput();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You Selected: " + selectedQuestion.IncorrectAnswers[input]);
                    String answer = selectedQuestion.IncorrectAnswers[input];
                    if (answer == selectedQuestion.CorrectAnswer)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You were right!");
                        correct = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You were Wrong! The correct answer was: " + selectedQuestion.CorrectAnswer);
                    }
                    AskAgain(selectedQuestion.Question, correct, answer, selectedQuestion.CorrectAnswer);
                }
            }

        }
        public  void AskAgain(String qText, Boolean correct, String input, String correctText)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Would you like another Question? [Y/N]");
            String val = Console.ReadLine();

            ++Answered;
            if (correct){
               ++CorrectNum;
            }else{
               ++IncorrectNum;
            }
            Item result = new Item();
            result.correct = correct;
            result.CorrectResult = correctText;
            result.IncorrectResult = input;
            result.QuestionText = qText;
            results.Add(result);
            if (val.ToLower() == "y")
            {
                Console.Clear();
                ShowPage();
            }
            else
            {
                Environment.Exit(0);
            }
        }
        public  _Question runGameQuestion()
        {
            if (Questions.Count == 0)
            {
                printResults();
                return null;
            }
            Random rnd = new Random();
            int max = Questions.Count;
            int index = rnd.Next(max);
            selectedQuestion = Questions[index];
            Questions.Remove(Questions[index]);
            return selectedQuestion;
        }
       public void printResults()
        {
            Console.WriteLine("Correct: " + CorrectNum.ToString() + ", Incorrect: " + IncorrectNum.ToString());
            foreach (Item _item in results)
            {
                if (_item.correct)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine("Question: " + _item.QuestionText);
                Console.WriteLine("-Correct Answer: " + _item.CorrectResult);
                Console.WriteLine("-Your Answer: " + _item.IncorrectResult);
                Console.WriteLine();
            }
        }
 
        public void Dispose()
        {
            
        }
    }
}
