using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/SignalSender", fileName = "New SignalSender")]
public class SignalSender : ScriptableObject
{
    public List<SignalListener> listeners = new List<SignalListener>();
    public void Raise()
    {
        for(int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }
    public void RegisterListener(SignalListener listener)
    {
        if (!listeners.Contains(listener))
        {
			listeners.Add(listener);
		}
    }
    public void DeRegisterListener(SignalListener listener)
    {
		if (listeners.Contains(listener))
		{
			listeners.Remove(listener);
		}
	}
}
