using UnityEngine;
using System.Collections.Generic;

public class Level4PlayerAttack : PlayerAttack
{
    [SerializeField] protected List<Collectable> gunCollectables; // List to hold multiple guns

    protected override void Awake()
    {
        base.Awake();
        gunCollectables = new List<Collectable>(); // Initialize the list
    }

    public override bool canAttack()
    {
        // Check if at least one gun in the list is collected
        if (gunCollectables != null && gunCollectables.Count > 0)
        {
            foreach (Collectable gun in gunCollectables)
            {
                if (gun != null && gun.IsCollected)
                {
                    return true; // At least one gun is collected
                }
            }
        }
        return false; // No guns are collected
    }
    public void AddGun(Collectable newGun)
    {
        if (newGun != null && !gunCollectables.Contains(newGun))
        {
            gunCollectables.Add(newGun);
            Debug.Log($"Gun added: {newGun.name}");
        }
    }

    public void RemoveGun(Collectable gunToRemove)
    {
        if (gunToRemove != null && gunCollectables.Contains(gunToRemove))
        {
            gunCollectables.Remove(gunToRemove);
            Debug.Log($"Gun removed: {gunToRemove.name}");
        }
    }
}
