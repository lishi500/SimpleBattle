using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMove : MonoBehaviour {
    Vector3 start;
    Vector3 end;
    float height;
    float duration;
    float executedTime;
    bool reachHalf;
    private bool pause;
    public bool destoryAfterAnimation;
    public CurveMoveStatus currentStatus;

    public delegate void CurveMoveDelegate(CurveMoveStatus status);
    public event CurveMoveDelegate notifyCurveMoveEvent;

    public enum CurveMoveStatus
    {
        IDLE,
        PAUSE,
        START,
        MOVING,
        HEIGHTEST,
        END,
        DESTORIED
    }


    // Use this for initialization
    void Awake() {
        pause = true;
        destoryAfterAnimation = true;
        reachHalf = false;
        height = 3f;
        duration = 1f;
        executedTime = 0f;
        start = this.transform.position;
       
    }

    void Start()
    {
        notifyCurveMove(CurveMoveStatus.IDLE);
    }

    public void Pause() {
        pause = true;
        notifyCurveMove(CurveMoveStatus.PAUSE);
    }

    public void Animate() {
        pause = false;
        notifyCurveMove(CurveMoveStatus.START);
    }

    private void notifyCurveMove(CurveMoveStatus status) {
        if (notifyCurveMoveEvent != null && currentStatus != status) {
            currentStatus = status;
            notifyCurveMoveEvent(status);
        }
    }
    public void SetPosition(Vector3 start, Vector3 end, float height, float duration) {
        this.start = start;
        SetPosition(end, height, duration);
    }

    public void SetPosition(Vector3 end, float height, float duration)
    {
        this.end = end;
        this.height = height;
        this.duration = duration;
    }

    public void SetPosition(float x, float y, float height, float duration)
    {
        SetRetelativeEnd(x, y);
        this.height = height;
        this.duration = duration;
    }

    public void SetEnd(Vector3 end) {
        this.end = end;
    }

    public void SetRetelativeEnd(float x, float y)
    {
        end = new Vector3(start.x + x, start.y + y);
    }

    private float LerpTime() {
        float standardTime = executedTime / duration;
        if (!reachHalf && standardTime >= 0.5f) {
            reachHalf = true;
            notifyCurveMove(CurveMoveStatus.HEIGHTEST);
        }
        return standardTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (!pause) {
            if (executedTime < duration)
            {
                executedTime = (executedTime + Time.deltaTime) > duration ? duration : executedTime + Time.deltaTime;
                Vector2 v2Pos = MathParabola.Parabola2(start, end, height, LerpTime());
                this.transform.position = v2Pos;
            }
            else {
                notifyCurveMove(CurveMoveStatus.END);
                if (destoryAfterAnimation) {
                    Destroy();
                }
            }

        }
	}

    public void Destroy()
    {
        notifyCurveMove(CurveMoveStatus.DESTORIED);
        Destroy(this.transform.gameObject);
    }
}
