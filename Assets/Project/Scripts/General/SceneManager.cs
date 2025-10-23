using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = QualitySettings.vSyncCount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
