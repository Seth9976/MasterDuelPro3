using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using Mono.Xml;

namespace System.Runtime.Remoting
{
	/// <summary>Provides various static methods for configuring the remoting infrastructure.</summary>
	// Token: 0x0200041B RID: 1051
	[ComVisible(true)]
	public static class RemotingConfiguration
	{
		/// <summary>Gets or sets the name of a remoting application.</summary>
		/// <returns>The name of a remoting application.</returns>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. This exception is thrown only when setting the property value. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x0008FB37 File Offset: 0x0008DD37
		// (set) Token: 0x060022FA RID: 8954 RVA: 0x0008FB3E File Offset: 0x0008DD3E
		public static string ApplicationName
		{
			get
			{
				return RemotingConfiguration.applicationName;
			}
			set
			{
				RemotingConfiguration.applicationName = value;
			}
		}

		/// <summary>Gets the ID of the currently executing process.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the ID of the currently executing process.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x0008FB46 File Offset: 0x0008DD46
		public static string ProcessId
		{
			get
			{
				if (RemotingConfiguration.processGuid == null)
				{
					RemotingConfiguration.processGuid = AppDomain.GetProcessGuid();
				}
				return RemotingConfiguration.processGuid;
			}
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x0008FB60 File Offset: 0x0008DD60
		internal static void LoadDefaultDelayedChannels()
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				if (!RemotingConfiguration.defaultDelayedConfigRead && !RemotingConfiguration.defaultConfigRead)
				{
					SmallXmlParser smallXmlParser = new SmallXmlParser();
					using (TextReader textReader = new StreamReader(Environment.GetMachineConfigPath()))
					{
						ConfigHandler configHandler = new ConfigHandler(true);
						smallXmlParser.Parse(textReader, configHandler);
					}
					RemotingConfiguration.defaultDelayedConfigRead = true;
				}
			}
		}

