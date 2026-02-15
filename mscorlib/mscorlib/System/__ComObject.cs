using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Mono.Interop;

namespace System
{
	// Token: 0x02000204 RID: 516
	[StructLayout(LayoutKind.Sequential)]
	internal class __ComObject : MarshalByRefObject
	{
		// Token: 0x06001385 RID: 4997
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern __ComObject CreateRCW(Type t);

		// Token: 0x06001386 RID: 4998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReleaseInterfaces();

		// Token: 0x06001387 RID: 4999 RVA: 0x0004F9BC File Offset: 0x0004DBBC
		~__ComObject()
		{
			if (this.hash_table != IntPtr.Zero)
			{
				if (this.synchronization_context != null)
				{
					this.synchronization_context.Post(delegate(object state)
					{
						this.ReleaseInterfaces();
					}, this);
				}
				else
				{
					this.ReleaseInterfaces();
				}
			}
			this.proxy = null;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0004FA24 File Offset: 0x0004DC24
		public __ComObject()
		{
			this.Initialize(base.GetType());
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0004FA38 File Offset: 0x0004DC38
		internal __ComObject(Type t)
		{
			this.Initialize(t);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0004FA48 File Offset: 0x0004DC48
		internal __ComObject(IntPtr pItf, ComInteropProxy p)
		{
			this.proxy = p;
			this.InitializeApartmentDetails();
			Guid iid_IUnknown = __ComObject.IID_IUnknown;
			Marshal.ThrowExceptionForHR(Marshal.QueryInterface(pItf, ref iid_IUnknown, out this.iunknown));
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0004FA81 File Offset: 0x0004DC81
		internal void Initialize(IntPtr pUnk, ComInteropProxy p)
		{
			this.proxy = p;
			this.InitializeApartmentDetails();
			this.iunknown = pUnk;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0004FA97 File Offset: 0x0004DC97
		internal void Initialize(Type t)
		{
			this.InitializeApartmentDetails();
			if (this.iunknown != IntPtr.Zero)
			{
				return;
			}
			this.iunknown = __ComObject.CreateIUnknown(t);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0004FAC0 File Offset: 0x0004DCC0
		internal static IntPtr CreateIUnknown(Type t)
		{
			RuntimeHelpers.RunClassConstructor(t.TypeHandle);
			ObjectCreationDelegate objectCreationCallback = ExtensibleClassFactory.GetObjectCreationCallback(t);
			IntPtr intPtr;
			if (objectCreationCallback != null)
			{
				intPtr = objectCreationCallback(IntPtr.Zero);
				if (intPtr == IntPtr.Zero)
				{
					throw new COMException(string.Format("ObjectCreationDelegate for type {0} failed to return a valid COM object", t));
				}
			}
			else
			{
				Marshal.ThrowExceptionForHR(__ComObject.CoCreateInstance(__ComObject.GetCLSID(t), IntPtr.Zero, 21U, __ComObject.IID_IUnknown, out intPtr));
			}
			return intPtr;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0004FB2C File Offset: 0x0004DD2C
		private void InitializeApartmentDetails()
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				return;
			}
			this.synchronization_context = SynchronizationContext.Current;
			if (this.synchronization_context != null && this.synchronization_context.GetType() == typeof(SynchronizationContext))
			{
				this.synchronization_context = null;
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0004FB7C File Offset: 0x0004DD7C
		private static Guid GetCLSID(Type t)
		{
			if (t.IsImport)
			{
				return t.GUID;
			}
			Type type = t.BaseType;
			while (type != typeof(object))
			{
				if (type.IsImport)
				{
					return type.GUID;
				}
				type = type.BaseType;
			}
			throw new COMException("Could not find base COM type for type " + t.ToString());
		}

		// Token: 0x06001390 RID: 5008
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetInterfaceInternal(Type t, bool throwException);

		// Token: 0x06001391 RID: 5009 RVA: 0x0004FBDE File Offset: 0x0004DDDE
		internal IntPtr GetInterface(Type t, bool throwException)
		{
			this.CheckIUnknown();
			return this.GetInterfaceInternal(t, throwException);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0004FBEE File Offset: 0x0004DDEE
		internal IntPtr GetInterface(Type t)
		{
			return this.GetInterface(t, true);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0004FBF8 File Offset: 0x0004DDF8
		private void CheckIUnknown()
		{
			if (this.iunknown == IntPtr.Zero)
			{
				throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0004FC17 File Offset: 0x0004DE17
		internal IntPtr IUnknown
		{
			get
			{
				if (this.iunknown == IntPtr.Zero)
				{
					throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
				}
				return this.iunknown;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0004FC3C File Offset: 0x0004DE3C
		internal IntPtr IDispatch
		{
			get
			{
				IntPtr @interface = this.GetInterface(typeof(IDispatch));
				if (@interface == IntPtr.Zero)
				{
					throw new InvalidComObjectException("COM object that has been separated from its underlying RCW cannot be used.");
				}
				return @interface;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0004FC66 File Offset: 0x0004DE66
		internal static Guid IID_IUnknown
		{
			get
			{
				return new Guid("00000000-0000-0000-C000-000000000046");
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0004FC72 File Offset: 0x0004DE72
		internal static Guid IID_IDispatch
		{
			get
			{
				return new Guid("00020400-0000-0000-C000-000000000046");
			}
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0004FC80 File Offset: 0x0004DE80
		public override bool Equals(object obj)
		{
			this.CheckIUnknown();
			if (obj == null)
			{
				return false;
			}
			__ComObject _ComObject = obj as __ComObject;
			return _ComObject != null && this.iunknown == _ComObject.IUnknown;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004FCB5 File Offset: 0x0004DEB5
		public override int GetHashCode()
		{
			this.CheckIUnknown();
			return this.iunknown.ToInt32();
		}

		// Token: 0x0600139A RID: 5018
		[DllImport("ole32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true)]
		private static extern int CoCreateInstance([MarshalAs(UnmanagedType.LPStruct)] [In] Guid rclsid, IntPtr pUnkOuter, uint dwClsContext, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid, out IntPtr pUnk);

		// Token: 0x040009B5 RID: 2485
		private IntPtr iunknown;

		// Token: 0x040009B6 RID: 2486
		private IntPtr hash_table;

		// Token: 0x040009B7 RID: 2487
		private SynchronizationContext synchronization_context;

		// Token: 0x040009B8 RID: 2488
		private ComInteropProxy proxy;
	}
}
