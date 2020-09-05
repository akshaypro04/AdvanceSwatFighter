using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Container : MonoBehaviour
{
    [System.Serializable]
    public class ContainerItem
    {
        public System.Guid Id;
        public string Name;
        public int Maximum;
        public int amountTaken;                                       // how much ammo we used

        public int Remaining
        {
            get
            {
                return Maximum - amountTaken;
            }
        }

        public ContainerItem()
        {
            Id = System.Guid.NewGuid();
        }


        public int Get(int Value)
        {
            if((amountTaken + Value) > Maximum)                   
            {
                int ToMuch = (amountTaken + Value) - Maximum;
                amountTaken = Maximum;
                return Value - ToMuch;
            }
            else
            {
                amountTaken += Value;
                return Value;
            }
        }

        public void Set(int amount)                           // item add to instance      means make less in amountTaken 
        {
            amountTaken -= amount;
            if (amountTaken < 0)
                amountTaken = 0;
        }
    }

    public List<ContainerItem> items;                          // item is instance of ContainerItem
    public event System.Action OnContainerReady;

    void Awake()
    {
        items = new List<ContainerItem>();
          if(OnContainerReady != null)
            OnContainerReady();                                  //  add weapon name and max Ammo in inventary
    }

    public System.Guid Add(string name, int maximum)            // item added to ContainerItem
    {
        items.Add(new ContainerItem {
            Maximum = maximum,                                  // set maxAmmo ContainerItem 
            Name = name                                         // set name ContainerItem 
        });

        return items.Last().Id;
    }

    public void Put(string name, int amount)                   // founded ammo add
    {
        var Item = items.Where(x => x.Name == name).FirstOrDefault();
        if (Item == null)
            return;

        Item.Set(amount);

    }

    public int TakeFromContainer(System.Guid id, int amount)                      // take Single ammo at a time for now
    {
        var Item = GetContainerItem(id);
        if (Item == null)
            return -1;
        return Item.Get(amount);
    }

    public int GetAmountRemaining(System.Guid id)
    {
        var Item = GetContainerItem(id);
        if (Item == null)
            return -1;
        return Item.Remaining;
    }

    private ContainerItem GetContainerItem(System.Guid id)
    {
        var Item = items.Where(x => x.Id == id).FirstOrDefault();
        if (Item == null)
            return null;
        return Item;
    }        
              
}
