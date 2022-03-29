using Fatca.Core.Configurations;
using Fatca.Core.Utils;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Fatca.Core.Services
{
    /// <summary>
    /// For sending messages
    /// </summary>
    public class MessageService
    {
        private readonly IMessageService messageService;
        public MessageService(IMessageService service)
        {
            messageService = service;
        }

        public void Send()
        {
            messageService.Send();
        }
    }

    public interface IMessageService
    {
        Task<bool> Send();
    }

    public class SftpMessageService : IMessageService
    {
        private readonly string userName;
        private readonly string password;
        private readonly string remoteServer;
        private readonly FatcaSettings _settings;
        readonly ILogger<SftpMessageService> _logger;
        public SftpMessageService(
            FatcaSettings settings, ILogger<SftpMessageService> logger)
        {
            _settings = settings;
            userName = _settings.SftpUserName;
            password = _settings.Password;
            remoteServer = _settings.SftpRemoteServer;
            _logger = logger;
        }
        public Task<bool> Send()
        {
            return Task.Run(() =>
            {
                var session = SFTPManager.CreateSFTPSession(remoteServer, userName, password);
                string fileName = ""; //files to upload
                string result = SFTPManager.UploadFile(session, fileName);

                //Raise event for this result

                //Log result
                _logger.LogInformation($"SFTP Result: {result}");
                return !string.IsNullOrWhiteSpace(result);
            });



        }
    }
}
