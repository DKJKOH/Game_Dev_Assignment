using UnityEngine;

[CreateAssetMenu(fileName = "Bullet Trail Configuration", menuName ="ScriptableObject/Bullet Trail Configuration")]

public class BulletTrailScriptableObject : ScriptableObject
{
    // Control how fat the curve is over time
    public AnimationCurve WidthCurve;

    // How long the bullet trail stays alive
    public float Time = 0.5f;

    // How far it should travel before new vertex is added
    public float MinVertexDist = 0.1f;

    // Color which is being applied over the trail
    public Gradient ColorGradient;

    public Material Material;

    // If bullets bend, how round the bend gonna be
    public int CornerVertices;

    // How sharp your end edge looks like
    public int EndCapVertices;

    public void TrailSetup(TrailRenderer TrailRenderer)
    {
        // Basically setting up the trail renderer with specified stuff above (Description found above)
        TrailRenderer.widthCurve = WidthCurve;

        TrailRenderer.time = Time;

        TrailRenderer.minVertexDistance= MinVertexDist;

        TrailRenderer.colorGradient = ColorGradient;

        TrailRenderer.sharedMaterial = Material;

        TrailRenderer.numCornerVertices = CornerVertices;

        TrailRenderer.numCapVertices = EndCapVertices;
    }
}
