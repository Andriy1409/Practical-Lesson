using _13Practical_Lesson;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Fruit fruit = new Fruit("apple", "red");
            string expected = "Name :  apple  -  Color :  red";

            string actual = fruit.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}