namespace CRUDTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange -- declarations of variables, objects, and any necessary setup
            MyMath mm = new MyMath();
            int input1 = 10;
            int input2 = 5;
            int expected = 15;
            //act -- execution of the method or functionality being tested
            int actual = mm.Add(input1, input2);
            //assert -- verification that the expected outcome matches the actual outcome
            Assert.Equal(expected, actual);

        }
    }
}
