using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public static class ArrayCheker
    {
        public static Tuple <int, List<int>> FindClosestSubset(int[] nums, int target)
        {
            Array.Sort(nums); // O(nlogn)

            int left = 0, right = 0;
            int closestSum = 0;
            List<int> bestSubset = new List<int>();

            int currentSum = 0;
            List<int> currentSubset = new List<int>();

            while (right < nums.Length) // O(n)
            {
                currentSum += nums[right];
                currentSubset.Add(nums[right]);

                while (currentSum > target && left <= right)
                {
                    currentSum -= nums[left];
                    currentSubset.RemoveAt(0);
                    left++;
                }

                if (Math.Abs(target - currentSum) < Math.Abs(target - closestSum))
                {
                    closestSum = currentSum;
                    bestSubset = new List<int>(currentSubset);
                }

                right++;
            }

            return Tuple.Create(closestSum, bestSubset);
        }
    }
}
