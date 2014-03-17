namespace MessagingToolkit.QRCode.Geom
{
    using System;

    public class SamplingGrid
    {
        private AreaGrid[][] grid;

        public SamplingGrid(int sqrtNumArea)
        {
            this.grid = new AreaGrid[sqrtNumArea][];
            for (int i = 0; i < sqrtNumArea; i++)
            {
                this.grid[i] = new AreaGrid[sqrtNumArea];
            }
        }

        public virtual void Adjust(Point adjust)
        {
            int x = adjust.X;
            int y = adjust.Y;
            for (int i = 0; i < this.grid[0].Length; i++)
            {
                for (int j = 0; j < this.grid.Length; j++)
                {
                    for (int k = 0; k < this.grid[j][i].XLines.Length; k++)
                    {
                        this.grid[j][i].XLines[k].Translate(x, y);
                    }
                    for (int m = 0; m < this.grid[j][i].YLines.Length; m++)
                    {
                        this.grid[j][i].YLines[m].Translate(x, y);
                    }
                }
            }
        }

        public virtual int GetHeight()
        {
            return this.grid.Length;
        }

        public virtual int GetHeight(int ax, int ay)
        {
            return this.grid[ax][ay].Height;
        }

        public virtual int GetWidth()
        {
            return this.grid[0].Length;
        }

        public virtual int GetWidth(int ax, int ay)
        {
            return this.grid[ax][ay].Width;
        }

        public virtual int GetX(int ax, int x)
        {
            int num = x;
            for (int i = 0; i < ax; i++)
            {
                num += this.grid[i][0].Width - 1;
            }
            return num;
        }

        public virtual Line GetXLine(int ax, int ay, int x)
        {
            return this.grid[ax][ay].GetXLine(x);
        }

        public virtual Line[] GetXLines(int ax, int ay)
        {
            return this.grid[ax][ay].XLines;
        }

        public virtual int GetY(int ay, int y)
        {
            int num = y;
            for (int i = 0; i < ay; i++)
            {
                num += this.grid[0][i].Height - 1;
            }
            return num;
        }

        public virtual Line GetYLine(int ax, int ay, int y)
        {
            return this.grid[ax][ay].GetYLine(y);
        }

        public virtual Line[] getYLines(int ax, int ay)
        {
            return this.grid[ax][ay].YLines;
        }

        public virtual void InitGrid(int ax, int ay, int width, int height)
        {
            this.grid[ax][ay] = new AreaGrid(this, width, height);
        }

        public virtual void SetXLine(int ax, int ay, int x, Line line)
        {
            this.grid[ax][ay].SetXLine(x, line);
        }

        public virtual void SetYLine(int ax, int ay, int y, Line line)
        {
            this.grid[ax][ay].SetYLine(y, line);
        }

        public virtual int TotalHeight
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.grid[0].Length; i++)
                {
                    num += this.grid[0][i].Height;
                    if (i > 0)
                    {
                        num--;
                    }
                }
                return num;
            }
        }

        public virtual int TotalWidth
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.grid.Length; i++)
                {
                    num += this.grid[i][0].Width;
                    if (i > 0)
                    {
                        num--;
                    }
                }
                return num;
            }
        }

        private class AreaGrid
        {
            private SamplingGrid enclosingInstance;
            private Line[] xLine;
            private Line[] yLine;

            public AreaGrid(SamplingGrid enclosingInstance, int width, int height)
            {
                this.InitBlock(enclosingInstance);
                this.xLine = new Line[width];
                this.yLine = new Line[height];
            }

            public virtual Line GetXLine(int x)
            {
                return this.xLine[x];
            }

            public virtual Line GetYLine(int y)
            {
                return this.yLine[y];
            }

            private void InitBlock(SamplingGrid enclosingInstance)
            {
                this.enclosingInstance = enclosingInstance;
            }

            public virtual void SetXLine(int x, Line line)
            {
                this.xLine[x] = line;
            }

            public virtual void SetYLine(int y, Line line)
            {
                this.yLine[y] = line;
            }

            public SamplingGrid EnclosingInstance
            {
                get
                {
                    return this.enclosingInstance;
                }
            }

            public virtual int Height
            {
                get
                {
                    return this.yLine.Length;
                }
            }

            public virtual int Width
            {
                get
                {
                    return this.xLine.Length;
                }
            }

            public virtual Line[] XLines
            {
                get
                {
                    return this.xLine;
                }
            }

            public virtual Line[] YLines
            {
                get
                {
                    return this.yLine;
                }
            }
        }
    }
}

