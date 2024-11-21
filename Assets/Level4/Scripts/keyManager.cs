using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private List<Collectable> keys; // List of all keys in the scene

    private Collectable realKey; // Reference to the real key

    void Start()
    {
        AssignRealKey();
    }

    /// <summary>
    /// Randomly assigns one key as the real key.
    /// </summary>
    private void AssignRealKey()
    {
        if (keys == null || keys.Count == 0)
        {
            Debug.LogError("No keys assigned to the KeyManager!");
            return;
        }

        // Reset all keys to be "not real"
        foreach (Collectable key in keys)
        {
            KeyScript keyScript = key.GetComponent<KeyScript>();
            if (keyScript != null)
            {
                keyScript.isRealKey = false;
            }
        }

        // Randomly pick one key to be the real key
        int realKeyIndex = Random.Range(0, keys.Count);
        realKey = keys[realKeyIndex];
        KeyScript realKeyScript = realKey.GetComponent<KeyScript>();
        if (realKeyScript != null)
        {
            realKeyScript.isRealKey = true;
        }

        Debug.Log($"The real key is: {realKey.name}");
    }
}
