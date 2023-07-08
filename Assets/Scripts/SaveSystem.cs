using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Types;

public static class SaveSystem
{
    public static void SaveLoveInterest (LoveInterest partner) {
        BinaryFormatter formatter = new BinaryFormatter();
        // want to format under a female directory like: "/female/{name}.bin"
        // except idk how to ensure that the "/female" directory exists
        string path = Application.persistentDataPath + "/partners_" + partner.characterName + ".bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        LoveInterestData data = new LoveInterestData(partner);
        formatter.Serialize(stream, data);
        stream.Close();
   }

   public static LoveInterestData LoadLoveInterest(LoveInterest partner) {
        string path = Application.persistentDataPath + "/partners_" + partner.characterName + ".bin";
        
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LoveInterestData data = formatter.Deserialize(stream) as LoveInterestData;
            stream.Close();
            return data;
        } else {
            Debug.LogError("File not found at: " + path);
            return null;
        }
   }

   public static void ClearPersistentData() {
    string[] filePaths = Directory.GetFiles(Application.persistentDataPath);
    foreach (string filePath in filePaths)
        File.Delete(filePath); 
   }
}
