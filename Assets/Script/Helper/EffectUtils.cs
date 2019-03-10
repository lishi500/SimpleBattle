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

    public void setEffectPosition(GameObject effectObj, Role from, Role to, EffectPositionType positionType) {
        switch (positionType) {
            case EffectPositionType.CENTOR:
                break;
            case EffectPositionType.SOURCE_POSITION:
                effectObj.transform.position = from.transform.position;
                break;
            case EffectPositionType.TARGET_POSITION:
                effectObj.transform.position = to.transform.position;
                break;
            case EffectPositionType.OTHER:
                break;
            default:
                break;
        }
    }

    public void BloodDrippingEffect(Vector3 position, GameObject parentObj)
    {
        GameObject bloodDrippingObj = PrafabUtils.Instance.create(gameManager.prefabHolder.bloodDripping, position, parentObj);
        bloodDrippingObj.GetComponent<BloodDrippingEffect>().PlayParticle();
    }


    public void CastAutoPlayEffect(Vector3 position, string path) {
        GameObject effectObj = PrafabUtils.Instance.create(path, position, null);
    }

    public LoopEffect initLoopEffect(string effectPath, float effectTime, float loopInterval) {
        LoopEffect loopEffect = new LoopEffect();
        loopEffect.effectName = effectPath;
        loopEffect.effectTime = effectTime;
        loopEffect.loopInterval = loopInterval;
        loopEffect.waitingTime = loopEffect.loopInterval;

        return loopEffect;
    }

    public LoopEffect initLoopEffect(string effectPath, float effectTime, float loopInterval, float offsetX, float offsetY)
    {
        LoopEffect loopEffect = initLoopEffect(effectPath, effectTime, loopInterval);
        loopEffect.offsetX = offsetX;
        loopEffect.offsetY = offsetY;

        return loopEffect;
    }

    public GameObject createEffectByLoopEffect(LoopEffect loopEffect, Vector3 position) {
        GameObject effectPrafab = PrafabUtils.Instance.create("effect/" + loopEffect.effectName);
        position.x += loopEffect.offsetX;
        position.y += loopEffect.offsetY;
        effectPrafab.transform.position = position;

        effectPrafab.AddComponent<CommonEffect>();
        CommonEffect effect = effectPrafab.GetComponent<CommonEffect>();
        effect.effectTime = loopEffect.effectTime;
        loopEffect.effectObj = effectPrafab;

        return effectPrafab;
    }

    public void loopEffectRePosition(LoopEffect loopEffect, Vector3 position) {
        if (loopEffect.effectObj != null) {
            position.x += loopEffect.offsetX;
            position.y += loopEffect.offsetY;
            loopEffect.effectObj.transform.position = position;
        }
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
