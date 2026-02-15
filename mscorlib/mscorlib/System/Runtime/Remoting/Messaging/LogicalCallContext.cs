using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Provides a set of properties that are carried with the execution code path during remote method calls.</summary>
	// Token: 0x02000473 RID: 1139
	[ComVisible(true)]
	[Serializable]
	public sealed class LogicalCallContext : ISerializable, ICloneable
	{
		// Token: 0x060024DA RID: 9434 RVA: 0x00003CE1 File Offset: 0x00001EE1
		internal LogicalCallContext()
		{
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x00096A10 File Offset: 0x00094C10
		internal LogicalCallContext(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name.Equals("__RemotingData"))
				{
					this.m_RemotingData = (CallContextRemotingData)enumerator.Value;
				}
				else if (enumerator.Name.Equals("__SecurityData"))
				{
					if (context.State == StreamingContextStates.CrossAppDomain)
					{
						this.m_SecurityData = (CallContextSecurityData)enumerator.Value;
					}
				}
				else if (enumerator.Name.Equals("__HostContext"))
				{
					this.m_HostContext = enumerator.Value;
				}
				else if (enumerator.Name.Equals("__CorrelationMgrSlotPresent"))
				{
					this.m_IsCorrelationMgr = (bool)enumerator.Value;
				}
				else
				{
					this.Datastore[enumerator.Name] = enumerator.Value;
				}
			}
		}

		/// <summary>Populates a specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the current <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">The contextual information about the source or destination of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have SerializationFormatter permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060024DC RID: 9436 RVA: 0x00096AF4 File Offset: 0x00094CF4
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.SetType(LogicalCallContext.s_callContextType);
			if (this.m_RemotingData != null)
			{
				info.AddValue("__RemotingData", this.m_RemotingData);
			}
			if (this.m_SecurityData != null && context.State == StreamingContextStates.CrossAppDomain)
			{
				info.AddValue("__SecurityData", this.m_SecurityData);
			}
			if (this.m_HostContext != null)
			{
				info.AddValue("__HostContext", this.m_HostContext);
			}
			if (this.m_IsCorrelationMgr)
			{
				info.AddValue("__CorrelationMgrSlotPresent", this.m_IsCorrelationMgr);
			}
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					info.AddValue((string)enumerator.Key, enumerator.Value);
				}
			}
		}

		/// <summary>Creates a new object that is a copy of the current instance.</summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060024DD RID: 9437 RVA: 0x00096BC4 File Offset: 0x00094DC4
		public object Clone()
		{
			LogicalCallContext logicalCallContext = new LogicalCallContext();
			if (this.m_RemotingData != null)
			{
				logicalCallContext.m_RemotingData = (CallContextRemotingData)this.m_RemotingData.Clone();
			}
			if (this.m_SecurityData != null)
			{
				logicalCallContext.m_SecurityData = (CallContextSecurityData)this.m_SecurityData.Clone();
			}
			if (this.m_HostContext != null)
			{
				logicalCallContext.m_HostContext = this.m_HostContext;
			}
			logicalCallContext.m_IsCorrelationMgr = this.m_IsCorrelationMgr;
			if (this.HasUserData)
			{
				IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
				if (!this.m_IsCorrelationMgr)
				{
					while (enumerator.MoveNext())
					{
						logicalCallContext.Datastore[(string)enumerator.Key] = enumerator.Value;
					}
				}
				else
				{
					while (enumerator.MoveNext())
					{
						string text = (string)enumerator.Key;
						if (text.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
						{
							logicalCallContext.Datastore[text] = ((ICloneable)enumerator.Value).Clone();
						}
						else
						{
							logicalCallContext.Datastore[text] = enumerator.Value;
						}
					}
				}
			}
			return logicalCallContext;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x00096CCC File Offset: 0x00094ECC
		internal void Merge(LogicalCallContext lc)
		{
			if (lc != null && this != lc && lc.HasUserData)
			{
				IDictionaryEnumerator enumerator = lc.Datastore.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this.Datastore[(string)enumerator.Key] = enumerator.Value;
				}
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> contains information.</summary>
		/// <returns>A Boolean value indicating whether the current <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> contains information.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060024DF RID: 9439 RVA: 0x00096D1C File Offset: 0x00094F1C
		public bool HasInfo
		{
			get
			{
				bool flag = false;
				if ((this.m_RemotingData != null && this.m_RemotingData.HasInfo) || (this.m_SecurityData != null && this.m_SecurityData.HasInfo) || this.m_HostContext != null || this.HasUserData)
				{
					flag = true;
				}
				return flag;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00096D68 File Offset: 0x00094F68
		private bool HasUserData
		{
			get
			{
				return this.m_Datastore != null && this.m_Datastore.Count > 0;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060024E1 RID: 9441 RVA: 0x00096D82 File Offset: 0x00094F82
		internal CallContextRemotingData RemotingData
		{
			get
			{
				if (this.m_RemotingData == null)
				{
					this.m_RemotingData = new CallContextRemotingData();
				}
				return this.m_RemotingData;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x00096D9D File Offset: 0x00094F9D
		internal CallContextSecurityData SecurityData
		{
			get
			{
				if (this.m_SecurityData == null)
				{
					this.m_SecurityData = new CallContextSecurityData();
				}
				return this.m_SecurityData;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060024E3 RID: 9443 RVA: 0x00096DB8 File Offset: 0x00094FB8
		private Hashtable Datastore
		{
			get
			{
				if (this.m_Datastore == null)
				{
					this.m_Datastore = new Hashtable();
				}
				return this.m_Datastore;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x00096DD3 File Offset: 0x00094FD3
		// (set) Token: 0x060024E5 RID: 9445 RVA: 0x00096DEA File Offset: 0x00094FEA
		internal IPrincipal Principal
		{
			get
			{
				if (this.m_SecurityData != null)
				{
					return this.m_SecurityData.Principal;
				}
				return null;
			}
			set
			{
				this.SecurityData.Principal = value;
			}
		}

		/// <summary>Retrieves an object associated with the specified name from the current instance.</summary>
		/// <returns>The object in the logical call context associated with the specified name.</returns>
		/// <param name="name">The name of the item in the call context. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060024E6 RID: 9446 RVA: 0x00096DF8 File Offset: 0x00094FF8
		public object GetData(string name)
		{
			return this.Datastore[name];
		}

		/// <summary>Stores the specified object in the current instance, and associates it with the specified name.</summary>
		/// <param name="name">The name with which to associate the new item in the call context. </param>
		/// <param name="data">The object to store in the call context. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060024E7 RID: 9447 RVA: 0x00096E06 File Offset: 0x00095006
		public void SetData(string name, object data)
		{
			this.Datastore[name] = data;
			if (name.Equals("System.Diagnostics.Trace.CorrelationManagerSlot"))
			{
				this.m_IsCorrelationMgr = true;
			}
		}

		// Token: 0x040011B7 RID: 4535
		private static Type s_callContextType = typeof(LogicalCallContext);

		// Token: 0x040011B8 RID: 4536
		private Hashtable m_Datastore;

		// Token: 0x040011B9 RID: 4537
		private CallContextRemotingData m_RemotingData;

		// Token: 0x040011BA RID: 4538
		private CallContextSecurityData m_SecurityData;

		// Token: 0x040011BB RID: 4539
		private object m_HostContext;

		// Token: 0x040011BC RID: 4540
		private bool m_IsCorrelationMgr;

		// Token: 0x02000474 RID: 1140
		internal struct Reader
		{
			// Token: 0x060024E9 RID: 9449 RVA: 0x00096E3A File Offset: 0x0009503A
			public Reader(LogicalCallContext ctx)
			{
				this.m_ctx = ctx;
			}

			// Token: 0x17000484 RID: 1156
			// (get) Token: 0x060024EA RID: 9450 RVA: 0x00096E43 File Offset: 0x00095043
			public bool IsNull
			{
				get
				{
					return this.m_ctx == null;
				}
			}

			// Token: 0x17000485 RID: 1157
			// (get) Token: 0x060024EB RID: 9451 RVA: 0x00096E4E File Offset: 0x0009504E
			public bool HasInfo
			{
				get
				{
					return !this.IsNull && this.m_ctx.HasInfo;
				}
			}

			// Token: 0x060024EC RID: 9452 RVA: 0x00096E65 File Offset: 0x00095065
			public LogicalCallContext Clone()
			{
				return (LogicalCallContext)this.m_ctx.Clone();
			}

			// Token: 0x17000486 RID: 1158
			// (get) Token: 0x060024ED RID: 9453 RVA: 0x00096E77 File Offset: 0x00095077
			public IPrincipal Principal
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_ctx.Principal;
					}
					return null;
				}
			}

			// Token: 0x060024EE RID: 9454 RVA: 0x00096E8E File Offset: 0x0009508E
			public object GetData(string name)
			{
				if (!this.IsNull)
				{
					return this.m_ctx.GetData(name);
				}
				return null;
			}

			// Token: 0x040011BD RID: 4541
			private LogicalCallContext m_ctx;
		}
	}
}
