using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour , IPointerClickHandler
{
    [SerializeField]
    private GameObject GOmain = null;
    [SerializeField]
    private Color color = Color.gray;

    public Color COLOR
    {
        get { return color; }
        set { color = value; }
    }

    public void SetMain(GameObject GO) { GOmain = GO; }

    //void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GOmain.GetComponent<Main>().Check1(gameObject) == false)
        {
            //throw new NotImplementedException();
            //Debug.Log(eventData.position);

            bool flag = true;

            // second click
            if (flag)
                if ((GOmain.GetComponent<Main>().CUBE1 != null) && (GOmain.GetComponent<Main>().CUBE2 == null))
                {
                    GOmain.GetComponent<Main>().CUBE2 = gameObject;
                    if (GOmain.GetComponent<Main>().Check2())
                        GOmain.GetComponent<Main>().Fline1(GOmain.GetComponent<Main>().CUBE1.transform.position, GOmain.GetComponent<Main>().CUBE2.transform.position, COLOR);

                    GOmain.GetComponent<Main>().Clear();
                    flag = false;

                    GOmain.GetComponent<Main>().CheckEnd();
                }

            // first click
            if (flag)
                if ((GOmain.GetComponent<Main>().CUBE1 == null) && (GOmain.GetComponent<Main>().CUBE2 == null))
                {
                    GOmain.GetComponent<Main>().CUBE1 = gameObject;
                    //GOmain.GetComponent<Main>().Fline1(gameObject.transform.position, gameObject.transform.position, COLOR);
                    flag = false;
                }

        }//if (GOmain.GetComponent<Main>().Check1(gameObject) == false)
    }//public void OnPointerClick(PointerEventData eventData)

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("i created");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
