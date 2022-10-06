using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WEI
{
    public class SystemDetectConversation : MonoBehaviour
    {
        public GameObject button;
        public GameObject talkUI;

        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log("進入區域");
            button.SetActive(true);
        }
        private void OnTriggerExit(Collider other)
        {
            //Debug.Log("沒有進入區域");
            button.SetActive(false);
        }
        private void Update()
        {
            if (button.activeSelf && Input.GetKeyDown(KeyCode.R))
            {
                talkUI.SetActive(true);
            }
        }
    }
}
