using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;

namespace Unity.Profiling
{
	// Token: 0x0200002E RID: 46
	public struct DebugScreenCapture
	{
		// Token: 0x17000011 RID: 17
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002E7F File Offset: 0x0000107F
		public NativeArray<byte> RawImageDataReference
		{
			[CompilerGenerated]
			set
			{
				this.<RawImageDataReference>k__BackingField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002E88 File Offset: 0x00001088
		public TextureFormat ImageFormat
		{
			[CompilerGenerated]
			set
			{
				this.<ImageFormat>k__BackingField = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002E91 File Offset: 0x00001091
		public int Width
		{
			[CompilerGenerated]
			set
			{
				this.<Width>k__BackingField = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002E9A File Offset: 0x0000109A
		public int Height
		{
			[CompilerGenerated]
			set
			{
				this.<Height>k__BackingField = value;
			}
		}
	}
}
