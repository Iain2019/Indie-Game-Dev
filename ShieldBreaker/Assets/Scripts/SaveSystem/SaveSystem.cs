using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSytem
{
    //get path
    static string m_systemDataPath = Application.persistentDataPath + "/SystemData.ISave";
    public static void SaveSystemData(SystemData m_systemData)
    {
        //create a save data variable
        SystemData data = new SystemData(m_systemData);
        //binary saving
        BinaryFormatter formatter = new BinaryFormatter();
        //create file
        FileStream stream = new FileStream(m_systemDataPath, FileMode.Create);
        //serialize and then close
        formatter.Serialize(stream, data);
        stream.Close();
    }

    //load quickhit save
    public static SystemData LoadSystemData()
    {
        //if there is save data already
        if (File.Exists(m_systemDataPath))
        {
            //load binary
            BinaryFormatter formatter = new BinaryFormatter();
            //open file
            FileStream stream = new FileStream(m_systemDataPath, FileMode.Open);
            //assign data and close
            SystemData data = formatter.Deserialize(stream) as SystemData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteSystemData()
    {
        //delete save data
        if (File.Exists(m_systemDataPath))
        {
            File.Delete(m_systemDataPath);
        }
    }
}
