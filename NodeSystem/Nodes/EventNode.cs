using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XNode;


namespace hiVN
{
    [CreateNodeMenu("Control/Event")]
    public class EventNode : BaseNode
    {
        [Input(ShowBackingValue.Never)] public bool prev;
        [Output(ShowBackingValue.Never, ConnectionType.Override, TypeConstraint.None)] public bool next;

        public string eventName;

        public override object Read()
        {

            return new EventReturn(eventName);
        }
    }

    public class EventReturn
    {
        public string eventName;

        public EventReturn(string _eventName)
        {
            eventName = _eventName;
        }
    }
}