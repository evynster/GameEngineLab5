/*
 * Brouwer, Evyn 100702629
 * 
 * The prefabCube has been changed to a list of prefab that can be added to
 * 
 * left click for cubes, right click for capsules
 * 
 * the command now uses a function call to get the prefab based on input, if somehow the input is incorrect it can send a random prefab
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlane : MonoBehaviour
{
    Camera maincam;
    RaycastHit hitInfo;
    public List<Transform> prefabList;
    int mouse = -1;//this is the selected shape and also the trigger for the command

    // Start is called before the first frame update
    void Awake()
    {
        maincam = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            mouse = 0;
        if (Input.GetMouseButtonDown(1))
            mouse = 1;
        if (mouse>-1)
        {
            Ray ray = maincam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                Color c = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
                ICommand command = new PlaceCubeCommand(hitInfo.point, c, getPrefab(mouse));
                CommandInvoker.AddCommand(command);

                mouse = -1;
            }
        }

    }

/*
*This function returns a prefab based on what button was pressed but also allows for random prefab if the input is somehow wrong
*/
    Transform getPrefab(int list = -1)
    {
        if (prefabList.Count > 0 && list >= 0)
            return prefabList[list];
        else return prefabList[Random.Range(0,prefabList.Count)];
    }
}
