using CoreFtp;
using Msmaldi.Financeiro.Website.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Msmaldi.Financeiro.Website.Data.ExternalProviders
{
    public class CetipMediaCDI : IDisposable
    {
        private readonly FtpClient _ftpClient;
        public CetipMediaCDI()
        {
            _ftpClient = new FtpClient(new FtpClientConfiguration
            {
                Host = "ftp.cetip.com.br",
                BaseDirectory = "MediaCDI"
            });
            _ftpClient.LoginAsync().Wait();
        }

        public async Task LoginAsync()
        {
            await _ftpClient.LoginAsync();
        }

        public async Task<List<DateTime>> DatasDisponiveisAsync()
        {
            var result = new List<DateTime>();

            var files = await _ftpClient.ListFilesAsync();
            foreach (var file in files)
            {
                var parsed = DateTime.TryParseExact(file.Name.Substring(0, 8),
                    "yyyyMMdd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime data);
                if (parsed)
                    result.Add(data);
            }
            return result;
        }

        public async Task<DIOver> ObterPorDataAsync(DateTime data)
        {
            var fileName = $"{data:yyyyMMdd}.txt";
            var stream = await _ftpClient.OpenFileReadStreamAsync(fileName);
            using (var reader = new StreamReader(stream))
            {
                var taxa = double.Parse(reader.ReadLine()) / 10000;
                return new DIOver(data, taxa);
            }
        }

        public void Dispose()
        {
            _ftpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
