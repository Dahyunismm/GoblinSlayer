using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class FoodScript : MonoBehaviour
    {
        public float eatTimer = 0;

        private Slider eatSlider;

        public float eatTime = 5;

        private PlayerData data;

        private void Start()
        {
            data = FindObjectOfType<PlayerData>();
            eatSlider = data.eatSlider;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                eatSlider.maxValue = eatTime;
                eatTimer = 0;
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                eatSlider.gameObject.SetActive(true);
                eatSlider.value = eatTimer;
                eatTimer += Time.deltaTime;
                if (eatTimer >= eatTime)
                {
                    data.lateUpdatehotbar(1f);
                    eatSlider.gameObject.SetActive(false);
                    data.Hotbar[data.curEquipped] = null;
                    Destroy(gameObject);
                }
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                eatSlider.gameObject.SetActive(false);
                eatTimer = 0;
            }
        }
    }
}
