using UnityEngine;

namespace Player
{
    public class MassController : PlayerSystem
    {
        
        // Update is called once per frame
        void Update()
        {
            if (Mathf.Approximately(main.Rb.mass, 10f))
            {
                main.Rb.AddForce(Vector3.down * main.GroundPoundForce, ForceMode.Impulse);
            }
        }
    }
}

