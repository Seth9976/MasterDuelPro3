using System;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000248 RID: 584
	public abstract class Focusable : CallbackEventHandler
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x000438B0 File Offset: 0x00041AB0
		protected Focusable()
		{
			UIElementsRuntimeUtilityNative.VisualElementCreation();
			this.focusable = true;
			this.tabIndex = 0;
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000FAD RID: 4013
		public abstract FocusController focusController { get; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x000438D7 File Offset: 0x00041AD7
		// (set) Token: 0x06000FAF RID: 4015 RVA: 0x000438E0 File Offset: 0x00041AE0
		[CreateProperty]
		public virtual bool focusable
		{
			get
			{
				return this.m_Focusable;
			}
			set
			{
				bool flag = this.m_Focusable == value;
				if (!flag)
				{
					this.m_Focusable = value;
					base.NotifyPropertyChanged(in Focusable.focusableProperty);
				}
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00043910 File Offset: 0x00041B10
		// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x00043918 File Offset: 0x00041B18
		[CreateProperty]
		public int tabIndex
		{
			get
			{
				return this.m_TabIndex;
			}
			set
			{
				bool flag = this.m_TabIndex == value;
				if (!flag)
				{
					this.m_TabIndex = value;
					base.NotifyPropertyChanged(in Focusable.tabIndexProperty);
				}
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00043948 File Offset: 0x00041B48
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x00043960 File Offset: 0x00041B60
		[CreateProperty]
		public bool delegatesFocus
		{
			get
			{
				return this.m_DelegatesFocus;
			}
			set
			{
				bool flag = this.m_DelegatesFocus == value;
				if (!flag)
				{
					this.m_DelegatesFocus = value;
					base.NotifyPropertyChanged(in Focusable.delegatesFocusProperty);
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x00043990 File Offset: 0x00041B90
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x000439A8 File Offset: 0x00041BA8
		internal bool excludeFromFocusRing
		{
			get
			{
				return this.m_ExcludeFromFocusRing;
			}
			set
			{
				bool flag = !((VisualElement)this).isCompositeRoot;
				if (flag)
				{
					throw new InvalidOperationException("excludeFromFocusRing should only be set on composite roots.");
				}
				this.m_ExcludeFromFocusRing = value;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x000439DB File Offset: 0x00041BDB
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x000439E3 File Offset: 0x00041BE3
		internal bool isEligibleToReceiveFocusFromDisabledChild { get; set; } = true;

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x000439EC File Offset: 0x00041BEC
		[CreateProperty(ReadOnly = true)]
		public virtual bool canGrabFocus
		{
			get
			{
				return this.focusable;
			}
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000439F4 File Offset: 0x00041BF4
		public virtual void Focus()
		{
			bool flag = this.focusController != null;
			if (flag)
			{
				bool canGrabFocus = this.canGrabFocus;
				if (canGrabFocus)
				{
					Focusable elementGettingFocused = this.GetFocusDelegate();
					this.focusController.SwitchFocus(elementGettingFocused, this != elementGettingFocused, DispatchMode.Default);
				}
				else
				{
					this.focusController.SwitchFocus(null, false, DispatchMode.Default);
				}
			}
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00043A4C File Offset: 0x00041C4C
		public virtual void Blur()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.Blur(this, false, DispatchMode.Default);
			}
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00043A64 File Offset: 0x00041C64
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void BlurImmediately()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.Blur(this, false, DispatchMode.Immediate);
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00043A7C File Offset: 0x00041C7C
		internal Focusable GetFocusDelegate()
		{
			Focusable f = this;
			while (f != null && f.delegatesFocus)
			{
				f = Focusable.GetFirstFocusableChild(f as VisualElement);
			}
			return f;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00043AB4 File Offset: 0x00041CB4
		private static Focusable GetFirstFocusableChild(VisualElement ve)
		{
			int veChildCount = ve.hierarchy.childCount;
			int i = 0;
			while (i < veChildCount)
			{
				VisualElement child = ve.hierarchy[i];
				bool flag = child.canGrabFocus && child.tabIndex >= 0;
				if (!flag)
				{
					bool isSlot = child.hierarchy.parent != null && child == child.hierarchy.parent.contentContainer;
					bool flag2 = !child.isCompositeRoot && !isSlot;
					if (flag2)
					{
						Focusable f = Focusable.GetFirstFocusableChild(child);
						bool flag3 = f != null;
						if (flag3)
						{
							return f;
						}
					}
					i++;
					continue;
				}
				return child;
			}
			return null;
		}

		// Token: 0x040008D3 RID: 2259
		internal static readonly BindingId focusableProperty = "focusable";

		// Token: 0x040008D4 RID: 2260
		internal static readonly BindingId tabIndexProperty = "tabIndex";

		// Token: 0x040008D5 RID: 2261
		internal static readonly BindingId delegatesFocusProperty = "delegatesFocus";

		// Token: 0x040008D6 RID: 2262
		internal static readonly BindingId canGrabFocusProperty = "canGrabFocus";

		// Token: 0x040008D7 RID: 2263
		private bool m_Focusable;

		// Token: 0x040008D8 RID: 2264
		private int m_TabIndex;

		// Token: 0x040008D9 RID: 2265
		private bool m_DelegatesFocus;

		// Token: 0x040008DA RID: 2266
		private bool m_ExcludeFromFocusRing;
	}
}
