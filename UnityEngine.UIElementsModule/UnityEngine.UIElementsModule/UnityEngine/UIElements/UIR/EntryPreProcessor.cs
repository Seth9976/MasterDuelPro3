using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200051E RID: 1310
	internal class EntryPreProcessor
	{
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x0008940C File Offset: 0x0008760C
		public int childrenIndex
		{
			get
			{
				return this.m_ChildrenIndex;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x00089414 File Offset: 0x00087614
		public List<EntryPreProcessor.AllocSize> headAllocs
		{
			get
			{
				return this.m_HeadAllocs;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x0008941C File Offset: 0x0008761C
		public List<EntryPreProcessor.AllocSize> tailAllocs
		{
			get
			{
				return this.m_TailAllocs;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x00089424 File Offset: 0x00087624
		public List<Entry> flattenedEntries
		{
			get
			{
				return this.m_FlattenedEntries;
			}
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0008942C File Offset: 0x0008762C
		public void PreProcess(Entry root)
		{
			this.m_ChildrenIndex = -1;
			this.m_FlattenedEntries.Clear();
			this.m_HeadAllocs.Clear();
			this.m_TailAllocs.Clear();
			this.m_Allocs = this.m_HeadAllocs;
			this.DoEvaluate(root);
			this.Flush();
			Debug.Assert(!this.m_IsPushingMask);
			Debug.Assert(this.m_Mask.Count == 0);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000894A4 File Offset: 0x000876A4
		private void DoEvaluate(Entry entry)
		{
			while (entry != null)
			{
				bool flag = entry.type != EntryType.DedicatedPlaceholder;
				if (flag)
				{
					this.m_FlattenedEntries.Add(entry);
				}
				switch (entry.type)
				{
				case EntryType.DrawSolidMesh:
				case EntryType.DrawTexturedMesh:
				case EntryType.DrawTexturedMeshSkipAtlas:
				case EntryType.DrawTextMesh:
				case EntryType.DrawGradients:
					Debug.Assert((long)entry.vertices.Length <= (long)((ulong)UIRenderDevice.maxVerticesPerPage));
					this.Add(entry.vertices.Length, entry.indices.Length);
					break;
				case EntryType.DrawImmediate:
				case EntryType.DrawImmediateCull:
				case EntryType.PushClippingRect:
				case EntryType.PopClippingRect:
				case EntryType.PushScissors:
				case EntryType.PopScissors:
				case EntryType.PushGroupMatrix:
				case EntryType.PopGroupMatrix:
				case EntryType.PushRenderTexture:
				case EntryType.BlitAndPopRenderTexture:
				case EntryType.PushDefaultMaterial:
				case EntryType.PopDefaultMaterial:
				case EntryType.CutRenderChain:
				case EntryType.DedicatedPlaceholder:
					break;
				case EntryType.DrawChildren:
					Debug.Assert(!this.m_IsPushingMask);
					Debug.Assert(this.m_ChildrenIndex == -1);
					this.Flush();
					this.m_ChildrenIndex = this.m_FlattenedEntries.Count - 1;
					this.m_Allocs = this.tailAllocs;
					break;
				case EntryType.BeginStencilMask:
					Debug.Assert(!this.m_IsPushingMask);
					this.m_IsPushingMask = true;
					break;
				case EntryType.EndStencilMask:
					Debug.Assert(this.m_IsPushingMask);
					this.m_IsPushingMask = false;
					break;
				case EntryType.PopStencilMask:
					for (;;)
					{
						EntryPreProcessor.AllocSize size;
						bool flag2 = this.m_Mask.TryPop(out size);
						if (!flag2)
						{
							break;
						}
						this.Add(size.vertexCount, size.indexCount);
					}
					break;
				default:
					throw new NotImplementedException();
				}
				bool flag3 = entry.firstChild != null;
				if (flag3)
				{
					this.DoEvaluate(entry.firstChild);
				}
				entry = entry.nextSibling;
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x0008965C File Offset: 0x0008785C
		private void Add(int vertexCount, int indexCount)
		{
			bool flag = vertexCount == 0 || indexCount == 0;
			if (!flag)
			{
				int nextVertexCount = this.m_Pending.vertexCount + vertexCount;
				bool flag2 = (long)nextVertexCount <= (long)((ulong)UIRenderDevice.maxVerticesPerPage);
				if (flag2)
				{
					this.m_Pending.vertexCount = nextVertexCount;
					this.m_Pending.indexCount = this.m_Pending.indexCount + indexCount;
				}
				else
				{
					this.Flush();
					this.m_Pending.vertexCount = vertexCount;
					this.m_Pending.indexCount = indexCount;
				}
				bool isPushingMask = this.m_IsPushingMask;
				if (isPushingMask)
				{
					this.m_Mask.Push(new EntryPreProcessor.AllocSize
					{
						vertexCount = vertexCount,
						indexCount = indexCount
					});
				}
			}
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00089710 File Offset: 0x00087910
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Flush()
		{
			bool flag = this.m_Pending.vertexCount > 0;
			if (flag)
			{
				this.m_Allocs.Add(this.m_Pending);
				this.m_Pending = default(EntryPreProcessor.AllocSize);
			}
		}

		// Token: 0x04001115 RID: 4373
		private int m_ChildrenIndex;

		// Token: 0x04001116 RID: 4374
		private List<EntryPreProcessor.AllocSize> m_Allocs;

		// Token: 0x04001117 RID: 4375
		private List<EntryPreProcessor.AllocSize> m_HeadAllocs = new List<EntryPreProcessor.AllocSize>(1);

		// Token: 0x04001118 RID: 4376
		private List<EntryPreProcessor.AllocSize> m_TailAllocs = new List<EntryPreProcessor.AllocSize>(1);

		// Token: 0x04001119 RID: 4377
		private List<Entry> m_FlattenedEntries = new List<Entry>(8);

		// Token: 0x0400111A RID: 4378
		private EntryPreProcessor.AllocSize m_Pending;

		// Token: 0x0400111B RID: 4379
		private Stack<EntryPreProcessor.AllocSize> m_Mask = new Stack<EntryPreProcessor.AllocSize>(1);

		// Token: 0x0400111C RID: 4380
		private bool m_IsPushingMask;

		// Token: 0x0200051F RID: 1311
		public struct AllocSize
		{
			// Token: 0x0400111D RID: 4381
			public int vertexCount;

			// Token: 0x0400111E RID: 4382
			public int indexCount;
		}
	}
}
