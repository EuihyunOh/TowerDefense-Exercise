using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployAreaCheck : MonoBehaviour
{
    bool _isPointerOnAllowedArea = true;

    public bool isPointerOnAllowedArea()
    {
        return _isPointerOnAllowedArea;
    }

    void OnMouseEnter()
    {
        _isPointerOnAllowedArea = true;
        //Debug.Log(string.Format("MouseEnter"));
    }

    void OnMouseExit()
    {
        _isPointerOnAllowedArea = false;
        //Debug.Log(string.Format("MouseExit"));
    }
}
