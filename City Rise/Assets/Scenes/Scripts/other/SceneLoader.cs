using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SceneLoader
{
    public static void Save(GameObject Buildingsplaced)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Data.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        Data data = new Data(Buildingsplaced);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Data Load()
    {
        string path = Application.persistentDataPath + "/Data.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Data data = formatter.Deserialize(stream) as Data;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Save File not found in: " + path);
            return null;
        }
    }

    public static void Delete()
    {
        string path = Application.persistentDataPath + "/Data.save";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.LogError("File not found");
        }
    }
}
