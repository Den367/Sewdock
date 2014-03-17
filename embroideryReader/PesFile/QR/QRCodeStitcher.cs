using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using EmbroideryFile.QRCode;

namespace EmbroideryFile.QR
{
    public class QrCodeStitcher :  IQRCodeStitchGeneration
    {
        int _dimension ;
         int _dX;
         int _dY;
         int _cellSize;
/*
         string QrCodeText { get; set; }
*/

        QRCodeStitchInfo _info;
        readonly QRCodeCreator _qrcode;
        #region [Public Properties]
        public QRCodeStitchInfo Info { get { return _info; }
            set {

                SetInfo(value);
            }
        }
        #endregion [Properties]
        #region [Constructors]
        public QrCodeStitcher()
        {
            _qrcode = new QRCodeCreator();
        }

        void SetInfo(QRCodeStitchInfo info)
        {
            _info = info;
            _info.Matrix = _qrcode.GetQRCodeMatrix(info.QrCodeText);      
            _dimension = info.Dimension;
            _dX = info.dX;
            _dY = info.dY;
            _cellSize = info.cellSize;
            GetDesignXOffset = -(_dimension * _cellSize) / 2;
            GetDesignYOffset = GetDesignXOffset;
        }
        #endregion [Constructors]



        int GetRectSize()
        {
            bool[][] m = _info.Matrix;
            int i = 0;
            do {i++;}
            while (i < _dimension && m[0][i]);
            return i;
        }


        private List<List<Coords>> GenerateQRCodeStitchesBoxed()
        {
            var blocks = new List<List<Coords>>();
            int rectSize = GetRectSize();
            // rectangle left top
            blocks.AddRange(GetRectangleSatin(0, 0, rectSize - 1, rectSize - 1));
            blocks.Add(GenerateBoxStitchBlock(2, 2, rectSize - 4));
            blocks.AddRange(GetSatinStitches(GetLaneList(0, rectSize + 1, rectSize, _dimension - rectSize - 1)));
            blocks.AddRange(GetRectangleSatin(0, _dimension - rectSize, rectSize - 1, _dimension - 1));
            blocks.Add(GenerateBoxStitchBlock(2, _dimension - rectSize + 2, rectSize - 4));


            blocks.AddRange(GetSatinStitches(GetLaneList(rectSize + 1, 0, _dimension - rectSize - 1, _dimension - 1)));

            blocks.AddRange(GetRectangleSatin(_dimension - rectSize, 0, _dimension - 1, rectSize - 1));
            blocks.Add(GenerateBoxStitchBlock(_dimension - rectSize + 2, 2, rectSize - 4));
            blocks.AddRange(GetSatinStitches(GetLaneList(_dimension - rectSize, rectSize + 1, _dimension - 1, _dimension - 1)));
            return blocks;
        }

/*
        public List<List<Coords>> GenerateQRCodeBoxedTiledStitches()
        {
            var blocks = new List<List<Coords>>();
            int rectSize = GetRectSize();
            // rectangle left top
            blocks.AddRange(GetRectangleSatin(0, 0, rectSize - 1, rectSize - 1));
            blocks.Add(GenerateBoxStitchBlock(2, 2, rectSize - 4));
            blocks.AddRange(GetLaneCellSatinStitches(GetLaneList(0, rectSize + 1, rectSize, _dimension - rectSize - 1)));
            blocks.AddRange(GetRectangleSatin(0, _dimension - rectSize, rectSize - 1, _dimension - 1));
            blocks.Add(GenerateBoxStitchBlock(2, _dimension - rectSize + 2, rectSize - 4));


            blocks.AddRange(GetLaneCellSatinStitches(GetLaneList(rectSize + 1, 0, _dimension - rectSize - 1, _dimension - 1)));

            blocks.AddRange(GetRectangleSatin(_dimension - rectSize, 0, _dimension - 1, rectSize - 1));
            blocks.Add(GenerateBoxStitchBlock(_dimension - rectSize + 2, 2, rectSize - 4));
            blocks.AddRange(GetLaneCellSatinStitches(GetLaneList(_dimension - rectSize, rectSize + 1, _dimension - 1, _dimension - 1)));
            return blocks;
        }
*/
        /// <summary>
        /// Create stitches for filled box
        /// </summary>
        /// <param name="cellHorizonPos">Horisontal position of top left cell of box</param>
        /// <param name="cellVerticalPos">Vertical postion of top left cell of box</param>
        /// <param name="boxSize"></param>
        /// <returns></returns>
        private List<Coords> GenerateBoxStitchBlock(int cellHorizonPos, int cellVerticalPos, int boxSize)
        {
            var block = new List<Coords>();

            int y = 0;
            int x = 0;
            int startX = cellHorizonPos * _cellSize;
            int startY = cellVerticalPos * _cellSize;
            block.Add(new Coords { X = startX, Y = startY });
            while (y < _cellSize * boxSize)
            {
                while (x < _cellSize * boxSize - _dX)
                {
                    x = x + _dX;
                    block.Add(new Coords{ X = startX + x, Y = startY + y });
                }
                x = boxSize * _cellSize;
                block.Add(new Coords { X = startX + x, Y = startY + y });
                y = y + _dY;
                while (x > _dX)
                {
                    x = x - _dX;
                    block.Add(new Coords { X = startX + x, Y = startY + y });
                }
                x = 0;
                block.Add(new Coords { X = startX + x, Y = startY + y });
                y = y + _dY;
            }

            return block;
        }

