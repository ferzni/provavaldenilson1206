using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComandosInicio : MonoBehaviour
{

    public void startButton()
    {
        SceneManager.LoadScene("GameProva");
    }

    public void instrucoesButton()
    {
        SceneManager.LoadScene("configura��es");
    }
    public void voltar()
    {
        SceneManager.LoadScene("Inicio");
    }
}
