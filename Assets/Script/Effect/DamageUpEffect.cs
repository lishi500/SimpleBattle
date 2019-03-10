using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DamageUpEffect : Effects {

    public float damageNumber;
    private TextMeshProUGUI textMesh;


    void Start () {
        StartCoroutine(AutoDestory());
    }

    public void SetDamage(float damage) {
        GameObject damageNum = this.transform.GetChild(0).gameObject;
        textMesh = damageNum.GetComponent<TextMeshProUGUI>();

        string damageStr = Mathf.Round(damage).ToString();
        textMesh.text = "- " + damageStr;
    }

    public void SetHeal(float heal)
    {
        GameObject damageNum = this.transform.GetChild(0).gameObject;
        textMesh = damageNum.GetComponent<TextMeshProUGUI>();

        string damageStr = Mathf.Round(heal).ToString();
        textMesh.text = "+ " + damageStr;
    }

    public void StartEffect() {
        StartCoroutine(MoveUpEffect());
    }

    IEnumerator MoveUpEffect() {
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = transform.localScale * 1.1f;
        endPos.y += 150f;
        

        for (float t = 0; t < effectTime; t += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t / effectTime);
            transform.localScale = Vector3.Lerp(startScale, endScale, t / effectTime);
            yield return 0;
        }

        transform.position = endPos;
    }

}
