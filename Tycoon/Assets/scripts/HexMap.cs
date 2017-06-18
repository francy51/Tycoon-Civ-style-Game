using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexMap
{

    public static int NumRows;
    public static int NumColumns;

    [SerializeField]
    int savedRowNum;
    [SerializeField]
    int savedColNum;
    [SerializeField]
    string layerName;

    //Can't use dictionaries since JSONUtility doesn't support them
   // Dictionary<HexComponent, Hex> hexMap;
    [SerializeField]
    public List<HexContinent> hexList;

    public HexMap(int Rows, int Col)
    {
        //hexMap = new Dictionary<HexComponent, Hex>();
        hexList = new List<HexContinent>();
        NumRows = Rows;
        NumColumns = Col;
        savedRowNum = Rows;
        savedColNum = Col;
    }

    //when load map need to reset static values so that outside scripts can access the map values
    public void resetStaticValues()
    {
        NumRows = savedRowNum;
        NumColumns = savedColNum;
    }

    public void AddHex(HexContinent hex)
    {
        //hexMap.Add(hexC, hex);
        hexList.Add(hex);
    }

    //public Hex getHex(HexComponent h)
    //{
    //    return hexMap[h];
    //}

    public HexContinent getHex(int id)
    {
        return hexList[id];
    }

    // If we can't use dictionary we will have to use this
    public HexContinent getHex(HexComponent h)
    {
        foreach (HexContinent hex in hexList)
        {
            if (hex == h.Hex)
            {
                return hex;
            }
        }
        return null;
    }
}
