using UnityEngine;

public class WindTower : MonoBehaviour
{
    [SerializeField] public float windPower;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GettingUp(Rigidbody other)
    {
        other.AddForce(Vector3.up * windPower,ForceMode.Force);
    }
}
