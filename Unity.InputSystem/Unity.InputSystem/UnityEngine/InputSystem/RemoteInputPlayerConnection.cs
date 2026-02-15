using System;
using UnityEngine.Events;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Networking.PlayerConnection;

namespace UnityEngine.InputSystem
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	internal class RemoteInputPlayerConnection : ScriptableObject, IObserver<InputRemoting.Message>, IObservable<InputRemoting.Message>
	{
		// Token: 0x06000989 RID: 2441 RVA: 0x00033440 File Offset: 0x00031640
		public void Bind(IEditorPlayerConnection connection, bool isConnected)
		{
			if (this.m_Connection == null)
			{
				connection.RegisterConnection(new UnityAction<int>(this.OnConnected));
				connection.RegisterDisconnection(new UnityAction<int>(this.OnDisconnected));
				connection.Register(RemoteInputPlayerConnection.kNewDeviceMsg, new UnityAction<MessageEventArgs>(this.OnNewDevice));
				connection.Register(RemoteInputPlayerConnection.kNewLayoutMsg, new UnityAction<MessageEventArgs>(this.OnNewLayout));
				connection.Register(RemoteInputPlayerConnection.kNewEventsMsg, new UnityAction<MessageEventArgs>(this.OnNewEvents));
				connection.Register(RemoteInputPlayerConnection.kRemoveDeviceMsg, new UnityAction<MessageEventArgs>(this.OnRemoveDevice));
				connection.Register(RemoteInputPlayerConnection.kChangeUsagesMsg, new UnityAction<MessageEventArgs>(this.OnChangeUsages));
				connection.Register(RemoteInputPlayerConnection.kStartSendingMsg, new UnityAction<MessageEventArgs>(this.OnStartSending));
				connection.Register(RemoteInputPlayerConnection.kStopSendingMsg, new UnityAction<MessageEventArgs>(this.OnStopSending));
				this.m_Connection = connection;
				if (isConnected)
				{
					this.OnConnected(0);
				}
				return;
			}
			if (this.m_Connection == connection)
			{
				return;
			}
			throw new InvalidOperationException("Already bound to an IEditorPlayerConnection");
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00033540 File Offset: 0x00031740
		public IDisposable Subscribe(IObserver<InputRemoting.Message> observer)
		{
			if (observer == null)
			{
				throw new ArgumentNullException("observer");
			}
			RemoteInputPlayerConnection.Subscriber subscriber = new RemoteInputPlayerConnection.Subscriber
			{
				owner = this,
				observer = observer
			};
			ArrayHelpers.Append<RemoteInputPlayerConnection.Subscriber>(ref this.m_Subscribers, subscriber);
			if (this.m_ConnectedIds != null)
			{
				foreach (int id in this.m_ConnectedIds)
				{
					observer.OnNext(new InputRemoting.Message
					{
						type = InputRemoting.MessageType.Connect,
						participantId = id
					});
				}
			}
			return subscriber;
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000335BE File Offset: 0x000317BE
		private void OnConnected(int id)
		{
			if (this.m_ConnectedIds != null && ArrayHelpers.Contains<int>(this.m_ConnectedIds, id))
			{
				return;
			}
			ArrayHelpers.Append<int>(ref this.m_ConnectedIds, id);
			this.SendToSubscribers(InputRemoting.MessageType.Connect, new MessageEventArgs
			{
				playerId = id
			});
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000335F7 File Offset: 0x000317F7
		private void OnDisconnected(int id)
		{
			if (this.m_ConnectedIds == null || !ArrayHelpers.Contains<int>(this.m_ConnectedIds, id))
			{
				return;
			}
			ArrayHelpers.Erase<int>(ref this.m_ConnectedIds, id);
			this.SendToSubscribers(InputRemoting.MessageType.Disconnect, new MessageEventArgs
			{
				playerId = id
			});
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00033630 File Offset: 0x00031830
		private void OnNewDevice(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.NewDevice, args);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0003363A File Offset: 0x0003183A
		private void OnNewLayout(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.NewLayout, args);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00033644 File Offset: 0x00031844
		private void OnNewEvents(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.NewEvents, args);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0003364E File Offset: 0x0003184E
		private void OnRemoveDevice(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.RemoveDevice, args);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x00033658 File Offset: 0x00031858
		private void OnChangeUsages(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.ChangeUsages, args);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00033662 File Offset: 0x00031862
		private void OnStartSending(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.StartSending, args);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0003366C File Offset: 0x0003186C
		private void OnStopSending(MessageEventArgs args)
		{
			this.SendToSubscribers(InputRemoting.MessageType.StopSending, args);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00033678 File Offset: 0x00031878
		private void SendToSubscribers(InputRemoting.MessageType type, MessageEventArgs args)
		{
			if (this.m_Subscribers == null)
			{
				return;
			}
			InputRemoting.Message msg = new InputRemoting.Message
			{
				participantId = args.playerId,
				type = type,
				data = args.data
			};
			for (int i = 0; i < this.m_Subscribers.Length; i++)
			{
				this.m_Subscribers[i].observer.OnNext(msg);
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x000336E0 File Offset: 0x000318E0
		void IObserver<InputRemoting.Message>.OnNext(InputRemoting.Message msg)
		{
			if (this.m_Connection == null)
			{
				return;
			}
			switch (msg.type)
			{
			case InputRemoting.MessageType.NewLayout:
				this.m_Connection.Send(RemoteInputPlayerConnection.kNewLayoutMsg, msg.data);
				return;
			case InputRemoting.MessageType.NewDevice:
				this.m_Connection.Send(RemoteInputPlayerConnection.kNewDeviceMsg, msg.data);
				return;
			case InputRemoting.MessageType.NewEvents:
				this.m_Connection.Send(RemoteInputPlayerConnection.kNewEventsMsg, msg.data);
				return;
			case InputRemoting.MessageType.RemoveDevice:
				this.m_Connection.Send(RemoteInputPlayerConnection.kRemoveDeviceMsg, msg.data);
				break;
			case InputRemoting.MessageType.RemoveLayout:
				break;
			case InputRemoting.MessageType.ChangeUsages:
				this.m_Connection.Send(RemoteInputPlayerConnection.kChangeUsagesMsg, msg.data);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x000049FE File Offset: 0x00002BFE
		void IObserver<InputRemoting.Message>.OnError(Exception error)
		{
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000049FE File Offset: 0x00002BFE
		void IObserver<InputRemoting.Message>.OnCompleted()
		{
		}

		// Token: 0x04000412 RID: 1042
		public static readonly Guid kNewDeviceMsg = new Guid("fcd9651ded40425995dfa6aeb78f1f1c");

		// Token: 0x04000413 RID: 1043
		public static readonly Guid kNewLayoutMsg = new Guid("fccfec2b7369466d88502a9dd38505f4");

		// Token: 0x04000414 RID: 1044
		public static readonly Guid kNewEventsMsg = new Guid("53546641df1347bc8aa315278a603586");

		// Token: 0x04000415 RID: 1045
		public static readonly Guid kRemoveDeviceMsg = new Guid("e5e299b2d9e44255b8990bb71af8922d");

		// Token: 0x04000416 RID: 1046
		public static readonly Guid kChangeUsagesMsg = new Guid("b9fe706dfc854d7ca109a5e38d7db730");

		// Token: 0x04000417 RID: 1047
		public static readonly Guid kStartSendingMsg = new Guid("0d58e99045904672b3ef34b8797d23cb");

		// Token: 0x04000418 RID: 1048
		public static readonly Guid kStopSendingMsg = new Guid("548716b2534a45369ab0c9323fc8b4a8");

		// Token: 0x04000419 RID: 1049
		[SerializeField]
		private IEditorPlayerConnection m_Connection;

		// Token: 0x0400041A RID: 1050
		[NonSerialized]
		private RemoteInputPlayerConnection.Subscriber[] m_Subscribers;

		// Token: 0x0400041B RID: 1051
		[SerializeField]
		private int[] m_ConnectedIds;

		// Token: 0x020000AC RID: 172
		private class Subscriber : IDisposable
		{
			// Token: 0x0600099A RID: 2458 RVA: 0x00033806 File Offset: 0x00031A06
			public void Dispose()
			{
				ArrayHelpers.Erase<RemoteInputPlayerConnection.Subscriber>(ref this.owner.m_Subscribers, this);
			}

			// Token: 0x0400041C RID: 1052
			public RemoteInputPlayerConnection owner;

			// Token: 0x0400041D RID: 1053
			public IObserver<InputRemoting.Message> observer;
		}
	}
}
