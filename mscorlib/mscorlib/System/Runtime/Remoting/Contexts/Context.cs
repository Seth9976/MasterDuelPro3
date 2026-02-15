using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Defines an environment for the objects that are resident inside it and for which a policy can be enforced.</summary>
	// Token: 0x0200043D RID: 1085
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public class Context
	{
		// Token: 0x06002404 RID: 9220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterContext(Context ctx);

		// Token: 0x06002405 RID: 9221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseContext(Context ctx);

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> class.</summary>
		// Token: 0x06002406 RID: 9222 RVA: 0x000943FC File Offset: 0x000925FC
		public Context()
		{
			this.domain_id = Thread.GetDomainID();
			this.context_id = Interlocked.Increment(ref Context.global_count);
			Context.RegisterContext(this);
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x00094428 File Offset: 0x00092628
		~Context()
		{
			Context.ReleaseContext(this);
		}

		/// <summary>Gets the default context for the current application domain.</summary>
		/// <returns>The default context for the <see cref="T:System.AppDomain" /> namespace.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x00094454 File Offset: 0x00092654
		public static Context DefaultContext
		{
			get
			{
				return AppDomain.InternalGetDefaultContext();
			}
		}

		/// <summary>Gets the context ID for the current context.</summary>
		/// <returns>The context ID for the current context.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x0009445B File Offset: 0x0009265B
		public virtual int ContextID
		{
			get
			{
				return this.context_id;
			}
		}

		/// <summary>Gets the array of the current context properties.</summary>
		/// <returns>The current context properties array; otherwise, null if the context does not have any properties attributed to it.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x00094463 File Offset: 0x00092663
		public virtual IContextProperty[] ContextProperties
		{
			get
			{
				if (this.context_properties == null)
				{
					return new IContextProperty[0];
				}
				return this.context_properties.ToArray();
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x0009447F File Offset: 0x0009267F
		internal bool IsDefaultContext
		{
			get
			{
				return this.context_id == 0;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600240C RID: 9228 RVA: 0x0009448A File Offset: 0x0009268A
		internal bool NeedsContextSink
		{
			get
			{
				return this.context_id != 0 || (Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties) || (this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties);
			}
		}

		/// <summary>Registers a dynamic property implementing the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> interface with the remoting service.</summary>
		/// <returns>true if the property was successfully registered; otherwise, false.</returns>
		/// <param name="prop">The dynamic property to register. </param>
		/// <param name="obj">The object/proxy for which the <paramref name="property" /> is registered. </param>
		/// <param name="ctx">The context for which the <paramref name="property" /> is registered. </param>
		/// <exception cref="T:System.ArgumentNullException">Either <paramref name="prop" /> or its name is null, or it is not dynamic (it does not implement <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" />). </exception>
		/// <exception cref="T:System.ArgumentException">Both an object as well as a context are specified (both <paramref name="obj" /> and <paramref name="ctx" /> are not null). </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600240D RID: 9229 RVA: 0x000944BE File Offset: 0x000926BE
		public static bool RegisterDynamicProperty(IDynamicProperty prop, ContextBoundObject obj, Context ctx)
		{
			return Context.GetDynamicPropertyCollection(obj, ctx).RegisterDynamicProperty(prop);
		}

		/// <summary>Unregisters a dynamic property implementing the <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> interface.</summary>
		/// <returns>true if the object was successfully unregistered; otherwise, false.</returns>
		/// <param name="name">The name of the dynamic property to unregister. </param>
		/// <param name="obj">The object/proxy for which the <paramref name="property" /> is registered. </param>
		/// <param name="ctx">The context for which the <paramref name="property" /> is registered. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">Both an object as well as a context are specified (both <paramref name="obj" /> and <paramref name="ctx" /> are not null). </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600240E RID: 9230 RVA: 0x000944CD File Offset: 0x000926CD
		public static bool UnregisterDynamicProperty(string name, ContextBoundObject obj, Context ctx)
		{
			return Context.GetDynamicPropertyCollection(obj, ctx).UnregisterDynamicProperty(name);
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000944DC File Offset: 0x000926DC
		private static DynamicPropertyCollection GetDynamicPropertyCollection(ContextBoundObject obj, Context ctx)
		{
			if (ctx == null && obj != null)
			{
				if (RemotingServices.IsTransparentProxy(obj))
				{
					return RemotingServices.GetRealProxy(obj).ObjectIdentity.ClientDynamicProperties;
				}
				return obj.ObjectIdentity.ServerDynamicProperties;
			}
			else
			{
				if (ctx != null && obj == null)
				{
					if (ctx.context_dynamic_properties == null)
					{
						ctx.context_dynamic_properties = new DynamicPropertyCollection();
					}
					return ctx.context_dynamic_properties;
				}
				if (ctx == null && obj == null)
				{
					if (Context.global_dynamic_properties == null)
					{
						Context.global_dynamic_properties = new DynamicPropertyCollection();
					}
					return Context.global_dynamic_properties;
				}
				throw new ArgumentException("Either obj or ctx must be null");
			}
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x0009455B File Offset: 0x0009275B
		internal static void NotifyGlobalDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties)
			{
				Context.global_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06002411 RID: 9233 RVA: 0x0009457E File Offset: 0x0009277E
		internal static bool HasGlobalDynamicSinks
		{
			get
			{
				return Context.global_dynamic_properties != null && Context.global_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00094593 File Offset: 0x00092793
		internal void NotifyDynamicSinks(bool start, IMessage req_msg, bool client_site, bool async)
		{
			if (this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties)
			{
				this.context_dynamic_properties.NotifyMessage(start, req_msg, client_site, async);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06002413 RID: 9235 RVA: 0x000945BA File Offset: 0x000927BA
		internal bool HasDynamicSinks
		{
			get
			{
				return this.context_dynamic_properties != null && this.context_dynamic_properties.HasProperties;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x000945D1 File Offset: 0x000927D1
		internal bool HasExitSinks
		{
			get
			{
				return !(this.GetClientContextSinkChain() is ClientContextTerminatorSink) || this.HasDynamicSinks || Context.HasGlobalDynamicSinks;
			}
		}

		/// <summary>Returns a specific context property, specified by name.</summary>
		/// <returns>The specified context property.</returns>
		/// <param name="name">The name of the property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002415 RID: 9237 RVA: 0x000945F0 File Offset: 0x000927F0
		public virtual IContextProperty GetProperty(string name)
		{
			if (this.context_properties == null)
			{
				return null;
			}
			foreach (IContextProperty contextProperty in this.context_properties)
			{
				if (contextProperty.Name == name)
				{
					return contextProperty;
				}
			}
			return null;
		}

		/// <summary>Sets a specific context property by name.</summary>
		/// <param name="prop">The actual context property. </param>
		/// <exception cref="T:System.InvalidOperationException">There is an attempt to add properties to the default context. </exception>
		/// <exception cref="T:System.InvalidOperationException">The context is frozen. </exception>
		/// <exception cref="T:System.ArgumentNullException">The property or the property name is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002416 RID: 9238 RVA: 0x0009465C File Offset: 0x0009285C
		public virtual void SetProperty(IContextProperty prop)
		{
			if (prop == null)
			{
				throw new ArgumentNullException("IContextProperty");
			}
			if (this == Context.DefaultContext)
			{
				throw new InvalidOperationException("Can not add properties to default context");
			}
			if (this.context_properties == null)
			{
				this.context_properties = new List<IContextProperty>();
			}
			this.context_properties.Add(prop);
		}

		/// <summary>Freezes the context, making it impossible to add or remove context properties from the current context.</summary>
		/// <exception cref="T:System.InvalidOperationException">The context is already frozen. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002417 RID: 9239 RVA: 0x000946AC File Offset: 0x000928AC
		public virtual void Freeze()
		{
			if (this.context_properties != null)
			{
				foreach (IContextProperty contextProperty in this.context_properties)
				{
					contextProperty.Freeze(this);
				}
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> class representation of the current context.</summary>
		/// <returns>A <see cref="T:System.String" /> class representation of the current context.</returns>
		// Token: 0x06002418 RID: 9240 RVA: 0x00094708 File Offset: 0x00092908
		public override string ToString()
		{
			return "ContextID: " + this.context_id.ToString();
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x00094720 File Offset: 0x00092920
		internal IMessageSink GetServerContextSinkChain()
		{
			if (this.server_context_sink_chain == null)
			{
				if (Context.default_server_context_sink == null)
				{
					Context.default_server_context_sink = new ServerContextTerminatorSink();
				}
				this.server_context_sink_chain = Context.default_server_context_sink;
				if (this.context_properties != null)
				{
					for (int i = this.context_properties.Count - 1; i >= 0; i--)
					{
						IContributeServerContextSink contributeServerContextSink = this.context_properties[i] as IContributeServerContextSink;
						if (contributeServerContextSink != null)
						{
							this.server_context_sink_chain = contributeServerContextSink.GetServerContextSink(this.server_context_sink_chain);
						}
					}
				}
			}
			return this.server_context_sink_chain;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000947A0 File Offset: 0x000929A0
		internal IMessageSink GetClientContextSinkChain()
		{
			if (this.client_context_sink_chain == null)
			{
				this.client_context_sink_chain = new ClientContextTerminatorSink(this);
				if (this.context_properties != null)
				{
					foreach (IContextProperty contextProperty in this.context_properties)
					{
						IContributeClientContextSink contributeClientContextSink = contextProperty as IContributeClientContextSink;
						if (contributeClientContextSink != null)
						{
							this.client_context_sink_chain = contributeClientContextSink.GetClientContextSink(this.client_context_sink_chain);
						}
					}
				}
			}
			return this.client_context_sink_chain;
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x00094828 File Offset: 0x00092A28
		internal IMessageSink CreateServerObjectSinkChain(MarshalByRefObject obj, bool forceInternalExecute)
		{
			IMessageSink messageSink = new StackBuilderSink(obj, forceInternalExecute);
			messageSink = new ServerObjectTerminatorSink(messageSink);
			messageSink = new LeaseSink(messageSink);
			if (this.context_properties != null)
			{
				for (int i = this.context_properties.Count - 1; i >= 0; i--)
				{
					IContributeObjectSink contributeObjectSink = this.context_properties[i] as IContributeObjectSink;
					if (contributeObjectSink != null)
					{
						messageSink = contributeObjectSink.GetObjectSink(obj, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x0009488C File Offset: 0x00092A8C
		internal IMessageSink CreateEnvoySink(MarshalByRefObject serverObject)
		{
			IMessageSink messageSink = EnvoyTerminatorSink.Instance;
			if (this.context_properties != null)
			{
				foreach (IContextProperty contextProperty in this.context_properties)
				{
					IContributeEnvoySink contributeEnvoySink = contextProperty as IContributeEnvoySink;
					if (contributeEnvoySink != null)
					{
						messageSink = contributeEnvoySink.GetEnvoySink(serverObject, messageSink);
					}
				}
			}
			return messageSink;
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000948F8 File Offset: 0x00092AF8
		internal static Context SwitchToContext(Context newContext)
		{
			return AppDomain.InternalSetContext(newContext);
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x00094900 File Offset: 0x00092B00
		internal static Context CreateNewContext(IConstructionCallMessage msg)
		{
			Context context = new Context();
			foreach (object obj in msg.ContextProperties)
			{
				IContextProperty contextProperty = (IContextProperty)obj;
				if (context.GetProperty(contextProperty.Name) == null)
				{
					context.SetProperty(contextProperty);
				}
			}
			context.Freeze();
			using (IEnumerator enumerator = msg.ContextProperties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((IContextProperty)enumerator.Current).IsNewContextOK(context))
					{
						throw new RemotingException("A context property did not approve the candidate context for activating the object");
					}
				}
			}
			return context;
		}

		/// <summary>Executes code in another context.</summary>
		/// <param name="deleg">The delegate used to request the callback. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600241F RID: 9247 RVA: 0x000949C8 File Offset: 0x00092BC8
		public void DoCallBack(CrossContextDelegate deleg)
		{
			lock (this)
			{
				if (this.callback_object == null)
				{
					Context context = Context.SwitchToContext(this);
					this.callback_object = new ContextCallbackObject();
					Context.SwitchToContext(context);
				}
			}
			this.callback_object.DoCallBack(deleg);
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x00094A28 File Offset: 0x00092C28
		private LocalDataStore MyLocalStore
		{
			get
			{
				if (this._localDataStore == null)
				{
					LocalDataStoreMgr localDataStoreMgr = Context._localDataStoreMgr;
					lock (localDataStoreMgr)
					{
						if (this._localDataStore == null)
						{
							this._localDataStore = Context._localDataStoreMgr.CreateLocalDataStore();
						}
					}
				}
				return this._localDataStore.Store;
			}
		}

		/// <summary>Allocates an unnamed data slot.</summary>
		/// <returns>A local data slot.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002421 RID: 9249 RVA: 0x00094A94 File Offset: 0x00092C94
		public static LocalDataStoreSlot AllocateDataSlot()
		{
			return Context._localDataStoreMgr.AllocateDataSlot();
		}

		/// <summary>Allocates a named data slot.</summary>
		/// <returns>A local data slot object.</returns>
		/// <param name="name">The required name for the data slot. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002422 RID: 9250 RVA: 0x00094AA0 File Offset: 0x00092CA0
		public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.AllocateNamedDataSlot(name);
		}

		/// <summary>Frees a named data slot on all the contexts.</summary>
		/// <param name="name">The name of the data slot to free. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002423 RID: 9251 RVA: 0x00094AAD File Offset: 0x00092CAD
		public static void FreeNamedDataSlot(string name)
		{
			Context._localDataStoreMgr.FreeNamedDataSlot(name);
		}

		/// <summary>Looks up a named data slot.</summary>
		/// <returns>Returns a local data slot.</returns>
		/// <param name="name">The data slot name. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002424 RID: 9252 RVA: 0x00094ABA File Offset: 0x00092CBA
		public static LocalDataStoreSlot GetNamedDataSlot(string name)
		{
			return Context._localDataStoreMgr.GetNamedDataSlot(name);
		}

		/// <summary>Retrieves the value from the specified slot on the current context.</summary>
		/// <returns>Returns the data associated with <paramref name="slot" />. </returns>
		/// <param name="slot">The data slot that contains the data. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002425 RID: 9253 RVA: 0x00094AC7 File Offset: 0x00092CC7
		public static object GetData(LocalDataStoreSlot slot)
		{
			return Thread.CurrentContext.MyLocalStore.GetData(slot);
		}

		/// <summary>Sets the data in the specified slot on the current context.</summary>
		/// <param name="slot">The data slot where the data is to be added. </param>
		/// <param name="data">The data that is to be added. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002426 RID: 9254 RVA: 0x00094AD9 File Offset: 0x00092CD9
		public static void SetData(LocalDataStoreSlot slot, object data)
		{
			Thread.CurrentContext.MyLocalStore.SetData(slot, data);
		}

		// Token: 0x04001167 RID: 4455
		private int domain_id;

		// Token: 0x04001168 RID: 4456
		private int context_id;

		// Token: 0x04001169 RID: 4457
		private UIntPtr static_data;

		// Token: 0x0400116A RID: 4458
		private UIntPtr data;

		// Token: 0x0400116B RID: 4459
		[ContextStatic]
		private static object[] local_slots;

		// Token: 0x0400116C RID: 4460
		private static IMessageSink default_server_context_sink;

		// Token: 0x0400116D RID: 4461
		private IMessageSink server_context_sink_chain;

		// Token: 0x0400116E RID: 4462
		private IMessageSink client_context_sink_chain;

		// Token: 0x0400116F RID: 4463
		private List<IContextProperty> context_properties;

		// Token: 0x04001170 RID: 4464
		private static int global_count;

		// Token: 0x04001171 RID: 4465
		private volatile LocalDataStoreHolder _localDataStore;

		// Token: 0x04001172 RID: 4466
		private static LocalDataStoreMgr _localDataStoreMgr = new LocalDataStoreMgr();

		// Token: 0x04001173 RID: 4467
		private static DynamicPropertyCollection global_dynamic_properties;

		// Token: 0x04001174 RID: 4468
		private DynamicPropertyCollection context_dynamic_properties;

		// Token: 0x04001175 RID: 4469
		private ContextCallbackObject callback_object;
	}
}
