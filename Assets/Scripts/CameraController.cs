using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private float spd;
    [SerializeField] private Transform player;
    private float cx;
    private Vector3 velocity = Vector3.zero;


    private void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 1, -5);
    }

}
