using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectTryout.Model.Services
{
    public interface IAppNavigation
    {
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
        Task PushAsync(Page page);
        void InsertBefore(Page nextPage);
        Task PopAsync();
        Task GoToCardExhibitPage();
        Task GoToFormPage();
        Task ChoosePageToNavigate(IAction action);
    }
}
