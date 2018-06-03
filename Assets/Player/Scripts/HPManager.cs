using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 100;
    [Tooltip("Ile procent HP regeneruje sie co sekunde")]
    [SerializeField]
    [Range(0f, 100f)]
    private float regenerationSpeed = 1f;
    [SerializeField]
    private float regenerationDelay = 5f;

    [Tooltip("Dodaj jeśli potrzebujesz wyświetlać liczbę HP")]
    [SerializeField]
    private TextMesh text;
    [SerializeField]
    private SimpleHealthBar healthBar;

    private float currentHP;
    private float currentDelay;

    void Start ()
    {
        this.currentHP = this.maxHP;
        this.currentDelay = this.regenerationDelay;
    }

    void Update()
    {
        if (this.currentDelay > 0.0f)
        {
            this.currentDelay -= Time.deltaTime;
        }
        if (this.currentDelay <= 0.0f && this.currentHP < this.maxHP)
        {
            Regenerate();
            if (this.healthBar != null)
            {
                this.healthBar.UpdateBar(this.currentHP, this.maxHP);
            }
        }
        if(Input.GetKey(KeyCode.P))
        {
            ReduceHP(5);
        }
    }

    private void OnDestroy()
    {
        Debug.Log(gameObject.name + "Destroyed");
    }

    public void ReduceHP(int healthPoints)
    {
        this.currentHP -= healthPoints;
        AfterReduce();
    }   

    public void ReduceHP(float healthPercentage)
    {
        this.currentHP -= maxHP * healthPercentage;
        AfterReduce();
    }

    private void AfterReduce()
    {
        if (this.currentHP <= 0)
        {
            Destroy(gameObject);
        }
        else 
        {
            if (this.currentHP < this.maxHP)
            {
                this.currentDelay = this.regenerationDelay;
            }
            if (text != null)
            {
                text.text = "" + this.currentHP;
            }
            if(this.healthBar != null)
            {
                this.healthBar.UpdateBar(this.currentHP, this.maxHP);
            }                
        }
    }

    private void Regenerate()
    {
        this.currentHP += this.maxHP * (this.regenerationSpeed / 100f) * Time.deltaTime;
        Mathf.Clamp(this.currentHP, 0, this.maxHP);
    }
}
