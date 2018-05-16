using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    [SerializeField]
    private float maxMP = 100;
    [Tooltip("Ile procent MP regeneruje sie co sekunde")]
    [SerializeField]
    [Range(0f, 100f)]
    private float regenerationSpeed = 1f;
    [SerializeField]
    private float regenerationDelay = 5f;

    [Tooltip("Dodaj jeśli potrzebujesz wyświetlać liczbę MP")]
    [SerializeField]
    private TextMesh text;
    [SerializeField]
    private SimpleHealthBar manaBar;

    private float currentMP;
    private float currentDelay;

    void Start()
    {
        this.currentMP = this.maxMP;
        this.regenerationDelay = this.currentDelay;
    }

    void Update()
    {
        if (this.currentDelay > 0.0f)
        {
            this.currentDelay -= Time.deltaTime;
        }
        if (this.currentDelay <= 0.0f && this.currentMP < this.maxMP)
        {
            Regenerate();
            if (this.manaBar != null)
            {
                this.manaBar.UpdateBar(this.currentMP, this.maxMP);
            }
        }
    }

    public void ReduceMP(float manaPoints)
    {
        this.currentMP -= manaPoints;
        AfterReduce();
    }

    public void ReducePercentageMP(float manaPercentage)
    {
        this.currentMP -= this.maxMP * manaPercentage;
        AfterReduce();
    }

    private void AfterReduce()
    {
        if (this.currentMP <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (this.currentMP < this.maxMP)
            {  
                this.currentDelay = this.regenerationDelay;
            }
            if (text != null)
            {
                text.text = "" + this.currentMP;
            }
            if (this.manaBar != null)
            {
                this.manaBar.UpdateBar(this.currentMP, this.maxMP);
            }
        }
    }

    private void Regenerate()
    {
        this.currentMP += this.maxMP * (this.regenerationSpeed / 100f) * Time.deltaTime;
        Mathf.Clamp(this.currentMP, 0, this.maxMP);
    }
}
