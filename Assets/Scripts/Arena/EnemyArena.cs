using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArena : MonoBehaviour
{
    [SerializeField] public GameObject resource;
    [SerializeField] public List<GameObject> listItems = new List<GameObject>();

    public void CreateRandomResource()
    {
        GameObject randomObject = GetWeightedRandomObject();

        if (randomObject != null)
        {
            Instantiate(randomObject, transform.position, Quaternion.identity);
        }

        Instantiate(resource, new Vector3 (transform.position.x + 2, transform.position.y, transform.position.z + 2f), Quaternion.identity);
    }
    
    GameObject GetWeightedRandomObject()
    {     
		int totalWeight = listItems.Count * (listItems.Count + 1) / 2;
        int randomValue = UnityEngine.Random.Range(0, totalWeight + 1);
        
        // Find out what object has fallen out
        int currentWeight = 0;
		
        for (int i = 0; i < listItems.Count; i++)
		{
			// First element has the maximum weight
			currentWeight += (listItems.Count - i);

			if (randomValue <= currentWeight)
			{
				return listItems[i];
			}
		}
        
        // Return the last element if nothing was found
        return listItems[listItems.Count - 1];
    }
}
