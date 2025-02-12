using UnityEngine; 
using UnityEngine.UI;

public class Score : MonoBehaviour
{
   public static Score instance;
   
   public Text scoreText;
   
   int score = 0;

   private void Awake() {
    instance = this;
   }

     void start()
    {
        scoreText.text = score.ToString() + "POINTS";

    }
   
   
    public void AddPoint() {
        score += 1;
        scoreText.text = score.ToString() + "POINTS";
        
    }
}
