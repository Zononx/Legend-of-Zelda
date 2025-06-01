using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FloatValue : ScriptableObject //, ISerializationCallbackReceiver
{
    public float initialValue; //gia tri goc
    //[HideInInspector]
    public float RuntimeValue;

    //public void OnAfterDeserialize()
    //{
    //    RuntimeValue = initialValue;
    //}
    //public void OnBeforeSerialize() { }
}

//ISerializationCallbackReceiver
//Giao dien nay cho phep thuc hien hanh dong truoc khi obj được serial hoa' va sau khi obj duoc deserial hoa'.
//Dieu nay huu ich khi ban muon du lieu khoi tao hoac hoi phuc sau khi luu/chuyen canh.

//OnAfterDeserialize()
//Sau khi obj dc deserial hoa' (vd: load scene, reset game), method nay se dc goi de thiet lap Runtime = initial
//moi khi start game or obj dc khoi tao lai, RuntimeValue luon = initialValue.
