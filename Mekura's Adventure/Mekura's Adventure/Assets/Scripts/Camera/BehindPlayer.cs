using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3 = player.position;
        vector3 = new Vector3(player.position.x, player.position.y, -10f);
        transform.position = vector3;
    }
}
