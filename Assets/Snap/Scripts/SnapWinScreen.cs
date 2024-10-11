using System;
using pure_unity_methods;
using UnityEngine;

namespace Snap.Scripts
{
   [RequireComponent(typeof(Canvas))]
   public class SnapWinScreen : MonoBehaviour
   {
      private Canvas canvas;

      private void Awake()
      {
         canvas = GetComponent<Canvas>();
      }

      public void Activate(int time, Action callBack)
      {
         canvas.enabled = true;
         StartCoroutine(Wait.WaitThenCallBack(time, () =>
         {
            canvas.enabled = false;
            callBack();
         }));
      }
   }
}
