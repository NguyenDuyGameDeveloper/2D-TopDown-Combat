public class SceneManagement : Singleton<SceneManagement>
{
    public string SceneTransitionName { get; private set; }

    public void SetTransitionName(string _sceneTransitionName)
    {
        this.SceneTransitionName = _sceneTransitionName;
    }
}
