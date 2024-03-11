using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerMap8 : MonoBehaviour
{
    public FourXButtonMap8 fourXButtonScriptMap8;
    public Text scoreText;
    public Text highestScoreText;
    public Text unlockText;
    public Text unlockText8; // Reference to the second unlock text
    public HighScoreManager highScoreManager; // Reference to the HighScoreManager
    public GameObject lockedItemImage; // Reference to the first locked image
    public GameObject lockedItemImage9; // Reference to the second locked image
    public Button yourButton9; // Reference to your button

    private int playerScore = 0;
    private int stackedItems = 0;
    private bool isUnlockMessageShowing = false;
    private bool isUnlockMessageShowing8 = false; // Track the second unlock message
    private bool isLockRemoved = false;
    private bool isButtonEnabled = false; // Track whether the button is enabled

    // PlayerPrefs keys
    private const string LockStateKey8 = "LockState8";
    private const string ButtonStateKey8 = "ButtonState8";
    private const string LockedImageStateKey = "LockedImageState";
    private const string LockedImage9StateKey = "LockedImage9State";
    private const string UnlockMessageShownKey = "UnlockMessageShown";
    private const string UnlockMessage8ShownKey = "UnlockMessage8Shown";

    // Custom event to notify score changes
    public event System.Action<int> ScoreChanged;

    private void Start()
    {
        LoadHighestScore();
        UpdateHighestScoreUI();

        // Load the saved states
        isLockRemoved = PlayerPrefs.GetInt(LockStateKey8, 0) == 1;
        isButtonEnabled = PlayerPrefs.GetInt(ButtonStateKey8, 0) == 1;
        bool isLockedImageActive = PlayerPrefs.GetInt(LockedImageStateKey, 1) == 1;
        bool isLockedImage9Active = PlayerPrefs.GetInt(LockedImage9StateKey, 1) == 1;

        // Set the button and locked images according to the saved states
        yourButton9.interactable = isButtonEnabled;
        lockedItemImage.SetActive(isLockedImageActive);
        lockedItemImage9.SetActive(isLockedImage9Active);

        // Check if unlock messages have been shown before
        isUnlockMessageShowing = PlayerPrefs.GetInt(UnlockMessageShownKey, 0) == 1;
        isUnlockMessageShowing8 = PlayerPrefs.GetInt(UnlockMessage8ShownKey, 0) == 1;
    }

    public int GetPlayerScore()
    {
        return playerScore;
    }

    public void IncreaseScore()
    {
        playerScore++;

        if (playerScore > highScoreManager.GetHighestScore())
        {
            highScoreManager.SetHighestScore(playerScore);
            UpdateHighestScoreUI();
        }

        if (playerScore == 3 && !isUnlockMessageShowing)
        {
            // Remove lock images when the player score reaches 3
            RemoveLockImages();
            // Also remove lockedItemImage
            lockedItemImage.SetActive(false);
            PlayerPrefs.SetInt(LockStateKey8, 1);
            PlayerPrefs.SetInt(LockedImageStateKey, 0);
            PlayerPrefs.Save();

            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "";



            // Mark the unlock message as shown
            PlayerPrefs.SetInt(UnlockMessageShownKey, 1);
            PlayerPrefs.Save();

        }
        else if
                  (playerScore == 3)
        {
            StartCoroutine(ShowUnlockMessage());
            unlockText.text = "";
        }
        if (playerScore == 40)
        {
            // Remove lockedItemImage1 when the player score reaches 4
            lockedItemImage9.SetActive(false);
            PlayerPrefs.SetInt(LockedImage9StateKey, 0);
            PlayerPrefs.Save();

            RemoveLockImages(); // Remove any remaining lock images if needed

            if (!isUnlockMessageShowing8)
            {
                StartCoroutine(ShowUnlockMessage1());
                unlockText8.text = "9th Map Is Unlocked";

                // Mark the second unlock message as shown
                PlayerPrefs.SetInt(UnlockMessage8ShownKey, 1);
                PlayerPrefs.Save();
            }
        }

        if (playerScore == 4 && highScoreManager.GetHighestScore() == 4)
        {
            isButtonEnabled = true;
            PlayerPrefs.SetInt(ButtonStateKey8, 1); // Save the button state
        }

        if (isButtonEnabled)
        {
            yourButton9.interactable = true;
        }


        if (fourXButtonScriptMap8.IsButtonActive())
        {
            playerScore += 3;
        }

        playerScore += stackedItems;

        scoreText.text = "Score: " + playerScore.ToString();

        // Notify subscribers of the score change
        ScoreChanged?.Invoke(playerScore);
    }
    public void DecreaseScore()
    {
        playerScore -= 1;
        playerScore += stackedItems;

        scoreText.text = "Score: " + playerScore.ToString();

        // Notify subscribers of the score change
        ScoreChanged?.Invoke(playerScore);
    }

    private void LoadHighestScore()
    {
        highScoreManager.LoadHighestScore();
    }

    private void UpdateHighestScoreUI()
    {
        highestScoreText.text = "Highest Score: " + highScoreManager.GetHighestScore().ToString();
    }

    private System.Collections.IEnumerator ShowUnlockMessage()
    {
        isUnlockMessageShowing = true;
        unlockText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText.gameObject.SetActive(false);
        isUnlockMessageShowing = false;
    }

    private System.Collections.IEnumerator ShowUnlockMessage1()
    {
        isUnlockMessageShowing8 = true;
        unlockText8.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);
        unlockText8.gameObject.SetActive(false);
        isUnlockMessageShowing8 = false;
    }

    private void RemoveLockImages()
    {
        PlayerPrefs.SetInt(LockStateKey8, 1);
        PlayerPrefs.Save();
        isLockRemoved = true;
    }
}