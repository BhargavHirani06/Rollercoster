using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;

public class SplineFollower : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool lookForward = true;
    
    private float currentProgress = 0f;
    
    void Update()
    {
        if (splineContainer == null) return;
        
        // Move along the spline
        currentProgress += speed * Time.deltaTime / splineContainer.Spline.GetLength();
        
        if (loop)
        {
            currentProgress = currentProgress % 1f;
        }
        else
        {
            currentProgress = Mathf.Clamp01(currentProgress);
        }
        
        // Get position on spline
        splineContainer.Evaluate(currentProgress, out float3 position, out float3 tangent, out float3 up);
        
        // Set cube position
        transform.position = position;
        
        // Optionally rotate to face forward along spline
        if (lookForward && math.lengthsq(tangent) > 0)
        {
            transform.rotation = Quaternion.LookRotation(tangent, up);
        }
    }
}
