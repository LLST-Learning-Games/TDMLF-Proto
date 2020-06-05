using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TDMLF.Locations
{
    public class CountdownTimer : MonoBehaviour
    {

        private Slider barSlider;

        // Start is called before the first frame update
        void Start()
        {
            barSlider = GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator CountDownAnimation(float time)
        {
            float animationTime = time;
            while (animationTime > 0)
            {
                animationTime -= Time.deltaTime;
                barSlider.value = animationTime / time;
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}
