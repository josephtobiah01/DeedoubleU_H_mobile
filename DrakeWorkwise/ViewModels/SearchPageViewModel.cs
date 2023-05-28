using DrakeWorkwise.Interfaces;
using timer = System.Timers;

namespace DrakeWorkwise.ViewModels
{
    public class SearchPageViewModel : ViewModelBase
    {

        #region Fields


        private List<string> _toSearch = null;
        private Action<string> _callBack = null;
        private List<string> _sourceItems;
        private string _searchBarText;


        #endregion Fields





        #region Properties

        private string _searchPlaceHolder = "";
        public string SearchPlaceHolder
        {
            get { return _searchPlaceHolder; }
            set
            {
                SetPropertyValue(ref _searchPlaceHolder, value);
            }
        }

        
        public Command SearchCollectionCommand { get; set; }
        public Command<string> SelectCommand { get; set; }

        public string SearchBarText
        {
            get { return _searchBarText; }
            set
            {
                SetPropertyValue(ref _searchBarText, value);
                _searchTimer?.Stop();
                _searchTimer?.Start();
            }
        }

        timer.Timer _searchTimer = null;
        public List<string> SourceItems
        {
            get => _sourceItems;
            set => SetPropertyValue(ref _sourceItems, value);
        }

        


        #endregion Properties





        #region Methods

        #region Constructor


        public SearchPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Initialize();

        }

        



        #endregion Constructor




        private void Initialize()
        {
            Title = "Search page";
            SelectCommand = new Command<string>(async (param) =>
            {
                _callBack?.Invoke(param);
                await Task.Delay(500);
                await NavigationService.GoBack();
            });

            _searchTimer = new timer.Timer();
            _searchTimer.Elapsed += _searchTimer_Elapsed;
            _searchTimer.Interval = 800;
        }

        private void _searchTimer_Elapsed(object sender, timer.ElapsedEventArgs e)
        {
            SearchCollection();
        }

        public override Task OnNavigatingTo(Dictionary<string, object> parameter)
        {
            if (parameter.TryGetValue("tosearch", out object toSearch))
            {
                _toSearch = new List<string>((List<string>)toSearch);
                SourceItems = new List<string>(_toSearch);
            }

            if (parameter.TryGetValue("callBack", out object callBack))
            {
                var action = (Action<string>)callBack;
                _callBack = new Action<string>(action);
            }

            if(parameter.TryGetValue("title", out object title))
            {
                Title = $"Select {title.ToString()}";
                SearchPlaceHolder = $"Search {title.ToString()}";
            }

            return base.OnNavigatingTo(parameter);
        }




        public override Task OnNavigatedFrom(bool isForwardNavigation)
        {
            _callBack = null;
            return base.OnNavigatedFrom(isForwardNavigation);
        }

        
      
        private void SearchCollection()
        {

            var searchEntered = SearchBarText;
  
            if (string.IsNullOrEmpty(searchEntered))
            {
                searchEntered = string.Empty;
                SourceItems = new List<string>(_toSearch);
            }
            else
            {
                searchEntered = searchEntered.ToLowerInvariant();

                var filteredItems = _toSearch.Where(x => x.ToLowerInvariant().Contains(searchEntered)).ToList();

                SourceItems = new List<string>(filteredItems);
            }
            





            //foreach (var value in _toSearch)
            //{
            //    if (!filteredItems.Contains(value))
            //    {
            //        SourceItems.Remove(value);
            //    }
            //    else if (!SourceItems.Contains(value))
            //    {
            //        SourceItems.Add(value);
            //    }

            //}
        }
        


        #endregion Methods
    }

}

