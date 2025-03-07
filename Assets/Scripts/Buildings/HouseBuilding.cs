using UnityEngine;

public class HouseBuilding : MonoBehaviour
{
    
    [Header("Settings")]
    [SerializeField] private int woodCost;
    [SerializeField] private float totalBuildTime;
    [SerializeField] private Color startColor;
    [SerializeField] private Color finishedColor;
    
    [Header("Components")]
    [SerializeField] private Transform buildPoint;
    [SerializeField] private SpriteRenderer buildingSprite;
    [SerializeField] private GameObject buildingCollider;

    private bool playerInRange;
    private PlayerAnim playerAnim;
    private Player player;
    private float buildTime;
    private bool isBuilding;

    private void Awake()
    {
        playerAnim = FindObjectOfType<PlayerAnim>();
        player = playerAnim.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PlayerItens playerItens = player.GetComponent<PlayerItens>();
            if (playerItens.TotalWood < woodCost)
            {
                Debug.Log("Not enough wood");
                return;
            }

            playerItens.TotalWood -= woodCost;
            isBuilding = true;
            player.transform.position = buildPoint.position;
            buildingSprite.color = startColor;
            playerAnim.OnHammeringStarted();
        }

        if (isBuilding)
        {
            buildTime += Time.deltaTime;

            if (buildTime >= totalBuildTime)
            {
                buildingSprite.color = finishedColor;
                buildingCollider.SetActive(true);
                playerAnim.OnHammeringEnded();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}