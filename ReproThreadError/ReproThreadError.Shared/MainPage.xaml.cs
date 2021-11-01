#nullable enable

using System;
using ReactiveUI;
using System.Reactive;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ReproThreadError
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = new MainPageViewModel();
            base.OnNavigatedTo(e);
        }
    }

    public class MainPageViewModel : ReactiveObject
    {
        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            protected set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }
        public ReactiveCommand<Unit, Unit> ButtonCommand { get; }

        public MainPageViewModel()
        {
            ButtonCommand = ReactiveCommand.Create(() => throw new Exception());
            ButtonCommand.ThrownExceptions.Subscribe(async _ =>
            {
                try
                {
                    await new ContentDialog().ShowAsync();
                }
                catch (Exception ex)
                {
                    //throws for UWP not wasm
                    throw ex; //{"The application called an interface that was marshalled for a different thread.

                }
            });
        }
    }
}
