using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct Dialogue
{
    public string Speaker;
    public string Content;
    public float Speed;
    public string Left;
    public string Right;
    public string Music;
    public string SoundEffect;
    public string Background;

    public static Dialogue[] ParseFromCSV(TextAsset csvFile)
    {
        List<Dialogue> dialogues = new List<Dialogue>();

        if (csvFile == null || string.IsNullOrEmpty(csvFile.text))
        {
            Debug.LogError("CSV文件无效");
            return new Dialogue[0];
        }

        string[] lines = csvFile.text.Split(
            new[] { "\r\n", "\r", "\n" },
            System.StringSplitOptions.RemoveEmptyEntries
        );

        if (lines.Length < 2) return new Dialogue[0];

        for (int i = 1; i < lines.Length; i++) // 跳过标题行
        {
            string[] fields = SplitCSVLine(lines[i]);

            dialogues.Add(new Dialogue
            {
                Speaker = GetField(fields, 0),
                Content = GetField(fields, 1),
                Speed = ParseFloat(GetField(fields, 2)),
                Left = GetField(fields, 3),
                Right = GetField(fields, 4),
                Music = GetField(fields, 5),
                SoundEffect = GetField(fields, 6),
                Background = GetField(fields, 7)
            });
        }

        return dialogues.ToArray();
    }

    private static string[] SplitCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        System.Text.StringBuilder current = new System.Text.StringBuilder();

        foreach (char c in line)
        {
            if (c == '"')
            {
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                result.Add(current.ToString().Trim());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }
        result.Add(current.ToString().Trim());
        return result.ToArray();
    }

    private static string GetField(string[] fields, int index)
    {
        return (fields.Length > index) ? fields[index] : "";
    }

    private static float ParseFloat(string value)
    {
        if (float.TryParse(value, NumberStyles.Float,
            CultureInfo.InvariantCulture, out float result))
        {
            return result;
        }
        return 1f;
    }
}

public static class DialogueLoader
{
    public static string DialogueID;
    public static void LoadDialogue(string name)
    {
        DialogueID = name;
        SceneManager.LoadScene(3);
    }
}