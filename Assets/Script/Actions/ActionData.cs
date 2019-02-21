using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionData {
    public string actionName;
    public float actionStartTime;

    public ActionConfig config;
    public ActionType[] actionTypes;

    public ActionData(Action action) {
        this.actionName = action.actionName;
        this.config = action.config;
        this.config.actionTypes = action.config.actionTypes;
        this.actionStartTime = action.actionStartTime;
    }
}
