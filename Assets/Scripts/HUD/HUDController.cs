using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [Header("Tools")]
    [SerializeField] private List<Image> toolsUI = new();
    [SerializeField] Color activeToolColor;
    [SerializeField] Color inactiveToolColor;
    
    [Header("Collectables")]
    [SerializeField] Image waterFillBar;
    [SerializeField] Image woodFillBar;
    [SerializeField] Image carrotsFillBar;
    [SerializeField] Image fishesFillBar;

    private PlayerItens playerItens;
    private Player player;

    private void Awake()
    {
        playerItens = FindObjectOfType<PlayerItens>();
        player = playerItens.GetComponent<Player>();
    }

    void Start()
    {
        waterFillBar.fillAmount = 0f;
        woodFillBar.fillAmount = 0f;
        carrotsFillBar.fillAmount = 0f;
        fishesFillBar.fillAmount = 0f;
    }

    void Update()
    {
        waterFillBar.fillAmount = playerItens.CurrentWater / playerItens.WaterLimit;
        woodFillBar.fillAmount = playerItens.TotalWood / playerItens.WoodLimit;
        carrotsFillBar.fillAmount = playerItens.TotalCarrots / playerItens.CarrotsLimit;
        fishesFillBar.fillAmount = playerItens.TotalFishes / playerItens.FishesLimit;
        
        // For better optimization, could be a method called only when a different tool is selected
        for (var i = 0; i < toolsUI.Count; i++)
        {
            toolsUI[i].color = i == (int) player.ActiveTool ? activeToolColor : inactiveToolColor;
        }
    }
}