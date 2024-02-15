using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;



public class PlayerAim : MonoBehaviour
{

    public event EventHandler<OnShootEventArgs> OnShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }
    private Transform aimTransform;
    private Transform aimGunEndPointTransform;

    private void Awake()
    {
        aimTransform = transform.Find("shooter");
        aimGunEndPointTransform = aimTransform.Find("gunEndPointPosition");

    }
    private void Update()
    {
        Aiming();
        Shooting();
    }
    private void Aiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.x, aimDirection.y) * -Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        Vector3 aimLocalScale = Vector3.one;
        aimTransform.localScale = aimLocalScale;
    }
    private void Shooting()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            OnShoot?.Invoke(this, new OnShootEventArgs { gunEndPointPosition = aimGunEndPointTransform.position, shootPosition = mousePosition });

        }

    }


}
