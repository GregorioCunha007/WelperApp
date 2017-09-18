using System.Threading;
using System.Threading.Tasks;
using ProjectTryout.Helpers;
using ProjectTryout.Model.Services;
using ProjectTryout.Views;


namespace ProjectTryout.ViewModelx
{
    public class WelcomePageViewModel : BaseViewModel
    {
        private readonly SemaphoreSlim _slim;
        private readonly IAppNavigation _navigation;
        public NotifyTaskCompletion<int> IsBusy { get; private set; }

        private bool _isLoaded;
        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                if (value)
                {
                    _slim.Release();
                }
            }
        }

        public WelcomePageViewModel(IAppNavigation navigation)
        {
            _slim = new SemaphoreSlim(0, 1);
            IsBusy = new NotifyTaskCompletion<int>(ChoosePageToNavigate());
            _navigation = navigation;
        }

        public async Task<int> ChoosePageToNavigate()
        {
            await _slim.WaitAsync();
            if (Settings.UserId != -1)
            {
                await _navigation.GoToCardExhibitPage();
            }
            else
            {
               await _navigation.GoToFormPage();
            }
            return 0;
        }
    }
}
