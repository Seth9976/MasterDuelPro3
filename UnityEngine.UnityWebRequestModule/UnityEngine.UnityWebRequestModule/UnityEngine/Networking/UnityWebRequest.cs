using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine.Networking
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequest.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class UnityWebRequest : IDisposable
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002EA0 File Offset: 0x000010A0
		[NativeMethod(IsThreadSafe = true)]
		[NativeConditional("ENABLE_UNITYWEBREQUEST")]
		private static string GetWebErrorString(UnityWebRequest.UnityWebRequestError err)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				UnityWebRequest.GetWebErrorString_Injected(err, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002ED0 File Offset: 0x000010D0
		[VisibleToOtherModules]
		internal static string GetHTTPStatusString(long responseCode)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				UnityWebRequest.GetHTTPStatusString_Injected(responseCode, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002F00 File Offset: 0x00001100
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002F08 File Offset: 0x00001108
		public bool disposeCertificateHandlerOnDispose { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002F11 File Offset: 0x00001111
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002F19 File Offset: 0x00001119
		public bool disposeDownloadHandlerOnDispose { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002F22 File Offset: 0x00001122
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002F2A File Offset: 0x0000112A
		public bool disposeUploadHandlerOnDispose { get; set; }

		// Token: 0x06000048 RID: 72
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Create();

		// Token: 0x06000049 RID: 73 RVA: 0x00002F34 File Offset: 0x00001134
		[NativeMethod(IsThreadSafe = true)]
		private void Release()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			UnityWebRequest.Release_Injected(intPtr);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F58 File Offset: 0x00001158
		internal void InternalDestroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Abort();
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F95 File Offset: 0x00001195
		private void InternalSetDefaults()
		{
			this.disposeDownloadHandlerOnDispose = true;
			this.disposeUploadHandlerOnDispose = true;
			this.disposeCertificateHandlerOnDispose = true;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002FB0 File Offset: 0x000011B0
		public UnityWebRequest(string url, string method)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002FDC File Offset: 0x000011DC
		public UnityWebRequest(string url, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003019 File Offset: 0x00001219
		public UnityWebRequest(Uri uri, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003058 File Offset: 0x00001258
		~UnityWebRequest()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003090 File Offset: 0x00001290
		public void Dispose()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000030A8 File Offset: 0x000012A8
		private void DisposeHandlers()
		{
			bool disposeDownloadHandlerOnDispose = this.disposeDownloadHandlerOnDispose;
			if (disposeDownloadHandlerOnDispose)
			{
				DownloadHandler dh = this.downloadHandler;
				bool flag = dh != null;
				if (flag)
				{
					dh.Dispose();
				}
			}
			bool disposeUploadHandlerOnDispose = this.disposeUploadHandlerOnDispose;
			if (disposeUploadHandlerOnDispose)
			{
				UploadHandler uh = this.uploadHandler;
				bool flag2 = uh != null;
				if (flag2)
				{
					uh.Dispose();
				}
			}
			bool disposeCertificateHandlerOnDispose = this.disposeCertificateHandlerOnDispose;
			if (disposeCertificateHandlerOnDispose)
			{
				CertificateHandler ch = this.certificateHandler;
				bool flag3 = ch != null;
				if (flag3)
				{
					ch.Dispose();
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003130 File Offset: 0x00001330
		[NativeThrows]
		internal UnityWebRequestAsyncOperation BeginWebRequest()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = UnityWebRequest.BeginWebRequest_Injected(intPtr);
			return (intPtr2 == 0) ? null : UnityWebRequestAsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr2);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003160 File Offset: 0x00001360
		public UnityWebRequestAsyncOperation SendWebRequest()
		{
			UnityWebRequestAsyncOperation webOp = this.BeginWebRequest();
			bool flag = webOp != null;
			if (flag)
			{
				webOp.webRequest = this;
			}
			return webOp;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000318C File Offset: 0x0000138C
		[NativeMethod(IsThreadSafe = true)]
		public void Abort()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			UnityWebRequest.Abort_Injected(intPtr);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000031B0 File Offset: 0x000013B0
		private UnityWebRequest.UnityWebRequestError SetMethod(UnityWebRequest.UnityWebRequestMethod methodType)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetMethod_Injected(intPtr, methodType);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000031D4 File Offset: 0x000013D4
		internal void InternalSetMethod(UnityWebRequest.UnityWebRequestMethod methodType)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError ret = this.SetMethod(methodType);
			bool flag2 = ret > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(ret));
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003218 File Offset: 0x00001418
		private unsafe UnityWebRequest.UnityWebRequestError SetCustomMethod(string customMethodName)
		{
			UnityWebRequest.UnityWebRequestError unityWebRequestError;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(customMethodName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = customMethodName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				unityWebRequestError = UnityWebRequest.SetCustomMethod_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return unityWebRequestError;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003280 File Offset: 0x00001480
		internal void InternalSetCustomMethod(string customMethodName)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError ret = this.SetCustomMethod(customMethodName);
			bool flag2 = ret > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(ret));
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000032C4 File Offset: 0x000014C4
		internal UnityWebRequest.UnityWebRequestMethod GetMethod()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetMethod_Injected(intPtr);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000032E8 File Offset: 0x000014E8
		internal string GetCustomMethod()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				UnityWebRequest.GetCustomMethod_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003328 File Offset: 0x00001528
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003384 File Offset: 0x00001584
		public string method
		{
			get
			{
				string text;
				switch (this.GetMethod())
				{
				case UnityWebRequest.UnityWebRequestMethod.Get:
					text = "GET";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Post:
					text = "POST";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Put:
					text = "PUT";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Head:
					text = "HEAD";
					break;
				default:
					text = this.GetCustomMethod();
					break;
				}
				return text;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					throw new ArgumentException("Cannot set a UnityWebRequest's method to an empty or null string");
				}
				string text = value.ToUpper();
				string text2 = text;
				if (!(text2 == "GET"))
				{
					if (!(text2 == "POST"))
					{
						if (!(text2 == "PUT"))
						{
							if (!(text2 == "HEAD"))
							{
								this.InternalSetCustomMethod(value.ToUpper());
							}
							else
							{
								this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Head);
							}
						}
						else
						{
							this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Put);
						}
					}
					else
					{
						this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Post);
					}
				}
				else
				{
					this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Get);
				}
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003420 File Offset: 0x00001620
		private UnityWebRequest.UnityWebRequestError GetError()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetError_Injected(intPtr);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003444 File Offset: 0x00001644
		public string error
		{
			get
			{
				UnityWebRequest.Result result = this.result;
				UnityWebRequest.Result result2 = result;
				string text;
				if (result2 > UnityWebRequest.Result.Success)
				{
					if (result2 != UnityWebRequest.Result.ProtocolError)
					{
						text = UnityWebRequest.GetWebErrorString(this.GetError());
					}
					else
					{
						text = string.Format("HTTP/1.1 {0} {1}", this.responseCode, UnityWebRequest.GetHTTPStatusString(this.responseCode));
					}
				}
				else
				{
					text = null;
				}
				return text;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000034A0 File Offset: 0x000016A0
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000034B8 File Offset: 0x000016B8
		public string url
		{
			get
			{
				return this.GetUrl();
			}
			set
			{
				string localUrl = "https://localhost/";
				this.InternalSetUrl(WebRequestUtils.MakeInitialUrl(value, localUrl));
			}
		}

		// Token: 0x1700000C RID: 12
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000034DC File Offset: 0x000016DC
		public Uri uri
		{
			set
			{
				bool flag = !value.IsAbsoluteUri;
				if (flag)
				{
					throw new ArgumentException("URI must be absolute");
				}
				this.InternalSetUrl(WebRequestUtils.MakeUriString(value, value.OriginalString, false));
				this.m_Uri = value;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003520 File Offset: 0x00001720
		private string GetUrl()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				UnityWebRequest.GetUrl_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003560 File Offset: 0x00001760
		private unsafe UnityWebRequest.UnityWebRequestError SetUrl(string url)
		{
			UnityWebRequest.UnityWebRequestError unityWebRequestError;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(url, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = url.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				unityWebRequestError = UnityWebRequest.SetUrl_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return unityWebRequestError;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000035C8 File Offset: 0x000017C8
		private void InternalSetUrl(string url)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its URL cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError ret = this.SetUrl(url);
			bool flag2 = ret > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(ret));
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000360C File Offset: 0x0000180C
		public long responseCode
		{
			get
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return UnityWebRequest.get_responseCode_Injected(intPtr);
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003630 File Offset: 0x00001830
		private bool IsExecuting()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.IsExecuting_Injected(intPtr);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003654 File Offset: 0x00001854
		public bool isModifiable
		{
			[NativeMethod("IsModifiable")]
			get
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return UnityWebRequest.get_isModifiable_Injected(intPtr);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003678 File Offset: 0x00001878
		public bool isDone
		{
			get
			{
				return this.result > UnityWebRequest.Result.InProgress;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003694 File Offset: 0x00001894
		public UnityWebRequest.Result result
		{
			[NativeMethod("GetResult")]
			get
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return UnityWebRequest.get_result_Injected(intPtr);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000036B8 File Offset: 0x000018B8
		private float GetDownloadProgress()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetDownloadProgress_Injected(intPtr);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000036DC File Offset: 0x000018DC
		public float downloadProgress
		{
			get
			{
				bool flag = !this.IsExecuting() && !this.isDone;
				float num;
				if (flag)
				{
					num = -1f;
				}
				else
				{
					num = this.GetDownloadProgress();
				}
				return num;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003714 File Offset: 0x00001914
		public ulong downloadedBytes
		{
			get
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return UnityWebRequest.get_downloadedBytes_Injected(intPtr);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003738 File Offset: 0x00001938
		[NativeThrows]
		private void SetRedirectLimitFromScripting(int limit)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			UnityWebRequest.SetRedirectLimitFromScripting_Injected(intPtr, limit);
		}

		// Token: 0x17000013 RID: 19
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000375B File Offset: 0x0000195B
		public int redirectLimit
		{
			set
			{
				this.SetRedirectLimitFromScripting(value);
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003768 File Offset: 0x00001968
		[NativeMethod("SetRequestHeader")]
		internal unsafe UnityWebRequest.UnityWebRequestError InternalSetRequestHeader(string name, string value)
		{
			UnityWebRequest.UnityWebRequestError unityWebRequestError;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = value.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				unityWebRequestError = UnityWebRequest.InternalSetRequestHeader_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
			return unityWebRequestError;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003804 File Offset: 0x00001A04
		public void SetRequestHeader(string name, string value)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				throw new ArgumentException("Cannot set a Request Header with a null or empty name");
			}
			bool flag2 = value == null;
			if (flag2)
			{
				throw new ArgumentException("Cannot set a Request header with a null");
			}
			bool flag3 = !this.isModifiable;
			if (flag3)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request headers cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError ret = this.InternalSetRequestHeader(name, value);
			bool flag4 = ret > UnityWebRequest.UnityWebRequestError.OK;
			if (flag4)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(ret));
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003874 File Offset: 0x00001A74
		public unsafe string GetResponseHeader(string name)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				UnityWebRequest.GetResponseHeader_Injected(intPtr, ref managedSpanWrapper, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000038E4 File Offset: 0x00001AE4
		internal string[] GetResponseHeaderKeys()
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.GetResponseHeaderKeys_Injected(intPtr);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003908 File Offset: 0x00001B08
		public Dictionary<string, string> GetResponseHeaders()
		{
			string[] headerKeys = this.GetResponseHeaderKeys();
			bool flag = headerKeys == null || headerKeys.Length == 0;
			Dictionary<string, string> dictionary;
			if (flag)
			{
				dictionary = null;
			}
			else
			{
				Dictionary<string, string> headers = new Dictionary<string, string>(headerKeys.Length, StringComparer.OrdinalIgnoreCase);
				for (int i = 0; i < headerKeys.Length; i++)
				{
					string val = this.GetResponseHeader(headerKeys[i]);
					headers.Add(headerKeys[i], val);
				}
				dictionary = headers;
			}
			return dictionary;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003978 File Offset: 0x00001B78
		private UnityWebRequest.UnityWebRequestError SetUploadHandler(UploadHandler uh)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetUploadHandler_Injected(intPtr, (uh == null) ? ((IntPtr)0) : UploadHandler.BindingsMarshaller.ConvertToNative(uh));
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000039AC File Offset: 0x00001BAC
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000039C4 File Offset: 0x00001BC4
		public UploadHandler uploadHandler
		{
			get
			{
				return this.m_UploadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the upload handler");
				}
				UnityWebRequest.UnityWebRequestError ret = this.SetUploadHandler(value);
				bool flag2 = ret > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(ret));
				}
				this.m_UploadHandler = value;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003A10 File Offset: 0x00001C10
		private UnityWebRequest.UnityWebRequestError SetDownloadHandler(DownloadHandler dh)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetDownloadHandler_Injected(intPtr, (dh == null) ? ((IntPtr)0) : DownloadHandler.BindingsMarshaller.ConvertToNative(dh));
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003A44 File Offset: 0x00001C44
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003A5C File Offset: 0x00001C5C
		public DownloadHandler downloadHandler
		{
			get
			{
				return this.m_DownloadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the download handler");
				}
				UnityWebRequest.UnityWebRequestError ret = this.SetDownloadHandler(value);
				bool flag2 = ret > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(ret));
				}
				this.m_DownloadHandler = value;
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003AA8 File Offset: 0x00001CA8
		private UnityWebRequest.UnityWebRequestError SetCertificateHandler(CertificateHandler ch)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetCertificateHandler_Injected(intPtr, (ch == null) ? ((IntPtr)0) : CertificateHandler.BindingsMarshaller.ConvertToNative(ch));
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003ADC File Offset: 0x00001CDC
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00003AF4 File Offset: 0x00001CF4
		public CertificateHandler certificateHandler
		{
			get
			{
				return this.m_CertificateHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the certificate handler");
				}
				UnityWebRequest.UnityWebRequestError ret = this.SetCertificateHandler(value);
				bool flag2 = ret > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(ret));
				}
				this.m_CertificateHandler = value;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003B40 File Offset: 0x00001D40
		private UnityWebRequest.UnityWebRequestError SetTimeoutMsec(int timeout)
		{
			IntPtr intPtr = UnityWebRequest.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return UnityWebRequest.SetTimeoutMsec_Injected(intPtr, timeout);
		}

		// Token: 0x17000017 RID: 23
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003B64 File Offset: 0x00001D64
		public int timeout
		{
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				value = Math.Max(value, 0);
				UnityWebRequest.UnityWebRequestError ret = this.SetTimeoutMsec(value * 1000);
				bool flag2 = ret > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(ret));
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003BB8 File Offset: 0x00001DB8
		public static UnityWebRequest Get(string uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public static UnityWebRequest Head(string uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003C00 File Offset: 0x00001E00
		public static UnityWebRequest PostWwwForm(string uri, string form)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPostWwwForm(request, form);
			return request;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003C28 File Offset: 0x00001E28
		private static void SetupPostWwwForm(UnityWebRequest request, string postData)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			bool flag = string.IsNullOrEmpty(postData);
			if (!flag)
			{
				string urlencoded = WWWTranscoder.DataEncode(postData, Encoding.UTF8);
				byte[] payload = Encoding.UTF8.GetBytes(urlencoded);
				request.uploadHandler = new UploadHandlerRaw(payload);
				request.uploadHandler.contentType = "application/x-www-form-urlencoded";
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003C88 File Offset: 0x00001E88
		public static UnityWebRequest Post(string uri, string postData, string contentType)
		{
			UnityWebRequest request = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(request, postData, contentType);
			return request;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003CB0 File Offset: 0x00001EB0
		private static void SetupPost(UnityWebRequest request, string postData, string contentType)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			bool flag = string.IsNullOrEmpty(postData);
			if (flag)
			{
				request.SetRequestHeader("Content-Type", contentType);
			}
			else
			{
				byte[] payload = Encoding.UTF8.GetBytes(postData);
				request.uploadHandler = new UploadHandlerRaw(payload);
				request.uploadHandler.contentType = contentType;
			}
		}

		// Token: 0x06000085 RID: 133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetWebErrorString_Injected(UnityWebRequest.UnityWebRequestError err, out ManagedSpanWrapper ret);

		// Token: 0x06000086 RID: 134
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetHTTPStatusString_Injected(long responseCode, out ManagedSpanWrapper ret);

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Release_Injected(IntPtr _unity_self);

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr BeginWebRequest_Injected(IntPtr _unity_self);

		// Token: 0x06000089 RID: 137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Abort_Injected(IntPtr _unity_self);

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetMethod_Injected(IntPtr _unity_self, UnityWebRequest.UnityWebRequestMethod methodType);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetCustomMethod_Injected(IntPtr _unity_self, ref ManagedSpanWrapper customMethodName);

		// Token: 0x0600008C RID: 140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestMethod GetMethod_Injected(IntPtr _unity_self);

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCustomMethod_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x0600008E RID: 142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError GetError_Injected(IntPtr _unity_self);

		// Token: 0x0600008F RID: 143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetUrl_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000090 RID: 144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetUrl_Injected(IntPtr _unity_self, ref ManagedSpanWrapper url);

		// Token: 0x06000091 RID: 145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long get_responseCode_Injected(IntPtr _unity_self);

		// Token: 0x06000092 RID: 146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsExecuting_Injected(IntPtr _unity_self);

		// Token: 0x06000093 RID: 147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isModifiable_Injected(IntPtr _unity_self);

		// Token: 0x06000094 RID: 148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.Result get_result_Injected(IntPtr _unity_self);

		// Token: 0x06000095 RID: 149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetDownloadProgress_Injected(IntPtr _unity_self);

		// Token: 0x06000096 RID: 150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong get_downloadedBytes_Injected(IntPtr _unity_self);

		// Token: 0x06000097 RID: 151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRedirectLimitFromScripting_Injected(IntPtr _unity_self, int limit);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError InternalSetRequestHeader_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, ref ManagedSpanWrapper value);

		// Token: 0x06000099 RID: 153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetResponseHeader_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, out ManagedSpanWrapper ret);

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetResponseHeaderKeys_Injected(IntPtr _unity_self);

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetUploadHandler_Injected(IntPtr _unity_self, IntPtr uh);

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetDownloadHandler_Injected(IntPtr _unity_self, IntPtr dh);

		// Token: 0x0600009D RID: 157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetCertificateHandler_Injected(IntPtr _unity_self, IntPtr ch);

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern UnityWebRequest.UnityWebRequestError SetTimeoutMsec_Injected(IntPtr _unity_self, int timeout);

		// Token: 0x04000017 RID: 23
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000018 RID: 24
		[NonSerialized]
		internal DownloadHandler m_DownloadHandler;

		// Token: 0x04000019 RID: 25
		[NonSerialized]
		internal UploadHandler m_UploadHandler;

		// Token: 0x0400001A RID: 26
		[NonSerialized]
		internal CertificateHandler m_CertificateHandler;

		// Token: 0x0400001B RID: 27
		[NonSerialized]
		internal Uri m_Uri;

		// Token: 0x0200000D RID: 13
		internal enum UnityWebRequestMethod
		{
			// Token: 0x04000020 RID: 32
			Get,
			// Token: 0x04000021 RID: 33
			Post,
			// Token: 0x04000022 RID: 34
			Put,
			// Token: 0x04000023 RID: 35
			Head,
			// Token: 0x04000024 RID: 36
			Custom
		}

		// Token: 0x0200000E RID: 14
		internal enum UnityWebRequestError
		{
			// Token: 0x04000026 RID: 38
			OK,
			// Token: 0x04000027 RID: 39
			OKCached,
			// Token: 0x04000028 RID: 40
			Unknown,
			// Token: 0x04000029 RID: 41
			SDKError,
			// Token: 0x0400002A RID: 42
			UnsupportedProtocol,
			// Token: 0x0400002B RID: 43
			MalformattedUrl,
			// Token: 0x0400002C RID: 44
			CannotResolveProxy,
			// Token: 0x0400002D RID: 45
			CannotResolveHost,
			// Token: 0x0400002E RID: 46
			CannotConnectToHost,
			// Token: 0x0400002F RID: 47
			AccessDenied,
			// Token: 0x04000030 RID: 48
			GenericHttpError,
			// Token: 0x04000031 RID: 49
			WriteError,
			// Token: 0x04000032 RID: 50
			ReadError,
			// Token: 0x04000033 RID: 51
			OutOfMemory,
			// Token: 0x04000034 RID: 52
			Timeout,
			// Token: 0x04000035 RID: 53
			HTTPPostError,
			// Token: 0x04000036 RID: 54
			SSLCannotConnect,
			// Token: 0x04000037 RID: 55
			Aborted,
			// Token: 0x04000038 RID: 56
			TooManyRedirects,
			// Token: 0x04000039 RID: 57
			ReceivedNoData,
			// Token: 0x0400003A RID: 58
			SSLNotSupported,
			// Token: 0x0400003B RID: 59
			FailedToSendData,
			// Token: 0x0400003C RID: 60
			FailedToReceiveData,
			// Token: 0x0400003D RID: 61
			SSLCertificateError,
			// Token: 0x0400003E RID: 62
			SSLCipherNotAvailable,
			// Token: 0x0400003F RID: 63
			SSLCACertError,
			// Token: 0x04000040 RID: 64
			UnrecognizedContentEncoding,
			// Token: 0x04000041 RID: 65
			LoginFailed,
			// Token: 0x04000042 RID: 66
			SSLShutdownFailed,
			// Token: 0x04000043 RID: 67
			RedirectLimitInvalid,
			// Token: 0x04000044 RID: 68
			InvalidRedirect,
			// Token: 0x04000045 RID: 69
			CannotModifyRequest,
			// Token: 0x04000046 RID: 70
			HeaderNameContainsInvalidCharacters,
			// Token: 0x04000047 RID: 71
			HeaderValueContainsInvalidCharacters,
			// Token: 0x04000048 RID: 72
			CannotOverrideSystemHeaders,
			// Token: 0x04000049 RID: 73
			AlreadySent,
			// Token: 0x0400004A RID: 74
			InvalidMethod,
			// Token: 0x0400004B RID: 75
			NotImplemented,
			// Token: 0x0400004C RID: 76
			NoInternetConnection,
			// Token: 0x0400004D RID: 77
			DataProcessingError,
			// Token: 0x0400004E RID: 78
			InsecureConnectionNotAllowed
		}

		// Token: 0x0200000F RID: 15
		public enum Result
		{
			// Token: 0x04000050 RID: 80
			InProgress,
			// Token: 0x04000051 RID: 81
			Success,
			// Token: 0x04000052 RID: 82
			ConnectionError,
			// Token: 0x04000053 RID: 83
			ProtocolError,
			// Token: 0x04000054 RID: 84
			DataProcessingError
		}

		// Token: 0x02000010 RID: 16
		internal static class BindingsMarshaller
		{
			// Token: 0x0600009F RID: 159 RVA: 0x00003D0A File Offset: 0x00001F0A
			public static IntPtr ConvertToNative(UnityWebRequest unityWebRequest)
			{
				return unityWebRequest.m_Ptr;
			}
		}
	}
}
