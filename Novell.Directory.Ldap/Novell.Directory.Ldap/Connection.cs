using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000016 RID: 22
	internal sealed class Connection
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000A6 RID: 166 RVA: 0x00003628 File Offset: 0x00001828
		// (remove) Token: 0x060000A7 RID: 167 RVA: 0x00003660 File Offset: 0x00001860
		public event CertificateValidationCallback OnCertificateValidation;

		// Token: 0x060000A8 RID: 168 RVA: 0x00003698 File Offset: 0x00001898
		private static string GetProblemMessage(Connection.CertificateProblem Problem)
		{
			string text = "";
			string text2 = Enum.GetName(typeof(Connection.CertificateProblem), Problem);
			if (text2 != null)
			{
				text += text2;
			}
			else
			{
				text = "Unknown Certificate Problem";
			}
			return text;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000036D8 File Offset: 0x000018D8
		private void InitBlock()
		{
			this.writeSemaphore = new object();
			this.encoder = new LBEREncoder();
			this.decoder = new LBERDecoder();
			this.stopReaderMessageID = -99;
			this.messages = new MessageVector(5, 5);
			this.unsolicitedListeners = new ArrayList(3);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003727 File Offset: 0x00001927
		internal bool Cloned
		{
			get
			{
				return this.cloneCount > 0;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003732 File Offset: 0x00001932
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000373A File Offset: 0x0000193A
		internal bool Ssl
		{
			get
			{
				return this.ssl;
			}
			set
			{
				this.ssl = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003743 File Offset: 0x00001943
		internal string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000AE RID: 174 RVA: 0x0000374B File Offset: 0x0000194B
		internal int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003753 File Offset: 0x00001953
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000375B File Offset: 0x0000195B
		internal int BindSemId
		{
			get
			{
				return this.bindSemaphoreId;
			}
			set
			{
				this.bindSemaphoreId = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003764 File Offset: 0x00001964
		internal bool BindSemIdClear
		{
			get
			{
				return this.bindSemaphoreId == 0;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003771 File Offset: 0x00001971
		internal bool Bound
		{
			get
			{
				return this.bindProperties != null && !this.bindProperties.Anonymous;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000378B File Offset: 0x0000198B
		internal bool Connected
		{
			get
			{
				return this.in_Renamed != null;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003796 File Offset: 0x00001996
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000379E File Offset: 0x0000199E
		internal BindProperties BindProperties
		{
			get
			{
				return this.bindProperties;
			}
			set
			{
				this.bindProperties = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000037A7 File Offset: 0x000019A7
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000037AF File Offset: 0x000019AF
		internal ReferralInfo ActiveReferral
		{
			get
			{
				return this.activeReferral;
			}
			set
			{
				this.activeReferral = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000037B8 File Offset: 0x000019B8
		internal string ConnectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000037C0 File Offset: 0x000019C0
		internal Connection()
		{
			this.InitBlock();
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000037F2 File Offset: 0x000019F2
		internal object copy()
		{
			Connection connection = new Connection();
			connection.host = this.host;
			connection.port = this.port;
			Connection.protocol = Connection.protocol;
			return connection;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000381B File Offset: 0x00001A1B
		internal int acquireWriteSemaphore()
		{
			return this.acquireWriteSemaphore(0);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003824 File Offset: 0x00001A24
		internal int acquireWriteSemaphore(int msgId)
		{
			int num = msgId;
			object obj = this.writeSemaphore;
			lock (obj)
			{
				if (num == 0)
				{
					int num3;
					if (this.ephemeralId != -2147483648)
					{
						int num2 = this.ephemeralId - 1;
						this.ephemeralId = num2;
						num3 = num2;
					}
					else
					{
						num3 = (this.ephemeralId = -1);
					}
					this.ephemeralId = num3;
					num = this.ephemeralId;
				}
				while (this.writeSemaphoreOwner != 0)
				{
					if (this.writeSemaphoreOwner != num)
					{
						try
						{
							Monitor.Wait(this.writeSemaphore);
							continue;
						}
						catch (ThreadInterruptedException)
						{
							continue;
						}
					}
					IL_0079:
					this.writeSemaphoreCount++;
					return num;
				}
				this.writeSemaphoreOwner = num;
				goto IL_0079;
			}
			return num;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000038E4 File Offset: 0x00001AE4
		internal void freeWriteSemaphore(int msgId)
		{
			object obj = this.writeSemaphore;
			lock (obj)
			{
				if (this.writeSemaphoreOwner == 0)
				{
					throw new SystemException("Connection.freeWriteSemaphore(" + msgId.ToString() + "): semaphore not owned by any thread");
				}
				if (this.writeSemaphoreOwner != msgId)
				{
					throw new SystemException("Connection.freeWriteSemaphore(" + msgId.ToString() + "): thread does not own the semaphore, owned by " + this.writeSemaphoreOwner.ToString());
				}
				int num = this.writeSemaphoreCount - 1;
				this.writeSemaphoreCount = num;
				if (num == 0)
				{
					this.writeSemaphoreOwner = 0;
					Monitor.Pulse(this.writeSemaphore);
				}
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003998 File Offset: 0x00001B98
		private void waitForReader(Thread thread)
		{
			Thread thread2;
			if (this.reader != null)
			{
				thread2 = this.reader;
			}
			else
			{
				thread2 = null;
			}
			Thread thread3;
			if (thread != null)
			{
				thread3 = thread;
			}
			else
			{
				thread3 = null;
			}
			while (!object.Equals(thread2, thread3))
			{
				try
				{
					if (thread == this.deadReader)
					{
						if (thread == null)
						{
							return;
						}
						IOException ex = this.deadReaderException;
						this.deadReaderException = null;
						this.deadReader = null;
						throw new LdapException("CONNECTION_READER", 91, null, ex);
					}
					else
					{
						lock (this)
						{
							Monitor.Wait(this, TimeSpan.FromMilliseconds(5.0));
						}
					}
				}
				catch (ThreadInterruptedException)
				{
				}
				if (this.reader != null)
				{
					thread2 = this.reader;
				}
				else
				{
					thread2 = null;
				}
				if (thread != null)
				{
					thread3 = thread;
					continue;
				}
				thread3 = null;
				continue;
			}
			this.deadReaderException = null;
			this.deadReader = null;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003A88 File Offset: 0x00001C88
		internal void connect(string host, int port)
		{
			this.connect(host, port, 0);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003A93 File Offset: 0x00001C93
		public bool ServerCertificateValidation(X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
		{
			if (this.OnCertificateValidation != null)
			{
				return this.OnCertificateValidation(certificate, errors);
			}
			return this.DefaultCertificateValidationHandler(certificate, chain, errors);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public bool DefaultCertificateValidationHandler(X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
		{
			return errors == SslPolicyErrors.None || errors == SslPolicyErrors.RemoteCertificateNameMismatch;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003AC4 File Offset: 0x00001CC4
		private void connect(string host, int port, int semaphoreId)
		{
			this.waitForReader(null);
			this.unsolSvrShutDnNotification = false;
			int num = this.acquireWriteSemaphore(semaphoreId);
			try
			{
				if (port == 0)
				{
					port = 389;
				}
				try
				{
					if (this.in_Renamed == null || this.out_Renamed == null)
					{
						if (this.Ssl)
						{
							this.host = host;
							this.port = port;
							this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
							IPEndPoint ipendPoint = new IPEndPoint(Dns.Resolve(host).AddressList[0], port);
							this.sock.Connect(ipendPoint);
							NetworkStream networkStream = new NetworkStream(this.sock, true);
							try
							{
								Assembly.LoadWithPartialName("Mono.Security");
							}
							catch (FileNotFoundException)
							{
								throw new LdapException("SSL_PROVIDER_MISSING", 114, null);
							}
							SslStream sslStream = new SslStream(networkStream, false);
							sslStream.AuthenticateAsClient(host);
							this.in_Renamed = sslStream;
							this.out_Renamed = sslStream;
						}
						else
						{
							this.socket = new TcpClient(host, port);
							this.in_Renamed = this.socket.GetStream();
							this.out_Renamed = this.socket.GetStream();
						}
					}
					else
					{
						Console.WriteLine("connect input/out Stream specified");
					}
				}
				catch (SocketException ex)
				{
					this.sock = null;
					this.socket = null;
					throw new LdapException("CONNECTION_ERROR", new object[] { host, port }, 91, null, ex);
				}
				catch (IOException ex2)
				{
					this.sock = null;
					this.socket = null;
					throw new LdapException("CONNECTION_ERROR", new object[] { host, port }, 91, null, ex2);
				}
				this.host = host;
				this.port = port;
				this.startReader();
				this.clientActive = true;
			}
			finally
			{
				this.freeWriteSemaphore(num);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003CC0 File Offset: 0x00001EC0
		internal void incrCloneCount()
		{
			lock (this)
			{
				this.cloneCount++;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003D04 File Offset: 0x00001F04
		internal Connection destroyClone(bool apiCall)
		{
			Connection connection2;
			lock (this)
			{
				Connection connection = this;
				if (this.cloneCount > 0)
				{
					this.cloneCount--;
					if (apiCall)
					{
						connection = (Connection)this.copy();
					}
					else
					{
						connection = null;
					}
				}
				else if (this.in_Renamed != null)
				{
					InterThreadException ex = new InterThreadException(apiCall ? "CONNECTION_CLOSED" : "CONNECTION_FINALIZED", null, 91, null, null);
					this.shutdown("destroy clone", 0, ex);
				}
				connection2 = connection;
			}
			return connection2;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003D9C File Offset: 0x00001F9C
		internal void clearBindSemId()
		{
			this.bindSemaphoreId = 0;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003DA8 File Offset: 0x00001FA8
		internal void writeMessage(Message info)
		{
			object[][] contents = new ExceptionMessages().getContents();
			this.messages.Add(info);
			if (info.BindRequest && !this.Connected && this.host != null)
			{
				this.connect(this.host, this.port, info.MessageID);
			}
			if (this.Connected)
			{
				LdapMessage request = info.Request;
				this.writeMessage(request);
				return;
			}
			int num = 0;
			while (num < contents.Length && contents[num][0] != "CONNECTION_CLOSED")
			{
				num++;
			}
			throw new LdapException("CONNECTION_CLOSED", new object[] { this.host, this.port }, 91, (string)contents[num][1]);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003E64 File Offset: 0x00002064
		internal void writeMessage(LdapMessage msg)
		{
			int messageID;
			if (this.bindSemaphoreId == 0)
			{
				messageID = msg.MessageID;
			}
			else
			{
				messageID = this.bindSemaphoreId;
			}
			Stream stream = this.out_Renamed;
			this.acquireWriteSemaphore(messageID);
			try
			{
				if (stream == null)
				{
					throw new IOException("Output stream not initialized");
				}
				if (!stream.CanWrite)
				{
					return;
				}
				sbyte[] encoding = msg.Asn1Object.getEncoding(this.encoder);
				stream.Write(SupportClass.ToByteArray(encoding), 0, encoding.Length);
				stream.Flush();
			}
			catch (IOException ex)
			{
				if (msg.Type == 0 && this.ssl)
				{
					string text = "Following problem(s) occurred while establishing SSL based Connection : ";
					if (this.handshakeProblemsEncountered.Count > 0)
					{
						text += Connection.GetProblemMessage((Connection.CertificateProblem)this.handshakeProblemsEncountered[0]);
						for (int i = 1; i < this.handshakeProblemsEncountered.Count; i++)
						{
							text = text + ", " + Connection.GetProblemMessage((Connection.CertificateProblem)this.handshakeProblemsEncountered[i]);
						}
					}
					else
					{
						text += "Unknown Certificate Problem";
					}
					throw new LdapException(text, new object[] { this.host, this.port }, 113, null, ex);
				}
				if (this.clientActive)
				{
					if (this.unsolSvrShutDnNotification)
					{
						throw new LdapException("SERVER_SHUTDOWN_REQ", new object[] { this.host, this.port }, 91, null, ex);
					}
					throw new LdapException("IO_EXCEPTION", new object[] { this.host, this.port }, 91, null, ex);
				}
			}
			finally
			{
				this.freeWriteSemaphore(messageID);
				this.handshakeProblemsEncountered.Clear();
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004058 File Offset: 0x00002258
		internal MessageAgent getMessageAgent(int msgId)
		{
			return this.messages.findMessageById(msgId).MessageAgent;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000406B File Offset: 0x0000226B
		internal void removeMessage(Message info)
		{
			SupportClass.VectorRemoveElement(this.messages, info);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000407C File Offset: 0x0000227C
		~Connection()
		{
			this.shutdown("Finalize", 0, null);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000040B0 File Offset: 0x000022B0
		private void shutdown(string reason, int semaphoreId, InterThreadException notifyUser)
		{
			Message message = null;
			if (!this.clientActive)
			{
				return;
			}
			this.clientActive = false;
			for (;;)
			{
				try
				{
					object obj = this.messages[0];
					this.messages.RemoveAt(0);
					message = (Message)obj;
				}
				catch (ArgumentOutOfRangeException)
				{
					break;
				}
				message.Abandon(null, notifyUser);
			}
			int num = this.acquireWriteSemaphore(semaphoreId);
			if (this.bindProperties != null && this.out_Renamed != null && this.out_Renamed.CanWrite && !this.bindProperties.Anonymous)
			{
				try
				{
					sbyte[] encoding = new LdapUnbindRequest(null).Asn1Object.getEncoding(this.encoder);
					this.out_Renamed.Write(SupportClass.ToByteArray(encoding), 0, encoding.Length);
					this.out_Renamed.Flush();
					this.out_Renamed.Close();
				}
				catch (Exception)
				{
				}
			}
			this.bindProperties = null;
			if (this.socket != null || this.sock != null)
			{
				if (this.reader != null && reason != "reader: thread stopping")
				{
					this.reader.Abort();
				}
				try
				{
					if (this.Ssl)
					{
						try
						{
							this.sock.Shutdown(SocketShutdown.Both);
						}
						catch
						{
						}
						this.sock.Close();
					}
					else
					{
						if (this.in_Renamed != null)
						{
							this.in_Renamed.Close();
						}
						this.socket.Close();
					}
				}
				catch (Exception)
				{
				}
				this.socket = null;
				this.sock = null;
				this.in_Renamed = null;
				this.out_Renamed = null;
			}
			this.freeWriteSemaphore(num);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004254 File Offset: 0x00002454
		internal bool areMessagesComplete()
		{
			object[] objectArray = this.messages.ObjectArray;
			int num = objectArray.Length;
			if (this.bindSemaphoreId != 0)
			{
				return false;
			}
			if (num == 0)
			{
				return true;
			}
			for (int i = 0; i < num; i++)
			{
				if (!((Message)objectArray[i]).Complete)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000429E File Offset: 0x0000249E
		internal void stopReaderOnReply(int messageID)
		{
			this.stopReaderMessageID = messageID;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000042A8 File Offset: 0x000024A8
		internal void startReader()
		{
			Thread thread = new Thread(new ThreadStart(new Connection.ReaderThread(this).Run));
			thread.IsBackground = true;
			thread.Start();
			this.waitForReader(thread);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000042E1 File Offset: 0x000024E1
		internal bool TLS
		{
			get
			{
				return this.nonTLSBackup != null;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000042EC File Offset: 0x000024EC
		internal void startTLS()
		{
			try
			{
				this.waitForReader(null);
				this.nonTLSBackup = this.socket;
				SslStream sslStream = new SslStream(this.socket.GetStream(), false);
				sslStream.AuthenticateAsClient(this.host);
				this.in_Renamed = sslStream;
				this.out_Renamed = sslStream;
			}
			catch (IOException ex)
			{
				this.nonTLSBackup = null;
				throw new LdapException("Could not negotiate a secure connection", 91, null, ex);
			}
			catch (Exception ex2)
			{
				this.nonTLSBackup = null;
				throw new LdapException("The host is unknown", 91, null, ex2);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004384 File Offset: 0x00002584
		internal void stopTLS()
		{
			try
			{
				this.stopReaderMessageID = -98;
				this.out_Renamed.Close();
				this.in_Renamed.Close();
				this.waitForReader(null);
				this.socket = this.nonTLSBackup;
				this.in_Renamed = this.socket.GetStream();
				this.out_Renamed = this.socket.GetStream();
				this.stopReaderMessageID = -99;
			}
			catch (IOException ex)
			{
				throw new LdapException("STOPTLS_ERROR", 91, null, ex);
			}
			finally
			{
				this.nonTLSBackup = null;
				this.startReader();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004428 File Offset: 0x00002628
		internal Stream InputStream
		{
			get
			{
				return this.in_Renamed;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004430 File Offset: 0x00002630
		internal Stream OutputStream
		{
			get
			{
				return this.out_Renamed;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004438 File Offset: 0x00002638
		internal void ReplaceStreams(Stream newIn, Stream newOut)
		{
			this.waitForReader(null);
			this.in_Renamed = newIn;
			this.out_Renamed = newOut;
			this.startReader();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004455 File Offset: 0x00002655
		internal void AddUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			this.unsolicitedListeners.Add(listener);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004464 File Offset: 0x00002664
		internal void RemoveUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			SupportClass.VectorRemoveElement(this.unsolicitedListeners, listener);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004474 File Offset: 0x00002674
		private void notifyAllUnsolicitedListeners(RfcLdapMessage message)
		{
			if (((LdapExtendedResponse)new LdapExtendedResponse(message)).ID.Equals("1.3.6.1.4.1.1466.20036"))
			{
				this.unsolSvrShutDnNotification = true;
			}
			int count = this.unsolicitedListeners.Count;
			for (int i = 0; i < count; i++)
			{
				LdapUnsolicitedNotificationListener ldapUnsolicitedNotificationListener = (LdapUnsolicitedNotificationListener)this.unsolicitedListeners[i];
				LdapExtendedResponse ldapExtendedResponse = new LdapExtendedResponse(message);
				new Connection.UnsolicitedListenerThread(this, ldapUnsolicitedNotificationListener, ldapExtendedResponse).Start();
			}
		}

		// Token: 0x04000045 RID: 69
		private ArrayList handshakeProblemsEncountered = new ArrayList();

		// Token: 0x04000046 RID: 70
		private object writeSemaphore;

		// Token: 0x04000047 RID: 71
		private int writeSemaphoreOwner;

		// Token: 0x04000048 RID: 72
		private int writeSemaphoreCount;

		// Token: 0x04000049 RID: 73
		private int ephemeralId = -1;

		// Token: 0x0400004A RID: 74
		private BindProperties bindProperties;

		// Token: 0x0400004B RID: 75
		private int bindSemaphoreId;

		// Token: 0x0400004C RID: 76
		private Thread reader;

		// Token: 0x0400004D RID: 77
		private Thread deadReader;

		// Token: 0x0400004E RID: 78
		private IOException deadReaderException;

		// Token: 0x0400004F RID: 79
		private LBEREncoder encoder;

		// Token: 0x04000050 RID: 80
		private LBERDecoder decoder;

		// Token: 0x04000051 RID: 81
		private Socket sock;

		// Token: 0x04000052 RID: 82
		private TcpClient socket;

		// Token: 0x04000053 RID: 83
		private TcpClient nonTLSBackup;

		// Token: 0x04000054 RID: 84
		private Stream in_Renamed;

		// Token: 0x04000055 RID: 85
		private Stream out_Renamed;

		// Token: 0x04000056 RID: 86
		private bool clientActive = true;

		// Token: 0x04000057 RID: 87
		private bool ssl;

		// Token: 0x04000058 RID: 88
		private bool unsolSvrShutDnNotification;

		// Token: 0x04000059 RID: 89
		private const int CONTINUE_READING = -99;

		// Token: 0x0400005A RID: 90
		private const int STOP_READING = -98;

		// Token: 0x0400005B RID: 91
		private int stopReaderMessageID;

		// Token: 0x0400005C RID: 92
		private MessageVector messages;

		// Token: 0x0400005D RID: 93
		private ReferralInfo activeReferral;

		// Token: 0x0400005E RID: 94
		private ArrayList unsolicitedListeners;

		// Token: 0x0400005F RID: 95
		private string host;

		// Token: 0x04000060 RID: 96
		private int port;

		// Token: 0x04000061 RID: 97
		private int cloneCount;

		// Token: 0x04000062 RID: 98
		private string name = "";

		// Token: 0x04000063 RID: 99
		private static object nameLock = new object();

		// Token: 0x04000064 RID: 100
		private static int connNum = 0;

		// Token: 0x04000065 RID: 101
		internal static string sdk = new StringBuilder("2.1.8").ToString();

		// Token: 0x04000066 RID: 102
		internal static int protocol = 3;

		// Token: 0x04000067 RID: 103
		internal static string security = "simple";

		// Token: 0x02000017 RID: 23
		public enum CertificateProblem : long
		{
			// Token: 0x04000069 RID: 105
			CertEXPIRED = 2148204801L,
			// Token: 0x0400006A RID: 106
			CertVALIDITYPERIODNESTING,
			// Token: 0x0400006B RID: 107
			CertROLE,
			// Token: 0x0400006C RID: 108
			CertPATHLENCONST,
			// Token: 0x0400006D RID: 109
			CertCRITICAL,
			// Token: 0x0400006E RID: 110
			CertPURPOSE,
			// Token: 0x0400006F RID: 111
			CertISSUERCHAINING,
			// Token: 0x04000070 RID: 112
			CertMALFORMED,
			// Token: 0x04000071 RID: 113
			CertUNTRUSTEDROOT,
			// Token: 0x04000072 RID: 114
			CertCHAINING,
			// Token: 0x04000073 RID: 115
			CertREVOKED = 2148204812L,
			// Token: 0x04000074 RID: 116
			CertUNTRUSTEDTESTROOT,
			// Token: 0x04000075 RID: 117
			CertREVOCATION_FAILURE,
			// Token: 0x04000076 RID: 118
			CertCN_NO_MATCH,
			// Token: 0x04000077 RID: 119
			CertWRONG_USAGE,
			// Token: 0x04000078 RID: 120
			CertUNTRUSTEDCA = 2148204818L
		}

		// Token: 0x02000018 RID: 24
		public class ReaderThread
		{
			// Token: 0x060000D9 RID: 217 RVA: 0x00004518 File Offset: 0x00002718
			private void InitBlock(Connection enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x060000DA RID: 218 RVA: 0x00004521 File Offset: 0x00002721
			public Connection Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x060000DB RID: 219 RVA: 0x00004529 File Offset: 0x00002729
			public ReaderThread(Connection enclosingInstance)
			{
				this.InitBlock(enclosingInstance);
			}

			// Token: 0x060000DC RID: 220 RVA: 0x00004538 File Offset: 0x00002738
			public virtual void Run()
			{
				string text = "reader: thread stopping";
				InterThreadException ex = null;
				Message message = null;
				IOException ex2 = null;
				this.enclosingInstance.reader = Thread.CurrentThread;
				try
				{
					for (;;)
					{
						Stream in_Renamed = this.enclosingInstance.in_Renamed;
						if (in_Renamed == null)
						{
							goto IL_0113;
						}
						Asn1Identifier asn1Identifier = new Asn1Identifier(in_Renamed);
						int tag = asn1Identifier.Tag;
						if (asn1Identifier.Tag == 16)
						{
							Asn1Length asn1Length = new Asn1Length(in_Renamed);
							RfcLdapMessage rfcLdapMessage = new RfcLdapMessage(this.enclosingInstance.decoder, in_Renamed, asn1Length.Length);
							int messageID = rfcLdapMessage.MessageID;
							try
							{
								message = this.enclosingInstance.messages.findMessageById(messageID);
								message.putReply(rfcLdapMessage);
							}
							catch (FieldAccessException)
							{
								if (messageID == 0)
								{
									this.enclosingInstance.notifyAllUnsolicitedListeners(rfcLdapMessage);
									if (this.enclosingInstance.unsolSvrShutDnNotification)
									{
										ex = new InterThreadException("SERVER_SHUTDOWN_REQ", new object[]
										{
											this.enclosingInstance.host,
											this.enclosingInstance.port
										}, 91, null, null);
										break;
									}
								}
							}
							if (this.enclosingInstance.stopReaderMessageID == messageID || this.enclosingInstance.stopReaderMessageID == -98)
							{
								break;
							}
						}
					}
					return;
					IL_0113:;
				}
				catch (ThreadAbortException)
				{
					return;
				}
				catch (IOException ex3)
				{
					ex2 = ex3;
					if (this.enclosingInstance.stopReaderMessageID != -98 && this.enclosingInstance.clientActive)
					{
						ex = new InterThreadException("CONNECTION_WAIT", new object[]
						{
							this.enclosingInstance.host,
							this.enclosingInstance.port
						}, 91, ex3, message);
					}
					this.enclosingInstance.in_Renamed = null;
					this.enclosingInstance.out_Renamed = null;
				}
				finally
				{
					if (!this.enclosingInstance.clientActive || ex != null)
					{
						this.enclosingInstance.shutdown(text, 0, ex);
					}
					else
					{
						this.enclosingInstance.stopReaderMessageID = -99;
					}
				}
				this.enclosingInstance.deadReaderException = ex2;
				this.enclosingInstance.deadReader = this.enclosingInstance.reader;
				this.enclosingInstance.reader = null;
			}

			// Token: 0x04000079 RID: 121
			private Connection enclosingInstance;
		}

		// Token: 0x02000019 RID: 25
		private class UnsolicitedListenerThread : SupportClass.ThreadClass
		{
			// Token: 0x060000DD RID: 221 RVA: 0x00004798 File Offset: 0x00002998
			private void InitBlock(Connection enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x060000DE RID: 222 RVA: 0x000047A1 File Offset: 0x000029A1
			public Connection Enclosing_Instance
			{
				get
				{
					return this.enclosingInstance;
				}
			}

			// Token: 0x060000DF RID: 223 RVA: 0x000047A9 File Offset: 0x000029A9
			internal UnsolicitedListenerThread(Connection enclosingInstance, LdapUnsolicitedNotificationListener l, LdapExtendedResponse m)
			{
				this.InitBlock(enclosingInstance);
				this.listenerObj = l;
				this.unsolicitedMsg = m;
			}

			// Token: 0x060000E0 RID: 224 RVA: 0x000047C6 File Offset: 0x000029C6
			public override void Run()
			{
				this.listenerObj.messageReceived(this.unsolicitedMsg);
			}

			// Token: 0x0400007A RID: 122
			private Connection enclosingInstance;

			// Token: 0x0400007B RID: 123
			private LdapUnsolicitedNotificationListener listenerObj;

			// Token: 0x0400007C RID: 124
			private LdapExtendedResponse unsolicitedMsg;
		}
	}
}
