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

        //���ο� �� �ε���
        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2f);
                break;
            }
        }

        //�� Active �Ϸ� Ȯ��
        while (SceneManager.GetActiveScene().name != name)
        {
            yield return null;
            operation.allowSceneActivation = true;
        }

        Debug.Log("Scene Change End");
    }
}
