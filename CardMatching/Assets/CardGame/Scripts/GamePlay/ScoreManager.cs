

using System;
using UnityEngine;

[Serializable]
public class ScoreManager
{
   public Action<int> OnScoreChanged = delegate { };
   private int score = 0;

   public ScoreManager(int score)
   {
       SetScore(0);
   }
   
   public int Score => score;

   public void AddScore(int amount)
   {
       score += amount;
       OnScoreChanged(score);
       Debug.Log("score : "+score);
   }

   public void SetScore(int score)
   {
       this.score = score;
       OnScoreChanged(score);
   }

   public void OnDisable()
   {
       OnScoreChanged = delegate { };
   }
}
