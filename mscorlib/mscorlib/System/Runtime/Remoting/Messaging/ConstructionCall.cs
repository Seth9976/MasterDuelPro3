using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Implements the <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> interface to create a request message that constitutes a constructor call on a remote object.</summary>
	// Token: 0x02000482 RID: 1154
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Serializable]
	public class ConstructionCall : MethodCall, IConstructionCallMessage, IMessage, IMethodCallMessage, IMethodMessage
	{
		// Token: 0x06002533 RID: 9523 RVA: 0x00097E87 File Offset: 0x00096087
		internal ConstructionCall(Type type)
		{
			this._activationType = type;
			this._activationTypeName = type.AssemblyQualifiedName;
			this._isContextOk = true;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x00097EA9 File Offset: 0x000960A9
		internal ConstructionCall(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x00097EB4 File Offset: 0x000960B4
		internal override void InitDictionary()
		{
			ConstructionCallDictionary constructionCallDictionary = new ConstructionCallDictionary(this);
			this.ExternalProperties = constructionCallDictionary;
			this.InternalProperties = constructionCallDictionary.GetInternalProperties();
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06002536 RID: 9526 RVA: 0x00097EDB File Offset: 0x000960DB
		// (set) Token: 0x06002537 RID: 9527 RVA: 0x00097EE3 File Offset: 0x000960E3
		internal bool IsContextOk
		{
			get
			{
				return this._isContextOk;
			}
			set
			{
				this._isContextOk = value;
			}
		}

		/// <summary>Gets the type of the remote object to activate. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the remote object to activate.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x00097EEC File Offset: 0x000960EC
		public Type ActivationType
		{
			get
			{
				if (this._activationType == null)
				{
					this._activationType = Type.GetType(this._activationTypeName);
				}
				return this._activationType;
			}
		}

		/// <summary>Gets the full type name of the remote object to activate. </summary>
		/// <returns>A <see cref="T:System.String" /> containing the full type name of the remote object to activate.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x00097F13 File Offset: 0x00096113
		public string ActivationTypeName
		{
			get
			{
				return this._activationTypeName;
			}
		}

		/// <summary>Gets or sets the activator that activates the remote object. </summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.Activation.IActivator" /> that activates the remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x00097F1B File Offset: 0x0009611B
		// (set) Token: 0x0600253B RID: 9531 RVA: 0x00097F23 File Offset: 0x00096123
		public IActivator Activator
		{
			get
			{
				return this._activator;
			}
			set
			{
				this._activator = value;
			}
		}

		/// <summary>Gets the call site activation attributes for the remote object. </summary>
		/// <returns>An array of type <see cref="T:System.Object" /> containing the call site activation attributes for the remote object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x00097F2C File Offset: 0x0009612C
		public object[] CallSiteActivationAttributes
		{
			get
			{
				return this._activationAttributes;
			}
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x00097F34 File Offset: 0x00096134
		internal void SetActivationAttributes(object[] attributes)
		{
			this._activationAttributes = attributes;
		}

		/// <summary>Gets a list of properties that define the context in which the remote object is to be created. </summary>
		/// <returns>A <see cref="T:System.Collections.IList" /> that contains a list of properties that define the context in which the remote object is to be created.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x00097F3D File Offset: 0x0009613D
		public IList ContextProperties
		{
			get
			{
				if (this._contextProperties == null)
				{
					this._contextProperties = new ArrayList();
				}
				return this._contextProperties;
			}
		}

		// Token: 0x0600253F RID: 9535 RVA: 0x00097F58 File Offset: 0x00096158
		internal override void InitMethodProperty(string key, object value)
		{
			if (key == "__Activator")
			{
				this._activator = (IActivator)value;
				return;
			}
			if (key == "__CallSiteActivationAttributes")
			{
				this._activationAttributes = (object[])value;
				return;
			}
			if (key == "__ActivationType")
			{
				this._activationType = (Type)value;
				return;
			}
			if (key == "__ContextProperties")
			{
				this._contextProperties = (IList)value;
				return;
			}
			if (!(key == "__ActivationTypeName"))
			{
				base.InitMethodProperty(key, value);
				return;
			}
			this._activationTypeName = (string)value;
		}

		// Token: 0x06002540 RID: 9536 RVA: 0x00097FF4 File Offset: 0x000961F4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			IList list = this._contextProperties;
			if (list != null && list.Count == 0)
			{
				list = null;
			}
			info.AddValue("__Activator", this._activator);
			info.AddValue("__CallSiteActivationAttributes", this._activationAttributes);
			info.AddValue("__ActivationType", null);
			info.AddValue("__ContextProperties", list);
			info.AddValue("__ActivationTypeName", this._activationTypeName);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> interface that represents a collection of the remoting message's properties.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06002541 RID: 9537 RVA: 0x00098068 File Offset: 0x00096268
		public override IDictionary Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x00098070 File Offset: 0x00096270
		// (set) Token: 0x06002543 RID: 9539 RVA: 0x00098078 File Offset: 0x00096278
		internal RemotingProxy SourceProxy
		{
			get
			{
				return this._sourceProxy;
			}
			set
			{
				this._sourceProxy = value;
			}
		}

		// Token: 0x040011EB RID: 4587
		private IActivator _activator;

		// Token: 0x040011EC RID: 4588
		private object[] _activationAttributes;

		// Token: 0x040011ED RID: 4589
		private IList _contextProperties;

		// Token: 0x040011EE RID: 4590
		private Type _activationType;

		// Token: 0x040011EF RID: 4591
		private string _activationTypeName;

		// Token: 0x040011F0 RID: 4592
		private bool _isContextOk;

		// Token: 0x040011F1 RID: 4593
		[NonSerialized]
		private RemotingProxy _sourceProxy;
	}
}
