using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallCardUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text statsText;
    public TMP_Text descText;
    public Button button;

    BallData data;
    System.Action<BallData> onClick;

    public void Setup(BallData ball, System.Action<BallData> clickCallback)
    {
        data = ball;
        onClick = clickCallback;

        icon.sprite = ball.prefab.GetComponent<SpriteRenderer>().sprite;
        nameText.text = ball.ballName;
        //statsText.text = $"DMG {ball.damage}  SPD {ball.speed}";
        //descText.text = ball.description;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Click);
    }

    void Click()
    {
        onClick?.Invoke(data);
    }
}
