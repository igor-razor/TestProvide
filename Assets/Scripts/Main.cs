using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Canvas canvas;

    private Color[] Colors = { Color.red, Color.blue, Color.green, Color.magenta, Color.yellow, Color.cyan, Color.black, Color.white};

    private float xL = -10;
    private float xR = 10;

    private float yU = 6;
    private float yD = -4;

    private int Ntec = 7;
    private int Mtec = 300;

    float delta = 0;

    /*
    [SerializeField]
    private GameObject tecLine = null;

    public GameObject TECLINE
    {
        get { return tecLine; }
        set { tecLine = value; }
    }
    */

    [SerializeField]
    private GameObject Cube1 = null;
    [SerializeField]
    private GameObject Cube2 = null;

    public GameObject CUBE1
    {
        get { return Cube1; }
        set { Cube1 = value; }
    }

    public GameObject CUBE2
    {
        get { return Cube2; }
        set { Cube2 = value; }
    }

    public void Clear() { CUBE1 = null; CUBE2 = null; }

    public bool Check1(GameObject go)
    {
        if (Lclicked.Find(x => x == go)) { return true; }
        else return false;
    }

    public bool Check2()
    {
        if ((CUBE1 != null) && (CUBE2 != null))
            if (CUBE1.GetComponent<Cube>().COLOR == CUBE2.GetComponent<Cube>().COLOR)
            {
                Lclicked.Add(CUBE1);
                Lclicked.Add(CUBE2);
                return true;
            }
        return false;
    }


    public void CheckEnd()
    {
        if (Lclicked.Count == 2*Player.N) { gameObject.GetComponent<Timer>().EndStage(); }
    }


    private List<GameObject> Lclicked = new List<GameObject>();

    private Material matLine;

    // Start is called before the first frame update
    void Start()
    {
        Mtec = Player.M;
        Ntec = Player.N;

        matLine = new Material(Shader.Find("GUI/Text Shader")); if (matLine == null) return;

        delta = Mathf.Abs(yU - yD) / (Ntec-1);

        List<Color> Lcolors = new List<Color>();
        for (byte i = 0; i < Ntec; i++) { Lcolors.Add(Colors[i]); }

        CreateCubes(xL, Lcolors);
        CreateCubes(xR, Lcolors);
        
        gameObject.GetComponent<Timer>().MAXTIME = Mtec;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePos);
        /*
        if ((CUBE1 != null)&&(CUBE2 == null))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Destroy(tecLine.gameObject);
            Fline1(CUBE1.transform.position, mousePos, CUBE1.GetComponent<Cube>().COLOR);
            //Debug.Log("repaint");
        }
        */
    }

    

    void CreateCubes(float x, List<Color> LcolorsIN)
    {
        float ytec = yD;
        List<Color> Lcolors = new List<Color>();

        foreach (Color c in LcolorsIN) { Lcolors.Add(c); }

        for (byte i = 0; i < Ntec; i++)
        {
            Color c = Lcolors[UnityEngine.Random.Range(0, Lcolors.Count)];

            CreateCube(c, x, ytec);
            Lcolors.Remove(c);

            ytec += delta;
        }
    }

    void CreateCube(Color color, float x, float y)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.SetParent(transform);
        cube.AddComponent<Cube>().SetMain(gameObject);

        cube.GetComponent<Renderer>().material.color = color;
        cube.GetComponent<Cube>().COLOR = color;
        
        cube.transform.position = new Vector3(x, y, 0);

    }

    float FireLineW = 0.2f;

    public void Fline1(Vector3 v1, Vector3 v2, Color LineColor)
    {
        GameObject line = new GameObject("Line");
        //line.transform.SetParent(LineParent.transform);
        LineRenderer tempLineRenderer = line.AddComponent<LineRenderer>();
        tempLineRenderer.SetPosition(0, v1);
        tempLineRenderer.SetPosition(1, v2);

        tempLineRenderer.material = matLine;
        tempLineRenderer.startColor = LineColor;
        tempLineRenderer.endColor = LineColor;

        tempLineRenderer.startWidth = FireLineW;
        tempLineRenderer.endWidth = FireLineW;

        //TECLINE = line;
        //Destroy(line, MunitData.destroy_delay_line);
    }//private void Fline1()

}