        private IEnumerable<Coords> GenerateBoxStitchBlockDir(int cellHorizonPos, int cellVerticalPos, int boxSize, Direction dir)
        {
            var block = new List<Coords>();

            int startX, startY;
            int y = 0;
            int x = 0;

            switch (dir)
            {
                case Direction.Down:
                    startX = cellHorizonPos * _cellSize;
                    startY = cellVerticalPos * _cellSize;
                    block.Add(new Coords { X = startX, Y = startY });
                    while (y < _cellSize * boxSize)
                    {
                        while (x < _cellSize * boxSize - _dX)
                        {
                            x = x + _dX;
                            block.Add(new Coords { X = startX + x, Y = startY + y });
                        }
                        x = boxSize * _cellSize;
                        block.Add(new Coords { X = startX + x, Y = startY + y });
                        y = y + _dY;
                        while (x > _dX)
                        {
                            x = x - _dX;
                            block.Add(new Coords { X = startX + x, Y = startY + y });
                        }
                        x = 0;
                        block.Add(new Coords { X = startX + x, Y = startY + y });
                        y = y + _dY;
                    }

                    break;

                case Direction.Up:
                    startX = cellHorizonPos * _cellSize;
                    startY = (cellVerticalPos + 1) * _cellSize;
                    block.Add(new Coords { X = startX, Y = startY });
                    y = _cellSize * boxSize;
                    while (y > 0)
                    {
                        while (x < _cellSize * boxSize - _dX)
                        {
                            x = x + _dX;
                            block.Add(new Coords{ X = startX + x, Y = startY + y });
                        }
                        x = boxSize * _cellSize;
                        block.Add(new Coords { X = startX + x, Y = startY + y });
                        y = y - _dY;
                        while (x > _dX)
                        {
                            x = x - _dX;
                            block.Add(new Coords { X = startX + x, Y = startY + y });
                        }
                        x = 0;
                        block.Add(new Coords { X = startX + x, Y = startY + y });
                        y = y - _dY;
                    }
                    break;
                case Direction.Right:
                    startX = cellHorizonPos * _cellSize;
                    startY = cellVerticalPos * _cellSize;
                    block.Add(new Coords { X = startX, Y = startY });
                    while (x < _cellSize * boxSize)
                    {
                        while (y < _cellSize * boxSize - _dY)
                        {
                            y = y + _dY;
                            block.Add(new Coords { Y = startY + y, X = startX + x });
                        }
                        y = boxSize * _cellSize;
                        block.Add(new Coords { Y = startY + y, X = startX + x });
                        x = x + _dX;
                        while (y > _dX)
                        {
                            y = y - _dX;
                            block.Add(new Coords { Y = startY + y, X = startX + x });
                        }
                        y = 0;
                        block.Add(new Coords { Y = startY + y, X = startX + x });
                        x = x + _dX;
                    }

                    break;

                case Direction.Left:
                    startX = (cellHorizonPos) * _cellSize;
                    startY = cellVerticalPos * _cellSize;
                    block.Add(new Coords { X = startX, Y = startY });
                    x = _cellSize * boxSize;
                    int dV = _dY, dW = _dX;
                    while (x > 0)
                    {
                        while (y < _cellSize * boxSize - dW)
                        {
                            y = y + dW;
                            block.Add(new Coords { Y = startY + y, X = startX + x });
                        }
                        y = boxSize * _cellSize;
                        block.Add(new Coords { Y = startY + y, X = startX + x });
                        x = x - dV;
                        while (y > dV)
                        {
                            y = y - dV;
                            block.Add(new Coords { Y = startY + y, X = startX + x });
                        }
                        y = 0;
                        block.Add(new Coords { Y = startY + y, X = startX + x });
                        x = x - dV;
                    }
                    break;
            }


            return block;
        }

