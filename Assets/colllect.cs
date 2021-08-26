using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colllect : MonoBehaviour
{
    [SerializeField] private string _resourseType;
    private string Id => name + transform.position;
    private void Awake()
    {
        if (PlayerPrefs.HasKey(Id))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerControl>();
        if (player)
        {
            player.GetResource(_resourseType);
            Destroy(gameObject);
            PlayerPrefs.SetFloat(Id, 1);
        }
    }
}
