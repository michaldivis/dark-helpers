using DarkHelpers.Abstractions;

namespace Sample.Lib
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
