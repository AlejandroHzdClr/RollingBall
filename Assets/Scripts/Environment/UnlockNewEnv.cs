using System;
using UnityEngine;

public class UnlockNewEnv : MonoBehaviour
{
    [SerializeField] private GameObject unlockThisThing;

    private void OnDisable()
    {
        unlockThisThing.SetActive(true);
    }
}
