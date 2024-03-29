﻿using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
class DashActionData : IComponentData{

    public float dashCoolDownTotal;
    public float dashCoolDown;
    public bool CanDash => dashCoolDown >= dashCoolDownTotal;
}
