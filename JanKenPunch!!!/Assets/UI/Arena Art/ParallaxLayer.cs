using UnityEngine;

public class FourLayerParallax : MonoBehaviour
{
    public Transform layer1;
    public float factor1 = 0.5f;

    public Transform layer2;
    public float factor2 = 0.4f;

    public Transform layer3;
    public float factor3 = 0.3f;

    public Transform layer4;
    public float factor4 = 0.2f;

    private Vector3 lastCameraPosition;
    private bool firstFrame = true;

    void LateUpdate()
    {
        if (Camera.main == null) return;

        if (firstFrame)
        {
            lastCameraPosition = Camera.main.transform.position;
            firstFrame = false;
        }

        Vector3 delta = Camera.main.transform.position - lastCameraPosition;

        if (layer1 != null)
            layer1.position += new Vector3(delta.x * factor1, delta.y * factor1, 0);
        if (layer2 != null)
            layer2.position += new Vector3(delta.x * factor2, delta.y * factor2, 0);
        if (layer3 != null)
            layer3.position += new Vector3(delta.x * factor3, delta.y * factor3, 0);
        if (layer4 != null)
            layer4.position += new Vector3(delta.x * factor4, delta.y * factor4, 0);

        lastCameraPosition = Camera.main.transform.position;
    }
}
