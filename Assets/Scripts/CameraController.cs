using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public float ExpectedTime;
    public bool IsMoving;
    private float originSize;
    private Vector3 originPosition;
    private Vector3 currentPosition;
    private Vector3 targetPosition;
    private float targetSize = 250f;
    private float timer;
    private bool zoomIn;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        originPosition = this.transform.position;
        originSize = this.GetComponent<Camera>().orthographicSize;
        zoomIn = false;
    }

    public void HandleZoomToPosition(Vector3 position)
    {
        currentPosition = transform.position;
        targetPosition = position;
        IsMoving = true;
        timer = 0;
        zoomIn = true;
    }

    public void HandleResetZoom()
    {
        if (IsMoving) return;
        zoomIn = false;
        IsMoving = true;
        timer = 0;
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsMoving && Input.GetMouseButtonDown(1))
        {
            HandleResetZoom();
            DesciptionPageManager.Instance.ResetPage();
        }

        if(IsMoving)
        {
            if(zoomIn)
            {
                timer += Time.deltaTime;
                this.transform.position = Vector3.Lerp(currentPosition, targetPosition, timer / ExpectedTime);
                if(this.GetComponent<Camera>().orthographicSize!= targetSize) this.GetComponent<Camera>().orthographicSize = Mathf.Lerp(originSize, targetSize, timer / ExpectedTime);
                if (timer / ExpectedTime >= 1)
                {
                    IsMoving = false;
                }
            }
            else
            {
                timer += Time.deltaTime;
                this.transform.position = Vector3.Lerp(currentPosition, originPosition, timer / ExpectedTime);
                this.GetComponent<Camera>().orthographicSize = Mathf.Lerp(targetSize, originSize, timer / ExpectedTime);
                if (timer / ExpectedTime >= 1)
                {
                    IsMoving = false;
                }
            }
        }
    }
}
