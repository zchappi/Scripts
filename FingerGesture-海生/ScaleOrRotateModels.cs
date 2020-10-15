using UnityEngine;

/// <summary>
/// 旋转和缩放模型
/// </summary>
public class ScaleOrRotateModels : MonoBehaviour
{
    //最大和最小缩放比例值
    private float maxScale = 100;
    private float minScale = 20;

    private float newdis = 0;
    private float olddis = 0;
    private float horizontalSpeed = 5;
    private float verticalSpeed = 5;
    private Vector3 rotatepos;
    private Vector3 defaultScale;

    void Start()
    {
        rotatepos = transform.position;
        defaultScale = transform.localScale;
    }

    void LateUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //单指触屏滑动，物体的旋转
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    float h = Input.GetAxis("Mouse X");//右正左负 
                    float v = Input.GetAxis("Mouse Y");//上正下负 
                    if (Mathf.Abs(h) >= Mathf.Abs(v))
                    {
                        if (h < 0)
                        {
                            transform.RotateAround(rotatepos, Vector3.up, horizontalSpeed);
                        }
                        if (h > 0)
                        {
                            transform.RotateAround(rotatepos, -Vector3.up, horizontalSpeed);
                        }
                    }
                    else
                    {
                        if (v < 0)
                        {
                            transform.RotateAround(rotatepos, -Vector3.right, verticalSpeed);
                        }
                        if (v > 0)
                        {
                            transform.RotateAround(rotatepos, Vector3.right, verticalSpeed);
                        }
                    }
                }
            }
            //两指触屏滑动，物体的缩放 
            if (Input.touchCount > 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    var pos1 = Input.GetTouch(0).position;
                    var pos2 = Input.GetTouch(1).position;
                    newdis = Vector2.Distance(pos1, pos2);
                    if (newdis < olddis && transform.localScale.x > minScale)
                    {
                        transform.localScale -= defaultScale * Time.deltaTime * 2;
                    }
                    if (newdis > olddis && transform.localScale.x < maxScale)
                    {
                        transform.localScale += defaultScale * Time.deltaTime * 2;
                    }
                    olddis = newdis;
                }
            }
        }
        else
        {
            //鼠标左键，物体旋转
            if (Input.GetMouseButton(0))
            {
                float h = Input.GetAxis("Mouse X");//右正左负 
                float v = Input.GetAxis("Mouse Y");//上正下负 

                if (Mathf.Abs(h) >= Mathf.Abs(v))
                {
                    if (h < 0)
                    {
                        transform.RotateAround(rotatepos, Vector3.up, horizontalSpeed);
                    }
                    if (h > 0)
                    {
                        transform.RotateAround(rotatepos, -Vector3.up, horizontalSpeed);
                    }
                }
                else
                {
                    if (v < 0)
                    {
                        transform.RotateAround(rotatepos, -Vector3.right, verticalSpeed);
                    }
                    if (v > 0)
                    {
                        transform.RotateAround(rotatepos, Vector3.right, verticalSpeed);
                    }
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.localScale.x < maxScale)
            {
                transform.localScale += defaultScale * Time.deltaTime * 2;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.localScale.x > minScale)
            {
                transform.localScale -= defaultScale * Time.deltaTime * 2;
            }
        }
    }
}