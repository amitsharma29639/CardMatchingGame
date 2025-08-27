
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using JTools.Sound.Core;
using JTools.Sound.Core.Constants;
using UnityEngine;
using UnityEngine.U2D;

namespace CardGame.GamePlay
{

    [RequireComponent(typeof(ClickableObject))]
    public class Card : MonoBehaviour
    {
        private Vector3 faceUpRotation = new Vector3(0, 0, 0);
        private Vector3 faceDownRotation = new Vector3(0, 180, 0);

        private const float FLIP_DURATION = 0.2f;

        [SerializeField]
        private SpriteRenderer frontFace;
        private CardData cardData;

        private CardFace currentFace;

        public CardFace CurrentFace
        {
            get => currentFace;
            set => currentFace = value;
        }

        public ClickableObject clickable;

        private List<ICardEventsListner> listeners;

        void Awake()
        {
            clickable = GetComponent<ClickableObject>();
            listeners = new List<ICardEventsListner>();
        }
        void OnEnable()
        {
            clickable.OnClick += OnCardClicked;
        }
        public void Init(CardData data)
        {
            this.cardData = data;
            frontFace.sprite = data.Sprite;
            currentFace = CardFace.backFace;
            transform.localEulerAngles = faceDownRotation;
           
        }

        public void AddListener(ICardEventsListner listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(ICardEventsListner listener)
        {
            listeners.Remove(listener);
        }

        public void ClearListeners()
        {
            listeners.Clear();
        }

        public CardData GetCardData()
        {
            return cardData;
        }

        private void OnCardClicked()
        {
           
            if (currentFace == CardFace.backFace)
            {
                AudioManager.Instance.PlayOneShot(AudioGroupConstants.GAMEPLAYSFX , AudioGroupConstants.FLIP, AudioGroupConstants.GAMEPLAYSFX);
                NotifyCardClicked();
                FlipCard();
            }
        }

        public void FlipCard()
        {
            clickable.enabled = false;
            Vector3 targetRotation = currentFace == CardFace.backFace ? faceUpRotation : faceDownRotation;
            currentFace = currentFace == CardFace.backFace ? CardFace.frontFace : CardFace.backFace;
            DOTween.To(() => transform.localEulerAngles, x => transform.localEulerAngles = x, targetRotation, FLIP_DURATION).onComplete += () =>
            {
                NotifyCardFlipped();
                if (currentFace == CardFace.frontFace)
                {
                    NotifyCardFaceUpAnimationFinished();
                }
                else
                {
                    NotifyCardFaceDownAnimationFinished();
                }

                clickable.enabled = true;
            };

        }

        public void FlipCardForHint()
        {
            clickable.enabled = false;
            DOTween.To(() => transform.localEulerAngles, x => transform.localEulerAngles = x, faceUpRotation, FLIP_DURATION).onComplete += async () =>
            {
                await Task.Delay(1);
                DOTween.To(() => transform.localEulerAngles, x => transform.localEulerAngles = x, faceDownRotation, FLIP_DURATION).onComplete +=  () =>
                {
                    clickable.enabled = true;
                };
            };
        }

        public void SetCardFrontFacing()
        {
            NotifyCardClicked();
            clickable.enabled = false;
            transform.localEulerAngles = faceUpRotation;
            CurrentFace = CardFace.frontFace;
            NotifyCardFaceUpAnimationFinished();
            clickable.enabled = true;
        }

        void OnDisable()
        {
            clickable.OnClick -= OnCardClicked;
            NotifyCardDisableAnimationFinished();
           
        }

        private void NotifyCardClicked()
        {
            foreach (var listener in listeners)
            {
                listener.OnCardClicked(this);
            }
        }

        private void NotifyCardFlipped()
        {
            foreach (var listener in listeners)
            {
                listener.OnCardFlipped(this);
            }
        }

        private void NotifyCardFaceUpAnimationFinished()
        {
            foreach (var listener in listeners)
            {
                listener.OnCardFaceUpAnimationFinished(this);
            }
        }

        private void NotifyCardFaceDownAnimationFinished()
        {
            foreach (var listener in listeners)
            {
                listener.OnCardFaceDownAnimationFinished(this);
            }
        }

        private void NotifyCardDisableAnimationFinished()
        {
            foreach (var listener in listeners)
            {
                listener.OnCardDisableAnimationFinished(this);
            }
        }

    }

    public struct CardData : IEquatable<CardData>
    {
        public int id;
        public string suit;
        public string rank;
        private Sprite sprite;

        public CardData(int id, string suit, string rank, SpriteAtlas atlas)
        {
            this.id = id;
            this.suit = suit;
            this.rank = rank;
            this.sprite = atlas.GetSprite("card_" + suit + "_" + rank);
        }
        
        public Sprite Sprite => sprite;

        public bool Equals(CardData other)
        {
            return this.id != other.id && this.suit == other.suit && this.rank == other.rank ;
        }
    }

    public enum CardFace
    {
        frontFace,
        backFace
    }
}
