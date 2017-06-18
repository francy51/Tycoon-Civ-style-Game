using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HexContinent 
{

    [SerializeField]
    float elevation;
    [SerializeField]
    public Hex hex;

    public HexContinent(int col,int row)
    {
        hex = new Hex(col,row);
    }

}
