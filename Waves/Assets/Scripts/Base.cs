using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Base : MonoBehaviour
{
   public float hp = 100;
   public TextMeshPro hpText;

   private void Start()
   {
      hpText.text = hp.ToString();
   }

   public void hit(float dmg)
   {
      hp -= dmg;
      hpText.text = hp.ToString();
   }

   public float getHp()
   {
      return hp;
   }

   private void Update()
   {
      if (hp <= 0)
      {
         string currentSceneName = SceneManager.GetActiveScene().name;

         // Reload the current scene
         SceneManager.LoadScene(currentSceneName);
      }
   }
}
