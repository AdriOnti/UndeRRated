using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesPlacementGround : MonoBehaviour
{
    public GameObject[] objectsToPlace; // Array de objetos a seleccionar
    public List<GameObject> movedObjects = new List<GameObject>();
    public Transform roadObject; // Objeto carretera
    private float[] positionObsX = new float[3];
    private float[] positionObsZ = new float[6];
    void Start()
    {
        positionObsX[0] = roadObject.transform.position.x - roadObject.localScale.x / 3;
        positionObsX[1] = roadObject.transform.position.x;
        positionObsX[2] = roadObject.transform.position.x + roadObject.localScale.x / 3;

        positionObsZ[0] = roadObject.transform.position.z - roadObject.localScale.z / 2;
        positionObsZ[1] = roadObject.transform.position.z - roadObject.localScale.z / 3.5f;
        positionObsZ[2] = roadObject.transform.position.z - roadObject.localScale.z / 5;
        positionObsZ[3] = roadObject.transform.position.z + roadObject.localScale.z / 5;
        positionObsZ[4] = roadObject.transform.position.z + roadObject.localScale.z / 3.5f;
        positionObsZ[5] = roadObject.transform.position.z + roadObject.localScale.z / 2;

        PlaceObjects1();
        PlaceObjects2();
    }

    void PlaceObjects1()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, objectsToPlace.Length); // Seleccionar un índice aleatorio
            GameObject selectedObject = objectsToPlace[randomIndex]; // Objeto seleccionado

            // Verificar si el objeto seleccionado ya ha sido movido previamente
            if (movedObjects.Contains(selectedObject))
            {
                i--; // Restar 1 al contador para repetir el ciclo y seleccionar otro objeto
                continue;
            }

            Transform obstacle = selectedObject.transform;

            // Mover el objeto seleccionado y establecer la posición encima del objeto carretera
            int randomZ = Random.Range(0, 3);
            
            if (obstacle.tag == "ObstacleHT")
            {
                selectedObject.transform.position = new Vector3(positionObsX[i], roadObject.transform.position.y + obstacle.localScale.y * 1.1f, positionObsZ[randomZ]);
            }
            else if (obstacle.tag == "ObstacleHN")
            {
                selectedObject.transform.position = new Vector3(positionObsX[i], roadObject.transform.position.y + obstacle.localScale.y * 2.5f, positionObsZ[randomZ]);
            }
            else selectedObject.transform.position = new Vector3(positionObsX[i], roadObject.transform.position.y + obstacle.localScale.y / 2, positionObsZ[randomZ]);

            // Establecer el objeto carretera como padre del objeto movido
            selectedObject.transform.SetParent(roadObject);

            // Agregar el objeto seleccionado a la lista de objetos movidos
            movedObjects.Add(selectedObject);

            // Eliminar el objeto seleccionado del array
            objectsToPlace = objectsToPlace.Where(obj => obj != selectedObject).ToArray();
        }
    }

    void PlaceObjects2()
    {
        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, objectsToPlace.Length); // Seleccionar un índice aleatorio
            GameObject selectedObject = objectsToPlace[randomIndex]; // Objeto seleccionado

            // Verificar si el objeto seleccionado ya ha sido movido previamente
            if (movedObjects.Contains(selectedObject))
            {
                i--; // Restar 1 al contador para repetir el ciclo y seleccionar otro objeto
                continue;
            }

            Transform obstacle = selectedObject.transform;

            // Mover el objeto seleccionado y establecer la posición encima del objeto carretera
            int randomZ = Random.Range(3, 6);
            
            if (obstacle.tag == "ObstacleHT")
            {
                selectedObject.transform.position = new Vector3(positionObsX[i], roadObject.transform.position.y + obstacle.localScale.y * 1.1f, positionObsZ[randomZ]);
            }
            else if (obstacle.tag == "ObstacleHN")
            {
                selectedObject.transform.position = new Vector3(positionObsX[i], roadObject.transform.position.y + obstacle.localScale.y * 2.5f, positionObsZ[randomZ]);
            }
            else selectedObject.transform.position = new Vector3(positionObsX[i], roadObject.transform.position.y + obstacle.localScale.y / 2, positionObsZ[randomZ]);

            // Establecer el objeto carretera como padre del objeto movido
            selectedObject.transform.SetParent(roadObject);

            // Agregar el objeto seleccionado a la lista de objetos movidos
            movedObjects.Add(selectedObject);

            // Eliminar el objeto seleccionado del array
            objectsToPlace = objectsToPlace.Where(obj => obj != selectedObject).ToArray();
        }
    }
}
