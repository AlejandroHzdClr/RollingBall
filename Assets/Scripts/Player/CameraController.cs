using System;
using UnityEngine;

namespace Player
{
    public class CameraController : PlayerSystem
    {
        [SerializeField] private float jump2DForce;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ChangeCamera"))
            {
                main.FollowCamera.SetActive(false);
                main.Camera2D.SetActive(true);
                main.Is2D = true;
                main.ChangeJump(jump2DForce);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ChangeCamera"))
            {
                main.Camera2D.SetActive(false);
                main.FollowCamera.SetActive(true);
                main.Is2D = false;
                main.ChangeJump(main.OriginalJumpForce);
            }
        }
    }

}
