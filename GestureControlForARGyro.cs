using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using static FingerGestures;
using System.Collections.Generic;

public class GestureControlForARGyro : MonoBehaviour, IPointerClickHandler
{
    InterFace interFace;
    bool isCanDrag = true;
    void Start()
    {

    }

    void Awake()
    {
        //	m_gyrogameupdate = Camera.main.GetComponent<GyroGameUpdate>();
        interFace = GameObject.Find("InterFace").GetComponent<InterFace>();

    }
    int count = 0;
    Vector3 pos;
    public bool isPinch = true;//限制拖动
    float speed = 2f;
    public float tempDeltaTime=0.1f;
    bool isNowPinch = false;//判断当前是否处于拖拽


    private Vector2 firstPos;
    private Vector2 secondPos;
    private Vector2 thirdPos;
    private Vector2 fourthPos;
    private Vector2 fifthPos;
    private float dotFloat = -0.5f;
    bool isCan = true;

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
    private void Update()
    {
        //Func();
        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
        

        }
        if (Input.touches.Length >= count || Input.touches.Length == 0)
        {
            count = Input.touches.Length;
        }
        if (count == 1)
        {

            if (Input.GetMouseButtonUp(0))
            {
                if (pos == Input.mousePosition)
                {
                    interFace.PauseOrContinueModel();
                }
                else
                {
                    Debug.Log("pos:" + pos + "    " + Input.mousePosition);
                }

                //count = 0;
            }
        }
        if (Input.GetMouseButton(0) && !isNowPinch)
        {
            float h = Input.GetAxis("Mouse X");//右正左负 
            float v = Input.GetAxis("Mouse Y");//上正下负 
            if (pos == Input.mousePosition)
            {
                return;
            }
            if (Mathf.Abs(h) >= Mathf.Abs(v))
            {
                //Debug.Log("缩放缩放缩放缩放缩放缩放缩放缩放");
                if (h < -0.1f)
                {
                    CustomTemplet.GameManager._Instance.m_Actor.gameObject.transform.RotateAround(CustomTemplet.GameManager._Instance.m_Actor.gameObject.transform.position, CustomTemplet.GameManager._Instance.m_Actor.gameObject.transform.up, speed);
                }
                if (h > 0.1f)
                {
                    CustomTemplet.GameManager._Instance.m_Actor.gameObject.transform.RotateAround(CustomTemplet.GameManager._Instance.m_Actor.gameObject.transform.position, -CustomTemplet.GameManager._Instance.m_Actor.gameObject.transform.up, speed);
                }
            }
            else
            {
                //Debug.Log("缩放缩放缩放缩放缩放缩放缩放缩放11111");
                if (v < -0.1f)
                {
                    CustomTemplet.GameManager._Instance.m_Actor.gameObject.transform.RotateAround(CustomTemplet.GameManager._Instance.m_Actor.gameObject.transform.position, -Camera.main.transform.right, speed);
                }
                if (v > 0.1f)
                {
                    CustomTemplet.GameManager._Instance.m_Actor.gameObject.transform.RotateAround(CustomTemplet.GameManager._Instance.m_Actor.gameObject.transform.position, Camera.main.transform.right, speed);
                }
            }
        }

