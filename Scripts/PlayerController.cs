using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed;  //  скорость движени игрока
    public float jumpForce;  //  высота прыжка игрока

    private const int healthMax = 5;  //  Максимальное здоровье
    public static float health;  //  Здоровье игрока

    public Image healthBar;  // Хранение разных спрайтов для полоски здоровья
    public Sprite healthFull;
    public Sprite healthFour;
    public Sprite healthThree;
    public Sprite healthTwo;
    public Sprite healthOne;
    public Sprite healthEmpty;


    private Rigidbody2D rb;  //  грани игрока
    private Animator anim;  //  аниматор игрока
    private float moveInput;  //  считывание клавиш
    public static bool facingRight = true;  //  игрок смотрит вправо

    private bool isGrounded, isGroundNear;  //  проверка приземлился ли игрок, првоерка близко ли земля
    public  Transform feetPos;  //  позиция ног игрока
    public float checkRadius, checkRadiusGround;  //  радиус насколько близко игрок к земле
    public LayerMask whatIsGround;  //  что мы считаем за землю

    

    private void Start()  //  только при запуске
    {
        anim = GetComponent<Animator>();  //  знакомим скрипт с аниматором
        rb = GetComponent<Rigidbody2D>();  //  знакомим скрипт с объектом грани игрока
        health = healthMax;
    }

    private void FixedUpdate()  //  каждый кадр
    {
        moveInput = Input.GetAxis("Horizontal");  //  принимаем значения по горизонтальной оси
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);  //  передвижение игрока

        HealUpdate();  //  Проверка количества здоровья

        if (health == 0) {  //  Смерть персонажа
            facingRight = true;  //  Дебаг

            rb.velocity = new Vector2 (0,0);  //  Фиксируем положение
            if (DeathScreen.isDeathScreen == true && Input.GetKeyDown(KeyCode.Space))
                DeathScreen.Restart();  //  Нажать пробел для рестарта
        }
            

        if (facingRight == false && moveInput > 0) {  //  зажата клавиша вправо, а игрок смотрит влево
            Flip();
        } else if (facingRight == true && moveInput < 0) {  //  зажата клавиша влево, а игрок смотрит вправо
            Flip();
        }

        if (moveInput == 0) {  //  если игрок не двигается, переменная в аниматоре isRunning меняется и наоборот
            anim.SetBool("isRunning", false);
        } else {
            anim.SetBool("isRunning", true);
        }
    }


    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);  //  физический круг с центром в feetPos, радиусом checkRadius и проверкой на касание с whatIsGround
        isGroundNear = Physics2D.OverlapCircle(feetPos.position, checkRadiusGround, whatIsGround);  //  физический круг с центром в feetPos, радиусом checkRadiusGround и проверкой на касание с whatIsGround

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)) {  //  когда игрок на земле и клавиша пробел нажата игрок прыгает
            rb.velocity = Vector2.up * jumpForce;  //  передвижение игрока вверх
            anim.SetTrigger("takeOf");  //  анимация отрыва от земли
        }

        if (isGrounded == true)  //  если на земеле то прыжок не активен
        {
            anim.SetBool("isJumping", false);
        } else {
            anim.SetBool("isJumping", true);
        }

        if (isGroundNear == true && isGrounded == false)  //  если в воздухе и земля близко, используем анимацию приземленя
        {
            anim.SetBool("isGroundNear", true);
        }
        else {
            anim.SetBool("isGroundNear", false);
        }
    }



    void Flip()  //  функция переворота игрока
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

     void OnTriggerEnter2D(Collider2D collision)   //  проверки на столкновение с разными объектами
    {
        if (collision.tag.Equals("Heal"))
        {
            health++;
            Destroy(collision.gameObject);
        }
        if (collision.tag.Equals("Heal2"))
        {
            health += 2;
            Destroy(collision.gameObject);
        }
        if (collision.tag.Equals("HealMax"))
        {
            health = healthMax;
            Destroy(collision.gameObject);
        }


        if (collision.tag.Equals("Trap"))
        {
            health -= 1;
        }


        if (collision.tag.Equals("Coin5"))
        {
            CoinCollect.coin += 5;
            Destroy(collision.gameObject);
        }
        if (collision.tag.Equals("Coin10"))
        {
            CoinCollect.coin += 10;
            Destroy(collision.gameObject);
        }
        if (collision.tag.Equals("Coin20"))
        {
            CoinCollect.coin += 20;
            Destroy(collision.gameObject);
        }
    }

    private void HealUpdate()
    {
        if (health > healthMax)  //  ограничители по количеству здоровья
            health = healthMax;
        if (health < 0)
            health = 0;

        if (health == 5)
        {
            healthBar.sprite = healthFull;
        }  //  выбор спрайта в зависимости от количества жизней
        else if (health == 4)
        {
            healthBar.sprite = healthFour;
        }
        else if (health == 3)
        {
            healthBar.sprite = healthThree;
        }
        else if (health == 2)
        {
            healthBar.sprite = healthTwo;
        }
        else if (health == 1)
        {
            healthBar.sprite = healthOne;
        }
        else if (health == 0)
        {
            healthBar.sprite = healthEmpty;
        }
    }    
}
