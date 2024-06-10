using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock: WorldItem{
    public override string Name { get { return "Rock";}}

    public override void OnUse(){
        base.OnUse();
    }
}