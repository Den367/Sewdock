namespace MessagingToolkit.QRCode.ExceptionHandler
{
    using System;

    [Serializable]
    public class FinderPatternNotFoundException : Exception
    {
        internal string message = null;

        public FinderPatternNotFoundException(string message)
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

