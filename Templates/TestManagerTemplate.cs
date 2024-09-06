using UnityEngine;

public class TestManagerTemplate : MonoBehaviour
{
    public static bool TestBool;
	[SerializeField] private bool testBool;

    private void OnEnable()
    {
        TestBool = testBool;
    }

    //[Button]
    public void OpenInventory()
    {
        if (!Application.isPlaying)
            return;

        // Opens inventory, for example by accessing the UIManager, or using a GlobalBlackboard.
        Debug.Log("Opening Inventory");
    }
}
