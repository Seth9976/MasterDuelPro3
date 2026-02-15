using System;
using System.Threading;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200004F RID: 79
	internal class Message
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x0000CB80 File Offset: 0x0000AD80
		private void InitBlock()
		{
			this.replies = new MessageVector(5, 5);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000CB90 File Offset: 0x0000AD90
		internal virtual int Count
		{
			get
			{
				int count = this.replies.Count;
				if (!this.complete)
				{
					return count;
				}
				if (count <= 0)
				{
					return count;
				}
				return count - 1;
			}
		}

		// Token: 0x170000BB RID: 187
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		internal virtual MessageAgent Agent
		{
			set
			{
				this.agent = value;
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000CBC5 File Offset: 0x0000ADC5
		internal virtual bool hasReplies()
		{
			return this.replies != null && this.replies.Count > 0;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000CBDF File Offset: 0x0000ADDF
		internal virtual int MessageType
		{
			get
			{
				if (this.msg == null)
				{
					return -1;
				}
				return this.msg.Type;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000CBF6 File Offset: 0x0000ADF6
		internal virtual int MessageID
		{
			get
			{
				return this.msgId;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000CBFE File Offset: 0x0000ADFE
		internal virtual bool Complete
		{
			get
			{
				return this.complete;
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000CC08 File Offset: 0x0000AE08
		internal virtual object waitForReply()
		{
			if (this.replies == null)
			{
				return null;
			}
			object syncRoot = this.replies.SyncRoot;
			object obj3;
			lock (syncRoot)
			{
				while (this.waitForReply_Renamed_Field)
				{
					if (this.replies.Count != 0)
					{
						object obj = this.replies[0];
						this.replies.RemoveAt(0);
						object obj2 = obj;
						if ((this.complete || !this.acceptReplies) && this.replies.Count == 0)
						{
							this.conn.removeMessage(this);
						}
						return obj2;
					}
					try
					{
						Monitor.Wait(this.replies.SyncRoot);
					}
					catch (ThreadInterruptedException)
					{
					}
					if (!this.waitForReply_Renamed_Field)
					{
						break;
					}
				}
				obj3 = null;
			}
			return obj3;
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		internal virtual object Reply
		{
			get
			{
				if (this.replies == null)
				{
					return null;
				}
				object syncRoot = this.replies.SyncRoot;
				object obj2;
				lock (syncRoot)
				{
					if (this.replies.Count == 0)
					{
						return null;
					}
					object obj = this.replies[0];
					this.replies.RemoveAt(0);
					obj2 = obj;
				}
				if (this.conn != null && (this.complete || !this.acceptReplies) && this.replies.Count == 0)
				{
					this.conn.removeMessage(this);
				}
				return obj2;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000CD88 File Offset: 0x0000AF88
		internal virtual bool acceptsReplies()
		{
			return this.acceptReplies;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000CD90 File Offset: 0x0000AF90
		internal virtual LdapMessage Request
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000CD98 File Offset: 0x0000AF98
		internal virtual bool BindRequest
		{
			get
			{
				return this.bindprops != null;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000CDA3 File Offset: 0x0000AFA3
		internal virtual MessageAgent MessageAgent
		{
			get
			{
				return this.agent;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		internal Message(LdapMessage msg, int mslimit, Connection conn, MessageAgent agent, LdapMessageQueue queue, BindProperties bindprops)
		{
			this.InitBlock();
			this.msg = msg;
			this.conn = conn;
			this.agent = agent;
			this.queue = queue;
			this.mslimit = mslimit;
			this.msgId = msg.MessageID;
			this.bindprops = bindprops;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000CE0C File Offset: 0x0000B00C
		internal void sendMessage()
		{
			this.conn.writeMessage(this);
			if (this.mslimit != 0)
			{
				int type = this.msg.Type;
				if (type == 2 || type == 16)
				{
					this.mslimit = 0;
					return;
				}
				this.timer = new Message.Timeout(this, this.mslimit, this);
				this.timer.IsBackground = true;
				this.timer.Start();
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000CE74 File Offset: 0x0000B074
		internal virtual void Abandon(LdapConstraints cons, InterThreadException informUserEx)
		{
			if (!this.waitForReply_Renamed_Field)
			{
				return;
			}
			this.acceptReplies = false;
			this.waitForReply_Renamed_Field = false;
			if (!this.complete)
			{
				try
				{
					if (this.bindprops != null)
					{
						int bindSemId;
						if (this.conn.BindSemIdClear)
						{
							bindSemId = this.msgId;
						}
						else
						{
							bindSemId = this.conn.BindSemId;
							this.conn.clearBindSemId();
						}
						this.conn.freeWriteSemaphore(bindSemId);
					}
					LdapControl[] array = null;
					if (cons != null)
					{
						array = cons.getControls();
					}
					LdapMessage ldapMessage = new LdapAbandonRequest(this.msgId, array);
					this.conn.writeMessage(ldapMessage);
				}
				catch (LdapException)
				{
				}
				if (informUserEx == null)
				{
					this.agent.Abandon(this.msgId, null);
				}
				this.conn.removeMessage(this);
			}
			if (informUserEx != null)
			{
				this.replies.Add(new LdapResponse(informUserEx, this.conn.ActiveReferral));
				this.stopTimer();
				this.sleepersAwake();
				return;
			}
			this.sleepersAwake();
			this.cleanup();
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000CF78 File Offset: 0x0000B178
		private void cleanup()
		{
			this.stopTimer();
			try
			{
				this.acceptReplies = false;
				if (this.conn != null)
				{
					this.conn.removeMessage(this);
				}
				if (this.replies != null)
				{
					while (this.replies.Count != 0)
					{
						object obj = this.replies[0];
						this.replies.RemoveAt(0);
					}
				}
			}
			catch (Exception)
			{
			}
			this.conn = null;
			this.msg = null;
			this.queue = null;
			this.bindprops = null;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000D008 File Offset: 0x0000B208
		~Message()
		{
			this.cleanup();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000D034 File Offset: 0x0000B234
		internal virtual void putReply(RfcLdapMessage message)
		{
			if (!this.acceptReplies)
			{
				return;
			}
			MessageVector messageVector = this.replies;
			lock (messageVector)
			{
				this.replies.Add(message);
			}
			message.RequestingMessage = this.msg;
			int type = message.Type;
			if (type != 4 && type != 19 && type != 25)
			{
				this.stopTimer();
				this.acceptReplies = false;
				this.complete = true;
				if (this.bindprops != null)
				{
					int num = ((RfcResponse)message.Response).getResultCode().intValue();
					if (num != 14)
					{
						if (num == 0)
						{
							this.conn.BindProperties = this.bindprops;
						}
						int bindSemId;
						if (this.conn.BindSemIdClear)
						{
							bindSemId = this.msgId;
						}
						else
						{
							bindSemId = this.conn.BindSemId;
							this.conn.clearBindSemId();
						}
						this.conn.freeWriteSemaphore(bindSemId);
					}
				}
			}
			this.sleepersAwake();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000D13C File Offset: 0x0000B33C
		internal virtual void stopTimer()
		{
			if (this.timer != null)
			{
				this.timer.Interrupt();
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000D154 File Offset: 0x0000B354
		private void sleepersAwake()
		{
			object syncRoot = this.replies.SyncRoot;
			lock (syncRoot)
			{
				Monitor.Pulse(this.replies.SyncRoot);
			}
			this.agent.sleepersAwake(false);
		}

		// Token: 0x0400019F RID: 415
		private LdapMessage msg;

		// Token: 0x040001A0 RID: 416
		private Connection conn;

		// Token: 0x040001A1 RID: 417
		private MessageAgent agent;

		// Token: 0x040001A2 RID: 418
		private LdapMessageQueue queue;

		// Token: 0x040001A3 RID: 419
		private int mslimit;

		// Token: 0x040001A4 RID: 420
		private SupportClass.ThreadClass timer;

		// Token: 0x040001A5 RID: 421
		private MessageVector replies;

		// Token: 0x040001A6 RID: 422
		private int msgId;

		// Token: 0x040001A7 RID: 423
		private bool acceptReplies = true;

		// Token: 0x040001A8 RID: 424
		private bool waitForReply_Renamed_Field = true;

		// Token: 0x040001A9 RID: 425
		private bool complete;

		// Token: 0x040001AA RID: 426
		private string name;

		// Token: 0x040001AB RID: 427
		private BindProperties bindprops;

		// Token: 0x02000050 RID: 80
		private sealed class Timeout : SupportClass.ThreadClass
		{
			// Token: 0x060002FD RID: 765 RVA: 0x0000D1B0 File Offset: 0x0000B3B0
			private void InitBlock(Message enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060002FE RID: 766 RVA: 0x0000D1B9 File Offset: 0x0000B3B9
			public Message Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x060002FF RID: 767 RVA: 0x0000D1C1 File Offset: 0x0000B3C1
			internal Timeout(Message enclosingInstance, int interval, Message msg)
			{
				this.InitBlock(enclosingInstance);
				this.timeToWait = interval;
				this.message = msg;
			}

			// Token: 0x06000300 RID: 768 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
			public override void Run()
			{
				try
				{
					Thread.Sleep(new TimeSpan((long)(10000 * this.timeToWait)));
					this.message.acceptReplies = false;
					this.message.Abandon(null, new InterThreadException("Client request timed out", null, 85, null, this.message));
				}
				catch (ThreadInterruptedException)
				{
				}
			}

			// Token: 0x040001AC RID: 428
			private Message enclosingInstance;

			// Token: 0x040001AD RID: 429
			private int timeToWait;

			// Token: 0x040001AE RID: 430
			private Message message;
		}
	}
}
