using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;

namespace MDPro3.UI
{
	// Token: 0x0200139D RID: 5021
	[RequireComponent(typeof(Selectable))]
	[RequireComponent(typeof(ElementObjectManager))]
	public class SelectionButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, ISubmitHandler, ISelectHandler, IDeselectHandler, IMoveHandler
	{
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06009110 RID: 37136 RVA: 0x00140980 File Offset: 0x0013EB80
		protected ElementObjectManager Manager
		{
			get
			{
				return this.m_Manager = ((this.m_Manager != null) ? this.m_Manager : base.GetComponent<ElementObjectManager>());
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06009111 RID: 37137 RVA: 0x001409B4 File Offset: 0x0013EBB4
		protected Selectable Selectable
		{
			get
			{
				return this.m_Selectable = ((this.m_Selectable != null) ? this.m_Selectable : base.GetComponent<Selectable>());
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06009112 RID: 37138 RVA: 0x001409E8 File Offset: 0x0013EBE8
		private CanvasGroup Hover
		{
			get
			{
				return this.m_Hover = ((this.m_Hover != null) ? this.m_Hover : this.Manager.GetElement<CanvasGroup>("Hover"));
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06009113 RID: 37139 RVA: 0x00140A24 File Offset: 0x0013EC24
		private DOTweenAnimation HoverAnimation
		{
			get
			{
				return this.m_HoverAnimation = ((this.m_HoverAnimation != null) ? this.m_HoverAnimation : this.Manager.GetElement<DOTweenAnimation>("Hover"));
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06009114 RID: 37140 RVA: 0x00140A60 File Offset: 0x0013EC60
		protected CanvasGroup SelectCursorOffset
		{
			get
			{
				return this.m_SelectCursorOffset = ((this.m_SelectCursorOffset != null) ? this.m_SelectCursorOffset : this.Manager.GetElement<CanvasGroup>("SelectCursorOffset"));
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06009115 RID: 37141 RVA: 0x00140A9C File Offset: 0x0013EC9C
		protected DOTweenAnimation SelectCursorOffsetAnimation
		{
			get
			{
				return this.m_SelectCursorOffsetAnimation = ((this.m_SelectCursorOffsetAnimation != null) ? this.m_SelectCursorOffsetAnimation : this.Manager.GetElement<DOTweenAnimation>("SelectCursorOffset"));
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06009116 RID: 37142 RVA: 0x00140AD8 File Offset: 0x0013ECD8
		private RectTransform Corner
		{
			get
			{
				return this.m_Corner = ((this.m_Corner != null) ? this.m_Corner : this.Manager.GetElement<RectTransform>("Corner"));
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06009117 RID: 37143 RVA: 0x00140B14 File Offset: 0x0013ED14
		private RectTransform Arrow
		{
			get
			{
				return this.m_Arrow = ((this.m_Arrow != null) ? this.m_Arrow : this.Manager.GetElement<RectTransform>("Arrow"));
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06009118 RID: 37144 RVA: 0x00140B50 File Offset: 0x0013ED50
		protected TextMeshProUGUI ButtonText
		{
			get
			{
				return this.m_ButtonText = ((this.m_ButtonText != null) ? this.m_ButtonText : this.Manager.GetElement<TextMeshProUGUI>("ButtonText"));
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06009119 RID: 37145 RVA: 0x00140B8C File Offset: 0x0013ED8C
		protected Image Icon
		{
			get
			{
				return this.m_Icon = ((this.m_Icon != null) ? this.m_Icon : this.Manager.GetElement<Image>("Icon"));
			}
		}

		// Token: 0x0600911A RID: 37146 RVA: 0x00140BC8 File Offset: 0x0013EDC8
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				this.OnClick();
				return;
			}
			if (eventData.button != PointerEventData.InputButton.Right)
			{
				if (eventData.button == PointerEventData.InputButton.Middle)
				{
					UnityEvent onMiddleClick = this.clickEvent.onMiddleClick;
					if (onMiddleClick == null)
					{
						return;
					}
					onMiddleClick.Invoke();
				}
				return;
			}
			UnityEvent onRightClick = this.clickEvent.onRightClick;
			if (onRightClick == null)
			{
				return;
			}
			onRightClick.Invoke();
		}

		// Token: 0x0600911B RID: 37147 RVA: 0x00140C21 File Offset: 0x0013EE21
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.hovering = true;
			this.OnEnter();
		}

		// Token: 0x0600911C RID: 37148 RVA: 0x00140C30 File Offset: 0x0013EE30
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.hovering = false;
			this.pressing = false;
			this.OnExit();
		}

		// Token: 0x0600911D RID: 37149 RVA: 0x00140C46 File Offset: 0x0013EE46
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				this.pressing = true;
				this.OnDown();
			}
		}

		// Token: 0x0600911E RID: 37150 RVA: 0x00140C5D File Offset: 0x0013EE5D
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				this.pressing = false;
				this.OnUp();
			}
		}

		// Token: 0x0600911F RID: 37151 RVA: 0x00140C74 File Offset: 0x0013EE74
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.OnSubmit();
		}

		// Token: 0x06009120 RID: 37152 RVA: 0x00140C7C File Offset: 0x0013EE7C
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.selected = true;
			if (!(eventData is PointerEventData) || !this.hovering)
			{
				this.OnSelect(eventData is AxisEventData || UserInput.NextSelectionIsAxis);
				UserInput.NextSelectionIsAxis = false;
			}
		}

		// Token: 0x06009121 RID: 37153 RVA: 0x00140CB1 File Offset: 0x0013EEB1
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.selected = false;
			this.OnDeselect();
		}

		// Token: 0x06009122 RID: 37154 RVA: 0x00140CC0 File Offset: 0x0013EEC0
		public virtual void OnMove(AxisEventData eventData)
		{
			this.OnNavigation(eventData);
		}

		// Token: 0x06009123 RID: 37155 RVA: 0x00140CC9 File Offset: 0x0013EEC9
		protected virtual void Awake()
		{
			this.SetColor(ColorContainer.SelectMode.Unselected, ColorContainer.StatusMode.Normal, this.Selectable.interactable);
			this.HoverOff(true);
		}

		// Token: 0x06009124 RID: 37156 RVA: 0x00140CE5 File Offset: 0x0013EEE5
		protected virtual void OnDisable()
		{
			this.SetColor(ColorContainer.SelectMode.Unselected, ColorContainer.StatusMode.Normal, this.Selectable.interactable);
		}

		// Token: 0x06009125 RID: 37157 RVA: 0x00140CFC File Offset: 0x0013EEFC
		protected virtual void OnClick()
		{
			AudioManager.PlaySE(this.Selectable.interactable ? this.SoundLabelClick : this.SoundLabelClickInactive, 1f);
			if (this.SetToResponser)
			{
				if (Program.instance.ui_.currentPopupB != null)
				{
					Program.instance.ui_.currentPopupB.lastSelectable = this.m_Selectable;
				}
				else
				{
					Program.instance.currentServant.lastSelectable = this.Selectable;
				}
			}
			UnityEvent onLeftClick = this.clickEvent.onLeftClick;
			if (onLeftClick == null)
			{
				return;
			}
			onLeftClick.Invoke();
		}

		// Token: 0x06009126 RID: 37158 RVA: 0x00140D93 File Offset: 0x0013EF93
		protected virtual void OnSubmit()
		{
			AudioManager.PlaySE(this.Selectable.interactable ? this.SoundLabelClick : this.SoundLabelClickInactive, 1f);
		}

		// Token: 0x06009127 RID: 37159 RVA: 0x00140DBC File Offset: 0x0013EFBC
		protected virtual void OnEnter()
		{
			this.HoverOn();
			AudioManager.PlaySE(this.SoundLabelPointerEnter, 1f);
			this.SetColor(this.selected ? ColorContainer.SelectMode.Selected : ColorContainer.SelectMode.Unselected, ColorContainer.StatusMode.Enter, this.Selectable.interactable);
			if (this.selectedWhenHover && !UserInput.InputFieldActivating())
			{
				this.Selectable.Select();
			}
		}

		// Token: 0x06009128 RID: 37160 RVA: 0x00140E18 File Offset: 0x0013F018
		protected virtual void OnExit()
		{
			if (!Program.Running)
			{
				return;
			}
			if (this.pointerExitThenDeselect && Cursor.lockState == CursorLockMode.None && EventSystem.current.currentSelectedGameObject == base.gameObject)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
			this.HoverOff(false);
			this.SetColor(this.selected ? ColorContainer.SelectMode.Selected : ColorContainer.SelectMode.Unselected, ColorContainer.StatusMode.Normal, this.Selectable.interactable);
		}

		// Token: 0x06009129 RID: 37161 RVA: 0x00140E83 File Offset: 0x0013F083
		protected virtual void OnDown()
		{
			this.SetColor(this.selected ? ColorContainer.SelectMode.Selected : ColorContainer.SelectMode.Unselected, ColorContainer.StatusMode.Down, this.Selectable.interactable);
		}

		// Token: 0x0600912A RID: 37162 RVA: 0x00140EA3 File Offset: 0x0013F0A3
		protected virtual void OnUp()
		{
			this.SetColor(this.selected ? ColorContainer.SelectMode.Selected : ColorContainer.SelectMode.Unselected, this.hovering ? ColorContainer.StatusMode.Enter : ColorContainer.StatusMode.Normal, this.Selectable.interactable);
		}

		// Token: 0x0600912B RID: 37163 RVA: 0x00140ED0 File Offset: 0x0013F0D0
		protected virtual void OnSelect(bool playSE)
		{
			this.HoverOn();
			UnityEvent onSelect = this.selectEvent.onSelect;
			if (onSelect != null)
			{
				onSelect.Invoke();
			}
			if (playSE)
			{
				AudioManager.PlaySE(this.SoundLabelSelectedGamePad, 1f);
			}
			if (this.SetToResponser)
			{
				if (Program.instance.ui_.currentPopupB != null)
				{
					Program.instance.ui_.currentPopupB.lastSelectable = this.Selectable;
				}
				else
				{
					Program.instance.currentServant.lastSelectable = this.Selectable;
				}
			}
			this.SetColor(ColorContainer.SelectMode.Selected, this.hovering ? ColorContainer.StatusMode.Enter : ColorContainer.StatusMode.Normal, this.Selectable.interactable);
		}

		// Token: 0x0600912C RID: 37164 RVA: 0x00140F7A File Offset: 0x0013F17A
		protected virtual void OnDeselect()
		{
			this.HoverOff(false);
			UnityEvent onDeselect = this.selectEvent.onDeselect;
			if (onDeselect != null)
			{
				onDeselect.Invoke();
			}
			this.SetColor(ColorContainer.SelectMode.Unselected, this.hovering ? ColorContainer.StatusMode.Enter : ColorContainer.StatusMode.Normal, this.Selectable.interactable);
		}

		// Token: 0x0600912D RID: 37165 RVA: 0x00140FB8 File Offset: 0x0013F1B8
		protected virtual void HoverOn()
		{
			if (this.hoverd)
			{
				return;
			}
			this.hoverd = true;
			foreach (Tweener tween in this.hoverOffTweens)
			{
				if (tween.IsActive())
				{
					tween.Kill(false);
				}
			}
			this.hoverOffTweens.Clear();
			if (this.SelectCursorOffset != null)
			{
				this.SelectCursorOffset.alpha = 1f;
			}
			if (this.SelectCursorOffsetAnimation != null)
			{
				this.SelectCursorOffsetAnimation.DORestart();
			}
			if (this.Corner != null)
			{
				this.Corner.offsetMin = new Vector2(-16f, -16f);
				this.Corner.offsetMax = new Vector2(16f, 16f);
				this.hoverOnTweens.Add(this.Corner.DOSizeDelta(Vector2.zero, 0.2f, false).SetEase(Ease.OutQuart));
			}
			if (this.Hover != null)
			{
				this.Hover.alpha = 1f;
			}
			if (this.HoverAnimation != null)
			{
				this.HoverAnimation.DORestart();
			}
			if (this.Arrow != null)
			{
				this.Arrow.anchoredPosition3D = new Vector3(-12f, 0f, 0f);
				this.hoverOnTweens.Add(this.Arrow.DOAnchorPos3D(Vector3.zero, 0.3f, false).SetEase(Ease.OutCubic));
			}
			this.CallHoverOnEvent();
		}

		// Token: 0x0600912E RID: 37166 RVA: 0x00141164 File Offset: 0x0013F364
		protected virtual void HoverOff(bool forced = false)
		{
			if (forced && !this.hoverd)
			{
				return;
			}
			this.hoverd = false;
			foreach (Tweener tween in this.hoverOnTweens)
			{
				if (tween.IsActive())
				{
					tween.Kill(false);
				}
			}
			this.hoverOnTweens.Clear();
			if (this.SelectCursorOffsetAnimation != null)
			{
				this.SelectCursorOffsetAnimation.DOPause();
			}
			if (this.SelectCursorOffset != null)
			{
				this.SelectCursorOffset.alpha = 0f;
			}
			if (this.HoverAnimation != null)
			{
				this.HoverAnimation.DOPause();
			}
			if (this.Hover != null)
			{
				this.Hover.alpha = 0f;
			}
			if (this.Arrow != null)
			{
				this.hoverOffTweens.Add(this.Arrow.DOAnchorPos3D(Vector3.zero, 0.3f, false).SetEase(Ease.OutCubic));
			}
			this.CallHoverOffEvent();
		}

		// Token: 0x0600912F RID: 37167 RVA: 0x00141288 File Offset: 0x0013F488
		protected virtual void CallHoverOnEvent()
		{
			UnityEvent onHoverOff = this.hoverEvent.onHoverOff;
			if (onHoverOff == null)
			{
				return;
			}
			onHoverOff.Invoke();
		}

		// Token: 0x06009130 RID: 37168 RVA: 0x00141288 File Offset: 0x0013F488
		protected virtual void CallHoverOffEvent()
		{
			UnityEvent onHoverOff = this.hoverEvent.onHoverOff;
			if (onHoverOff == null)
			{
				return;
			}
			onHoverOff.Invoke();
		}

		// Token: 0x06009131 RID: 37169 RVA: 0x001412A0 File Offset: 0x0013F4A0
		public virtual void SetClickEvent(UnityAction call)
		{
			Button button = this.Selectable as Button;
			if (button != null)
			{
				button.onClick.AddListener(call);
				return;
			}
			this.clickEvent.onLeftClick.AddListener(call);
		}

		// Token: 0x06009132 RID: 37170 RVA: 0x001412DA File Offset: 0x0013F4DA
		public virtual void SetRightClickEvent(UnityAction call)
		{
			this.clickEvent.onRightClick.AddListener(call);
		}

		// Token: 0x06009133 RID: 37171 RVA: 0x001412ED File Offset: 0x0013F4ED
		public virtual void SetMiddleClickEvent(UnityAction call)
		{
			this.clickEvent.onMiddleClick.AddListener(call);
		}

		// Token: 0x06009134 RID: 37172 RVA: 0x00141300 File Offset: 0x0013F500
		public virtual void RemoveAllListeners()
		{
			Button button = this.Selectable as Button;
			if (button != null)
			{
				button.onClick.RemoveAllListeners();
			}
		}

		// Token: 0x06009135 RID: 37173 RVA: 0x00141328 File Offset: 0x0013F528
		public virtual void SetNavigationEvent(MoveDirection direction, UnityAction call)
		{
			this.nonPersistentNavigationEventAdded = true;
			switch (direction)
			{
			case MoveDirection.Left:
				this.navigationEvent.onLeftNavigation.AddListener(call);
				return;
			case MoveDirection.Up:
				this.navigationEvent.onUpNavigation.AddListener(call);
				return;
			case MoveDirection.Right:
				this.navigationEvent.onRightNavigation.AddListener(call);
				return;
			case MoveDirection.Down:
				this.navigationEvent.onDownNavigation.AddListener(call);
				return;
			default:
				return;
			}
		}

		// Token: 0x06009136 RID: 37174 RVA: 0x0014139A File Offset: 0x0013F59A
		public virtual void SetButtonText(string title)
		{
			if (this.ButtonText != null)
			{
				this.ButtonText.text = title;
			}
		}

		// Token: 0x06009137 RID: 37175 RVA: 0x001413B6 File Offset: 0x0013F5B6
		public virtual string GetButtonText()
		{
			if (this.ButtonText != null)
			{
				return this.ButtonText.text;
			}
			return string.Empty;
		}

		// Token: 0x06009138 RID: 37176 RVA: 0x001413D7 File Offset: 0x0013F5D7
		public virtual void SetButtonTextColor(Color color)
		{
			if (this.ButtonText != null)
			{
				this.ButtonText.color = color;
			}
		}

		// Token: 0x06009139 RID: 37177 RVA: 0x001413F3 File Offset: 0x0013F5F3
		public virtual Color GetButtonTextColor()
		{
			if (this.ButtonText != null)
			{
				return this.ButtonText.color;
			}
			return Color.white;
		}

		// Token: 0x0600913A RID: 37178 RVA: 0x00141414 File Offset: 0x0013F614
		public virtual Sprite GetIconSprite()
		{
			if (this.Icon != null)
			{
				return this.Icon.sprite;
			}
			return null;
		}

		// Token: 0x0600913B RID: 37179 RVA: 0x00141431 File Offset: 0x0013F631
		public virtual void SetIconSprite(Sprite sprite)
		{
			if (this.Icon != null)
			{
				this.Icon.sprite = sprite;
			}
		}

		// Token: 0x0600913C RID: 37180 RVA: 0x0014144D File Offset: 0x0013F64D
		public virtual void ShowIcon(bool show)
		{
			if (this.Icon != null)
			{
				this.Icon.gameObject.SetActive(show);
			}
		}

		// Token: 0x0600913D RID: 37181 RVA: 0x0014146E File Offset: 0x0013F66E
		public virtual void SetHoverOnEvent(UnityAction call)
		{
			this.hoverEvent.onHoverOn.AddListener(call);
		}

		// Token: 0x0600913E RID: 37182 RVA: 0x00141481 File Offset: 0x0013F681
		public virtual void SetHoverOffEvent(UnityAction call)
		{
			this.hoverEvent.onHoverOff.AddListener(call);
		}

		// Token: 0x0600913F RID: 37183 RVA: 0x00141494 File Offset: 0x0013F694
		public virtual void SetSelectEvent(UnityAction call)
		{
			this.selectEvent.onSelect.AddListener(call);
		}

		// Token: 0x06009140 RID: 37184 RVA: 0x001414A7 File Offset: 0x0013F6A7
		public virtual void SetDeselectEvent(UnityAction call)
		{
			this.selectEvent.onDeselect.AddListener(call);
		}

		// Token: 0x06009141 RID: 37185 RVA: 0x001414BC File Offset: 0x0013F6BC
		public virtual void SetInteractable(bool active)
		{
			this.Selectable.interactable = active;
			this.SetColor(this.selected ? ColorContainer.SelectMode.Selected : ColorContainer.SelectMode.Unselected, this.hovering ? ColorContainer.StatusMode.Enter : ColorContainer.StatusMode.Normal, active);
			if (this.shortcutIcons == null)
			{
				this.shortcutIcons = base.transform.GetComponentsInChildren<ShortcutIcon>(true);
			}
			ShortcutIcon[] array = this.shortcutIcons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Show = active;
			}
		}

		// Token: 0x06009142 RID: 37186 RVA: 0x0014152C File Offset: 0x0013F72C
		public Selectable GetSelectable()
		{
			return this.Selectable;
		}

		// Token: 0x06009143 RID: 37187 RVA: 0x00141534 File Offset: 0x0013F734
		protected virtual void OnNavigation(AxisEventData eventData)
		{
			if (eventData.moveDir == MoveDirection.Up)
			{
				if (this.navigationEvent.onUpNavigation.GetPersistentEventCount() > 0 || this.nonPersistentNavigationEventAdded)
				{
					this.navigationEvent.onUpNavigation.Invoke();
					return;
				}
			}
			else if (eventData.moveDir == MoveDirection.Down)
			{
				if (this.navigationEvent.onDownNavigation.GetPersistentEventCount() > 0 || this.nonPersistentNavigationEventAdded)
				{
					this.navigationEvent.onDownNavigation.Invoke();
					return;
				}
			}
			else if (eventData.moveDir == MoveDirection.Left)
			{
				if (this.navigationEvent.onLeftNavigation.GetPersistentEventCount() > 0 || this.nonPersistentNavigationEventAdded)
				{
					this.navigationEvent.onLeftNavigation.Invoke();
					return;
				}
			}
			else if (eventData.moveDir == MoveDirection.Right && (this.navigationEvent.onRightNavigation.GetPersistentEventCount() > 0 || this.nonPersistentNavigationEventAdded))
			{
				this.navigationEvent.onRightNavigation.Invoke();
				return;
			}
			if (this.manuallySetNavigation)
			{
				if (eventData.moveDir == MoveDirection.Left && this.Selectable.navigation.selectOnLeft != null && !this.Selectable.navigation.selectOnLeft.gameObject.activeSelf)
				{
					Selectable nextSeletable = this.Selectable.navigation.selectOnLeft;
					while (nextSeletable != null && !nextSeletable.gameObject.activeSelf)
					{
						nextSeletable = this.GetNextSelectable(nextSeletable, MoveDirection.Left);
					}
					if (nextSeletable != null)
					{
						UserInput.NextSelectionIsAxis = true;
						EventSystem.current.SetSelectedGameObject(nextSeletable.gameObject);
						return;
					}
				}
				else if (eventData.moveDir == MoveDirection.Right && this.Selectable.navigation.selectOnRight != null && !this.Selectable.navigation.selectOnRight.gameObject.activeSelf)
				{
					Selectable nextSeletable2 = this.Selectable.navigation.selectOnRight;
					while (nextSeletable2 != null && !nextSeletable2.gameObject.activeSelf)
					{
						nextSeletable2 = this.GetNextSelectable(nextSeletable2, MoveDirection.Right);
					}
					if (nextSeletable2 != null)
					{
						UserInput.NextSelectionIsAxis = true;
						EventSystem.current.SetSelectedGameObject(nextSeletable2.gameObject);
						return;
					}
				}
				else if (eventData.moveDir == MoveDirection.Up && this.Selectable.navigation.selectOnUp != null && !this.Selectable.navigation.selectOnUp.gameObject.activeSelf)
				{
					Selectable nextSeletable3 = this.Selectable.navigation.selectOnUp;
					while (nextSeletable3 != null && !nextSeletable3.gameObject.activeSelf)
					{
						nextSeletable3 = this.GetNextSelectable(nextSeletable3, MoveDirection.Up);
					}
					if (nextSeletable3 != null)
					{
						UserInput.NextSelectionIsAxis = true;
						EventSystem.current.SetSelectedGameObject(nextSeletable3.gameObject);
						return;
					}
				}
				else if (eventData.moveDir == MoveDirection.Down && this.Selectable.navigation.selectOnDown != null && !this.Selectable.navigation.selectOnDown.gameObject.activeSelf)
				{
					Selectable nextSeletable4 = this.Selectable.navigation.selectOnDown;
					while (nextSeletable4 != null && !nextSeletable4.gameObject.activeSelf)
					{
						nextSeletable4 = this.GetNextSelectable(nextSeletable4, MoveDirection.Down);
					}
					if (nextSeletable4 != null)
					{
						UserInput.NextSelectionIsAxis = true;
						EventSystem.current.SetSelectedGameObject(nextSeletable4.gameObject);
						return;
					}
				}
			}
			else
			{
				int selfIndex = this.index;
				if (selfIndex < 0)
				{
					selfIndex = base.transform.GetSiblingIndex();
				}
				int count = this.GetButtonsCount();
				int columes = this.GetColumnsCount();
				int targetIndex = selfIndex + 1;
				if (eventData.moveDir == MoveDirection.Left)
				{
					if (selfIndex % columes == 0)
					{
						this.OnNavigationLeftBorder();
						return;
					}
					targetIndex = selfIndex - 1;
				}
				else if (eventData.moveDir == MoveDirection.Right)
				{
					if (selfIndex % columes == columes - 1 || targetIndex >= count)
					{
						this.OnNavigationRightBorder();
						return;
					}
				}
				else if (eventData.moveDir == MoveDirection.Up)
				{
					targetIndex = selfIndex - columes;
					if (targetIndex < 0)
					{
						this.OnNavigationUpBorder();
						return;
					}
				}
				else if (eventData.moveDir == MoveDirection.Down)
				{
					int lastLineLeft = count % columes;
					int bound = count - lastLineLeft - 1;
					if (lastLineLeft == 0)
					{
						bound -= columes;
					}
					if (selfIndex > bound)
					{
						this.OnNavigationDownBorder();
						return;
					}
					targetIndex = selfIndex + columes;
					if (targetIndex >= count)
					{
						targetIndex = count - 1;
					}
				}
				for (int i = 0; i < base.transform.parent.childCount; i++)
				{
					Transform child = base.transform.parent.GetChild(i);
					SelectionButton selection;
					if (child.gameObject.activeSelf && child.TryGetComponent<SelectionButton>(out selection))
					{
						int buttonIndex = selection.index;
						if (buttonIndex < 0)
						{
							buttonIndex = i;
						}
						if (buttonIndex == targetIndex)
						{
							UserInput.NextSelectionIsAxis = true;
							EventSystem.current.SetSelectedGameObject(base.transform.parent.GetChild(i).gameObject);
							return;
						}
					}
				}
			}
		}

		// Token: 0x06009144 RID: 37188 RVA: 0x00141A08 File Offset: 0x0013FC08
		protected Selectable GetNextSelectable(Selectable selectable, MoveDirection direction)
		{
			Selectable selectable2;
			switch (direction)
			{
			case MoveDirection.Left:
				selectable2 = selectable.navigation.selectOnLeft;
				break;
			case MoveDirection.Up:
				selectable2 = selectable.navigation.selectOnUp;
				break;
			case MoveDirection.Right:
				selectable2 = selectable.navigation.selectOnRight;
				break;
			case MoveDirection.Down:
				selectable2 = selectable.navigation.selectOnDown;
				break;
			default:
				selectable2 = null;
				break;
			}
			return selectable2;
		}

		// Token: 0x06009145 RID: 37189 RVA: 0x0000763C File Offset: 0x0000583C
		protected virtual int GetColumnsCount()
		{
			return 1;
		}

		// Token: 0x06009146 RID: 37190 RVA: 0x00141A74 File Offset: 0x0013FC74
		protected virtual int GetButtonsCount()
		{
			return base.transform.parent.childCount;
		}

		// Token: 0x06009147 RID: 37191 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnNavigationLeftBorder()
		{
		}

		// Token: 0x06009148 RID: 37192 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnNavigationRightBorder()
		{
		}

		// Token: 0x06009149 RID: 37193 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnNavigationUpBorder()
		{
		}

		// Token: 0x0600914A RID: 37194 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnNavigationDownBorder()
		{
		}

		// Token: 0x0600914B RID: 37195 RVA: 0x00141A88 File Offset: 0x0013FC88
		protected void SetColor(ColorContainer.SelectMode selectMode, ColorContainer.StatusMode statusMode, bool interactable)
		{
			if (this.colorContainers == null)
			{
				this.colorContainers = base.transform.GetComponentsInChildren<ColorContainerGraphic>(true);
			}
			ColorContainerGraphic[] array = this.colorContainers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetColor(selectMode, statusMode, interactable);
			}
		}

		// Token: 0x0400CFDD RID: 53213
		private ElementObjectManager m_Manager;

		// Token: 0x0400CFDE RID: 53214
		private Selectable m_Selectable;

		// Token: 0x0400CFDF RID: 53215
		private const string LABEL_CG_HOVER = "Hover";

		// Token: 0x0400CFE0 RID: 53216
		private CanvasGroup m_Hover;

		// Token: 0x0400CFE1 RID: 53217
		private DOTweenAnimation m_HoverAnimation;

		// Token: 0x0400CFE2 RID: 53218
		private const string LABEL_CG_SELECTCURSOROFFSET = "SelectCursorOffset";

		// Token: 0x0400CFE3 RID: 53219
		private CanvasGroup m_SelectCursorOffset;

		// Token: 0x0400CFE4 RID: 53220
		private DOTweenAnimation m_SelectCursorOffsetAnimation;

		// Token: 0x0400CFE5 RID: 53221
		private const string LABEL_RT_CORNER = "Corner";

		// Token: 0x0400CFE6 RID: 53222
		private RectTransform m_Corner;

		// Token: 0x0400CFE7 RID: 53223
		private const string LABEL_RT_ARROW = "Arrow";

		// Token: 0x0400CFE8 RID: 53224
		private RectTransform m_Arrow;

		// Token: 0x0400CFE9 RID: 53225
		private const string LABEL_TXT_BUTTONTEXT = "ButtonText";

		// Token: 0x0400CFEA RID: 53226
		private TextMeshProUGUI m_ButtonText;

		// Token: 0x0400CFEB RID: 53227
		private const string LABEL_IMG_ICON = "Icon";

		// Token: 0x0400CFEC RID: 53228
		private Image m_Icon;

		// Token: 0x0400CFED RID: 53229
		[Header("Selection Button")]
		public int index = -1;

		// Token: 0x0400CFEE RID: 53230
		[SerializeField]
		protected bool SetToResponser = true;

		// Token: 0x0400CFEF RID: 53231
		[SerializeField]
		protected bool manuallySetNavigation = true;

		// Token: 0x0400CFF0 RID: 53232
		[SerializeField]
		protected string SoundLabelClick;

		// Token: 0x0400CFF1 RID: 53233
		[SerializeField]
		protected string SoundLabelClickInactive;

		// Token: 0x0400CFF2 RID: 53234
		[SerializeField]
		protected string SoundLabelPointerEnter;

		// Token: 0x0400CFF3 RID: 53235
		[SerializeField]
		protected string SoundLabelSelectedGamePad;

		// Token: 0x0400CFF4 RID: 53236
		[SerializeField]
		protected SelectionButtonNavigationEvent navigationEvent;

		// Token: 0x0400CFF5 RID: 53237
		[SerializeField]
		protected SelectionButtonHoverEvent hoverEvent;

		// Token: 0x0400CFF6 RID: 53238
		[SerializeField]
		protected SelectionButtonSelectEvent selectEvent;

		// Token: 0x0400CFF7 RID: 53239
		[SerializeField]
		protected SelectionButtonClickEvent clickEvent;

		// Token: 0x0400CFF8 RID: 53240
		protected List<Tweener> hoverOnTweens = new List<Tweener>();

		// Token: 0x0400CFF9 RID: 53241
		protected List<Tweener> hoverOffTweens = new List<Tweener>();

		// Token: 0x0400CFFA RID: 53242
		protected bool hovering;

		// Token: 0x0400CFFB RID: 53243
		protected bool pressing;

		// Token: 0x0400CFFC RID: 53244
		protected bool hoverd;

		// Token: 0x0400CFFD RID: 53245
		public bool selected;

		// Token: 0x0400CFFE RID: 53246
		protected bool clickIsSubmit = true;

		// Token: 0x0400CFFF RID: 53247
		protected bool selectedWhenHover;

		// Token: 0x0400D000 RID: 53248
		protected bool pointerExitThenDeselect = true;

		// Token: 0x0400D001 RID: 53249
		protected bool nonPersistentNavigationEventAdded;

		// Token: 0x0400D002 RID: 53250
		private ShortcutIcon[] shortcutIcons;

		// Token: 0x0400D003 RID: 53251
		private ColorContainerGraphic[] colorContainers;
	}
}
