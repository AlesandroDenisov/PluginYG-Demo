using System.Collections;
using UnityEngine;

namespace HomoLudens.Core
{
    public class LoadingCurtain : MonoBehaviour
    {
        public CanvasGroup Curtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Show()

        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
        }
    
        public void Hide() => StartCoroutine(DoFadeIn());
    
        private IEnumerator DoFadeIn()
        {
            while (Curtain.alpha > 0.001f)
            {
            Curtain.alpha -= 0.03f;
            yield return new WaitForSeconds(0.03f);
            }

            Curtain.alpha = 0;
            gameObject.SetActive(false);
        }
    }
}