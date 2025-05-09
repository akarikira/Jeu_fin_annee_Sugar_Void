using UnityEngine;

public class MouvementSimple : MonoBehaviour
{
    public float vitesseDeplacement = 5f;
    private Rigidbody2D rb;
    private Vector2 mouvement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        mouvement = Vector2.zero;


        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow))
        {
            mouvement.y = 1f;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            mouvement.y = -1f;
        }
        else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
        {
            mouvement.x = -1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            mouvement.x = 1f;
        }


        if (mouvement.x != 0) mouvement.y = 0;
        if (mouvement.y != 0) mouvement.x = 0;
    }
     void FixedUpdate()
     {

    rb.linearVelocity = mouvement.normalized * vitesseDeplacement;
     }
}