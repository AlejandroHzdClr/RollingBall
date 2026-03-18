using UnityEngine;

public class WallCanDestroy : MonoBehaviour
{
    public void Interact()
    {
        Destroy(this.gameObject);
    }
}
