
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AirconAnim : UdonSharpBehaviour
{
    public float speed = 10.0f;
    public int closedWeight = 80, opennedWeight = 40;
    private bool isOpen = false;
    private float weight = 60;
    public SkinnedMeshRenderer renderer;
    void Start()
    {
        renderer = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
    }

    private void Update()
    {
        if (closedWeight < weight) isOpen = false;
        else if (opennedWeight > weight) isOpen = true;

        weight += (isOpen ? 1 : -1) * UnityEngine.Time.deltaTime * speed;
        renderer.SetBlendShapeWeight(0, weight);
    }
}
