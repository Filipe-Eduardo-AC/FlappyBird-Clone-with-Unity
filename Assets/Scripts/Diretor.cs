using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diretor : MonoBehaviour
{
    private Aviao aviao;
    private Pontuacao pontuacao;
    private InterfaceGameOver interfaceGameOver;

    private void Start()
    {
        this.aviao = Object.FindFirstObjectByType(typeof(Aviao)) as Aviao;
        this.pontuacao = Object.FindFirstObjectByType(typeof(Pontuacao)) as Pontuacao;
        this.interfaceGameOver = Object.FindFirstObjectByType(typeof(InterfaceGameOver)) as InterfaceGameOver;
    }

    public void FinalizarJogo()
    {
        Time.timeScale = 0;
        this.pontuacao.SalvarRecorde();
        this.interfaceGameOver.MostrarInterface();
    }

    public void ReiniciarJogo()
    {
        this.interfaceGameOver.EsconderInterface();
        Time.timeScale = 1;
        this.aviao.Reiniciar();
        this.DestruirObstaculos();
        this.pontuacao.Reiniciar();
    }

    private void DestruirObstaculos()
    {
        Obstaculo[] obstaculos = (Obstaculo[])Object.FindObjectsByType(typeof(Obstaculo), FindObjectsSortMode.None);
        foreach (Obstaculo obstaculo in obstaculos)
        {
            obstaculo.Destruir();
        }
    }
}
