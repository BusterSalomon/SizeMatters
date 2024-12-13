using UnityEngine;

public class Level4PlayerAttack : PlayerAttack
{
    /*protected override void Awake()
    {
        base.Awake();
        FindGunCollectable();
    }*/

    protected override void Update()
    {
        base.Update();
        FindGunCollectable(); // Continuously update the gunCollectable during gameplay
    }

    private void FindGunCollectable()
    {
        Collectable[] collectables = FindObjectsOfType<Collectable>();

        foreach (var collectable in collectables)
        {
            if (collectable.IsCollected && collectable.transform.parent == this.transform)
            {
                gunCollectable = collectable;
                Debug.Log($"GunCollectable set to {gunCollectable.name}");
                return;
            }
        }

        // If no gun is found, set gunCollectable to null
        gunCollectable = null;
    }

    public override bool canAttack()
    {
        return gunCollectable != null && gunCollectable.IsCollected;
    }
}