        void Init()
        {
            _lines.Clear();
         _cells = _info.Matrix;
         _state = true;
         _endLaneFlag = false;
         _laneLen = 1;
         if (_cells != null) ;
         _curLane.Lowest = false;
        }


        List<CoordsBlock> GetListCoordsBlock(IEnumerable<List<Coords>> listCoords)
        {
            if (listCoords == null) return null;
            var result = new List<CoordsBlock>();
            foreach (var listCoord in listCoords)
            { 
                var block = new CoordsBlock{color = Color.Black};
                block.AddRange(listCoord);
                result.Add(block);
            }
            return result;

        }
        #region [Public Methods]
        public List<List<Coords>> GetQRCodeStitches()
        {
           
            // return GetDownwardSatinStitches(GetLaneList());
           
                Init();
                return GenerateQRCodeStitchesBoxed();
    
        }


        public List<CoordsBlock> GetQRCodeStitchBlocks()
        {

            return GetListCoordsBlock(GetQRCodeStitches());
        }


        public int GetDesignXOffset { get; private set; }

        public int GetDesignYOffset { get; private set; }

        #endregion [Public Methods]

        #region [Private block]
        IEnumerable<List<Coords>> GetRectangleSatin(int x1, int y1, int x2, int y2)
        {
            int LeftX = (x1 > x2) ? x2 : x1;
            int TopY = (y1 > y2) ? y2 : y1;
            int RightX = (x1 < x2) ? x2 : x1;
            var BottomY = (y1 < y2) ? y2 : y1;
            int length = RightX - LeftX;
            var rect = new List<List<Coords>>();
            rect.Add(GenerateVerticalColumnStitchBlock(LeftX, TopY, length));
            rect.Add(GenerateHorizonColumnStitchBlock(LeftX, BottomY, length));
            rect.Add(ReverseCoords(GenerateVerticalColumnStitchBlock(RightX, TopY + 1, length)));
            rect.Add(ReverseCoords(GenerateHorizonColumnStitchBlock(LeftX + 1, TopY, length)));
            return rect;

        }

        List<Coords> ReverseCoords(List<Coords> coords)
        {
            coords.Reverse();
            return coords;
        }

