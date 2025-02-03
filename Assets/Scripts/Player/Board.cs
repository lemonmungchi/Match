using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Sprite[] cardSprites; // 카드 앞면 (1~6번 이미지)
    private List<Card> cards = new List<Card>();
    private List<Card> selectedCards = new List<Card>(); // 선택된 카드들

    private int rowCount = 5;           //세로
    private int colCount = 4;           //가로
    private float xStart = -2.1f;
    private float yStart = 3.3f;
    private float xSpacing = 1.4f;
    private float ySpacing = -1.8f;

    private void Awake()
    {
        cardPrefab = Resources.Load<GameObject>("Prefabs/Card/Card");
        LoadSprites();
        ShuffleCards();
        InitBoard();
    }

    void LoadSprites()
    {
        // Resources 폴더에서 "Sprites/0~9" 로드
        cardSprites = new Sprite[10];
        for (int i = 0; i < 10; i++)
        {
            cardSprites[i] = Resources.Load<Sprite>($"Sprites/Back/{i}");
        }
    }

    public void ShuffleCards()
    {
        List<Sprite> tempSprites = new List<Sprite>();

        // 0~9번 카드 각각 2장씩 추가
        foreach (var sprite in cardSprites)
        {
            tempSprites.Add(sprite);
            tempSprites.Add(sprite);
        }

        // 랜덤 섞기
        tempSprites = tempSprites.OrderBy(x => Random.value).ToList();

        // cards 리스트에 카드 추가
        cards.Clear();
        for (int i = 0; i < tempSprites.Count; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, this.transform);
            Card card = newCard.GetComponent<Card>();
            card.SetCard(tempSprites[i], this);
            cards.Add(card);
        }
    }


    public void InitBoard()
    {
        int index = 0;
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                if (index >= cards.Count) return;  // 카드 개수 초과 방지

                // 위치 설정
                Vector3 pos = new Vector3(xStart + (xSpacing * j), yStart + (ySpacing * i), 0);

                // 기존에 생성된 카드 객체를 위치만 변경
                cards[index++].transform.position = pos;

            }
        }
    }


    public void SelectCard(Card card)
    {
        if (selectedCards.Contains(card) || selectedCards.Count >= 2)
            return;

        selectedCards.Add(card);

        if (selectedCards.Count == 2)
        {
            CheckMatch();
        }
    }

    void CheckMatch()
    {
        if (selectedCards.Count < 2) return; // 두 장 선택되지 않으면 비교 불가

        if (selectedCards[0].GetSprite() == selectedCards[1].GetSprite())
        {
            // 같은 카드라면 유지
            selectedCards.Clear();
            Managers.Audio.PlaySound("Match");  // 카드 맞추면 효과음 재생!
        }
        else
        {
            // 다른 카드라면 1초 후 다시 뒤집기
            Invoke(nameof(ResetCards), 1f);
        }
    }

    void ResetCards()
    {
        foreach (var card in selectedCards)
        {
            card.FlipBack();
        }
        selectedCards.Clear();
    }


    public int GetSelectedCardCount() => selectedCards.Count;
    public List<Card> GetCards() => cards;
}
