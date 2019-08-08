﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ThirPersonCamera : MonoBehaviour
{
    //Variables para restringir camara
    public float ANGLE_MIN ;
    public float ANGLE_MAX ;

    public Transform lookat; //localizacion del jugador
    public Transform camTransform; //transform de esta camara

    public float distancia; // Que tanto se aleja
    float currentX ;
    float currentY ;
   
    // Start is called before the first frame update
    void Start()
    {
        camTransform = transform;
        
        lookat = GameObject.Find("cabeza_holder").GetComponent<Transform>(); // mira ala cabeza del jugador
    }


    private void Update()
    {
        currentX +=CrossPlatformInputManager.GetAxis("Mouse Y");
        currentY += CrossPlatformInputManager.GetAxis("Mouse X");
        //print(currentX);
        //print(currentY);
        currentX=Mathf.Clamp(currentX, ANGLE_MIN, ANGLE_MAX); // limita el giro de la camara
    }
    private void LateUpdate()
    {
        //Offset de la camra con respecto al jugador
        Vector3 dir = new Vector3(0, 0, -distancia); // que tanto se aleja del jugador
        Quaternion rotation = Quaternion.Euler(currentX, currentY, 0); //Rotacion depende del Mouse
        camTransform.position = lookat.position + rotation * dir;
        camTransform.LookAt(lookat.position);    // Asegura que la rotacion sea  alrededor de ljugador
    }
    
}