using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingMenu : MonoBehaviour
{
    public GameObject[] buttonList;
    public MenuSection[] menuSection;
    [SerializeField] private float UIAngle = 360.0f;
    [SerializeField] private float sectionSize;
    private Vector2 mousePosition;
    private InputReader inputReader;



    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {

        }
    }

    private void FixedUpdate()
    {
        mousePosition = inputReader.GetMousePos();
    }

    // Update is called once per frame
    void Update()
    {
        //normalizedMousePosition = new Vector2(inputReader.GetMousePos().x - Screen.width/2, inputReader.GetMousePos().y - Screen.height/2);
        sectionSize = UIAngle / buttonList.Length;

        for(int i = 0; i < buttonList.Length; i++)
        {

        }


    }

}
