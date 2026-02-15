using System;
using System.Runtime.InteropServices;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x02000022 RID: 34
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 2)]
	public readonly struct ProfilerCategory
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002B59 File Offset: 0x00000D59
		internal ProfilerCategory(ushort category)
		{
			this.m_CategoryId = category;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002B64 File Offset: 0x00000D64
		public string Name
		{
			get
			{
				ProfilerCategoryDescription desc = ProfilerUnsafeUtility.GetCategoryDescription(this.m_CategoryId);
				return ProfilerUnsafeUtility.Utf8ToString(desc.NameUtf8, desc.NameUtf8Len);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002B94 File Offset: 0x00000D94
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002BAC File Offset: 0x00000DAC
		public static ProfilerCategory Scripts
		{
			get
			{
				return new ProfilerCategory(1);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002BB4 File Offset: 0x00000DB4
		public static ProfilerCategory Loading
		{
			get
			{
				return new ProfilerCategory(15);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public static implicit operator ushort(ProfilerCategory category)
		{
			return category.m_CategoryId;
		}

		// Token: 0x04000035 RID: 53
		[FieldOffset(0)]
		private readonly ushort m_CategoryId;
	}
}
