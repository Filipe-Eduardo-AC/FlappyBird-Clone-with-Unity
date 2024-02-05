using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiretorMultiplayer : Diretor
{
    [SerializeField]
    private int pontosParaReviver;
    private Jogador[] jogadores;
    private bool alguemMorto;
    private int pontosDesdeAMorte;
    private InterfaceCanvasInativo interfaceInativo;

    protected override void Start()
    {
        base.Start();
        this.jogadores = (Jogador[])Object.FindObjectsByType(typeof(Jogador), FindObjectsSortMode.None);
        this.interfaceInativo = Object.FindFirstObjectByType(typeof(InterfaceCanvasInativo)) as InterfaceCanvasInativo;
    }

    public void AvisarQueAlguemMorreu(Camera camera)
    {
        if (this.alguemMorto)
        {
            this.FinalizarJogo();
            this.interfaceInativo.Sumir();
        }
        else
        {
            this.alguemMorto = true;
            this.pontosDesdeAMorte = 0;
            this.interfaceInativo.AtualizarTexto(this.pontosParaReviver);
            this.interfaceInativo.Mostrar(camera);
        }        
    }

    public override void ReiniciarJogo()
    {
        base.ReiniciarJogo();
        this.ReviverJogadores();
    }

    public void ReviverSePrecisar()
    {
        if (this.alguemMorto)
        {
            this.pontosDesdeAMorte++;
            this.interfaceInativo.AtualizarTexto(this.pontosParaReviver - this.pontosDesdeAMorte);
            if (this.pontosDesdeAMorte >= pontosParaReviver)
            {
                this.interfaceInativo.Sumir();
                this.ReviverJogadores();
            }
        }        
    }

    private void ReviverJogadores()
    {
        this.alguemMorto = false;
        foreach (var jogador in this.jogadores)
        {
            jogador.Ativar();
        }
    }
}
