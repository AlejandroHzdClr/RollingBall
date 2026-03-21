using System;
using UnityEngine;

namespace Environment
{
    public class UnlockNewEnv : MonoBehaviour
    {
        [SerializeField] private GameObject unlockThisThing;

        private void Awake()
        {
            unlockThisThing.SetActive(false);
        }

        private void OnDisable()
        {
            unlockThisThing.SetActive(true);
        }
    }
}
