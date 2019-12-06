using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
        private set { _instance = value; }
    }

    void Awake()
    {
        // don't fucking move this check 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public int levelIndex
    {
        get => PlayerPrefs.GetInt("level_index", 0);
        set => PlayerPrefs.SetInt("level_index", value);
    }
    [HideInInspector]
    public float blockDestroyDelay = 0f;

    [SerializeField] CameraController cameraController;
    [SerializeField] Camera mainCam;
    [SerializeField] BlockCreator blockCreator;
    [SerializeField] LayerMask blockLayerMask;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] Transform ballSpawnPoint;
    [SerializeField] float ballShootForce = 5f;
    [SerializeField] GameObject dummyBall;
    [SerializeField] GameObject dummyBomb;
    [SerializeField] UiManager uiManager;
    [SerializeField] int[] scoreLimits;
    [SerializeField] int defaultBallCount = 15;

    bool canShoot = false;
    Color prevBallColor = Color.black;
    int score = 0;
    Color acitveColor;
    int ballCount;
    int enableBombCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }

    void StartLevel()
    {
        enableBombCount = 0;
        ballCount = defaultBallCount - levelIndex;
        score = 0;
        dummyBall.SetActive(false);
        blockCreator.Setup();
        cameraController.OnMoveToStartCompleted += CamSetupCompleted;
        uiManager.SetLevelText(levelIndex);
        uiManager.UpdateBallCountText(ballCount);
    }

    void Update()
    {
        if (canShoot && Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayerMask))
            {
                Shoot(hit);
            }
        }
    }

    void Shoot(RaycastHit _hit)
    {
        blockDestroyDelay = 0f;
        GameObject shootPrefab = ballPrefab;
        if (enableBombCount == 1)
        {
            print("bomb enabled");
            enableBombCount = 2;
            shootPrefab = bombPrefab;
            uiManager.StopBombAnimation();
        }

        Rigidbody ballRBD = Instantiate(shootPrefab,
                    ballSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        Vector3 shootDir = _hit.transform.position - ballSpawnPoint.position;
        ballRBD.isKinematic = false;
        ballRBD.AddForce(shootDir.normalized * ballShootForce, ForceMode.Impulse);
        Renderer brr = ballRBD.transform.gameObject.GetComponent<Renderer>();
        if (brr != null)
        {
            Material dummyballMaterial = dummyBall.GetComponent<Renderer>().material;
            brr.material.CopyPropertiesFromMaterial(dummyballMaterial);
            brr.material.color = dummyballMaterial.color;
            brr.material.SetColor("_EmissionColor", dummyballMaterial.color * 0.4f);
        }
        if (ballRBD.gameObject != null)
            Destroy(ballRBD.gameObject, 5f);
        RefreshDummyBall();

        // reduce the ball count
        ballCount--;
        uiManager.UpdateBallCountText(ballCount);
        if (ballCount <= 0)
        {
            uiManager.ShowTryAgainPopup();
            Invoke("RestartLevel", 2f);
        }
    }

    void CamSetupCompleted()
    {
        cameraController.OnMoveToStartCompleted -= CamSetupCompleted;
        print("cam setup completed.");
        blockCreator.CamSetupCompleted();
        canShoot = true;
        cameraController.canSwipe = true;
        uiManager.ShowProgressbarGroup();
        RefreshDummyBall();
    }

    void RefreshDummyBall()
    {
        ShowDummyBall();
        Vector3 endValue = dummyBall.transform.localScale;
        dummyBall.transform.localScale = Vector3.zero;
        dummyBall.transform.DOScale(endValue, 0.8f).SetEase(Ease.OutElastic);

        // dummyBall.transform.DOPunchScale(new Vector3(-0.21f, -0.21f, -0.21f), 0.2f, 5, 1f);

        // set dummy color
        Renderer drr = dummyBall.GetComponent<Renderer>();
        drr.material.CopyPropertiesFromMaterial(blockCreator.GetBlockMaterial());
        Color[] cp = blockCreator.GetActiveColorPalette();
        // assign the random color 
        Color randomColor = cp[Random.Range(0, cp.Length)];
        while (prevBallColor.Equals(randomColor))
        {
            randomColor = cp[Random.Range(0, cp.Length)];
        }
        prevBallColor = randomColor;
        drr.material.color = randomColor;
        acitveColor = randomColor;
        uiManager.SetBgColor(acitveColor);
        drr.material.SetColor("_EmissionColor", randomColor * 0.4f);
    }

    public void BlockReachedDeadZone(Collider _other)
    {
        Destroy(_other.gameObject);
        UpdateScore();
    }

    public void UpdateScore(int _value = 1)
    {
        score += _value;


        if (score < scoreLimits[levelIndex])
        {
            float pval = (float)score / (float)scoreLimits[levelIndex];
            uiManager.UpdateProgressbar(pval);
            // bomb powerup 
            if (enableBombCount <= 0 && pval >= 0.5f)
            {
                enableBombCount = 1;
                uiManager.AnimateBomb();
                ShowDummyBomb();
            }
        }
        else
        {
            levelIndex++;
            if (levelIndex >= 6)
                levelIndex = 0;
            uiManager.LevelCompleted();
            Invoke("RestartLevel", 2f);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(Helper.GAME_SCENE_NAME);
    }

    public void ShowDummyBall()
    {
        dummyBall.SetActive(true);
        dummyBomb.SetActive(false);
    }

    public void ShowDummyBomb()
    {
        dummyBall.SetActive(false);
        dummyBomb.SetActive(true);
    }
}
