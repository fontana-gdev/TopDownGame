using UnityEngine;
using UnityEngine.Serialization;

public class PlayerItens : MonoBehaviour
{
    [Header("Amounts")] 
    [SerializeField] private int totalWood;
    [SerializeField] private int totalCarrots;
    [SerializeField] private float currentWater;
    [SerializeField] private float totalFishes;

    [Header("Limits")] 
    [SerializeField] private float waterLimit;
    [SerializeField] private float woodLimit;
    [SerializeField] private float carrotsLimit;
    [SerializeField] private float fishesLimit;

    public void AddWater(float water)
    {
        if (currentWater >= waterLimit) return;

        currentWater += water;
    }

    public void SpendWater(float water)
    {
        if (currentWater == 0) return;
        if (currentWater - water <= 0)
        {
            currentWater = 0;
            return;
        }

        currentWater -= water;
    }

    public void AddWood(int woodAmount)
    {
        totalWood += woodAmount;
    }

    public void AddCarrot(int carrotAmount)
    {
        totalCarrots += carrotAmount;
    }

    public void AddFish(int fishAmount)
    {
        totalFishes += fishAmount;
    }
    
    public int TotalWood
    {
        get => totalWood;
        set => totalWood = value;
    }

    public int TotalCarrots
    {
        get => totalCarrots;
        set => totalCarrots = value;
    }

    public float CurrentWater
    {
        get => currentWater;
        set => currentWater = value;
    }

    public float FishesLimit
    {
        get => fishesLimit;
        set => fishesLimit = value;
    }

    public float TotalFishes
    {
        get => totalFishes;
        set => totalFishes = value;
    }

    public float WaterLimit
    {
        get => waterLimit;
        set => waterLimit = value;
    }

    public float WoodLimit
    {
        get => woodLimit;
        set => woodLimit = value;
    }

    public float CarrotsLimit
    {
        get => carrotsLimit;
        set => carrotsLimit = value;
    }
}