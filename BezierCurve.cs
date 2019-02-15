using UnityEngine;
using System.Collections.Generic;

public class BezierCurve : MonoBehaviour
{
    public List<Transform> controlPointList;
    public LineRenderer lineRenderer;
    public int iteration;

    public float speed;
    public float distance;
    public GameObject objectRoot;

    private float curveLength;

    float GetApproxCurveLength( List<Vector3> curvePointList)
    {
        float curveLength = 0;
        for(int i = 0; i < curvePointList.Count - 1; i++)
        {
            curveLength += Vector3.Distance(curvePointList[i], curvePointList[i + 1]);
        }

        return curveLength;
    }

    List<Vector3> GetControlPointPositionList( List<Transform> controlPointList )
    {
        List<Vector3> controlPointPositionList = new List<Vector3>();
        foreach( Transform controlPoint in controlPointList )
        {
            controlPointPositionList.Add(controlPoint.position);
        }

        return controlPointPositionList;
    }

    void SetCurveLength()
    {
        double step = 1.0f / iteration;
        List<Vector3> curvePointList = new List<Vector3>();
        List<Vector3> controlPointPositionList = GetControlPointPositionList(controlPointList);

        for (int i = 0; i <= iteration; i++)
        {
            Vector3 curvePoint = CurveHelper.BezierCurveValue(i * step, controlPointPositionList);
            curvePointList.Add(curvePoint);
        }

        curveLength = GetApproxCurveLength(curvePointList);
    }

    void DrawCurve()
    {
        double step = 1.0f / iteration;
        List<Vector3> curvePointList = new List<Vector3>();
        List<Vector3> controlPointPositionList = GetControlPointPositionList(controlPointList);

        for (int i=0; i<=iteration; i++)
        {
            Vector3 curvePoint = CurveHelper.BezierCurveValue(i * step, controlPointPositionList);
            curvePointList.Add(curvePoint);
        }

        curveLength = GetApproxCurveLength(curvePointList);

        Vector3[] curvePointArray = curvePointList.ToArray();
        lineRenderer.positionCount = curvePointArray.Length;
        lineRenderer.SetPositions(curvePointArray);
    }

    void MoveObjectRoot()
    {
        List<Vector3> controlPointPositionList = GetControlPointPositionList(controlPointList);
        distance += speed;
        float parameter = ( distance + speed )/curveLength;
        if(distance > curveLength)
        {
            distance = 0;
        }

        Vector3 newObjectPosition = CurveHelper.BezierCurveValue(parameter, controlPointPositionList);
        objectRoot.transform.position = newObjectPosition;
    }

    void Start()
    {
        //DrawCurve();
        SetCurveLength();
    }

    void Update()
    {
        MoveObjectRoot();
    }


}