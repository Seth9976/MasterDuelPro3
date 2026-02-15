using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AF RID: 687
	internal struct TextureId
	{
		// Token: 0x06001294 RID: 4756 RVA: 0x0004D341 File Offset: 0x0004B541
		public TextureId(int index)
		{
			this.m_Index = index + 1;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x0004D34D File Offset: 0x0004B54D
		public int index
		{
			get
			{
				return this.m_Index - 1;
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0004D358 File Offset: 0x0004B558
		public float ConvertToGpu()
		{
			return (float)this.index;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0004D374 File Offset: 0x0004B574
		public override bool Equals(object obj)
		{
			bool flag = !(obj is TextureId);
			return !flag && (TextureId)obj == this;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0004D3AC File Offset: 0x0004B5AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.m_Index.GetHashCode();
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0004D3CC File Offset: 0x0004B5CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(TextureId left, TextureId right)
		{
			return left.m_Index == right.m_Index;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0004D3EC File Offset: 0x0004B5EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(TextureId left, TextureId right)
		{
			return !(left == right);
		}

		// Token: 0x04000AC3 RID: 2755
		private readonly int m_Index;

		// Token: 0x04000AC4 RID: 2756
		public static readonly TextureId invalid = new TextureId(-1);
	}
}
