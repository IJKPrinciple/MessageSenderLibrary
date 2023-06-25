namespace MessageSenderLib
{
    internal interface ISender<C, R>
    {
        void ModifyCredentials(C creds);

        R SendMessage(string to, string message);
    }
}
