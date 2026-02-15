using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandler.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class DownloadHandler : IDisposable
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002998 File Offset: 0x00000B98
		[NativeMethod(IsThreadSafe = true)]
		private void ReleaseFromScripting()
		{
			IntPtr intPtr = DownloadHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			DownloadHandler.ReleaseFromScripting_Injected(intPtr);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000029BA File Offset: 0x00000BBA
		[VisibleToOtherModules]
		internal DownloadHandler()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000029C4 File Offset: 0x00000BC4
		~DownloadHandler()
		{
			this.Dispose();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000029F4 File Offset: 0x00000BF4
		public virtual void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.ReleaseFromScripting();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002A2C File Offset: 0x00000C2C
		public string error
		{
			get
			{
				return this.GetErrorMsg();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A44 File Offset: 0x00000C44
		private string GetErrorMsg()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = DownloadHandler.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				DownloadHandler.GetErrorMsg_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002A84 File Offset: 0x00000C84
		public byte[] data
		{
			get
			{
				return this.GetData();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002A9C File Offset: 0x00000C9C
		public string text
		{
			get
			{
				return this.GetText();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002AB4 File Offset: 0x00000CB4
		protected virtual NativeArray<byte> GetNativeData()
		{
			return default(NativeArray<byte>);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002AD0 File Offset: 0x00000CD0
		protected virtual byte[] GetData()
		{
			return DownloadHandler.InternalGetByteArray(this);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002AE8 File Offset: 0x00000CE8
		protected unsafe virtual string GetText()
		{
			NativeArray<byte> nativeData = this.GetNativeData();
			bool flag = nativeData.IsCreated && nativeData.Length > 0;
			string text;
			if (flag)
			{
				text = new string((sbyte*)nativeData.GetUnsafeReadOnlyPtr<byte>(), 0, nativeData.Length, this.GetTextEncoder());
			}
			else
			{
				text = "";
			}
			return text;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B40 File Offset: 0x00000D40
		private Encoding GetTextEncoder()
		{
			string contentType = this.GetContentType();
			bool flag = !string.IsNullOrEmpty(contentType);
			if (flag)
			{
				int charsetKeyIndex = contentType.IndexOf("charset", StringComparison.OrdinalIgnoreCase);
				bool flag2 = charsetKeyIndex > -1;
				if (flag2)
				{
					int charsetValueIndex = contentType.IndexOf('=', charsetKeyIndex);
					bool flag3 = charsetValueIndex > -1;
					if (flag3)
					{
						string encoding = contentType.Substring(charsetValueIndex + 1).Trim().Trim(new char[] { '\'', '"' })
							.Trim();
						int semicolonIndex = encoding.IndexOf(';');
						bool flag4 = semicolonIndex > -1;
						if (flag4)
						{
							encoding = encoding.Substring(0, semicolonIndex);
						}
						try
						{
							return Encoding.GetEncoding(encoding);
						}
						catch (ArgumentException e)
						{
							Debug.LogWarning(string.Format("Unsupported encoding '{0}': {1}", encoding, e.Message));
						}
						catch (NotSupportedException e2)
						{
							Debug.LogWarning(string.Format("Unsupported encoding '{0}': {1}", encoding, e2.Message));
						}
					}
				}
			}
			return Encoding.UTF8;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002C5C File Offset: 0x00000E5C
		private string GetContentType()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = DownloadHandler.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				DownloadHandler.GetContentType_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002C9C File Offset: 0x00000E9C
		[RequiredByNativeCode]
		protected virtual bool ReceiveData(byte[] data, int dataLength)
		{
			return true;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002CAF File Offset: 0x00000EAF
		[RequiredByNativeCode]
		protected virtual void ReceiveContentLengthHeader(ulong contentLength)
		{
			this.ReceiveContentLength((int)contentLength);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002CBB File Offset: 0x00000EBB
		[Obsolete("Use ReceiveContentLengthHeader")]
		protected virtual void ReceiveContentLength(int contentLength)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002CBB File Offset: 0x00000EBB
		[RequiredByNativeCode]
		protected virtual void CompleteContent()
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002CC0 File Offset: 0x00000EC0
		[RequiredByNativeCode]
		protected virtual float GetProgress()
		{
			return 0f;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002CD8 File Offset: 0x00000ED8
		protected static T GetCheckedDownloader<T>(UnityWebRequest www) where T : DownloadHandler
		{
			bool flag = www == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get content from a null UnityWebRequest object");
			}
			bool flag2 = !www.isDone;
			if (flag2)
			{
				throw new InvalidOperationException("Cannot get content from an unfinished UnityWebRequest object");
			}
			bool flag3 = www.result == UnityWebRequest.Result.ProtocolError;
			if (flag3)
			{
				throw new InvalidOperationException(www.error);
			}
			return (T)((object)www.downloadHandler);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002D3C File Offset: 0x00000F3C
		[NativeThrows]
		[VisibleToOtherModules]
		internal unsafe static byte* InternalGetByteArray(DownloadHandler dh, out int length)
		{
			return DownloadHandler.InternalGetByteArray_Injected((dh == null) ? ((IntPtr)0) : DownloadHandler.BindingsMarshaller.ConvertToNative(dh), out length);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D60 File Offset: 0x00000F60
		internal static byte[] InternalGetByteArray(DownloadHandler dh)
		{
			NativeArray<byte> nativeData = dh.GetNativeData();
			bool isCreated = nativeData.IsCreated;
			byte[] array;
			if (isCreated)
			{
				array = nativeData.ToArray();
			}
			else
			{
				array = null;
			}
			return array;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D90 File Offset: 0x00000F90
		[VisibleToOtherModules(new string[] { "UnityEngine.UnityWebRequestAudioModule", "UnityEngine.UnityWebRequestTextureModule" })]
		internal unsafe static NativeArray<byte> InternalGetNativeArray(DownloadHandler dh, ref NativeArray<byte> nativeArray)
		{
			int length;
			byte* bytes = DownloadHandler.InternalGetByteArray(dh, out length);
			bool isCreated = nativeArray.IsCreated;
			if (isCreated)
			{
				bool flag = nativeArray.Length == length;
				if (flag)
				{
					return nativeArray;
				}
				DownloadHandler.DisposeNativeArray(ref nativeArray);
			}
			DownloadHandler.CreateNativeArrayForNativeData(ref nativeArray, bytes, length);
			return nativeArray;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002DE8 File Offset: 0x00000FE8
		[VisibleToOtherModules(new string[] { "UnityEngine.UnityWebRequestAudioModule", "UnityEngine.UnityWebRequestTextureModule" })]
		internal static void DisposeNativeArray(ref NativeArray<byte> data)
		{
			bool flag = !data.IsCreated;
			if (!flag)
			{
				data = default(NativeArray<byte>);
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002E0C File Offset: 0x0000100C
		internal unsafe static void CreateNativeArrayForNativeData(ref NativeArray<byte> data, byte* bytes, int length)
		{
			data = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)bytes, length, Allocator.Persistent);
		}

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseFromScripting_Injected(IntPtr _unity_self);

		// Token: 0x06000033 RID: 51
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetErrorMsg_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000034 RID: 52
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetContentType_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern byte* InternalGetByteArray_Injected(IntPtr dh, out int length);

		// Token: 0x04000014 RID: 20
		[VisibleToOtherModules]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x02000008 RID: 8
		internal static class BindingsMarshaller
		{
			// Token: 0x06000036 RID: 54 RVA: 0x00002E1D File Offset: 0x0000101D
			public static IntPtr ConvertToNative(DownloadHandler handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}
