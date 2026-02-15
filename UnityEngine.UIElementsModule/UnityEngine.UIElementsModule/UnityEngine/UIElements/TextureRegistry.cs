using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B0 RID: 688
	internal class TextureRegistry
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x0004D415 File Offset: 0x0004B615
		public static TextureRegistry instance { get; } = new TextureRegistry();

		// Token: 0x0600129D RID: 4765 RVA: 0x0004D41C File Offset: 0x0004B61C
		public Texture GetTexture(TextureId id)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			Texture texture;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to get an invalid texture (index={0}).", id.index));
				texture = null;
			}
			else
			{
				TextureRegistry.TextureInfo info = this.m_Textures[id.index];
				bool flag2 = info.refCount < 1;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to get a texture (index={0}) that is not allocated.", id.index));
					texture = null;
				}
				else
				{
					texture = info.texture;
				}
			}
			return texture;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0004D4C0 File Offset: 0x0004B6C0
		public TextureId AllocAndAcquireDynamic()
		{
			return this.AllocAndAcquire(null, true);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0004D4DC File Offset: 0x0004B6DC
		public void UpdateDynamic(TextureId id, Texture texture)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to update an invalid dynamic texture (index={0}).", id.index));
			}
			else
			{
				TextureRegistry.TextureInfo info = this.m_Textures[id.index];
				bool flag2 = !info.dynamic;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to update a texture (index={0}) that is not dynamic.", id.index));
				}
				else
				{
					bool flag3 = info.refCount < 1;
					if (flag3)
					{
						Debug.LogError(string.Format("Attempted to update a dynamic texture (index={0}) that is not allocated.", id.index));
					}
					else
					{
						info.texture = texture;
						this.m_Textures[id.index] = info;
					}
				}
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0004D5BC File Offset: 0x0004B7BC
		private TextureId AllocAndAcquire(Texture texture, bool dynamic)
		{
			TextureRegistry.TextureInfo info = new TextureRegistry.TextureInfo
			{
				texture = texture,
				dynamic = dynamic,
				refCount = 1
			};
			bool flag = this.m_FreeIds.Count > 0;
			TextureId id;
			if (flag)
			{
				id = this.m_FreeIds.Pop();
				this.m_Textures[id.index] = info;
			}
			else
			{
				bool flag2 = this.m_Textures.Count == 2048;
				if (flag2)
				{
					Debug.LogError(string.Format("Failed to allocate a {0} because the limit of {1} textures is reached.", "TextureId", 2048));
					return TextureId.invalid;
				}
				id = new TextureId(this.m_Textures.Count);
				this.m_Textures.Add(info);
			}
			bool flag3 = !dynamic;
			if (flag3)
			{
				this.m_TextureToId[texture] = id;
			}
			return id;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0004D6A4 File Offset: 0x0004B8A4
		public TextureId Acquire(Texture tex)
		{
			TextureId id;
			bool flag = this.m_TextureToId.TryGetValue(tex, out id);
			TextureId textureId;
			if (flag)
			{
				TextureRegistry.TextureInfo info = this.m_Textures[id.index];
				Debug.Assert(info.refCount > 0);
				Debug.Assert(!info.dynamic);
				info.refCount++;
				this.m_Textures[id.index] = info;
				textureId = id;
			}
			else
			{
				textureId = this.AllocAndAcquire(tex, false);
			}
			return textureId;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0004D728 File Offset: 0x0004B928
		public void Release(TextureId id)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to release an invalid texture (index={0}).", id.index));
			}
			else
			{
				TextureRegistry.TextureInfo info = this.m_Textures[id.index];
				bool flag2 = info.refCount < 1;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to release a texture (index={0}) that is not allocated.", id.index));
				}
				else
				{
					info.refCount--;
					bool flag3 = info.refCount == 0;
					if (flag3)
					{
						bool flag4 = !info.dynamic;
						if (flag4)
						{
							this.m_TextureToId.Remove(info.texture);
						}
						info.texture = null;
						info.dynamic = false;
						this.m_FreeIds.Push(id);
					}
					this.m_Textures[id.index] = info;
				}
			}
		}

		// Token: 0x04000AC5 RID: 2757
		private List<TextureRegistry.TextureInfo> m_Textures = new List<TextureRegistry.TextureInfo>(128);

		// Token: 0x04000AC6 RID: 2758
		private Dictionary<Texture, TextureId> m_TextureToId = new Dictionary<Texture, TextureId>(128);

		// Token: 0x04000AC7 RID: 2759
		private Stack<TextureId> m_FreeIds = new Stack<TextureId>();

		// Token: 0x04000AC8 RID: 2760
		internal const int maxTextures = 2048;

		// Token: 0x020002B1 RID: 689
		private struct TextureInfo
		{
			// Token: 0x04000ACA RID: 2762
			public Texture texture;

			// Token: 0x04000ACB RID: 2763
			public bool dynamic;

			// Token: 0x04000ACC RID: 2764
			public int refCount;
		}
	}
}
