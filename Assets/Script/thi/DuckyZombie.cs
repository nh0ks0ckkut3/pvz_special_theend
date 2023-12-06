using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class duckyZombie : MonoBehaviour
{
  
  public float speed;
  private float speedCool;
  private float speedStart;
  private Renderer ren;
  public int health;
  public int damage;
  public float eatCooldown;
  private bool isRun = true;

  private game_manager gameManager;
  private Vector3 vector3;
  private bool canEat = true;
  public Plant targetPlant;

  private Animator zombieAnimator;// Tham chiếu tới Animator của zombie
  void Start()
  {
    gameManager = game_manager.instance;
    zombieAnimator = GetComponent<Animator>();
    ren = GetComponent<Renderer>();
    speedCool = 0.6f * speed;
    speedStart = speed;
    //StartCoroutine(ApplyDamageOverTime());
    if (gameManager.isRoofMap)
    {
      vector3 = new Vector3(-1, -0.11f, 0);
    }
    else
    {
      vector3 = Vector3.left;

    }
  }

  // Update is called once per frame
  void Update()
  {

    if (isRun)
    {
      transform.Translate(vector3 * speed * Time.deltaTime);

      zombieAnimator.SetInteger("status", 1);
      if ((float)health <= 0)
      {
        isRun = !isRun;
        gameManager.sound_die.Play();
        zombieAnimator.SetInteger("status", 2);
        Destroy(gameObject, 1.3f);
      }

    }
    else
    {
      if ((float)health <= 0)
      {
        isRun = !isRun;
        gameManager.sound_die.Play();
        zombieAnimator.SetInteger("DuckyZombie_status", 3);
        Destroy(gameObject, 1.3f);
      }
    }


  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("plant"))
    {
      isRun = false;
      zombieAnimator.SetBool("Eat", true);
    }
    if (collision.CompareTag("bullet"))
    {
      gameManager.sound_bullet.Play();
      Destroy(collision.gameObject);
      health--;
    }
    if (collision.CompareTag("bullet_cold"))
    {
      gameManager.sound_bullet.Play();
      Destroy(collision.gameObject);
      health--;
      Invoke("khoiphuctrangthai", 5f);
      speed = speedCool;
      ren.material.color = Color.blue;
    }
    if (collision.CompareTag("LawnMower"))
    {
      LawnMowers lawnMowersComponent = collision.gameObject.GetComponent<LawnMowers>();

      if (lawnMowersComponent == null)
      {
        // Nếu chưa tồn tại, thêm mới
        collision.gameObject.AddComponent<LawnMowers>();
        gameManager.sound_lawnMower.Play();

      }

      // Sau đó, hủy GameObject hiện tại
      Destroy(gameObject);
    }
    if (collision.CompareTag("GameOver"))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  }
  private void OnTriggerStay2D(Collider2D collision)
  {
    if (collision.CompareTag("plant"))
    {
      targetPlant = collision.GetComponent<Plant>();
      Eat();
    }
  }
  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.CompareTag("plant"))
      isRun = true;
    zombieAnimator.SetBool("Eat", false);
  }
  void Eat()
  {



    // nếu đúng 1 trong 2 điều kiện thì hàm sẽ return không cho chạy hàm dưới
    if (!canEat || !targetPlant)
      return;
    gameManager.sound_eat_plant.Play();
    canEat = false;
    Invoke("ResetEatCooldown", eatCooldown);

    targetPlant.Hit(damage);
  }
  void ResetEatCooldown()
  {
    canEat = true;
  }
  void khoiphuctrangthai()
  {
    speed = speedStart;
    ren.material.color = Color.white;
  }
  // hàm test xuống máu
  //private IEnumerator ApplyDamageOverTime()
  //{
  //    while (true)
  //    {
  //        yield return new WaitForSeconds(1f); // Chờ 1 giây

  //        health -= damagePerSecond; // Trừ máu

  //    }
  //}
}
