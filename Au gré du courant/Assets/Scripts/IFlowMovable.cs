using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlowMovable
{
    Vector2 CurrentFlowDir {
        get;
        set;
    }

    float CurrentFlowForce {
        get;
        set;
    }

    void ApplyFlow();

}
