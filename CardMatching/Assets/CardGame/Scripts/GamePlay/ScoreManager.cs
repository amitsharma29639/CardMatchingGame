

using System;
using UnityEngine;

public class ScoreManager
{
   public Action<int> OnScoreChanged = delegate { };
   private int score = 0;

   public void AddScore(int amount)
   {
       score += amount;
       OnScoreChanged(score);
       Debug.Log("score : "+score);
   }

   public void OnDisable()
   {
       OnScoreChanged = delegate { };
   }
}
