using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Unity.Mathematics;

public class Follow_player : MonoBehaviour
{

    public Transform playerTransform;
    public GameObject playerObj;
    private Rigidbody2D playerRb;
    public float lightDistanceFromPlayer; // tem q ser maior q 0

    public float yOffset;
    void Start()
    {
        playerRb = playerObj.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        // float moveX = Input.GetAxisRaw("Horizontal");
        // float moveY = Input.GetAxisRaw("Vertical");
        // Vector2 moveDirection = new Vector2(moveX, moveY);
        Vector3 playerMovement = playerRb.velocity;
        if (playerMovement != Vector3.zero)
        {
            //pega nova posição da luz
            Vector3 newPosition = playerTransform.position + 
                (playerMovement.normalized * lightDistanceFromPlayer);
            
            //pega novo angulo entre o player e a direção = tan(altura y, distancia x)
            float newAngle = Mathf.Atan2(
                playerTransform.position.y - newPosition.y,
                playerTransform.position.x - newPosition.x  
            ) * Mathf.Rad2Deg + 90;
            newPosition.y += yOffset;

            transform.position = newPosition;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));
            Debug.Log(newAngle);
        }
    }

    Vector3 V2toV3(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0.0f);
    }
}