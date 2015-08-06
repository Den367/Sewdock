

namespace IntersectionArea
{
    internal class SquareOfRectIntersectonCalculator
    {
        private int FirstLeftTopX, FirstLeftTopY;
        private int FirstRightTopX, FirstRightTopY;
        private int FirstLeftBottomX, FirstLeftBottomY;
        private int FirstRightBottomX, FirstRightBottomY;

        private int SecondLeftTopX, SecondLeftTopY;
        private int SecondRightTopX, SecondRightTopY;
        private int SecondLeftBottomX, SecondLeftBottomY;
        private int SecondRightBottomX, SecondRightBottomY;

        private int ResultLeftTopX, ResultLeftTopY;
        private int ResultRightTopX, ResultRightTopY;
        private int ResultLeftBottomX, ResultLeftBottomY;
        private int ResultRightBottomX, ResultRightBottomY;


        public int solution(int[] vert)
        {
            SetRectFrames(vert);
            SetResultRectFrame();
            if (CheckIntersection()) return CalcSquare();
            else return -1;
        }
        private void SetRectFrames(int[] vert)
        {
            // setting first rectangle X coords 
            if (vert[0] > vert[2])
            {
                FirstLeftTopX = vert[2];
                FirstRightTopX = vert[0];
            }
            else
            {
                FirstLeftTopX = vert[0];
                FirstRightTopX = vert[2];
            }
            FirstLeftBottomX = FirstLeftTopX;
            FirstRightBottomX = FirstRightTopX;
            // setting second rectangle X coords
            if (vert[4] > vert[6])
            {
                SecondLeftTopX = vert[6];
                SecondRightTopX = vert[4];
            }
            else
            {
                SecondLeftTopX = vert[4];
                SecondRightTopX = vert[6];
            }
            SecondLeftBottomX = SecondLeftTopX;
            SecondRightBottomX = SecondRightTopX;

            // setting first rectangle Y coords 
            if (vert[1] > vert[3])
            {
                FirstLeftTopY = vert[1];
                FirstLeftBottomY = vert[3];
            }
            else
            {
                FirstLeftTopY = vert[3];
                FirstLeftBottomY = vert[1];
            }
            FirstRightTopY = FirstLeftTopY;
            FirstRightBottomY = FirstLeftBottomY;
            // setting second rectangle Y coords
            if (vert[5] > vert[7])
            {
                SecondLeftTopY = vert[5];
                SecondLeftBottomY = vert[7];
            }
            else
            {
                SecondLeftTopY = vert[7];
                SecondLeftBottomY = vert[5];
            }
            SecondRightTopY = SecondLeftTopY;
            SecondRightBottomY = SecondLeftBottomY;

        }

        void SetResultRectFrame()
        {
            if (FirstLeftTopX > SecondLeftTopX)
            {
                ResultLeftTopX = FirstLeftTopX;
            }
            else { ResultLeftTopX = SecondLeftTopX; }
            ResultLeftBottomX = ResultLeftTopX;

            if (FirstRightTopX < SecondRightTopX)
            {
                ResultRightTopX = FirstRightTopX;
            }
            else { ResultRightTopX = SecondRightTopX; }
            ResultRightBottomX = ResultRightTopX;

            if (FirstLeftTopY < SecondLeftTopY)
            {
                ResultLeftTopY = FirstLeftTopY;
            }
            else { ResultLeftTopY = SecondLeftTopY; }
            ResultRightTopY = ResultLeftTopY;

            if (FirstLeftBottomY > SecondLeftBottomY)
            {
                ResultLeftBottomY = FirstLeftBottomY;
            }
            else { ResultLeftBottomY = SecondLeftBottomY; }
            ResultRightBottomY = ResultLeftBottomY;
        }

        bool CheckIntersection()
        {
            if ((ResultLeftTopX > ResultRightTopX) || (ResultLeftTopY < ResultLeftBottomY)) return false;
           return true;
        }

        int CalcSquare()
        {
            return ((ResultRightTopX - ResultLeftTopX)*(ResultLeftTopY - ResultLeftBottomY));
        }

    }
}

//, FirstLeftTopY;
    //     FirstRightTopX, FirstRightTopY;
    //     FirstLeftBottomX, FirstLeftBottomY;
    //     FirstRightBottomX, FirstRightBottomY;
/*
 Number splitting
Devise a function that takes an input 'n' (integer) and returns a string that is the decimal representation of that number grouped by commas after every 3 digits. You can't solve the task using a built-in formatting function that can accomplish the whole task on its own.
Assume: 0 <= n < 1000000000
1 -> «1» 10 -> «10» 100 -> «100» 1000 -> «1,000» 10000 -> «10,000» 100000 -> «100,000» 1000000 -> «1,000,000» 35235235 -> «35,235,235» 
Anagram
Devise a function that gets one parameter 'w' and returns all the anagrams for 'w' from the file wl.txt.
«Anagram»: An anagram is a type of word play, the result of rearranging the letters of a word or phrase to produce a new word or phrase, using all the original letters exactly once; for example orchestra can be rearranged into carthorse.
anagrams(«horse») should return: ['heros', 'horse', 'shore']

 */
