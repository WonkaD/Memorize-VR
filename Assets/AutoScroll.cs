using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{


    [SerializeField] private Text _scrollText;
    [SerializeField] private RectTransform _viewportTransform;

    private float _scrollTextPreferredHeight;
    private float _viewportSizeDeltaY;
    private Vector2 _originalRectTransformAnchoredPosition;

    // Use this for initialization
    void Start ()
    {
        _originalRectTransformAnchoredPosition = _scrollText.rectTransform.anchoredPosition;
        _viewportSizeDeltaY = _viewportTransform.sizeDelta.y;
        StartCoroutine(autoScroll());
    }

    private void ResetPosition()
    {
        _scrollText.rectTransform.anchoredPosition = _originalRectTransformAnchoredPosition;
    }

    private IEnumerator autoScroll()
    {
        while (true)
        {
            
            _scrollTextPreferredHeight = _scrollText.preferredHeight;

            if (_scrollTextPreferredHeight - _viewportSizeDeltaY <=
                _scrollText.rectTransform.anchoredPosition.y) ResetPosition();
            else MoveUp();
            yield return new WaitForEndOfFrame();
        }

    }

    private void MoveUp()
    {
        _scrollText.rectTransform.anchoredPosition += new Vector2(0, 0.1f); 
    }

    // Update is called once per frame
	void Update ()
	{

	}
}
