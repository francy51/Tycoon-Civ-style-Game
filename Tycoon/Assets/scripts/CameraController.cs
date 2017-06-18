using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    HexMapManager HexMap;
    MouseManager mouseMan;

    [SerializeField]
    float lerpSpeed;

    // Use this for initialization
    void Start()
    {
        oldPosition = this.transform.position;
        HexMap = GameObject.Find("Managers").GetComponent<HexMapManager>();
        mouseMan = GameObject.Find("Managers").GetComponent<MouseManager>();
    }

    Vector3 oldPosition;

    // Update is called once per frame
    void Update()
    {

        // TODO: Code to click-and-drag camera
        //          WASD
        //          Zoom in and out



        CheckIfCameraMoved();
    }



    public void PanToHex(HexComponent hexComp)
    {

        // TODO: Move camera to hexS
  
            StartCoroutine(disableMouse());

            transform.position = Vector3.Lerp(transform.position, new Vector3(hexComp.transform.position.x, transform.position.y, hexComp.transform.position.z), lerpSpeed * Time.time);

        

        //transform.position = new Vector3(hexComp.transform.position.x, transform.position.y, hexComp.transform.position.z);
    }

    HexComponent[] hexes;

    bool CheckMoving()
    {
        if (oldPosition != transform.position)
            return true;
        else return false;
    }

    void CheckIfCameraMoved()
    {
        if (oldPosition != this.transform.position)
        {
            // SOMETHING moved the camera.
            oldPosition = this.transform.position;

            // TODO: Probably HexMap will have a dictionary of all these later
            if (hexes == null)
                hexes = GameObject.FindObjectsOfType<HexComponent>();

            // TODO: Maybe there's a better way to cull what hexes get updated?

            foreach (HexComponent hex in hexes)
            {
                hex.UpdatePosition();
            }
        }
    }

    IEnumerator disableMouse()
    {
        Debug.Log("mouse in off");
        mouseMan.enabled = false;

        yield return new WaitForSeconds(.4f);

        Debug.Log("mouse in on");
        mouseMan.enabled = true;

    }


}
