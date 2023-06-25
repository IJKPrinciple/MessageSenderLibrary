using Twilio.Rest.Api.V2010.Account;
using Twilio;
using Twilio.Types;

namespace MessageSenderLib.Twilio
{
    public struct TwilioCredentials
    {
        public string accountSid;
        public string authToken;
        public string From
        {
            get => _from.ToString();
            set => _from = new PhoneNumber(value);
        }
        private PhoneNumber _from;
    }
    public class TwilioSender : ISender<TwilioCredentials, MessageResource>
    {
        private TwilioSender? _instance;
        private TwilioCredentials _creds;

        private TwilioSender() { }
        public TwilioSender GetInstance() {
            _instance ??= new TwilioSender();
            return _instance;
        }

        public void ModifyCredentials(TwilioCredentials creds)
        {
            _creds.accountSid = creds.accountSid;
            _creds.authToken = creds.authToken;
            _creds.From = creds.From;
            TwilioClient.Init(_creds.accountSid, _creds.authToken);
        }

        public MessageResource SendMessage(string to, string message)
        {
            var messageOptions = new CreateMessageOptions(
              new PhoneNumber(to));
            messageOptions.From = new PhoneNumber(_creds.From);
            messageOptions.Body = message;


            var retMessage = MessageResource.Create(messageOptions);
            return retMessage;
        }
    }
}