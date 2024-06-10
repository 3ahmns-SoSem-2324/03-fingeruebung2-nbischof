using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FizzBuzz : MonoBehaviour
{
    public TextMeshProUGUI zufZahl;
    public TextMeshProUGUI[] infoTexts;
    public Image panel;

    private void Start()
    {
        StartCoroutine(ShowInfoTextForSeconds(infoTexts[0], "W drücken, wenn die Zahl durch 3&5 teilbar ist, um FIZZBUZZ zu sagen.", 5f));
    }

    IEnumerator ShowInfoTextForSeconds(TextMeshProUGUI textObject, string text, float seconds)
    {
        textObject.text = text;
        yield return new WaitForSeconds(seconds);
        textObject.text = "";

        int index = System.Array.IndexOf(infoTexts, textObject);
        if (index < infoTexts.Length - 1)
            StartCoroutine(ShowInfoTextForSeconds(infoTexts[index + 1], GetNextMessage(index + 1), 5f));
        else
            PrintRandomNumber();
    }

    string GetNextMessage(int index)
    {
        switch (index)
        {
            case 1: return "S drücken, wenn die Zahl durch 3 teilbar ist, um FIZZ zu sagen.";
            case 2: return "A drücken, wenn die Zahl durch 5 teilbar ist, um BUZZ zu sagen.";
            case 3: return "D drücken, wenn die Zahl durch nichts teilbar ist.";
            default: return "";
        }
    }

    private void GenerateRandomNumber()
    {
        int randomNumber = Random.Range(1, 1000);
        zufZahl.text = randomNumber.ToString();
    }

    public void PrintRandomNumber()
    {
        ChangePanelColor(Color.grey);
        GenerateRandomNumber();

        try
        {
            int number = int.Parse(zufZahl.text);

            if (number % 3 == 0 && number % 5 == 0)
            {
                WaitForInput(KeyCode.W, "FizzBuzz", Color.green);
            }
            else if (number % 3 == 0)
            {
                WaitForInput(KeyCode.S, "Fizz", Color.green);
            }
            else if (number % 5 == 0)
            {
                WaitForInput(KeyCode.A, "Buzz", Color.green);
            }
            else
            {
                WaitForInput(KeyCode.D, "keine Zahl", Color.green);
            }
        }
        catch
        {
            Debug.Log("Leider falscher Input");
            ChangePanelColor(Color.red);
        }
    }

    void ChangePanelColor(Color color)
    {
        panel.color = color;
    }

    private void WaitForInput(KeyCode expectedKey, string message, Color color)
    {
        StartCoroutine(WaitForKeyPress(expectedKey, message, color));
    }

    private IEnumerator WaitForKeyPress(KeyCode expectedKey, string message, Color color)
    {
        float elapsedTime = 0f;
        float waitTime = 10f;

        while (elapsedTime < waitTime)
        {
            if (Input.GetKeyDown(expectedKey))
            {
                Debug.Log(message);
                ChangePanelColor(color);
                yield return new WaitForSeconds(1f);
                PrintRandomNumber();
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Zeit abgelaufen! Korrekte Taste nicht gedrückt.");
        ChangePanelColor(Color.red);
        yield return new WaitForSeconds(1f);
    }
}
