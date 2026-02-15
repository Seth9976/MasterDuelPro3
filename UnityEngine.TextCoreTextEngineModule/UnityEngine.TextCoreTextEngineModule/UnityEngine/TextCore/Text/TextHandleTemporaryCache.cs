using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200005B RID: 91
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal class TextHandleTemporaryCache
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0002D798 File Offset: 0x0002B998
		public void ClearTemporaryCache()
		{
			for (int i = 0; i < this.s_TextInfoPool.Count; i++)
			{
				this.s_TextInfoPool.First.Value.RemoveFromCache();
			}
			this.s_TextInfoPool.Clear();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0002D7E4 File Offset: 0x0002B9E4
		public void AddTextInfoToCache(TextHandle textHandle, int hashCode)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool isCachedPermanent = textHandle.IsCachedPermanent;
				if (isCachedPermanent)
				{
					return;
				}
				bool canWriteOnAsset = !TextGenerator.IsExecutingJob;
				bool flag2 = canWriteOnAsset;
				if (flag2)
				{
					this.currentFrame = Time.frameCount;
				}
				bool flag3 = this.s_TextInfoPool.Count > 0 && ((double)this.currentFrame - this.s_TextInfoPool.Last.Value.lastTimeInCache < 0.0 || (double)this.currentFrame - this.s_TextInfoPool.First.Value.lastTimeInCache < 0.0);
				if (flag3)
				{
					this.ClearTemporaryCache();
				}
				bool isCachedTemporary = textHandle.IsCachedTemporary;
				if (isCachedTemporary)
				{
					this.RefreshCaching(textHandle);
					return;
				}
				bool flag4 = this.s_TextInfoPool.Count > 0 && (double)this.currentFrame - this.s_TextInfoPool.Last.Value.lastTimeInCache > 2.0;
				if (flag4)
				{
					this.RecycleTextInfoFromCache(textHandle);
				}
				else
				{
					TextInfo textInfo = new TextInfo(VertexDataLayout.VBO);
					textHandle.TextInfoNode = new LinkedListNode<TextInfo>(textInfo);
					this.s_TextInfoPool.AddFirst(textHandle.TextInfoNode);
					textInfo.lastTimeInCache = (double)this.currentFrame;
					TextInfo textInfo2 = textInfo;
					textInfo2.removedFromCache = (Action)Delegate.Combine(textInfo2.removedFromCache, new Action(textHandle.RemoveTextInfoFromTemporaryCache));
				}
			}
			textHandle.IsCachedTemporary = true;
			textHandle.SetDirty();
			textHandle.UpdateWithHash(hashCode);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0002D9A4 File Offset: 0x0002BBA4
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		public virtual void RemoveTextInfoFromCache(TextHandle textHandle)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				bool flag2 = !textHandle.IsCachedTemporary;
				if (!flag2)
				{
					textHandle.IsCachedTemporary = false;
					textHandle.TextInfoNode.Value.lastTimeInCache = 0.0;
					textHandle.TextInfoNode.Value.removedFromCache = null;
					bool flag3 = textHandle.TextInfoNode != null;
					if (flag3)
					{
						this.s_TextInfoPool.Remove(textHandle.TextInfoNode);
						this.s_TextInfoPool.AddLast(textHandle.TextInfoNode);
					}
					textHandle.TextInfoNode = null;
				}
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0002DA60 File Offset: 0x0002BC60
		private void RefreshCaching(TextHandle textHandle)
		{
			bool flag = !TextGenerator.IsExecutingJob;
			if (flag)
			{
				this.currentFrame = Time.frameCount;
			}
			textHandle.TextInfoNode.Value.lastTimeInCache = (double)this.currentFrame;
			this.s_TextInfoPool.Remove(textHandle.TextInfoNode);
			this.s_TextInfoPool.AddFirst(textHandle.TextInfoNode);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0002DAC0 File Offset: 0x0002BCC0
		private void RecycleTextInfoFromCache(TextHandle textHandle)
		{
			bool flag = !TextGenerator.IsExecutingJob;
			if (flag)
			{
				this.currentFrame = Time.frameCount;
			}
			textHandle.TextInfoNode = this.s_TextInfoPool.Last;
			textHandle.TextInfoNode.Value.RemoveFromCache();
			this.s_TextInfoPool.RemoveLast();
			this.s_TextInfoPool.AddFirst(textHandle.TextInfoNode);
			textHandle.IsCachedTemporary = true;
			TextInfo value = textHandle.TextInfoNode.Value;
			value.removedFromCache = (Action)Delegate.Combine(value.removedFromCache, new Action(textHandle.RemoveTextInfoFromTemporaryCache));
			textHandle.TextInfoNode.Value.lastTimeInCache = (double)this.currentFrame;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0002DB71 File Offset: 0x0002BD71
		public void UpdateCurrentFrame()
		{
			this.currentFrame = Time.frameCount;
		}

		// Token: 0x0400036E RID: 878
		internal LinkedList<TextInfo> s_TextInfoPool = new LinkedList<TextInfo>();

		// Token: 0x0400036F RID: 879
		internal const int s_MinFramesInCache = 2;

		// Token: 0x04000370 RID: 880
		internal int currentFrame;

		// Token: 0x04000371 RID: 881
		private object syncRoot = new object();
	}
}
