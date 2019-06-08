using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class EventListener : MonoBehaviour, IListener
    {

        private void Start()
        {

            //EventManager.Instance.AddListener(EVENT_TYPE.BROADCAST_BUTTON_PRESSED, this);
        }

        public void OnEvent(EVENT_TYPE eventType, Component Sender, Object param = null)
        {

        }
    }