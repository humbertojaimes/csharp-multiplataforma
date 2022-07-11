namespace SpeakersApp;

public partial class MainPage : ContentPage
{

	private readonly SpeakersApiClient.SpeakersApiClient _apiClient;

	public MainPage(SpeakersApiClient.SpeakersApiClient speakersApiClient)
	{
		InitializeComponent();
		_apiClient = speakersApiClient;
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		var speakers = await _apiClient.Get();

			collection.ItemsSource = speakers.Data;
	}
}


