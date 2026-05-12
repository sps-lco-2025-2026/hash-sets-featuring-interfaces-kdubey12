using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HSetWork
{
    [TestClass]
    public class IHashSetTests
    {
        [TestMethod]
        public void TestAdd()
        {
            var set = new IHashSet<SPSStudent>(4, CollisionApproach.SeparateChaining);

            set.Add(new SPSStudent("Alice", SchoolYear.Year12, "RGS"));
            set.Add(new SPSStudent("Bob",   SchoolYear.Year11, "MHT"));
            set.Add(new SPSStudent("Alice", SchoolYear.Year12, "RGS"));
            set.Add(new SPSStudent("Clara", SchoolYear.Year10, "JBW"));

            Assert.IsTrue(set.IsPresent(new SPSStudent("Alice", SchoolYear.Year12, "RGS")));
        }
    }
}