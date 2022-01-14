using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSytem
{
    #region SystemData
    static string m_systemDataPath = Application.persistentDataPath + "/SystemData.ISave";
    //create/replace save file (used for score)
    public static void SaveSystemData(SystemData m_systemData)
    {
        SystemData data = new SystemData(m_systemData);

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(m_systemDataPath, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //load quickhit save
    public static SystemData LoadSystemData()
    {
        if (File.Exists(m_systemDataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(m_systemDataPath, FileMode.Open);

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
        if (File.Exists(m_systemDataPath))
        {
            File.Delete(m_systemDataPath);
        }
    }
    #endregion
}
