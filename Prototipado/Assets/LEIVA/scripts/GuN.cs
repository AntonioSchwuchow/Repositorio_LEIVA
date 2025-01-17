﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuN : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    
    public Camera FPSCAM;
    public ParticleSystem muzzleflash;
    public GameObject impactEffect;
    public float impactforce=100f;
    public float ShootInterval ;
    public AudioSource disparoaudio;
    bool canfire = true;
   // private float nextTimeTofire = 0f;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && canfire)
        {
            canfire = false;
            //nextTimeTofire = Time.time + 100f / firerate; // El fire rate es una decima de segundo 1/10
            Shoot();
            StartCoroutine("Firerate");
        }
    }
    IEnumerator Firerate() {
        yield return new WaitForSeconds(ShootInterval);
        canfire = true;
    }
    void Shoot()
    {
        muzzleflash.Play();
        RaycastHit hit;
        disparoaudio.Play();
        if (Physics.Raycast(FPSCAM.transform.position,FPSCAM.transform.forward,out hit,range)) {
            Debug.Log(hit.transform.name);
            Damageable target = hit.transform.GetComponent<Damageable>();
            if (target != null) {
                target.takeDamage(damage);
            }
            if (hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal*impactforce);
            }
        }
        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //ParticleSystem nnuevon = creado.GetComponent<ParticleSystem>();
        //nnuevon.Play();
        Destroy(impactGO,2);
    }

}
