using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUtils : MonoBehaviour {
    public GameManager gameManager;
    public GameState gameState;

    private static EffectUtils _instance;
    public static EffectUtils Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        gameManager = gameController.GetComponent<GameManager>();
        gameState = gameController.GetComponent<GameState>();
    }

    public void DamageEffect(Vector3 position, float damage, ActionType damageType)
    {
        GameObject damageObj = Object.Instantiate(gameManager.prefabHolder.damage, position, Quaternion.identity);
        damageObj.transform.SetParent(gameManager.canvas.transform, false);
        Vector3 wts = UtilHelper.Instance.worldToUISpace(gameManager.canvas, position);
        damageObj.transform.position = wts;

        DamageUpEffect damageUpEffect = damageObj.GetComponent<DamageUpEffect>();

        damageUpEffect.SetDamage(damage);
        damageUpEffect.StartEffect();

    }

    public void HealEffect(Vector3 position, float heal)
    {
        GameObject damageObj = Object.Instantiate(gameManager.prefabHolder.heal, position, Quaternion.identity);
        damageObj.transform.SetParent(gameManager.canvas.transform, false);
        Vector3 wts = UtilHelper.Instance.worldToUISpace(gameManager.canvas, position);
        damageObj.transform.position = wts;

        DamageUpEffect damageUpEffect = damageObj.GetComponent<DamageUpEffect>();

        damageUpEffect.SetHeal(heal);
        damageUpEffect.StartEffect();

        HealEffect(position);
    }

    private void HealEffect(Vector3 position)
    {
        GameObject healEffectObj = PrafabUtils.Instance.create("healEffect");
        healEffectObj.transform.position = position;

        PlayParticle(healEffectObj);
    }



    public void BloodDrippingEffect(Vector3 position, GameObject parentObj)
    {
        GameObject bloodDrippingObj = PrafabUtils.Instance.create(gameManager.prefabHolder.bloodDripping, position, parentObj);
        bloodDrippingObj.GetComponent<BloodDrippingEffect>().PlayParticle();
    }

    public void CastAutoPlayEffect(Vector3 position, string path) {
        GameObject effectObj = PrafabUtils.Instance.create(path, position, null);
    }

    public void PlayParticle(GameObject effect) {
        ParticleSystem particleInChild, particle;
        particleInChild = gameObject.GetComponentInChildren<ParticleSystem>();
        particle = gameObject.GetComponent<ParticleSystem>();

        if (particleInChild != null)
        {
            PlayParticle(particleInChild);
        }

        if (particle != null) {
            PlayParticle(particle);
        }

    }

    public bool isParticleSystem(GameObject effect) {
        ParticleSystem particleInChild, particle;
        particleInChild = gameObject.GetComponentInChildren<ParticleSystem>();
        particle = gameObject.GetComponent<ParticleSystem>();

        return (particleInChild != null || particle != null);
    }

    private void PlayParticle(ParticleSystem particleSystem)
    {
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }
}
