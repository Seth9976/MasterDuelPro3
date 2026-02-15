using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x02000069 RID: 105
	internal class ReusableTreeViewItem : ReusableCollectionItem
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00011DE5 File Offset: 0x0000FFE5
		public override VisualElement rootElement
		{
			get
			{
				return this.m_Container ?? base.bindableElement;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060003AF RID: 943 RVA: 0x00011DF8 File Offset: 0x0000FFF8
		// (remove) Token: 0x060003B0 RID: 944 RVA: 0x00011E30 File Offset: 0x00010030
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<PointerUpEvent> onPointerUp;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060003B1 RID: 945 RVA: 0x00011E68 File Offset: 0x00010068
		// (remove) Token: 0x060003B2 RID: 946 RVA: 0x00011EA0 File Offset: 0x000100A0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ChangeEvent<bool>> onToggleValueChanged;

		// Token: 0x060003B3 RID: 947 RVA: 0x00011ED5 File Offset: 0x000100D5
		public ReusableTreeViewItem()
		{
			this.m_PointerUpCallback = new EventCallback<PointerUpEvent>(this.OnPointerUp);
			this.m_ToggleValueChangedCallback = new EventCallback<ChangeEvent<bool>>(this.OnToggleValueChanged);
			this.m_ToggleGeometryChangedCallback = new EventCallback<GeometryChangedEvent>(this.OnToggleGeometryChanged);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00011F18 File Offset: 0x00010118
		public override void Init(VisualElement item)
		{
			base.Init(item);
			VisualElement container = new VisualElement
			{
				name = BaseTreeView.itemUssClassName
			};
			container.AddToClassList(BaseTreeView.itemUssClassName);
			this.InitExpandHierarchy(container, item);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00011F58 File Offset: 0x00010158
		protected void InitExpandHierarchy(VisualElement root, VisualElement item)
		{
			this.m_Container = root;
			this.m_Container.style.flexDirection = FlexDirection.Row;
			this.m_IndentElement = new VisualElement
			{
				name = BaseTreeView.itemIndentUssClassName,
				style = 
				{
					flexDirection = FlexDirection.Row
				}
			};
			this.m_Container.hierarchy.Add(this.m_IndentElement);
			this.m_Toggle = new Toggle
			{
				name = BaseTreeView.itemToggleUssClassName,
				userData = this
			};
			this.m_Toggle.AddToClassList(Foldout.toggleUssClassName);
			this.m_Toggle.AddToClassList(BaseTreeView.itemToggleUssClassName);
			this.m_Toggle.visualInput.AddToClassList(Foldout.inputUssClassName);
			this.m_Checkmark = this.m_Toggle.visualInput.Q(null, Toggle.checkmarkUssClassName);
			this.m_Checkmark.AddToClassList(Foldout.checkmarkUssClassName);
			this.m_Container.hierarchy.Add(this.m_Toggle);
			this.m_BindableContainer = new VisualElement
			{
				name = BaseTreeView.itemContentContainerUssClassName,
				style = 
				{
					flexGrow = 1f
				}
			};
			this.m_BindableContainer.AddToClassList(BaseTreeView.itemContentContainerUssClassName);
			this.m_Container.Add(this.m_BindableContainer);
			this.m_BindableContainer.Add(item);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000120C4 File Offset: 0x000102C4
		public override void PreAttachElement()
		{
			base.PreAttachElement();
			this.rootElement.AddToClassList(BaseTreeView.itemUssClassName);
			VisualElement container = this.m_Container;
			if (container != null)
			{
				container.RegisterCallback<PointerUpEvent>(this.m_PointerUpCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle = this.m_Toggle;
			if (toggle != null)
			{
				toggle.visualInput.Q(null, Toggle.checkmarkUssClassName).RegisterCallback<GeometryChangedEvent>(this.m_ToggleGeometryChangedCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle2 = this.m_Toggle;
			if (toggle2 != null)
			{
				toggle2.RegisterValueChangedCallback(this.m_ToggleValueChangedCallback);
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00012144 File Offset: 0x00010344
		public override void DetachElement()
		{
			base.DetachElement();
			this.rootElement.RemoveFromClassList(BaseTreeView.itemUssClassName);
			VisualElement container = this.m_Container;
			if (container != null)
			{
				container.UnregisterCallback<PointerUpEvent>(this.m_PointerUpCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle = this.m_Toggle;
			if (toggle != null)
			{
				toggle.visualInput.Q(null, Toggle.checkmarkUssClassName).UnregisterCallback<GeometryChangedEvent>(this.m_ToggleGeometryChangedCallback, TrickleDown.NoTrickleDown);
			}
			Toggle toggle2 = this.m_Toggle;
			if (toggle2 != null)
			{
				toggle2.UnregisterValueChangedCallback(this.m_ToggleValueChangedCallback);
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000121C4 File Offset: 0x000103C4
		public void Indent(int depth)
		{
			bool flag = this.m_IndentElement == null;
			if (!flag)
			{
				this.m_Depth = depth;
				this.UpdateIndentLayout();
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000121EF File Offset: 0x000103EF
		public void SetExpandedWithoutNotify(bool expanded)
		{
			Toggle toggle = this.m_Toggle;
			if (toggle != null)
			{
				toggle.SetValueWithoutNotify(expanded);
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00012208 File Offset: 0x00010408
		public void SetToggleVisibility(bool visible)
		{
			bool flag = this.m_Toggle != null;
			if (flag)
			{
				this.m_Toggle.visible = visible;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00012230 File Offset: 0x00010430
		private void OnToggleGeometryChanged(GeometryChangedEvent evt)
		{
			float width = this.m_Checkmark.resolvedStyle.width + this.m_Checkmark.resolvedStyle.marginLeft + this.m_Checkmark.resolvedStyle.marginRight;
			bool flag = Math.Abs(width - this.m_IndentWidth) < float.Epsilon;
			if (!flag)
			{
				this.m_IndentWidth = width;
				this.UpdateIndentLayout();
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001229C File Offset: 0x0001049C
		private void UpdateIndentLayout()
		{
			this.m_IndentElement.style.width = this.m_IndentWidth * (float)this.m_Depth;
			this.m_IndentElement.EnableInClassList(BaseTreeView.itemIndentUssClassName, this.m_Depth > 0);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000122E8 File Offset: 0x000104E8
		private void OnPointerUp(PointerUpEvent evt)
		{
			Action<PointerUpEvent> action = this.onPointerUp;
			if (action != null)
			{
				action(evt);
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000122FE File Offset: 0x000104FE
		private void OnToggleValueChanged(ChangeEvent<bool> evt)
		{
			Action<ChangeEvent<bool>> action = this.onToggleValueChanged;
			if (action != null)
			{
				action(evt);
			}
		}

		// Token: 0x040001EB RID: 491
		private Toggle m_Toggle;

		// Token: 0x040001EC RID: 492
		private VisualElement m_Container;

		// Token: 0x040001ED RID: 493
		private VisualElement m_IndentElement;

		// Token: 0x040001EE RID: 494
		private VisualElement m_BindableContainer;

		// Token: 0x040001EF RID: 495
		private VisualElement m_Checkmark;

		// Token: 0x040001F2 RID: 498
		private int m_Depth;

		// Token: 0x040001F3 RID: 499
		private float m_IndentWidth;

		// Token: 0x040001F4 RID: 500
		private EventCallback<PointerUpEvent> m_PointerUpCallback;

		// Token: 0x040001F5 RID: 501
		private EventCallback<ChangeEvent<bool>> m_ToggleValueChangedCallback;

		// Token: 0x040001F6 RID: 502
		private EventCallback<GeometryChangedEvent> m_ToggleGeometryChangedCallback;
	}
}
