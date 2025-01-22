using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneReference_ExampleMono: MonoBehaviour
{
    [SerializeField] private SceneReference sceneToLoad;
    [SerializeField] private List<SceneReference> sceneList;
    [SerializeField] private bool enableDontDestroy;
    int i = 0;
    void Awake() { if (enableDontDestroy) DontDestroyOnLoad(gameObject); }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && sceneToLoad != null)
        {
            DebugLog(sceneToLoad);
            //if (!enableDontDestroy) Destroy(gameObject);
            SceneManager.LoadScene(sceneToLoad);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (sceneList != null && sceneList.Count > 0)
            {
                DebugLog(sceneList[i]);
                //if (!enableDontDestroy) Destroy(gameObject);
                SceneManager.LoadScene(sceneList[i].SceneBuildIndex);
                i++;
                if (i >= sceneList.Count) i = 0;
            }
        }
    }
    void DebugLog(SceneReference sceneReference) => Debug.Log(
                  "<color=green>path : </color>" + sceneReference.ScenePath +
                  "<color=green>, name : </color>" + sceneReference.SceneFileName +
                  "<color=green>, index : </color>" + sceneReference.SceneBuildIndex);
  
}

