using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> interface to create a request message that acts as a method call on a remote object.</summary>
	// Token: 0x02000490 RID: 1168
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class MethodCall : IMethodCallMessage, IMethodMessage, IMessage, ISerializable, IInternalMessage
	{
		// Token: 0x06002571 RID: 9585 RVA: 0x000982B0 File Offset: 0x000964B0
		internal MethodCall(SerializationInfo info, StreamingContext context)
		{
			this.Init();
			foreach (SerializationEntry serializationEntry in info)
			{
				this.InitMethodProperty(serializationEntry.Name, serializationEntry.Value);
			}
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000982F8 File Offset: 0x000964F8
		internal MethodCall(CADMethodCallMessage msg)
		{
			this._uri = string.Copy(msg.Uri);
			ArrayList arguments = msg.GetArguments();
			this._args = msg.GetArgs(arguments);
			this._callContext = msg.GetLogicalCallContext(arguments);
			if (this._callContext == null)
			{
				this._callContext = new LogicalCallContext();
			}
			this._methodBase = msg.GetMethod();
			this.Init();
			if (msg.PropertiesCount > 0)
			{
				CADMessageBase.UnmarshalProperties(this.Properties, msg.PropertiesCount, arguments);
			}
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x00098380 File Offset: 0x00096580
		internal MethodCall(object handlerObject, BinaryMethodCallMessage smuggledMsg)
		{
			if (handlerObject != null)
			{
				this._uri = handlerObject as string;
				if (this._uri == null && handlerObject is MarshalByRefObject)
				{
					throw new NotImplementedException("MarshalByRefObject.GetIdentity");
				}
			}
			this._typeName = smuggledMsg.TypeName;
			this._methodName = smuggledMsg.MethodName;
			this._methodSignature = (Type[])smuggledMsg.MethodSignature;
			this._args = smuggledMsg.Args;
			this._genericArguments = smuggledMsg.InstantiationArgs;
			this._callContext = smuggledMsg.LogicalCallContext;
			this.ResolveMethod();
			if (smuggledMsg.HasProperties)
			{
				smuggledMsg.PopulateMessageProperties(this.Properties);
			}
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal MethodCall()
		{
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x00098424 File Offset: 0x00096624
		internal void CopyFrom(IMethodMessage call)
		{
			this._uri = call.Uri;
			this._typeName = call.TypeName;
			this._methodName = call.MethodName;
			this._args = call.Args;
			this._methodSignature = (Type[])call.MethodSignature;
			this._methodBase = call.MethodBase;
			this._callContext = call.LogicalCallContext;
			this.Init();
		}

		// Token: 0x06002576 RID: 9590 RVA: 0x00098490 File Offset: 0x00096690
		internal virtual void InitMethodProperty(string key, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1619225942U)
			{
				if (num != 990701179U)
				{
					if (num != 1201911322U)
					{
						if (num == 1619225942U)
						{
							if (key == "__Args")
							{
								this._args = (object[])value;
								return;
							}
						}
					}
					else if (key == "__CallContext")
					{
						this._callContext = (LogicalCallContext)value;
						return;
					}
				}
				else if (key == "__Uri")
				{
					this._uri = (string)value;
					return;
				}
			}
			else if (num <= 2850677384U)
			{
				if (num != 2010141056U)
				{
					if (num == 2850677384U)
					{
						if (key == "__GenericArguments")
						{
							this._genericArguments = (Type[])value;
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
			else if (num != 3166241401U)
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
			else if (key == "__MethodName")
			{
				this._methodName = (string)value;
				return;
			}
			this.Properties[key] = value;
		}

		/// <summary>The <see cref="M:System.Runtime.Remoting.Messaging.MethodCall.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method is not implemented. </summary>
		/// <param name="info">The data for serializing or deserializing the remote object.</param>
		/// <param name="context">The context of a certain serialized stream.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002577 RID: 9591 RVA: 0x000985E4 File Offset: 0x000967E4
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("__TypeName", this._typeName);
			info.AddValue("__MethodName", this._methodName);
			info.AddValue("__MethodSignature", this._methodSignature);
			info.AddValue("__Args", this._args);
			info.AddValue("__CallContext", this._callContext);
			info.AddValue("__Uri", this._uri);
			info.AddValue("__GenericArguments", this._genericArguments);
			if (this.InternalProperties != null)
			{
				foreach (object obj in this.InternalProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					info.AddValue((string)dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		/// <summary>Gets the number of arguments passed to a method. </summary>
		/// <returns>A <see cref="T:System.Int32" /> that represents the number of arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x000986D0 File Offset: 0x000968D0
		public int ArgCount
		{
			get
			{
				return this._args.Length;
			}
		}

		/// <summary>Gets an array of arguments passed to a method. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> that represents the arguments passed to a method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x000986DA File Offset: 0x000968DA
		public object[] Args
		{
			get
			{
				return this._args;
			}
		}

		/// <summary>Gets the <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call. </summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> for the current method call.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x000986E2 File Offset: 0x000968E2
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
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x000986FD File Offset: 0x000968FD
		public MethodBase MethodBase
		{
			get
			{
				if (this._methodBase == null)
				{
					this.ResolveMethod();
				}
				return this._methodBase;
			}
		}

		/// <summary>Gets the name of the invoked method. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the invoked method.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600257C RID: 9596 RVA: 0x00098719 File Offset: 0x00096919
		public string MethodName
		{
			get
			{
				if (this._methodName == null)
				{
					this._methodName = this._methodBase.Name;
				}
				return this._methodName;
			}
		}

		/// <summary>Gets an object that contains the method signature. </summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the method signature.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x0009873C File Offset: 0x0009693C
		public object MethodSignature
		{
			get
			{
				if (this._methodSignature == null && this._methodBase != null)
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

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600257E RID: 9598 RVA: 0x0009879D File Offset: 0x0009699D
		public virtual IDictionary Properties
		{
			get
			{
				if (this.ExternalProperties == null)
				{
					this.InitDictionary();
				}
				return this.ExternalProperties;
			}
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000987B4 File Offset: 0x000969B4
		internal virtual void InitDictionary()
		{
			MCMDictionary mcmdictionary = new MCMDictionary(this);
			this.ExternalProperties = mcmdictionary;
			this.InternalProperties = mcmdictionary.GetInternalProperties();
		}

		/// <summary>Gets the full type name of the remote object on which the method call is being made. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the full type name of the remote object on which the method call is being made.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x000987DB File Offset: 0x000969DB
		public string TypeName
		{
			get
			{
				if (this._typeName == null)
				{
					this._typeName = this._methodBase.DeclaringType.AssemblyQualifiedName;
				}
				return this._typeName;
			}
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of the remote object on which the method call is being made.</summary>
		/// <returns>The URI of a remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06002581 RID: 9601 RVA: 0x00098801 File Offset: 0x00096A01
		// (set) Token: 0x06002582 RID: 9602 RVA: 0x00098809 File Offset: 0x00096A09
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

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x00098812 File Offset: 0x00096A12
		// (set) Token: 0x06002584 RID: 9604 RVA: 0x0009881A File Offset: 0x00096A1A
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
		// Token: 0x06002585 RID: 9605 RVA: 0x00098823 File Offset: 0x00096A23
		public object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		/// <summary>Initializes a <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" />. </summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002586 RID: 9606 RVA: 0x00002C89 File Offset: 0x00000E89
		public virtual void Init()
		{
		}

		/// <summary>Sets method information from previously initialized remoting message properties. </summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002587 RID: 9607 RVA: 0x00098830 File Offset: 0x00096A30
		public void ResolveMethod()
		{
			if (this._uri != null)
			{
				Type serverTypeForUri = RemotingServices.GetServerTypeForUri(this._uri);
				if (serverTypeForUri == null)
				{
					string text = ((this._typeName != null) ? (" (" + this._typeName + ")") : "");
					throw new RemotingException("Requested service not found" + text + ". No receiver for uri " + this._uri);
				}
				Type type = this.CastTo(this._typeName, serverTypeForUri);
				if (type == null)
				{
					throw new RemotingException(string.Concat(new string[] { "Cannot cast from client type '", this._typeName, "' to server type '", serverTypeForUri.FullName, "'" }));
				}
				this._methodBase = RemotingServices.GetMethodBaseFromName(type, this._methodName, this._methodSignature);
				if (this._methodBase == null)
				{
					string text2 = "Method ";
					string methodName = this._methodName;
					string text3 = " not found in ";
					Type type2 = type;
					throw new RemotingException(text2 + methodName + text3 + ((type2 != null) ? type2.ToString() : null));
				}
				if (type != serverTypeForUri && type.IsInterface && !serverTypeForUri.IsInterface)
				{
					this._methodBase = RemotingServices.GetVirtualMethod(serverTypeForUri, this._methodBase);
					if (this._methodBase == null)
					{
						string text4 = "Method ";
						string methodName2 = this._methodName;
						string text5 = " not found in ";
						Type type3 = serverTypeForUri;
						throw new RemotingException(text4 + methodName2 + text5 + ((type3 != null) ? type3.ToString() : null));
					}
				}
			}
			else
			{
				this._methodBase = RemotingServices.GetMethodBaseFromMethodMessage(this);
				if (this._methodBase == null)
				{
					throw new RemotingException("Method " + this._methodName + " not found in " + this.TypeName);
				}
			}
			if (this._methodBase.IsGenericMethod && this._methodBase.ContainsGenericParameters)
			{
				if (this.GenericArguments == null)
				{
					throw new RemotingException("The remoting infrastructure does not support open generic methods.");
				}
				this._methodBase = ((MethodInfo)this._methodBase).MakeGenericMethod(this.GenericArguments);
			}
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x00098A2C File Offset: 0x00096C2C
		private Type CastTo(string clientType, Type serverType)
		{
			clientType = MethodCall.GetTypeNameFromAssemblyQualifiedName(clientType);
			if (clientType == serverType.FullName)
			{
				return serverType;
			}
			Type type = serverType.BaseType;
			while (type != null)
			{
				if (clientType == type.FullName)
				{
					return type;
				}
				type = type.BaseType;
			}
			foreach (Type type2 in serverType.GetInterfaces())
			{
				if (clientType == type2.FullName)
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x00098AA4 File Offset: 0x00096CA4
		private static string GetTypeNameFromAssemblyQualifiedName(string aqname)
		{
			int num = aqname.IndexOf("]]");
			int num2 = aqname.IndexOf(',', (num == -1) ? 0 : (num + 2));
			if (num2 != -1)
			{
				aqname = aqname.Substring(0, num2).Trim();
			}
			return aqname;
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x00098AE4 File Offset: 0x00096CE4
		// (set) Token: 0x0600258B RID: 9611 RVA: 0x00098AEC File Offset: 0x00096CEC
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

		// Token: 0x0600258C RID: 9612 RVA: 0x00098AF5 File Offset: 0x00096CF5
		bool IInternalMessage.HasProperties()
		{
			return this.ExternalProperties != null || this.InternalProperties != null;
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x00098B0C File Offset: 0x00096D0C
		private Type[] GenericArguments
		{
			get
			{
				if (this._genericArguments != null)
				{
					return this._genericArguments;
				}
				return this._genericArguments = this.MethodBase.GetGenericArguments();
			}
		}

		// Token: 0x040011F5 RID: 4597
		private string _uri;

		// Token: 0x040011F6 RID: 4598
		private string _typeName;

		// Token: 0x040011F7 RID: 4599
		private string _methodName;

		// Token: 0x040011F8 RID: 4600
		private object[] _args;

		// Token: 0x040011F9 RID: 4601
		private Type[] _methodSignature;

		// Token: 0x040011FA RID: 4602
		private MethodBase _methodBase;

		// Token: 0x040011FB RID: 4603
		private LogicalCallContext _callContext;

		// Token: 0x040011FC RID: 4604
		private Identity _targetIdentity;

		// Token: 0x040011FD RID: 4605
		private Type[] _genericArguments;

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		// Token: 0x040011FE RID: 4606
		protected IDictionary ExternalProperties;

		/// <summary>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		// Token: 0x040011FF RID: 4607
		protected IDictionary InternalProperties;
	}
}
