using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [ReadOnly][SerializeField] int currentHealth = 0;
    [SerializeField] Canvas healthBarCanvas;
    [SerializeField] Image healthBarImage;

    WayPoint baseWayPoint;
    bool damageable = false;
    
    public void Init(WayPoint wayPoint)
    {
        baseWayPoint = wayPoint;
    }
    
    private void Awake()
    {
        currentHealth = maxHealth;
        healthBarCanvas.worldCamera = Camera.main;
        healthBarCanvas.gameObject.SetActive(false);

        
        SetRagRoll(false);
    }

    void SetRagRoll(bool val)
    {
        
        foreach (var col in GetComponentsInChildren<Collider>())
        {
            if (col.gameObject == gameObject) continue;
            col.enabled = val;
        }

        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.useGravity = val;
            rb.freezeRotation = !val;
        }
    }

    public void UpdateHpView()
    {
        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    }
    
    public void ShowCanvas()
    {
        damageable = true;
        healthBarCanvas.gameObject.SetActive(true);
        
        // rotate to camera
        if(healthBarCanvas.worldCamera != null)
            healthBarCanvas.transform.rotation = healthBarCanvas.worldCamera.transform.rotation;
    }
    
    public void GetDamage(int damage)
    {
        if (!damageable) return;
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHpView();
        
        if (currentHealth == 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Invoke(nameof(Deactivate), 2f);
        baseWayPoint.EnemyDied(this);
        damageable = false;
        GetComponent<Animator>().enabled = false;
        
        SetRagRoll(true);
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
