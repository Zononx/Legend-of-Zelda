using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.SceneView;

public class SceneTransition : MonoBehaviour
{
    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public Vector2 cameraNewMax;
    public Vector2 cameraNewMin;
    public VectorValue cameraMin;
    public VectorValue cameraMax;

    [Header("Transition Variables")]
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    private void Awake()
    {
        if(fadeInPanel != null)
        {
            //Instantiate tao ra 1 ban sao cua fadeInPanel, tai vi tri Vector3.zero,
            //giu nguyen huong ban dau ko rotation
            //as GameObject: ep kieu (casting) obj ve kieu GameObject
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel,1); //destroy in 1s
        }
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            if(playerStorage != null)
            {
				playerStorage.initialValue = playerPosition;
				//SceneManager.LoadScene(sceneToLoad);
				StartCoroutine(FadeCo());
			}
            else
            {
                return;
            }
        }
    }
    public IEnumerator FadeCo()
    {
        
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        ResetCameraBounds();
        //Bat dau tai scene moi ma khong lam gian doan tro choi (tro choi khong bi dong bang)
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        //cho doi scene moi chua tai xong (tam dung = null)
        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    public void ResetCameraBounds()
    {
        cameraMax.initialValue = cameraNewMax;
        cameraMin.initialValue = cameraNewMin;
    }
}
