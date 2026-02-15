using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A7 RID: 679
	internal class MeshGenerationNodeImpl : IDisposable
	{
		// Token: 0x06001252 RID: 4690 RVA: 0x0004C5DF File Offset: 0x0004A7DF
		public MeshGenerationNodeImpl()
		{
			this.m_SelfHandle = GCHandle.Alloc(this);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0004C5F5 File Offset: 0x0004A7F5
		public void Init(Entry parentEntry, EntryRecorder entryRecorder, bool safe)
		{
			Debug.Assert(this.m_ParentEntry == null);
			Debug.Assert(parentEntry != null);
			Debug.Assert(entryRecorder != null);
			this.m_ParentEntry = parentEntry;
			this.m_EntryRecorder = entryRecorder;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0004C629 File Offset: 0x0004A829
		public void Reset()
		{
			Debug.Assert(this.m_ParentEntry != null);
			Debug.Assert(this.m_EntryRecorder != null);
			this.m_ParentEntry = null;
			this.m_EntryRecorder = null;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0004C658 File Offset: 0x0004A858
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetNode(out MeshGenerationNode node)
		{
			MeshGenerationNode.Create(this.m_SelfHandle, out node);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0004C668 File Offset: 0x0004A868
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetUnsafeNode(out UnsafeMeshGenerationNode node)
		{
			UnsafeMeshGenerationNode.Create(this.m_SelfHandle, out node);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0004C678 File Offset: 0x0004A878
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Entry GetParentEntry()
		{
			return this.m_ParentEntry;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0004C680 File Offset: 0x0004A880
		public void DrawMesh(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture = null, bool skipAtlas = false)
		{
			bool flag = vertices.Length == 0 || indices.Length == 0;
			if (!flag)
			{
				this.m_EntryRecorder.DrawMesh(this.m_ParentEntry, vertices, indices, texture, skipAtlas);
			}
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0004C6C4 File Offset: 0x0004A8C4
		public void DrawGradients(NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, VectorImage gradientsOwner)
		{
			bool flag = vertices.Length == 0 || indices.Length == 0 || gradientsOwner == null;
			if (!flag)
			{
				this.m_EntryRecorder.DrawGradients(this.m_ParentEntry, vertices, indices, gradientsOwner);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0004C709 File Offset: 0x0004A909
		// (set) Token: 0x0600125B RID: 4699 RVA: 0x0004C711 File Offset: 0x0004A911
		private protected bool disposed { protected get; private set; }

		// Token: 0x0600125C RID: 4700 RVA: 0x0004C71A File Offset: 0x0004A91A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0004C72C File Offset: 0x0004A92C
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.m_ParentEntry != null;
					if (flag)
					{
						this.Reset();
					}
					this.m_SelfHandle.Free();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000AA2 RID: 2722
		private GCHandle m_SelfHandle;

		// Token: 0x04000AA3 RID: 2723
		private Entry m_ParentEntry;

		// Token: 0x04000AA4 RID: 2724
		private EntryRecorder m_EntryRecorder;
	}
}
