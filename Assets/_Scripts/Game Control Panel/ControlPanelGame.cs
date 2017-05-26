using System;
using System.Linq;
using Assets.Scripts.GamesControllers;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelGame : MonoBehaviour
{
    [SerializeField] private RoomGame _roomGame;
    [SerializeField] private Text _levelText;
    private EnumLevels _difficulty = EnumLevels.Easy;
    private Animator _animator;

    public EnumLevels Difficulty
    {
        get { return _difficulty; }

        set
        {
            _difficulty = value;
            _levelText.text = _difficulty.ToString();
        }
    }

    public void Awake()
    {
        _levelText.text = _difficulty.ToString();
        _animator = gameObject.GetComponent<Animator>();
    }

    public void StartGame()
    {
        _roomGame.StartGame(_difficulty);
    }

    public void FinishGame()
    {
        _roomGame.AbortGame();
    }

    public void OpenPanel()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("ClosedState")) _animator.SetTrigger("Action");
        
    }

    public void ClosePanel()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("OpenState")) _animator.SetTrigger("Action");
    }

    public void LevelUp()
    {
        Difficulty = Enum.IsDefined(typeof(EnumLevels), (int) _difficulty + 1)
            ? (EnumLevels) ((int) _difficulty + 1)
            : 0;
    }

    public void LevelDown()
    {
        Difficulty = Enum.IsDefined(typeof(EnumLevels), (int) _difficulty - 1)
            ? (EnumLevels) ((int) _difficulty - 1)
            : LastEnum();
    }

    private static EnumLevels LastEnum()
    {
        return Enum.GetValues(typeof(EnumLevels)).Cast<EnumLevels>().Last();
    }
}