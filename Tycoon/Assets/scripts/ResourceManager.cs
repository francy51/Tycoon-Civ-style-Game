using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{

    ResourceData data;

    float time;

    [SerializeField]
    float taxCollectTimePeriod;

    [SerializeField]
    Text moneyText;
    [SerializeField]
    Text resourceText;

    void Start()
    {
        try
        {
            data = JsonManager<ResourceData>.readJson();
            ResourceData.money = data.savedMoney;
            ResourceData.resource = data.savedResources;
            Debug.Log("Loaded resources");

        }
        catch
        {
            Debug.Log("resources didn't exist. Creating ...");
            data = new ResourceData();
            JsonManager<ResourceData>.saveJson(data);
            Debug.Log("Created");
        }
        time = Time.time + taxCollectTimePeriod;
        moneyText.text = "Money : " + ResourceData.money.ToString();
        resourceText.text = "Resource : " + ResourceData.resource.ToString();
    }

    public void Update()
    {
        if (time <= Time.time)
        {
            //Add tax
            ResourceData.money += ResourceData.houses * data.tax;
            ResourceData.resource += ResourceData.factories * data.resourceGain;
            //Change display
            moneyText.text = "Money : " + ResourceData.money.ToString();
            resourceText.text = "Resource : " + ResourceData.resource.ToString();
            //fix all values
            data.savedMoney = ResourceData.money;
            data.savedResources = ResourceData.resource;
            data.savedHouses = ResourceData.houses;
            data.savedFactories = ResourceData.factories;
            //save resources
            JsonManager<ResourceData>.saveJson(data);
            //make new tick time
            time = Time.time + taxCollectTimePeriod;
        }
        //figure out how to slow this down

    }

}
