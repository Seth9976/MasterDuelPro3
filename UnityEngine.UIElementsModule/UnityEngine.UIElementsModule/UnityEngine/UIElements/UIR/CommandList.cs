using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000503 RID: 1283
	internal class CommandList : IDisposable
	{
		// Token: 0x060023C5 RID: 9157 RVA: 0x00083634 File Offset: 0x00081834
		public CommandList(VisualElement owner, IntPtr vertexDecl, IntPtr stencilState)
		{
			this.m_Owner = owner;
			this.m_VertexDecl = vertexDecl;
			this.m_StencilState = stencilState;
			this.m_DrawRanges = new NativeList<DrawBufferRange>(1024);
			this.handle = GCHandle.Alloc(this);
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000836B4 File Offset: 0x000818B4
		public void Reset(VisualElement newOwner)
		{
			this.m_Owner = newOwner;
			this.m_Commands.Clear();
			this.m_DrawRanges.Clear();
			for (int i = 0; i < this.m_GpuTextureData.Length; i++)
			{
				this.m_GpuTextureData[i] = Vector4.zero;
			}
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x0008370C File Offset: 0x0008190C
		public unsafe void Execute()
		{
			IntPtr* vStream;
			checked
			{
				vStream = stackalloc IntPtr[unchecked((UIntPtr)1) * (UIntPtr)sizeof(IntPtr)];
				Utility.SetPropertyBlock(this.constantProps);
				Utility.SetStencilState(this.m_StencilState, 0);
			}
			for (int i = 0; i < this.m_Commands.Count; i++)
			{
				SerializedCommand cmd = this.m_Commands[i];
				switch (cmd.type)
				{
				case SerializedCommandType.DrawRanges:
					*vStream = cmd.vertexBuffer;
					Utility.DrawRanges(cmd.indexBuffer, vStream, 1, new IntPtr(this.m_DrawRanges.GetSlice(cmd.firstRange, cmd.rangeCount).GetUnsafePtr<DrawBufferRange>()), cmd.rangeCount, this.m_VertexDecl);
					break;
				case SerializedCommandType.SetTexture:
					this.batchProps.SetTexture(cmd.textureName, cmd.texture);
					this.m_GpuTextureData[cmd.gpuDataOffset] = cmd.gpuData0;
					this.m_GpuTextureData[cmd.gpuDataOffset + 1] = cmd.gpuData1;
					this.batchProps.SetVectorArray(TextureSlotManager.textureTableId, this.m_GpuTextureData);
					break;
				case SerializedCommandType.ApplyBatchProps:
					Utility.SetPropertyBlock(this.batchProps);
					break;
				default:
					throw new NotImplementedException();
				}
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x0008384C File Offset: 0x00081A4C
		public void SetTexture(int name, Texture texture, int gpuDataOffset, Vector4 gpuData0, Vector4 gpuData1)
		{
			SerializedCommand cmd = new SerializedCommand
			{
				type = SerializedCommandType.SetTexture,
				textureName = name,
				texture = texture,
				gpuDataOffset = gpuDataOffset,
				gpuData0 = gpuData0,
				gpuData1 = gpuData1
			};
			this.m_Commands.Add(cmd);
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000838A4 File Offset: 0x00081AA4
		public void ApplyBatchProps()
		{
			SerializedCommand cmd = new SerializedCommand
			{
				type = SerializedCommandType.ApplyBatchProps
			};
			this.m_Commands.Add(cmd);
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000838D4 File Offset: 0x00081AD4
		public void DrawRanges(Utility.GPUBuffer<ushort> ib, Utility.GPUBuffer<Vertex> vb, NativeSlice<DrawBufferRange> ranges)
		{
			SerializedCommand cmd = new SerializedCommand
			{
				type = SerializedCommandType.DrawRanges,
				vertexBuffer = vb.BufferPointer,
				indexBuffer = ib.BufferPointer,
				firstRange = this.m_DrawRanges.Count,
				rangeCount = ranges.Length
			};
			this.m_Commands.Add(cmd);
			this.m_DrawRanges.Add(ranges);
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x00083948 File Offset: 0x00081B48
		// (set) Token: 0x060023CC RID: 9164 RVA: 0x00083950 File Offset: 0x00081B50
		private protected bool disposed { protected get; private set; }

		// Token: 0x060023CD RID: 9165 RVA: 0x00083959 File Offset: 0x00081B59
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x0008396C File Offset: 0x00081B6C
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_DrawRanges.Dispose();
					this.m_DrawRanges = null;
					bool isAllocated = this.handle.IsAllocated;
					if (isAllocated)
					{
						this.handle.Free();
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x04001062 RID: 4194
		public VisualElement m_Owner;

		// Token: 0x04001063 RID: 4195
		private readonly IntPtr m_VertexDecl;

		// Token: 0x04001064 RID: 4196
		private readonly IntPtr m_StencilState;

		// Token: 0x04001065 RID: 4197
		public MaterialPropertyBlock constantProps = new MaterialPropertyBlock();

		// Token: 0x04001066 RID: 4198
		public MaterialPropertyBlock batchProps = new MaterialPropertyBlock();

		// Token: 0x04001067 RID: 4199
		public GCHandle handle;

		// Token: 0x04001068 RID: 4200
		private List<SerializedCommand> m_Commands = new List<SerializedCommand>();

		// Token: 0x04001069 RID: 4201
		private Vector4[] m_GpuTextureData = new Vector4[TextureSlotManager.k_SlotSize * TextureSlotManager.k_SlotCount];

		// Token: 0x0400106A RID: 4202
		private NativeList<DrawBufferRange> m_DrawRanges;
	}
}
