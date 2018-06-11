using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceManager {

    [Tooltip("Maksymalna wartość zasobu.")]
    [SerializeField]
    private float maxValue = 100;
    [Tooltip("Wartość regeneracji zasobu na sekundę.")]
    [SerializeField]
    private float regenerationValuePerSec = 0f;
    [Tooltip("Co ile sekund aktualizować regenerację. [sec]")]
    [SerializeField]
    private float regenerationInterval = 0.1f;
    [Tooltip("Opóźnienie regeneracji po otrzymaniu obrażeń. [sec]")]
    [SerializeField]
    private float regenerationDelay = 5f;
    [Tooltip("Czy jeśli zasób spadł do 0, czy powinien się dalej regenerować.")]
    [SerializeField]
    private bool canRegainWhenEmpty = true;

    [Tooltip("Dodaj jeśli potrzebujesz wyświetlać liczbę HP")]
    [SerializeField]
    private TextMesh text;
    [Tooltip("Odwołanie do obiektu paska zasobu. Jeśli będzie pusty, wygeneruje pasek nad obiektem.")]
    [SerializeField]
    private SimpleHealthBar resourceBar;

    private float currentValue;
    private float currentInterval;
    private float currentDelay;

    public float GetActualPercentage() { return currentValue / maxValue; }
    public float GetActualValue() { return currentValue; }
    public float GetMaxValue() { return maxValue; }
    public void SetMaxValue(float maxValue) { this.maxValue = maxValue; }
    public void SetRegenerationValuePerSec(float valuePerSec) { this.regenerationValuePerSec = valuePerSec; }
    public bool IsEmpty() { return currentValue <= 0f; }
    
    private ResourceManager()
    {
        Debug.Log("New ResourceManager");
        this.currentValue = this.maxValue;
        this.currentInterval = this.regenerationInterval;
    }

    /// <summary>
    /// Should be called by MonoBehaviour object.
    /// </summary>
    public void Update()
    {
        CheckRegeneration();
  //      Debug.Log("Update ResourceManager");
        //Debug paska HP gracza:
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReduceByPercentage(0.05f);
            Debug.Log("Kliknięto P");
        }
    }
    
    private void CheckRegeneration()
    {
        if (this.currentInterval > 0.0f)
            this.currentInterval -= Time.deltaTime;
        
        if (this.currentDelay > 0.0f)
            this.currentDelay -= Time.deltaTime;

        if (this.currentInterval <= 0.0f &&                 
            this.currentValue < this.maxValue && 
            this.currentDelay <= 0.0f && 
            (!this.IsEmpty() || this.canRegainWhenEmpty))
        {
            Regenerate();
            this.currentInterval = regenerationInterval;

            if (this.resourceBar != null)
                this.resourceBar.UpdateBar(this.currentValue, this.maxValue);
        }
    }

    //private void OnDestroy()
    //{
    //    Debug.Log(gameObject.name + "Destroyed");
    //}

    public void ReduceByValue(float value)
    {
        this.currentValue -= value;
        AfterReduce();
    }

    public void ReduceByPercentage(float percentage)
    {
        this.currentValue -= maxValue * percentage;
        AfterReduce();
    }

    private void AfterReduce()
    {
        this.currentDelay = this.regenerationDelay;
        this.currentInterval = 0f;

        if (text != null)
            text.text = "" + this.currentValue;

        if (this.resourceBar != null)
            this.resourceBar.UpdateBar(this.currentValue, this.maxValue);
    }

    /// <summary>
    /// Regeneruje zasób o wartość regeneracji.
    /// </summary>
    private void Regenerate()
    {
        this.currentValue += this.regenerationValuePerSec * 
            (this.regenerationInterval > 0f ? this.regenerationInterval : Time.deltaTime);
        Mathf.Clamp(this.currentValue, 0, this.maxValue);
    }
}
