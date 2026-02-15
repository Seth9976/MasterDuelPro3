using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200055F RID: 1375
	internal class TextureSlotManager
	{
		// Token: 0x060025DA RID: 9690 RVA: 0x00096A14 File Offset: 0x00094C14
		static TextureSlotManager()
		{
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				TextureSlotManager.slotIds[i] = Shader.PropertyToID(string.Format("_Texture{0}", i));
			}
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x00096A7C File Offset: 0x00094C7C
		public TextureSlotManager()
		{
			this.m_Textures = new TextureId[TextureSlotManager.k_SlotCount];
			this.m_Tickets = new int[TextureSlotManager.k_SlotCount];
			this.m_GpuTextures = new Vector4[TextureSlotManager.k_SlotCount * TextureSlotManager.k_SlotSize];
			this.Reset();
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x00096AE4 File Offset: 0x00094CE4
		public void Reset()
		{
			this.m_CurrentTicket = 0;
			this.m_FirstUsedTicket = 0;
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				this.m_Textures[i] = TextureId.invalid;
				this.m_Tickets[i] = -1;
				this.SetGpuData(i, TextureId.invalid, 1, 1, 0f, 0f);
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x00096B4C File Offset: 0x00094D4C
		public void StartNewBatch()
		{
			int num = this.m_CurrentTicket + 1;
			this.m_CurrentTicket = num;
			this.m_FirstUsedTicket = num;
			this.FreeSlots = TextureSlotManager.k_SlotCount;
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x00096B80 File Offset: 0x00094D80
		public int IndexOf(TextureId id)
		{
			for (int i = 0; i < TextureSlotManager.k_SlotCount; i++)
			{
				bool flag = this.m_Textures[i].index == id.index;
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x00096BCC File Offset: 0x00094DCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MarkUsed(int slotIndex)
		{
			int oldTicket = this.m_Tickets[slotIndex];
			bool flag = oldTicket < this.m_FirstUsedTicket;
			int num;
			if (flag)
			{
				num = this.FreeSlots - 1;
				this.FreeSlots = num;
			}
			int[] tickets = this.m_Tickets;
			num = this.m_CurrentTicket + 1;
			this.m_CurrentTicket = num;
			tickets[slotIndex] = num;
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x00096C1A File Offset: 0x00094E1A
		// (set) Token: 0x060025E1 RID: 9697 RVA: 0x00096C22 File Offset: 0x00094E22
		public int FreeSlots { get; private set; } = TextureSlotManager.k_SlotCount;

		// Token: 0x060025E2 RID: 9698 RVA: 0x00096C2C File Offset: 0x00094E2C
		public int FindOldestSlot()
		{
			int ticket = this.m_Tickets[0];
			int slot = 0;
			for (int i = 1; i < TextureSlotManager.k_SlotCount; i++)
			{
				bool flag = this.m_Tickets[i] < ticket;
				if (flag)
				{
					ticket = this.m_Tickets[i];
					slot = i;
				}
			}
			return slot;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x00096C80 File Offset: 0x00094E80
		public void Bind(TextureId id, float sdfScale, float sharpness, int slot, MaterialPropertyBlock mat, CommandList commandList = null)
		{
			Texture tex = this.textureRegistry.GetTexture(id);
			bool flag = tex == null;
			if (flag)
			{
				tex = Texture2D.whiteTexture;
			}
			this.m_Textures[slot] = id;
			this.MarkUsed(slot);
			this.SetGpuData(slot, id, tex.width, tex.height, sdfScale, sharpness);
			bool flag2 = commandList == null;
			if (flag2)
			{
				mat.SetTexture(TextureSlotManager.slotIds[slot], tex);
				mat.SetVectorArray(TextureSlotManager.textureTableId, this.m_GpuTextures);
			}
			else
			{
				int offset = slot * TextureSlotManager.k_SlotSize;
				commandList.SetTexture(TextureSlotManager.slotIds[slot], tex, offset, this.m_GpuTextures[offset], this.m_GpuTextures[offset + 1]);
			}
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x00096D44 File Offset: 0x00094F44
		public void SetGpuData(int slotIndex, TextureId id, int textureWidth, int textureHeight, float sdfScale, float sharpness)
		{
			int offset = slotIndex * TextureSlotManager.k_SlotSize;
			float texelWidth = 1f / (float)textureWidth;
			float texelHeight = 1f / (float)textureHeight;
			this.m_GpuTextures[offset] = new Vector4(id.ConvertToGpu(), texelWidth, texelHeight, sdfScale);
			this.m_GpuTextures[offset + 1] = new Vector4((float)textureWidth, (float)textureHeight, sharpness, 0f);
		}

		// Token: 0x0400130B RID: 4875
		internal static readonly int k_SlotCount = 8;

		// Token: 0x0400130C RID: 4876
		internal static readonly int k_SlotSize = 2;

		// Token: 0x0400130D RID: 4877
		internal static readonly int[] slotIds = new int[TextureSlotManager.k_SlotCount];

		// Token: 0x0400130E RID: 4878
		internal static readonly int textureTableId = Shader.PropertyToID("_TextureInfo");

		// Token: 0x0400130F RID: 4879
		private TextureId[] m_Textures;

		// Token: 0x04001310 RID: 4880
		private int[] m_Tickets;

		// Token: 0x04001311 RID: 4881
		private int m_CurrentTicket;

		// Token: 0x04001312 RID: 4882
		private int m_FirstUsedTicket;

		// Token: 0x04001313 RID: 4883
		private Vector4[] m_GpuTextures;

		// Token: 0x04001315 RID: 4885
		internal TextureRegistry textureRegistry = TextureRegistry.instance;
	}
}
