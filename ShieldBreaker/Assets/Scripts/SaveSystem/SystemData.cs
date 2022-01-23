using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemData
{
    //System save data
    public int m_stars;
    public int m_coins;
    public bool m_soundOn;
    public bool m_musicOn;
    public LevelData[] m_levelsData;
    public ShieldData[] m_shieldData;

    public SystemData(SystemData a_systemData)
    {
        //in data passed in assign it
        if (a_systemData != null)
        {
            m_stars = a_systemData.m_stars;
            m_coins = a_systemData.m_coins;
            m_soundOn = a_systemData.m_soundOn;
            m_musicOn = a_systemData.m_musicOn;
            m_levelsData = a_systemData.m_levelsData;
            m_shieldData = a_systemData.m_shieldData;
        }
    }
}

[System.Serializable]
public class LevelData
{
    public bool m_unlocked;
    public int m_stars;
    public LevelData(LevelData a_levelData)
    {
        //in data passed in assign it
        if (a_levelData != null)
        {
            m_unlocked = a_levelData.m_unlocked;
            m_stars = a_levelData.m_stars;
        }
    }
}

[System.Serializable]
public class ShieldData
{
    public bool m_unlocked;
    public bool m_equipped;
    public ShieldData(ShieldData a_shiledData)
    {
        //in data passed in assign it
        if (a_shiledData != null)
        {
            m_unlocked = a_shiledData.m_unlocked;
            m_equipped = a_shiledData.m_equipped;
        }
    }
}