using System;


namespace Incapsulaion
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var walker = new StepList();
            var A0 = new int[4] { 2,1,1,2 };
            var A1 = new int[20] { 10, 10, 9, 9, 8, 8, 7, 7, 6, 6, 5, 5, 4, 4, 3, 3, 2, 2, 1, 1 };
            var A2 = new int[20] {1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10};
            var A3 = new int[5] {2, 1, 3, 1, 1};
            var A4 = new int[]{};
            Console.WriteLine(" touch 4  {0}", walker.Proceed(A0)); Console.ReadLine();

            Console.WriteLine(" no touch {0}", walker.Proceed(A2)); Console.ReadLine();
           
            Console.WriteLine(string.Format(" no touch {0}", walker.Proceed(A1))); Console.ReadLine();
            
            Console.WriteLine(string.Format(" touch 5  {0}", walker.Proceed(A3))); Console.ReadLine();
            Console.WriteLine(string.Format(" empty    {0}", walker.Proceed(A4))); Console.ReadLine();
            Console.ReadLine();
        }

class StepList
{
    public StepList()
    {
        currentDirect = Direction.Up;
        StepCount = 0;
       

    }

    public int StepCount { get; set; }

    
    private Step _head;
    public Step Head {
        get { return _head; } 
    }

    private Step Tail { get; set; }
   
    public Direction currentDirect { get; set; }


    public int Proceed(int[] A)
    {
        Init();
        StepCount = 0;
        for (int i = 0; i < A.Length; i++)
        {
            if (WalkTroughAndAdd(A[i])) return StepCount + 1;
        }
        return 0;
    }

    void Init()
    {
        currentDirect = Direction.Up;
        if (_head != Tail)
        {
            _head = null;
            Tail = _head;
          
        }

    }
    public bool WalkTroughAndAdd(int N)
    {
        Line line;
        if (StepCount == 0)
        {
            line = new Line(0, 0, N, currentDirect);
            //_head = new Step();
            //Tail = _head;
        }
        else
        {
            var prevLine = Tail.Trace;
            line = new Line(prevLine.X2, prevLine.Y2, N, currentDirect);
            if (detectCollision( line)) return true;
        }
        addNewStep(line);

        changeDirection();
        StepCount++;
        return false;// no touches
    }


    private void addNewStep(Line line)
    {
        if (Head == null)
        {
            _head = new Step();
           
            Tail = _head;
        }
        else
        {
            var preLast = Tail;
            Tail = new Step();
            preLast.Next = Tail;
        }
        Tail.Trace = line;
        //Console.WriteLine(string.Format("X1:{0} Y1:{1} X2:{2} Y2:{3}",line.X1,line.Y1,line.X2,line.Y2));
    }

    private bool detectCollision( Line line)
    {
       
       
        if (_head == Tail) return false;
        var cur = _head;
        if (cur != null)
        while (cur != Tail )
        {
            var curTrace = cur.Trace;
            if (CheckIntersection(curTrace, line)) 
            {
                Console.WriteLine("Detect collision");
                Console.WriteLine("Line X1:{0} Y1:{1} X2:{2} Y2:{3}", line.X1, line.Y1, line.X2, line.Y2);
                Console.WriteLine(" check X1:{0} Y1:{1} X2:{2} Y2:{3}", curTrace.X1, curTrace.Y1, curTrace.X2, curTrace.Y2);
                return true;
            }
            cur = cur.Next;
          
          
           
        }
        return false;
    }

    private void changeDirection()
    {
        if (currentDirect == Direction.Up) currentDirect = Direction.Right;
        else if (currentDirect == Direction.Right) currentDirect = Direction.Down;
        else if (currentDirect == Direction.Down) currentDirect = Direction.Left;
        else if (currentDirect == Direction.Left) currentDirect = Direction.Up;
    }

