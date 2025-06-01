using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
	//public static GameSaveManager gameSave;
	public List<ScriptableObject> objects = new List<ScriptableObject>();


	//private void Awake()
	//{
	//	if (gameSave == null)
	//	{
	//		gameSave = this;
	//	}
	//	else
	//	{
	//		Destroy(this.gameObject);
	//	}
	//	DontDestroyOnLoad(this);
	//}
	private void OnEnable()
	{
		LoadScriptables();
	}
	private void OnDisable()
	{
		SaveScriptables();
	}
	public void SaveScriptables()
	{
		for (int i = 0; i<objects.Count; i++)
		{
			FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));
			BinaryFormatter binary = new BinaryFormatter();
			var json = JsonUtility.ToJson(objects[i]);
			binary.Serialize(file, json);
			file.Close();
		}
	}
	public void LoadScriptables()
	{
		for (int i = 0; i<objects.Count;i++)
		{
			if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
			{
				FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);
				BinaryFormatter binary = new BinaryFormatter();
				JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);
				file.Close();
			}
		}
	}
	public void ResetScriptables()
	{
		for (int i = 0;i < objects.Count;i++)
		{
			if(File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
			{
				File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
			}
		}
	}
}

//"serialize"(tuan tu hoa)
//Luu tru trang thai: Luu gia tri cua cac bien trong Editor hoac scene.
//Hien thi du lieu trong Inspector: Hien thi va chinh sua cac object phuc tap (nhu danh sach, struct, hoac class).
//Luu du lieu vao file: Ghi lai trang thai game (save game) hoac cau hinh (settings).

//vi du:
//Game luu trang thai cua nhan vat:
//Gia su ban co mot nhan vat voi du lieu nhu sau:
//Ten: "Hero"
//Mau: 100
//Vu khi: "Sword"

//Khi ban muon luu trang thai nay, Unity se can phai "serialize" (tuan tu hoa)
//du lieu cua nhan vat thanh mot dang du lieu don gian (nhu JSON hoac binary) de ghi vao file.
//Sau nay, khi mo game, Unity co the doc file va "deserialize" (khoi phuc) du lieu thanh object nhan vat ban dau.