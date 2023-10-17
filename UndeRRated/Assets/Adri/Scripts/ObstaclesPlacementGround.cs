using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesPlacementGround : MonoBehaviour
{
    public GameObject[] objectsToPlace; // Array de objetos a seleccionar
    public List<GameObject> movedObjects = new List<GameObject>();
    public Transform roadObject; // Objeto carretera

    void Start()
    {
        PlaceObjects();
    }

    void PlaceObjects()
    {
        for (int i = 0; i < 6; i++)
        {
            int randomIndex = Random.Range(0, objectsToPlace.Length); // Seleccionar un índice aleatorio
            GameObject selectedObject = objectsToPlace[randomIndex]; // Objeto seleccionado

            // Verificar si el objeto seleccionado ya ha sido movido previamente
            if (movedObjects.Contains(selectedObject))
            {
                i--; // Restar 1 al contador para repetir el ciclo y seleccionar otro objeto
                continue;
            }

            Transform a = selectedObject.transform;

            // Mover el objeto seleccionado y establecer la posición encima del objeto carretera
            float randomX = Random.Range(roadObject.transform.position.x - roadObject.localScale.x / 2, roadObject.transform.position.x + roadObject.localScale.x/2);
            float randomZ = Random.Range(roadObject.transform.position.z - roadObject.localScale.z / 2, roadObject.transform.position.z + roadObject.localScale.z/2);
            selectedObject.transform.position = new Vector3(randomX, roadObject.transform.position.y + a.localScale.y/2, randomZ);
            
            // Establecer el objeto carretera como padre del objeto movido
            selectedObject.transform.SetParent(roadObject);

            // Agregar el objeto seleccionado a la lista de objetos movidos
            movedObjects.Add(selectedObject);

            // Eliminar el objeto seleccionado del array
            objectsToPlace = objectsToPlace.Where(obj => obj != selectedObject).ToArray();
        }
    }
}
