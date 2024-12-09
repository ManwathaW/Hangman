using HangmanAssignment.ViewModels;

namespace HangmanAssignment;

public partial class HangmanGamePage : ContentPage
{
	public HangmanGamePage()
	{
		InitializeComponent();
        InitializeComponent(); BindingContext = new HangmanViewModel();
    }
}