using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// IsOnLayer - Custom Visual Scripting Node
/// by Connor McGrath
///
/// This node detects whether a GameObject is on one of the layers in the specified LayerMask and returns a boolean.
/// Based on IsOnLayerNode by Malcolm Ryan
/// 
/// Licensed under Creative Commons License CC0 Universal
/// https://creativecommons.org/publicdomain/zero/1.0/

namespace WordsOnPlay.Nodes {

[TypeIcon(typeof(LayerMask))]
public class IsOnLayer : Unit
{
    [DoNotSerialize]
    public ValueInput gameObjectValue;

    [DoNotSerialize]
    public ValueInput layerMaskValue;

    [DoNotSerialize]
    public ValueOutput resultValue;

    protected override void Definition()
    {
        gameObjectValue  = ValueInput<GameObject>("object");
        layerMaskValue  = ValueInput<LayerMask>("layer mask");
        resultValue = ValueOutput<bool>("result", (flow) =>
        {
            GameObject obj = flow.GetValue<GameObject>(gameObjectValue);
            LayerMask layerMask = flow.GetValue<LayerMask>(layerMaskValue);

            return (layerMask.value & (1 << obj.layer)) != 0;
        });

        Requirement(gameObjectValue, resultValue);
        Requirement(layerMaskValue, resultValue);
    }
}

}