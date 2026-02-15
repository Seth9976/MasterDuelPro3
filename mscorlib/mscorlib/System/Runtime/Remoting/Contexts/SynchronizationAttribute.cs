using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Enforces a synchronization domain for the current context and all contexts that share the same instance.</summary>
	// Token: 0x0200044E RID: 1102
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class)]
	[Serializable]
	public class SynchronizationAttribute : ContextAttribute, IContributeClientContextSink, IContributeServerContextSink
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with default values.</summary>
		// Token: 0x0600244E RID: 9294 RVA: 0x00095008 File Offset: 0x00093208
		public SynchronizationAttribute()
			: this(8, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> class with a flag indicating the behavior of the object to which this attribute is applied, and a Boolean value indicating whether reentry is required.</summary>
		/// <param name="flag">An integer value indicating the behavior of the object to which this attribute is applied. </param>
		/// <param name="reEntrant">true if reentry is required, and callouts must be intercepted and serialized; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="flag" /> parameter was not one of the defined flags. </exception>
		// Token: 0x0600244F RID: 9295 RVA: 0x00095014 File Offset: 0x00093214
		public SynchronizationAttribute(int flag, bool reEntrant)
			: base("Synchronization")
		{
			if (flag != 1 && flag != 4 && flag != 8 && flag != 2)
			{
				throw new ArgumentException("flag");
			}
			this._bReEntrant = reEntrant;
			this._flavor = flag;
		}

		/// <summary>Gets or sets a Boolean value indicating whether reentry is required.</summary>
		/// <returns>A Boolean value indicating whether reentry is required.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x00095061 File Offset: 0x00093261
		public virtual bool IsReEntrant
		{
			get
			{
				return this._bReEntrant;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> implementing this instance of <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> is locked.</summary>
		/// <returns>A Boolean value indicating whether the <see cref="T:System.Runtime.Remoting.Contexts.Context" /> implementing this instance of <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> is locked.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000453 RID: 1107
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x0009506C File Offset: 0x0009326C
		public virtual bool Locked
		{
			set
			{
				SynchronizationAttribute synchronizationAttribute;
				if (value)
				{
					this.AcquireLock();
					synchronizationAttribute = this;
					lock (synchronizationAttribute)
					{
						if (this._lockCount > 1)
						{
							this.ReleaseLock();
						}
						return;
					}
				}
				synchronizationAttribute = this;
				lock (synchronizationAttribute)
				{
					while (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
					{
						this.ReleaseLock();
					}
				}
			}
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000950FC File Offset: 0x000932FC
		internal void AcquireLock()
		{
			this._mutex.WaitOne();
			lock (this)
			{
				this._ownerThread = Thread.CurrentThread;
				this._lockCount++;
			}
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00095158 File Offset: 0x00093358
		internal void ReleaseLock()
		{
			lock (this)
			{
				if (this._lockCount > 0 && this._ownerThread == Thread.CurrentThread)
				{
					this._lockCount--;
					this._mutex.ReleaseMutex();
					if (this._lockCount == 0)
					{
						this._ownerThread = null;
					}
				}
			}
		}

		/// <summary>Adds the Synchronized context property to the specified <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.</summary>
		/// <param name="ctorMsg">The <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" /> to which to add the property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002454 RID: 9300 RVA: 0x000951CC File Offset: 0x000933CC
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (this._flavor != 1)
			{
				ctorMsg.ContextProperties.Add(this);
			}
		}

		/// <summary>Creates a CallOut sink and chains it in front of the provided chain of sinks at the context boundary on the client end of a remoting call.</summary>
		/// <returns>The composite sink chain with the new CallOut sink.</returns>
		/// <param name="nextSink">The chain of sinks composed so far. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002455 RID: 9301 RVA: 0x000951E4 File Offset: 0x000933E4
		public virtual IMessageSink GetClientContextSink(IMessageSink nextSink)
		{
			return new SynchronizedClientContextSink(nextSink, this);
		}

		/// <summary>Creates a synchronized dispatch sink and chains it in front of the provided chain of sinks at the context boundary on the server end of a remoting call.</summary>
		/// <returns>The composite sink chain with the new synchronized dispatch sink.</returns>
		/// <param name="nextSink">The chain of sinks composed so far. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002456 RID: 9302 RVA: 0x000951ED File Offset: 0x000933ED
		public virtual IMessageSink GetServerContextSink(IMessageSink nextSink)
		{
			return new SynchronizedServerContextSink(nextSink, this);
		}

		/// <summary>Returns a Boolean value indicating whether the context parameter meets the context attribute's requirements.</summary>
		/// <returns>true if the passed in context is OK; otherwise, false.</returns>
		/// <param name="ctx">The context to check. </param>
		/// <param name="msg">Information gathered at construction time of the context bound object marked by this attribute. The <see cref="T:System.Runtime.Remoting.Contexts.SynchronizationAttribute" /> can inspect, add to, and remove properties from the context while determining if the context is acceptable to it. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ctx" /> or <paramref name="msg" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002457 RID: 9303 RVA: 0x000951F8 File Offset: 0x000933F8
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			SynchronizationAttribute synchronizationAttribute = ctx.GetProperty("Synchronization") as SynchronizationAttribute;
			int flavor = this._flavor;
			switch (flavor)
			{
			case 1:
				return synchronizationAttribute == null;
			case 2:
				return true;
			case 3:
				break;
			case 4:
				return synchronizationAttribute != null;
			default:
				if (flavor == 8)
				{
					return false;
				}
				break;
			}
			return false;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x0009524C File Offset: 0x0009344C
		internal static void ExitContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = false;
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00095288 File Offset: 0x00093488
		internal static void EnterContext()
		{
			if (Thread.CurrentContext.IsDefaultContext)
			{
				return;
			}
			SynchronizationAttribute synchronizationAttribute = Thread.CurrentContext.GetProperty("Synchronization") as SynchronizationAttribute;
			if (synchronizationAttribute == null)
			{
				return;
			}
			synchronizationAttribute.Locked = true;
		}

		// Token: 0x0400117D RID: 4477
		private bool _bReEntrant;

		// Token: 0x0400117E RID: 4478
		private int _flavor;

		// Token: 0x0400117F RID: 4479
		[NonSerialized]
		private int _lockCount;

		// Token: 0x04001180 RID: 4480
		[NonSerialized]
		private Mutex _mutex = new Mutex(false);

		// Token: 0x04001181 RID: 4481
		[NonSerialized]
		private Thread _ownerThread;
	}
}
