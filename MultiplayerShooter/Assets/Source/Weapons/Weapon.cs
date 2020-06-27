using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private int magSize;
    [SerializeField] private int maxBulletCount; 
    [SerializeField] private float reloadSpeed;
    
    [SerializeField] private GameObject bulletType;
    
    private int loadedBullets;
    private int currentBulletCount;

    protected virtual void Fire()
    {
        if(loadedBullets == 0)
        {
            Reload();
            return;
        }
        loadedBullets -= 1;
        GameObject bullet = GameObject.Instantiate(bulletType);
    }
    protected virtual void Reload() {
        if (currentBulletCount <= magSize)
        {
            loadedBullets = currentBulletCount;
            currentBulletCount = 0;
        }
        else
        {
            loadedBullets = magSize;
            currentBulletCount -= magSize;
        }
    }
}