		/// <summary>Returns a Boolean value that indicates whether the specified <see cref="T:System.Type" /> is allowed to be client activated.</summary>
		/// <returns>true if the specified <see cref="T:System.Type" /> is allowed to be client activated; otherwise, false.</returns>
		/// <param name="svrType">The object <see cref="T:System.Type" /> to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x060022FD RID: 8957 RVA: 0x0008FBEC File Offset: 0x0008DDEC
		public static bool IsActivationAllowed(Type svrType)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			bool flag2;
			lock (hashtable)
			{
				flag2 = RemotingConfiguration.activatedServiceEntries.ContainsKey(svrType);
			}
			return flag2;
		}

		/// <summary>Checks whether the specified object <see cref="T:System.Type" /> is registered as a remotely activated client type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <param name="svrType">The object type to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x060022FE RID: 8958 RVA: 0x0008FC34 File Offset: 0x0008DE34
		public static ActivatedClientTypeEntry IsRemotelyActivatedClientType(Type svrType)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			ActivatedClientTypeEntry activatedClientTypeEntry;
			lock (hashtable)
			{
				activatedClientTypeEntry = RemotingConfiguration.activatedClientEntries[svrType] as ActivatedClientTypeEntry;
			}
			return activatedClientTypeEntry;
		}

		/// <summary>Checks whether the specified object <see cref="T:System.Type" /> is registered as a well-known client type.</summary>
		/// <returns>The <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> that corresponds to the specified object type.</returns>
		/// <param name="svrType">The object <see cref="T:System.Type" /> to check. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x060022FF RID: 8959 RVA: 0x0008FC80 File Offset: 0x0008DE80
		public static WellKnownClientTypeEntry IsWellKnownClientType(Type svrType)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			WellKnownClientTypeEntry wellKnownClientTypeEntry;
			lock (hashtable)
			{
				wellKnownClientTypeEntry = RemotingConfiguration.wellKnownClientEntries[svrType] as WellKnownClientTypeEntry;
			}
			return wellKnownClientTypeEntry;
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.ActivatedClientTypeEntry" /> on the client end as a type that can be activated on the server.</summary>
		/// <param name="entry">Configuration settings for the client-activated type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002300 RID: 8960 RVA: 0x0008FCCC File Offset: 0x0008DECC
		public static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.activatedClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		/// <summary>Registers an object type recorded in the provided <see cref="T:System.Runtime.Remoting.ActivatedServiceTypeEntry" /> on the service end as one that can be activated on request from a client.</summary>
		/// <param name="entry">Configuration settings for the client-activated type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002301 RID: 8961 RVA: 0x0008FD68 File Offset: 0x0008DF68
		public static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				RemotingConfiguration.activatedServiceEntries.Add(entry.ObjectType, entry);
			}
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.WellKnownClientTypeEntry" /> on the client end as a well-known type that can be activated on the server.</summary>
		/// <param name="entry">Configuration settings for the well-known type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002302 RID: 8962 RVA: 0x0008FDB4 File Offset: 0x0008DFB4
		public static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				if (RemotingConfiguration.wellKnownClientEntries.ContainsKey(entry.ObjectType) || RemotingConfiguration.activatedClientEntries.ContainsKey(entry.ObjectType))
				{
					throw new RemotingException("Attempt to redirect activation of type '" + entry.ObjectType.FullName + "' which is already redirected.");
				}
				RemotingConfiguration.wellKnownClientEntries[entry.ObjectType] = entry;
				ActivationServices.EnableProxyActivation(entry.ObjectType, true);
			}
		}

		/// <summary>Registers an object <see cref="T:System.Type" /> recorded in the provided <see cref="T:System.Runtime.Remoting.WellKnownServiceTypeEntry" /> on the service end as a well-known type.</summary>
		/// <param name="entry">Configuration settings for the well-known type. </param>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002303 RID: 8963 RVA: 0x0008FE50 File Offset: 0x0008E050
		public static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
		{
			Hashtable hashtable = RemotingConfiguration.channelTemplates;
			lock (hashtable)
			{
				RemotingConfiguration.wellKnownServiceEntries[entry.ObjectUri] = entry;
				RemotingServices.CreateWellKnownServerIdentity(entry.ObjectType, entry.ObjectUri, entry.Mode);
			}
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x0008FEB4 File Offset: 0x0008E0B4
		internal static void RegisterChannelTemplate(ChannelData channel)
		{
			RemotingConfiguration.channelTemplates[channel.Id] = channel;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x0008FEC7 File Offset: 0x0008E0C7
		internal static void RegisterClientProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.clientProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x0008FEDA File Offset: 0x0008E0DA
		internal static void RegisterServerProviderTemplate(ProviderData prov)
		{
			RemotingConfiguration.serverProviderTemplates[prov.Id] = prov;
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x0008FEF0 File Offset: 0x0008E0F0
		internal static void RegisterChannels(ArrayList channels, bool onlyDelayed)
		{
			foreach (object obj in channels)
			{
				ChannelData channelData = (ChannelData)obj;
				if ((!onlyDelayed || !(channelData.DelayLoadAsClientChannel != "true")) && (!RemotingConfiguration.defaultDelayedConfigRead || !(channelData.DelayLoadAsClientChannel == "true")))
				{
					if (channelData.Ref != null)
					{
						ChannelData channelData2 = (ChannelData)RemotingConfiguration.channelTemplates[channelData.Ref];
						if (channelData2 == null)
						{
							throw new RemotingException("Channel template '" + channelData.Ref + "' not found");
						}
						channelData.CopyFrom(channelData2);
					}
					foreach (object obj2 in channelData.ServerProviders)
					{
						ProviderData providerData = (ProviderData)obj2;
						if (providerData.Ref != null)
						{
							ProviderData providerData2 = (ProviderData)RemotingConfiguration.serverProviderTemplates[providerData.Ref];
							if (providerData2 == null)
							{
								throw new RemotingException("Provider template '" + providerData.Ref + "' not found");
							}
							providerData.CopyFrom(providerData2);
						}
					}
					foreach (object obj3 in channelData.ClientProviders)
					{
						ProviderData providerData3 = (ProviderData)obj3;
						if (providerData3.Ref != null)
						{
							ProviderData providerData4 = (ProviderData)RemotingConfiguration.clientProviderTemplates[providerData3.Ref];
							if (providerData4 == null)
							{
								throw new RemotingException("Provider template '" + providerData3.Ref + "' not found");
							}
							providerData3.CopyFrom(providerData4);
						}
					}
					ChannelServices.RegisterChannelConfig(channelData);
				}
			}
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x00090108 File Offset: 0x0008E308
		internal static void RegisterTypes(ArrayList types)
		{
			foreach (object obj in types)
			{
				TypeEntry typeEntry = (TypeEntry)obj;
				if (typeEntry is ActivatedClientTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedClientType((ActivatedClientTypeEntry)typeEntry);
				}
				else if (typeEntry is ActivatedServiceTypeEntry)
				{
					RemotingConfiguration.RegisterActivatedServiceType((ActivatedServiceTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownClientTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownClientType((WellKnownClientTypeEntry)typeEntry);
				}
				else if (typeEntry is WellKnownServiceTypeEntry)
				{
					RemotingConfiguration.RegisterWellKnownServiceType((WellKnownServiceTypeEntry)typeEntry);
				}
			}
		}

		/// <summary>Indicates whether the server channels in this application domain return filtered or complete exception information to local or remote callers.</summary>
		/// <returns>true if only filtered exception information is returned to local or remote callers, as specified by the <paramref name="isLocalRequest" /> parameter; false if complete exception information is returned.</returns>
		/// <param name="isLocalRequest">true to specify local callers; false to specify remote callers. </param>
		// Token: 0x06002309 RID: 8969 RVA: 0x000901A8 File Offset: 0x0008E3A8
		public static bool CustomErrorsEnabled(bool isLocalRequest)
		{
			return RemotingConfiguration._errorMode != CustomErrorsModes.Off && (RemotingConfiguration._errorMode == CustomErrorsModes.On || !isLocalRequest);
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000901C4 File Offset: 0x0008E3C4
		internal static void SetCustomErrorsMode(string mode)
		{
			if (mode == null)
			{
				throw new RemotingException("mode attribute is required");
			}
			string text = mode.ToLower();
			if (text != "on" && text != "off" && text != "remoteonly")
			{
				throw new RemotingException("Invalid custom error mode: " + mode);
			}
			RemotingConfiguration._errorMode = (CustomErrorsModes)Enum.Parse(typeof(CustomErrorsModes), text, true);
		}

		// Token: 0x040010F7 RID: 4343
		private static string applicationID = null;

		// Token: 0x040010F8 RID: 4344
		private static string applicationName = null;

		// Token: 0x040010F9 RID: 4345
		private static string processGuid = null;

		// Token: 0x040010FA RID: 4346
		private static bool defaultConfigRead = false;

		// Token: 0x040010FB RID: 4347
		private static bool defaultDelayedConfigRead = false;

		// Token: 0x040010FC RID: 4348
		private static CustomErrorsModes _errorMode = CustomErrorsModes.RemoteOnly;

		// Token: 0x040010FD RID: 4349
		private static Hashtable wellKnownClientEntries = new Hashtable();

		// Token: 0x040010FE RID: 4350
		private static Hashtable activatedClientEntries = new Hashtable();

		// Token: 0x040010FF RID: 4351
		private static Hashtable wellKnownServiceEntries = new Hashtable();

		// Token: 0x04001100 RID: 4352
		private static Hashtable activatedServiceEntries = new Hashtable();

		// Token: 0x04001101 RID: 4353
		private static Hashtable channelTemplates = new Hashtable();

		// Token: 0x04001102 RID: 4354
		private static Hashtable clientProviderTemplates = new Hashtable();

		// Token: 0x04001103 RID: 4355
		private static Hashtable serverProviderTemplates = new Hashtable();
	}
}
