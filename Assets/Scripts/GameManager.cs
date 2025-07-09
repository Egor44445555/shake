using UnityEngine;
// using GamePush;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	private LevelManager levelManager;

	private LayerManager layerManager;

	private ParticleManager particleManager;

	private PhysicsManager physicsManager;

	private PostManager postManager;

	private TimeScaleManager timeScaleManager;

	private BoomManager boomManager;

	private UIManager uiManager;

	private CameraManager cameraManager;

	private bool inited;

	public static GameManager Instance => instance;

	public LevelManager LevelManager => levelManager;

	public LayerManager LayerManager => layerManager;

	public ParticleManager ParticleManager => particleManager;

	public PhysicsManager PhysicsManager => physicsManager;

	public PostManager PostManager => postManager;

	public TimeScaleManager TimeScaleManager => timeScaleManager;

	public BoomManager BoomManager => boomManager;

	public UIManager UIManager => uiManager;

	public CameraManager CameraManager => cameraManager;

	public int currentLevel => levelManager.levelInfo.currentLevelIndex;

	public bool isMobile = false;
	public int maxLevel = 7;

	public void Init()
	{
		isMobile = false;
		Object.DontDestroyOnLoad(base.gameObject);
		instance = this;
		DataManager.SetSavesIndex(0);
		DataManager.Load();
		levelManager = GetComponent<LevelManager>();
		layerManager = GetComponent<LayerManager>();
		particleManager = GetComponent<ParticleManager>();
		physicsManager = GetComponent<PhysicsManager>();
		postManager = GetComponent<PostManager>();
		timeScaleManager = GetComponent<TimeScaleManager>();
		boomManager = GetComponent<BoomManager>();
		uiManager = GetComponent<UIManager>();
		cameraManager = GetComponent<CameraManager>();
		inited = true;
		Application.targetFrameRate = 144;	
	}

	private void Awake()
	{
		if (!inited)
		{
			Init();
		}
	}

	private void Start()
	{
		// if (GP_Device.IsMobile())
		// {
		// 	// isMobile = true;
		// }

		if (!isMobile)
		{
			foreach (VariableJoystick joystick in FindObjectsOfType<VariableJoystick>())
			{
				joystick.gameObject.SetActive(false);
			}
		}
	}

	public void DeleteDatas()
	{
		DataManager.DeleteSaveData();
	}

	public void Kill100()
	{
		for (int i = 0; i < 100; i++)
		{
			DataManager.CountEnemyKilled();
		}
		DataManager.Save();
	}
}
