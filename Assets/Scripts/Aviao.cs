using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Aviao : MonoBehaviour
{
    private Rigidbody2D fisica;
    [SerializeField]
    private float forca = 10;
    [SerializeField]
    private UnityEvent aoBater;
    [SerializeField]
    private UnityEvent aoPassarPeloObstaculo;
    private Vector3 posicaoInicial;
    private bool deveImpulsionar;
    private Animator animacao;

    private void Awake()
    {
        this.posicaoInicial = this.transform.position;
        this.fisica = GetComponent<Rigidbody2D>();    
        this.animacao = this.GetComponent<Animator>();
    }

    void Update()
    {
        this.animacao.SetFloat("VelocidadeY", this.fisica.velocity.y);

    }

    private void FixedUpdate()
    {
        if (this.deveImpulsionar)
        {
            this.Impulsionar();
        }
    }

    public void DarImpulso()
    {
        this.deveImpulsionar = true;
    }

    public void Reiniciar()
    {
        this.transform.position = this.posicaoInicial;
        this.fisica.simulated = true;
    }

    private void Impulsionar()
    {
        this.fisica.velocity = Vector2.zero;
        this.fisica.AddForce(Vector2.up * this.forca, ForceMode2D.Impulse);
        this.deveImpulsionar = false;
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        this.fisica.simulated = false;
        this.aoBater.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.aoPassarPeloObstaculo.Invoke();
    }
}
