using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using HangmanAssignment.Models;

namespace HangmanAssignment.ViewModels
{
    public class HangmanViewModel : INotifyPropertyChanged
    {
        private HangmanGame _game;
        private string _userGuess;
        private string _hangmanImage;

        public HangmanViewModel()
        {
            _game = new HangmanGame { WordToGuess = "CARRENCEGUES" }; // Example word
            GuessCommand = new Command(GuessLetter); // Set the GuessCommand to call GuessLetter
            UpdateHangmanImage();
        }

        public string DisplayWord => _game.DisplayWord;
        public string IncorrectGuesses => string.Join(", ", _game.IncorrectGuesses);
        public string Message => _game.IsGameOver ? (_game.IsWordGuessed ? "You win!" : "Game over!") : "Guess a letter";

        // Property to bind to the user's guess input
        public string UserGuess
        {
            get => _userGuess;
            set
            {
                _userGuess = value;
                OnPropertyChanged();
            }
        }

        // Property to bind to the hangman image
        public string HangmanImage
        {
            get => _hangmanImage;
            set
            {
                _hangmanImage = value;
                OnPropertyChanged();
            }
        }

        // Command for the Guess button
        public ICommand GuessCommand { get; }

        // Method called when the Guess button is clicked
        private void GuessLetter()
        {
            if (string.IsNullOrEmpty(UserGuess) || UserGuess.Length != 1)
                return;

            char letter = UserGuess.ToUpper()[0];
            if (_game.CorrectGuesses.Contains(letter) || _game.IncorrectGuesses.Contains(letter))
                return;

            if (_game.WordToGuess.Contains(letter))
                _game.CorrectGuesses.Add(letter);
            else
                _game.IncorrectGuesses.Add(letter);

            UserGuess = string.Empty;
            UpdateHangmanImage();
            OnPropertyChanged(nameof(DisplayWord));
            OnPropertyChanged(nameof(IncorrectGuesses));
            OnPropertyChanged(nameof(Message));
        }

        // Method to update the hangman image based on the current attempt count
        private void UpdateHangmanImage()
        {
            switch (_game.CurrentAttempt)
            {
                case 1: HangmanImage = "hang1.png"; break;
                case 2: HangmanImage = "hang2.png"; break;
                case 3: HangmanImage = "hang3.png"; break;
                case 4: HangmanImage = "hang4.png"; break;
                case 5: HangmanImage = "hang5.png"; break;
                case 6: HangmanImage = "hang6.png"; break;
                case 7: HangmanImage = "hang7.png"; break;
                case 8: HangmanImage = "hang8.png"; break;
                default: HangmanImage = "hang1.png"; break;
            }
            OnPropertyChanged(nameof(HangmanImage));
        }

        // Implementation of INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
