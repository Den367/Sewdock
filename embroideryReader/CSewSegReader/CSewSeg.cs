using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace CSewSegReader
{
    public  class CSewSeg
    {
        BinaryReader _file;
        int blockCount = 0;
        int stitchCount = 0;
        int colorCount = 0;
        public CSewSeg(BinaryReader file)
        {
            _file = file;
          _file.BaseStream.Position = 0x68;

        }

        public void Read()
        {

            ReadBlocks();
            
            ReadColorTable();

            ReadLA();
        }


        void ReadBlocks()
        {
            while (ReadBlock()) { };
            Trace.WriteLine(string.Format("Total blocks: {0}",blockCount));
        }

        bool ReadBlock()
        {

            blockCount++;
            stitchCount = 0;
            Trace.Write(string.Format("Block: {0}",blockCount));
            ReadColorInfo();

            ReadStitches(ReadStitchesCount());
         Trace.WriteLine(string.Format("Found {0} stitches", stitchCount));
            return ReadEndOfBlock();
   
        }

        void ReadColorInfo()
        {
           ReadColorChange();
           ReadColorIndex(); 
        }

        void ReadColorChange()
        {
          short index = _file.ReadInt16();
          Trace.Write(string.Format("ColorChange: {0} ", index ));
        }

        short ReadColorIndex()
        {
             short index = _file.ReadInt16();
             Trace.Write(string.Format("ColorIndex: {0} ", index ));
            return index;
        }

         short ReadStitchesCount()
        {
            short index = _file.ReadInt16();
          Trace.WriteLine(string.Format("Stitches in Block: {0}", index ));
            return  index;
           
        }

        void ReadStitches(short count)
        
        {
            
        for(int i = 0 ; i < count  ; i++)
        
       if (!ReadStitch()) break;

        }
        

        bool ReadStitch()
        {
            short x,y;
         x = _file.ReadInt16();
            if(x != -32765)
            {y = _file.ReadInt16();
            }
            else return false;
            stitchCount++;
            Trace.WriteLine(string.Format("X:{0} Y:{1}",x,y)); 
            return true;
        }

        bool ReadEndOfBlock()
        {
            short index = _file.ReadInt16();
            
            if (index == -32765)
            {
                Trace.WriteLine(string.Format("Found Block separator: {0}", index));
                return true;
            }
            else if (index == 0)
            {
                Trace.WriteLine(string.Format("Stitch blocks are done: {0}", index));
                
            }
            else Trace.WriteLine(string.Format("Unexpected End of Block: {0}", index));
            return false;
        }

        void ReadColorTable()
        {
            short index ;
            short count ;
            do{
                index =   ReadColorIndex();
                count =  ReadBlocksCount();
                colorCount++;
            }
            while(!((index == 0) && (count== 0) && _file.BaseStream.CanRead));

            Trace.WriteLine(string.Format("Total Color Changes: {0} ", colorCount));
        }
        
        void ReadLA()
        {
         short index = _file.ReadInt16();
          Trace.WriteLine(string.Format("End of CSwqSeg reached: {0}", index ));
          
        }
        short ReadBlocksCount()
        {
         short index = _file.ReadInt16();
          Trace.WriteLine(string.Format("Blocks count: {0}", index ));
            return  index;
        }


    }
}
