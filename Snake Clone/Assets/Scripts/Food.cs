using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("Bounds")]
    public BoxCollider2D gridArea;
    [Space(1)]
    [Header("SuperFood Variables")]
    public bool isSuper = false;
    [Range(1, 100)]
    public int superChance = 50;
    [Space(1)]
    [Header("Particle Effects")]
    public ParticleSystem superEffect;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            RandomizePosition();
            int dropRange = Random.Range(1, 100);
            superEffect.Clear();
            if (dropRange < superChance)
            {
                isSuper = true;
                superEffect.Play();
                Invoke("ChangeFoodSuper", 0.1f);
            }
            else
            {
                isSuper = false;
                superEffect.Clear();
                superEffect.Pause();
                Invoke("ChangeFoodNormal", 0.1f);
            }
        }

    }

    private void ChangeFoodSuper()
    {
        this.tag = "SuperFood";
    }
    private void ChangeFoodNormal()
    {
        this.tag = "Food";
    }
}
