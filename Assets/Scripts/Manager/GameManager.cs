using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GameManager
{
    private Board board;
    private List<Card> cards;
    private bool isGameActive = false;
    private float gameTime = 60f; // 총 게임 시간
    private float remainingTime; // 현재 남은 시간

    private Slider timeSlider;
    private Image sliderFill; // 슬라이더의 Fill 색상 변경용
    private TextMeshProUGUI timeText; // 남은 시간을 표시하는 UI
    public event Action<float> OnTimeUpdated;

    public void Init()
    {
        board = GameObject.Find("Board")?.GetComponent<Board>();
        timeSlider = GameObject.Find("TimeOutSlider")?.GetComponent<Slider>();
        timeText = GameObject.Find("TimeOutText")?.GetComponent<TextMeshProUGUI>();

        if (board == null || timeSlider == null || timeText == null)
        {
            Debug.LogError("GameManager 초기화 실패 - 필수 UI 요소가 없음.");
            return;
        }

        // 게임 시작
        isGameActive = true;

        sliderFill = timeSlider.fillRect.GetComponent<Image>();
        remainingTime = gameTime;

        CoroutineHelper.StartCoroutine(StartGameSequence());
    }


    IEnumerator StartGameSequence()
    {
        // 보드가 초기화될 시간을 기다림
        yield return new WaitForSeconds(0.3f);
        cards = board.GetCards();

        // 모든 카드 공개 (처음 1초 동안)
        foreach (var card in cards)
        {
            card.FlipCard();
        }
        yield return new WaitForSeconds(1.5f);

        // 다시 뒤집기
        foreach (var card in cards)
        {
            card.FlipBack();
        }

        yield return new WaitForSeconds(0.3f);

        // 타이머 UI 활성화
        timeSlider.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        Managers.Audio.PlayBGM("BGM");

  
        CoroutineHelper.StartCoroutine(UpdateTimer());
    }

    IEnumerator UpdateTimer()
    {
        while (remainingTime > 0 && isGameActive)
        {
            remainingTime -= Time.deltaTime;
            timeSlider.value = remainingTime;
            OnTimeUpdated?.Invoke(remainingTime); // UI 업데이트 호출

            if (CheckWinCondition())
            {
                GameOver(true);
                yield return new WaitForSeconds(1f);
                yield break;
            }

            yield return null;
        }

        if (remainingTime <= 0)
        {
            GameOver(false);
        }
    }

    private bool CheckWinCondition()
    {
        foreach (var card in board.GetCards())
        {
            if (!card.IsFlipped()) return false;
        }
        return true;
    }

    private void GameOver(bool isWin)
    {
        isGameActive = false;
        Time.timeScale = 0.0f;
        CoroutineHelper.StartCoroutine(GameOverSequence(isWin));
    }

    private IEnumerator GameOverSequence(bool isWin)
    {
        yield return new WaitForSecondsRealtime(0.5f); // 0.5초 딜레이 후 실행

        // DOTween의 모든 트위닝을 제거
        DG.Tweening.DOTween.KillAll();

        if (isWin)
        {
            Managers.UI.ShowPopupUI<UI_Success>();
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_GameOver>();
        }
    }

}
