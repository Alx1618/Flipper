using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PulseScaleXZAsym : MonoBehaviour
{
    [Header("Taille")]
    [SerializeField] float maxFactor = 1.5f;     // facteur max X/Z
    [SerializeField] float growDuration = 0.10f; // montée rapide
    [SerializeField] float shrinkDuration = 2f;  // descente lente
    [SerializeField] float waitDuration = 2f;    // pause à la taille d’origine

    Vector3 original;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;                           // collider piloté par Transform
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
       
    }

    void Start()
    {
        original = transform.localScale;
        StartCoroutine(PulseFixedLoop());
    }

    IEnumerator PulseFixedLoop()
    {
        for (;;)
        {
           
            float t = 0f;
            while (t < 1f)
            {
                t += Time.fixedDeltaTime / Mathf.Max(0.0001f, growDuration);
                float f = Mathf.SmoothStep(1f, maxFactor, Mathf.Clamp01(t));
                ApplyScale(f);
                Physics.SyncTransforms();                
                yield return new WaitForFixedUpdate();   
            }

            // RÉTRÉCIR
            t = 0f;
            while (t < 1f)
            {
                t += Time.fixedDeltaTime / Mathf.Max(0.0001f, shrinkDuration);
                float f = Mathf.SmoothStep(maxFactor, 1f, Mathf.Clamp01(t));
                ApplyScale(f);
                Physics.SyncTransforms();
                yield return new WaitForFixedUpdate();
            }

            // Taille d’origine + PAUSE
            ApplyScale(1f);
            Physics.SyncTransforms();
            float timer = waitDuration;
            while (timer > 0f) { timer -= Time.fixedDeltaTime; yield return new WaitForFixedUpdate(); }
        }
    }

    void ApplyScale(float factor)
    {
        transform.localScale = new Vector3(original.x * factor, original.y, original.z * factor);
    }
}
