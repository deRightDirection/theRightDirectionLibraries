using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using theRightDirection.Data;

namespace theRightDirection.Library.Data.DropBox
{
    public class DropBoxRepository<T> : BaseRepository<T> where T : IEntity
    {
        private DropboxClient _dropBox;

        internal DropBoxRepository(DropboxClient dropboxClient, string fileName) : base(fileName)
        {
            _dropBox = dropboxClient;
        }

        protected async override Task<string> GetJsonFromFile()
        {
            try
            {
                using (var response = await _dropBox.Files.DownloadAsync(string.Empty + "/" + _fileName))
                {
                    return await response.GetContentAsStringAsync();
                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        protected async override Task<bool> SaveJsonToFile(string json)
        {
            try
            {
                using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    await _dropBox.Files.UploadAsync(string.Empty + "/" + _fileName, WriteMode.Overwrite.Instance, body: mem);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
