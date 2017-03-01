using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private int[] _colums = {5, 15, 13, 10, 14};
    [SerializeField] private readonly string _separator = " - ";

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void UpdateScoreBoard(List<LevelStatus> listLevelStatus)
    {
        Header();
        if (listLevelStatus == null) return;
        var level = 1;
        foreach (var levelStatus in listLevelStatus)
        {
            foreach (var punctuations in levelStatus.Punctuations.OrderByDescending(x => x.date))
                _scoreText.text += ToString(level, punctuations);
            level++;
        }
    }

    private void Header()
    {
        _scoreText.text = TableFormat(new[] {"Difficulty", "Stamp", "Difficulty", "Points", "Date"}) + "\n";
    }


    private string ToString(int level, Punctuation punctuation)
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

    private string TableFormat(string[] strings)
    {
        CenterStrings(ref strings);
        return string.Format(FormatString(), strings);
    }

    private void CenterStrings(ref string[] strings)
    {
        for (var i = 0; i < strings.Length; i++)
            strings[i] = X_Spaces(CalculateNumberOfSpaces(strings, i)) + strings[i];
    }

    private int CalculateNumberOfSpaces(string[] strings, int i)
    {
        return (_colums[i] - strings[i].Length) / 2;
    }

    private string X_Spaces(int i)
    {
        var spaceString = "";
        for (var j = 0; j < i; j++) spaceString += " ";
        return spaceString;
    }

    private string FormatString()
    {
        var format = "";
        int i;
        for (i = 0; i < _colums.Length - 1; i++)
            format += "{" + i + ",-" + _colums[i] + "}" + _separator;

        return format + "{" + i + ",-" + _colums[i] + "}" + "\n";
    }

    private static string FomatDate(DateTime date)
    {
        var localTime = date.ToLocalTime();
        return localTime.ToString("dd/MM/yy");
    }
}
