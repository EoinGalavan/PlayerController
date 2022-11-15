using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ControllerTests
{
    private GameObject control;

    [SetUp]
    public void Setup()
    {
        control = new GameObject();
        control.AddComponent<Rigidbody2D>();
        control.AddComponent<PlayerController>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(control.gameObject);
    }

    [UnityTest]
    public IEnumerator DuckApplies()
    {
        Rigidbody2D rb = control.GetComponent<Rigidbody2D>();
        float controlY = rb.transform.localScale.y;
        PlayerController.Duck(rb);
        yield return new WaitForSeconds(0.2f);
        Assert.Greater(controlY, rb.transform.localScale.y);
    }
    [UnityTest]
    public IEnumerator DuckEnds()
    {
        Rigidbody2D rb = control.GetComponent<Rigidbody2D>();
        float controlY = rb.transform.localScale.y;
        PlayerController.Duck(rb);
        yield return new WaitForSeconds(0.2f);
        PlayerController.CancelDuck();
        yield return new WaitForSeconds(0.2f);
        Assert.AreEqual(controlY, rb.transform.localScale.y);
    }

    [UnityTest]
    public IEnumerator WeaponSets()
    {
        GameObject weapon = new GameObject();
        PlayerController.EstablishWeapon(weapon);
        Assert.IsFalse(weapon.gameObject.activeSelf);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Attack()
    {
        GameObject weapon = new GameObject();
        Quaternion rotation = weapon.transform.rotation;
        PlayerController.Attack(weapon);
        yield return new WaitForSeconds(0.2f);
        Quaternion newRotation = weapon.transform.rotation;
        Assert.IsTrue(weapon.gameObject.activeSelf);
        Assert.Greater(newRotation.z, rotation.z);
    }
}
