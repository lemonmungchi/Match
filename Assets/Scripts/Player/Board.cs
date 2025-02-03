using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Sprite[] cardSprites; // ī�� �ո� (1~6�� �̹���)
    private List<Card> cards = new List<Card>();
    private List<Card> selectedCards = new List<Card>(); // ���õ� ī���

    private int rowCount = 5;           //����
    private int colCount = 4;           //����
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
        // Resources �������� "Sprites/0~9" �ε�
        cardSprites = new Sprite[10];
        for (int i = 0; i < 10; i++)
        {
            cardSprites[i] = Resources.Load<Sprite>($"Sprites/Back/{i}");
        }
    }

    public void ShuffleCards()
    {
        List<Sprite> tempSprites = new List<Sprite>();

        // 0~9�� ī�� ���� 2�徿 �߰�
        foreach (var sprite in cardSprites)
        {
            tempSprites.Add(sprite);
            tempSprites.Add(sprite);
        }

        // ���� ����
        tempSprites = tempSprites.OrderBy(x => Random.value).ToList();

        // cards ����Ʈ�� ī�� �߰�
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
                if (index >= cards.Count) return;  // ī�� ���� �ʰ� ����

                // ��ġ ����
                Vector3 pos = new Vector3(xStart + (xSpacing * j), yStart + (ySpacing * i), 0);

                // ������ ������ ī�� ��ü�� ��ġ�� ����
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
        if (selectedCards.Count < 2) return; // �� �� ���õ��� ������ �� �Ұ�

        if (selectedCards[0].GetSprite() == selectedCards[1].GetSprite())
        {
            // ���� ī���� ����
            selectedCards.Clear();
            Managers.Audio.PlaySound("Match");  // ī�� ���߸� ȿ���� ���!
        }
        else
        {
            // �ٸ� ī���� 1�� �� �ٽ� ������
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
