using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000069 RID: 105
	[AddComponentMenu("UI/Selectable", 35)]
	[ExecuteAlways]
	[SelectionBase]
	[DisallowMultipleComponent]
	public class Selectable : UIBehaviour, IMoveHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000138B8 File Offset: 0x00011AB8
		public static Selectable[] allSelectablesArray
		{
			get
			{
				Selectable[] temp = new Selectable[Selectable.s_SelectableCount];
				Array.Copy(Selectable.s_Selectables, temp, Selectable.s_SelectableCount);
				return temp;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x000138E1 File Offset: 0x00011AE1
		public static int allSelectableCount
		{
			get
			{
				return Selectable.s_SelectableCount;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x000138E8 File Offset: 0x00011AE8
		[Obsolete("Replaced with allSelectablesArray to have better performance when disabling a element", false)]
		public static List<Selectable> allSelectables
		{
			get
			{
				return new List<Selectable>(Selectable.allSelectablesArray);
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000138F4 File Offset: 0x00011AF4
		public static int AllSelectablesNoAlloc(Selectable[] selectables)
		{
			int copyCount = ((selectables.Length < Selectable.s_SelectableCount) ? selectables.Length : Selectable.s_SelectableCount);
			Array.Copy(Selectable.s_Selectables, selectables, copyCount);
			return copyCount;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x00013923 File Offset: 0x00011B23
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0001392B File Offset: 0x00011B2B
		public Navigation navigation
		{
			get
			{
				return this.m_Navigation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Navigation>(ref this.m_Navigation, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00013941 File Offset: 0x00011B41
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x00013949 File Offset: 0x00011B49
		public Selectable.Transition transition
		{
			get
			{
				return this.m_Transition;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Selectable.Transition>(ref this.m_Transition, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0001395F File Offset: 0x00011B5F
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x00013967 File Offset: 0x00011B67
		public ColorBlock colors
		{
			get
			{
				return this.m_Colors;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ColorBlock>(ref this.m_Colors, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001397D File Offset: 0x00011B7D
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x00013985 File Offset: 0x00011B85
		public SpriteState spriteState
		{
			get
			{
				return this.m_SpriteState;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<SpriteState>(ref this.m_SpriteState, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001399B File Offset: 0x00011B9B
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x000139A3 File Offset: 0x00011BA3
		public AnimationTriggers animationTriggers
		{
			get
			{
				return this.m_AnimationTriggers;
			}
			set
			{
				if (SetPropertyUtility.SetClass<AnimationTriggers>(ref this.m_AnimationTriggers, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x000139B9 File Offset: 0x00011BB9
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x000139C1 File Offset: 0x00011BC1
		public Graphic targetGraphic
		{
			get
			{
				return this.m_TargetGraphic;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Graphic>(ref this.m_TargetGraphic, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000139D7 File Offset: 0x00011BD7
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x000139E0 File Offset: 0x00011BE0
		public bool interactable
		{
			get
			{
				return this.m_Interactable;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_Interactable, value))
				{
					if (!this.m_Interactable && EventSystem.current != null && EventSystem.current.currentSelectedGameObject == base.gameObject)
					{
						EventSystem.current.SetSelectedGameObject(null);
					}
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00013A38 File Offset: 0x00011C38
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x00013A40 File Offset: 0x00011C40
		private bool isPointerInside { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00013A49 File Offset: 0x00011C49
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x00013A51 File Offset: 0x00011C51
		private bool isPointerDown { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x00013A5A File Offset: 0x00011C5A
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x00013A62 File Offset: 0x00011C62
		private bool hasSelection { get; set; }

		// Token: 0x06000438 RID: 1080 RVA: 0x00013A6C File Offset: 0x00011C6C
		protected Selectable()
		{
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00013AC7 File Offset: 0x00011CC7
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00013AD4 File Offset: 0x00011CD4
		public Image image
		{
			get
			{
				return this.m_TargetGraphic as Image;
			}
			set
			{
				this.m_TargetGraphic = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00013ADD File Offset: 0x00011CDD
		public Animator animator
		{
			get
			{
				return base.GetComponent<Animator>();
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00013AE5 File Offset: 0x00011CE5
		protected override void Awake()
		{
			if (this.m_TargetGraphic == null)
			{
				this.m_TargetGraphic = base.GetComponent<Graphic>();
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00013B04 File Offset: 0x00011D04
		protected override void OnCanvasGroupChanged()
		{
			bool parentGroupAllowsInteraction = this.ParentGroupAllowsInteraction();
			if (parentGroupAllowsInteraction != this.m_GroupsAllowInteraction)
			{
				this.m_GroupsAllowInteraction = parentGroupAllowsInteraction;
				this.OnSetProperty();
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00013B30 File Offset: 0x00011D30
		private bool ParentGroupAllowsInteraction()
		{
			Transform t = base.transform;
			while (t != null)
			{
				t.GetComponents<CanvasGroup>(this.m_CanvasGroupCache);
				for (int i = 0; i < this.m_CanvasGroupCache.Count; i++)
				{
					if (this.m_CanvasGroupCache[i].enabled && !this.m_CanvasGroupCache[i].interactable)
					{
						return false;
					}
					if (this.m_CanvasGroupCache[i].ignoreParentGroups)
					{
						return true;
					}
				}
				t = t.parent;
			}
			return true;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00013BB6 File Offset: 0x00011DB6
		public virtual bool IsInteractable()
		{
			return this.m_GroupsAllowInteraction && this.m_Interactable;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00013BC8 File Offset: 0x00011DC8
		protected override void OnDidApplyAnimationProperties()
		{
			this.OnSetProperty();
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00013BD0 File Offset: 0x00011DD0
		protected override void OnEnable()
		{
			if (this.m_EnableCalled)
			{
				return;
			}
			base.OnEnable();
			if (Selectable.s_SelectableCount == Selectable.s_Selectables.Length)
			{
				Selectable[] temp = new Selectable[Selectable.s_Selectables.Length * 2];
				Array.Copy(Selectable.s_Selectables, temp, Selectable.s_Selectables.Length);
				Selectable.s_Selectables = temp;
			}
			if (EventSystem.current && EventSystem.current.currentSelectedGameObject == base.gameObject)
			{
				this.hasSelection = true;
			}
			this.m_CurrentIndex = Selectable.s_SelectableCount;
			Selectable.s_Selectables[this.m_CurrentIndex] = this;
			Selectable.s_SelectableCount++;
			this.isPointerDown = false;
			this.m_GroupsAllowInteraction = this.ParentGroupAllowsInteraction();
			this.DoStateTransition(this.currentSelectionState, true);
			this.m_EnableCalled = true;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00013C96 File Offset: 0x00011E96
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.OnCanvasGroupChanged();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00013CA4 File Offset: 0x00011EA4
		private void OnSetProperty()
		{
			this.DoStateTransition(this.currentSelectionState, false);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00013CB4 File Offset: 0x00011EB4
		protected override void OnDisable()
		{
			if (!this.m_EnableCalled)
			{
				return;
			}
			Selectable.s_SelectableCount--;
			Selectable.s_Selectables[Selectable.s_SelectableCount].m_CurrentIndex = this.m_CurrentIndex;
			Selectable.s_Selectables[this.m_CurrentIndex] = Selectable.s_Selectables[Selectable.s_SelectableCount];
			Selectable.s_Selectables[Selectable.s_SelectableCount] = null;
			this.InstantClearState();
			base.OnDisable();
			this.m_EnableCalled = false;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00013D22 File Offset: 0x00011F22
		private void OnApplicationFocus(bool hasFocus)
		{
			if (!hasFocus && this.IsPressed())
			{
				this.InstantClearState();
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00013D35 File Offset: 0x00011F35
		protected Selectable.SelectionState currentSelectionState
		{
			get
			{
				if (!this.IsInteractable())
				{
					return Selectable.SelectionState.Disabled;
				}
				if (this.isPointerDown)
				{
					return Selectable.SelectionState.Pressed;
				}
				if (this.hasSelection)
				{
					return Selectable.SelectionState.Selected;
				}
				if (this.isPointerInside)
				{
					return Selectable.SelectionState.Highlighted;
				}
				return Selectable.SelectionState.Normal;
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00013D60 File Offset: 0x00011F60
		protected virtual void InstantClearState()
		{
			string triggerName = this.m_AnimationTriggers.normalTrigger;
			this.isPointerInside = false;
			this.isPointerDown = false;
			this.hasSelection = false;
			switch (this.m_Transition)
			{
			case Selectable.Transition.ColorTint:
				this.StartColorTween(Color.white, true);
				return;
			case Selectable.Transition.SpriteSwap:
				this.DoSpriteSwap(null);
				return;
			case Selectable.Transition.Animation:
				this.TriggerAnimation(triggerName);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00013DC8 File Offset: 0x00011FC8
		protected virtual void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			Color tintColor;
			Sprite transitionSprite;
			string triggerName;
			switch (state)
			{
			case Selectable.SelectionState.Normal:
				tintColor = this.m_Colors.normalColor;
				transitionSprite = null;
				triggerName = this.m_AnimationTriggers.normalTrigger;
				break;
			case Selectable.SelectionState.Highlighted:
				tintColor = this.m_Colors.highlightedColor;
				transitionSprite = this.m_SpriteState.highlightedSprite;
				triggerName = this.m_AnimationTriggers.highlightedTrigger;
				break;
			case Selectable.SelectionState.Pressed:
				tintColor = this.m_Colors.pressedColor;
				transitionSprite = this.m_SpriteState.pressedSprite;
				triggerName = this.m_AnimationTriggers.pressedTrigger;
				break;
			case Selectable.SelectionState.Selected:
				tintColor = this.m_Colors.selectedColor;
				transitionSprite = this.m_SpriteState.selectedSprite;
				triggerName = this.m_AnimationTriggers.selectedTrigger;
				break;
			case Selectable.SelectionState.Disabled:
				tintColor = this.m_Colors.disabledColor;
				transitionSprite = this.m_SpriteState.disabledSprite;
				triggerName = this.m_AnimationTriggers.disabledTrigger;
				break;
			default:
				tintColor = Color.black;
				transitionSprite = null;
				triggerName = string.Empty;
				break;
			}
			switch (this.m_Transition)
			{
			case Selectable.Transition.ColorTint:
				this.StartColorTween(tintColor * this.m_Colors.colorMultiplier, instant);
				return;
			case Selectable.Transition.SpriteSwap:
				this.DoSpriteSwap(transitionSprite);
				return;
			case Selectable.Transition.Animation:
				this.TriggerAnimation(triggerName);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00013F10 File Offset: 0x00012110
		public Selectable FindSelectable(Vector3 dir)
		{
			dir = dir.normalized;
			Vector3 localDir = Quaternion.Inverse(base.transform.rotation) * dir;
			Vector3 pos = base.transform.TransformPoint(Selectable.GetPointOnRectEdge(base.transform as RectTransform, localDir));
			float maxScore = float.NegativeInfinity;
			float maxFurthestScore = float.NegativeInfinity;
			bool wantsWrapAround = this.navigation.wrapAround && (this.m_Navigation.mode == Navigation.Mode.Vertical || this.m_Navigation.mode == Navigation.Mode.Horizontal);
			Selectable bestPick = null;
			Selectable bestFurthestPick = null;
			for (int i = 0; i < Selectable.s_SelectableCount; i++)
			{
				Selectable sel = Selectable.s_Selectables[i];
				if (!(sel == this) && sel.IsInteractable() && sel.navigation.mode != Navigation.Mode.None)
				{
					RectTransform selRect = sel.transform as RectTransform;
					Vector3 selCenter = ((selRect != null) ? selRect.rect.center : Vector3.zero);
					Vector3 myVector = sel.transform.TransformPoint(selCenter) - pos;
					float dot = Vector3.Dot(dir, myVector);
					if (wantsWrapAround && dot < 0f)
					{
						float score = -dot * myVector.sqrMagnitude;
						if (score > maxFurthestScore)
						{
							maxFurthestScore = score;
							bestFurthestPick = sel;
						}
					}
					else if (dot > 0f)
					{
						float score = dot / myVector.sqrMagnitude;
						if (score > maxScore)
						{
							maxScore = score;
							bestPick = sel;
						}
					}
				}
			}
			if (wantsWrapAround && null == bestPick)
			{
				return bestFurthestPick;
			}
			return bestPick;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000140B4 File Offset: 0x000122B4
		private static Vector3 GetPointOnRectEdge(RectTransform rect, Vector2 dir)
		{
			if (rect == null)
			{
				return Vector3.zero;
			}
			if (dir != Vector2.zero)
			{
				dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
			}
			dir = rect.rect.center + Vector2.Scale(rect.rect.size, dir * 0.5f);
			return dir;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00014139 File Offset: 0x00012339
		private void Navigate(AxisEventData eventData, Selectable sel)
		{
			if (sel != null && sel.IsActive())
			{
				eventData.selectedObject = sel.gameObject;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00014158 File Offset: 0x00012358
		public virtual Selectable FindSelectableOnLeft()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnLeft;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.left);
			}
			return null;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000141AC File Offset: 0x000123AC
		public virtual Selectable FindSelectableOnRight()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnRight;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.right);
			}
			return null;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00014200 File Offset: 0x00012400
		public virtual Selectable FindSelectableOnUp()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnUp;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.up);
			}
			return null;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00014254 File Offset: 0x00012454
		public virtual Selectable FindSelectableOnDown()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnDown;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.down);
			}
			return null;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000142A8 File Offset: 0x000124A8
		public virtual void OnMove(AxisEventData eventData)
		{
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				this.Navigate(eventData, this.FindSelectableOnLeft());
				return;
			case MoveDirection.Up:
				this.Navigate(eventData, this.FindSelectableOnUp());
				return;
			case MoveDirection.Right:
				this.Navigate(eventData, this.FindSelectableOnRight());
				return;
			case MoveDirection.Down:
				this.Navigate(eventData, this.FindSelectableOnDown());
				return;
			default:
				return;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001430A File Offset: 0x0001250A
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (this.m_TargetGraphic == null)
			{
				return;
			}
			this.m_TargetGraphic.CrossFadeColor(targetColor, instant ? 0f : this.m_Colors.fadeDuration, true, true);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001433E File Offset: 0x0001253E
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (this.image == null)
			{
				return;
			}
			this.image.overrideSprite = newSprite;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001435C File Offset: 0x0001255C
		private void TriggerAnimation(string triggername)
		{
			if (this.transition != Selectable.Transition.Animation || this.animator == null || !this.animator.isActiveAndEnabled || !this.animator.hasBoundPlayables || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			this.animator.ResetTrigger(this.m_AnimationTriggers.normalTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.highlightedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.pressedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.selectedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.disabledTrigger);
			this.animator.SetTrigger(triggername);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001441D File Offset: 0x0001261D
		protected bool IsHighlighted()
		{
			return this.IsActive() && this.IsInteractable() && (this.isPointerInside && !this.isPointerDown) && !this.hasSelection;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001444C File Offset: 0x0001264C
		protected bool IsPressed()
		{
			return this.IsActive() && this.IsInteractable() && this.isPointerDown;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00014466 File Offset: 0x00012666
		private void EvaluateAndTransitionToSelectionState()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.DoStateTransition(this.currentSelectionState, false);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00014488 File Offset: 0x00012688
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.IsInteractable() && this.navigation.mode != Navigation.Mode.None && EventSystem.current != null)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			}
			this.isPointerDown = true;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000144E1 File Offset: 0x000126E1
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.isPointerDown = false;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000144F9 File Offset: 0x000126F9
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.isPointerInside = true;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00014508 File Offset: 0x00012708
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.isPointerInside = false;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00014517 File Offset: 0x00012717
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.hasSelection = true;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00014526 File Offset: 0x00012726
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.hasSelection = false;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00014535 File Offset: 0x00012735
		public virtual void Select()
		{
			if (EventSystem.current == null || EventSystem.current.alreadySelecting)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}

		// Token: 0x04000205 RID: 517
		protected static Selectable[] s_Selectables = new Selectable[10];

		// Token: 0x04000206 RID: 518
		protected static int s_SelectableCount = 0;

		// Token: 0x04000207 RID: 519
		private bool m_EnableCalled;

		// Token: 0x04000208 RID: 520
		[FormerlySerializedAs("navigation")]
		[SerializeField]
		private Navigation m_Navigation = Navigation.defaultNavigation;

		// Token: 0x04000209 RID: 521
		[FormerlySerializedAs("transition")]
		[SerializeField]
		private Selectable.Transition m_Transition = Selectable.Transition.ColorTint;

		// Token: 0x0400020A RID: 522
		[FormerlySerializedAs("colors")]
		[SerializeField]
		private ColorBlock m_Colors = ColorBlock.defaultColorBlock;

		// Token: 0x0400020B RID: 523
		[FormerlySerializedAs("spriteState")]
		[SerializeField]
		private SpriteState m_SpriteState;

		// Token: 0x0400020C RID: 524
		[FormerlySerializedAs("animationTriggers")]
		[SerializeField]
		private AnimationTriggers m_AnimationTriggers = new AnimationTriggers();

		// Token: 0x0400020D RID: 525
		[Tooltip("Can the Selectable be interacted with?")]
		[SerializeField]
		private bool m_Interactable = true;

		// Token: 0x0400020E RID: 526
		[FormerlySerializedAs("highlightGraphic")]
		[FormerlySerializedAs("m_HighlightGraphic")]
		[SerializeField]
		private Graphic m_TargetGraphic;

		// Token: 0x0400020F RID: 527
		private bool m_GroupsAllowInteraction = true;

		// Token: 0x04000210 RID: 528
		protected int m_CurrentIndex = -1;

		// Token: 0x04000214 RID: 532
		private readonly List<CanvasGroup> m_CanvasGroupCache = new List<CanvasGroup>();

		// Token: 0x0200006A RID: 106
		public enum Transition
		{
			// Token: 0x04000216 RID: 534
			None,
			// Token: 0x04000217 RID: 535
			ColorTint,
			// Token: 0x04000218 RID: 536
			SpriteSwap,
			// Token: 0x04000219 RID: 537
			Animation
		}

		// Token: 0x0200006B RID: 107
		protected enum SelectionState
		{
			// Token: 0x0400021B RID: 539
			Normal,
			// Token: 0x0400021C RID: 540
			Highlighted,
			// Token: 0x0400021D RID: 541
			Pressed,
			// Token: 0x0400021E RID: 542
			Selected,
			// Token: 0x0400021F RID: 543
			Disabled
		}
	}
}