        /// <summary>
        /// Generates stitches for vertical lane according to position of Dots
        /// </summary>
        /// <param name="cellHorizonPos"></param>
        /// <param name="cellVerticalPos"></param>
        /// <param name="length"></param>
        private List<Coords> GenerateVerticalColumnStitchBlock(int cellHorizonPos, int cellVerticalPos, int length)
        {
            var block = new List<Coords>();
            int curX, curY;
            int columnLength = _cellSize * length;
            int startX = cellHorizonPos * _cellSize;
            int startY = cellVerticalPos * _cellSize;
            for (curY = 0; curY < columnLength; curY = curY + _dY)
            {
                for (curX = (curY == 0) ? 0 : _dX; (curX < _cellSize) && (curY < columnLength); curX = curX + _dX)
                {
                    block.Add(new Coords { X = startX + curX, Y = startY + curY });
                    curY = curY + _dY;
                }
                int edgedX = _cellSize - (curX - _dX);
                int edgedY = edgedX * _dY / _dX;
                curX = _cellSize;
                curY = curY + edgedY - _dY;
                block.Add(new Coords { X = startX + curX, Y = startY + curY });
                curY = curY + _dY;
                for (curX = _cellSize - _dX; (curX > 0) && (curY < columnLength); curX = curX - _dX)
                {
                    block.Add(new Coords { X = startX + curX, Y = startY + curY });
                    curY = curY + _dY;
                }
                edgedX = curX + _dX;
                edgedY = edgedX * _dY / _dX;
                curY = curY + edgedY - _dY;
                block.Add(new Coords { X = startX, Y = startY + curY });
            }
            curX = _cellSize;
            curY = columnLength;
            block.Add(new Coords { X = startX + curX, Y = startY + curY });
            block.Add(new Coords { X = startX, Y = startY + curY });
            return block;
        }
        private List<Coords> GenerateVerticalTiledColumnStitchBlock(int cellHorizonPos, int cellVerticalPos, int length)
        {
            var block = new List<Coords>();
            for (int i = cellVerticalPos; i < cellVerticalPos + length; i++)
            {
                block.AddRange((i + cellHorizonPos)%2 == 0
                                   ? GenerateBoxStitchBlockDir(cellHorizonPos, i, 1, Direction.Down)
                                   : GenerateBoxStitchBlockDir(cellHorizonPos, i, 1, Direction.Left));
            }
            return block;
        }
        private List<Coords> GenerateHorizonColumnStitchBlock(int cellHorizonPos, int cellVerticalPos, int Length)
        {
            var block = new List<Coords>();

            int curX, curY;
            int columnLength = _cellSize * Length;
            int startX = cellHorizonPos * _cellSize;
            int startY = cellVerticalPos * _cellSize;

            for (curX = 0; curX < columnLength; curX = curX + _dY)
            {
                for (curY = (curX == 0) ? 0 : _dX; (curY < _cellSize) && (curX < columnLength); curY = curY + _dX)
                {
                    block.Add(new Coords { X = startX + curX, Y = startY + curY });
                    curX = curX + _dY;
                }

                int edgedY = _cellSize - (curY - _dX);
                int edgedX = edgedY * _dY / _dX;
                curY = _cellSize;
                curX = curX + edgedX - _dY;
                block.Add(new Coords{ X = startX + curX, Y = startY + curY });
                curX = curX + _dY;
                for (curY = _cellSize - _dX; (curY > 0) && (curX < columnLength); curY = curY - _dX)
                {
                    block.Add(new Coords { X = startX + curX, Y = startY + curY });
                    curX = curX + _dY;
                }

                edgedY = curY + _dX;
                edgedX = edgedY * _dY / _dX;
                curX = curX + edgedX - _dY;
                block.Add(new Coords{ X = startX + curX, Y = startY });
            }
            curY = _cellSize;
            curX = columnLength;
            block.Add(new Coords { X = startX + curX, Y = startY + curY });  // right bottom edge of column
            block.Add(new Coords() { X = startX + curX, Y = startY });       // 
            return block;
        }
/*
        private List<Coords> GenerateHorizonTiledColumnStitchBlock(int CellHorizonPos, int CellVerticalPos, int Length)
        {
            List<Coords> block = new List<Coords>();
            for (int i = CellVerticalPos; i < CellVerticalPos + Length; i++)
            {
                if (i % 2 == 0) block.AddRange(GenerateBoxStitchBlockDir(CellHorizonPos, i, 1, Direction.Down));
                else block.AddRange(GenerateBoxStitchBlockDir(CellHorizonPos, i, 1, Direction.Left));
            }
            return block;
        }
*/

        readonly List<Lane> _lines = new List<Lane>();
        Lane _curLane;
        Coords _dot1;
        Coords _dot2;
        bool[][] _cells;
        bool _state;
        bool _endLaneFlag;
        int _laneLen;
/*
        void ConsumeCell(int j, int i)
        {
            if (_cells[j][i] == true)
            {
                // begin lane at the top
                if ((i == 0))
                {
                    _dot1.X = j;
                    _dot1.Y = i; //= new Coords() { X = i, Y = j };
                    _curLane.Dot1 = _dot1;
                    _laneLen = 1;
                    _state = true;
                }
                else if ((_state == false))
                {
                    // single dot at the bottom
                    if (i == _len)
                    {
                        _dot1 = new Coords { X = j, Y = i };
                        _curLane.Dot1 = _dot1;
                        _dot2 = new Coords {X = j, Y = i};
                        _curLane.Dot2 = _dot2;
                        _curLane.Length = 1;
                        _curLane.Lowest = true;
                        _endLaneFlag = true;
                    }
                    // begin lane
                    else
                    {
                        _dot1.X = j;
                        _dot1.Y = i; //= new Coords() { X = i, Y = j };
                        _curLane.Dot1 = _dot1;
                        _laneLen = 1;
                        _state = true;
                    }
                }
                else if ((i == _len))
                {
                    //   end of lane at the bottom
                    _dot2 = new Coords() { X = j, Y = i };
                    _curLane.Dot2 = _dot2;
                    _curLane.Length = ++_laneLen;
                    _curLane.Lowest = true;
                    _endLaneFlag = true;
                }  // in lane
                else
                {
                    _laneLen++;
                }
            }
            // end lane not an edge
            else if (_state)
            {
                _dot2 = new Coords() { X = j, Y = i - 1 };
                _curLane.Dot2 = _dot2;
                _curLane.Length = _laneLen;
                _state = false;
                _endLaneFlag = true;
            }
            if (_endLaneFlag == true)
            {
                _lines.Add(_curLane);
                _endLaneFlag = false;
            }


        }
*/

