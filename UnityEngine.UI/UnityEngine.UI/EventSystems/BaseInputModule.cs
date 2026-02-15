using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UnityEngine.EventSystems
{
	// Token: 0x020000BA RID: 186
	[RequireComponent(typeof(EventSystem))]
	public abstract class BaseInputModule : UIBehaviour
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0001A2A3 File Offset: 0x000184A3
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x0001A2AB File Offset: 0x000184AB
		protected internal bool sendPointerHoverToParent
		{
			get
			{
				return this.m_SendPointerHoverToParent;
			}
			set
			{
				this.m_SendPointerHoverToParent = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001A2B4 File Offset: 0x000184B4
		public BaseInput input
		{
			get
			{
				if (this.m_InputOverride != null)
				{
					return this.m_InputOverride;
				}
				if (this.m_DefaultInput == null)
				{
					foreach (BaseInput baseInput in base.GetComponents<BaseInput>())
					{
						if (baseInput != null && baseInput.GetType() == typeof(BaseInput))
						{
							this.m_DefaultInput = baseInput;
							break;
						}
					}
					if (this.m_DefaultInput == null)
					{
						this.m_DefaultInput = base.gameObject.AddComponent<BaseInput>();
					}
				}
				return this.m_DefaultInput;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001A34B File Offset: 0x0001854B
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0001A353 File Offset: 0x00018553
		public BaseInput inputOverride
		{
			get
			{
				return this.m_InputOverride;
			}
			set
			{
				this.m_InputOverride = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0001A35C File Offset: 0x0001855C
		protected EventSystem eventSystem
		{
			get
			{
				return this.m_EventSystem;
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001A364 File Offset: 0x00018564
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_EventSystem = base.GetComponent<EventSystem>();
			this.m_EventSystem.UpdateModules();
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001A383 File Offset: 0x00018583
		protected override void OnDisable()
		{
			this.m_EventSystem.UpdateModules();
			base.OnDisable();
		}

		// Token: 0x060006DC RID: 1756
		public abstract void Process();

		// Token: 0x060006DD RID: 1757 RVA: 0x0001A398 File Offset: 0x00018598
		protected static RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
		{
			int candidatesCount = candidates.Count;
			for (int i = 0; i < candidatesCount; i++)
			{
				if (!(candidates[i].gameObject == null))
				{
					return candidates[i];
				}
			}
			return default(RaycastResult);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001A3E0 File Offset: 0x000185E0
		protected static MoveDirection DetermineMoveDirection(float x, float y)
		{
			return BaseInputModule.DetermineMoveDirection(x, y, 0.6f);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001A3F0 File Offset: 0x000185F0
		protected static MoveDirection DetermineMoveDirection(float x, float y, float deadZone)
		{
			if (new Vector2(x, y).sqrMagnitude < deadZone * deadZone)
			{
				return MoveDirection.None;
			}
			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				if (x <= 0f)
				{
					return MoveDirection.Left;
				}
				return MoveDirection.Right;
			}
			else
			{
				if (y <= 0f)
				{
					return MoveDirection.Down;
				}
				return MoveDirection.Up;
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001A438 File Offset: 0x00018638
		protected static GameObject FindCommonRoot(GameObject g1, GameObject g2)
		{
			if (g1 == null || g2 == null)
			{
				return null;
			}
			Transform t = g1.transform;
			while (t != null)
			{
				Transform t2 = g2.transform;
				while (t2 != null)
				{
					if (t == t2)
					{
						return t.gameObject;
					}
					t2 = t2.parent;
				}
				t = t.parent;
			}
			return null;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001A49C File Offset: 0x0001869C
		protected void HandlePointerExitAndEnter(PointerEventData currentPointerData, GameObject newEnterTarget)
		{
			if (newEnterTarget == null || currentPointerData.pointerEnter == null)
			{
				int hoveredCount = currentPointerData.hovered.Count;
				for (int i = 0; i < hoveredCount; i++)
				{
					currentPointerData.fullyExited = true;
					ExecuteEvents.Execute<IPointerMoveHandler>(currentPointerData.hovered[i], currentPointerData, ExecuteEvents.pointerMoveHandler);
					ExecuteEvents.Execute<IPointerExitHandler>(currentPointerData.hovered[i], currentPointerData, ExecuteEvents.pointerExitHandler);
				}
				currentPointerData.hovered.Clear();
				if (newEnterTarget == null)
				{
					currentPointerData.pointerEnter = null;
					return;
				}
			}
			if (currentPointerData.pointerEnter == newEnterTarget && newEnterTarget)
			{
				if (currentPointerData.IsPointerMoving())
				{
					int hoveredCount2 = currentPointerData.hovered.Count;
					for (int j = 0; j < hoveredCount2; j++)
					{
						ExecuteEvents.Execute<IPointerMoveHandler>(currentPointerData.hovered[j], currentPointerData, ExecuteEvents.pointerMoveHandler);
					}
				}
				return;
			}
			GameObject commonRoot = BaseInputModule.FindCommonRoot(currentPointerData.pointerEnter, newEnterTarget);
			Component component = (Component)newEnterTarget.GetComponentInParent<IPointerExitHandler>();
			GameObject pointerParent = ((component != null) ? component.gameObject : null);
			if (currentPointerData.pointerEnter != null)
			{
				Transform t = currentPointerData.pointerEnter.transform;
				while (t != null && (!this.m_SendPointerHoverToParent || !(commonRoot != null) || !(commonRoot.transform == t)) && (this.m_SendPointerHoverToParent || !(pointerParent == t.gameObject)))
				{
					currentPointerData.fullyExited = t.gameObject != commonRoot && currentPointerData.pointerEnter != newEnterTarget;
					ExecuteEvents.Execute<IPointerMoveHandler>(t.gameObject, currentPointerData, ExecuteEvents.pointerMoveHandler);
					ExecuteEvents.Execute<IPointerExitHandler>(t.gameObject, currentPointerData, ExecuteEvents.pointerExitHandler);
					currentPointerData.hovered.Remove(t.gameObject);
					if (this.m_SendPointerHoverToParent)
					{
						t = t.parent;
					}
					if (commonRoot != null && commonRoot.transform == t)
					{
						break;
					}
					if (!this.m_SendPointerHoverToParent)
					{
						t = t.parent;
					}
				}
			}
			GameObject oldPointerEnter = currentPointerData.pointerEnter;
			currentPointerData.pointerEnter = newEnterTarget;
			if (newEnterTarget != null)
			{
				Transform t2 = newEnterTarget.transform;
				while (t2 != null)
				{
					currentPointerData.reentered = t2.gameObject == commonRoot && t2.gameObject != oldPointerEnter;
					if (this.m_SendPointerHoverToParent && currentPointerData.reentered)
					{
						break;
					}
					ExecuteEvents.Execute<IPointerEnterHandler>(t2.gameObject, currentPointerData, ExecuteEvents.pointerEnterHandler);
					ExecuteEvents.Execute<IPointerMoveHandler>(t2.gameObject, currentPointerData, ExecuteEvents.pointerMoveHandler);
					currentPointerData.hovered.Add(t2.gameObject);
					if (!this.m_SendPointerHoverToParent && t2.gameObject.GetComponent<IPointerEnterHandler>() != null)
					{
						break;
					}
					if (this.m_SendPointerHoverToParent)
					{
						t2 = t2.parent;
					}
					if (commonRoot != null && commonRoot.transform == t2)
					{
						break;
					}
					if (!this.m_SendPointerHoverToParent)
					{
						t2 = t2.parent;
					}
				}
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001A7A4 File Offset: 0x000189A4
		protected virtual AxisEventData GetAxisEventData(float x, float y, float moveDeadZone)
		{
			if (this.m_AxisEventData == null)
			{
				this.m_AxisEventData = new AxisEventData(this.eventSystem);
			}
			this.m_AxisEventData.Reset();
			this.m_AxisEventData.moveVector = new Vector2(x, y);
			this.m_AxisEventData.moveDir = BaseInputModule.DetermineMoveDirection(x, y, moveDeadZone);
			return this.m_AxisEventData;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001A800 File Offset: 0x00018A00
		protected virtual BaseEventData GetBaseEventData()
		{
			if (this.m_BaseEventData == null)
			{
				this.m_BaseEventData = new BaseEventData(this.eventSystem);
			}
			this.m_BaseEventData.Reset();
			return this.m_BaseEventData;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000093DE File Offset: 0x000075DE
		public virtual bool IsPointerOverGameObject(int pointerId)
		{
			return false;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001A82C File Offset: 0x00018A2C
		public virtual bool ShouldActivateModule()
		{
			return base.enabled && base.gameObject.activeInHierarchy;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void DeactivateModule()
		{
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void ActivateModule()
		{
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00002209 File Offset: 0x00000409
		public virtual void UpdateModule()
		{
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000D133 File Offset: 0x0000B333
		public virtual bool IsModuleSupported()
		{
			return true;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001A843 File Offset: 0x00018A43
		public virtual int ConvertUIToolkitPointerId(PointerEventData sourcePointerData)
		{
			if (sourcePointerData.pointerId >= 0)
			{
				return PointerId.touchPointerIdBase + sourcePointerData.pointerId;
			}
			return PointerId.mousePointerId;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001A860 File Offset: 0x00018A60
		public virtual Vector2 ConvertPointerEventScrollDeltaToTicks(Vector2 scrollDelta)
		{
			return scrollDelta / this.input.mouseScrollDeltaPerTick;
		}

		// Token: 0x04000316 RID: 790
		[NonSerialized]
		protected List<RaycastResult> m_RaycastResultCache = new List<RaycastResult>();

		// Token: 0x04000317 RID: 791
		[SerializeField]
		private bool m_SendPointerHoverToParent = true;

		// Token: 0x04000318 RID: 792
		private AxisEventData m_AxisEventData;

		// Token: 0x04000319 RID: 793
		private EventSystem m_EventSystem;

		// Token: 0x0400031A RID: 794
		private BaseEventData m_BaseEventData;

		// Token: 0x0400031B RID: 795
		protected BaseInput m_InputOverride;

		// Token: 0x0400031C RID: 796
		private BaseInput m_DefaultInput;
	}
}
