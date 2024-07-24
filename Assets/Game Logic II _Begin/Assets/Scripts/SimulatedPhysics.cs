using UnityEngine;
using UnityEngine.SceneManagement;


public class SimulatedPhysics : MonoBehaviour
{
    private Scene _simulatedScene;
    private PhysicsScene _physicsScence;
    private Transform _labParent;

    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsIterations;
    [SerializeField] private int _steps;
    
    void Start()
    {
        _labParent = gameObject.transform.parent.root;
        CreateSimulatedPhysicsScene();
    }

    void CreateSimulatedPhysicsScene()
    {
        _simulatedScene = SceneManager.CreateScene("Simulated Physics", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScence = _simulatedScene.GetPhysicsScene();
        MoveObjectsToNewScene();
    }

    void MoveObjectsToNewScene()
    {
        foreach (Transform sceneObject in _labParent)
        {
            if (sceneObject.CompareTag("Obstacle"))
            {
                var simulatedObject = Instantiate(sceneObject.gameObject, sceneObject.position, sceneObject.rotation);
                if (sceneObject.GetComponent<Renderer>() != null)
                {
                    sceneObject.GetComponent<Renderer>().enabled = false;
                }
                
                SceneManager.MoveGameObjectToScene(simulatedObject, _simulatedScene);
            }
        }
    }

    public void CreateSimulatedTrajectory(AirmailPackage airmailPackagePrefab, Vector3 pos, Vector3 velocity)
    {
        var simulatedObj = Instantiate(airmailPackagePrefab, pos, Quaternion.identity);
        if (simulatedObj.GetComponent<Renderer>() != null)
        {
            simulatedObj.GetComponent<Renderer>().enabled = false;
        }
        SceneManager.MoveGameObjectToScene(simulatedObj.gameObject, _simulatedScene);
        simulatedObj.Init(velocity);

        DrawTrajectoryLine(simulatedObj);
        
        Destroy(simulatedObj.gameObject);
    }

    void DrawTrajectoryLine(AirmailPackage simulatedObject)
    {
        _line.positionCount = _maxPhysicsIterations;
        for (int i = 0; i < _maxPhysicsIterations; i++)
        {
            _physicsScence.Simulate(Time.fixedDeltaTime * _steps);
            _line.SetPosition(i, simulatedObject.transform.position);
        }
    }
}

