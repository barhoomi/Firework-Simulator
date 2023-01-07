using UnityEngine;
using Random = UnityEngine.Random;

public class spawnFireworks : MonoBehaviour
{

    public GameObject fireworkParticle;

    public float spawnRadius;
    public float localSpawnRadius;

    public int numberOfParticles;

    public bool useGravity;
    public float drag;

    private float previousSpawnTime;
    public float lifeTime;
    public float spawnPeriod;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - previousSpawnTime > spawnPeriod)
        {
            generateFirework(numberOfParticles);
            previousSpawnTime= Time.time;
        }
    }

    Vector3 generateRandomVector3(float min, float max)
    {
        return new Vector3(
            Random.Range(min, max),
            Random.Range(min, max),
            Random.Range(min, max)
            );
    }

    Color generateRandomColor()
    {
        float r = Random.Range(0, 1f);
        float g = Random.Range(0, 1f);
        float b = Random.Range(0, 1f);

        switch (Random.Range(1, 4))
        {
            case 1: r = 1f; break;
            case 2: g = 1f; break;
            case 3: b = 1f; break;
        }

        return new Color(r, g, b);
    }

    void generateFirework(int particles)
    {
        Vector3 center = transform.position;

        Vector3 cluster = generateRandomVector3(-spawnRadius, spawnRadius);

        Color color = generateRandomColor();

        for (int i = 0; i < particles; i++)
        {
            Vector3 random = generateRandomVector3(-localSpawnRadius, localSpawnRadius);
            GameObject obj = Instantiate(fireworkParticle, center + cluster + random, Quaternion.identity);

            var rigidBody = obj.GetComponent<Rigidbody>();
            rigidBody.useGravity = useGravity;
            rigidBody.drag = drag;

            var renderer = obj.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", color);

            var trail = obj.GetComponent<TrailRenderer>();
            trail.material.SetColor("_Color", color);

            Destroy(obj, lifeTime);
        }
    }
}


