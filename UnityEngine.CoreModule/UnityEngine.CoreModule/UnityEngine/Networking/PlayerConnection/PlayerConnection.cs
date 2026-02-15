using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using UnityEngine.Scripting;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x020002F0 RID: 752
	[Serializable]
	public class PlayerConnection : ScriptableObject, IEditorPlayerConnection
	{
		// Token: 0x1700033B RID: 827
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0002C57C File Offset: 0x0002A77C
		public static PlayerConnection instance
		{
			get
			{
				bool flag = PlayerConnection.s_Instance == null;
				PlayerConnection playerConnection;
				if (flag)
				{
					playerConnection = PlayerConnection.CreateInstance();
				}
				else
				{
					playerConnection = PlayerConnection.s_Instance;
				}
				return playerConnection;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0002C5AC File Offset: 0x0002A7AC
		public bool isConnected
		{
			get
			{
				return this.GetConnectionNativeApi().IsConnected();
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0002C5CC File Offset: 0x0002A7CC
		private static PlayerConnection CreateInstance()
		{
			PlayerConnection.s_Instance = ScriptableObject.CreateInstance<PlayerConnection>();
			PlayerConnection.s_Instance.hideFlags = HideFlags.HideAndDontSave;
			return PlayerConnection.s_Instance;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0002C5FC File Offset: 0x0002A7FC
		public void OnEnable()
		{
			bool isInitilized = this.m_IsInitilized;
			if (!isInitilized)
			{
				this.m_IsInitilized = true;
				this.GetConnectionNativeApi().Initialize();
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0002C62C File Offset: 0x0002A82C
		private IPlayerEditorConnectionNative GetConnectionNativeApi()
		{
			return PlayerConnection.connectionNative ?? new PlayerConnectionInternal();
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0002C64C File Offset: 0x0002A84C
		public void Register(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			bool flag2 = !this.m_PlayerEditorConnectionEvents.messageTypeSubscribers.Any((PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag2)
			{
				this.GetConnectionNativeApi().RegisterInternal(messageId);
			}
			this.m_PlayerEditorConnectionEvents.AddAndCreate(messageId).AddListener(callback);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0002C6DC File Offset: 0x0002A8DC
		public void Unregister(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			this.m_PlayerEditorConnectionEvents.UnregisterManagedCallback(messageId, callback);
			bool flag = !this.m_PlayerEditorConnectionEvents.messageTypeSubscribers.Any((PlayerEditorConnectionEvents.MessageTypeSubscribers x) => x.MessageTypeId == messageId);
			if (flag)
			{
				this.GetConnectionNativeApi().UnregisterInternal(messageId);
			}
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0002C744 File Offset: 0x0002A944
		public void RegisterConnection(UnityAction<int> callback)
		{
			foreach (int playerId in this.m_connectedPlayers)
			{
				callback(playerId);
			}
			this.m_PlayerEditorConnectionEvents.connectionEvent.AddListener(callback);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0002C7B0 File Offset: 0x0002A9B0
		public void RegisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.AddListener(callback);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0002C7C5 File Offset: 0x0002A9C5
		public void UnregisterConnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.connectionEvent.RemoveListener(callback);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0002C7DA File Offset: 0x0002A9DA
		public void UnregisterDisconnection(UnityAction<int> callback)
		{
			this.m_PlayerEditorConnectionEvents.disconnectionEvent.RemoveListener(callback);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0002C7F0 File Offset: 0x0002A9F0
		public void Send(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			this.GetConnectionNativeApi().SendMessage(messageId, data, 0);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0002C830 File Offset: 0x0002AA30
		public bool TrySend(Guid messageId, byte[] data)
		{
			bool flag = messageId == Guid.Empty;
			if (flag)
			{
				throw new ArgumentException("Cant be Guid.Empty", "messageId");
			}
			return this.GetConnectionNativeApi().TrySendMessage(messageId, data, 0);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0002C870 File Offset: 0x0002AA70
		public bool BlockUntilRecvMsg(Guid messageId, int timeout)
		{
			bool msgReceived = false;
			UnityAction<MessageEventArgs> callback = delegate(MessageEventArgs args)
			{
				msgReceived = true;
			};
			DateTime startTime = DateTime.Now;
			this.Register(messageId, callback);
			while ((DateTime.Now - startTime).TotalMilliseconds < (double)timeout && !msgReceived)
			{
				this.GetConnectionNativeApi().Poll();
			}
			this.Unregister(messageId, callback);
			return msgReceived;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0002C8F2 File Offset: 0x0002AAF2
		public void DisconnectAll()
		{
			this.GetConnectionNativeApi().DisconnectAll();
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0002C904 File Offset: 0x0002AB04
		[RequiredByNativeCode]
		private static void MessageCallbackInternal(IntPtr data, ulong size, ulong guid, string messageId)
		{
			byte[] bytes = null;
			bool flag = size > 0UL;
			if (flag)
			{
				bytes = new byte[size];
				Marshal.Copy(data, bytes, 0, (int)size);
			}
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.InvokeMessageIdSubscribers(new Guid(messageId), bytes, (int)guid);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0002C94B File Offset: 0x0002AB4B
		[RequiredByNativeCode]
		private static void ConnectedCallbackInternal(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Add(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.connectionEvent.Invoke(playerId);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0002C975 File Offset: 0x0002AB75
		[RequiredByNativeCode]
		private static void DisconnectedCallback(int playerId)
		{
			PlayerConnection.instance.m_connectedPlayers.Remove(playerId);
			PlayerConnection.instance.m_PlayerEditorConnectionEvents.disconnectionEvent.Invoke(playerId);
		}

		// Token: 0x040007E1 RID: 2017
		internal static IPlayerEditorConnectionNative connectionNative;

		// Token: 0x040007E2 RID: 2018
		[SerializeField]
		private PlayerEditorConnectionEvents m_PlayerEditorConnectionEvents = new PlayerEditorConnectionEvents();

		// Token: 0x040007E3 RID: 2019
		[SerializeField]
		private List<int> m_connectedPlayers = new List<int>();

		// Token: 0x040007E4 RID: 2020
		private bool m_IsInitilized;

		// Token: 0x040007E5 RID: 2021
		private static PlayerConnection s_Instance;
	}
}
