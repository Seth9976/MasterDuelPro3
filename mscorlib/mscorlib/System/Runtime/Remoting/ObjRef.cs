using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting
{
	/// <summary>Stores all relevant information required to generate a proxy in order to communicate with a remote object.</summary>
	// Token: 0x02000419 RID: 1049
	[ComVisible(true)]
	[Serializable]
	public class ObjRef : IObjectReference, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class with default values.</summary>
		// Token: 0x060022E3 RID: 8931 RVA: 0x0008F7E8 File Offset: 0x0008D9E8
		public ObjRef()
		{
			this.UpdateChannelInfo();
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0008F7F6 File Offset: 0x0008D9F6
		internal ObjRef(string uri, IChannelInfo cinfo)
		{
			this.uri = uri;
			this.channel_info = cinfo;
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0008F80C File Offset: 0x0008DA0C
		internal ObjRef DeserializeInTheCurrentDomain(int domainId, byte[] tInfo)
		{
			string text = string.Copy(this.uri);
			ChannelInfo channelInfo = new ChannelInfo(new CrossAppDomainData(domainId));
			ObjRef objRef = new ObjRef(text, channelInfo);
			IRemotingTypeInfo remotingTypeInfo = (IRemotingTypeInfo)CADSerializer.DeserializeObjectSafe(tInfo);
			objRef.typeInfo = remotingTypeInfo;
			return objRef;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0008F849 File Offset: 0x0008DA49
		internal byte[] SerializeType()
		{
			if (this.typeInfo == null)
			{
				throw new Exception("Attempt to serialize a null TypeInfo.");
			}
			return CADSerializer.SerializeObject(this.typeInfo).GetBuffer();
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x0008F86E File Offset: 0x0008DA6E
		internal ObjRef(Type type, string url, object remoteChannelData)
		{
			this.uri = url;
			this.typeInfo = new TypeInfo(type);
			if (remoteChannelData != null)
			{
				this.channel_info = new ChannelInfo(remoteChannelData);
			}
			this.flags |= ObjRef.WellKnowObjectRef;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.ObjRef" /> class from serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination of the exception. </param>
		// Token: 0x060022E8 RID: 8936 RVA: 0x0008F8AC File Offset: 0x0008DAAC
		protected ObjRef(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			bool flag = true;
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "uri"))
				{
					if (!(name == "typeInfo"))
					{
						if (!(name == "channelInfo"))
						{
							if (!(name == "envoyInfo"))
							{
								if (!(name == "fIsMarshalled"))
								{
									if (!(name == "objrefFlags"))
									{
										throw new NotSupportedException();
									}
									this.flags = Convert.ToInt32(enumerator.Value);
								}
								else
								{
									object value = enumerator.Value;
									int num;
									if (value is string)
									{
										num = ((IConvertible)value).ToInt32(null);
									}
									else
									{
										num = (int)value;
									}
									if (num == 0)
									{
										flag = false;
									}
								}
							}
							else
							{
								this.envoyInfo = (IEnvoyInfo)enumerator.Value;
							}
						}
						else
						{
							this.channel_info = (IChannelInfo)enumerator.Value;
						}
					}
					else
					{
						this.typeInfo = (IRemotingTypeInfo)enumerator.Value;
					}
				}
				else
				{
					this.uri = (string)enumerator.Value;
				}
			}
			if (flag)
			{
				this.flags |= ObjRef.MarshalledObjectRef;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x0008F9DF File Offset: 0x0008DBDF
		internal bool IsReferenceToWellKnow
		{
			get
			{
				return (this.flags & ObjRef.WellKnowObjectRef) > 0;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Remoting.IChannelInfo" /> for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.IChannelInfo" /> interface for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x0008F9F0 File Offset: 0x0008DBF0
		public virtual IChannelInfo ChannelInfo
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.channel_info;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Remoting.IEnvoyInfo" /> for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.IEnvoyInfo" /> interface for the <see cref="T:System.Runtime.Remoting.ObjRef" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x0008F9F8 File Offset: 0x0008DBF8
		// (set) Token: 0x060022EC RID: 8940 RVA: 0x0008FA00 File Offset: 0x0008DC00
		public virtual IEnvoyInfo EnvoyInfo
		{
			get
			{
				return this.envoyInfo;
			}
			set
			{
				this.envoyInfo = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> for the object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> for the object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x0008FA09 File Offset: 0x0008DC09
		// (set) Token: 0x060022EE RID: 8942 RVA: 0x0008FA11 File Offset: 0x0008DC11
		public virtual IRemotingTypeInfo TypeInfo
		{
			get
			{
				return this.typeInfo;
			}
			set
			{
				this.typeInfo = value;
			}
		}

		/// <summary>Gets or sets the URI of the specific object instance.</summary>
		/// <returns>The URI of the specific object instance.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x0008FA1A File Offset: 0x0008DC1A
		// (set) Token: 0x060022F0 RID: 8944 RVA: 0x0008FA22 File Offset: 0x0008DC22
		public virtual string URI
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		/// <summary>Populates a specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the current <see cref="T:System.Runtime.Remoting.ObjRef" /> instance.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">The contextual information about the source or destination of the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have serialization formatter permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060022F1 RID: 8945 RVA: 0x0008FA2C File Offset: 0x0008DC2C
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.SetType(base.GetType());
			info.AddValue("uri", this.uri);
			info.AddValue("typeInfo", this.typeInfo, typeof(IRemotingTypeInfo));
			info.AddValue("envoyInfo", this.envoyInfo, typeof(IEnvoyInfo));
			info.AddValue("channelInfo", this.channel_info, typeof(IChannelInfo));
			info.AddValue("objrefFlags", this.flags);
		}

		/// <summary>Returns a reference to the remote object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</summary>
		/// <returns>A reference to the remote object that the <see cref="T:System.Runtime.Remoting.ObjRef" /> describes.</returns>
		/// <param name="context">The context where the current object resides. </param>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have serialization formatter permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter, RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		// Token: 0x060022F2 RID: 8946 RVA: 0x0008FAB8 File Offset: 0x0008DCB8
		public virtual object GetRealObject(StreamingContext context)
		{
			if ((this.flags & ObjRef.MarshalledObjectRef) > 0)
			{
				return RemotingServices.Unmarshal(this);
			}
			return this;
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x0008FAD1 File Offset: 0x0008DCD1
		internal void UpdateChannelInfo()
		{
			this.channel_info = new ChannelInfo();
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0008FADE File Offset: 0x0008DCDE
		internal Type ServerType
		{
			get
			{
				if (this._serverType == null)
				{
					this._serverType = Type.GetType(this.typeInfo.TypeName);
				}
				return this._serverType;
			}
		}

		// Token: 0x040010EE RID: 4334
		private IChannelInfo channel_info;

		// Token: 0x040010EF RID: 4335
		private string uri;

		// Token: 0x040010F0 RID: 4336
		private IRemotingTypeInfo typeInfo;

		// Token: 0x040010F1 RID: 4337
		private IEnvoyInfo envoyInfo;

		// Token: 0x040010F2 RID: 4338
		private int flags;

		// Token: 0x040010F3 RID: 4339
		private Type _serverType;

		// Token: 0x040010F4 RID: 4340
		private static int MarshalledObjectRef = 1;

		// Token: 0x040010F5 RID: 4341
		private static int WellKnowObjectRef = 2;
	}
}
