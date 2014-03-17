namespace MessagingToolkit.QRCode.Helper
{
    using MessagingToolkit.QRCode.Geom;
    using System;

    public interface DebugCanvas
    {
        void DrawCross(Point point, int color);
        void DrawLine(Line line, int color);
        void DrawLines(Line[] lines, int color);
        void DrawMatrix(bool[][] matrix);
        void DrawPoint(Point point, int color);
        void DrawPoints(Point[] points, int color);
        void DrawPolygon(Point[] points, int color);
        void Print(string str);
    }
}

