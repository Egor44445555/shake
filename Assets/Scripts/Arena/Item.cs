using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float moveSpeed = 15f;
    bool moveToPlayer = false;

    [Header("Animate")]
    public bool isRotating = false;
    public bool rotateX = false;
    public bool rotateY = false;
    public bool rotateZ = false;
    public float rotationSpeed = 90f; // Degrees per second

    public bool isFloating = false;
    public bool useEasingForFloating = false; // Separate toggle for floating ease
    public float floatHeight = 1f; // Max height displacement
    public float floatSpeed = 1f;
    Vector3 initialPosition;
    float floatTimer;

    Vector3 initialScale;
    public Vector3 startScale;
    public Vector3 endScale;

    public bool isScaling = false;
    public bool useEasingForScaling = false; // Separate toggle for scaling ease
    public float scaleLerpSpeed = 1f; // Speed of scaling transition
    float scaleTimer;

    void Start()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;

        // Adjust start and end scale based on initial scale
        startScale = initialScale;
        endScale = initialScale * (endScale.magnitude / startScale.magnitude);

        if (UIManager.main)
        {
            UIManager.main.XPCount.text = Spawner.main.currentEXP.ToString() + " / " + Spawner.main.maxEXP.ToString();
        }
    }

    void Update()
    {
        // Flight of an object to the player
        if (moveToPlayer)
        {
            Vector3 targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        
        if (isRotating && !moveToPlayer)
        {
            Vector3 rotationVector = new Vector3(
                rotateX ? 1 : 0,
                rotateY ? 1 : 0,
                rotateZ ? 1 : 0
            );
            transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
        }

        if (isFloating && !moveToPlayer)
        {
            floatTimer += Time.deltaTime * floatSpeed;
            float t = Mathf.PingPong(floatTimer, 1f);
            if (useEasingForFloating) t = EaseInOutQuad(t);

            transform.position = initialPosition + new Vector3(0, t * floatHeight, 0);
        }

        if (isScaling && !moveToPlayer)
        {
            scaleTimer += Time.deltaTime * scaleLerpSpeed;
            float t = Mathf.PingPong(scaleTimer, 1f); // Oscillates between 0 and 1

            if (useEasingForScaling)
            {
                t = EaseInOutQuad(t);
            }

            transform.localScale = Vector3.Lerp(startScale, endScale, t);
        }
    }

    float EaseInOutQuad(float t)
    {
        return t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
    }
    
    void OnTriggerEnter(Collider other)
    {
        Combat combat = other.GetComponent<Combat>();

        if (other.transform.CompareTag("Player") && combat != null)
        {
            switch (types)
            {
                case Types.Experience:
                    Spawner.main.currentEXP += amount;

                    if (Spawner.main.currentEXP >= Spawner.main.maxEXP)
                    {
                        Spawner.main.currentLevel += 1;
                        Spawner.main.currentEXP = 0;
                        Spawner.main.maxEXP += 5;
                        Instantiate(Spawner.main.followerPrefab, transform.position, Quaternion.identity);
                        GameManager.Instance.LevelManager.CountABro();
                    }

                    UIManager.main.XPCount.text = Spawner.main.currentEXP.ToString() + "/" + Spawner.main.maxEXP.ToString();
                    Destroy(gameObject);
                    break;
                case Types.Health:
                    combat.health += amount;
                    combat.health = combat.health > combat.maxHealth ? combat.maxHealth : combat.health;
                    Destroy(gameObject);
                    break;
                case Types.Magnet:
                    foreach (Item item in FindObjectsOfType<Item>())
                    {
                        if (item.types.ToString() == "Experience")
                        {
                            item.moveToPlayer = true;
                        }
                    }

                    Destroy(gameObject);
                    break;
            }
        }
    }
}
