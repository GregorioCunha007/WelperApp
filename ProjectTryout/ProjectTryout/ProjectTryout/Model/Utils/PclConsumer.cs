using System.Threading.Tasks;
using ProjectTryout.Model.Services;
using Xamarin.Forms;

namespace ProjectTryout.Model.Utils
{
    public class PclConsumer 
    {
        public string FileName => "OldCards.txt";

        public async Task<bool> IsFileExistAsync()
        {
            return await PclService.IsFileExistAsync(FileName);
        }

        public async Task<bool> WriteTextAllAsync(string content)
        {
            return await PclService.WriteTextAllAsync(FileName,content);
        }

        public async Task<string> ReadAllTextAsync()
        {
            return await PclService.ReadAllTextAsync(FileName);
        }

        public async Task<bool> DeleteFileAsync()
        {
            return await PclService.DeleteFile(FileName);
        }
    }
}
