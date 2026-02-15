using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides static methods to aid with remoting channel registration, resolution, and URL discovery. This class cannot be inherited.</summary>
	// Token: 0x02000452 RID: 1106
	[ComVisible(true)]
	public sealed class ChannelServices
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x00095478 File Offset: 0x00093678
		internal static CrossContextChannel CrossContextChannel
		{
			get
			{
				return ChannelServices._crossContextSink;
			}
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x00095480 File Offset: 0x00093680
		internal static IMessageSink CreateClientChannelSinkChain(string url, object remoteChannelData, out string objectUri)
		{
			object[] array = (object[])remoteChannelData;
			object syncRoot = ChannelServices.registeredChannels.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in ChannelServices.registeredChannels)
				{
					IChannelSender channelSender = ((IChannel)obj) as IChannelSender;
					if (channelSender != null)
					{
						IMessageSink messageSink = ChannelServices.CreateClientChannelSinkChain(channelSender, url, array, out objectUri);
						if (messageSink != null)
						{
							return messageSink;
						}
					}
				}
				RemotingConfiguration.LoadDefaultDelayedChannels();
				foreach (object obj2 in ChannelServices.delayedClientChannels)
				{
					IChannelSender channelSender2 = (IChannelSender)obj2;
					IMessageSink messageSink2 = ChannelServices.CreateClientChannelSinkChain(channelSender2, url, array, out objectUri);
					if (messageSink2 != null)
					{
						ChannelServices.delayedClientChannels.Remove(channelSender2);
						ChannelServices.RegisterChannel(channelSender2);
						return messageSink2;
					}
				}
			}
			objectUri = null;
			return null;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000955A4 File Offset: 0x000937A4
		internal static IMessageSink CreateClientChannelSinkChain(IChannelSender sender, string url, object[] channelDataArray, out string objectUri)
		{
			objectUri = null;
			if (channelDataArray == null)
			{
				return sender.CreateMessageSink(url, null, out objectUri);
			}
			foreach (object obj in channelDataArray)
			{
				IMessageSink messageSink;
				if (obj is IChannelDataStore)
				{
					messageSink = sender.CreateMessageSink(null, obj, out objectUri);
				}
				else
				{
					messageSink = sender.CreateMessageSink(url, obj, out objectUri);
				}
				if (messageSink != null)
				{
					return messageSink;
				}
			}
			return null;
		}

		/// <summary>Registers a channel with the channel services. <see cref="M:System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(System.Runtime.Remoting.Channels.IChannel)" /> is obsolete. Please use <see cref="M:System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(System.Runtime.Remoting.Channels.IChannel,System.Boolean)" /> instead.</summary>
		/// <param name="chnl">The channel to register. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="chnl" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The channel has already been registered. </exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the callstack does not have permission to configure remoting types and channels. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration" />
		/// </PermissionSet>
		// Token: 0x06002466 RID: 9318 RVA: 0x000955F9 File Offset: 0x000937F9
		[Obsolete("Use RegisterChannel(IChannel,Boolean)")]
		public static void RegisterChannel(IChannel chnl)
		{
			ChannelServices.RegisterChannel(chnl, false);
		}

		/// <summary>Registers a channel with the channel services.</summary>
		/// <param name="chnl">The channel to register.</param>
		/// <param name="ensureSecurity">true ensures that security is enabled; otherwise false. Setting the value to false does not effect the security setting on the TCP or IPC channel. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="chnl" /> parameter is null. </exception>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">The channel has already been registered. </exception>
		/// <exception cref="T:System.Security.SecurityException">At least one of the callers higher in the call stack does not have permission to configure remoting types and channels. </exception>
		/// <exception cref="T:System.NotSupportedException">Not supported in Windows 98 for <see cref="T:System.Runtime.Remoting.Channels.Tcp.TcpServerChannel" /> and on all platforms for <see cref="T:System.Runtime.Remoting.Channels.Http.HttpServerChannel" />. Host the service using Internet Information Services (IIS) if you require a secure HTTP channel.</exception>
		// Token: 0x06002467 RID: 9319 RVA: 0x00095604 File Offset: 0x00093804
		public static void RegisterChannel(IChannel chnl, bool ensureSecurity)
		{
			if (chnl == null)
			{
				throw new ArgumentNullException("chnl");
			}
			if (ensureSecurity)
			{
				ISecurableChannel securableChannel = chnl as ISecurableChannel;
				if (securableChannel == null)
				{
					throw new RemotingException(string.Format("Channel {0} is not securable while ensureSecurity is specified as true", chnl.ChannelName));
				}
				securableChannel.IsSecured = true;
			}
			object syncRoot = ChannelServices.registeredChannels.SyncRoot;
			lock (syncRoot)
			{
				int num = -1;
				for (int i = 0; i < ChannelServices.registeredChannels.Count; i++)
				{
					IChannel channel = (IChannel)ChannelServices.registeredChannels[i];
					if (channel.ChannelName == chnl.ChannelName && chnl.ChannelName != "")
					{
						throw new RemotingException("Channel " + channel.ChannelName + " already registered");
					}
					if (channel.ChannelPriority < chnl.ChannelPriority && num == -1)
					{
						num = i;
					}
				}
				if (num != -1)
				{
					ChannelServices.registeredChannels.Insert(num, chnl);
				}
				else
				{
					ChannelServices.registeredChannels.Add(chnl);
				}
				IChannelReceiver channelReceiver = chnl as IChannelReceiver;
				if (channelReceiver != null && ChannelServices.oldStartModeTypes.Contains(chnl.GetType().ToString()))
				{
					channelReceiver.StartListening(null);
				}
			}
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00095744 File Offset: 0x00093944
		internal static void RegisterChannelConfig(ChannelData channel)
		{
			IServerChannelSinkProvider serverChannelSinkProvider = null;
			IClientChannelSinkProvider clientChannelSinkProvider = null;
			for (int i = channel.ServerProviders.Count - 1; i >= 0; i--)
			{
				IServerChannelSinkProvider serverChannelSinkProvider2 = (IServerChannelSinkProvider)ChannelServices.CreateProvider(channel.ServerProviders[i] as ProviderData);
				serverChannelSinkProvider2.Next = serverChannelSinkProvider;
				serverChannelSinkProvider = serverChannelSinkProvider2;
			}
			for (int j = channel.ClientProviders.Count - 1; j >= 0; j--)
			{
				IClientChannelSinkProvider clientChannelSinkProvider2 = (IClientChannelSinkProvider)ChannelServices.CreateProvider(channel.ClientProviders[j] as ProviderData);
				clientChannelSinkProvider2.Next = clientChannelSinkProvider;
				clientChannelSinkProvider = clientChannelSinkProvider2;
			}
			Type type = Type.GetType(channel.Type);
			if (type == null)
			{
				throw new RemotingException("Type '" + channel.Type + "' not found");
			}
			bool flag = typeof(IChannelSender).IsAssignableFrom(type);
			bool flag2 = typeof(IChannelReceiver).IsAssignableFrom(type);
			Type[] array;
			object[] array2;
			if (flag && flag2)
			{
				array = new Type[]
				{
					typeof(IDictionary),
					typeof(IClientChannelSinkProvider),
					typeof(IServerChannelSinkProvider)
				};
				array2 = new object[] { channel.CustomProperties, clientChannelSinkProvider, serverChannelSinkProvider };
			}
			else if (flag)
			{
				array = new Type[]
				{
					typeof(IDictionary),
					typeof(IClientChannelSinkProvider)
				};
				array2 = new object[] { channel.CustomProperties, clientChannelSinkProvider };
			}
			else
			{
				if (!flag2)
				{
					Type type2 = type;
					throw new RemotingException(((type2 != null) ? type2.ToString() : null) + " is not a valid channel type");
				}
				array = new Type[]
				{
					typeof(IDictionary),
					typeof(IServerChannelSinkProvider)
				};
				array2 = new object[] { channel.CustomProperties, serverChannelSinkProvider };
			}
			ConstructorInfo constructor = type.GetConstructor(array);
			if (constructor == null)
			{
				Type type3 = type;
				throw new RemotingException(((type3 != null) ? type3.ToString() : null) + " does not have a valid constructor");
			}
			IChannel channel2;
			try
			{
				channel2 = (IChannel)constructor.Invoke(array2);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			object syncRoot = ChannelServices.registeredChannels.SyncRoot;
			lock (syncRoot)
			{
				if (channel.DelayLoadAsClientChannel == "true" && !(channel2 is IChannelReceiver))
				{
					ChannelServices.delayedClientChannels.Add(channel2);
				}
				else
				{
					ChannelServices.RegisterChannel(channel2);
				}
			}
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000959D0 File Offset: 0x00093BD0
		private static object CreateProvider(ProviderData prov)
		{
			Type type = Type.GetType(prov.Type);
			if (type == null)
			{
				throw new RemotingException("Type '" + prov.Type + "' not found");
			}
			object[] array = new object[] { prov.CustomProperties, prov.CustomData };
			object obj;
			try
			{
				obj = Activator.CreateInstance(type, array);
			}
			catch (Exception innerException)
			{
				if (innerException is TargetInvocationException)
				{
					innerException = ((TargetInvocationException)innerException).InnerException;
				}
				string text = "An instance of provider '";
				Type type2 = type;
				throw new RemotingException(text + ((type2 != null) ? type2.ToString() : null) + "' could not be created: " + innerException.Message);
			}
			return obj;
		}

		/// <summary>Synchronously dispatches the incoming message to the server-side chain(s) based on the URI embedded in the message.</summary>
		/// <returns>A reply message is returned by the call to the server-side chain.</returns>
		/// <param name="msg">The message to dispatch. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="msg" /> parameter is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600246A RID: 9322 RVA: 0x00095A80 File Offset: 0x00093C80
		public static IMessage SyncDispatchMessage(IMessage msg)
		{
			IMessage message = ChannelServices.CheckIncomingMessage(msg);
			if (message != null)
			{
				return ChannelServices.CheckReturnMessage(msg, message);
			}
			message = ChannelServices._crossContextSink.SyncProcessMessage(msg);
			return ChannelServices.CheckReturnMessage(msg, message);
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x00095AB4 File Offset: 0x00093CB4
		private static ReturnMessage CheckIncomingMessage(IMessage msg)
		{
			IMethodMessage methodMessage = (IMethodMessage)msg;
			ServerIdentity serverIdentity = RemotingServices.GetIdentityForUri(methodMessage.Uri) as ServerIdentity;
			if (serverIdentity == null)
			{
				return new ReturnMessage(new RemotingException("No receiver for uri " + methodMessage.Uri), (IMethodCallMessage)msg);
			}
			RemotingServices.SetMessageTargetIdentity(msg, serverIdentity);
			return null;
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00095B08 File Offset: 0x00093D08
		internal static IMessage CheckReturnMessage(IMessage callMsg, IMessage retMsg)
		{
			IMethodReturnMessage methodReturnMessage = retMsg as IMethodReturnMessage;
			if (methodReturnMessage != null && methodReturnMessage.Exception != null && RemotingConfiguration.CustomErrorsEnabled(ChannelServices.IsLocalCall(callMsg)))
			{
				retMsg = new MethodResponse(new Exception("Server encountered an internal error. For more information, turn off customErrors in the server's .config file."), (IMethodCallMessage)callMsg);
			}
			return retMsg;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0000C091 File Offset: 0x0000A291
		private static bool IsLocalCall(IMessage callMsg)
		{
			return true;
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x00095B4C File Offset: 0x00093D4C
		internal static object[] GetCurrentChannelInfo()
		{
			List<object> list = new List<object>();
			object syncRoot = ChannelServices.registeredChannels.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in ChannelServices.registeredChannels)
				{
					IChannelReceiver channelReceiver = obj as IChannelReceiver;
					if (channelReceiver != null)
					{
						object channelData = channelReceiver.ChannelData;
						if (channelData != null)
						{
							list.Add(channelData);
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x04001189 RID: 4489
		private static ArrayList registeredChannels = new ArrayList();

		// Token: 0x0400118A RID: 4490
		private static ArrayList delayedClientChannels = new ArrayList();

		// Token: 0x0400118B RID: 4491
		private static CrossContextChannel _crossContextSink = new CrossContextChannel();

		// Token: 0x0400118C RID: 4492
		internal static string CrossContextUrl = "__CrossContext";

		// Token: 0x0400118D RID: 4493
		private static IList oldStartModeTypes = new string[] { "Novell.Zenworks.Zmd.Public.UnixServerChannel", "Novell.Zenworks.Zmd.Public.UnixChannel" };
	}
}
