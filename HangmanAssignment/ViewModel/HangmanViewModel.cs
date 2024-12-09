using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace HangmanAssignment.ViewModel
{
    // ViewModel class for the Hangman game, inheriting from ObservableObject for property change notifications
    public partial class HangmanViewModel : ObservableObject
    {
        // List of possible words for the hangman game
        private readonly string[] words = {
            "CURRENT", "GUESS", "HANGMAN", "MAUI", "CODE",
            "BANANA", "PICKLE", "WHIMSICAL", "FLABBERGAST",
            "NINCOMPOOP", "GIGGLE", "BUMFUZZLE", "SHENANIGANS",
            "BAZOOKIE", "GOBBLEDYGOOK", "SNICKERDOODLE",
            "LOLLIPOP", "WIGGLEWORM", "FROLIC",
            "DINGLEBERRY", "QUIRKY", "JELLYBEAN",
            "BUMPKIN", "WIGGLE", "HOOPLA"
        };

        private string currentWord; // The word currently being guessed
        private int wrongGuesses; // Counter for wrong guesses
        private HashSet<char> guessedLetters; // Set of letters that have been guessed

        // Observable property for the word displayed to the user, with guessed letters revealed
        [ObservableProperty]
        private string displayWord;

        // Observable property for the current letter guess input by the user
        [ObservableProperty]
        private string currentGuess;

        // Observable property for the source of the hangman image to display
        [ObservableProperty]
        private string imageSource;

        // Constructor to initialize the game
        public HangmanViewModel()
        {
            InitializeGame();
        }

        // Method to initialize or reset the game state
        private void InitializeGame()
        {
            Random random = new Random();
            currentWord = words[random.Next(words.Length)]; // Randomly select a word from the list
            wrongGuesses = 1; 
            guessedLetters = new HashSet<char>();
            ImageSource = "hang1.png"; 
            RevealRandomLetters(2); 
            UpdateDisplayWord(); 
        }

        // Method to reveal a specified number of random letters from the current word
        private void RevealRandomLetters(int numberOfLetters)
        {
            Random random = new Random();
            var letters = currentWord.ToCharArray().OrderBy(x => random.Next()).Take(numberOfLetters);
            foreach (var letter in letters)
            {
                guessedLetters.Add(letter); // Add each randomly selected letter to the set of guessed letters
            }
        }

        // Method to update the display word based on guessed letters
        private void UpdateDisplayWord()
        {
            DisplayWord = string.Join("  ", currentWord.Select(c =>
                guessedLetters.Contains(c) ? c.ToString() : "__"));
        }

        // Command method for when the user makes a guess
        [ICommand]
        private void Guess()
        {
            if (string.IsNullOrEmpty(CurrentGuess))
                return; 

            char guess = CurrentGuess.ToUpper()[0]; 
            CurrentGuess = string.Empty; 

            if (guessedLetters.Contains(guess))
                return; 

            guessedLetters.Add(guess);

            if (!currentWord.Contains(guess))
            {
                wrongGuesses++;
                ImageSource = $"hang{wrongGuesses}.png"; 

                if (wrongGuesses >= 2)
                {                  
                    Application.Current.MainPage.DisplayAlert("Game Over",
                        $"The word was: {currentWord}", "New Game").ContinueWith(_ => InitializeGame());
                    return;
                }
            }

            UpdateDisplayWord(); // Update the display word with the guessed letters

            if (!DisplayWord.Contains("_"))
            {
                
                Application.Current.MainPage.DisplayAlert("Congratulations",
                    "You won!", "New Game").ContinueWith(_ => InitializeGame());
            }
        }

        // Command method to start a new game
        [ICommand]
        private void NewGame()
        {
            InitializeGame(); 
        }
    }
}
