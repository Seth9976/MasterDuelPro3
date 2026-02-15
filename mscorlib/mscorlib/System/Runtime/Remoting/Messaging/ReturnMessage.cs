using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Holds a message returned in response to a method call on a remote object.</summary>
	// Token: 0x0200049C RID: 1180
	[ComVisible(true)]
	public class ReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage, IInternalMessage
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class with all the information returning to the caller after the method call.</summary>
		/// <param name="ret">The object returned by the invoked method from which the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> instance originated. </param>
		/// <param name="outArgs">The objects returned from the invoked method as out parameters. </param>
		/// <param name="outArgsCount">The number of out parameters returned from the invoked method. </param>
		/// <param name="callCtx">The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the method call. </param>
		/// <param name="mcm">The original method call to the invoked method. </param>
		// Token: 0x060025F9 RID: 9721 RVA: 0x0009A12C File Offset: 0x0009832C
		public ReturnMessage(object ret, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IMethodCallMessage mcm)
		{
			this._returnValue = ret;
			this._args = outArgs;
			this._callCtx = callCtx;
			if (mcm != null)
			{
				this._uri = mcm.Uri;
				this._methodBase = mcm.MethodBase;
			}
			if (this._args == null)
			{
				this._args = new object[outArgsCount];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class.</summary>
		/// <param name="e">The exception that was thrown during execution of the remotely called method. </param>
		/// <param name="mcm">An <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> with which to create an instance of the <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> class. </param>
		// Token: 0x060025FA RID: 9722 RVA: 0x0009A187 File Offset: 0x00098387
		public ReturnMessage(Exception e, IMethodCallMessage mcm)
		{
			this._exception = e;
			if (mcm != null)
			{
				this._methodBase = mcm.MethodBase;
				this._callCtx = mcm.LogicalCallContext;
			}
			this._args = new object[0];
		}

		/// <summary>Gets the number of arguments of the called method.</summary>
		/// <returns>The number of arguments of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x0009A1BD File Offset: 0x000983BD
		public int ArgCount
		{
			get
			{
				return this._args.Length;
			}
		}

		/// <summary>Gets a specified argument passed to the method called on the remote object.</summary>
		/// <returns>An argument passed to the method called on the remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x0009A1C7 File Offset: 0x000983C7
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x0009A1CF File Offset: 0x000983CF
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				if (this._callCtx == null)
				{
					this._callCtx = new LogicalCallContext();
				}
				return this._callCtx;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x0009A1EA File Offset: 0x000983EA
		public MethodBase MethodBase
		{
			get
			{
				return this._methodBase;
			}
		}

		/// <summary>Gets the name of the called method.</summary>
		/// <returns>The name of the method that the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> originated from.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x0009A1F2 File Offset: 0x000983F2
		public string MethodName
		{
			get
			{
				if (this._methodBase != null && this._methodName == null)
				{
					this._methodName = this._methodBase.Name;
				}
				return this._methodName;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Type" /> objects containing the method signature.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects containing the method signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06002600 RID: 9728 RVA: 0x0009A224 File Offset: 0x00098424
		public object MethodSignature
		{
			get
			{
				if (this._methodBase != null && this._methodSignature == null)
				{
					ParameterInfo[] parameters = this._methodBase.GetParameters();
					this._methodSignature = new Type[parameters.Length];
					for (int i = 0; i < parameters.Length; i++)
					{
						this._methodSignature[i] = parameters[i].ParameterType;
					}
				}
				return this._methodSignature;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> of properties contained in the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> of properties contained in the current <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x0009A285 File Offset: 0x00098485
		public virtual IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodReturnDictionary(this);
				}
				return this._properties;
			}
		}

		/// <summary>Gets the name of the type on which the remote method was called.</summary>
		/// <returns>The type name of the remote object on which the remote method was called.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x0009A2A1 File Offset: 0x000984A1
		public string TypeName
		{
			get
			{
				if (this._methodBase != null && this._typeName == null)
				{
					this._typeName = this._methodBase.DeclaringType.AssemblyQualifiedName;
				}
				return this._typeName;
			}
		}

		/// <summary>Gets or sets the URI of the remote object on which the remote method was called.</summary>
		/// <returns>The URI of the remote object on which the remote method was called.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x0009A2D5 File Offset: 0x000984D5
		// (set) Token: 0x06002604 RID: 9732 RVA: 0x0009A2DD File Offset: 0x000984DD
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x0009A2E6 File Offset: 0x000984E6
		// (set) Token: 0x06002606 RID: 9734 RVA: 0x0009A2EE File Offset: 0x000984EE
		string IInternalMessage.Uri
		{
			get
			{
				return this.Uri;
			}
			set
			{
				this.Uri = value;
			}
		}

		/// <summary>Returns a specified argument passed to the remote method during the method call.</summary>
		/// <returns>An argument passed to the remote method during the method call.</returns>
		/// <param name="argNum">The zero-based index of the requested argument. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002607 RID: 9735 RVA: 0x0009A2F7 File Offset: 0x000984F7
		public object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		/// <summary>Gets the exception that was thrown during the remote method call.</summary>
		/// <returns>The exception thrown during the method call, or null if an exception did not occur during the call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x0009A301 File Offset: 0x00098501
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		/// <summary>Gets a specified object passed as an out or ref parameter to the called method.</summary>
		/// <returns>An object passed as an out or ref parameter to the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x0009A30C File Offset: 0x0009850C
		public object[] OutArgs
		{
			get
			{
				if (this._outArgs == null && this._args != null)
				{
					if (this._inArgInfo == null)
					{
						this._inArgInfo = new ArgInfo(this.MethodBase, ArgInfoType.Out);
					}
					this._outArgs = this._inArgInfo.GetInOutArgs(this._args);
				}
				return this._outArgs;
			}
		}

		/// <summary>Gets the object returned by the called method.</summary>
		/// <returns>The object returned by the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x0009A360 File Offset: 0x00098560
		public virtual object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x0009A368 File Offset: 0x00098568
		// (set) Token: 0x0600260C RID: 9740 RVA: 0x0009A370 File Offset: 0x00098570
		Identity IInternalMessage.TargetIdentity
		{
			get
			{
				return this._targetIdentity;
			}
			set
			{
				this._targetIdentity = value;
			}
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x0009A379 File Offset: 0x00098579
		bool IInternalMessage.HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x0009A379 File Offset: 0x00098579
		internal bool HasProperties()
		{
			return this._properties != null;
		}

		// Token: 0x0400122F RID: 4655
		private object[] _outArgs;

		// Token: 0x04001230 RID: 4656
		private object[] _args;

		// Token: 0x04001231 RID: 4657
		private LogicalCallContext _callCtx;

		// Token: 0x04001232 RID: 4658
		private object _returnValue;

		// Token: 0x04001233 RID: 4659
		private string _uri;

		// Token: 0x04001234 RID: 4660
		private Exception _exception;

		// Token: 0x04001235 RID: 4661
		private MethodBase _methodBase;

		// Token: 0x04001236 RID: 4662
		private string _methodName;

		// Token: 0x04001237 RID: 4663
		private Type[] _methodSignature;

		// Token: 0x04001238 RID: 4664
		private string _typeName;

		// Token: 0x04001239 RID: 4665
		private MethodReturnDictionary _properties;

		// Token: 0x0400123A RID: 4666
		private Identity _targetIdentity;

		// Token: 0x0400123B RID: 4667
		private ArgInfo _inArgInfo;
	}
}
