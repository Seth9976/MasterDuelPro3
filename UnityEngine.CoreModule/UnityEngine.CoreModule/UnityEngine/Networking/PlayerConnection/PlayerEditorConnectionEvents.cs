using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x020002F4 RID: 756
	[Serializable]
	internal class PlayerEditorConnectionEvents
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x0002C9ED File Offset: 0x0002ABED
		public IReadOnlyList<PlayerEditorConnectionEvents.MessageTypeSubscribers> messageTypeSubscribers
		{
			get
			{
				return this.m_MessageTypeSubscribers;
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0002C9F8 File Offset: 0x0002ABF8
		private void BuildLookup()
		{
			bool flag = this.m_SubscriberLookup == null;
			if (flag)
			{
				this.m_SubscriberLookup = new Dictionary<Guid, PlayerEditorConnectionEvents.MessageTypeSubscribers>();
				foreach (PlayerEditorConnectionEvents.MessageTypeSubscribers subscriber in this.messageTypeSubscribers)
				{
					this.m_SubscriberLookup.Add(subscriber.MessageTypeId, subscriber);
				}
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0002CA70 File Offset: 0x0002AC70
		public void InvokeMessageIdSubscribers(Guid messageId, byte[] data, int playerId)
		{
			this.BuildLookup();
			PlayerEditorConnectionEvents.MessageTypeSubscribers eventSubscriber;
			bool flag = !this.m_SubscriberLookup.TryGetValue(messageId, out eventSubscriber);
			if (flag)
			{
				string text = "No actions found for messageId: ";
				Guid guid = messageId;
				Debug.LogError(text + guid.ToString());
			}
			else
			{
				MessageEventArgs messageEventArg = new MessageEventArgs
				{
					playerId = playerId,
					data = data
				};
				eventSubscriber.messageCallback.Invoke(messageEventArg);
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
		public UnityEvent<MessageEventArgs> AddAndCreate(Guid messageId)
		{
			this.BuildLookup();
			PlayerEditorConnectionEvents.MessageTypeSubscribers eventSubscriber;
			bool flag = !this.m_SubscriberLookup.TryGetValue(messageId, out eventSubscriber);
			if (flag)
			{
				eventSubscriber = new PlayerEditorConnectionEvents.MessageTypeSubscribers
				{
					MessageTypeId = messageId,
					messageCallback = new PlayerEditorConnectionEvents.MessageEvent()
				};
				this.m_MessageTypeSubscribers.Add(eventSubscriber);
				this.m_SubscriberLookup.Add(messageId, eventSubscriber);
			}
			eventSubscriber.subscriberCount++;
			return eventSubscriber.messageCallback;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0002CB58 File Offset: 0x0002AD58
		public void UnregisterManagedCallback(Guid messageId, UnityAction<MessageEventArgs> callback)
		{
			this.BuildLookup();
			PlayerEditorConnectionEvents.MessageTypeSubscribers eventSubscriber;
			bool flag = !this.m_SubscriberLookup.TryGetValue(messageId, out eventSubscriber);
			if (!flag)
			{
				eventSubscriber.subscriberCount--;
				eventSubscriber.messageCallback.RemoveListener(callback);
				bool flag2 = eventSubscriber.subscriberCount <= 0;
				if (flag2)
				{
					this.m_MessageTypeSubscribers.Remove(eventSubscriber);
					this.m_SubscriberLookup.Remove(messageId);
				}
			}
		}

		// Token: 0x040007E9 RID: 2025
		[SerializeField]
		private List<PlayerEditorConnectionEvents.MessageTypeSubscribers> m_MessageTypeSubscribers = new List<PlayerEditorConnectionEvents.MessageTypeSubscribers>();

		// Token: 0x040007EA RID: 2026
		private Dictionary<Guid, PlayerEditorConnectionEvents.MessageTypeSubscribers> m_SubscriberLookup;

		// Token: 0x040007EB RID: 2027
		[SerializeField]
		public PlayerEditorConnectionEvents.ConnectionChangeEvent connectionEvent = new PlayerEditorConnectionEvents.ConnectionChangeEvent();

		// Token: 0x040007EC RID: 2028
		[SerializeField]
		public PlayerEditorConnectionEvents.ConnectionChangeEvent disconnectionEvent = new PlayerEditorConnectionEvents.ConnectionChangeEvent();

		// Token: 0x020002F5 RID: 757
		[Serializable]
		public class MessageEvent : UnityEvent<MessageEventArgs>
		{
		}

		// Token: 0x020002F6 RID: 758
		[Serializable]
		public class ConnectionChangeEvent : UnityEvent<int>
		{
		}

		// Token: 0x020002F7 RID: 759
		[Serializable]
		public class MessageTypeSubscribers
		{
			// Token: 0x1700033E RID: 830
			// (get) Token: 0x06001520 RID: 5408 RVA: 0x0002CC08 File Offset: 0x0002AE08
			// (set) Token: 0x06001521 RID: 5409 RVA: 0x0002CC25 File Offset: 0x0002AE25
			public Guid MessageTypeId
			{
				get
				{
					return new Guid(this.m_messageTypeId);
				}
				set
				{
					this.m_messageTypeId = value.ToString();
				}
			}

			// Token: 0x040007ED RID: 2029
			[SerializeField]
			private string m_messageTypeId;

			// Token: 0x040007EE RID: 2030
			public int subscriberCount = 0;

			// Token: 0x040007EF RID: 2031
			public PlayerEditorConnectionEvents.MessageEvent messageCallback = new PlayerEditorConnectionEvents.MessageEvent();
		}
	}
}
