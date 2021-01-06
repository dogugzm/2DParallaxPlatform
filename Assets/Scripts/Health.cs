using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Health : MonoBehaviour
{
    public enum AttackableObjectType
    {
        PLAYER,TREE

    }

    public AttackableObjectType attackableObject;
    public int health;

    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public virtual void TakeDamage(int damage)
    {
        
        health -= damage;
        if (this.gameObject == null)
        {
            return;                                //YENİ EKLENDİ NORMALDE YOK
        }
        if (attackableObject == AttackableObjectType.TREE)
        {
            HitFeedback();
        }
        CheckIfDead();
    }

    protected virtual void CheckIfDead()
    {
        if (health <= 0)
        {
            health = 0;
            Debug.Log("You Are Died" + gameObject.name);
            DeathFeedback();
        }
    }

    private void HitFeedback()
    {
        this.gameObject.transform.DOShakePosition(0.1f, new Vector3(0.4f, 0.1f, 0), 10, 90);
        Tween colorTween = spriteRenderer.DOBlendableColor(Color.red, 0.2f);
        colorTween.OnComplete(() => spriteRenderer.DOBlendableColor(Color.white, 0.1f));
    }
    private void DeathFeedback()
    {
        this.gameObject.transform.DOShakePosition(0.15f, new Vector3(0.4f, 0.1f, 0), 10, 90);
        Tween colorTween = spriteRenderer.DOBlendableColor(Color.red, 0.2f);
        colorTween.OnComplete(() => this.gameObject.SetActive(false));              //DESTROY YERİNE SETACTİVE FALSE OLDU
    }
}
