using DarkHelpers.Interfaces;

namespace Sample.WPF
{
    public static class FakeDiContainer
    {
        public static IDarkNavigationService DarkNavigationService;

        public static void Initialize(IDarkNavigationService darkNavigationService)
        {
            DarkNavigationService = darkNavigationService;
        }
    }
}
