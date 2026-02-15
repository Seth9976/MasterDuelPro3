using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005F3 RID: 1523
	internal class MultiColumnHeaderColumnIcon : Image
	{
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x000AB18A File Offset: 0x000A938A
		// (set) Token: 0x06002962 RID: 10594 RVA: 0x000AB192 File Offset: 0x000A9392
		public bool isImageInline { get; set; }

		// Token: 0x06002963 RID: 10595 RVA: 0x000AB19B File Offset: 0x000A939B
		public MultiColumnHeaderColumnIcon()
		{
			base.AddToClassList(MultiColumnHeaderColumnIcon.ussClassName);
			base.RegisterCallback<CustomStyleResolvedEvent>(delegate(CustomStyleResolvedEvent evt)
			{
				this.UpdateClassList();
			}, TrickleDown.NoTrickleDown);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000AB1C8 File Offset: 0x000A93C8
		public void UpdateClassList()
		{
			base.parent.RemoveFromClassList(MultiColumnHeaderColumn.hasIconUssClassName);
			bool flag = base.image != null || base.sprite != null || base.vectorImage != null;
			if (flag)
			{
				base.parent.AddToClassList(MultiColumnHeaderColumn.hasIconUssClassName);
			}
		}

		// Token: 0x040015D9 RID: 5593
		public new static readonly string ussClassName = MultiColumnHeaderColumn.ussClassName + "__icon";
	}
}
