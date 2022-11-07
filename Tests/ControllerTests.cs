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
}