        int _topY, _leftX, _bottomY, _rightX;

        void ConsumeRelativeCellDown(int j, int i)
        {
            if (_cells[j][i] == true)
            {
                // begin lane at the top of part
                if ((i == _topY))
                {
                    _dot1 = new Coords() { X = j, Y = i };
                    
                    _curLane.Dot1 = _dot1;
                    _laneLen = 1;
                    _state = true;
                }
                else if ((_state == false))
                {
                    // single dot at the bottom of 
                    if (i == _bottomY)
                    {
                        _dot1 = new Coords() { X = j, Y = i };
                        _curLane.Dot1 = _dot1;
                        _dot2 = new Coords() { X = j, Y = i };
                        _curLane.Dot2 = _dot2;
                        _curLane.Length = 1;
                        _curLane.Lowest = true;
                        _endLaneFlag = true;
                    }
                    // begin lane
                    else
                    {
                        _dot1 = new Coords() { X = j, Y = i };                        
                        _curLane.Dot1 = _dot1;
                        _laneLen = 1;
                        _state = true;
                    }
                }
                else if ((i == _bottomY))
                {
                    //   end of lane at the bottom
                    _dot2 = new Coords() { X = j, Y = i };
                    _curLane.Dot2 = _dot2;
                    _curLane.Length = ++_laneLen;
                    _curLane.Lowest = true;
                    _endLaneFlag = true;
                }  // in lane
                else
                {
                    _laneLen++;
                }
            }
            // end lane not an edge
            else if (_state == true)
            {
                _dot2 = new Coords() { X = j, Y = i - 1 };
                _curLane.Dot2 = _dot2;
                _curLane.Length = _laneLen;
                _state = false;
                _endLaneFlag = true;
            }
            if (_endLaneFlag == true)
            {
                _lines.Add(_curLane);
                _endLaneFlag = false;
            }


        }

        void ConsumeRelativeCellUp(int j, int i)
        {
            if (_cells[j][i] == true)
            {
                // begin lane at the bottom of part
                if ((i == _bottomY))
                {

                    _dot1 = new Coords { X = j, Y = i };
                    _curLane.Dot1 = _dot1;
                    _laneLen = 1;
                    _state = true;
                }
                else if ((_state == false))
                {
                    // single dot at the top of part
                    if (i == _topY)
                    {
                        _dot1 = new Coords { X = j, Y = i };
                        _curLane.Dot1 = _dot1;
                        _dot2 = new Coords { X = j, Y = i };
                        _curLane.Dot2 = _dot2;
                        _curLane.Length = 1;
                        _curLane.Lowest = true;
                        _endLaneFlag = true;
                    }
                    // begin lane
                    else
                    {
                        _dot1 = new Coords { X = j, Y = i };
                        _curLane.Dot1 = _dot1;
                        _laneLen = 1;
                        _state = true;
                    }
                }
                else if ((i == _topY))
                {
                    //   end of lane at the top
                    _dot2 = new Coords { X = j, Y = i };
                    _curLane.Dot2 = _dot2;
                    _curLane.Length = ++_laneLen;
                    _curLane.Lowest = true;
                    _endLaneFlag = true;
                }  // in lane
                else
                {
                    _laneLen++;
                }
            }
            // end lane not an edge
            else if (_state)
            {
                _dot2 = new Coords { X = j, Y = i + 1 };
                _curLane.Dot2 = _dot2;
                _curLane.Length = _laneLen;
                _state = false;
                _endLaneFlag = true;
            }
            if (_endLaneFlag)
            {
                _lines.Add(_curLane);
                _endLaneFlag = false;
            }


        }

/*
        /// <summary>
        /// Generates list of lane from unbreakable d sequence of code dots in from top to bottom side of QR Code
        /// </summary>
        /// <returns>Returns list Lane with coords of start, end Dot and length of sequence</returns>
        private List<Lane> GetLaneList()
        {



            for (int j = 0; j <= _len; j = j + 2)  //X
            {
                for (int i = 0; i <= _len; i++) // Y               
                {
                    ConsumeCell(j, i);
                }
                if (j >= _len) break;
                for (int i = _len; i >= 0; i--) // Y               
                {
                    ConsumeCell(j + 1, i);
                }
            }
            return _lines;
        }
*/

