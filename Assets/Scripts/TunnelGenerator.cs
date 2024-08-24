using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelGenerator : MonoBehaviour
{
    public Transform Rail;
    public float Speed = 1f;
    public float GenerateTime = 1f, DestroyTime = 70f;

    public GameObject[] TunnelPrefabs;

    private float lastX = 20f;

    private void Start()
    {
        StartCoroutine(GenerateTunnel());
    }

    private void FixedUpdate()
    {
        Vector3 newPos = Rail.position;
        Rail.position = new Vector3(newPos.x - (Speed * Time.fixedDeltaTime), newPos.y, newPos.z);
    }

    IEnumerator GenerateTunnel()
    {
        yield return new WaitForSeconds(GenerateTime);
        var tunnel = Instantiate(TunnelPrefabs[Random.Range(0, TunnelPrefabs.Length)], new Vector3(100f, 0f, 0f), Quaternion.identity, Rail);
        tunnel.transform.localPosition = new Vector3(lastX + 10f, 0f, 0f);
        lastX += 10f;
        Destroy(tunnel, DestroyTime);

        StartCoroutine(GenerateTunnel());
    }
}
