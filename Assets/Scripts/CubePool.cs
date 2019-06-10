using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : ObjectPool<BlockCheck>
{
    public static CubePool Cb;

    private void Awake()
    {
        if (Cb == null)
        {
            Cb = this;
        }
        base.Awake();
    }
}
