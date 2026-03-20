using UnityEngine;

namespace Environment
{
    public class WallCanDestroy : MonoBehaviour
    {
        private AudioSource audioComp;

        private void Awake()
        {
            audioComp = GetComponent<AudioSource>();
        }
    }
}
