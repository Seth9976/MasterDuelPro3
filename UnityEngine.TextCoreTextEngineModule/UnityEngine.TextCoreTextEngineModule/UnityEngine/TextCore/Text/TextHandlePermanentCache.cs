using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200005A RID: 90
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal class TextHandlePermanentCache
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0002D640 File Offset: 0x0002B840
		public virtual void AddTextInfoToCache(TextHandle textHandle)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool isCachedPermanent = textHandle.IsCachedPermanent;
				if (isCachedPermanent)
				{
					return;
				}
				bool isCachedTemporary = textHandle.IsCachedTemporary;
				if (isCachedTemporary)
				{
					textHandle.RemoveTextInfoFromTemporaryCache();
				}
				bool flag2 = this.s_TextInfoPool.Count > 0;
				if (flag2)
				{
					textHandle.TextInfoNode = this.s_TextInfoPool.Last;
					this.s_TextInfoPool.RemoveLast();
				}
				else
				{
					TextInfo textInfo = new TextInfo(VertexDataLayout.VBO);
					textHandle.TextInfoNode = new LinkedListNode<TextInfo>(textInfo);
				}
			}
			textHandle.IsCachedPermanent = true;
			textHandle.SetDirty();
			textHandle.Update();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0002D708 File Offset: 0x0002B908
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		public void RemoveTextInfoFromCache(TextHandle textHandle)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool flag2 = !textHandle.IsCachedPermanent;
				if (!flag2)
				{
					this.s_TextInfoPool.AddFirst(textHandle.TextInfoNode);
					textHandle.TextInfoNode = null;
					textHandle.IsCachedPermanent = false;
				}
			}
		}

		// Token: 0x0400036C RID: 876
		internal LinkedList<TextInfo> s_TextInfoPool = new LinkedList<TextInfo>();

		// Token: 0x0400036D RID: 877
		private object syncRoot = new object();
	}
}
