using UnityEngine;

public class ParticleController : MonoBehaviour
{
    private void Start()
    {
        ParticleSystem psys = this.GetComponent<ParticleSystem>();
        Destroy(this.gameObject, psys.main.duration);
    }
    
}
