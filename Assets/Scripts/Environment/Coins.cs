using Player;
using UnityEngine;

namespace Environment
{
    public class Coins : PlayerSystem
    {
        [SerializeField] private int pointNum;
        private void OnTriggerEnter(Collider other)
        {
            main.Points += pointNum;
            Destroy(this);
        }
    }
}

