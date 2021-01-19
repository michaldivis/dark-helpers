namespace DarkHelpers
{
    /// <summary>
	/// Base view model
	/// </summary>
    public class DarkViewModel : DarkObservableObject
    {
		bool _isBusy;
		/// <summary>
		/// Gets or sets a value indicating whether this instance is busy.
		/// </summary>
		/// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
		public bool IsBusy
		{
			get => _isBusy;
			set
			{
				if (SetProperty(ref _isBusy, value))
                {
                    IsNotBusy = !_isBusy;
                }
            }
		}

		bool _isNotBusy = true;
		/// <summary>
		/// Gets or sets a value indicating whether this instance is not busy.
		/// </summary>
		/// <value><c>true</c> if this instance is not busy; otherwise, <c>false</c>.</value>
		public bool IsNotBusy
		{
			get => _isNotBusy;
			set
			{
				if (SetProperty(ref _isNotBusy, value))
                {
                    IsBusy = !_isNotBusy;
                }
            }
		}

		bool _canLoadMore = true;
		/// <summary>
		/// Gets or sets a value indicating whether this instance can load more items
		/// </summary>
		/// <value><c>true</c> if this instance can load more items; otherwise, <c>false</c>.</value>
		public bool CanLoadMore
		{
			get => _canLoadMore;
			set => SetProperty(ref _canLoadMore, value);
		}

		bool _isLoadingMore = true;
		/// <summary>
		/// Gets or sets a value indicating whether this instance is currently loading more items
		/// </summary>
		/// <value><c>true</c> if this instance is loading more items; otherwise, <c>false</c>.</value>
		public bool IsLoadingMore
		{
			get => _isLoadingMore;
			set => SetProperty(ref _isLoadingMore, value);
		}
	}
}
