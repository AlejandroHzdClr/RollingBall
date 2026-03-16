using System;
using UnityEngine;

namespace Player
{
    public class CameraController : PlayerSystem
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ChangeCamera"))
            {
                main.FollowCamera.SetActive(false);
                main.Camera2D.SetActive(true);
                main.Is2D = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ChangeCamera"))
            {
                main.Camera2D.SetActive(false);
                main.FollowCamera.SetActive(true);
                main.Is2D = false;
            }
        }
    }

}
