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
        public static Tuple<int, List<int>> FindClosestSubsetBruteForce(int[] nums, int target) // O(2^n) - перебор всех подмножеств
        {
            int closestSum = int.MinValue;
            List<int> bestSubset = new List<int>();

            int n = nums.Length;
            for (int mask = 1; mask < (1 << n); mask++) // сдиг 1 на n бит (2^n)
            {
                List<int> subset = new List<int>();
                int sum = 0;
                for (int i = 0; i < n; i++)
                {
                    if ((mask & (1 << i)) != 0) // установлен ли i бит в mask
                    {
                        sum += nums[i];
                        subset.Add(nums[i]);
                    }
                }
                if (subset.Count == 1 && sum == target)
                    continue;
                if (Math.Abs(target - sum) < Math.Abs(target - closestSum))
                {
                    closestSum = sum;
                    bestSubset = new List<int>(subset);
                }
            }

            return Tuple.Create(closestSum, bestSubset);
        }
    }

}
