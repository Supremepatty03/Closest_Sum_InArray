using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public static class ArrayCheker
    {
        public static Tuple<int, List<int>> FindClosestSubset(int[] nums, int target)
        {
            // Если массив пустой, 0
            if (nums.Length == 0)
            {
                return Tuple.Create(0, new List<int>());
            }
            int minSum = nums.Where(n => n < 0).Sum(); // Сумма всех отрицательных чисел
            int maxSum = nums.Where(n => n > 0).Sum(); // Сумма всех положительных чисел

            //  Hashset для отслеживания всех возможных сумм
            HashSet<int> possibleSums = new HashSet<int> { 0 }; // Начинаем с пустого подмножества (сумма = 0)
            Dictionary<int, List<int>> subsets = new Dictionary<int, List<int>>();
            subsets[0] = new List<int>();

            // Перебираем все элементы массива
            foreach (int num in nums)
            {
                // Создаем новый список возможных сумм на основе текущих
                List<int> newSums = new List<int>();
                foreach (int sum in possibleSums)
                {
                    int newSum = sum + num;
                    if (!possibleSums.Contains(newSum)) // Если эта сумма еще не была найдена
                    {
                        newSums.Add(newSum);
                        subsets[newSum] = new List<int>(subsets[sum]) { num };
                    }
                }

                // Добавляем все новые возможные суммы в HashSet
                foreach (int newSum in newSums)
                {
                    possibleSums.Add(newSum);
                }
            }

            // Ищем ближайшую сумму к целевому числу
            int closestSum = minSum;
            List<int> closestSubset = subsets[minSum];

            foreach (int sum in possibleSums)
            {
                if (Math.Abs(target - sum) < Math.Abs(target - closestSum))
                {
                    closestSum = sum;
                    closestSubset = subsets[sum];
                }
            }

            return Tuple.Create(closestSum, closestSubset); // Возвращаем найденную сумму и подмассив
        }
    }
}
