using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Collections;

public static class PlaymodeTestsHelper
{
  private const string targetSceneName = "SampleScene";
  private const string clockRootObjectName = "Clock";

  private static GameObject rootObject;
 

  public static IEnumerator LoadTestSceneCoroutine()
  {
    if (SceneManager.GetActiveScene().name != targetSceneName)
    {
      var loadOp = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Single);
      yield return new WaitUntil(() => loadOp.isDone);
    }
  }

  public static T GetComponentFromChild<T>(string path) where T : Component
  {
    GameObject root = GetRootObject();

    if (root == null)
    {
      Debug.LogError("Root object is not set or not found.");
      return null;
    }

    Transform childTransform = root.transform.Find(path);

    if (childTransform == null)
    {
      Debug.LogError($"Child path '{path}' not found under root '{clockRootObjectName}'.");
      return null;
    }

    T component = childTransform.GetComponent<T>();

    if (component == null)
    {
      Debug.LogError($"Component of type {typeof(T)} not found on object '{childTransform.name}'.");
    }

    return component;
  }

  private static GameObject GetRootObject()
  {
    if (rootObject == null)
    {
      rootObject = GameObject.Find(clockRootObjectName);

      if (rootObject == null)
      {
        Debug.LogError($"Root object '{clockRootObjectName}' not found in scene.");
      }
    }
    return rootObject;
  }
}