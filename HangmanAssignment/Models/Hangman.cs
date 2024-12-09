using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace HangmanAssignment.Models
{
    
    public class HangmanGame
    {
        public string WordToGuess { get; set; }
        public HashSet<char> CorrectGuesses { get; set; } = new HashSet<char>();
        public HashSet<char> IncorrectGuesses { get; set; } = new HashSet<char>();
        public int MaxAttempts { get; set; } = 8;
        public int CurrentAttempt => IncorrectGuesses.Count;

        public string DisplayWord
        {
            get
            {
                char[] display = new char[WordToGuess.Length];
                for (int i = 0; i < WordToGuess.Length; i++)
                {
                    display[i] = CorrectGuesses.Contains(WordToGuess[i]) ? WordToGuess[i] : '_';
                }
                return new string(display);
            }
        }

        public bool IsGameOver => CurrentAttempt >= MaxAttempts || IsWordGuessed;
        public bool IsWordGuessed => WordToGuess == DisplayWord;
    }
}
