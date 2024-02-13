using UnityEngine;
using UnityEngine.UI;

public class TwoXButton : MonoBehaviour
{
    public Text scoreText;
    public Button twoXButton;
    private bool buttonActive = false;
    private float countdown = 30f;

    void Start()
    {
        twoXButton.onClick.AddListener(ActivateTwoX);
    }

    void Update()
    {
        if (buttonActive)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                twoXButton.interactable = false;
                buttonActive = false;
                Destroy(gameObject);
            }
        }
    }

    void ActivateTwoX()
    {
        buttonActive = true;
        twoXButton.interactable = false; // Assuming the button will disappear after activation
    }

    public bool IsButtonActive()
    {
        return buttonActive;
    }

    public void IncreaseScore()
    {
        if (buttonActive)
        {
            // Increase the score by 2 for each successful drop
            // Example: scoreText.text = "Score: " + (int.Parse(scoreText.text.Split(' ')[1]) + 2);
        }
        else
        {
            // Increase the score by 1 for each successful drop
            // Example: scoreText.text = "Score: " + (int.Parse(scoreText.text.Split(' ')[1]) + 1);
        }
    }
}