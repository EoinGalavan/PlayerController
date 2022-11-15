using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    bool ducking = false;
    bool inCouroutine = false;

    void Awake()
    {
        instance = this;
    }

    public static void EstablishWeapon(GameObject weapon)
    {
        weapon.SetActive(false);
    }

    public static void Attack(GameObject weapon)
    {
        if (!instance.inCouroutine)
        {
            instance.inCouroutine = true;
            instance.StartCoroutine(instance.AttackCoroutine(weapon));
        }
    }

    public static void Duck(Rigidbody2D rb)
    {
        if (!instance.inCouroutine)
        {
            instance.ducking = true;
            instance.inCouroutine = true;
            instance.StartCoroutine(instance.DuckCoroutine(rb));
        }

    }

    public static void CancelDuck()
    {
        instance.ducking = false;
    }

    IEnumerator DuckCoroutine(Rigidbody2D rb)
    {
        Vector3 scale = rb.transform.localScale;
        Vector3 startScale = scale;
        float gravity = rb.gravityScale;
        rb.gravityScale = 100;
        float startScaleY = scale.y;
        float scalePerSession = 4f;
        while (instance.ducking)
        {
            if (scale.y > startScaleY / 4)
            {
                scale.y -= startScaleY / scalePerSession;
                scale.x += startScaleY / (scalePerSession * 2);
                rb.transform.localScale = scale;
            }
            yield return new WaitForSeconds(0.1f);
        }
        rb.gravityScale = gravity;
        while (scale.y < startScaleY)
        {
            yield return new WaitForSeconds(0.1f);
            scale.y += startScaleY / (scalePerSession * 0.5f);
            scale.y += startScaleY / scalePerSession * 1;
            rb.transform.localScale = scale;
        }
        rb.transform.localScale = startScale;
        instance.inCouroutine = false;
    }

    IEnumerator AttackCoroutine(GameObject weapon)
    {
        weapon.SetActive(true);
        Quaternion rotation = weapon.transform.rotation;
        float startRotation = rotation.z;
        rotation *= Quaternion.Euler(0, 0, 75);
        while (rotation.z > startRotation)
        {
            rotation *= Quaternion.Euler(0, 0, -25);
            weapon.transform.rotation = rotation;
            yield return new WaitForSeconds(0.1f);
        }
        rotation.z = startRotation;
        weapon.transform.rotation = rotation;
        weapon.SetActive(false);
        instance.inCouroutine = false;
    }
}

