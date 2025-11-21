using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class ScriptBumper : MonoBehaviour
{
    [Header("Bump")]
    [SerializeField] float bounceMultiplier = 2.0f; // amplification de l'impact
    [SerializeField] float bonusOutSpeed   = 30f;    // vitesse ajoutée
    [SerializeField] float minOutSpeed     = 5f;    // plancher de sortie
    [SerializeField] float maxOutSpeed     = 400f;   // plafond de sécurité

    void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = false;
    }

    void OnCollisionEnter(Collision c)
    {
        var rb = c.rigidbody;
        if (!rb) return;


        var contact = c.GetContact(0);
        Vector3 n   = contact.normal;       
        Vector3 vIn = c.relativeVelocity;  
     
        float speedIn = Vector3.Dot(-vIn, n);
        if (speedIn <= 0f) return;


        Vector3 outDir     = Vector3.Reflect(vIn, n).normalized;
        float  targetSpeed = Mathf.Clamp(Mathf.Max(minOutSpeed, speedIn * bounceMultiplier) + bonusOutSpeed,
                                         0f, maxOutSpeed);

  
        rb.linearVelocity = outDir * targetSpeed;

      
        Debug.DrawRay(contact.point, n,      Color.green, 0.5f);
        Debug.DrawRay(contact.point, outDir, Color.cyan,  0.5f);
    }
}
