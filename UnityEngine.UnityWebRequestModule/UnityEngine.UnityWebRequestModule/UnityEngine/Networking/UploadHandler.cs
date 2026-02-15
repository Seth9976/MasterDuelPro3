using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandler.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class UploadHandler : IDisposable
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00003D14 File Offset: 0x00001F14
		[NativeMethod(IsThreadSafe = true)]
		private void ReleaseFromScripting()
		{
			IntPtr intPtr = UploadHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			UploadHandler.ReleaseFromScripting_Injected(intPtr);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000029BA File Offset: 0x00000BBA
		internal UploadHandler()
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003D38 File Offset: 0x00001F38
		~UploadHandler()
		{
			this.Dispose();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003D68 File Offset: 0x00001F68
		public virtual void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.ReleaseFromScripting();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x17000018 RID: 24
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003D9E File Offset: 0x00001F9E
		public string contentType
		{
			set
			{
				this.SetContentType(value);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003DA9 File Offset: 0x00001FA9
		internal virtual void SetContentType(string newContentType)
		{
			this.InternalSetContentType(newContentType);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003DB4 File Offset: 0x00001FB4
		[NativeMethod("SetContentType")]
		private unsafe void InternalSetContentType(string newContentType)
		{
			try
			{
				IntPtr intPtr = UploadHandler.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(newContentType, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = newContentType.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				UploadHandler.InternalSetContentType_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060000A7 RID: 167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseFromScripting_Injected(IntPtr _unity_self);

		// Token: 0x060000A8 RID: 168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetContentType_Injected(IntPtr _unity_self, ref ManagedSpanWrapper newContentType);

		// Token: 0x04000055 RID: 85
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x02000012 RID: 18
		internal static class BindingsMarshaller
		{
			// Token: 0x060000A9 RID: 169 RVA: 0x00003E18 File Offset: 0x00002018
			public static IntPtr ConvertToNative(UploadHandler uploadHandler)
			{
				return uploadHandler.m_Ptr;
			}
		}
	}
}
