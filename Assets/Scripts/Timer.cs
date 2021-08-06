using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TextTimer;
    private int MaxTime;
    [SerializeField]
    private int tec;

    public int MAXTIME
    {
        get { return MaxTime; }
        set { MaxTime = value; tec = MaxTime; started = true; }
    }

    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            tec--;
            TextTimer.text = tec.ToString();
            if (tec <= 0) { Player.game = false; SceneManager.LoadScene("Menu"); }
        }
    }

    public void EndStage()
    {
        Player.game = true;
        Player.score += tec;
        SceneManager.LoadScene("Menu");
    }
}
