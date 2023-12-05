using UnityEngine;

public class TeleportRoad : MonoBehaviour
{
   
    public Transform road1;
    public Transform road2;
    public Transform road3;
    

    //road3.position.z + 52.82955f
    void OnTriggerEnter(Collider other)
    {
        // We move the roads that already passed the camera(the moment they hit the collider) to the end of the circuit
        // so it generates the illusion of being an infinite road.
        if (other.gameObject.name == "RoadTile")
        {
            other.gameObject.transform.position = new Vector3(road3.position.x, road3.position.y, road3.position.z + road1.localScale.z);

           ObstaclesPlacementGround.PlaceObjects1(other.gameObject.transform);
            //ObstaclesPlacementGround.PlaceObjects2(other.gameObject.transform);
        }
        else if (other.gameObject.name == "RoadTile2")
        {
            other.gameObject.transform.position = new Vector3(road1.position.x, road1.position.y, road1.position.z + road1.localScale.z);
            ObstaclesPlacementGround.PlaceObjects1(other.gameObject.transform);
            //ObstaclesPlacementGround.PlaceObjects2(other.gameObject.transform);

        }
        else if (other.gameObject.name == "RoadTile3")
        {
            other.gameObject.transform.position = new Vector3(road2.position.x, road2.position.y, road2.position.z + road1.localScale.z);

            ObstaclesPlacementGround.PlaceObjects1(other.gameObject.transform);
            //ObstaclesPlacementGround.PlaceObjects2(other.gameObject.transform);
        }
        ObstaclesPlacementGround.CheesePLacement(other.gameObject.transform);
    }
}
