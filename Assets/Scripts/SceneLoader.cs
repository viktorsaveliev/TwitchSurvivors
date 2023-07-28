using DG.Tweening;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public enum Scene
    {
        Menu = 1,
        Level
    }

    public void LoadMenu()
    {
        ResetData();
        SceneManager.LoadSceneAsync((int) Scene.Menu, LoadSceneMode.Single);
    }

    public void LoadLevel()
    {
        ResetData();
        SceneManager.LoadSceneAsync((int) Scene.Level, LoadSceneMode.Single);
    }

    private void ResetData()
    {
        DOTween.Clear();
    }
}
