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

    /// <summary>
    /// Coloca los obstáculos en la road
    /// </summary>
    /// <param name="road"></param>
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
        }
    }

    /// <summary>
    /// Coge las posiciones donde irán los objetos deseados en la road
    /// </summary>
    /// <param name="road"></param>
    private static void SetPositionsRoad(Transform road)
    {
        positionObsX[0] = road.position.x - road.localScale.x / 3.3f;
        positionObsX[1] = road.position.x;
        positionObsX[2] = road.position.x + road.localScale.x / 3.3f;

        for (int i = 0; i < positionObsZ.Length; i++)
        {
            if(i==0) positionObsZ[i] = road.position.z - road.localScale.z / 3;
            else positionObsZ[i] = positionObsZ[i-1] + 10f;
        }
    }

    /// <summary>
    /// Coloca quesos en road
    /// </summary>
    /// <param name="road"></param>
    public static void CheesePLacement(Transform road)
    {
        SetPositionsRoad(road);
        float cheesePos = -5f;
        randomPath = Random.Range(0, 3);
        int randomMegaCheese = Random.Range(0, 5);

        for (int i = 0; i < 6; i++)
        {
            GameObject selectedObject;
            if (i == 5 && randomMegaCheese == 1) { selectedObject = ObjectsPool.instance.GetPooledMegaCheese(); }
            else { selectedObject = ObjectsPool.instance.GetPooledCheese(); }

            // Mover el objeto seleccionado y establecer la posición encima del objeto carretera
            selectedObject.transform.position = new Vector3(positionObsX[randomPath], road.position.y + 1f, positionObsZ[0] + cheesePos);
            cheesePos += 5f; 
            selectedObject.SetActive(true);

            // Debug.Log(road);

            // Establecer el objeto carretera como padre del objeto movido
            selectedObject.transform.SetParent(road.Find("Cheeses"));
        }
    }
}
