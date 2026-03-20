using Player;
using UnityEngine;

namespace Environment
{
    public class Coins : PlayerSystem
    {
        [SerializeField] private int pointNum;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerMain player = other.GetComponent<PlayerMain>();
                player.Points += pointNum;
                Destroy(gameObject);
            }
        }
    }
}

