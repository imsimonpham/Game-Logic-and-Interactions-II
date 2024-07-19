using UnityEngine;
using UnityEngine.SceneManagement;


public class SimulatedPhysics : MonoBehaviour
{
    private Scene _simulatedScene;
    private PhysicsScene _physicsScence;
    [SerializeField] private Transform _labParent;

    [SerializeField] private LineRenderer _line;
    [SerializeField] private int _maxPhysicsInterations;
    
    void Start()
    {
        _labParent = gameObject.transform.parent.root;
        CreateSimulatedPhysicsScene();
    }

    void CreateSimulatedPhysicsScene()
    {
        _simulatedScene = SceneManager.CreateScene("Simulated Physics", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScence = _simulatedScene.GetPhysicsScene();

        foreach (Transform obstacle in _labParent)
        {
            if (obstacle.CompareTag("Obstacle"))
            {
                var simulatedObstacle = Instantiate(obstacle.gameObject, obstacle.position, obstacle.rotation);
                if (simulatedObstacle.GetComponent<Renderer>() != null)
                {
                    obstacle.GetComponent<Renderer>().enabled = false;
                }
                
                SceneManager.MoveGameObjectToScene(simulatedObstacle, _simulatedScene);
            }
        }
    }

    public void SimulatedTrajectory(AirmailPackage airmailPackagePrefab, Vector3 pos, Vector3 velocity)
    {
        var simulatedObj = Instantiate(airmailPackagePrefab, pos, Quaternion.identity);
        simulatedObj.GetComponent<Renderer>().enabled = false;
        SceneManager.MoveGameObjectToScene(simulatedObj.gameObject, _simulatedScene);
        simulatedObj.Init(velocity);

        _line.positionCount = _maxPhysicsInterations;
        for (int i = 0; i < _maxPhysicsInterations; i++)
        {
            _physicsScence.Simulate(Time.fixedDeltaTime * 3);
            _line.SetPosition(i, simulatedObj.transform.position);
        }
        
        Destroy(simulatedObj.gameObject);
    }
}

