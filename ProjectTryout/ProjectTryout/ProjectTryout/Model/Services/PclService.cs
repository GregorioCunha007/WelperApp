using System;
using System.IO;
using System.Threading.Tasks;
using PCLStorage;

namespace ProjectTryout.Model.Services
{
    public static class PclService
    {

        public static async Task<bool> IsFileExistAsync(string fileName, IFolder rootFolder = null)
        {
            // get hold of the file system  
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult folderexist = await folder.CheckExistsAsync(fileName);
            // already run at least once, don't overwrite what's there  
            if (folderexist == ExistenceCheckResult.FileExists)
            {
                return true;

            }
            return false;
        }

        public static async Task<bool> IsFolderExistAsync(string folderName, IFolder rootFolder = null)
        {
            // get hold of the file system  
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            ExistenceCheckResult folderexist = await folder.CheckExistsAsync(folderName);
            // already run at least once, don't overwrite what's there  
            if (folderexist == ExistenceCheckResult.FolderExists)
            {
                return true;

            }
            return false;
        }

        public static async Task<IFolder> CreateFolder(string folderName, IFolder rootFolder = null)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
            return folder;
        }

        public static async Task<IFile> CreateFile(string filename, IFolder rootFolder = null)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            return file;
        }
        public static async Task<bool> WriteTextAllAsync(string filename, string content = "", IFolder rootFolder = null)
        {
            IFile file = await CreateFile(filename,rootFolder);
            await file.WriteAllTextAsync(content);
            return true;
        }

        public static async Task<string> ReadAllTextAsync(string fileName, IFolder rootFolder = null)
        {
            string content = "";
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            bool exist = await IsFileExistAsync(fileName,folder);
            if (exist)
            {
                IFile file = await folder.GetFileAsync(fileName);
                content = await file.ReadAllTextAsync();
            }
            return content;
        }
        public static async Task<bool> DeleteFile(string fileName, IFolder rootFolder = null)
        {
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;
            bool exist = await IsFileExistAsync(fileName,folder);
            if (exist)
            {
                IFile file = await folder.GetFileAsync(fileName);
                await file.DeleteAsync();
                return true;
            }
            return false;
        }

        public static async Task SaveImage(byte[] image, String fileName, IFolder rootFolder = null)
        {
            // get hold of the file system  
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;

            // create a file, overwriting any existing file  
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

            // populate the file with image data  
            using (Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                stream.Write(image, 0, image.Length);
            }
        }

        public static async Task<byte[]> LoadImage(byte[] image, String fileName, IFolder rootFolder = null)
        {
            // get hold of the file system  
            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;

            //open file if exists  
            IFile file = await folder.GetFileAsync(fileName);
            //load stream to buffer  
            using (Stream stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                long length = stream.Length;
                byte[] streamBuffer = new byte[length];
                stream.Read(streamBuffer, 0, (int)length);
                return streamBuffer;
            }

        }
    }
}