        private List<Lane> GetLaneList(int x1, int y1, int x2, int y2)
        {
            try
            {
 if (_lines != null) _lines.Clear();
            if (y1 > y2)
            {
                _topY = y2; _bottomY = y1;
            }
            else { _topY = y1; _bottomY = y2; }
            if (x1 > x2)
            {
                _leftX = x2; _rightX = x1;
            }
            else
            {
                _leftX = x1; _rightX = x2;
            }

            for (int j = _leftX; j <= _rightX; j = j + 2)  //X
            {
                _state = false;
                for (int i = _topY; i <= _bottomY; i++) // Y               
                {
                    ConsumeRelativeCellDown(j, i);
                }
                if (j >= _rightX) break;
                _state = false;
                for (int i = _bottomY; i >= _topY; i--) // Y               
                {
                    ConsumeRelativeCellUp(j + 1, i);
                }
            }
            return _lines;
            }
            catch (Exception ex)
            {
                {}
                throw ex;
            }
           
        }



        /// <summary>
        /// Generates sequence of stitch blocks (columns) 
        /// </summary>


        private List<List<Coords>> GetSatinStitches(List<Lane> lanes)
        {
            List<List<Coords>> blockList = new List<List<Coords>>();
            int ln = 0;
            foreach (var lane in lanes)
            {
                blockList.Add(lane.Dot2.Y > lane.Dot1.Y
                                  ? GenerateVerticalColumnStitchBlock(lane.Dot1.X, lane.Dot1.Y, lane.Length)
                                  : ReverseCoords(GenerateVerticalColumnStitchBlock(lane.Dot2.X, lane.Dot2.Y,
                                                                                    lane.Length)));
                ln++;
            }
            return blockList;
        }


        private List<List<Coords>> GetLaneCellSatinStitches(List<Lane> lanes)
        {
            List<List<Coords>> blockList = new List<List<Coords>>();
            int ln = 0;
            foreach (var lane in lanes)
            {
                if (lane.Dot2.Y > lane.Dot1.Y)
                    blockList.Add(GenerateVerticalTiledColumnStitchBlock(lane.Dot1.X, lane.Dot1.Y, lane.Length));
                else blockList.Add(ReverseCoords(GenerateVerticalTiledColumnStitchBlock(lane.Dot2.X, lane.Dot2.Y, lane.Length)));
                ln++;

            }
            return blockList;
        }
  

        /// <summary>
        /// Converts <see cref="List<List<Coords>>"/> to <see cref="List<CoordsBlock>"/>
        /// </summary>
        /// <param name="listBlocks"></param>
        /// <returns></returns>
        List<CoordsBlock> GetListOfCoordsBlock(List<List<Coords>> listBlocks)
        {

            List<CoordsBlock> reslult = new List<CoordsBlock>();
            CoordsBlock coordsBlock;
            CoordsBlock prevBlock = null;
            foreach (var block in listBlocks)
            {
                if (prevBlock != null)
                {
                    var jumpBlock = new CoordsBlock() { Jumped = true};
                    jumpBlock.Add(prevBlock.Last());
                    jumpBlock.Add(block.First());
                    reslult.Add(jumpBlock);
                }
                coordsBlock = new CoordsBlock();
                foreach (var coords in block)                                  
                    coordsBlock.Add(coords);
                reslult.Add(coordsBlock);
                prevBlock = coordsBlock;

            }
            return reslult;
        }

       
        #endregion [Private block]

       
    }
}
