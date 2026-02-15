using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000013 RID: 19
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandlerRaw.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class UploadHandlerRaw : UploadHandler
	{
		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr Create([Unmarshalled] UploadHandlerRaw self, byte* data, int dataLength);

		// Token: 0x060000AB RID: 171 RVA: 0x00003E20 File Offset: 0x00002020
		public UploadHandlerRaw(byte[] data)
			: this((data == null || data.Length == 0) ? default(NativeArray<byte>) : new NativeArray<byte>(data, Allocator.Persistent), true)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003E50 File Offset: 0x00002050
		public unsafe UploadHandlerRaw(NativeArray<byte> data, bool transferOwnership)
		{
			bool flag = !data.IsCreated || data.Length == 0;
			if (flag)
			{
				this.m_Ptr = UploadHandlerRaw.Create(this, null, 0);
			}
			else
			{
				if (transferOwnership)
				{
					this.m_Payload = data;
				}
				this.m_Ptr = UploadHandlerRaw.Create(this, (byte*)data.GetUnsafeReadOnlyPtr<byte>(), data.Length);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003EBC File Offset: 0x000020BC
		public override void Dispose()
		{
			bool isCreated = this.m_Payload.IsCreated;
			if (isCreated)
			{
				this.m_Payload.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x04000056 RID: 86
		private NativeArray<byte> m_Payload;
	}
}
