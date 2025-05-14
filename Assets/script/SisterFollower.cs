using UnityEngine;

public class SisterFollower : MonoBehaviour
{

    public PlayerMovement Gyaru;
    public int lateness = 20;

    // Si la liste contient au moins un element on va sur la premiere pos de la liste et on supp la premiere pos de la liste
    void Update()
    {
        if (Gyaru.LatestPath.Count >= lateness)
        {
            Vector2 Position = Gyaru.LatestPath[0];
            transform.position = Position;
            Gyaru.LatestPath.RemoveAt(0);
        }
    }
}