        if (Input.touchCount > 2)
        {
            if(isCan)
            {
                firstPos = Input.GetTouch(0).position;
                secondPos = Input.GetTouch(1).position;
                thirdPos = Input.GetTouch(2).position;
                if (Input.touchCount > 3)
                {
                    fourthPos = Input.GetTouch(3).position;
                }
                if (Input.touchCount > 4)
                {
                    fifthPos = Input.GetTouch(4).position;
                }
                isCan = false;
                return;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved || Input.GetTouch(2).phase == TouchPhase.Moved || Input.GetTouch(3).phase == TouchPhase.Moved || Input.GetTouch(4).phase == TouchPhase.Moved)
            {
                var tempPos1 = Input.GetTouch(0).position;
                var tempPos2 = Input.GetTouch(1).position;
                var tempPos3 = Input.GetTouch(2).position;
                var tempPos4 = Input.GetTouch(2).position; ;
                var tempPos5 = Input.GetTouch(2).position; ;


                var arrow0 = tempPos1 - firstPos;
                var arrow1 = tempPos2 - secondPos;
                var arrow2 = tempPos3 - thirdPos;

                var arrow3 = tempPos3 - thirdPos;
                var arrow4 = tempPos3 - thirdPos;


                float dot = Vector3.Dot(arrow0.normalized, arrow1.normalized);
                float dot1 = Vector3.Dot(arrow0.normalized, arrow2.normalized);
                float dot2 = Vector3.Dot(arrow1.normalized, arrow2.normalized);
                float dot3 = 0;
                float dot4 = 0;
                float dot5 = 0;
                float dot6 = 0;
                float dot7 = 0;
                float dot8 = 0;
                float dot9 = 0;
                if (Input.touchCount > 3)
                {
                    tempPos4 = Input.GetTouch(3).position;

                    arrow3 = tempPos4 - fourthPos;

                    dot3 = Vector3.Dot(arrow0.normalized, arrow3.normalized);
                    dot4 = Vector3.Dot(arrow1.normalized, arrow3.normalized);
                    dot5 = Vector3.Dot(arrow2.normalized, arrow3.normalized);
                }
                if (Input.touchCount > 4)
                {
                    tempPos5 = Input.GetTouch(4).position;
                    arrow4 = tempPos5 - fifthPos;
                    dot6 = Vector3.Dot(arrow0.normalized, arrow4.normalized);
                    dot7 = Vector3.Dot(arrow1.normalized, arrow4.normalized);
                    dot8 = Vector3.Dot(arrow2.normalized, arrow4.normalized);
                    dot9 = Vector3.Dot(arrow3.normalized, arrow4.normalized);

                }
                Debug.Log("dot:" + dot + "dot1:" + dot1 + "dot2:" + dot2);
                if (dot < dotFloat || dot1 < dotFloat || dot2 < dotFloat || dot3 < dotFloat || dot4 < dotFloat || dot5 < dotFloat || dot6 < dotFloat || dot7 < dotFloat || dot8 < dotFloat || dot9 < dotFloat)
                {
    
                    List<float> list = new List<float>();
                    list.Add(dot);
                    list.Add(dot1);
                    list.Add(dot2);
                    list.Add(dot3);
                    list.Add(dot4);
                    list.Add(dot5);
                    list.Add(dot6);
                    list.Add(dot7);
                    list.Add(dot8);
                    list.Add(dot9);
        
                    //int index = 0;
                    //for (int i = 0; i < list.Count; i++)
                    //{
                    //    if (list[i]< dotFloat)
                    //    {
                    //        index++;
                    //    }
                    //}
                    //Debug.Log("index"+ index);
                    
                    float max = Mathf.Max(list.ToArray());
           
                    float min = Mathf.Min(list.ToArray());
       
                    if (max <= 0 || max <= (Mathf.Abs(min)))
                    {
                        if(min<-0.9f)
                        {
                            interFace.deltaTime = tempDeltaTime;
                            //视为反向 --缩放

                            float oldDis = Vector3.Distance(firstPos, secondPos);
                            float newDis = Vector3.Distance(tempPos1, tempPos2);
                            float offset = newDis - oldDis;
                            if (offset > 0)
                            {
                                isNowPinch = true;
                                interFace.EnlargeModel();
                            }
                            else if (offset < 0)
                            {
                                isNowPinch = true;
                                interFace.LessenModel();
                            }
                          
                        }
                    }
                    // if (index>=2)

                }

                firstPos = tempPos1;
                secondPos = tempPos2;
                thirdPos = tempPos3;
                if (Input.touchCount > 3)
                {
                    fourthPos = tempPos4;
                }
                if (Input.touchCount > 4)
                {
                    fifthPos = tempPos5;
                }

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isCan = true;
            isNowPinch = false;
            interFace.deltaTime = 0.03f;
        }
        //////////////////////////////
        //if (Input.touchCount==4)
        //{
        //  interFace.ResetPos();
        //}
    }

