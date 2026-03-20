using System;
using UnityEngine;

public class UnlockNewEnv : MonoBehaviour
{
    [SerializeField] private GameObject unlockThisThing;

    private void OnDestroy()
    {
        unlockThisThing.SetActive(true);
    }
}
