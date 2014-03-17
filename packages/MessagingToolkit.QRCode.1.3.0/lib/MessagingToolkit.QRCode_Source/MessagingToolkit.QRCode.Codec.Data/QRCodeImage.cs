namespace MessagingToolkit.QRCode.Codec.Data
{
    using System;

    public interface QRCodeImage
    {
        int GetPixel(int x, int y);

        int Height { get; }

        int Width { get; }
    }
}

