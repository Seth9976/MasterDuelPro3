using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000199 RID: 409
	public struct StartDragArgs
	{
		// Token: 0x06000BED RID: 3053 RVA: 0x00038BE2 File Offset: 0x00036DE2
		public StartDragArgs(string title, DragVisualMode visualMode)
		{
			this.title = title;
			this.visualMode = visualMode;
			this.genericData = null;
			this.assetPaths = null;
			this.unityObjectReferences = null;
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00038C0B File Offset: 0x00036E0B
		public readonly string title { get; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00038C13 File Offset: 0x00036E13
		public readonly DragVisualMode visualMode { get; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00038C1B File Offset: 0x00036E1B
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x00038C23 File Offset: 0x00036E23
		internal Hashtable genericData { readonly get; private set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00038C2C File Offset: 0x00036E2C
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00038C34 File Offset: 0x00036E34
		internal IEnumerable<Object> unityObjectReferences { readonly get; private set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00038C3D File Offset: 0x00036E3D
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x00038C45 File Offset: 0x00036E45
		internal string[] assetPaths { readonly get; private set; }

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00038C50 File Offset: 0x00036E50
		public void SetGenericData(string key, object data)
		{
			if (this.genericData == null)
			{
				this.genericData = new Hashtable();
			}
			this.genericData[key] = data;
		}
	}
}
