using HangmanAssignment.ViewModel;
namespace HangmanAssignment
{
    public partial class HangmanGamePage : ContentPage
    {
        public HangmanGamePage(HangmanViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}