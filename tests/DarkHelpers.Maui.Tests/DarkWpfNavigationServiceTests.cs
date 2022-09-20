using DarkHelpers.Maui.HelperModels;

namespace DarkHelpers.Maui;

public class DarkMauiNavigationServiceTests
{
    private readonly DarkMauiNavigationService _nav;

    public DarkMauiNavigationServiceTests()
    {
        _nav = new DarkMauiNavigationService();
    }

    [Fact]
    public void Register_ArgumentsCorrect_Passes()
    {
        _nav.Register<SomeViewModel, SomeView>();
    }
}