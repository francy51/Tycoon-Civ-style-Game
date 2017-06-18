using UnityEngine;

[System.Serializable]
public class Hex
{
    [SerializeField]
    //public HexMap HexMap;
    //[SerializeField]
    public Color curColor = Color.white;
    [SerializeField]
    public bool selected;
    [SerializeField]
    public bool hasBuilding;
    [SerializeField]
    int col; // column
    [SerializeField]
    int row; // row
    [SerializeField]
    int s; // z axis
    [SerializeField]
    float radius = 1f;
    [SerializeField]
    static float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
    [SerializeField]
    public string BuildingType;

    public Hex(int x, int y)
    {
        col = x;
        row = y;
        s = -(col + row);
    }

    public Vector3 getPosition()
    {
        return new Vector3(
            HexHorizontalSpacing() * (col + row / 2f),
            0,
            HexVerticalSpacing() * row
            );
    }

    public float HexHeight()
    {
        return radius * 2;
    }

    public float HexWidth()
    {
        return WIDTH_MULTIPLIER * HexHeight();
    }

    public float HexVerticalSpacing()
    {
        return HexHeight() * 0.75f;
    }

    //TODO: optimize get rid of repeated call
    public float HexHorizontalSpacing()
    {
        return HexWidth();
    }

    public Vector4 PositionFromCamera(Vector3 cameraPosition, float numRows, float numColumns)
    {
        float mapHeight = numRows * HexVerticalSpacing();
        float mapWidth = numColumns * HexHorizontalSpacing();

        Vector4 position = getPosition();

        if (HexMapManager.allowWrapEastWest)
        {
            float howManyWidthsFromCamera = (position.x - cameraPosition.x) / mapWidth;

            // We want howManyWidthsFromCamera to be between -0.5 to 0.5
            if (howManyWidthsFromCamera > 0)
                howManyWidthsFromCamera += 0.5f;
            else
                howManyWidthsFromCamera -= 0.5f;

            int howManyWidthToFix = (int)howManyWidthsFromCamera;

            position.x -= howManyWidthToFix * mapWidth;
        }

        if (HexMapManager.allowWrapNorthSouth)
        {
            float howManyHeightsFromCamera = (position.z - cameraPosition.z) / mapHeight;

            // We want howManyHeightsFromCamera to be between -0.5 to 0.5
            if (howManyHeightsFromCamera > 0)
                howManyHeightsFromCamera += 0.5f;
            else
                howManyHeightsFromCamera -= 0.5f;

            int howManyHeightsToFix = (int)howManyHeightsFromCamera;

            position.z -= howManyHeightsToFix * mapHeight;
            position.w = 1;
        }


        return position;
    }

}
