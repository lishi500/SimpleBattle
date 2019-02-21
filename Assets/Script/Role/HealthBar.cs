using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    private Transform bar;
    private Transform shield;

    public float currentHealth;
    public float targetHealth;
    public float totalHealth;
    public float currentShield;
    public float targetShield;
    private bool modifing = false;

    void Awake()
    {
        bar = transform.Find("Bar");
        shield = transform.Find("Shield");

    }

    public void InitialHealth(float targetHealth, float totalHealth, float shield)
    {
        this.currentShield = shield - 1;
        this.targetShield = shield;
        InitialHealth(targetHealth, totalHealth);
    }

    public void InitialHealth(float targetHealth, float totalHealth) {
        this.targetHealth = targetHealth;
        this.currentHealth = targetHealth - 1;
        this.totalHealth = totalHealth;
    }

    public void SetHealth(float targetHealth, float totalHealth, float targetShield) {
        this.targetHealth = targetHealth;
        this.totalHealth = totalHealth;
        this.targetShield = targetShield;
    }
    private void SetSize(float healthNormalized, float shieldNormalized) {
        bar.localScale = new Vector3(healthNormalized, 1f);
        shield.localScale = new Vector3(shieldNormalized, 1f);
    }


    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void LateUpdate() {
        if ((currentHealth != targetHealth || currentShield != targetShield) && !modifing) {
            StartCoroutine(ModifyHealth());
        }

    }

    private float calcCurrentValue(float diff, float currentValue) {
        if (diff != 0 && Mathf.Abs(diff) > 1) {
            float constAdd = diff / Mathf.Abs(diff);
            return currentValue + diff * 0.25f + constAdd;
        }

        return currentValue;
    }

    private IEnumerator ModifyHealth() {
        float healthDiff = targetHealth - currentHealth;
        float shieldDiff = targetShield - currentShield;
        float currentTotalWithShield = currentHealth + currentShield;
        if (healthDiff != 0 || shieldDiff != 0)
        {
            currentHealth = calcCurrentValue(healthDiff, currentHealth);
            currentShield = calcCurrentValue(shieldDiff, currentShield);
            if (Mathf.Abs(healthDiff) < 1)
            {
                currentHealth = targetHealth;
            }
            if (Mathf.Abs(shieldDiff) < 1) {
                currentShield = targetShield;
            }
            modifing = true;
            if (currentTotalWithShield >= totalHealth)
            {
                modifyHealthCoroutine = ModifyHealthCoroutine(currentHealth / currentTotalWithShield, 1f, 0.03f);
                yield return StartCoroutine(modifyHealthCoroutine);
            }
            else
            {
                modifyHealthCoroutine = ModifyHealthCoroutine(currentHealth / totalHealth, currentHealth / totalHealth + currentShield / totalHealth, 0.03f);
                yield return StartCoroutine(modifyHealthCoroutine);
            }

        }
        
        //if (diff != 0) {
        //    float constAdd = diff / Mathf.Abs(diff);
        //    currentHealth = currentHealth + diff * 0.25f + constAdd;
        //    if (Mathf.Abs(diff) <= 1) {
        //        currentHealth = targetHealth;
        //    }
        //    modifing = true;
        //    modifyHealthCoroutine = ModifyHealthCoroutine(currentHealth / totalHealth, 0.03f);
        //    yield return StartCoroutine(modifyHealthCoroutine);
        //}
        yield return null;
    }
    private IEnumerator modifyHealthCoroutine;
    private IEnumerator ModifyHealthCoroutine(float healthNormalized, float shieldNormalized, float time) {
        SetSize(healthNormalized, shieldNormalized);
        yield return new WaitForSeconds(time);
        modifing = false;
    }
}
