using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B8 RID: 952
	[MovedFrom("UnityEngine.Rendering.RendererUtils")]
	[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/RendererList.h")]
	public struct RendererList
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060019AF RID: 6575
		public extern bool isValid
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x000380D6 File Offset: 0x000362D6
		internal RendererList(UIntPtr ctx, uint indx)
		{
			this.context = ctx;
			this.index = indx;
			this.frame = 0U;
			this.type = 0U;
			this.contextID = 0U;
		}

		// Token: 0x04000C28 RID: 3112
		internal UIntPtr context;

		// Token: 0x04000C29 RID: 3113
		internal uint index;

		// Token: 0x04000C2A RID: 3114
		internal uint frame;

		// Token: 0x04000C2B RID: 3115
		internal uint type;

		// Token: 0x04000C2C RID: 3116
		internal uint contextID;

		// Token: 0x04000C2D RID: 3117
		public static readonly RendererList nullRendererList = new RendererList(UIntPtr.Zero, uint.MaxValue);
	}
}
