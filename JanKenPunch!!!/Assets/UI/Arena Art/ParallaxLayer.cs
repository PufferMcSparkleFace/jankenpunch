using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform layerTransform;
    [Range(0f, 1f)]
    public float parallaxFactor = 0.5f;
}

public class MultiLayerParallax : MonoBehaviour
{
    public ParallaxLayer[] layers;
    private Transform cam;
    private Vector3 prevPos;

    void Start()
    {
        cam = Camera.main.transform;
        prevPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - prevPos;

        foreach (var layer in layers)
        {
            if (layer.layerTransform)
            {
                layer.layerTransform.position += new Vector3(delta.x * layer.parallaxFactor, delta.y * layer.parallaxFactor);
            }
        }

        prevPos = cam.position;
    }
}