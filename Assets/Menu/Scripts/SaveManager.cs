using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.TextCore.Text;
using System;
public static class SaveManager 
{
    public static SaveSlot currentSaveSlot;
    public static void SaveSlotData(SaveSlot _slot)
    {
        SlotData slotD= new SlotData(_slot);
        Debug.Log(slotD.slotName);
        string dataPath = Application.persistentDataPath + "/" + slotD.slotName + ".save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, slotD);
        fileStream.Close();
    }
    public static SlotData LoadSlotData(SaveSlot _slot)
    {
        SlotData dataD = new SlotData(_slot);
        string dataPath = Application.persistentDataPath + "/" + dataD.slotName + ".save";
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SlotData slotData = (SlotData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return slotData;
        }
        else 
        { 
            return null;
        }
    }
    public static void DeleteSlotData(SaveSlot _slot)
    {
        SlotData dataD = new SlotData(_slot);
        string dataPath = Application.persistentDataPath + "/" + dataD.slotName + ".save";
        if (File.Exists(dataPath))
        {
            File.Delete(dataPath); 
        }
    }
}
