using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private class TimeEvent
    {
        public float timeToExecute;
        public CallBack Method;
    }

    private List<TimeEvent> events;

    public delegate void CallBack();

    void Awake()
    {
        events = new List<TimeEvent>();
    }

    public void add(CallBack method, float inSeconds)
    {
        events.Add(new TimeEvent
        {
            Method = method,
            timeToExecute =Time.time + inSeconds
        });
    }

    void Update()
    {
        if (events.Count == 0)
            return;

        for(int i=0;i< events.Count; i++)
        {
            var timeEvent = events[i];
            if (timeEvent.timeToExecute <= Time.time)
            {
                timeEvent.Method();
                events.Remove(timeEvent);
            }
        }
    }
    
}
