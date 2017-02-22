using System;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text ScoreText;
    [SerializeField] private OfficeGameController _officeGameController;
    public int[] Colums = {5,15,13,10,14};
    public string Separator = " - ";

    // Use this for initialization
    private void Start()
    {
        UpdateScoreBoard();
    }

    public void UpdateScoreBoard()
    {
        ScoreText.text = TableFormat(new[]{"Level","Stamp","Difficulty","Points","Date"});
        ScoreText.text += "\n";
        var level = 1;
        foreach (var levelsScoreRecord in _officeGameController.GetLevelsScoreRecords())
        {
            foreach (var recordPunctuation in levelsScoreRecord.RecordPunctuations.OrderByDescending(x => x.date))
                ScoreText.text += ToString(level, recordPunctuation);
            level++;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateScoreBoard();
    }

    public string ToString(int level, Punctuation punctuation)
    {
        return TableFormat(new[]
        {
            level.ToString(),
            punctuation.timeStamp.ToString("00.000"),
            punctuation.difficulty.ToString(),
            punctuation.points.ToString(),
            FomatDate(punctuation.date)
        });
    }

    private string TableFormat(string [] strings)
    {
        CenterStrings(ref strings);
        return string.Format(FormatString(), strings);
    }

    private void CenterStrings(ref string[] strings)
    {
        for (int i = 0; i < strings.Length; i++)
        {
            strings[i] = XSpace((Colums[i] - strings[i].Length) / 2) + strings[i];

        }
    }

    private string XSpace(int i)
    {
        var spaceString = "";

        for (var j = 0; j < i; j++)
            spaceString += " ";
        return spaceString;
    }

    private string FormatString()
    {
        string format ="";
        int i;
        for (i = 0; i < Colums.Length-1; i++)
            format += "{" + i + ",-" + Colums[i] + "}" + Separator;

        return format + "{" + i + ",-" + Colums[i] + "}" + "\n";
    }

    private string FomatDate(DateTime date)
    {
        var localTime = date.ToLocalTime();
        return localTime.ToString("dd/MM/yy");
    }
}
