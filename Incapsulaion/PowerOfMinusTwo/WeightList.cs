using System;

namespace PowerOfMinusTwo
{
    internal class WeightList
    {

        private WeightItem _head;
        private WeightItem _tail;

        private int[] _result;
        private int _bitCount;

        public int[] Solution(int N)
        {
            CreateAndSetIncluded(N);
            return _result;
        }

        // set Included when needed
        private void CreateAndSetIncluded(int N)
        {
            CreateList(N);
            Console.Write("{0}: ", N);
            DetectWeightIncluding();
        }

        //building list of weights
        private void CreateList(int N)
        {
            //if (N == 0) return 1;
            int modN;
            int power = 0;
            int growingSumNegative = 0;
            int growingSumPositive = 1;
            int sign;
            WeightItem prev;
            int val;
            sign = (N < 0) ? -1 : 1;
            //modN =
            if (N < 0)
            {
                sign = -1;
                modN = N*(-1);
            }
            else
            {
                sign = 1;
                modN = N;
            }
            val = 1;
            _head = new WeightItem {bitNo = power, Value = val};
            power = power + 1;
            prev = _head;
            WeightItem currentItem = _head;
            // fill  list with weights
            while (((modN > growingSumPositive) && (sign == 1)) || ((modN > growingSumNegative*(-1)) && (sign == -1)))
            {
                val = (-2)*val;
                if ((power % 2) == 0) growingSumPositive += val;
                else growingSumNegative += val;
                currentItem = new WeightItem {bitNo = power, Value = val, prevItem = prev};
                prev.nextItem = currentItem;
                prev = currentItem;
                power = power + 1;
                //Console.WriteLine("power:{0} val:{1} growingSumNegative:{2} growingSumPositive:{3}", power, val, growingSumNegative, growingSumPositive);
            }
            _tail = currentItem;
            _bitCount = power;
            _result = new int[power];
            _tail.Included = true;
            _delta = -N + _tail.Value;
            _result[_tail.bitNo] = 1;
            curItem = _tail.prevItem;
        }

        private WeightItem curItem;
        private WeightItem curItemNegative;
        // lets decrement _delta by weights until zero 
        private int _delta;

        private void DetectWeightIncluding()
        {
            Console.Write("1");
            // because we already set heaviest bit and quantity is bigger than last number in 1 
            // it is neccesary to subtract 2 from curBitNo
            int curBitNo = _bitCount - 2;
            while (_delta != 0)
            {
                int curDeltaSign = (_delta < 0) ? (-1) : 1;
                var modDelta = _delta*curDeltaSign;
                int curWeightSign = (curItem.Value < 0) ? (-1) : 1;
                var modCurItem = (curItem.Value < 0) ? (-curItem.Value) : curItem.Value;
                if ((1 <= (modDelta/modCurItem)) && ((modDelta/modCurItem) < 2))
                {
                    // if we have opposite weight lets get previous to do compensation
                    if (curWeightSign == curDeltaSign)
                    {
                        curItem.nextItem.Included = true;
                        _delta = _delta + curItem.nextItem.Value;
                        _result[curBitNo + 1] = 1;
                    }
                    curItem.Included = true;
                    _delta = _delta + curItem.Value;
                    _result[curBitNo] = 1;
                }
                curBitNo = curBitNo - 1;
                curItem = curItem.prevItem;
            }
            if (curBitNo >= 0)
                while (curBitNo >= 0)
                {
                    Console.Write("0");
                    curBitNo--;
                }
            Console.WriteLine();
        }
    }

    class WeightItem
    {        
        public WeightItem prevItem { get; set; }
        public WeightItem nextItem { get; set; }
        public int Value { get; set; } 
        public int bitNo { get; set; }
        public bool Included { get; set; }
    }
}
