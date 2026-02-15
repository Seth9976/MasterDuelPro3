using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule.NativeRenderPassCompiler
{
	// Token: 0x0200029B RID: 667
	internal struct ResourceVersionedData
	{
		// Token: 0x060011D1 RID: 4561 RVA: 0x00043F4F File Offset: 0x0004214F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetWritingPass(CompilerContextData ctx, ResourceHandle h, int passId)
		{
			this.writePassId = passId;
			this.written = true;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00043F60 File Offset: 0x00042160
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RegisterReadingPass(CompilerContextData ctx, ResourceHandle h, int passId, int index)
		{
			ctx.resources.readerData[h.iType][ResourcesData.IndexReader(h, this.numReaders)] = new ResourceReaderData
			{
				passId = passId,
				inputSlot = index
			};
			this.numReaders++;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00043FC0 File Offset: 0x000421C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveReadingPass(CompilerContextData ctx, ResourceHandle h, int passId)
		{
			int r = 0;
			while (r < this.numReaders)
			{
				ref ResourceReaderData reader = ref ctx.resources.readerData[h.iType].ElementAt(ResourcesData.IndexReader(h, r));
				if (reader.passId == passId)
				{
					if (r < this.numReaders - 1)
					{
						reader = ctx.resources.readerData[h.iType][ResourcesData.IndexReader(h, this.numReaders - 1)];
					}
					this.numReaders--;
				}
				else
				{
					r++;
				}
			}
		}

		// Token: 0x04000BF2 RID: 3058
		public bool written;

		// Token: 0x04000BF3 RID: 3059
		public int writePassId;

		// Token: 0x04000BF4 RID: 3060
		public int numReaders;
	}
}
