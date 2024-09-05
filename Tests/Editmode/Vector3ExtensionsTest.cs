using MarcoUtilities.Extensions;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;


namespace Tests.Editmode
{
    public class Vector3ExtensionsTest
    {
        private Vector3 old = new Vector3(1, 2, 3);
        private Vector3 newVector= new Vector3(-1, -3, -5);

        [Test]
        public void TestWhereY()
        {
            Vector3 result = old.Where(y: newVector.y);
            Assert.AreEqual(new Vector3(1, 2, 3), old);
            Assert.AreEqual(new Vector3(1, -3, 3), result);
        }

        [Test]
        public void TestWhereAll()
        {
            Vector3 result = old.Where(newVector.x, newVector.y, newVector.z);
            Assert.AreEqual(new Vector3(1, 2, 3), old);
            Assert.AreEqual(newVector, result);
        }
    }
}
