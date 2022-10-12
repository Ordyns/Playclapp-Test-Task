using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceVisualizer : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;

    public void SetDistance(float distance){
        lineRenderer.SetPositions(new Vector3[] { transform.position, transform.position + new Vector3(distance, 0, 0) });
    }

    public void Show() => lineRenderer.gameObject.SetActive(true);
    public void Hide() => lineRenderer.gameObject.SetActive(false);
}
