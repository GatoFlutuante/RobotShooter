using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Parametros do personagem
    [Header("Componetes")]
    CharacterController controller;
    Animator an;
    AudioSource au;
    public GunScript gun;

    [Header("Movimento")]
    public float speed;
    public float rotSpeed;
    float rotY;
    float gravity = 150f;

    Vector3 moveDirection;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        an = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
    }

    private void Update()
    {
        movement();
    }

    //Funções do personagem
    void movement()
    {
        //Movimentação e gravidade
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.y -= gravity * Time.deltaTime;

        //Rotação atraves do movimento do mouse
        rotY += Input.GetAxis("Mouse X");
        Quaternion rotate = Quaternion.Euler(0, rotY, 0 * rotSpeed * Time.deltaTime);

        //Aplicação dos valores a cima
        controller.Move(moveDirection * speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, 15f * Time.deltaTime);

        //Aplicando as animações no personagem
        an.SetFloat("pMoveY", Input.GetAxis("Vertical")); an.SetFloat("pMoveX", Input.GetAxis("Horizontal"));
        if (gun.ammu > 0)
        {
            an.SetBool("Atirando", Input.GetButton("Fire1"));
        }
        else if (Input.GetButton("Fire1"))
        {
            an.SetBool("Atirando", false);
            an.SetTrigger("Reload");
        }
        if (Input.GetButtonDown("Fire2") && gun.ammu < gun.maxAmmu)
        {
            an.SetTrigger("Reload");
        }
    }
    public void Reload()
    {
        gun.ammu = gun.maxAmmu;
    }
}
