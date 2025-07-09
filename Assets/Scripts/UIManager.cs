using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager main;

	[SerializeField]
	private Canvas UiCanvas;

	[SerializeField]
	private Image healthBarImage;

	private Material healthBarMat;

	[SerializeField]
	private Text remainCountText;
	[SerializeField] GameObject remainCountBlock;

	[SerializeField]
	private Text targetCountText;

	[SerializeField]
	private GameObject targetCountBlock;


	[SerializeField] public Text XPCount;
	[SerializeField] GameObject XPCountBlock;
	[SerializeField] public Text levelCount;
	[SerializeField] GameObject levelCountBlock;


	public Color redColor;

	public Color greenColor;

	[SerializeField]
	private CanvasGroup defeatCanvasGroup;

	[SerializeField]
	private Text defeatStringText;

	[SerializeField]
	private CanvasGroup successCanvasGroup;

	[SerializeField]
	private AnimationCurve overShowCurve;

	[SerializeField]
	private float overShowTime;

	[SerializeField]
	private GameObject hudPanel;

	private float overShowTimer;

	private Color tempColor;

	private bool overIsSuccess;

	private bool over;

	private void Awake()
	{
		main = this;
		healthBarMat = healthBarImage.material;
	}

	public void Init(Camera _camera)
	{
		UiCanvas.worldCamera = _camera;
		UiCanvas.planeDistance = 1f;
		defeatCanvasGroup.alpha = 0f;
		defeatCanvasGroup.gameObject.SetActive(value: false);
		successCanvasGroup.alpha = 0f;
		successCanvasGroup.gameObject.SetActive(value: false);
		hudPanel.SetActive(value: true);
		over = false;
		overShowTimer = 0f;
	}

	public void UpdateCount(int _teamCount, int _remainCount, int _targetCount)
	{
		remainCountText.text = _remainCount.ToString();
		targetCountText.text = _teamCount.ToString() + "/" + _targetCount.ToString();

		if (_teamCount >= _targetCount)
		{
			targetCountText.color = greenColor;
		}
		else
		{
			targetCountText.color = redColor;
		}
	}

	private void Update()
	{
		remainCountBlock.SetActive(FindObjectOfType<Spawner>() == null);
		targetCountBlock.SetActive(FindObjectOfType<Spawner>() == null);
		XPCountBlock.SetActive(FindObjectOfType<Spawner>() != null);
		levelCountBlock.SetActive(FindObjectOfType<Spawner>() != null);

		if (GameManager.Instance.LevelManager.Player != null)
		{
			healthBarImage.fillAmount = GameManager.Instance.LevelManager.Player.Combat.HealthPercent;
		}

		if (over && overShowTimer < overShowTime)
		{
			overShowTimer += Time.unscaledDeltaTime;
			overShowTimer = Mathf.Min(overShowTimer, overShowTime);
			if (!overIsSuccess)
			{
				defeatCanvasGroup.alpha = overShowCurve.Evaluate(overShowTimer / overShowTime);
			}
			else
			{
				successCanvasGroup.alpha = overShowCurve.Evaluate(overShowTimer / overShowTime);
			}
		}
	}

	public void Defeat(string _defeatString)
	{
		overIsSuccess = false;
		defeatStringText.text = _defeatString;
		hudPanel.SetActive(value: false);
		over = true;
		defeatCanvasGroup.gameObject.SetActive(value: true);
	}

	public void Success()
	{
		overIsSuccess = true;
		hudPanel.SetActive(value: false);
		over = true;
		successCanvasGroup.gameObject.SetActive(value: true);
	}

	public void ChangeTextAlpha(Text _text, float alpha)
	{
		tempColor = _text.color;
		tempColor.a = alpha;
		_text.color = tempColor;
	}
}
