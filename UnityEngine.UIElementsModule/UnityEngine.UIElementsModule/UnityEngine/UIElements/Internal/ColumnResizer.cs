using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005EA RID: 1514
	internal class ColumnResizer : PointerManipulator
	{
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002910 RID: 10512 RVA: 0x000A9271 File Offset: 0x000A7471
		// (set) Token: 0x06002911 RID: 10513 RVA: 0x000A9279 File Offset: 0x000A7479
		public ColumnLayout columnLayout { get; set; }

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002912 RID: 10514 RVA: 0x000A9282 File Offset: 0x000A7482
		// (set) Token: 0x06002913 RID: 10515 RVA: 0x000A928A File Offset: 0x000A748A
		public bool preview { get; set; }

		// Token: 0x06002914 RID: 10516 RVA: 0x000A9294 File Offset: 0x000A7494
		public ColumnResizer(Column column)
		{
			this.m_Column = column;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.m_Active = false;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000A92D8 File Offset: 0x000A74D8
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000A934C File Offset: 0x000A754C
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000A93C0 File Offset: 0x000A75C0
		private void OnKeyDown(KeyDownEvent e)
		{
			bool flag = e.keyCode == KeyCode.Escape && this.m_Resizing && this.preview;
			if (flag)
			{
				this.EndDragResize(0f, true);
			}
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000A93FC File Offset: 0x000A75FC
		private void OnPointerDown(PointerDownEvent e)
		{
			bool active = this.m_Active;
			if (active)
			{
				e.StopImmediatePropagation();
			}
			else
			{
				bool flag = base.CanStartManipulation(e);
				if (flag)
				{
					VisualElement ve = e.currentTarget as VisualElement;
					this.m_Header = ve.GetFirstAncestorOfType<MultiColumnCollectionHeader>();
					this.preview = this.m_Column.collection.resizePreview;
					bool preview = this.preview;
					if (preview)
					{
						bool flag2 = this.m_PreviewElement == null;
						if (flag2)
						{
							this.m_PreviewElement = new MultiColumnHeaderColumnResizePreview();
						}
						ScrollView firstAncestorOfType = this.m_Header.GetFirstAncestorOfType<ScrollView>();
						VisualElement previewParent = ((firstAncestorOfType != null) ? firstAncestorOfType.parent : null) ?? this.m_Header.parent;
						previewParent.hierarchy.Add(this.m_PreviewElement);
					}
					this.columnLayout = this.m_Header.columnLayout;
					this.m_Start = ve.ChangeCoordinatesTo(this.m_Header, e.localPosition);
					this.BeginDragResize(this.m_Start.x);
					this.m_Active = true;
					base.target.CaptureMouse();
					e.StopPropagation();
				}
			}
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000A9520 File Offset: 0x000A7720
		private void OnPointerMove(PointerMoveEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId);
			if (!flag)
			{
				VisualElement ve = e.currentTarget as VisualElement;
				Vector2 pos = ve.ChangeCoordinatesTo(this.m_Header, e.localPosition);
				this.DragResize(pos.x);
				e.StopPropagation();
			}
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000A958C File Offset: 0x000A778C
		private void OnPointerUp(PointerUpEvent e)
		{
			bool flag = !this.m_Active || !base.target.HasPointerCapture(e.pointerId) || !base.CanStopManipulation(e);
			if (!flag)
			{
				VisualElement ve = e.currentTarget as VisualElement;
				Vector2 pos = ve.ChangeCoordinatesTo(this.m_Header, e.localPosition);
				this.EndDragResize(pos.x, false);
				this.m_Active = false;
				base.target.ReleasePointer(e.pointerId);
				e.StopPropagation();
			}
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000A961C File Offset: 0x000A781C
		private void BeginDragResize(float pos)
		{
			this.m_Resizing = true;
			ColumnLayout columnLayout = this.columnLayout;
			if (columnLayout != null)
			{
				columnLayout.BeginDragResize(this.m_Column, this.m_Start.x, this.preview);
			}
			bool preview = this.preview;
			if (preview)
			{
				this.UpdatePreviewPosition();
			}
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000A9670 File Offset: 0x000A7870
		private void DragResize(float pos)
		{
			bool flag = !this.m_Resizing;
			if (!flag)
			{
				ColumnLayout columnLayout = this.columnLayout;
				if (columnLayout != null)
				{
					columnLayout.DragResize(this.m_Column, pos);
				}
				bool preview = this.preview;
				if (preview)
				{
					this.UpdatePreviewPosition();
				}
			}
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x000A96B9 File Offset: 0x000A78B9
		private void UpdatePreviewPosition()
		{
			this.m_PreviewElement.style.left = this.columnLayout.GetDesiredPosition(this.m_Column) + this.columnLayout.GetDesiredWidth(this.m_Column);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000A96F8 File Offset: 0x000A78F8
		private void EndDragResize(float pos, bool cancelled)
		{
			bool flag = !this.m_Resizing;
			if (!flag)
			{
				bool preview = this.preview;
				if (preview)
				{
					VisualElement previewElement = this.m_PreviewElement;
					if (previewElement != null)
					{
						previewElement.RemoveFromHierarchy();
					}
					this.m_PreviewElement = null;
				}
				ColumnLayout columnLayout = this.columnLayout;
				if (columnLayout != null)
				{
					columnLayout.EndDragResize(this.m_Column, cancelled);
				}
				this.m_Resizing = false;
			}
		}

		// Token: 0x040015A9 RID: 5545
		private Vector2 m_Start;

		// Token: 0x040015AA RID: 5546
		protected bool m_Active;

		// Token: 0x040015AB RID: 5547
		private bool m_Resizing;

		// Token: 0x040015AC RID: 5548
		private MultiColumnCollectionHeader m_Header;

		// Token: 0x040015AD RID: 5549
		private Column m_Column;

		// Token: 0x040015AE RID: 5550
		private VisualElement m_PreviewElement;
	}
}
