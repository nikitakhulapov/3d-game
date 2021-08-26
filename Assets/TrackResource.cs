using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackResource : MonoBehaviour
{
    [SerializeField] private string _resourceName;
    private void Awake()
    {
        foreach(var resource in FindObjectsOfType<Resource>())
        {
            if (resource.ResourceName == _resourceName)
            {
                resource.OnChange += Changed;
            }
        }
    }
    private void Changed(float value)
    {
        GetComponentInChildren<UnityEngine.UI.Text>().text = Mathf.FloorToInt(value).ToString();
    }
}
