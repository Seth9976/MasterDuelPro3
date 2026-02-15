using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerBuffer.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerBuffer : DownloadHandler
	{
		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create([Unmarshalled] DownloadHandlerBuffer obj);

		// Token: 0x06000038 RID: 56 RVA: 0x00002E25 File Offset: 0x00001025
		private void InternalCreateBuffer()
		{
			this.m_Ptr = DownloadHandlerBuffer.Create(this);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002E34 File Offset: 0x00001034
		public DownloadHandlerBuffer()
		{
			this.InternalCreateBuffer();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002E48 File Offset: 0x00001048
		protected override NativeArray<byte> GetNativeData()
		{
			return DownloadHandler.InternalGetNativeArray(this, ref this.m_NativeData);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002E66 File Offset: 0x00001066
		public override void Dispose()
		{
			DownloadHandler.DisposeNativeArray(ref this.m_NativeData);
			base.Dispose();
		}

		// Token: 0x04000015 RID: 21
		private NativeArray<byte> m_NativeData;
	}
}
