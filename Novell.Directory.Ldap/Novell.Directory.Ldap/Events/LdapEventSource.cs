using System;
using System.Threading;

namespace Novell.Directory.Ldap.Events
{
	// Token: 0x020000BF RID: 191
	public abstract class LdapEventSource
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x000158F5 File Offset: 0x00013AF5
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x000158FD File Offset: 0x00013AFD
		public int SleepInterval
		{
			get
			{
				return this.sleep_interval;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("SleepInterval", "cannot take the negative or zero values ");
				}
				this.sleep_interval = value;
			}
		}

		// Token: 0x060004E2 RID: 1250
		protected abstract int GetListeners();

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001591C File Offset: 0x00013B1C
		protected LdapEventSource.LISTENERS_COUNT GetCurrentListenersState()
		{
			int num = 0;
			num += this.GetListeners();
			if (this.directory_event != null)
			{
				num += this.directory_event.GetInvocationList().Length;
			}
			if (this.directory_exception_event != null)
			{
				num += this.directory_exception_event.GetInvocationList().Length;
			}
			if (num == 0)
			{
				return LdapEventSource.LISTENERS_COUNT.ZERO;
			}
			if (1 == num)
			{
				return LdapEventSource.LISTENERS_COUNT.ONE;
			}
			return LdapEventSource.LISTENERS_COUNT.MORE_THAN_ONE;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00015970 File Offset: 0x00013B70
		protected void ListenerAdded()
		{
			switch (this.GetCurrentListenersState())
			{
			case LdapEventSource.LISTENERS_COUNT.ZERO:
			case LdapEventSource.LISTENERS_COUNT.MORE_THAN_ONE:
				break;
			case LdapEventSource.LISTENERS_COUNT.ONE:
				this.StartSearchAndPolling();
				break;
			default:
				return;
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000159A0 File Offset: 0x00013BA0
		protected void ListenerRemoved()
		{
			LdapEventSource.LISTENERS_COUNT currentListenersState = this.GetCurrentListenersState();
			if (currentListenersState != LdapEventSource.LISTENERS_COUNT.ZERO)
			{
				int num = currentListenersState - LdapEventSource.LISTENERS_COUNT.ONE;
				return;
			}
			this.StopSearchAndPolling();
		}

		// Token: 0x060004E6 RID: 1254
		protected abstract void StartSearchAndPolling();

		// Token: 0x060004E7 RID: 1255
		protected abstract void StopSearchAndPolling();

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060004E8 RID: 1256 RVA: 0x000159C4 File Offset: 0x00013BC4
		// (remove) Token: 0x060004E9 RID: 1257 RVA: 0x000159E3 File Offset: 0x00013BE3
		public event LdapEventSource.DirectoryEventHandler DirectoryEvent
		{
			add
			{
				this.directory_event = (LdapEventSource.DirectoryEventHandler)Delegate.Combine(this.directory_event, value);
				this.ListenerAdded();
			}
			remove
			{
				this.directory_event = (LdapEventSource.DirectoryEventHandler)Delegate.Remove(this.directory_event, value);
				this.ListenerRemoved();
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060004EA RID: 1258 RVA: 0x00015A02 File Offset: 0x00013C02
		// (remove) Token: 0x060004EB RID: 1259 RVA: 0x00015A21 File Offset: 0x00013C21
		public event LdapEventSource.DirectoryExceptionEventHandler DirectoryExceptionEvent
		{
			add
			{
				this.directory_exception_event = (LdapEventSource.DirectoryExceptionEventHandler)Delegate.Combine(this.directory_exception_event, value);
				this.ListenerAdded();
			}
			remove
			{
				this.directory_exception_event = (LdapEventSource.DirectoryExceptionEventHandler)Delegate.Remove(this.directory_exception_event, value);
				this.ListenerRemoved();
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00015A40 File Offset: 0x00013C40
		protected void StartEventPolling(LdapMessageQueue queue, LdapConnection conn, int msgid)
		{
			if (queue == null || conn == null)
			{
				throw new ArgumentException("No parameter can be Null.");
			}
			if (this.m_objEventsGenerator == null)
			{
				this.m_objEventsGenerator = new LdapEventSource.EventsGenerator(this, queue, conn, msgid);
				this.m_objEventsGenerator.SleepTime = this.sleep_interval;
				this.m_objEventsGenerator.StartEventPolling();
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00015A91 File Offset: 0x00013C91
		protected void StopEventPolling()
		{
			if (this.m_objEventsGenerator != null)
			{
				this.m_objEventsGenerator.StopEventPolling();
				this.m_objEventsGenerator = null;
			}
		}

		// Token: 0x060004EE RID: 1262
		protected abstract bool NotifyEventListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType);

		// Token: 0x060004EF RID: 1263 RVA: 0x00015AAD File Offset: 0x00013CAD
		protected void NotifyListeners(LdapMessage sourceMessage, EventClassifiers aClassification, int nType)
		{
			if (!this.NotifyEventListeners(sourceMessage, aClassification, nType))
			{
				this.NotifyDirectoryListeners(sourceMessage, aClassification);
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00015AC2 File Offset: 0x00013CC2
		protected void NotifyDirectoryListeners(LdapMessage sourceMessage, EventClassifiers aClassification)
		{
			this.NotifyDirectoryListeners(new DirectoryEventArgs(sourceMessage, aClassification));
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00015AD1 File Offset: 0x00013CD1
		protected void NotifyDirectoryListeners(DirectoryEventArgs objDirectoryEventArgs)
		{
			if (this.directory_event != null)
			{
				this.directory_event(this, objDirectoryEventArgs);
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00015AE8 File Offset: 0x00013CE8
		protected void NotifyExceptionListeners(LdapMessage sourceMessage, LdapException ldapException)
		{
			if (this.directory_exception_event != null)
			{
				this.directory_exception_event(this, new DirectoryExceptionEventArgs(sourceMessage, ldapException));
			}
		}

		// Token: 0x04000346 RID: 838
		protected internal const int EVENT_TYPE_UNKNOWN = -1;

		// Token: 0x04000347 RID: 839
		protected const int DEFAULT_SLEEP_TIME = 1000;

		// Token: 0x04000348 RID: 840
		protected int sleep_interval = 1000;

		// Token: 0x04000349 RID: 841
		protected LdapEventSource.DirectoryEventHandler directory_event;

		// Token: 0x0400034A RID: 842
		protected LdapEventSource.DirectoryExceptionEventHandler directory_exception_event;

		// Token: 0x0400034B RID: 843
		protected LdapEventSource.EventsGenerator m_objEventsGenerator;

		// Token: 0x020000C0 RID: 192
		protected enum LISTENERS_COUNT
		{
			// Token: 0x0400034D RID: 845
			ZERO,
			// Token: 0x0400034E RID: 846
			ONE,
			// Token: 0x0400034F RID: 847
			MORE_THAN_ONE
		}

		// Token: 0x020000C1 RID: 193
		// (Invoke) Token: 0x060004F5 RID: 1269
		public delegate void DirectoryEventHandler(object source, DirectoryEventArgs objDirectoryEventArgs);

		// Token: 0x020000C2 RID: 194
		// (Invoke) Token: 0x060004F9 RID: 1273
		public delegate void DirectoryExceptionEventHandler(object source, DirectoryExceptionEventArgs objDirectoryExceptionEventArgs);

		// Token: 0x020000C3 RID: 195
		protected class EventsGenerator
		{
			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060004FC RID: 1276 RVA: 0x00015B18 File Offset: 0x00013D18
			// (set) Token: 0x060004FD RID: 1277 RVA: 0x00015B20 File Offset: 0x00013D20
			public int SleepTime
			{
				get
				{
					return this.sleep_time;
				}
				set
				{
					this.sleep_time = value;
				}
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x00015B29 File Offset: 0x00013D29
			public EventsGenerator(LdapEventSource objEventSource, LdapMessageQueue queue, LdapConnection conn, int msgid)
			{
				this.m_objLdapEventSource = objEventSource;
				this.searchqueue = queue;
				this.ldapconnection = conn;
				this.messageid = msgid;
				this.sleep_time = 1000;
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x00015B64 File Offset: 0x00013D64
			protected void Run()
			{
				while (this.isrunning)
				{
					LdapMessage ldapMessage = null;
					try
					{
						while (this.isrunning && !this.searchqueue.isResponseReceived(this.messageid))
						{
							try
							{
								Thread.Sleep(this.sleep_time);
							}
							catch (ThreadInterruptedException ex)
							{
								Console.WriteLine("EventsGenerator::Run Got ThreadInterruptedException e = {0}", ex);
							}
						}
						if (this.isrunning)
						{
							ldapMessage = this.searchqueue.getResponse(this.messageid);
						}
						if (ldapMessage != null)
						{
							this.processmessage(ldapMessage);
						}
					}
					catch (LdapException ex2)
					{
						this.m_objLdapEventSource.NotifyExceptionListeners(ldapMessage, ex2);
					}
				}
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x00015C10 File Offset: 0x00013E10
			protected void processmessage(LdapMessage response)
			{
				if (response is LdapResponse)
				{
					try
					{
						((LdapResponse)response).chkResultCode();
						this.m_objLdapEventSource.NotifyEventListeners(response, EventClassifiers.CLASSIFICATION_UNKNOWN, -1);
						return;
					}
					catch (LdapException ex)
					{
						this.m_objLdapEventSource.NotifyExceptionListeners(response, ex);
						return;
					}
				}
				this.m_objLdapEventSource.NotifyEventListeners(response, EventClassifiers.CLASSIFICATION_UNKNOWN, -1);
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x00015C70 File Offset: 0x00013E70
			public void StartEventPolling()
			{
				this.isrunning = true;
				new Thread(new ThreadStart(this.Run)).Start();
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x00015C91 File Offset: 0x00013E91
			public void StopEventPolling()
			{
				this.isrunning = false;
			}

			// Token: 0x04000350 RID: 848
			private LdapEventSource m_objLdapEventSource;

			// Token: 0x04000351 RID: 849
			private LdapMessageQueue searchqueue;

			// Token: 0x04000352 RID: 850
			private int messageid;

			// Token: 0x04000353 RID: 851
			private LdapConnection ldapconnection;

			// Token: 0x04000354 RID: 852
			private volatile bool isrunning = true;

			// Token: 0x04000355 RID: 853
			private int sleep_time;
		}
	}
}
