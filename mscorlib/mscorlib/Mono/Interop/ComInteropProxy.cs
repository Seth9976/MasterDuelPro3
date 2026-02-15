using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace Mono.Interop
{
	// Token: 0x0200004F RID: 79
	[StructLayout(LayoutKind.Sequential)]
	internal class ComInteropProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x060000C9 RID: 201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddProxy(IntPtr pItf, ref ComInteropProxy proxy);

		// Token: 0x060000CA RID: 202
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FindProxy(IntPtr pItf, ref ComInteropProxy proxy);

		// Token: 0x060000CB RID: 203 RVA: 0x000038F8 File Offset: 0x00001AF8
		private ComInteropProxy(Type t)
			: base(t)
		{
			this.com_object = __ComObject.CreateRCW(t);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003914 File Offset: 0x00001B14
		private void CacheProxy()
		{
			ComInteropProxy comInteropProxy = null;
			ComInteropProxy.FindProxy(this.com_object.IUnknown, ref comInteropProxy);
			if (comInteropProxy == null)
			{
				ComInteropProxy comInteropProxy2 = this;
				ComInteropProxy.AddProxy(this.com_object.IUnknown, ref comInteropProxy2);
				return;
			}
			Interlocked.Increment(ref this.ref_count);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003959 File Offset: 0x00001B59
		private ComInteropProxy(IntPtr pUnk)
			: this(pUnk, typeof(__ComObject))
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000396C File Offset: 0x00001B6C
		internal ComInteropProxy(IntPtr pUnk, Type t)
			: base(t)
		{
			this.com_object = new __ComObject(pUnk, this);
			this.CacheProxy();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003990 File Offset: 0x00001B90
		internal static ComInteropProxy GetProxy(IntPtr pItf, Type t)
		{
			Guid iid_IUnknown = __ComObject.IID_IUnknown;
			IntPtr intPtr;
			Marshal.ThrowExceptionForHR(Marshal.QueryInterface(pItf, ref iid_IUnknown, out intPtr));
			ComInteropProxy comInteropProxy = null;
			ComInteropProxy.FindProxy(intPtr, ref comInteropProxy);
			if (comInteropProxy == null)
			{
				Marshal.Release(intPtr);
				return new ComInteropProxy(intPtr);
			}
			Marshal.Release(intPtr);
			Interlocked.Increment(ref comInteropProxy.ref_count);
			return comInteropProxy;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000039E4 File Offset: 0x00001BE4
		internal static ComInteropProxy CreateProxy(Type t)
		{
			IntPtr intPtr = __ComObject.CreateIUnknown(t);
			ComInteropProxy comInteropProxy = null;
			ComInteropProxy.FindProxy(intPtr, ref comInteropProxy);
			ComInteropProxy comInteropProxy2;
			if (comInteropProxy != null)
			{
				Type type = comInteropProxy.com_object.GetType();
				if (type != t)
				{
					throw new InvalidCastException(string.Format("Unable to cast object of type '{0}' to type '{1}'.", type, t));
				}
				comInteropProxy2 = comInteropProxy;
				Marshal.Release(intPtr);
			}
			else
			{
				comInteropProxy2 = new ComInteropProxy(t);
				comInteropProxy2.com_object.Initialize(intPtr, comInteropProxy2);
			}
			return comInteropProxy2;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003A4C File Offset: 0x00001C4C
		public override IMessage Invoke(IMessage msg)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003A58 File Offset: 0x00001C58
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00003A60 File Offset: 0x00001C60
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003A6C File Offset: 0x00001C6C
		public bool CanCastTo(Type fromType, object o)
		{
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new NotSupportedException("Only RCWs are currently supported");
			}
			return (fromType.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic && !(_ComObject.GetInterface(fromType, false) == IntPtr.Zero);
		}

		// Token: 0x04000151 RID: 337
		private __ComObject com_object;

		// Token: 0x04000152 RID: 338
		private int ref_count = 1;

		// Token: 0x04000153 RID: 339
		private string type_name;
	}
}
