using MarcoUtilities.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests.Playmode
{
    public class ColliderExtensionsTest
    {
        private GameObject gameObject;
        private Collider collider;

        public void SetupCollider()
        {
            gameObject = new GameObject();
            collider = gameObject.AddComponent<CapsuleCollider>();
        }

        [UnityTest]
        public IEnumerator TestGetColliderHeight()
        {
            SetupCollider();

            float result1 = collider.GetColliderHeight();
            float result2 = collider.GetHalfColliderHeight();

            Assert.AreApproximatelyEqual(result1, 1f, 0.01f);
            Assert.AreApproximatelyEqual(result2, 0.5f, 0.01f);
            Assert.AreApproximatelyEqual(result2, result1 / 2f, 0.01f);

            yield return null;
        }
    }
}
