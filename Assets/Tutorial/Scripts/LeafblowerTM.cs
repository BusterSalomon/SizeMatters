using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafblowerTM : TutorialManager
{
    private bool collected;
    private bool didCollectOnce;
    private bool didReleaseOnce;
    private bool leafblowerUsed;

    void Start()
    {
        collected = false;
        leafblowerUsed = false;
    }

    protected override void Update()
    {
        base.Update();

        if (collected && Input.GetKey(KeyCode.N))
        {
            leafblowerUsed = true;
        }
    }

    public override bool DidComplete()
    {
        return didCollectOnce && didReleaseOnce && leafblowerUsed;
    }

    public void DidCollectLeafblower ()
    {
        didCollectOnce = true;
        collected = true;
    }

    public void DidReleaseLeafblower ()
    {
        didReleaseOnce = true;
        collected = false;
    }
}
