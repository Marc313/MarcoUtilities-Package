using MarcoUtilities.Extensions;
using NUnit.Framework;
using System.Collections.Generic;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests.Editmode
{
    public class CollectionExtensionsTest
    {
        readonly int[] list1 = { 0, 1};
        readonly List<int> list2 = new(){ 12, 13};

        [Test]
        public void TestRandomEntries()
        {
            var random1 = list1.GetRandomEntry();
            var random2 = list2.GetRandomEntry();
            Assert.IsTrue(random1 == 0 || random1 == 1);
            Assert.IsTrue(list2.Contains(random2));
        }

        [Test]
        public void TestLastEntries()
        {
            Assert.AreEqual(list1.GetLastEntry(), 1);
            Assert.AreEqual(list2.GetLastEntry(), 13);
        }
    }
}
