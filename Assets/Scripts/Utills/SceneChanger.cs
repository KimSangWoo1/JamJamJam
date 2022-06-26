using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : Singleton<SceneChanger>
{

    public void SceneChange(string name)
    {
        StartCoroutine(LoadingScene(name));
    }

    private IEnumerator LoadingScene(string name)
    {
        Debug.Log("Scene Change Start");

        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        operation.allowSceneActivation = false;

        //새로운 씬 로딩중
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2f);
                break;
            }
        }

        //씬 Active 완료 확인
        while (SceneManager.GetActiveScene().name != name)
        {
            yield return null;
            operation.allowSceneActivation = true;
        }

        Debug.Log("Scene Change End");
    }
}
