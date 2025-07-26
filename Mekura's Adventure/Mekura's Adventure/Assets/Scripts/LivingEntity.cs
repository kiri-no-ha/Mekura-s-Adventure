using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : Entity
{
    // Основные атрибуты
    public float strength = 10;
    public float agility = 10;
    public float intelligence = 10;
    // Здоровье
    public float currentHealth;
    public float maxHealthBase = 100;
    public float maxHealth { get { return maxHealthBase + strength * healthPerStrength; } }
    private float healthPerStrength = 20; // каждая единица силы дает 20 здоровья

    // Мана
    public float currentMana;
    public float maxManaBase = 50;
    public float maxMana { get { return maxManaBase + intelligence * manaPerIntelligence; } }
    private float manaPerIntelligence = 10; // каждая единица интеллекта дает 10 маны

    // Броня

    public float armorBase = 0;
    public float armor { get { return armorBase + agility * armorPerAgility; } }
    private float armorPerAgility = 0.1f; // каждая единица ловкости дает 0.1 брони
    // Скорость передвижения
    public float moveSpeed = 5;
    // Урон
    public float attackDamageMin = 10;
    public float attackDamageMax = 15;
    // Скорость атаки (атак в секунду)

    public float attackSpeed = 1.0f;


    // Регенерация здоровья и маны (в единицах в секунду)

    public float healthRegenBase = 0.1f;

    public float healthRegen { get { return healthRegenBase + strength * healthRegenPerStrength; } }

    private float healthRegenPerStrength = 0.05f;

    public float evasionChance;  
    public float criticalChance; 
    public float lifeSteal;      

    public float manaRegenBase = 0.05f;

    public float manaRegen { get { return manaRegenBase + intelligence * manaRegenPerIntelligence; } }

    private float manaRegenPerIntelligence = 0.01f;

    // Метод для применения урона

    public virtual void TakeDamage(float damage)
    {
        // Учитываем броню при расчете физического урона (упрощенно)

        float damageMultiplier = 1 - (armor * 0.06f) / (1 + 0.06f * Mathf.Abs(armor));
        float actualDamage = damage * damageMultiplier;

        currentHealth -= actualDamage;
        if (currentHealth <= 0)
        {
            //Die();
        }
    }
   
    protected virtual void Attack(LivingEntity target)
    {
        float damage = UnityEngine.Random.Range(attackDamageMin, attackDamageMax);
        target.TakeDamage(damage);
    }
}

