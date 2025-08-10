using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public string entityName;
    public Vector3 position;
    public virtual void Interact(Entity other) { }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
