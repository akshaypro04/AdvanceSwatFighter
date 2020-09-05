using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
        public class EventListner
        {
            public delegate void Callback();
            public bool IsSingleShot;
            public Callback Method;

            public EventListner()
            {
                IsSingleShot = true;
            }
        }

        private Dictionary<string, IList<EventListner>> m_EventTable;
        private Dictionary<string, IList<EventListner>> EventTable
        {
            get
            {
                if (m_EventTable == null)
                    m_EventTable = new Dictionary<string, IList<EventListner>>();
                return m_EventTable;
            }
        }

        public void AddListener(string name, EventListner listner)
        {
            if (!EventTable.ContainsKey(name))
                EventTable.Add(name, new List<EventListner>());

            if (EventTable[name].Contains(listner))
                return;

            EventTable[name].Add(listner);
        }

        public void RaiseEvent(string name)
        {
            for(int i = 0; i < EventTable[name].Count; i++)
            {
                EventListner listner = EventTable[name][i];
                listner.Method();
                if (listner.IsSingleShot)
                    EventTable[name].Remove(listner);
            }
        }
}