    private bool CheckIntersection(Line line1, Line line2)
    {
        int X11, X12, Y11, Y12;
        int X21, X22, Y21, Y22;
        X11 = line1.X1;
        X12 = line1.X2;
        X21 = line2.X1;
        X22 = line2.X2;
        Y11 = line1.Y1;
        Y12 = line1.Y2;
        Y21 = line2.Y1;
        Y22 = line2.Y2;
        // first line vertical and second line horizontal
        if ((X11 == X12) && (Y21 == Y22) && (((Y21 <= Y11) && (Y21 >= Y12)) || ((Y21 >= Y11) && (Y21 <= Y12))) && (((X11 <= X21) && (X11 >= X22)) || ((X11 <= X21) && (X11 >= X22)))) 
        {return true;}
        //first line horizontal and second line vertical 
        if ((Y11 == Y12) && (X21 == X22) && (((X21 <= X11) && (X21 >= X12)) || ((X21 >= X11) && (X21 <= X12))) && (((Y11 <= Y21) && (Y11 >= Y22)) || ((Y11 <= Y21) && (Y11 >= Y22))))
        { return true; }
        // both lines horizontal
        if ((Y11 == Y12) && (Y21 == Y22) && (Y11 == Y22) && (((X11 <= X21) && (X11 >= X22)) || ((X11 >= X21) && (X11 <= X22)) || ((X12 <= X21) && (X12 >= X22)) || ((X12 >= X21) && (X12 <= X22))))
        { return true; }
        // both lines vertical
        if ((X11 == X12) && (X21 == X22) && (X11 == X22) && (((Y11 <= Y21) && (Y11 >= Y22)) || ((Y11 >= Y21) && (Y11 <= Y22)) || ((Y12 <= Y21) && (Y12 >= Y22)) || ((Y12 >= Y21) && (Y12 <= Y22))))
        { return true; }

        //if ((line1.Y1 == line1.Y2) && ((line2.X2 <= line1.X1) && (line2.X1 >= line1.X1) || (line2.X1 <= line1.X1) && (line2.X2 >= line1.X1))) return true;

        //if ((line1.X1 == line1.X2) && ((line2.Y2 <= line1.Y1) && (line2.Y1 >= line1.Y1) || (line2.Y1 <= line1.Y1) && (line2.Y2 >= line1.Y1))) return true;
            
        return false;
    }


}
        enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }
        
        class Step
        {
            public Line Trace { get; set; }
            //public Step Prev { get; set; }
            public Step Next { get; set; }
        }

        class Line
        {

            public Line(int x1, int y1,int N, Direction dir)
            {
                
                X1 = x1;
                Y1 = y1;
                switch (dir)
                {
                    case Direction.Up:
                      
                        X2 = X1;
                        Y2 = Y1 + N;
                        break;
                    case Direction.Right:

                        X2 = X1 + N;
                        Y2 = Y1;
                        break;
                    case Direction.Down:

                        X2 = X1 ;
                        Y2 = Y1 - N;
                        break;
                    case Direction.Left:

                        X2 = X1 - N;
                        Y2 = Y1;
                        break;
                }
            }
            public int X1 { get; set; }
            public int Y1 { get; set; }
            public int X2 { get; set; }
            public int Y2 { get; set; }

        }

        class Solution
        {
            int sumTotal = 0;
            int tailSum = 0;
            public int solution(int[] A)
            {
                // write your code in C# 5.0 with .NET 4.5 (Mono)
                int sumBefore = A[0];
                sumTotal = calcSumTotal(A);
                for (int i = 0; i < A.Length; i++)
                {
                    var cur = A[i];
                    sumTotal -= cur;    
                    if (( tailSum) == (sumTotal)) return i;
                    else
                    {
                        Console.WriteLine("{0}", tailSum);
                        tailSum += cur;
                       
                    }


                }
                return -1;
            }

            public int calcSumTotal(int[] A)
            {
                var result = 0;
                for (int i = 0; i < A.Length; i++)
                {
                    result  += A[i];
                }
                return result;
            }


        }


      
    }
}