using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024B RID: 587
	public class FocusController
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x00043C3F File Offset: 0x00041E3F
		public FocusController(IFocusRing focusRing)
		{
			this.focusRing = focusRing;
			this.imguiKeyboardControl = 0;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00043C6A File Offset: 0x00041E6A
		private IFocusRing focusRing { get; }

		// Token: 0x170002EB RID: 747
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x00043C74 File Offset: 0x00041E74
		internal TextElement selectedTextElement
		{
			set
			{
				bool flag = this.m_SelectedTextElement == value;
				if (!flag)
				{
					TextElement selectedTextElement = this.m_SelectedTextElement;
					if (selectedTextElement != null)
					{
						selectedTextElement.selection.SelectNone();
					}
					this.m_SelectedTextElement = value;
				}
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00043CB0 File Offset: 0x00041EB0
		public Focusable focusedElement
		{
			get
			{
				Focusable result = this.GetRetargetedFocusedElement(null);
				return this.IsLocalElement(result) ? result : null;
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00043CD8 File Offset: 0x00041ED8
		public void IgnoreEvent(EventBase evt)
		{
			evt.processedByFocusController = true;
			IMouseEventInternal me = evt as IMouseEventInternal;
			EventBase pe;
			bool flag;
			if (me != null)
			{
				pe = me.sourcePointerEvent as EventBase;
				flag = pe != null;
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				pe.processedByFocusController = true;
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00043D18 File Offset: 0x00041F18
		internal bool IsFocused(Focusable f)
		{
			bool flag = !this.IsLocalElement(f);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				foreach (FocusController.FocusedElement fe in this.m_FocusedElements)
				{
					bool flag3 = fe.m_FocusedElement == f;
					if (flag3)
					{
						return true;
					}
				}
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00043D94 File Offset: 0x00041F94
		internal Focusable GetRetargetedFocusedElement(VisualElement retargetAgainst)
		{
			VisualElement retargetRoot = ((retargetAgainst != null) ? retargetAgainst.hierarchy.parent : null);
			bool flag = retargetRoot == null;
			if (flag)
			{
				bool flag2 = this.m_FocusedElements.Count > 0;
				if (flag2)
				{
					return this.m_FocusedElements[this.m_FocusedElements.Count - 1].m_FocusedElement;
				}
			}
			else
			{
				while (!retargetRoot.isCompositeRoot && retargetRoot.hierarchy.parent != null)
				{
					retargetRoot = retargetRoot.hierarchy.parent;
				}
				foreach (FocusController.FocusedElement fe in this.m_FocusedElements)
				{
					bool flag3 = fe.m_SubTreeRoot == retargetRoot;
					if (flag3)
					{
						return fe.m_FocusedElement;
					}
				}
			}
			return null;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00043E9C File Offset: 0x0004209C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Focusable GetLeafFocusedElement()
		{
			bool flag = this.m_FocusedElements.Count > 0;
			Focusable focusable;
			if (flag)
			{
				VisualElement result = this.m_FocusedElements[0].m_FocusedElement;
				focusable = (this.IsLocalElement(result) ? result : null);
			}
			else
			{
				focusable = null;
			}
			return focusable;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00043EE4 File Offset: 0x000420E4
		private bool IsLocalElement(Focusable f)
		{
			return ((f != null) ? f.focusController : null) == this;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00043F08 File Offset: 0x00042108
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool IsPendingFocus(Focusable f)
		{
			for (VisualElement pending = this.m_LastPendingFocusedElement as VisualElement; pending != null; pending = pending.hierarchy.parent)
			{
				bool flag = f == pending;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00043F50 File Offset: 0x00042150
		internal void SetFocusToLastFocusedElement()
		{
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				this.m_LastFocusedElement.Focus();
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00043F8C File Offset: 0x0004218C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void BlurLastFocusedElement()
		{
			this.selectedTextElement = null;
			bool flag = this.m_LastFocusedElement != null && !(this.m_LastFocusedElement is IMGUIContainer);
			if (flag)
			{
				Focusable tmpLastFocusedElement = this.m_LastFocusedElement;
				this.m_LastFocusedElement.Blur();
				this.m_LastFocusedElement = tmpLastFocusedElement;
			}
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00043FDE File Offset: 0x000421DE
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void DoFocusChange(Focusable f)
		{
			this.m_FocusedElements.Clear();
			FocusController.GetFocusTargets(f, this.m_FocusedElements);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00043FFC File Offset: 0x000421FC
		internal void ProcessPendingFocusChange(Focusable f)
		{
			this.m_PendingFocusCount--;
			bool flag = this.m_PendingFocusCount == 0;
			if (flag)
			{
				this.m_LastPendingFocusedElement = null;
			}
			foreach (FocusController.FocusedElement element in this.m_FocusedElements)
			{
				element.m_FocusedElement.pseudoStates &= ~PseudoStates.Focus;
			}
			this.DoFocusChange(f);
			foreach (FocusController.FocusedElement element2 in this.m_FocusedElements)
			{
				element2.m_FocusedElement.pseudoStates |= PseudoStates.Focus;
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000440E0 File Offset: 0x000422E0
		private static void GetFocusTargets(Focusable f, List<FocusController.FocusedElement> outTargets)
		{
			VisualElement ve = f as VisualElement;
			for (VisualElement subTreeRoot = ve; subTreeRoot != null; subTreeRoot = subTreeRoot.hierarchy.parent)
			{
				bool flag = subTreeRoot.hierarchy.parent == null || subTreeRoot.isCompositeRoot;
				if (flag)
				{
					outTargets.Add(new FocusController.FocusedElement
					{
						m_SubTreeRoot = subTreeRoot,
						m_FocusedElement = ve
					});
					ve = subTreeRoot;
				}
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00044158 File Offset: 0x00042358
		internal Focusable FocusNextInDirection(Focusable currentFocusable, FocusChangeDirection direction)
		{
			Focusable f = this.focusRing.GetNextFocusable(currentFocusable, direction);
			direction.ApplyTo(this, f);
			return f;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00044184 File Offset: 0x00042384
		private void AboutToReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			using (FocusOutEvent e = FocusEventBase<FocusOutEvent>.GetPooled(focusable, willGiveFocusTo, direction, this, false))
			{
				focusable.SendEvent(e, dispatchMode);
			}
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x000441C8 File Offset: 0x000423C8
		private void ReleaseFocus(Focusable focusable, Focusable willGiveFocusTo, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			List<FocusController.FocusedElement> focusedElements;
			using (CollectionPool<List<FocusController.FocusedElement>, FocusController.FocusedElement>.Get(out focusedElements))
			{
				FocusController.GetFocusTargets(focusable, focusedElements);
				foreach (FocusController.FocusedElement f in focusedElements)
				{
					using (BlurEvent e = FocusEventBase<BlurEvent>.GetPooled(f.m_FocusedElement, willGiveFocusTo, direction, this, false))
					{
						e.target = f.m_FocusedElement;
						focusable.SendEvent(e, dispatchMode);
					}
				}
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00044288 File Offset: 0x00042488
		private void AboutToGrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction, DispatchMode dispatchMode)
		{
			using (FocusInEvent e = FocusEventBase<FocusInEvent>.GetPooled(focusable, willTakeFocusFrom, direction, this, false))
			{
				focusable.SendEvent(e, dispatchMode);
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x000442CC File Offset: 0x000424CC
		private void GrabFocus(Focusable focusable, Focusable willTakeFocusFrom, FocusChangeDirection direction, bool bIsFocusDelegated, DispatchMode dispatchMode)
		{
			List<FocusController.FocusedElement> focusedElements;
			using (CollectionPool<List<FocusController.FocusedElement>, FocusController.FocusedElement>.Get(out focusedElements))
			{
				FocusController.GetFocusTargets(focusable, focusedElements);
				foreach (FocusController.FocusedElement f in focusedElements)
				{
					using (FocusEvent e = FocusEventBase<FocusEvent>.GetPooled(f.m_FocusedElement, willTakeFocusFrom, direction, this, bIsFocusDelegated))
					{
						e.target = f.m_FocusedElement;
						focusable.SendEvent(e, dispatchMode);
					}
				}
			}
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00044390 File Offset: 0x00042590
		internal void Blur(Focusable focusable, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			bool ownsFocus = ((this.m_PendingFocusCount > 0) ? this.IsPendingFocus(focusable) : this.IsFocused(focusable));
			bool flag = ownsFocus;
			if (flag)
			{
				this.SwitchFocus(null, bIsFocusDelegated, dispatchMode);
			}
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x000443C9 File Offset: 0x000425C9
		internal void SwitchFocus(Focusable newFocusedElement, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			this.SwitchFocus(newFocusedElement, FocusChangeDirection.unspecified, bIsFocusDelegated, dispatchMode);
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000443DC File Offset: 0x000425DC
		internal void SwitchFocus(Focusable newFocusedElement, FocusChangeDirection direction, bool bIsFocusDelegated = false, DispatchMode dispatchMode = DispatchMode.Default)
		{
			this.m_LastFocusedElement = newFocusedElement;
			Focusable oldFocusedElement = ((this.m_PendingFocusCount > 0) ? this.m_LastPendingFocusedElement : this.GetLeafFocusedElement());
			bool flag = oldFocusedElement == newFocusedElement;
			if (!flag)
			{
				VisualElement newFocusedVe = newFocusedElement as VisualElement;
				bool flag2 = newFocusedVe == null || !newFocusedElement.canGrabFocus || newFocusedVe.panel == null;
				if (flag2)
				{
					VisualElement oldFocusedVe = oldFocusedElement as VisualElement;
					bool flag3 = oldFocusedVe != null;
					if (flag3)
					{
						this.m_LastPendingFocusedElement = null;
						this.m_PendingFocusCount++;
						using (new EventDispatcherGate(oldFocusedVe.panel.dispatcher))
						{
							this.AboutToReleaseFocus(oldFocusedElement, null, direction, dispatchMode);
							this.ReleaseFocus(oldFocusedElement, null, direction, dispatchMode);
						}
					}
				}
				else
				{
					bool flag4 = newFocusedElement != oldFocusedElement;
					if (flag4)
					{
						VisualElement visualElement = newFocusedElement as VisualElement;
						Focusable retargetedNewFocusedElement = ((visualElement != null) ? visualElement.RetargetElement(oldFocusedElement as VisualElement) : null) ?? newFocusedElement;
						VisualElement visualElement2 = oldFocusedElement as VisualElement;
						Focusable retargetedOldFocusedElement = ((visualElement2 != null) ? visualElement2.RetargetElement(newFocusedElement as VisualElement) : null) ?? oldFocusedElement;
						this.m_LastPendingFocusedElement = newFocusedElement;
						this.m_PendingFocusCount++;
						using (new EventDispatcherGate(newFocusedVe.panel.dispatcher))
						{
							bool flag5 = oldFocusedElement != null;
							if (flag5)
							{
								this.AboutToReleaseFocus(oldFocusedElement, retargetedNewFocusedElement, direction, dispatchMode);
							}
							this.AboutToGrabFocus(newFocusedElement, retargetedOldFocusedElement, direction, dispatchMode);
							bool flag6 = oldFocusedElement != null;
							if (flag6)
							{
								this.ReleaseFocus(oldFocusedElement, retargetedNewFocusedElement, direction, dispatchMode);
							}
							this.GrabFocus(newFocusedElement, retargetedOldFocusedElement, direction, bIsFocusDelegated, dispatchMode);
						}
					}
				}
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0004459C File Offset: 0x0004279C
		internal void SwitchFocusOnEvent(Focusable currentFocusable, EventBase e)
		{
			bool processedByFocusController = e.processedByFocusController;
			if (!processedByFocusController)
			{
				using (FocusChangeDirection direction = this.focusRing.GetFocusChangeDirection(currentFocusable, e))
				{
					bool flag = direction != FocusChangeDirection.none;
					if (flag)
					{
						this.FocusNextInDirection(currentFocusable, direction);
						e.processedByFocusController = true;
					}
				}
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00044608 File Offset: 0x00042808
		internal void ReevaluateFocus()
		{
			VisualElement currentFocus = this.focusedElement as VisualElement;
			bool flag = currentFocus != null;
			if (flag)
			{
				bool flag2 = !currentFocus.areAncestorsAndSelfDisplayed || !currentFocus.visible;
				if (flag2)
				{
					currentFocus.Blur();
				}
			}
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0004464C File Offset: 0x0004284C
		internal bool GetFocusableParentForPointerEvent(Focusable target, out Focusable effectiveTarget)
		{
			bool flag = target == null || !target.focusable;
			bool flag2;
			if (flag)
			{
				effectiveTarget = target;
				flag2 = target != null;
			}
			else
			{
				effectiveTarget = target;
				for (;;)
				{
					VisualElement ve = effectiveTarget as VisualElement;
					bool flag3 = ve != null && (!ve.enabledInHierarchy || !ve.focusable || !ve.isEligibleToReceiveFocusFromDisabledChild) && ve.hierarchy.parent != null;
					if (!flag3)
					{
						break;
					}
					effectiveTarget = ve.hierarchy.parent;
				}
				flag2 = !this.IsFocused(effectiveTarget);
			}
			return flag2;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x000446DB File Offset: 0x000428DB
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x000446E3 File Offset: 0x000428E3
		internal int imguiKeyboardControl { get; set; }

		// Token: 0x06000FE6 RID: 4070 RVA: 0x000446EC File Offset: 0x000428EC
		internal void SyncIMGUIFocus(int imguiKeyboardControlID, Focusable imguiContainerHavingKeyboardControl, bool forceSwitch)
		{
			this.imguiKeyboardControl = imguiKeyboardControlID;
			bool flag = forceSwitch || this.imguiKeyboardControl != 0;
			if (flag)
			{
				this.SwitchFocus(imguiContainerHavingKeyboardControl, FocusChangeDirection.unspecified, false, DispatchMode.Default);
			}
			else
			{
				this.SwitchFocus(null, FocusChangeDirection.unspecified, false, DispatchMode.Default);
			}
		}

		// Token: 0x040008E1 RID: 2273
		private TextElement m_SelectedTextElement;

		// Token: 0x040008E2 RID: 2274
		private List<FocusController.FocusedElement> m_FocusedElements = new List<FocusController.FocusedElement>();

		// Token: 0x040008E3 RID: 2275
		private Focusable m_LastFocusedElement;

		// Token: 0x040008E4 RID: 2276
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Focusable m_LastPendingFocusedElement;

		// Token: 0x040008E5 RID: 2277
		private int m_PendingFocusCount = 0;

		// Token: 0x0200024C RID: 588
		private struct FocusedElement
		{
			// Token: 0x040008E7 RID: 2279
			public VisualElement m_SubTreeRoot;

			// Token: 0x040008E8 RID: 2280
			public VisualElement m_FocusedElement;
		}
	}
}
