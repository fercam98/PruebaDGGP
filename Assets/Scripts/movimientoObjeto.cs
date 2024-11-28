using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class movimientoObjeto : MonoBehaviour
{
    /* 
     Este Script moverá un gameobject hacia una direccion a una velocidad constante y a una distancia máxima, luego volverá hasta su posición inicial.
     */

    #region Parametros
    public float velocidad = 1f;
    public float distancia = 1f;
    public EnumDireccion direccion = EnumDireccion.Adelante;

    private Rigidbody rg;
    private Vector3 posicionInicial = new Vector3(0, 0, 0);
    private Vector3 distanciaMaxima = new Vector3(0, 0, 0);
    private Vector3 vectorIda = Vector3.forward;
    private Vector3 vectorVuelta = Vector3.back;
    private Boolean blnIr = true;
    private Boolean blnDireccionPositiva = true;
    private EnumEje enmEje = EnumEje.X;
    #endregion

    public enum EnumDireccion
    {
        Adelante,
        Atras,
        Izquierda,
        Derecha,
        Arriba,
        Abajo
    }
    private enum EnumEje
    {
        X,
        Y,
        Z
    }

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        Inicializar();
    }

    private void FixedUpdate()
    {
        ComprobarIrVolver();
        RealizarMovimiento();
    }

    private void Inicializar()
    {
        try
        {
            posicionInicial = transform.position;

            switch (direccion)
            {
                case EnumDireccion.Adelante:
                    distanciaMaxima = new Vector3(0, 0, posicionInicial.z + distancia);
                    vectorIda = Vector3.forward;
                    vectorVuelta = Vector3.back;
                    blnDireccionPositiva = true;
                    enmEje = EnumEje.Z;
                    break;
                case EnumDireccion.Atras:
                    distanciaMaxima = new Vector3(0, 0, posicionInicial.z - distancia);
                    vectorIda = Vector3.back;
                    vectorVuelta = Vector3.forward;
                    blnDireccionPositiva = false;
                    enmEje = EnumEje.Z;
                    break;
                case EnumDireccion.Izquierda:
                    distanciaMaxima = new Vector3(posicionInicial.x - distancia, 0, 0);
                    vectorIda = Vector3.left;
                    vectorVuelta = Vector3.right;
                    blnDireccionPositiva = false;
                    enmEje = EnumEje.X;
                    break;
                case EnumDireccion.Derecha:
                    distanciaMaxima = new Vector3(posicionInicial.x + distancia, 0, 0);
                    vectorIda = Vector3.right;
                    vectorVuelta = Vector3.left;
                    blnDireccionPositiva = true;
                    enmEje = EnumEje.X;
                    break;
                case EnumDireccion.Arriba:
                    distanciaMaxima = new Vector3(0, posicionInicial.y + distancia, 0);
                    vectorIda = Vector3.up;
                    vectorVuelta = Vector3.down;
                    blnDireccionPositiva = true;
                    enmEje = EnumEje.Y;
                    break;
                case EnumDireccion.Abajo:
                    distanciaMaxima = new Vector3(0, posicionInicial.y - distancia, 0);
                    vectorIda = Vector3.down;
                    vectorVuelta = Vector3.up;
                    blnDireccionPositiva = false;
                    enmEje = EnumEje.Y;
                    break;
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogException(ex, this);
        }
        
    }
    private void ComprobarIrVolver()
    {
        //Compruebo si tengo que ir hacia delante o hacia atrás
        try
        {
            if (enmEje == EnumEje.X)
            {
                if (blnDireccionPositiva)
                {
                    if (blnIr && transform.position.x >= distanciaMaxima.x)
                    {
                        blnIr = false;
                    }

                    if (!blnIr && transform.position.x <= posicionInicial.x)
                    {
                        blnIr = true;
                    }
                }
                else
                {
                    if (blnIr && transform.position.x <= distanciaMaxima.x)
                    {
                        blnIr = false;
                    }

                    if (!blnIr && transform.position.x >= posicionInicial.x)
                    {
                        blnIr = true;
                    }
                }
            }
            else if (enmEje == EnumEje.Y)
            {
                if (blnDireccionPositiva)
                {
                    if (blnIr && transform.position.y >= distanciaMaxima.y)
                    {
                        blnIr = false;
                    }

                    if (!blnIr && transform.position.y <= posicionInicial.y)
                    {
                        blnIr = true;
                    }
                }
                else
                {
                    if (blnIr && transform.position.y <= distanciaMaxima.y)
                    {
                        blnIr = false;
                    }

                    if (!blnIr && transform.position.y >= posicionInicial.y)
                    {
                        blnIr = true;
                    }
                }
            }
            else if (enmEje == EnumEje.Z)
            {
                if (blnDireccionPositiva)
                {
                    if (blnIr && transform.position.z >= distanciaMaxima.z)
                    {
                        blnIr = false;
                    }

                    if (!blnIr && transform.position.z <= posicionInicial.z)
                    {
                        blnIr = true;
                    }
                }
                else
                {
                    if (blnIr && transform.position.z <= distanciaMaxima.z)
                    {
                        blnIr = false;
                    }

                    if (!blnIr && transform.position.z >= posicionInicial.z)
                    {
                        blnIr = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogException(ex, this);
        }
        
    }
    private void RealizarMovimiento()
    {
        //Me muevo hacia delante o atrás
        try
        {
            if (blnIr)
            {
                rg.MovePosition(transform.position + (vectorIda * velocidad * Time.fixedDeltaTime));
                //transform.position += vectorIda * velocidad * Time.deltaTime;
            }
            else
            {
                rg.MovePosition(transform.position + (vectorVuelta * velocidad * Time.fixedDeltaTime));
                //transform.position += vectorVuelta * velocidad * Time.deltaTime;
            }
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogException(ex, this);
        }
    } 
}
