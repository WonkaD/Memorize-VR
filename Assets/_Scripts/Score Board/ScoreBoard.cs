using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Text _topScoreText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private int[] _colums = { 10, 15, 14, 10, 14};
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
        if (listLevelStatus == null && listLevelStatus.Count < 1 ) return;
        /* Cuando hay mas niveles
        var level = 1;
        foreach (var levelStatus in listLevelStatus)
        {*/
        listLevelStatus[0].Punctuations.Sort();
        TopScores(listLevelStatus);

        var punctuations = listLevelStatus[0].Punctuations;
        if (punctuations.Count > 3)
            foreach (var punctuation in punctuations.GetRange(3, punctuations.Count-3))
                _scoreText.text += ToString(1, punctuation);
            //level++;
       // }
    }

    private void TopScores(List<LevelStatus> listLevelStatus)
    {
        List<Punctuation> punctuations = listLevelStatus[0].Punctuations;
        foreach (var punctuation in punctuations.GetRange(0, VerifyCountMoreThan3(punctuations)))
            _topScoreText.text += ToString(1, punctuation);

    }

    private static int VerifyCountMoreThan3(List<Punctuation> punctuations)
    {
        return punctuations.Count > 3 ? 3 : punctuations.Count;
    }

    private void Header()
    {
        _topScoreText.text = TableFormat(new[] {"Room", "Stamp", "Difficulty", "Points", "Date"}) + "\n";
        _scoreText.text = "";
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
