using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class MouseManager : MonoBehaviour
{
    [SerializeField]
    LayerMask mouseControllerLayer;

    bool clickAvailable;

    [SerializeField]
    BuildingManager buildMan;

    HexComponent selectedHex;
    HexComponent lastRemoved;
    HexComponent curHover;
    void Start()
    {
        clickAvailable = true;

        buildMan = GameObject.Find("Managers").GetComponent<BuildingManager>();
    }


    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseControllerLayer) && !EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log(hit.point);
            if (hit.transform.GetComponent<HexComponent>())
            {

                HexComponent hexC = hit.transform.GetComponent<HexComponent>();
                Hex h = hexC.Hex.hex;
                if (Input.GetMouseButton(0) && clickAvailable)
                {
                    clickAvailable = false;
                    if (h.selected)
                    {
                        h.selected = false;
                        hexC.hexModel.material.color = h.curColor;
                        lastRemoved = hexC;
                        buildMan.closeUI();
                        return;
                    }
                    if (selectedHex != null)
                    {
                        selectedHex.hexModel.material.color = h.curColor;
                        selectedHex.Hex.hex.selected = false;
                        lastRemoved = selectedHex;
                    }
                    h.selected = true;
                    hexC.hexModel.material.color = Color.blue;
                    selectedHex = hexC;
                    //open build ui
                    buildMan.openUI(hexC);
                    Debug.DrawRay(Camera.main.transform.position, hexC.transform.position);


                }
                else if (Input.GetMouseButton(1))
                {
                    if (h.selected)
                    {
                        hexC.hexModel.material.color = Color.green;
                        Camera.main.GetComponent<CameraController>().PanToHex(hexC);
                        h.selected = false;
                        buildMan.closeUI();
                    }
                }
                else
                {
                    if (!h.selected)
                    {
                        if (curHover != null && !curHover.Hex.hex.selected)
                            curHover.hexModel.material.color = curHover.Hex.hex.curColor;
                        curHover = hexC;
                        curHover.hexModel.material.color = Color.red;
                    }
                }
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            clickAvailable = true;

        }
    }

    //private void unselectAll()
    //{
    //    foreach (HexComponent h in selectedHexList)
    //    {
    //        h.hexModel.material.color = Color.white;

    //    }
    //    selectedHexList = new List<HexComponent>();

    //}

    public void removeHexFromSelected(HexComponent h)
    {
        if (h = selectedHex)
        {
            h.Hex.hex.selected = false;
            h.hexModel.material.color = h.Hex.hex.curColor;
            lastRemoved = h;
        }
    }

}
