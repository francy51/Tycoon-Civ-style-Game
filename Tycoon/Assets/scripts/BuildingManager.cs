using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    [SerializeField]
    GameObject BuildUI;

    MouseManager MouseMan;

    [SerializeField]
    GameObject house;

    HexComponent hexC;

    void Start()
    {
        BuildUI.SetActive(false);
        MouseMan = GetComponent<MouseManager>();
    }

    public void openUI(HexComponent h)
    {
        BuildUI.SetActive(true);
        hexC = h;
    }

    public void closeUI()
    {
        BuildUI.SetActive(false);
    }

    public void buildingOptionOne()
    {
        if (ResourceData.resource >= 20 && ResourceData.money >= 10)
        {
            Hex h = hexC.Hex.hex;
            h.selected = false;
            hexC.hexModel.material.color = Color.yellow;
            hexC.BuildBuilding(house);
            h.curColor = Color.yellow;
            MouseMan.removeHexFromSelected(hexC);
            closeUI();
            ResourceData.resource -= 20;
            ResourceData.money -= 10;
            ResourceData.houses++;
        }
        else
        {
            print("Not enough resources");
            MouseMan.removeHexFromSelected(hexC);
            closeUI();
        }
    }

    public void buildingOptionTwo()
    {
        hexC.Hex.hex.selected = false;
        hexC.hexModel.material.color = Color.magenta;
        hexC.Hex.hex.curColor = Color.magenta;
        MouseMan.removeHexFromSelected(hexC);
        closeUI();
    }


}
