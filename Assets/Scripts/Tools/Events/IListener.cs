using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum EVENT_TYPE
    {

    }

    public interface IListener
    {
        void OnEvent(EVENT_TYPE eventType, Component Sender, Object param = null);
    }

