using UnityEngine;

namespace Environment
{
    public class UnlockNewEnv : MonoBehaviour
    {
        [SerializeField] private GameObject unlockThisThing;

        private void OnDisable()
        {
            unlockThisThing.SetActive(true);
        }
    }
}
