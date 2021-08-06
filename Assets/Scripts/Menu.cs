using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Linq;

public class Menu : MonoBehaviour
{
    public Text TextTecScore;
    public Text TextTopScore;

    string pathSettings = "settings.txt";
    string pathTop = "top.txt";

    private byte itop = 10;

    // Start is called before the first frame update
    void Start()
    {
        if (Player.game == false)
        {
            TextTecScore.text = Player.score.ToString();
            //Debug.Log("game false");
            ReadData();

            OutputTop( ReTop() , INDEX(Player.score) );
        }

        if (Player.game == true)
        {
            //Debug.Log("game true");
            Player.M = Player.M - Player.Mdelta;
            if (Player.N + Player.Ndelta < Player.Nmax) { Player.N = Player.N + Player.Ndelta; }
            SceneManager.LoadScene("Game");
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ReadData()
    {
        try
        {
            using (StreamReader sr = new StreamReader(pathSettings))
            {
                //string data = sr.ReadLine();
                //Debug.Log(data);

                string[] words = sr.ReadLine().Split(' ');
                //Debug.Log(words);
                Player.Mmax = Convert.ToInt32(words[0]);
                Player.Mdelta = Convert.ToInt32(words[1]);
                Player.Nmax = Convert.ToInt32(words[2]);
                Player.Ndelta = Convert.ToInt32(words[3]);
            }

            using (StreamReader sr = new StreamReader(pathTop))
            {
                string[] words = sr.ReadLine().Split(' ');
                for (byte i = 0; i<10; i++)
                    { Player.Top[i] = Convert.ToInt32(words[i]); }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }

    public void ClickStart()
    {
        Player.M = Player.Mmax;
        Player.N = 1;
        Player.game = true;
        SceneManager.LoadScene("Game");
    }

    ////// TOP //////

    private void OutputTop(bool flag, int index)
    {
        TextTopScore.text = "TOP 10 \n";
        for (int i = itop-1; i >= 0; i--)
        {
            string color = "lime";
            if ((i == index) && (flag == true)) { color = "gold"; }
            TextTopScore.text += "<color=" + color + ">" + Player.Top[i] + "</color>\n";
        }
    }//private void OutputTop()


    private bool ReTop()
    {
        int imin = INDEX(Player.Top.Min());
        if (Player.score > Player.Top.Min()) { Player.Top[imin] = Player.score; Player.Top.OrderBy(x => x >= 0); WriteTop();  return true; }
        return false;
    }//private void reTop()

    private void AddTec()
    {
        for (byte i=0; i<itop; i++)
        {
           // if ()
        }
    }//private void AddTec()

    private int INDEX(int value)
    {        
        int index = itop;

        for (int i = 0; i < itop; i++)  if (Player.Top[i] == value) { index = i; }
       
        return index;
    }//private int MinTopIndex()

    private void WriteTop()
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(pathTop, false, System.Text.Encoding.Default))
            {
                string text = "";
                for (byte i=0; i<itop; i++)
                {
                    text += Player.Top[i].ToString();
                    if (i != itop-1) { text += " "; }
                }
                sw.WriteLine(text);
            }            
        }
        catch (Exception e)
        {
           Debug.Log(e.Message);
        }
    }
}
