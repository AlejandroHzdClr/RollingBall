using UnityEngine;

public class WallCanDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyWall(Rigidbody other)
    {
        if (other.mass >= 5)
        {
            Destroy(this);
        }
    }
}
