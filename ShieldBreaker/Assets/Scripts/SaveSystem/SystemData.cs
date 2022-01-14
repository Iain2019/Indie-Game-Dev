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

    public SystemData(SystemData m_systemData)
    {
        if (m_systemData != null)
        {
            m_stars = m_systemData.m_stars;
            m_coins = m_systemData.m_coins;
            m_soundOn = m_systemData.m_soundOn;
            m_musicOn = m_systemData.m_musicOn;
            m_levelsData = m_systemData.m_levelsData;
        }
    }
}

public class LevelData
{
    public bool m_unlocked;
    public int m_stars;
    public LevelData(bool a_unlocked, int a_stars)
    {
        m_unlocked = a_unlocked;
        m_stars = a_stars;
    }
}