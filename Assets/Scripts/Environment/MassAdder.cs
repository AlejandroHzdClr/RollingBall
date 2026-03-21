using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Environment
{
    public class MassAdder : MonoBehaviour
    {
        [SerializeField] private float mass;
        [SerializeField] private GameObject imagen;
        [SerializeField] private float tiempoVisible = 4f;
        [SerializeField] private float fadeDuration = 1f;

        private AudioSource musicSource;
        private AudioSource localSound;
        private RawImage rawImg;

        private void Start()
        {
            GameObject musicaObj = GameObject.Find("Musica");
            if (musicaObj != null)
                musicSource = musicaObj.GetComponent<AudioSource>();

            localSound = GetComponent<AudioSource>();
            if (localSound == null)
                localSound = gameObject.AddComponent<AudioSource>();

            rawImg = imagen.GetComponent<RawImage>();

            Color c = rawImg.color;
            c.a = 0f;
            rawImg.color = c;
        }

        public void AddingMass(List<float> massList)
        {
            massList.Add(mass);
            imagen.SetActive(true);

            if (musicSource != null)
                musicSource.Pause();

            if (localSound != null)
                localSound.Play();

            StartCoroutine(FadeInOutRoutine());
        }

        private IEnumerator FadeInOutRoutine()
        {
            yield return StartCoroutine(Fade(0f, 1f, fadeDuration));
            yield return new WaitForSeconds(tiempoVisible);
            yield return StartCoroutine(Fade(1f, 0f, fadeDuration));

            imagen.SetActive(false);

            if (localSound != null)
                localSound.Stop();

            if (musicSource != null)
                musicSource.UnPause();

            Destroy(gameObject);
        }

        private IEnumerator Fade(float start, float end, float duration)
        {
            float t = 0f;
            Color c = rawImg.color;

            while (t < duration)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Lerp(start, end, t / duration);
                c.a = alpha;
                rawImg.color = c;
                yield return null;
            }

            c.a = end;
            rawImg.color = c;
        }
    }
}