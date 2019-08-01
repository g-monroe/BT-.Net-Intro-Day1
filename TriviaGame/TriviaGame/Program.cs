using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Core;

namespace TriviaGame
{
	class Program
	{
        private static Core.Game _game = null;
		 static void Main(string[] args){ //Start Program Here
            int gameMode = 0;
            if (args[0] != null) { //Grab mode for the game if set.
                gameMode = Convert.ToInt32(args[0]); // Set Set Game mode Val to arg. (Would recommend a try catch here.
            }
            _game = new Game();
			Console.WriteLine("Welcome to Trivia!"); //Start Output
            _game.InitQuestions(gameMode);
            _game.ShowPage();
        }
      
    }
}
