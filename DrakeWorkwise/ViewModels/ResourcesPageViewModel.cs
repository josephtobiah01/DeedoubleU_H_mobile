
using DrakeWorkwise.Interfaces;
using DrakeWorkwise.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DrakeWorkwise.ViewModels
{
    public class ResourcesPageViewModel : CallUsViewModelBase
    {
        private readonly IGetDataService _getDataService;

        public ResourcesPageViewModel(INavigationService navigationService, IGetDataService getDataService) : base(navigationService)
        {
            _getDataService = getDataService;
            Initialize();
        }

        private async void Initialize()
        {
            LoadResourcesCommand = new Command<string>((res) =>
            {
                if (res.ToLower() == "art")
                {
                    if (!ArticlesSelected)
                    {
                        ExternalSelected = false;
                        ArticlesSelected = true;
                        //DisplayResource.Clear();
                        ////ClearList(Articles.ToList<DWHResources>());
                        DisplayResource = Articles.ToList<DWHResources>();
                    }
                }
                else
                {
                    if (!ExternalSelected)
                    {
                        ExternalSelected = true;
                        ArticlesSelected = false;
                        //DisplayResource.Clear();
                        ////ClearList(ExternalResources.ToList<DWHResources>());
                        DisplayResource = ExternalResources.ToList<DWHResources>();
                    }
                }
            });

            OpenResourceCommand = new Command<DWHResources>(async (res) =>
            {
                if (res.ResType == "Article")
                {
                    var art = (Article)res;
                    Dictionary<string, object> _parameters = new Dictionary<string, object>();
                    _parameters.Add("title", art.Title);
                    _parameters.Add("content", art.Content);
                    _parameters.Add("image", art.Image);
                    await NavigationService.NavigateTo("WebViewPage", _parameters);
                }
                else if (res.ResType == "ExternalResources")
                {
                    var ext = (ExternalResources)res;
                    Uri uri = new Uri(ext.Link);
                    await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                }
            });

            var rawData = await _getDataService.GetRawData();

            Articles = new List<Article>(SetCommands(rawData.Data.Resources.Articles.Where(x => !x.IsDeleted).OrderByDescending(x => x.PostedDate).ToList()));
            ExternalResources = new List<ExternalResources>(SetCommands(rawData.Data.Resources.ExternalResources.Where(x => !x.IsDeleted).ToList()));
            ArticlesSelected = true;
            DisplayResource = Articles.ToList<DWHResources>();
        }

        private IEnumerable<T> SetCommands<T>(IEnumerable<T> list) where T : DWHResources
        {
            list.ToList().ForEach(x =>
            {
                var dwh = (DWHResources)x;
                dwh.OpenResourceCommand = this.OpenResourceCommand;
            });
            return list;
        }

        private void ClearList(List<DWHResources> resources)
        {
            while(DisplayResource.Count > 1)
            {
                DisplayResource.RemoveAt(1);
            }

            DisplayResource.AddRange(resources);
            DisplayResource.RemoveAt(0);
        }


        private bool _articlesSelected = false;
        public bool ArticlesSelected
        {
            get { return _articlesSelected; }
            set
            {
                SetPropertyValue(ref _articlesSelected, value);
            }
        }

        private bool _externalSelected = false;
        public bool ExternalSelected
        {
            get { return _externalSelected; }
            set
            {
                SetPropertyValue(ref _externalSelected, value);
            }
        }

        private List<Article> _articles = null;

        public List<Article> Articles
        {
            get { return _articles; }
            set
            {
                SetPropertyValue(ref _articles, value);
            }
        }

        private List<ExternalResources> _externalResources = null;
        public List<ExternalResources> ExternalResources
        {
            get { return _externalResources; }
            set
            {
                SetPropertyValue(ref _externalResources, value);
            }
        }

        private List<DWHResources> _displayResource = null;
        public List<DWHResources> DisplayResource
        {
            get { return _displayResource; }
            set
            {
                SetPropertyValue(ref _displayResource, value);
            }
        }

        public Command<string> LoadResourcesCommand { get; set; }
        public Command<DWHResources> OpenResourceCommand { get; set; }


    }
}
