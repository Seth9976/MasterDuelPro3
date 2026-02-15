using System;
using System.Threading;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000051 RID: 81
	internal class MessageAgent
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000D248 File Offset: 0x0000B448
		private void InitBlock()
		{
			this.messages = new MessageVector(5, 5);
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000D257 File Offset: 0x0000B457
		internal virtual object[] MessageArray
		{
			get
			{
				return this.messages.ObjectArray;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000D264 File Offset: 0x0000B464
		internal virtual int[] MessageIDs
		{
			get
			{
				int count = this.messages.Count;
				int[] array = new int[count];
				for (int i = 0; i < count; i++)
				{
					Message message = (Message)this.messages[i];
					array[i] = message.MessageID;
				}
				return array;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		internal virtual string AgentName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		internal virtual int Count
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.messages.Count; i++)
				{
					Message message = (Message)this.messages[i];
					num += message.Count;
				}
				return num;
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000D2F5 File Offset: 0x0000B4F5
		internal MessageAgent()
		{
			this.InitBlock();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000D304 File Offset: 0x0000B504
		internal void merge(MessageAgent fromAgent)
		{
			object[] messageArray = fromAgent.MessageArray;
			for (int i = 0; i < messageArray.Length; i++)
			{
				this.messages.Add(messageArray[i]);
				((Message)messageArray[i]).Agent = this;
			}
			object syncRoot = this.messages.SyncRoot;
			lock (syncRoot)
			{
				if (messageArray.Length > 1)
				{
					Monitor.PulseAll(this.messages.SyncRoot);
				}
				else if (messageArray.Length == 1)
				{
					Monitor.Pulse(this.messages.SyncRoot);
				}
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		internal void sleepersAwake(bool all)
		{
			object syncRoot = this.messages.SyncRoot;
			lock (syncRoot)
			{
				if (all)
				{
					Monitor.PulseAll(this.messages.SyncRoot);
				}
				else
				{
					Monitor.Pulse(this.messages.SyncRoot);
				}
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000D408 File Offset: 0x0000B608
		internal bool isResponseReceived()
		{
			int count = this.messages.Count;
			int num = this.indexLastRead + 1;
			for (int i = 0; i < count; i++)
			{
				if (num == count)
				{
					num = 0;
				}
				if (((Message)this.messages[num]).hasReplies())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000D458 File Offset: 0x0000B658
		internal bool isResponseReceived(int msgId)
		{
			bool flag;
			try
			{
				flag = this.messages.findMessageById(msgId).hasReplies();
			}
			catch (FieldAccessException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000D490 File Offset: 0x0000B690
		internal void Abandon(int msgId, LdapConstraints cons)
		{
			try
			{
				Message message = this.messages.findMessageById(msgId);
				SupportClass.VectorRemoveElement(this.messages, message);
				message.Abandon(cons, null);
				return;
			}
			catch (FieldAccessException)
			{
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000D4D8 File Offset: 0x0000B6D8
		internal void AbandonAll()
		{
			int count = this.messages.Count;
			for (int i = 0; i < count; i++)
			{
				Message message = (Message)this.messages[i];
				SupportClass.VectorRemoveElement(this.messages, message);
				message.Abandon(null, null);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000D524 File Offset: 0x0000B724
		internal bool isComplete(int msgid)
		{
			try
			{
				if (!this.messages.findMessageById(msgid).Complete)
				{
					return false;
				}
			}
			catch (FieldAccessException)
			{
			}
			return true;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000D560 File Offset: 0x0000B760
		internal Message getMessage(int msgid)
		{
			return this.messages.findMessageById(msgid);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000D570 File Offset: 0x0000B770
		internal void sendMessage(Connection conn, LdapMessage msg, int timeOut, LdapMessageQueue queue, BindProperties bindProps)
		{
			Message message = new Message(msg, timeOut, conn, this, queue, bindProps);
			this.messages.Add(message);
			message.sendMessage();
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000D59E File Offset: 0x0000B79E
		internal object getLdapMessage(int msgId)
		{
			return this.getLdapMessage(new Integer32(msgId));
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		internal object getLdapMessage(Integer32 msgId)
		{
			if (this.messages.Count == 0)
			{
				return null;
			}
			if (msgId != null)
			{
				try
				{
					Message message = this.messages.findMessageById(msgId.intValue);
					object obj = message.waitForReply();
					if (!message.acceptsReplies() && !message.hasReplies())
					{
						SupportClass.VectorRemoveElement(this.messages, message);
						message.Abandon(null, null);
					}
					return obj;
				}
				catch (FieldAccessException)
				{
					return null;
				}
			}
			object syncRoot = this.messages.SyncRoot;
			object obj2;
			lock (syncRoot)
			{
				object obj;
				for (;;)
				{
					int num = this.indexLastRead + 1;
					for (int i = 0; i < this.messages.Count; i++)
					{
						if (num >= this.messages.Count)
						{
							num = 0;
						}
						Message message2 = (Message)this.messages[num];
						this.indexLastRead = num++;
						obj = message2.Reply;
						if (!message2.acceptsReplies() && !message2.hasReplies())
						{
							SupportClass.VectorRemoveElement(this.messages, message2);
							message2.Abandon(null, null);
							i--;
						}
						if (obj != null)
						{
							goto Block_12;
						}
					}
					if (this.messages.Count == 0)
					{
						goto Block_14;
					}
					try
					{
						Monitor.Wait(this.messages.SyncRoot);
					}
					catch (ThreadInterruptedException)
					{
					}
				}
				Block_12:
				return obj;
				Block_14:
				obj2 = null;
			}
			return obj2;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00002A00 File Offset: 0x00000C00
		private void debugDisplayMessages()
		{
		}

		// Token: 0x040001AF RID: 431
		private MessageVector messages;

		// Token: 0x040001B0 RID: 432
		private int indexLastRead;

		// Token: 0x040001B1 RID: 433
		private static object nameLock = new object();

		// Token: 0x040001B2 RID: 434
		private static int agentNum;

		// Token: 0x040001B3 RID: 435
		private string name;
	}
}
