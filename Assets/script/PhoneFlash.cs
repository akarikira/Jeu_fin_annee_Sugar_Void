using UnityEngine;

public class PhoneFlash : MonoBehaviour
{
    [Header("Réglages Flash")]
    [Tooltip("Durée du flash en secondes")] 
    public float flashDuration = 0.3f;
    [Tooltip("Temps entre chaque utilisation")]
    public float cooldown = 1.5f;
    [Space(10)]
    public float flashRange = 8f;
    public int enemyDamage = 15;
    
    [Header("Références")]
    [SerializeField] private Light flashLight;
    [SerializeField] private AudioSource audioSource;
    public AudioClip flashSound;
    public ParticleSystem flashEffect;

    private float nextFlashTime;
    private bool isReady = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isReady)
        {
            ActivateFlash();
        }
    }

    void ActivateFlash()
    {
        // Activation visuelle
        if(flashLight != null) flashLight.enabled = true;
        if(flashEffect != null) flashEffect.Play();
        
        // Activation sonore
        if(audioSource != null && flashSound != null)
            audioSource.PlayOneShot(flashSound);

        // Détection
        RevealSecrets();
        DamageEnemies();
        
        // Cooldown
        isReady = false;
        Invoke(nameof(ResetFlash), flashDuration);
        nextFlashTime = Time.time + cooldown;
    }

    void ResetFlash()
    {
        if(flashLight != null) flashLight.enabled = false;
        isReady = true;
    }

    void RevealSecrets()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, flashRange);
        
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out SecretObject secret))
            {
                secret.ShowHiddenContent();
            }
        }
    }

    void DamageEnemies()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, flashRange);
        
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(enemyDamage);
            }
        }
    }

    // Visualisation de la portée dans l'éditeur
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0.8f, 0.4f, 0.3f);
        Gizmos.DrawSphere(transform.position, flashRange);
    }
}