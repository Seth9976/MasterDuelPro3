using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using Mono;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x02000432 RID: 1074
	[StructLayout(LayoutKind.Sequential)]
	internal class TransparentProxy
	{
		// Token: 0x060023B9 RID: 9145 RVA: 0x00093388 File Offset: 0x00091588
		internal RuntimeType GetProxyType()
		{
			return (RuntimeType)Type.GetTypeFromHandle(this._class.ProxyClass.GetTypeHandle());
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x000933B2 File Offset: 0x000915B2
		private bool IsContextBoundObject
		{
			get
			{
				return this.GetProxyType().IsContextful;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x000933BF File Offset: 0x000915BF
		private Context TargetContext
		{
			get
			{
				return this._rp._targetContext;
			}
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000933CC File Offset: 0x000915CC
		private bool InCurrentContext()
		{
			return this.IsContextBoundObject && this.TargetContext == Thread.CurrentContext;
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000933E8 File Offset: 0x000915E8
		internal object LoadRemoteFieldNew(IntPtr classPtr, IntPtr fieldPtr)
		{
			RuntimeClassHandle runtimeClassHandle = new RuntimeClassHandle(classPtr);
			RuntimeFieldHandle runtimeFieldHandle = new RuntimeFieldHandle(fieldPtr);
			RuntimeTypeHandle typeHandle = runtimeClassHandle.GetTypeHandle();
			FieldInfo fieldFromHandle = FieldInfo.GetFieldFromHandle(runtimeFieldHandle);
			if (this.InCurrentContext())
			{
				object server = this._rp._server;
				return fieldFromHandle.GetValue(server);
			}
			string fullName = Type.GetTypeFromHandle(typeHandle).FullName;
			string name = fieldFromHandle.Name;
			object[] array = new object[] { fullName, name };
			object[] array2 = new object[1];
			MethodInfo method = typeof(object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new MissingMethodException("System.Object", "FieldGetter");
			}
			MonoMethodMessage monoMethodMessage = new MonoMethodMessage(method, array, array2);
			Exception ex;
			object[] array3;
			RealProxy.PrivateInvoke(this._rp, monoMethodMessage, out ex, out array3);
			if (ex != null)
			{
				throw ex;
			}
			return array3[0];
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000934B4 File Offset: 0x000916B4
		internal void StoreRemoteField(IntPtr classPtr, IntPtr fieldPtr, object arg)
		{
			RuntimeClassHandle runtimeClassHandle = new RuntimeClassHandle(classPtr);
			RuntimeFieldHandle runtimeFieldHandle = new RuntimeFieldHandle(fieldPtr);
			RuntimeTypeHandle typeHandle = runtimeClassHandle.GetTypeHandle();
			FieldInfo fieldFromHandle = FieldInfo.GetFieldFromHandle(runtimeFieldHandle);
			if (this.InCurrentContext())
			{
				object server = this._rp._server;
				fieldFromHandle.SetValue(server, arg);
				return;
			}
			string fullName = Type.GetTypeFromHandle(typeHandle).FullName;
			string name = fieldFromHandle.Name;
			object[] array = new object[] { fullName, name, arg };
			MethodInfo method = typeof(object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.NonPublic);
			if (method == null)
			{
				throw new MissingMethodException("System.Object", "FieldSetter");
			}
			MonoMethodMessage monoMethodMessage = new MonoMethodMessage(method, array, null);
			Exception ex;
			object[] array2;
			RealProxy.PrivateInvoke(this._rp, monoMethodMessage, out ex, out array2);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x04001141 RID: 4417
		public RealProxy _rp;

		// Token: 0x04001142 RID: 4418
		private RuntimeRemoteClassHandle _class;

		// Token: 0x04001143 RID: 4419
		private bool _custom_type_info;
	}
}
