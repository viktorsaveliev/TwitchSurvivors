using UnityEngine.SceneManagement;

public class SceneLoader
{
    public enum Scene
    {
        Menu,
        Level
    }

    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync((int) Scene.Menu, LoadSceneMode.Single);
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync((int)Scene.Level, LoadSceneMode.Single);
    }
}
