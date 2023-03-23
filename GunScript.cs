using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    //Parametros da arma
    [Header("Componentes")]
    public Camera cam;
    AudioSource au;

    [Header("Sistema de Tiro")]
    public float rateFire;
    float timeByShoots;
    public int maxAmmu;
    public int ammu;
    public float dammage;

    [Header("GameEffects")]
    public GameObject[] vfxGun;
    public AudioClip[] sounds;

    [Header("UI")]
    public TextMesh ammuText;
    public TextMesh feedbackHit;

    private void Start()
    {
        au = GetComponent<AudioSource>();
    }
    void Update()
    {
        Shoot();
    }

    //FUnções da arma
    void Shoot()
    {
        //adicionando os valores da munição na interace do jogo
        ammuText.text = ammu.ToString() + "/" + maxAmmu.ToString();

        //Definindo uma colisão de acordo com um raycast criado a partir da camera para o ponto central da tela
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity);

        //Definindo o fireRate da arma e o tiro
        if (Input.GetButton("Fire1") && Time.time > timeByShoots)
        {
            //Verificando se a arma esta com munição
            if (ammu > 0)
            {
                //aplicando o firerate de acordo com o valor que definimos
                timeByShoots = Time.time + rateFire;
                //decrescendo a munição conforme a arma atira
                ammu--;

                   //Instanciando os efeitos visuais e sonoros da arma
                   GameObject Instancia = Instantiate(vfxGun[0], GameObject.Find("Ponta").transform.position, GameObject.Find("Ponta").transform.rotation);
                   Instancia.transform.parent = GameObject.Find("Ponta").transform;
                   au.PlayOneShot(sounds[0]);

                //aplicando uma ação para caso o tiro atinja algum objeto
                if (hit.collider != null)
                {
                    Instantiate(vfxGun[1], hit.point, transform.rotation);
                    if(hit.collider.gameObject.tag == "hittable")
                    {
                        feedbackHit.text = dammage.ToString();
                        Instantiate(feedbackHit, hit.point, transform.rotation);
                    }
                }
            }
        }
    }
}
