using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000192 RID: 402
	internal struct DragAndDropArgs : IListDragAndDropArgs
	{
		// Token: 0x17000221 RID: 545
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x00038B3C File Offset: 0x00036D3C
		public object target
		{
			[CompilerGenerated]
			set
			{
				this.<target>k__BackingField = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x00038B45 File Offset: 0x00036D45
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x00038B4D File Offset: 0x00036D4D
		public int insertAtIndex { readonly get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00038B56 File Offset: 0x00036D56
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x00038B5E File Offset: 0x00036D5E
		public int parentId { readonly get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00038B67 File Offset: 0x00036D67
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x00038B6F File Offset: 0x00036D6F
		public int childIndex { readonly get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x00038B78 File Offset: 0x00036D78
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x00038B80 File Offset: 0x00036D80
		public DragAndDropPosition dragAndDropPosition { readonly get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x00038B89 File Offset: 0x00036D89
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x00038B91 File Offset: 0x00036D91
		public DragAndDropData dragAndDropData { readonly get; set; }
	}
}
