
namespace BinaryPeriod
{
    internal class BinaryPeriodDetector
    {
        private bitListItem _headOriginal;
        private bitListItem _tailOriginal;

        private bitListItem _tailOfSearch;

        private int sequnceLength;
        private int lengthOfSearch;

        private bitListItem curItem;
        private bitListItem curSearch;

        public int Solution(int N)
        {
            _headOriginal = getBinarySequnce(N);
            preFillSearch();
            return ReturnPeriod();
        }

        /// <summary>
        /// Returns of sequence
        /// </summary>
        /// <param name="M"></param>
        /// <returns></returns>
        private bitListItem getBinarySequnce(int M)
        {
            if (M < 0) return null;
            sequnceLength = 0;
            int bit;
            int ratio = M;
            int remainder = ratio%2;
            ratio = ratio/2;
            if (remainder != 0) bit = 1;
            else bit = 0;
            _headOriginal = new bitListItem(bit);
            _tailOriginal = _headOriginal;
            while (ratio > 0)
            {
                remainder = ratio%2;
                ratio = ratio/2;
                if (remainder != 0) bit = 1;
                else bit = 0;
                var newOne = new bitListItem(bit);
                _tailOriginal.Next = newOne;
                _tailOriginal = newOne;
                sequnceLength++;
            }
            return _headOriginal;
        }

        private void preFillSearch()
        {
            //the end of search string is on the third item
            _tailOfSearch = _headOriginal.Next.Next;
            lengthOfSearch = 2;
        }

        /// <summary>
        /// Compare at specific positon
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool CompareAtPostion(int n)
        {
            curSearch = _headOriginal;
            //check equality
            for (int j = 0; j < lengthOfSearch && n < sequnceLength; j++)
            {
                if (curItem.Value != curSearch.Value) return false;
                //reached the end of sequence
                //if (curItem.Next == null) return false;
                curItem = curItem.Next;
                curSearch = curSearch.Next;
                n++;
            }
            return true;
        }

        private int CheckAllPeriods()
        {
            int periodNo = lengthOfSearch;
            curItem = _tailOfSearch; // set on third item            
            while (periodNo <= sequnceLength - lengthOfSearch)
            {
                if (!CompareAtPostion(periodNo)) break;
                periodNo += lengthOfSearch;
            }
            if (periodNo > (sequnceLength - lengthOfSearch)) return lengthOfSearch;
            else return -1;
        }

        public int ReturnPeriod()
        {
            while (lengthOfSearch <= sequnceLength/2)
            {
                if (CheckAllPeriods() != -1) return lengthOfSearch;
                //increase length of search
                _tailOfSearch = _tailOfSearch.Next;
                lengthOfSearch++;
            }
            return -1;
        }

        private class bitListItem
        {
            public bitListItem(int value)
            {
                Value = value;
            }

            public int Value { get; set; }
            public bitListItem Next { get; set; }
        }
    }
}
