using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JsonManager<T> where T : class
{

    public static T readJson()
    {
        string Path = Application.dataPath + "/save/" + typeof(T).ToString() + ".json";

        if (File.Exists(Path))
        {
            string jsonString = File.ReadAllText(Path);

            T obj = JsonUtility.FromJson<T>(jsonString);
            return obj;
        }
        else
        {
            Debug.Log("Doesn't Exist");

            return null;
        }
    }

    public static T saveJson(T obj)
    {

        string Path = Application.dataPath + "/save/" + typeof(T).ToString() + ".json";

        string jsonData = JsonUtility.ToJson(obj, true);
        File.WriteAllText(Path, jsonData);
        return readJson();
    }


}
