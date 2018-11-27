using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invoker : Singleton<Invoker> {

    public void InvokeAction(Action actionToRun, float delay)
    {
        StartCoroutine(RunAction(actionToRun, delay));
    }

    IEnumerator RunAction(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }
}
