using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class ObstaclesPlacementGround : MonoBehaviour
{
    public static GameObject[] objectsToPlace; // Array de objetos a seleccionar
                                               //  public List<GameObject> movedObjects = new List<GameObject>();
    private static float[] positionObsX = new float[3];
    private static float[] positionObsZ = new float[6];
    public static ObstaclesPlacementGround Instance;
    static int randomPath;

    public static Transform[] parentsCheese;

    void Start()
    {
        
    }

    public static void PlaceObjects1(Transform road)
    {
        SetPositionsRoad(road);

        for (int i = 0; i < 3; i++)
        {
            GameObject selectedObject = ObjectsPool.instance.GetPooledObstacle();
           

            // Mover el objeto seleccionado y establecer la posición encima del objeto carretera
            int randomZ = Random.Range(0, 3);

            if (selectedObject.CompareTag("Bat"))
            {
                selectedObject.transform.position = new Vector3(positionObsX[i], road.position.y + 15f, positionObsZ[randomZ]);
            }
            else selectedObject.transform.position = new Vector3(positionObsX[i], road.position.y + selectedObject.transform.localScale.y / 2, positionObsZ[randomZ]);

            selectedObject.SetActive(true);
            // Establecer el objeto carretera como padre del objeto movido
            selectedObject.transform.SetParent(road);

            // Eliminar el objeto seleccionado del array
            //objectsToPlace = objectsToPlace.Where(obj => obj != selectedObject).ToArray();
        }
    }

    //public static void PlaceObjects2(Transform road)
    //{
    //    SetPositionsRoad(road);

    //    for (int i = 0; i < 3; i++)
    //    {
    //        GameObject selectedObject = ObjectsPool.instance.GetPooledObstacle();
            

    //        // Mover el objeto seleccionado y establecer la posición encima del objeto carretera
    //        int randomZ = Random.Range(3, 6);


    //        if (selectedObject.CompareTag("Bat"))
    //        {
    //            selectedObject.transform.position = new Vector3(positionObsX[i], road.position.y + 15f, positionObsZ[randomZ]);
    //        }
    //        else selectedObject.transform.position = new Vector3(positionObsX[i], road.position.y + selectedObject.transform.localScale.y / 2, positionObsZ[randomZ]);

    //        selectedObject.SetActive(true);
    //        // Establecer el objeto carretera como padre del objeto movido
    //        selectedObject.transform.SetParent(road);


    //        // Eliminar el objeto seleccionado del array
    //        //objectsToPlace = objectsToPlace.Where(obj => obj != selectedObject).ToArray();
    //    }
   
    //}

    private static void SetPositionsRoad(Transform road)
    {
        positionObsX[0] = road.position.x - road.localScale.x / 3.3f;
        positionObsX[1] = road.position.x;
        positionObsX[2] = road.position.x + road.localScale.x / 3.3f;

        positionObsZ[0] = road.position.z - road.localScale.z / 3;
        positionObsZ[1] = positionObsZ[0] + 10f;
        positionObsZ[2] = positionObsZ[1] + 10f;
        positionObsZ[3] = positionObsZ[2] + 30f;
        positionObsZ[4] = positionObsZ[3] + 10f;
        positionObsZ[5] = positionObsZ[4] + 10f;
    }

    public static void CheesePLacement(Transform road)
    {
        SetPositionsRoad(road);
        float cheesePos = -5f;
        randomPath = Random.Range(0, 3);
        for (int i = 0; i < 6; i++)
        {
            GameObject selectedObject = ObjectsPool.instance.GetPooledCheese();

            // Mover el objeto seleccionado y establecer la posición encima del objeto carretera
            selectedObject.transform.position = new Vector3(positionObsX[randomPath], road.position.y + 1f, positionObsZ[0] - cheesePos);
            cheesePos += 5f; 
            selectedObject.SetActive(true);

            //Debug.Log(road);

            // Establecer el objeto carretera como padre del objeto movido
            selectedObject.transform.SetParent(road.Find("Cheeses"));
        }
    }
}
