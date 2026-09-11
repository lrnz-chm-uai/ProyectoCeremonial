using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslucentObstacle : MonoBehaviour
{
    public Camera playerCamera;
    public Transform player;
    public Material opaqueMaterial, translucentMaterial;
    private Renderer rend;

    void Start()
    {
        playerCamera = Camera.main;
        player = GameObject.FindWithTag("Player").transform;
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 toCamera = (playerCamera.transform.position - transform.position).normalized;
        Vector3 toPlayer = (player.transform.position - transform.position).normalized;

        // Check if the player is behind the wall from the camera's perspective using the dot product
        bool playerBehindWall = Vector3.Dot(toCamera, toPlayer) < 0;
        // If playerBehindWall is true, it means the player is behind the wall
        rend.material = playerBehindWall ? translucentMaterial : opaqueMaterial;
    }
}
