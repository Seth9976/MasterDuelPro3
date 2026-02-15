using System;
using System.Diagnostics;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x02000065 RID: 101
	internal class ReusableCollectionItem
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000385 RID: 901 RVA: 0x000116CE File Offset: 0x0000F8CE
		public virtual VisualElement rootElement
		{
			get
			{
				return this.bindableElement;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000386 RID: 902 RVA: 0x000116D6 File Offset: 0x0000F8D6
		// (set) Token: 0x06000387 RID: 903 RVA: 0x000116DE File Offset: 0x0000F8DE
		public VisualElement bindableElement { get; protected set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000388 RID: 904 RVA: 0x000116E7 File Offset: 0x0000F8E7
		// (set) Token: 0x06000389 RID: 905 RVA: 0x000116EF File Offset: 0x0000F8EF
		public ValueAnimation<StyleValues> animator { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600038A RID: 906 RVA: 0x000116F8 File Offset: 0x0000F8F8
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00011700 File Offset: 0x0000F900
		public int index { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00011709 File Offset: 0x0000F909
		// (set) Token: 0x0600038D RID: 909 RVA: 0x00011711 File Offset: 0x0000F911
		public int id { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0001171A File Offset: 0x0000F91A
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00011722 File Offset: 0x0000F922
		internal bool isDragGhost { get; private set; }

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000390 RID: 912 RVA: 0x0001172C File Offset: 0x0000F92C
		// (remove) Token: 0x06000391 RID: 913 RVA: 0x00011764 File Offset: 0x0000F964
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ReusableCollectionItem> onGeometryChanged;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000392 RID: 914 RVA: 0x0001179C File Offset: 0x0000F99C
		// (remove) Token: 0x06000393 RID: 915 RVA: 0x000117D4 File Offset: 0x0000F9D4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<ReusableCollectionItem> onDestroy;

		// Token: 0x06000394 RID: 916 RVA: 0x0001180C File Offset: 0x0000FA0C
		public ReusableCollectionItem()
		{
			this.index = (this.id = -1);
			this.m_GeometryChangedEventCallback = new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00011845 File Offset: 0x0000FA45
		public virtual void Init(VisualElement item)
		{
			this.bindableElement = item;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00011850 File Offset: 0x0000FA50
		public virtual void PreAttachElement()
		{
			this.rootElement.AddToClassList(BaseVerticalCollectionView.itemUssClassName);
			this.rootElement.RegisterCallback<GeometryChangedEvent>(this.m_GeometryChangedEventCallback, TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011878 File Offset: 0x0000FA78
		public virtual void DetachElement()
		{
			this.rootElement.RemoveFromClassList(BaseVerticalCollectionView.itemUssClassName);
			this.rootElement.UnregisterCallback<GeometryChangedEvent>(this.m_GeometryChangedEventCallback, TrickleDown.NoTrickleDown);
			VisualElement rootElement = this.rootElement;
			if (rootElement != null)
			{
				rootElement.RemoveFromHierarchy();
			}
			this.SetSelected(false);
			this.SetDragGhost(false);
			this.index = (this.id = -1);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000118DE File Offset: 0x0000FADE
		public virtual void DestroyElement()
		{
			Action<ReusableCollectionItem> action = this.onDestroy;
			if (action != null)
			{
				action(this);
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000118F4 File Offset: 0x0000FAF4
		public virtual void SetSelected(bool selected)
		{
			if (selected)
			{
				this.rootElement.AddToClassList(BaseVerticalCollectionView.itemSelectedVariantUssClassName);
				this.rootElement.pseudoStates |= PseudoStates.Checked;
			}
			else
			{
				this.rootElement.RemoveFromClassList(BaseVerticalCollectionView.itemSelectedVariantUssClassName);
				this.rootElement.pseudoStates &= ~PseudoStates.Checked;
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00011958 File Offset: 0x0000FB58
		public virtual void SetDragGhost(bool dragGhost)
		{
			this.isDragGhost = dragGhost;
			this.rootElement.style.maxHeight = (this.isDragGhost ? StyleKeyword.Undefined : StyleKeyword.Initial);
			this.bindableElement.style.display = (this.isDragGhost ? DisplayStyle.None : DisplayStyle.Flex);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000119B2 File Offset: 0x0000FBB2
		protected void OnGeometryChanged(GeometryChangedEvent evt)
		{
			Action<ReusableCollectionItem> action = this.onGeometryChanged;
			if (action != null)
			{
				action(this);
			}
		}

		// Token: 0x040001E5 RID: 485
		protected EventCallback<GeometryChangedEvent> m_GeometryChangedEventCallback;
	}
}
