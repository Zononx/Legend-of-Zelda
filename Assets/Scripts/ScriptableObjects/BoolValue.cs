using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BoolValue : ScriptableObject //, ISerializationCallbackReceiver
{
    public bool initialValue; //gia tri goc
    //[HideInInspector]
    public bool RuntimeValue;

    //public void OnAfterDeserialize()
    //{
    //    RuntimeValue = initialValue;
    //}
    //public void OnBeforeSerialize() { }

}
