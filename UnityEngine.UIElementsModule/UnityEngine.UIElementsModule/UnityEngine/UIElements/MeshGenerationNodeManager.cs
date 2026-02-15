using System;
using System.Collections.Generic;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A8 RID: 680
	internal class MeshGenerationNodeManager : IDisposable
	{
		// Token: 0x0600125E RID: 4702 RVA: 0x0004C777 File Offset: 0x0004A977
		public MeshGenerationNodeManager(EntryRecorder entryRecorder)
		{
			this.m_EntryRecorder = entryRecorder;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0004C794 File Offset: 0x0004A994
		public void CreateNode(Entry parentEntry, out MeshGenerationNode node)
		{
			MeshGenerationNodeImpl nodeImpl = this.CreateImpl(parentEntry, true);
			nodeImpl.GetNode(out node);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0004C7B4 File Offset: 0x0004A9B4
		public void CreateUnsafeNode(Entry parentEntry, out UnsafeMeshGenerationNode node)
		{
			MeshGenerationNodeImpl nodeImpl = this.CreateImpl(parentEntry, false);
			nodeImpl.GetUnsafeNode(out node);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004C7D4 File Offset: 0x0004A9D4
		private MeshGenerationNodeImpl CreateImpl(Entry parentEntry, bool safe)
		{
			bool disposed = this.disposed;
			MeshGenerationNodeImpl meshGenerationNodeImpl;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				meshGenerationNodeImpl = null;
			}
			else
			{
				bool flag = this.m_Nodes.Count == this.m_UsedCounter;
				if (flag)
				{
					for (int i = 0; i < 200; i++)
					{
						this.m_Nodes.Add(new MeshGenerationNodeImpl());
					}
				}
				List<MeshGenerationNodeImpl> nodes = this.m_Nodes;
				int usedCounter = this.m_UsedCounter;
				this.m_UsedCounter = usedCounter + 1;
				MeshGenerationNodeImpl nodeImpl = nodes[usedCounter];
				nodeImpl.Init(parentEntry, this.m_EntryRecorder, safe);
				meshGenerationNodeImpl = nodeImpl;
			}
			return meshGenerationNodeImpl;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0004C870 File Offset: 0x0004AA70
		public void ResetAll()
		{
			for (int i = 0; i < this.m_UsedCounter; i++)
			{
				this.m_Nodes[i].Reset();
			}
			this.m_UsedCounter = 0;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x0004C8AC File Offset: 0x0004AAAC
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x0004C8B4 File Offset: 0x0004AAB4
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001265 RID: 4709 RVA: 0x0004C8BD File Offset: 0x0004AABD
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004C8D0 File Offset: 0x0004AAD0
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					int i = 0;
					int count = this.m_Nodes.Count;
					while (i < count)
					{
						this.m_Nodes[i].Dispose();
						i++;
					}
					this.m_Nodes.Clear();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000AA6 RID: 2726
		private List<MeshGenerationNodeImpl> m_Nodes = new List<MeshGenerationNodeImpl>(8);

		// Token: 0x04000AA7 RID: 2727
		private int m_UsedCounter;

		// Token: 0x04000AA8 RID: 2728
		private EntryRecorder m_EntryRecorder;
	}
}
