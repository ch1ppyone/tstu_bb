using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<Game>
{
   public Transform path;
   public GameData data;
   
   
   
   [SerializeField] private Castle _castleEnemy;
   [SerializeField] private Castle _castlePlayer;
   [SerializeField] private AudioSource _bgm;
   
   [SerializeField] private AudioClip _lose;
   [SerializeField] private AudioClip _victory;
   
   
   private int wavesRemain;
   private int currentWave =  0;
   private GameStatusUI _totalStatus;
   private ButtonCoolDown _btnCoolDown;

   public bool isEnd = false;

   
   
   public void Start()
   {
      wavesRemain = data.waves.Count;
      _totalStatus = GetComponent<GameStatusUI>();
      _bgm = GetComponent<AudioSource>();
      _btnCoolDown = GetComponent<ButtonCoolDown>();
      _btnCoolDown.DisabelAll();



   }

   public void GameStart()
   {
      _bgm.Play();
      _bgm.loop = true;
      _btnCoolDown.EnbaleAll();
      StartCoroutine(RunSpawner());
   }

   public void CheckStatus(string layer)
   {
      if (layer == "Player")
      {
         Lose();
      }
      else Victory();
   }


   private void Lose()
   {
     _totalStatus.ShowLose();
     _castleEnemy.DestroyAll();
     isEnd = true;
     _bgm.clip = _lose;
     _bgm.loop = false;
     _bgm.Play();

   }

   private void Victory()
   {
      _totalStatus.ShowVictory();
      _castlePlayer.DestroyAll();
      isEnd = true;
      _bgm.clip = _victory;
      _bgm.loop = false;
      _bgm.Play();
      
   }

   
   IEnumerator SpawnWave()
   {
      if (_castleEnemy.units.Count == 0)
      {
         for (int j = 0; j < data.waves[currentWave].content.Count; j++)
         {
            yield return new WaitForSeconds(1);
            _castleEnemy.Spawn(data.waves[currentWave].content[j]);
         }
         
         currentWave++;

      }
   }

   
   private IEnumerator RunSpawner()
   {
      yield return new WaitForSeconds(1);
      
      while (!isEnd)
      {
         yield return SpawnWave();
      }
   }
   
   
}
