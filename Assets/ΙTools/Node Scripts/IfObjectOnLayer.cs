using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// IsOnLayerNode - Custom Visual Scritping Node
/// by Malcolm Ryan
/// IfObjectOnLayer, tweaked by me :)
///
/// This node detects whether a GameObject is on one of the layers in the specified LayerMask.
/// 
/// Licensed under Creative Commons License CC0 Universal
/// https://creativecommons.org/publicdomain/zero/1.0/

namespace WordsOnPlay.Nodes {

public class IfObjectOnLayer : Unit
{
    [DoNotSerialize]
    public ControlInput inputTrigger;

    [DoNotSerialize]
    public ControlOutput outputTriggerTrue;

    [DoNotSerialize]
    public ControlOutput outputTriggerFalse;

    [DoNotSerialize]
    public ValueInput gameObjectValue;

    [DoNotSerialize]
    public ValueInput layerMaskValue;

    protected override void Definition()
    {
        inputTrigger = ControlInput("", (flow) =>
        {
            GameObject obj = flow.GetValue<GameObject>(gameObjectValue);
            LayerMask layerMask = flow.GetValue<LayerMask>(layerMaskValue);

            bool output = (layerMask.value & (1 << obj.layer)) != 0;
            if (output) {
                return outputTriggerTrue;
            } else {
                return outputTriggerFalse;
            }
        });
        outputTriggerTrue = ControlOutput("True");
        outputTriggerFalse = ControlOutput("False");

        gameObjectValue  = ValueInput<GameObject>("object");
        layerMaskValue  = ValueInput<LayerMask>("layerMask");

        Requirement(gameObjectValue, inputTrigger);
        Requirement(layerMaskValue, inputTrigger);
        Succession(inputTrigger, outputTriggerTrue);
        Succession(inputTrigger, outputTriggerFalse);
    }
}

}