using UnityEngine;

public class SharkSpawner : MonoBehaviour {
    [SerializeField] GameObject objectPrefab;
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] int numberOfObjects;
    [SerializeField] int xRange = 10;
    [SerializeField] int yRange = 0;
    [SerializeField] int zRange = 10;
    [SerializeField] int maxSpeed = 6;

    int minSpeed = 2;

    private static SharkSpawner instance;
    public static SharkSpawner Instance { get {
            if (instance == null)
                instance = GameObject.FindAnyObjectByType<SharkSpawner>();
            return instance;
        } }


    private void Start() {
        // Spawn initial objects
        for (int i = 0; i < numberOfObjects; i++) {
            Transform startPos = Random.value < 0.5f ? startPoint : endPoint;
            Transform endPos = startPos == startPoint ? endPoint : startPoint;

            Vector3 start = new Vector3(startPos.position.x + Random.Range(-xRange, xRange),
                startPos.position.y + Random.Range(-yRange, yRange),
                startPos.position.z + Random.Range(-zRange, zRange));
            Vector3 end = new Vector3(start.x, start.y, endPos.position.z);

            GameObject spawnedObject = Instantiate(objectPrefab, start, Quaternion.identity);

            Respawn(spawnedObject, start, end);
        }
    }

    public void Respawn(GameObject spawnedObject, Vector3 start, Vector3 end) {
        // Calculate the direction from the spawned object to the target transform
        Vector3 direction = spawnedObject.transform.position - end;
        // Rotate the spawned object to face the target transform
        Quaternion rotation = Quaternion.Euler(0f, -90f, 0f);
        direction = rotation * direction;
        spawnedObject.transform.rotation = Quaternion.LookRotation(direction);

        Shark spawnedShark = spawnedObject.GetComponent<Shark>();
        spawnedShark.startPoint = start;
        spawnedShark.endPoint = end;
        spawnedShark.movementSpeed = Random.Range(minSpeed, maxSpeed);
    }
}
