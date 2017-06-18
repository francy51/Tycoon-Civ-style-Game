using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManger : MonoBehaviour {

    public void SaveMap()
    {
        JsonManager<HexMap>.saveJson(GetComponent<HexMapManager>().map);
        Debug.Log("Map saved");
    }
}
