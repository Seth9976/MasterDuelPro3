using System;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000525 RID: 1317
	internal class EntryRecorder
	{
		// Token: 0x06002475 RID: 9333 RVA: 0x0008ACE5 File Offset: 0x00088EE5
		public EntryRecorder(EntryPool entryPool)
		{
			Debug.Assert(entryPool != null);
			this.m_EntryPool = entryPool;
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x0008AD00 File Offset: 0x00088F00
		public void DrawMesh(Entry parentEntry, NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture, bool skipAtlas)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.vertices = vertices;
			entry.indices = indices;
			entry.texture = texture;
			bool flag = texture == null;
			if (flag)
			{
				entry.type = EntryType.DrawSolidMesh;
			}
			else
			{
				entry.type = (skipAtlas ? EntryType.DrawTexturedMeshSkipAtlas : EntryType.DrawTexturedMesh);
			}
			EntryRecorder.AppendMeshEntry(parentEntry, entry);
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x0008AD58 File Offset: 0x00088F58
		public void DrawRasterText(Entry parentEntry, NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture, bool multiChannel)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = (multiChannel ? EntryType.DrawTexturedMeshSkipAtlas : EntryType.DrawTextMesh);
			entry.flags = EntryFlags.UsesTextCoreSettings;
			entry.vertices = vertices;
			entry.indices = indices;
			entry.texture = texture;
			entry.textScale = 0f;
			entry.fontSharpness = 0f;
			EntryRecorder.AppendMeshEntry(parentEntry, entry);
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x0008ADBC File Offset: 0x00088FBC
		public void DrawSdfText(Entry parentEntry, NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture, float scale, float sharpness)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.DrawTextMesh;
			entry.flags = EntryFlags.UsesTextCoreSettings;
			entry.vertices = vertices;
			entry.indices = indices;
			entry.texture = texture;
			entry.textScale = scale;
			entry.fontSharpness = sharpness;
			EntryRecorder.AppendMeshEntry(parentEntry, entry);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x0008AE14 File Offset: 0x00089014
		public void DrawGradients(Entry parentEntry, NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, VectorImage gradientsOwner)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.DrawGradients;
			entry.vertices = vertices;
			entry.indices = indices;
			entry.gradientsOwner = gradientsOwner;
			EntryRecorder.AppendMeshEntry(parentEntry, entry);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x0008AE54 File Offset: 0x00089054
		public void DrawImmediate(Entry parentEntry, Action callback, bool cullingEnabled)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = (cullingEnabled ? EntryType.DrawImmediateCull : EntryType.DrawImmediate);
			entry.immediateCallback = callback;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x0008AE8C File Offset: 0x0008908C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DrawChildren(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.DrawChildren;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x0008AEB8 File Offset: 0x000890B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void BeginStencilMask(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.BeginStencilMask;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x0008AEE4 File Offset: 0x000890E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void EndStencilMask(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.EndStencilMask;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0008AF10 File Offset: 0x00089110
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PopStencilMask(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PopStencilMask;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x0008AF3C File Offset: 0x0008913C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushClippingRect(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PushClippingRect;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x0008AF68 File Offset: 0x00089168
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PopClippingRect(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PopClippingRect;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x0008AF94 File Offset: 0x00089194
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushScissors(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PushScissors;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x0008AFC0 File Offset: 0x000891C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PopScissors(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PopScissors;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x0008AFEC File Offset: 0x000891EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushGroupMatrix(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PushGroupMatrix;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x0008B018 File Offset: 0x00089218
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PopGroupMatrix(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PopGroupMatrix;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x0008B044 File Offset: 0x00089244
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushRenderTexture(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PushRenderTexture;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x0008B070 File Offset: 0x00089270
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void BlitAndPopRenderTexture(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.BlitAndPopRenderTexture;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x0008B09C File Offset: 0x0008929C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushDefaultMaterial(Entry parentEntry, Material material)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PushDefaultMaterial;
			entry.material = material;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0008B0D0 File Offset: 0x000892D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PopDefaultMaterial(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PopDefaultMaterial;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x0008B0FC File Offset: 0x000892FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CutRenderChain(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.CutRenderChain;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x0008B128 File Offset: 0x00089328
		public Entry InsertPlaceholder(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.DedicatedPlaceholder;
			EntryRecorder.Append(parentEntry, entry);
			return entry;
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x0008B158 File Offset: 0x00089358
		private static void AppendMeshEntry(Entry parentEntry, Entry entry)
		{
			int vertexCount = entry.vertices.Length;
			int indexCount = entry.indices.Length;
			bool flag = vertexCount == 0;
			if (flag)
			{
				Debug.LogError("Attempting to add an entry without vertices.");
			}
			else
			{
				bool flag2 = (long)vertexCount > (long)((ulong)UIRenderDevice.maxVerticesPerPage);
				if (flag2)
				{
					Debug.LogError(string.Format("Attempting to add an entry with {0} vertices. The maximum number of vertices per entry is {1}.", vertexCount, UIRenderDevice.maxVerticesPerPage));
				}
				else
				{
					bool flag3 = indexCount == 0;
					if (flag3)
					{
						Debug.LogError("Attempting to add an entry without indices.");
					}
					else
					{
						EntryRecorder.Append(parentEntry, entry);
					}
				}
			}
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0008B1E8 File Offset: 0x000893E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Append(Entry parentEntry, Entry entry)
		{
			bool flag = parentEntry.lastChild == null;
			if (flag)
			{
				Debug.Assert(parentEntry.firstChild == null);
				parentEntry.firstChild = entry;
				parentEntry.lastChild = entry;
			}
			else
			{
				parentEntry.lastChild.nextSibling = entry;
				parentEntry.lastChild = entry;
			}
		}

		// Token: 0x04001173 RID: 4467
		private EntryPool m_EntryPool;
	}
}
