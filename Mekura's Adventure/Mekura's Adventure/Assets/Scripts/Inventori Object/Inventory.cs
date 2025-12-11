using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    public int lenght = 10;
    public List<Item> inventory;
    void Start()
    {
        inventory = new List<Item>(lenght);
        Debug.Log(inventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<Item> GetItems()
    {
        return inventory;
    }
    public bool AddItem(Item item)
    {
        bool win = false;
        int item_index = HasIN(item);
        if (item_index >= 0)
        {
            inventory[item_index].amount += item.amount;
            win = true;
            while (inventory[item_index].amount > inventory[item_index].count_in_stack)
            {
                int index_emty = HasEmpty();
                Item item_for_emty = inventory[item_index].CloneObject();
                item_for_emty.amount = inventory[item_index].amount - inventory[item_index].count_in_stack;
                inventory[item_index].amount = inventory[item_index].count_in_stack;
                if (index_emty >= 0)
                { 
                    inventory[index_emty] = item_for_emty;
                    item_index = index_emty;
                }
                else
                {
                    DropItem(item_for_emty);
                    win = false;
                    break;
                }
            }
        }
        else
        {
            // переделать
            int new_item_index = HasEmpty();
            inventory[new_item_index] = item;
            while (inventory[new_item_index].amount > inventory[new_item_index].count_in_stack)
            {
                int index_emty = HasEmpty();
                Item item_for_emty = inventory[new_item_index].CloneObject();
                item_for_emty.amount = inventory[new_item_index].amount - inventory[new_item_index].count_in_stack;
                inventory[new_item_index].amount = inventory[new_item_index].count_in_stack;
                if (index_emty >= 0)
                {
                    inventory[index_emty] = item_for_emty;
                    new_item_index = index_emty;
                }
                else
                {
                    DropItem(item_for_emty);
                    win = false;
                    break;
                }
            }
        }
        return win;
            //if (HasIN(item))
            //{
            //    int i = 0;
            //    while (item.amount<=0)
            //    {
            //        if (inventory[i] == item)
            //        {

            //        }
            //    }
            //}
            //for (int i = 0; i < lenght; i++)
            //{
            //    if (inventory[i] == item)
            //    {
            //        //inventory[i].amount += item.amount;
            //        //Item over_item = inventory[i];
            //        //int j = 0;
            //        //while (over_item.amount> over_item.count_in_stack)
            //        //{

            //        //}
            //        ////if (inventory[i].amount > inventory[i].count_in_stack)
            //        ////{
            //        ////    inventory[i].amount = inventory[i].count_in_stack;

            //        ////}
            //    }
            //}
    }
    public bool DelItem(Item item)
    {
        int current_item = HasIN(item);
        while (current_item > 0 || item.amount > 0) 
        {
            int diff_amount = inventory[current_item].amount - item.amount;
            if (diff_amount > 0)
            {
                inventory[current_item].amount -= item.amount;
                item.amount = 0;
                return true;
            }
            else
            {
                inventory[current_item] = null;
                item.amount -= diff_amount;
            }
        }
        return false;
        
    }
    public int HasIN(Item item)
    {
        for(int i = 0; i<lenght; i++)
        {
            if (inventory[i] == item)
            {
                return i;
            }
        }
        return -1;
    }
    public void SortItems()
    {
        List<Item> new_list = new List<Item>(lenght);
        int j = 0;
        for (int i =0; i<lenght; i++)
        {
            if (inventory[i] != null)
            { 
                new_list[j] = inventory[i];
                j++; 
            }
        }
        inventory = new_list;
    }
    public void SetNewLenght(int new_len)
    {
        if (new_len > lenght)
        {
            SortItems();
            inventory.AddRange(new List<Item>(lenght - new_len));
            lenght = new_len;
        }
        else
        {
            SortItems();
            List <Item>  new_inventory = new List<Item>(new_len);
            List<Item> drop_list = new List<Item>(lenght - new_len);
            for (int i = 0;i < lenght; i++)
            {
                if (i < new_len - 1)
                {
                    new_inventory[i] = inventory[i];
                }
                else
                {
                    drop_list[i] = inventory[i];
                }
            }
            inventory = new_inventory;
            lenght = new_len;
            foreach (Item item in drop_list)
            {
                DropItem(item);
            }
        }
    }
    public int HasEmpty()
    {
        for (int i = 0; i<lenght; i++)
        {
            if (inventory[i] == null)
            {
                return i;
            }
        }
        return -1;
    }
    public void DropItem(Item item)
    {
        // Механика справна например меча как самого предмета на пол
        // Instantiate(spawnobject)
    }
}
