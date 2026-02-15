using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> interface to create a message that acts as a method response on a remote object.</summary>
	// Token: 0x02000494 RID: 1172
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class MethodResponse : IMethodReturnMessage, IMethodMessage, IMessage, ISerializable, IInternalMessage
	{
		// Token: 0x060025B0 RID: 9648 RVA: 0x00099380 File Offset: 0x00097580
		internal MethodResponse(Exception e, IMethodCallMessage msg)
		{
			this._callMsg = msg;
			if (msg != null)
			{
				this._uri = msg.Uri;
			}
			else
			{
				this._uri = string.Empty;
			}
			this._exception = e;
			this._returnValue = null;
			this._outArgs = new object[0];
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x000993D0 File Offset: 0x000975D0
		internal MethodResponse(object returnValue, object[] outArgs, LogicalCallContext callCtx, IMethodCallMessage msg)
		{
			this._callMsg = msg;
			this._uri = msg.Uri;
			this._exception = null;
			this._returnValue = returnValue;
			this._args = outArgs;
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x00099404 File Offset: 0x00097604
		internal MethodResponse(IMethodCallMessage msg, CADMethodReturnMessage retmsg)
		{
			this._callMsg = msg;
			this._methodBase = msg.MethodBase;
			this._uri = msg.Uri;
			this._methodName = msg.MethodName;
			ArrayList arguments = retmsg.GetArguments();
			this._exception = retmsg.GetException(arguments);
			this._returnValue = retmsg.GetReturnValue(arguments);
			this._args = retmsg.GetArgs(arguments);
			this._callContext = retmsg.GetLogicalCallContext(arguments);
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			if (retmsg.PropertiesCount > 0)
			{
				CADMessageBase.UnmarshalProperties(this.Properties, retmsg.PropertiesCount, arguments);
			}
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x000994AC File Offset: 0x000976AC
		internal MethodResponse(IMethodCallMessage msg, object handlerObject, BinaryMethodReturnMessage smuggledMrm)
		{
			if (msg != null)
			{
				this._methodBase = msg.MethodBase;
				this._methodName = msg.MethodName;
				this._uri = msg.Uri;
			}
			this._returnValue = smuggledMrm.ReturnValue;
			this._args = smuggledMrm.Args;
			this._exception = smuggledMrm.Exception;
			this._callContext = smuggledMrm.LogicalCallContext;
			if (smuggledMrm.HasProperties)
			{
				smuggledMrm.PopulateMessageProperties(this.Properties);
			}
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x0009952C File Offset: 0x0009772C
		internal MethodResponse(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				this.InitMethodProperty(serializationEntry.Name, serializationEntry.Value);
			}
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x0009956C File Offset: 0x0009776C
		internal void InitMethodProperty(string key, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1960967436U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num == 1201911322U)
						{
							if (key == "__CallContext")
							{
								this._callContext = (LogicalCallContext)value;
								return;
							}
						}
					}
					else if (key == "__Uri")
					{
						this._uri = (string)value;
						return;
					}
				}
				else if (num != 1637783905U)
				{
					if (num == 1960967436U)
					{
						if (key == "__OutArgs")
						{
							this._args = (object[])value;
							return;
						}
					}
				}
				else if (key == "__Return")
				{
					this._returnValue = value;
					return;
				}
			}
			else if (num <= 3166241401U)
			{
				if (num != 2010141056U)
				{
					if (num == 3166241401U)
					{
						if (key == "__MethodName")
						{
							this._methodName = (string)value;
							return;
						}
					}
				}
				else if (key == "__TypeName")
				{
					this._typeName = (string)value;
					return;
				}
			}
			else if (num != 3626951189U)
			{
				if (num == 3679129400U)
				{
					if (key == "__MethodSignature")
					{
						this._methodSignature = (Type[])value;
						return;
					}
				}
			}
			else if (key == "__fault")
			{
				this._exception = (Exception)value;
				return;
			}
			this.Properties[key] = value;
		}

		/// <summary>Gets the number of arguments passed to the method. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060025B6 RID: 9654 RVA: 0x000996F2 File Offset: 0x000978F2
		public int ArgCount
		{
			get
			{
				if (this._args == null)
				{
					return 0;
				}
				return this._args.Length;
			}
		}

		/// <summary>Gets an array of arguments passed to the method. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x00099706 File Offset: 0x00097906
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		/// <summary>Gets the exception thrown during the method call, or null if the method did not throw an exception. </summary>
		/// <returns>The <see cref="T:System.Exception" /> thrown during the method call, or null if the method did not throw an exception.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060025B8 RID: 9656 RVA: 0x0009970E File Offset: 0x0009790E
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call. </summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x00099716 File Offset: 0x00097916
		public LogicalCallContext LogicalCallContext
		{
			get
			{
				if (this._callContext == null)
				{
					this._callContext = new LogicalCallContext();
				}
				return this._callContext;
			}
		}

		/// <summary>Gets the <see cref="T:System.Reflection.MethodBase" /> of the called method. </summary>
		/// <returns>The <see cref="T:System.Reflection.MethodBase" /> of the called method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x00099734 File Offset: 0x00097934
		public MethodBase MethodBase
		{
			get
			{
				if (null == this._methodBase)
				{
					if (this._callMsg != null)
					{
						this._methodBase = this._callMsg.MethodBase;
					}
					else if (this.MethodName != null && this.TypeName != null)
					{
						this._methodBase = RemotingServices.GetMethodBaseFromMethodMessage(this);
					}
				}
				return this._methodBase;
			}
		}

		/// <summary>Gets the name of the invoked method. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x0009978C File Offset: 0x0009798C
		public string MethodName
		{
			get
			{
				if (this._methodName == null && this._callMsg != null)
				{
					this._methodName = this._callMsg.MethodName;
				}
				return this._methodName;
			}
		}

		/// <summary>Gets an object that contains the method signature. </summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x000997B5 File Offset: 0x000979B5
		public object MethodSignature
		{
			get
			{
				if (this._methodSignature == null && this._callMsg != null)
				{
					this._methodSignature = (Type[])this._callMsg.MethodSignature;
				}
				return this._methodSignature;
			}
		}

		/// <summary>Gets an array of arguments in the method call that are marked as ref or out parameters.</summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments in the method call that are marked as ref or out parameters.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x000997E4 File Offset: 0x000979E4
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

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x00099838 File Offset: 0x00097A38
		public virtual IDictionary Properties
		{
			get
			{
				if (this.ExternalProperties == null)
				{
					MethodReturnDictionary methodReturnDictionary = new MethodReturnDictionary(this);
					this.ExternalProperties = methodReturnDictionary;
					this.InternalProperties = methodReturnDictionary.GetInternalProperties();
				}
				return this.ExternalProperties;
			}
		}

		/// <summary>Gets the return value of the method call. </summary>
		/// <returns>A <see cref="T:System.Object" /> that represents the return value of the method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x0009986D File Offset: 0x00097A6D
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x00099875 File Offset: 0x00097A75
		public string TypeName
		{
			get
			{
				if (this._typeName == null && this._callMsg != null)
				{
					this._typeName = this._callMsg.TypeName;
				}
				return this._typeName;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made. </summary>
		/// <returns>The URI of a remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060025C1 RID: 9665 RVA: 0x0009989E File Offset: 0x00097A9E
		// (set) Token: 0x060025C2 RID: 9666 RVA: 0x000998C7 File Offset: 0x00097AC7
		public string Uri
		{
			get
			{
				if (this._uri == null && this._callMsg != null)
				{
					this._uri = this._callMsg.Uri;
				}
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x000998D0 File Offset: 0x00097AD0
		// (set) Token: 0x060025C4 RID: 9668 RVA: 0x000998D8 File Offset: 0x00097AD8
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

		/// <summary>Gets a method argument, as an object, at a specified index. </summary>
		/// <returns>The method argument as an object.</returns>
		/// <param name="argNum">The index of the requested argument.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060025C5 RID: 9669 RVA: 0x000998E1 File Offset: 0x00097AE1
		public object GetArg(int argNum)
		{
			if (this._args == null)
			{
				return null;
			}
			return this._args[argNum];
		}

		/// <summary>The <see cref="M:System.Runtime.Remoting.Messaging.MethodResponse.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method is not implemented. </summary>
		/// <param name="info">Data for serializing or deserializing the remote object.</param>
		/// <param name="context">Context of a certain serialized stream.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060025C6 RID: 9670 RVA: 0x000998F8 File Offset: 0x00097AF8
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (this._exception == null)
			{
				info.AddValue("__TypeName", this._typeName);
				info.AddValue("__MethodName", this._methodName);
				info.AddValue("__MethodSignature", this._methodSignature);
				info.AddValue("__Uri", this._uri);
				info.AddValue("__Return", this._returnValue);
				info.AddValue("__OutArgs", this._args);
			}
			else
			{
				info.AddValue("__fault", this._exception);
			}
			info.AddValue("__CallContext", this._callContext);
			if (this.InternalProperties != null)
			{
				foreach (object obj in this.InternalProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					info.AddValue((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x00099A00 File Offset: 0x00097C00
		// (set) Token: 0x060025C8 RID: 9672 RVA: 0x00099A08 File Offset: 0x00097C08
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

		// Token: 0x060025C9 RID: 9673 RVA: 0x00099A11 File Offset: 0x00097C11
		bool IInternalMessage.HasProperties()
		{
			return this.ExternalProperties != null || this.InternalProperties != null;
		}

		// Token: 0x04001208 RID: 4616
		private string _methodName;

		// Token: 0x04001209 RID: 4617
		private string _uri;

		// Token: 0x0400120A RID: 4618
		private string _typeName;

		// Token: 0x0400120B RID: 4619
		private MethodBase _methodBase;

		// Token: 0x0400120C RID: 4620
		private object _returnValue;

		// Token: 0x0400120D RID: 4621
		private Exception _exception;

		// Token: 0x0400120E RID: 4622
		private Type[] _methodSignature;

		// Token: 0x0400120F RID: 4623
		private ArgInfo _inArgInfo;

		// Token: 0x04001210 RID: 4624
		private object[] _args;

		// Token: 0x04001211 RID: 4625
		private object[] _outArgs;

		// Token: 0x04001212 RID: 4626
		private IMethodCallMessage _callMsg;

		// Token: 0x04001213 RID: 4627
		private LogicalCallContext _callContext;

		// Token: 0x04001214 RID: 4628
		private Identity _targetIdentity;

		/// <summary>Specifies an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		// Token: 0x04001215 RID: 4629
		protected IDictionary ExternalProperties;

		/// <summary>Specifies an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		// Token: 0x04001216 RID: 4630
		protected IDictionary InternalProperties;
	}
}
