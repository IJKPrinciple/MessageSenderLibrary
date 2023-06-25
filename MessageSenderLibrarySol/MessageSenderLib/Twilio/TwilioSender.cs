using Twilio.Rest.Api.V2010.Account;
using Twilio;
using Twilio.Types;

namespace MessageSenderLib.Twilio
{
    /// <summary>
    /// Struct <c>TwilioCredentials</c> contains Account
    /// SID and Auth Token as accepted by the <c>TwilioClient.Init</c>
    /// method and also a from telephone number as accepted by
    /// <c>Twilio.Types.PhoneNumber</c> constructor.
    /// </summary>
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
        private static TwilioSender? _instance;
        private TwilioCredentials _creds;

        private TwilioSender() { }
        public static TwilioSender GetInstance() {
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

        /// <summary>
        /// Sends a message to the specified number.
        /// <c>ModifyCredentials</c> should be called beforehand to set the from number,
        /// among other things.
        /// </summary>
        /// <param name="to">To phone number as accepted by the
        /// <c>Twilio.Types.PhoneNumber</c> constructor.</param>
        /// <param name="message">Message as stored in
        /// <c>Twilio.Rest.Api.V2010.Account.CreateMessageOptions.Body</c></param>
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