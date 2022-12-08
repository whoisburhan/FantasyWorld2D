using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GS.FanstayWorld2D.Enemy;
using GS.AudioAsset;

[RequireComponent(typeof(Animator), typeof(AudioSourceScript), typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy Attributes")]
    [SerializeField] private EnemySpecies enemySpecies;
    private EnemeyHealth enemeyHealth;
    private Animator animator;


    private void Awake()
    {
        enemeyHealth = new EnemeyHealth(GetComponentInChildren<HealthBarBehaviour>());
        animator = GetComponent<Animator>();
    }
}

public class EnemeyHealth
{
    private int health, maxHealth;

    private HealthBarBehaviour healthUI;

    public EnemeyHealth(HealthBarBehaviour healthUI)
    {
        this.healthUI = healthUI;
    }

    public void SetHealth(int health, int maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;
        healthUI.SetHealth(health, maxHealth);
    }

    public void UpdateHealth(int amount)
    {
        health -= amount;
        healthUI.SetHealth(health, maxHealth);
    }
}

[Serializable]
public class EnemeySensor
{
    [SerializeField] private LayerMask sensorMask;

    [Header("Enemy Chase Zone Detector Attritube")]
    [SerializeField] private Vector2 chaseZone;
    [SerializeField] private Vector2 chaseZoneOffeset;
    [SerializeField] private float chaseZoneAngle;

    [Header("Enemy Attack Zone Detector Attritube")]
    [SerializeField] private Vector2 attakZone;
    [SerializeField] private Vector2 attakZoneOffset;
    [SerializeField] private float attakZoneAngle;

    [Header("Enemey Petrol Attribute")]
    [SerializeField] private Vector3[] patrolPoints;
    [SerializeField] private bool isPetrolRandomPoint;
}

public enum EnemySpecies
{

}