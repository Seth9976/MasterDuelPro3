using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000096 RID: 150
	[Serializable]
	public class TMP_TextInfo
	{
		// Token: 0x06000589 RID: 1417 RVA: 0x00023100 File Offset: 0x00021300
		public TMP_TextInfo()
		{
			this.characterInfo = new TMP_CharacterInfo[8];
			this.wordInfo = new TMP_WordInfo[16];
			this.linkInfo = new TMP_LinkInfo[0];
			this.lineInfo = new TMP_LineInfo[2];
			this.pageInfo = new TMP_PageInfo[4];
			this.meshInfo = new TMP_MeshInfo[1];
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0002315C File Offset: 0x0002135C
		internal TMP_TextInfo(int characterCount)
		{
			this.characterInfo = new TMP_CharacterInfo[characterCount];
			this.wordInfo = new TMP_WordInfo[16];
			this.linkInfo = new TMP_LinkInfo[0];
			this.lineInfo = new TMP_LineInfo[2];
			this.pageInfo = new TMP_PageInfo[4];
			this.meshInfo = new TMP_MeshInfo[1];
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000231B8 File Offset: 0x000213B8
		public TMP_TextInfo(TMP_Text textComponent)
		{
			this.textComponent = textComponent;
			this.characterInfo = new TMP_CharacterInfo[8];
			this.wordInfo = new TMP_WordInfo[4];
			this.linkInfo = new TMP_LinkInfo[0];
			this.lineInfo = new TMP_LineInfo[2];
			this.pageInfo = new TMP_PageInfo[4];
			this.meshInfo = new TMP_MeshInfo[1];
			this.meshInfo[0].mesh = textComponent.mesh;
			this.materialCount = 1;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00023238 File Offset: 0x00021438
		internal void Clear()
		{
			this.characterCount = 0;
			this.spaceCount = 0;
			this.wordCount = 0;
			this.linkCount = 0;
			this.lineCount = 0;
			this.pageCount = 0;
			this.spriteCount = 0;
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].vertexCount = 0;
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0002329C File Offset: 0x0002149C
		internal void ClearAllData()
		{
			this.characterCount = 0;
			this.spaceCount = 0;
			this.wordCount = 0;
			this.linkCount = 0;
			this.lineCount = 0;
			this.pageCount = 0;
			this.spriteCount = 0;
			this.characterInfo = new TMP_CharacterInfo[4];
			this.wordInfo = new TMP_WordInfo[1];
			this.lineInfo = new TMP_LineInfo[1];
			this.pageInfo = new TMP_PageInfo[1];
			this.linkInfo = new TMP_LinkInfo[0];
			this.materialCount = 0;
			this.meshInfo = new TMP_MeshInfo[1];
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0002332C File Offset: 0x0002152C
		public void ClearMeshInfo(bool updateMesh)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(updateMesh);
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00023360 File Offset: 0x00021560
		public void ClearAllMeshInfo()
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(true);
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00023394 File Offset: 0x00021594
		public void ResetVertexLayout(bool isVolumetric)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].ResizeMeshInfo(0, isVolumetric);
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000233C8 File Offset: 0x000215C8
		public void ClearUnusedVertices(MaterialReference[] materials)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				int start = 0;
				this.meshInfo[i].ClearUnusedVertices(start);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000233FC File Offset: 0x000215FC
		internal void ClearLineInfo()
		{
			if (this.lineInfo == null)
			{
				this.lineInfo = new TMP_LineInfo[1];
			}
			int length = this.lineInfo.Length;
			for (int i = 0; i < length; i++)
			{
				this.lineInfo[i].characterCount = 0;
				this.lineInfo[i].spaceCount = 0;
				this.lineInfo[i].wordCount = 0;
				this.lineInfo[i].controlCharacterCount = 0;
				this.lineInfo[i].visibleCharacterCount = 0;
				this.lineInfo[i].visibleSpaceCount = 0;
				this.lineInfo[i].ascender = TMP_TextInfo.k_InfinityVectorNegative.x;
				this.lineInfo[i].baseline = 0f;
				this.lineInfo[i].descender = TMP_TextInfo.k_InfinityVectorPositive.x;
				this.lineInfo[i].maxAdvance = 0f;
				this.lineInfo[i].marginLeft = 0f;
				this.lineInfo[i].marginRight = 0f;
				this.lineInfo[i].lineExtents.min = TMP_TextInfo.k_InfinityVectorPositive;
				this.lineInfo[i].lineExtents.max = TMP_TextInfo.k_InfinityVectorNegative;
				this.lineInfo[i].width = 0f;
			}
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00023580 File Offset: 0x00021780
		internal void ClearPageInfo()
		{
			if (this.pageInfo == null)
			{
				this.pageInfo = new TMP_PageInfo[2];
			}
			int length = this.pageInfo.Length;
			for (int i = 0; i < length; i++)
			{
				this.pageInfo[i].firstCharacterIndex = 0;
				this.pageInfo[i].lastCharacterIndex = 0;
				this.pageInfo[i].ascender = -32767f;
				this.pageInfo[i].baseLine = 0f;
				this.pageInfo[i].descender = 32767f;
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0002361C File Offset: 0x0002181C
		public TMP_MeshInfo[] CopyMeshInfoVertexData()
		{
			if (this.m_CachedMeshInfo == null || this.m_CachedMeshInfo.Length != this.meshInfo.Length)
			{
				this.m_CachedMeshInfo = new TMP_MeshInfo[this.meshInfo.Length];
				for (int i = 0; i < this.m_CachedMeshInfo.Length; i++)
				{
					int length = this.meshInfo[i].vertices.Length;
					this.m_CachedMeshInfo[i].vertices = new Vector3[length];
					this.m_CachedMeshInfo[i].uvs0 = new Vector4[length];
					this.m_CachedMeshInfo[i].uvs2 = new Vector2[length];
					this.m_CachedMeshInfo[i].colors32 = new Color32[length];
				}
			}
			for (int j = 0; j < this.m_CachedMeshInfo.Length; j++)
			{
				int length2 = this.meshInfo[j].vertices.Length;
				if (this.m_CachedMeshInfo[j].vertices.Length != length2)
				{
					this.m_CachedMeshInfo[j].vertices = new Vector3[length2];
					this.m_CachedMeshInfo[j].uvs0 = new Vector4[length2];
					this.m_CachedMeshInfo[j].uvs2 = new Vector2[length2];
					this.m_CachedMeshInfo[j].colors32 = new Color32[length2];
				}
				Array.Copy(this.meshInfo[j].vertices, this.m_CachedMeshInfo[j].vertices, length2);
				Array.Copy(this.meshInfo[j].uvs0, this.m_CachedMeshInfo[j].uvs0, length2);
				Array.Copy(this.meshInfo[j].uvs2, this.m_CachedMeshInfo[j].uvs2, length2);
				Array.Copy(this.meshInfo[j].colors32, this.m_CachedMeshInfo[j].colors32, length2);
			}
			return this.m_CachedMeshInfo;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00023824 File Offset: 0x00021A24
		public static void Resize<T>(ref T[] array, int size)
		{
			int newSize = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			Array.Resize<T>(ref array, newSize);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00023850 File Offset: 0x00021A50
		public static void Resize<T>(ref T[] array, int size, bool isBlockAllocated)
		{
			if (isBlockAllocated)
			{
				size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			}
			if (size == array.Length)
			{
				return;
			}
			Array.Resize<T>(ref array, size);
		}

		// Token: 0x04000546 RID: 1350
		internal static Vector2 k_InfinityVectorPositive = new Vector2(32767f, 32767f);

		// Token: 0x04000547 RID: 1351
		internal static Vector2 k_InfinityVectorNegative = new Vector2(-32767f, -32767f);

		// Token: 0x04000548 RID: 1352
		public TMP_Text textComponent;

		// Token: 0x04000549 RID: 1353
		public int characterCount;

		// Token: 0x0400054A RID: 1354
		public int spriteCount;

		// Token: 0x0400054B RID: 1355
		public int spaceCount;

		// Token: 0x0400054C RID: 1356
		public int wordCount;

		// Token: 0x0400054D RID: 1357
		public int linkCount;

		// Token: 0x0400054E RID: 1358
		public int lineCount;

		// Token: 0x0400054F RID: 1359
		public int pageCount;

		// Token: 0x04000550 RID: 1360
		public int materialCount;

		// Token: 0x04000551 RID: 1361
		public TMP_CharacterInfo[] characterInfo;

		// Token: 0x04000552 RID: 1362
		public TMP_WordInfo[] wordInfo;

		// Token: 0x04000553 RID: 1363
		public TMP_LinkInfo[] linkInfo;

		// Token: 0x04000554 RID: 1364
		public TMP_LineInfo[] lineInfo;

		// Token: 0x04000555 RID: 1365
		public TMP_PageInfo[] pageInfo;

		// Token: 0x04000556 RID: 1366
		public TMP_MeshInfo[] meshInfo;

		// Token: 0x04000557 RID: 1367
		private TMP_MeshInfo[] m_CachedMeshInfo;
	}
}
