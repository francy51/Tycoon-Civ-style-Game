using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMapManager : MonoBehaviour
{
    [SerializeField]
    public static bool allowWrapEastWest = true;
    [SerializeField]
    public static bool allowWrapNorthSouth;

    [SerializeField]
    GameObject mapHolder;

    public HexMap map;
    public bool createMapBool;

    public int maxSize;
    public int minSize;
    
        


    void Start()
    {
        if (createMapBool == true)
        {
            createMap();
            return;
        }
        try
        {
            map = JsonManager<HexMap>.readJson();
            map.resetStaticValues();
            loadMap();
        }
        catch
        {
            Debug.Log("Missing map save. Creating Map");
            createMap();
        }

    }

    private void loadMap()
    {
        foreach (HexContinent h in map.hexList)
        {
            //loop through the map and create a position for each tile
            Vector3 pos = h.hex.PositionFromCamera(
                Camera.main.transform.position,
                HexMap.NumRows,
                HexMap.NumColumns
            );

            //Instantiate a hex component object for the game world
            GameObject hexGO = (GameObject)Instantiate(
                HexPrefab,
                pos,
                Quaternion.identity,
                mapHolder.transform
            );
            HexComponent hexC = hexGO.GetComponent<HexComponent>();
            hexC.Hex = h;
            //hexC.Hex.hex.HexMap = map;

            //Add any building to the tile
            if (h.hex.hasBuilding)
            {
                Debug.Log("Loading - " + Application.dataPath + "/prefabs/" + h.hex.BuildingType +".prefab");
                hexC.ForceBuild((GameObject)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/" + h.hex.BuildingType + ".prefab", typeof(GameObject)));
                
            }
        }
    }

    public GameObject HexPrefab;



    private void createMap()
    {
        map = new HexMap(UnityEngine.Random.Range(minSize,maxSize), UnityEngine.Random.Range(minSize, maxSize));
        for (int column = 0; column < HexMap.NumColumns; column++)
        {
            for (int row = 0; row < HexMap.NumRows; row++)
            {
                // Instantiate a Hex
                HexContinent h = new HexContinent(column,row);
                Vector3 pos = h.hex.PositionFromCamera(
                    Camera.main.transform.position,
                    HexMap.NumRows,
                    HexMap.NumColumns
                );


                GameObject hexGO = (GameObject)Instantiate(
                    HexPrefab,
                    pos,
                    Quaternion.identity,
                    mapHolder.transform
                );
                HexComponent hexC = hexGO.GetComponent<HexComponent>();
                hexC.Hex = h;
                //hexC.Hex.hex.HexMap = map;

                map.AddHex(hexC.Hex);

                // MeshRenderer mr = hexGO.GetComponentInChildren<MeshRenderer>();
                // mr.material = HexMaterials[Random.Range(0, HexMaterials.Length)];
            }
        }
        JsonManager<HexMap>.saveJson(map);
    }


}
