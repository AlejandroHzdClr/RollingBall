using System;
using Player;
using TMPro;
using UnityEngine;

namespace Player
{
    public class GameController : PlayerSystem
    {
        [SerializeField] private TMP_Text texto;
        private void OnTriggerEnter(Collider other)
        {
            throw new NotImplementedException();
        }
    }
}

