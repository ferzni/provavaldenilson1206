using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Commands : MonoBehaviour
{

    private Rigidbody2D robo;  //ATRIBUINDO CORPO RIGIDO AO PERSONAGEM;
    private Animator anim;
    public float forcaPulo;  //VARIAVEL FLOAT PARA PULO;
    public float velocidade; //Variável para velocidade
    private float inputMovimento;
    public float ForcaPulo;

    public SpriteRenderer spriteRb;
    public bool sensor;
    public Transform posicaoSensor;
    public LayerMask LayerChao;
    public GameObject projetil;
    public Transform localDisparo;

    public bool verificarDirecao; //variavel utilizada para
    public float velocidadeTiro; //atribui velocidade ao tiro

    public TextMeshProUGUI textocoin, textovida;
    private int quantidadeMoedas;

    private int quantidadeVidas;

    // Start is called before the first frame update
    void Start()
    {
        robo = GetComponent<Rigidbody2D>();  //DEFININDO COMPONENTE PARA PERSONAGEM;
        anim = GetComponent<Animator>();
        spriteRb = GetComponent<SpriteRenderer>();
        quantidadeVidas = 3;
    }

    // Update is called once per frame
    void Update()
    {
        inputMovimento = Input.GetAxisRaw("Horizontal");  //adição de movimento componente horizontal;

        robo.velocity = new Vector2(inputMovimento * velocidade, robo.velocity.y); //VECTOR DE VELOCIDADE VARIAVEL;

        if (Input.GetButtonDown("Jump") && sensor == true)  //MOVIMENTO DE PULo
        {
            robo.AddForce(new Vector2(0, forcaPulo), ForceMode2D.Impulse);  //MOVIMENTO DO SALTO/FORÇA0
                                                                               //0;
        }
        anim.SetInteger("walk", (int)inputMovimento); //Atribui valor da var. ao parametro do animator
        anim.SetBool("jump", sensor);
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("shoot");
        }

        if (inputMovimento > 0 && verificarDirecao == true)
        {
            Flip();
        }
        else if (inputMovimento < 0 && verificarDirecao == false)
        {

            Flip();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject temp = Instantiate(projetil);

            temp.transform.position = localDisparo.transform.position;

            temp.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidadeTiro, 0);
            Destroy(temp.gameObject, 2);
        }

    }

    private void FixedUpdate()
    {
        sensor = Physics2D.OverlapCircle(posicaoSensor.position, 0.5f, LayerChao);
    }

    public void Flip()
    {
        verificarDirecao = !verificarDirecao;

        float x = transform.localScale.x * -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        velocidadeTiro *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            quantidadeMoedas += 1;

            textocoin.text = quantidadeMoedas.ToString();
            Destroy(collision.gameObject);

        }
        if (collision.gameObject.tag == "coin1")
        {
            quantidadeMoedas += 5;

            textocoin.text = quantidadeMoedas.ToString();
            Destroy(collision.gameObject);

        }

        if (collision.gameObject.tag == "espinho")
        {
            quantidadeVidas -= 1;

            textovida.text = quantidadeVidas.ToString();
        }

        if (quantidadeVidas <= 0)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "potion")
        {
            quantidadeVidas += 1;

            textovida.text = quantidadeVidas.ToString();

            Destroy(collision.gameObject);
        }
    }
}