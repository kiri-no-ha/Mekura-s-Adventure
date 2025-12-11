using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Entity
{
    // Start is called before the first frame update
    public int count_in_stack = 1;
    public int amount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Item CloneObject()
    {
        return new Item(); 
    }
}
