using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;
    private Board board;
    private bool isFlipped = false;
    private bool isAnimating = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void SetCard(Sprite sprite, Board board)
    {
        frontSprite = sprite;
        this.board = board;
        backSprite = Resources.Load<Sprite>("Sprites/Front/Front"); // 뒷면 기본 이미지
        spriteRenderer.sprite = backSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (board.GetSelectedCardCount() >= 2 || isFlipped || isAnimating)
            return; // 이미 2장이 선택되었으면 클릭 방지

        FlipCard();
        board.SelectCard(this);
    }


    public void FlipCard()
    {
        if (isAnimating) return;
        isAnimating = true;

        Vector3 targetScale = new Vector3(0f, transform.localScale.y, transform.localScale.z);


        transform.DOScale(targetScale, 0.2f).OnComplete(() =>
        {
            spriteRenderer.sprite = isFlipped ? backSprite : frontSprite;
            isFlipped = !isFlipped;

            transform.DOScale(Vector3.one, 0.2f).OnComplete(() =>
            {
                isAnimating = false;
            });
        });


    }

    public void FlipBack()
    {
        if (!isFlipped) return;
        FlipCard();
    }

    public Sprite GetSprite()
    {
        return frontSprite;
    }

    public bool IsFlipped()
    {
        return isFlipped;
    }

}
