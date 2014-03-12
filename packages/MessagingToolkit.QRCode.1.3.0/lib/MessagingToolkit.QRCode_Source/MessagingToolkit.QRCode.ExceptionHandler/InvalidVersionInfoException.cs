namespace MessagingToolkit.QRCode.ExceptionHandler
{
    using System;

    [Serializable]
    public class InvalidVersionInfoException : VersionInformationException
    {
        internal string message = null;

        public InvalidVersionInfoException(string message)
        {
            this.message = message;
        }

        public override string Message
        {
            get
            {
                return this.message;
            }
        }
    }
}

