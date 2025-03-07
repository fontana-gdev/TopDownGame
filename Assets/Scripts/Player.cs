using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool actionsBlocked;

    [SerializeField] float speed;
    [SerializeField] float runSpeed;

    private Rigidbody2D rig;
    private PlayerItens playerItens;
    
    private float initialSpeed;
    private bool isRunning;
    private bool isRolling;
    private bool isCutting;
    private bool isDigging;
    private bool isWatering;
    private bool isStrikingWithSword;

    private Vector2 _direction;
    
    public bool IsRunning => isRunning;
    public bool IsRolling => isRolling;
    public bool IsCutting => isCutting;
    public bool IsDigging => isDigging;
    public bool IsWatering => isWatering;
    public bool IsStrikingWithSword => isStrikingWithSword;

    private Tool activeTool;

    public Tool ActiveTool => activeTool;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
        activeTool = Tool.Axe;
        playerItens = GetComponent<PlayerItens>();
    }

    private void Update()
    {
        if (actionsBlocked) return;
        
        OnInputDirection();
        OnInputChangeTool();
        OnRun();
        OnRoll();
        OnCutting();
        OnDigging();
        OnWatering();
        OnStrikeWithSword();

        // Just for scene testing
        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("Graveyard");
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void FixedUpdate()
    {
        if (actionsBlocked) return;
        
        OnMove();
    }
    
    public Vector2 direction
    {
        get => _direction;
        set => _direction = value;
    }

    #region Movement
    void OnInputDirection()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    
    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * (speed * Time.fixedDeltaTime));
    }

    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            isRunning = false;
        }
    }

    void OnRoll()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRolling = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRolling = false;
        }
    }
    #endregion

    #region ToolsUsage
    private void OnInputChangeTool()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeTool = Tool.Axe;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeTool = Tool.Shovel;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeTool = Tool.Bucket;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            activeTool = Tool.Sword;
        }
    }
    
    void OnCutting()
    {
        if (activeTool != Tool.Axe)
        {
            isCutting = false;
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            isCutting = true;
            speed = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isCutting = false;
            speed = initialSpeed;
        }
    }
    
    void OnDigging()
    {
        if (activeTool != Tool.Shovel)
        {
            isDigging = false;
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            isDigging = true;
            speed = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDigging = false;
            speed = initialSpeed;
        }
    }
    
    void OnWatering()
    {
        if (activeTool != Tool.Bucket)
        {
            isWatering = false;
            return;
        }
        
        if (playerItens.CurrentWater <= 0 && isWatering)
        {
            isWatering = false;
            speed = initialSpeed;
            return;
        }
        
        if (Input.GetMouseButtonDown(0) && playerItens.CurrentWater > 0)
        {
            isWatering = true;
            speed = 0;
        }

        if (Input.GetMouseButtonUp(0) && isWatering)
        {
            isWatering = false;
            speed = initialSpeed;
        }

        if (isWatering)
        {
            playerItens.SpendWater(0.01f);
        }
    }
    #endregion

    #region Combat
    void OnStrikeWithSword()
    {
        if (activeTool != Tool.Sword)
        {
            isStrikingWithSword = false;
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            isStrikingWithSword = true;
            speed = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isStrikingWithSword = false;
            speed = initialSpeed;
        }
    }

    #endregion
    
}