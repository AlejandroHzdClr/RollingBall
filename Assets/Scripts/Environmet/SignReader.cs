using UnityEngine;

namespace Environment
{
    public class SignReader : MonoBehaviour
    {
        [SerializeField] private GameObject information;
        [SerializeField] private GameObject texto;

        private bool playerInside;
        private bool signActive;

        private void Update()
        {
            if (playerInside && Input.GetKeyDown(KeyCode.E))
            {
                signActive = !signActive;
                information.SetActive(signActive);
                Debug.Log("Ha interactuado");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInside = true;
                texto.SetActive(true);
                Debug.Log("Ha entrado el player");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInside = false;
                signActive = false;
                information.SetActive(false);
                texto.SetActive(false);
                Debug.Log("Ha salido");
            }
        }
    }
}