    /// <summary>
    /// 复位 还原相机位置以及模型角度
    /// </summary>

    private Vector3 getNormal(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float a = ((p2.y - p1.y) * (p3.z - p1.z) - (p2.z - p1.z) * (p3.y - p1.y));

        float b = ((p2.z - p1.z) * (p3.x - p1.x) - (p2.x - p1.x) * (p3.z - p1.z));

        float c = ((p2.x - p1.x) * (p3.y - p1.y) - (p2.y - p1.y) * (p3.x - p1.x));
        //a对应的屏幕的垂直方向，b对应的屏幕的水平方向。
        return new Vector3(a, -b, c);
    }

    //启动时调用，注册手势操作事件//
    void OnEnable()
    {
        //拖动事件:开始/中/结束//
        FingerGestures.OnFingerDragBegin += OnFingerDragBegin;
        FingerGestures.OnFingerDragMove += OnFingerDragMove;
        FingerGestures.OnFingerDragEnd += OnFingerDragEnd;
        //连击事件 连续点击事件//
        FingerGestures.OnFingerTap += OnFingerTap;
        //双指缩放事件//
        FingerGestures.OnPinchMove += OnPinchMove;
        FingerGestures.OnPinchBegin += OnPinchBegin;
        FingerGestures.OnPinchEnd += OnPinchEnd;
        //双指旋转事件//
        FingerGestures.OnRotationBegin += OnRotationBegin;
        FingerGestures.OnRotationMove += OnRotationMove;
        FingerGestures.OnRotationEnd += OnRotationEnd;
        //FingerGestures.
        FingerGestures.OnFingerLongPress += OnFingerLongPress;


    }

    //关闭时调用，销毁手势操作事件//
    void OnDisable()
    {
        FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
        FingerGestures.OnFingerDragMove -= OnFingerDragMove;
        FingerGestures.OnFingerDragEnd -= OnFingerDragEnd;

        FingerGestures.OnFingerTap -= OnFingerTap;

        FingerGestures.OnPinchMove -= OnPinchMove;
        FingerGestures.OnPinchBegin -= OnPinchBegin;
        FingerGestures.OnPinchEnd -= OnPinchEnd;

        FingerGestures.OnRotationBegin -= OnRotationBegin;
        FingerGestures.OnRotationMove -= OnRotationMove;
        FingerGestures.OnRotationEnd -= OnRotationEnd;
        FingerGestures.OnFingerLongPress -= OnFingerLongPress;
    }

    void OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
    {

    }

    int index = 0;
    void OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {


    }

    void OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
    {
    }

    void OnFingerTap(int fingerIndex, Vector2 fingerPos, int tapCount)
    {
        //Debug.Log("点击"+Input.touchCount);

    }

    void OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        //isCanDrag = false;
        interFace.deltaTime = tempDeltaTime;
    }

    void OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
        isNowPinch = true;
        if (isPinch)
        {
            if (delta > 0)
            {
                interFace.EnlargeModel();
            }
            else if (delta < 0)
            {
                interFace.LessenModel();
            }
        }

    }

    void OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
    {
        isPinch = true;
        isNowPinch = false;
        interFace.deltaTime = 0.03f;
    }

    void OnRotationBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {

    }

    void OnRotationMove(Vector2 fingerPos1, Vector2 fingerPos2, float rotationAngleDelta)
    {

    }

    void OnRotationEnd(Vector2 fingerPos1, Vector2 fingerPos2, float totalRotationAngle)
    {

    }

    void OnFingerLongPress(int fingerIndex, Vector2 fingerPos)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("点击点击");
        //if (Input.touchCount == 1)
        //{
        //    interFace.AudioController();
        //}


    }
}
