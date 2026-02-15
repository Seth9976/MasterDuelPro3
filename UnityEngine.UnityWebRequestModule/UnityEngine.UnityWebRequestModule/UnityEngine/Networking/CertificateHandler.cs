using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x02000005 RID: 5
	[NativeHeader("Modules/UnityWebRequest/Public/CertificateHandler/CertificateHandlerScript.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class CertificateHandler : IDisposable
	{
		// Token: 0x06000011 RID: 17
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create([Unmarshalled] CertificateHandler obj);

		// Token: 0x06000012 RID: 18 RVA: 0x000028C0 File Offset: 0x00000AC0
		[NativeMethod(IsThreadSafe = true)]
		private void ReleaseFromScripting()
		{
			IntPtr intPtr = CertificateHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CertificateHandler.ReleaseFromScripting_Injected(intPtr);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000028E2 File Offset: 0x00000AE2
		protected CertificateHandler()
		{
			this.m_Ptr = CertificateHandler.Create(this);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000028F8 File Offset: 0x00000AF8
		~CertificateHandler()
		{
			this.Dispose();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002928 File Offset: 0x00000B28
		protected virtual bool ValidateCertificate(byte[] certificateData)
		{
			return false;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000293C File Offset: 0x00000B3C
		[RequiredByNativeCode]
		internal bool ValidateCertificateNative(byte[] certificateData)
		{
			return this.ValidateCertificate(certificateData);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002958 File Offset: 0x00000B58
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.ReleaseFromScripting();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseFromScripting_Injected(IntPtr _unity_self);

		// Token: 0x04000013 RID: 19
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x02000006 RID: 6
		internal static class BindingsMarshaller
		{
			// Token: 0x06000019 RID: 25 RVA: 0x0000298E File Offset: 0x00000B8E
			public static IntPtr ConvertToNative(CertificateHandler handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}
