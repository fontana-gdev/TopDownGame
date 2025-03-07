using UnityEngine;

public class FarmHarvestSlot : MonoBehaviour
{
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip harvestSFX;
    
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite holeSprite;
    [SerializeField] private Sprite carrotSprite;
    [SerializeField] private ParticleSystem dirtParticles;
    
    [Header("Settings")]
    [SerializeField] private int digAmount;
    [SerializeField] private float waterNeeded;
    
    [Header("Debug")]
    [SerializeField] private bool beingWatered;
    [SerializeField] private float currentWater;
    [SerializeField] private bool playerInRange;
    
    private int initialDigAmount;
    private bool isDug;
    
    PlayerItens playerItens;

    private void Awake()
    {
        playerItens = FindObjectOfType<PlayerItens>();
    }

    private void Start()
    {
        initialDigAmount = digAmount;
       
    }

    private void Update()
    {
        if (isDug && beingWatered)
        {
            currentWater += 0.01f;
        }

        if (currentWater >= waterNeeded)
        {
            spriteRenderer.sprite = carrotSprite;

            if (Input.GetKeyDown(KeyCode.E) && playerInRange)
            {
                HarvestCarrot();
            }
        }
        
    }

    private void HarvestCarrot()
    {
        currentWater = 0;
        spriteRenderer.sprite = holeSprite;
        playerItens.AddCarrot(1);
        audioSource.PlayOneShot(harvestSFX);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dig"))
        {
            OnDig();
        }

        if (other.CompareTag("Water"))
        {
            beingWatered = true;
        }

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            beingWatered = false;
        }
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void OnDig()
    {
        if (isDug) return;
        
        digAmount--;
        dirtParticles.Play();

        if (digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = holeSprite;
            audioSource.PlayOneShot(holeSFX);
            isDug = true;
        }

        if (digAmount < 0)
        {
            spriteRenderer.sprite = carrotSprite;
        }
    }
    
}
