using MessageSenderLib.Twilio;

namespace LibTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            TwilioCredentials tc;
            tc.accountSid = ""; // edit
            tc.authToken = ""; // edit
            tc.From = "+10000000000"; // edit

            TwilioSender ts = TwilioSender.GetInstance();
            //ISender<TwilioCredentials, MessageResource> is = TwilioSender.GetInstance();
            ts.ModifyCredentials(tc);

            //var message = ts.SendMessage("+420776000000", "this is a message for you, M.");
            var message = ts.SendMessage("+420723000000", "yet another message for you, I."); // edit
            Console.WriteLine(message.Body);

        }
    }
}