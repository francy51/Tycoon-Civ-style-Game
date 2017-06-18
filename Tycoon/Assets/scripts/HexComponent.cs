using UnityEngine;

[System.Serializable]
public class HexComponent : MonoBehaviour
{


    [SerializeField]
    public HexContinent Hex;
    [SerializeField]
    public MeshRenderer hexModel;
    [SerializeField]
    GameObject buildingHolder;

    void Start()
    {
        hexModel = GetComponentInChildren<MeshRenderer>();
        hexModel.material.color = Hex.hex.curColor;
        Hex.hex.selected = false;
    }

    public void UpdatePosition()
    {
        Vector4 newPosition = Hex.hex.PositionFromCamera(
            Camera.main.transform.position,
            HexMap.NumRows,
            HexMap.NumColumns);

        if (newPosition.w == 1)
        {
            hexModel.enabled = false;
        }
        else
        {
            hexModel.enabled = true;
        }

        transform.position = newPosition;

    }

    public void BuildBuilding(GameObject building)
    {
        if (!Hex.hex.hasBuilding)
        {
            Instantiate(building, buildingHolder.transform.position, Quaternion.identity, buildingHolder.transform);
            Hex.hex.BuildingType = building.name;
            Hex.hex.hasBuilding = true;
        }
        else
        {
            print("Building already built");
        }
    }

    public void ForceBuild(GameObject building)
    {
        Instantiate(building, buildingHolder.transform.position, Quaternion.identity, buildingHolder.transform);
        Hex.hex.BuildingType = building.name;
        Hex.hex.hasBuilding = true;
    }



}
