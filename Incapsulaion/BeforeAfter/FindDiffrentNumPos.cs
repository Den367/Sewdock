

namespace BeforeAfter
{
    internal class FindDiffrentNumPos
    {
        public int solution(int[] A, int M)
        {
            int differencesCount = A.Length - findNumberOfM(A, M);
            int MCount = 0;
            for (int i = 0; i < A.Length; i++)
            {
                var val = A[i];
                if(val != M)
                differencesCount--;
                if (differencesCount == MCount) return i;
                if (val == M) MCount++;
            }
            return -1;
        }

        int findNumberOfM(int[] A, int M)
        {
            int result = 0;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == M) result++;
            }
            return result;
        }
    }
}
