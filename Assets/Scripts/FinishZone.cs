using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
	public float radius;

	private bool canSuccess;

	public GameObject finshObj;

	public GameObject unfinshObj;
	public GameObject lockPrefab;

	private bool locked;

	[Space]
	[Header("selecter")]
	[Space]
	public bool isSlecter;

	public bool forceUnlock;
	public bool isArena = false;

	public int selectIndex = 1;

	public List<Renderer> renderers;

	public Material lockMaterial;

	public TextMesh text;

	public Color lockedTextColor;

	public LevelManager.gameModes gameMode;

	public LevelManager.game3Ctypes game3CType;
	public bool reverseTypeChange = false;

	bool savedSuccessLevel = false;

	private void Start()
	{
		PlayerSaveData loadedData = JsonSave.LoadData<PlayerSaveData>("playerData");

		if (!isSlecter)
		{
			finshObj.SetActive(value: false);
			unfinshObj.SetActive(value: true);
		}
		else if (loadedData.currentLevel < selectIndex && !forceUnlock && !isArena)
		{
			LockedArea();
		}

		if (isArena && loadedData.currentLevel < 7)
		{
			LockedArea();
		}

		if (reverseTypeChange)
		{
			if (loadedData.game3Ctypes == "fps")
			{
				game3CType = LevelManager.game3Ctypes.topDown;
			}
			else
			{
				game3CType = LevelManager.game3Ctypes.fps;
			}
		}
	}

	void LockedArea()
	{
		locked = true;

		if (text != null)
		{
			text.color = lockedTextColor;
		}
		
		for (int i = 0; i < renderers.Count; i++)
		{
			renderers[i].material = lockMaterial;
		}

		if (lockPrefab != null)
		{
			lockPrefab.SetActive(true);
		}
	}

	private void Update()
	{
		if (!isSlecter)
		{
			if (!canSuccess && GameManager.Instance.LevelManager.CheckCanSuccess())
			{
				canSuccess = true;
				finshObj.SetActive(value: true);
				unfinshObj.SetActive(value: false);
			}
			else if (canSuccess && !GameManager.Instance.LevelManager.CheckCanSuccess())
			{
				canSuccess = false;
				finshObj.SetActive(value: false);
				unfinshObj.SetActive(value: true);
			}
			if (canSuccess && Vector3.Distance(GameManager.Instance.LevelManager.Player.transform.position, base.transform.position) < radius)
			{
				PlayerSaveData loadedData = JsonSave.LoadData<PlayerSaveData>("playerData");

				if (!savedSuccessLevel && loadedData.currentLevel == selectIndex)
				{
					loadedData.currentLevel += 1;
					JsonSave.SaveData(loadedData, "playerData");
					savedSuccessLevel = true;
				}

				GameManager.Instance.LevelManager.Success();
			}
		}
		else if (!locked && Vector3.Distance(GameManager.Instance.LevelManager.Player.transform.position, base.transform.position) < radius)
		{
			LoadLevel();
		}

		if (isArena && locked && Vector3.Distance(GameManager.Instance.LevelManager.Player.transform.position, base.transform.position) < radius)
		{
			if (AdsManager.main.rewardedSuccess)
			{
				LoadLevel();
			}
			else
			{
				AdsManager.main.ShowRewarded();
			}
		}
	}

	void LoadLevel()
	{
		PlayerSaveData loadedData = JsonSave.LoadData<PlayerSaveData>("playerData");

		if (selectIndex == GameManager.Instance.maxLevel)
		{
			loadedData.currentLevel = selectIndex;
			selectIndex = 0;
		}

		GameManager.Instance.LevelManager.gameMode = gameMode;
		GameManager.Instance.LevelManager.game3CType = game3CType;
		GameManager.Instance.LevelManager.TryLoadLevel(selectIndex);

		loadedData.game3Ctypes = game3CType.ToString();
		JsonSave.SaveData(loadedData, "playerData");
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(base.transform.position, radius);
	}
}
