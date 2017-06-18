using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceData
{
    public static int houses;

    public int savedHouses;

    public static int factories;

    public int savedFactories;

    public static int resource = 100;

    public static int money = 100;

    public int savedResources;

    public int savedMoney;

    public int tax = 10;

    public int resourceGain = 15;
    
    public ResourceData()
    {
        savedFactories = factories;
        savedMoney = money;
    }

}
