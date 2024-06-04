using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TargetArea : MonoBehaviour
{
    [SerializeField] private DecalProjector _decalProjector;

    public void SetArea(bool value)
    {
        _decalProjector.enabled = value;
    }
    
    public void SetArea(float size)
    {
        _decalProjector.size = new Vector3(size, size, _decalProjector.size.z);
    }
}