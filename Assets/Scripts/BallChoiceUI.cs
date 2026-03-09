using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallChoiceUI : MonoBehaviour
{
    public BallCardUI[] cards; // size = 3
    public BallData[] allBalls;

    System.Action<BallData> onChosen;

    public void Show(System.Action<BallData> callback)
    {
        Debug.Log("Show() called on: " + gameObject.name);
        Debug.Log("Before SetActive: " + gameObject.activeSelf);

        onChosen = callback;
        gameObject.SetActive(true);

        Debug.Log("After SetActive: " + gameObject.activeSelf);

        var rolled = RollUnique(2);

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].Setup(rolled[i], Choose);
        }
        Time.timeScale = 0f;
    }

    void Choose(BallData chosen)
    {
        var list = new List<BallData>(allBalls);
        list.Remove(chosen);
        allBalls = list.ToArray();

        gameObject.SetActive(false);
        onChosen?.Invoke(chosen);
        Time.timeScale = 1f;
    }

    BallData[] RollUnique(int count)
    {
        var pool = new System.Collections.Generic.List<BallData>(allBalls);
        var result = new BallData[count];

        for (int i = 0; i < count; i++)
        {
            int idx = Random.Range(0, pool.Count);
            result[i] = pool[idx];
            pool.RemoveAt(idx);
        }

        return result;
    }
}

