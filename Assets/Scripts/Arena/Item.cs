using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Types
    {
        Experience,
        Health,
        Bomb,
        Magnet
	}
	public Types types = Types.Experience;

    public int amount = 0;

    void OnTriggerEnter(Collider other)
    {
        Combat combat = other.GetComponent<Combat>();

        if (other.transform.CompareTag("Player") && combat != null)
        {
            switch (types)
            {
                case Types.Experience:
                    combat.currentEXP += amount;

                    if (combat.currentEXP >= combat.maxEXP)
                    {
                        combat.currentLevel += 1;
                        combat.currentEXP = 0;
                    }

                    Destroy(gameObject);
                    break;
                case Types.Health:
                    combat.health += amount;
                    combat.health = combat.health > combat.maxHealth ? combat.maxHealth : combat.health;
                    Destroy(gameObject);
                    break;
                case Types.Bomb:

                    break;
                case Types.Magnet:
                    
                    break;
            }
        }
    }
}
