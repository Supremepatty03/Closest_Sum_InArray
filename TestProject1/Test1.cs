namespace Lab2.Tests
{
    [TestClass]
    public class ClosestSumTests
    {
        [TestMethod]
        public void FindClosestSubset_ShouldReturnClosestSumAndSubset()
        {
            int[] input = { 1, 3, 4, 7, 10 };
            int target = 9;
            var expected = new Tuple<int, List<int>>(8, new List<int> { 1, 3, 4 });

            var result = ArrayCheker.FindClosestSubset(input, target);

            Assert.AreEqual(expected.Item1, result.Item1, "Сумма не совпадает.");
            CollectionAssert.AreEqual(expected.Item2, result.Item2, "Подмассив найден неверно.");
        }
        [TestMethod]
        public void FindClosestSubset_ShouldNotReturnWrongSum()
        {
            // Arrange
            int[] input = { 2, 5, 8, 12 };
            int target = 10;

            // Act
            var result = ArrayCheker.FindClosestSubset(input, target);

            // Assert
            Assert.AreNotEqual(15, result.Item1, "Ошибка: сумма не должна быть 15.");
        }

        [TestMethod]
        public void FindClosestSubset_ShouldNotReturnWrongSubset()
        {
            // Arrange
            int[] input = { 1, 2, 5, 9 };
            int target = 8;
            var wrongSubset = new List<int> { 2, 5, 9 }; // Нарочно неправильный вариант

            // Act
            var result = ArrayCheker.FindClosestSubset(input, target);

            // Assert
            CollectionAssert.AreNotEqual(wrongSubset, result.Item2, "Ошибка: найден неверный подмассив.");
        }

        [TestMethod]
        public void FindClosestSubset_ShouldHandleEmptyArray()
        {
            // Arrange
            int[] input = { };
            int target = 5;

            // Act
            var result = ArrayCheker.FindClosestSubset(input, target);

            // Assert
            Assert.AreEqual(0, result.Item1, "Ошибка: для пустого массива сумма должна быть 0.");
            Assert.AreEqual(0, result.Item2.Count, "Ошибка: подмассив должен быть пустым.");
        }

        [TestMethod]
        public void FindClosestSubset_ShouldHandleSingleElementArray()
        {
            // Arrange
            int[] input = { 4 };
            int target = 7;

            // Act
            var result = ArrayCheker.FindClosestSubset(input, target);

            // Assert
            Assert.AreEqual(4, result.Item1, "Ошибка: сумма должна быть равна единственному элементу.");
            CollectionAssert.AreEqual(new List<int> { 4 }, result.Item2, "Ошибка: подмассив должен содержать только один элемент.");
        }
    }
}
