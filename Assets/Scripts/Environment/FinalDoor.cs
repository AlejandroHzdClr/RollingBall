using System.Collections;
using Player;
using TMPro;
using UnityEngine;

namespace Environment
{
    public class FinalDoor : MonoBehaviour
    {
        [SerializeField] private TMP_Text textoPuntos;
        [SerializeField] private TMP_Text textoTiempo;
        [SerializeField] private TMP_Text textoPuntosFinal;

        [SerializeField] private GameObject panelFinal;   // Nuevo
        [SerializeField] private GameObject imagenFinal;  // Nuevo

        private PlayerMain player;
        private WaitForSeconds wait = new WaitForSeconds(0.02f);

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            player = other.GetComponent<PlayerMain>();
            if (player == null) return;

            player.CanMove = false;
            
            panelFinal.SetActive(true);
            imagenFinal.SetActive(true);

            StartCoroutine(MostrarPuntuajeFinal());
        }

        private IEnumerator MostrarPuntuajeFinal()
        {
            int puntos = player.Points;
            float tiempo = player.TimePlayed;

            textoTiempo.text = tiempo.ToString("F1") + "s";

            float puntosFinal = Mathf.Max(0, puntos - (tiempo * 2f));
            int puntosFinalInt = Mathf.RoundToInt(puntosFinal);

            int contador = 0;
            while (contador < puntos)
            {
                contador++;
                textoPuntos.text = contador.ToString();
                yield return wait;
            }

            int contadorFinal = 0;
            while (contadorFinal < puntosFinalInt)
            {
                contadorFinal++;
                textoPuntosFinal.text = contadorFinal.ToString();
                yield return wait;
            }
        }
    }
}