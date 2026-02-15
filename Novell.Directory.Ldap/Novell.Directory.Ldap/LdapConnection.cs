using System;
using System.Collections;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000027 RID: 39
	public class LdapConnection : ICloneable
	{
		// Token: 0x06000142 RID: 322 RVA: 0x000060C7 File Offset: 0x000042C7
		private void InitBlock()
		{
			this.defSearchCons = new LdapSearchConstraints();
			this.responseCtlSemaphore = new object();
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000060E0 File Offset: 0x000042E0
		public virtual int ProtocolVersion
		{
			get
			{
				BindProperties bindProperties = this.conn.BindProperties;
				if (bindProperties == null)
				{
					return 3;
				}
				return bindProperties.ProtocolVersion;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006104 File Offset: 0x00004304
		public virtual string AuthenticationDN
		{
			get
			{
				BindProperties bindProperties = this.conn.BindProperties;
				if (bindProperties == null)
				{
					return null;
				}
				if (bindProperties.Anonymous)
				{
					return null;
				}
				return bindProperties.AuthenticationDN;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006132 File Offset: 0x00004332
		public virtual string AuthenticationMethod
		{
			get
			{
				if (this.conn.BindProperties == null)
				{
					return "simple";
				}
				return this.conn.BindProperties.AuthenticationMethod;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00006157 File Offset: 0x00004357
		public virtual IDictionary SaslBindProperties
		{
			get
			{
				if (this.conn.BindProperties == null)
				{
					return null;
				}
				return this.conn.BindProperties.SaslBindProperties;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006178 File Offset: 0x00004378
		public virtual object SaslBindCallbackHandler
		{
			get
			{
				if (this.conn.BindProperties == null)
				{
					return null;
				}
				return this.conn.BindProperties.SaslCallbackHandler;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00006199 File Offset: 0x00004399
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000061AC File Offset: 0x000043AC
		public virtual LdapConstraints Constraints
		{
			get
			{
				return (LdapConstraints)this.defSearchCons.Clone();
			}
			set
			{
				if (value is LdapSearchConstraints)
				{
					this.defSearchCons = (LdapSearchConstraints)value.Clone();
					return;
				}
				LdapSearchConstraints ldapSearchConstraints = (LdapSearchConstraints)this.defSearchCons.Clone();
				ldapSearchConstraints.HopLimit = value.HopLimit;
				ldapSearchConstraints.TimeLimit = value.TimeLimit;
				ldapSearchConstraints.setReferralHandler(value.getReferralHandler());
				ldapSearchConstraints.ReferralFollowing = value.ReferralFollowing;
				LdapControl[] controls = value.getControls();
				if (controls != null)
				{
					ldapSearchConstraints.setControls(controls);
				}
				Hashtable properties = ldapSearchConstraints.Properties;
				if (properties != null)
				{
					ldapSearchConstraints.Properties = properties;
				}
				this.defSearchCons = ldapSearchConstraints;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000623D File Offset: 0x0000443D
		public virtual string Host
		{
			get
			{
				return this.conn.Host;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000624A File Offset: 0x0000444A
		public virtual int Port
		{
			get
			{
				return this.conn.Port;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00006257 File Offset: 0x00004457
		public virtual LdapSearchConstraints SearchConstraints
		{
			get
			{
				return (LdapSearchConstraints)this.defSearchCons.Clone();
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00006269 File Offset: 0x00004469
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00006276 File Offset: 0x00004476
		public bool SecureSocketLayer
		{
			get
			{
				return this.conn.Ssl;
			}
			set
			{
				this.conn.Ssl = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00006284 File Offset: 0x00004484
		public virtual bool Bound
		{
			get
			{
				return this.conn.Bound;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00006291 File Offset: 0x00004491
		public virtual bool Connected
		{
			get
			{
				return this.conn.Connected;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000629E File Offset: 0x0000449E
		public virtual bool TLS
		{
			get
			{
				return this.conn.TLS;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000062AC File Offset: 0x000044AC
		public virtual LdapControl[] ResponseControls
		{
			get
			{
				if (this.responseCtls == null)
				{
					return null;
				}
				LdapControl[] array = new LdapControl[this.responseCtls.Length];
				object obj = this.responseCtlSemaphore;
				lock (obj)
				{
					for (int i = 0; i < this.responseCtls.Length; i++)
					{
						array[i] = (LdapControl)this.responseCtls[i].Clone();
					}
				}
				return array;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006328 File Offset: 0x00004528
		internal virtual Connection Connection
		{
			get
			{
				return this.conn;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006330 File Offset: 0x00004530
		internal virtual string ConnectionName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000155 RID: 341 RVA: 0x00006338 File Offset: 0x00004538
		// (remove) Token: 0x06000156 RID: 342 RVA: 0x00006346 File Offset: 0x00004546
		public event CertificateValidationCallback UserDefinedServerCertValidationDelegate
		{
			add
			{
				this.conn.OnCertificateValidation += value;
			}
			remove
			{
				this.conn.OnCertificateValidation -= value;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006354 File Offset: 0x00004554
		public LdapConnection()
		{
			this.InitBlock();
			this.conn = new Connection();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006370 File Offset: 0x00004570
		public object Clone()
		{
			object obj;
			LdapConnection ldapConnection;
			try
			{
				obj = base.MemberwiseClone();
				ldapConnection = (LdapConnection)obj;
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			ldapConnection.conn = this.conn;
			if (this.defSearchCons != null)
			{
				ldapConnection.defSearchCons = (LdapSearchConstraints)this.defSearchCons.Clone();
			}
			else
			{
				ldapConnection.defSearchCons = null;
			}
			if (this.responseCtls != null)
			{
				ldapConnection.responseCtls = new LdapControl[this.responseCtls.Length];
				for (int i = 0; i < this.responseCtls.Length; i++)
				{
					ldapConnection.responseCtls[i] = (LdapControl)this.responseCtls[i].Clone();
				}
			}
			else
			{
				ldapConnection.responseCtls = null;
			}
			this.conn.incrCloneCount();
			return obj;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000643C File Offset: 0x0000463C
		~LdapConnection()
		{
			this.Disconnect(this.defSearchCons, false);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006470 File Offset: 0x00004670
		public virtual object getProperty(string name)
		{
			if (name.ToUpper().Equals("version.sdk".ToUpper()))
			{
				return Connection.sdk;
			}
			if (name.ToUpper().Equals("version.protocol".ToUpper()))
			{
				return Connection.protocol;
			}
			if (name.ToUpper().Equals("version.security".ToUpper()))
			{
				return Connection.security;
			}
			return null;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000064DA File Offset: 0x000046DA
		public virtual void AddUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			if (listener != null)
			{
				this.conn.AddUnsolicitedNotificationListener(listener);
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000064EB File Offset: 0x000046EB
		public virtual void RemoveUnsolicitedNotificationListener(LdapUnsolicitedNotificationListener listener)
		{
			if (listener != null)
			{
				this.conn.RemoveUnsolicitedNotificationListener(listener);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000064FC File Offset: 0x000046FC
		public virtual void startTLS()
		{
			LdapMessage ldapMessage = this.MakeExtendedOperation(new LdapExtendedOperation("1.3.6.1.4.1.1466.20037", null), null);
			int messageID = ldapMessage.MessageID;
			this.conn.acquireWriteSemaphore(messageID);
			try
			{
				if (!this.conn.areMessagesComplete())
				{
					throw new LdapLocalException("OUTSTANDING_OPERATIONS", 1);
				}
				this.conn.stopReaderOnReply(messageID);
				((LdapExtendedResponse)this.SendRequestToServer(ldapMessage, this.defSearchCons.TimeLimit, null, null).getResponse()).chkResultCode();
				this.conn.startTLS();
			}
			finally
			{
				this.conn.startReader();
				this.conn.freeWriteSemaphore(messageID);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000065B0 File Offset: 0x000047B0
		public virtual void stopTLS()
		{
			if (!this.TLS)
			{
				throw new LdapLocalException("NO_STARTTLS", 1);
			}
			int num = this.conn.acquireWriteSemaphore();
			try
			{
				if (!this.conn.areMessagesComplete())
				{
					throw new LdapLocalException("OUTSTANDING_OPERATIONS", 1);
				}
				this.conn.stopTLS();
			}
			finally
			{
				this.conn.freeWriteSemaphore(num);
				this.Connect(this.Host, this.Port);
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006634 File Offset: 0x00004834
		public virtual void Abandon(LdapSearchResults results)
		{
			this.Abandon(results, this.defSearchCons);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006643 File Offset: 0x00004843
		public virtual void Abandon(LdapSearchResults results, LdapConstraints cons)
		{
			results.Abandon();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000664B File Offset: 0x0000484B
		public virtual void Abandon(int id)
		{
			this.Abandon(id, this.defSearchCons);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000665C File Offset: 0x0000485C
		public virtual void Abandon(int id, LdapConstraints cons)
		{
			try
			{
				this.conn.getMessageAgent(id).Abandon(id, cons);
			}
			catch (FieldAccessException)
			{
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006694 File Offset: 0x00004894
		public virtual void Abandon(LdapMessageQueue queue)
		{
			this.Abandon(queue, this.defSearchCons);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000066A4 File Offset: 0x000048A4
		public virtual void Abandon(LdapMessageQueue queue, LdapConstraints cons)
		{
			if (queue != null)
			{
				MessageAgent messageAgent;
				if (queue is LdapSearchQueue)
				{
					messageAgent = queue.MessageAgent;
				}
				else
				{
					messageAgent = queue.MessageAgent;
				}
				int[] messageIDs = messageAgent.MessageIDs;
				for (int i = 0; i < messageIDs.Length; i++)
				{
					messageAgent.Abandon(messageIDs[i], cons);
				}
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000066EB File Offset: 0x000048EB
		public virtual void Add(LdapEntry entry)
		{
			this.Add(entry, this.defSearchCons);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000066FC File Offset: 0x000048FC
		public virtual void Add(LdapEntry entry, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Add(entry, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006764 File Offset: 0x00004964
		public virtual LdapResponseQueue Add(LdapEntry entry, LdapResponseQueue queue)
		{
			return this.Add(entry, queue, this.defSearchCons);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006774 File Offset: 0x00004974
		public virtual LdapResponseQueue Add(LdapEntry entry, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			if (entry == null)
			{
				throw new ArgumentException("The LdapEntry parameter cannot be null");
			}
			if (entry.DN == null)
			{
				throw new ArgumentException("The DN value must be present in the LdapEntry object");
			}
			LdapMessage ldapMessage = new LdapAddRequest(entry, cons.getControls());
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000067C9 File Offset: 0x000049C9
		public virtual void Bind(string dn, string passwd)
		{
			this.Bind(dn, passwd, AuthenticationTypes.None);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000067D4 File Offset: 0x000049D4
		public virtual void Bind(string dn, string passwd, AuthenticationTypes authenticationTypes)
		{
			this.Bind(3, dn, passwd, this.defSearchCons);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000067E5 File Offset: 0x000049E5
		public virtual void Bind(int version, string dn, string passwd)
		{
			this.Bind(version, dn, passwd, this.defSearchCons);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000067F6 File Offset: 0x000049F6
		public virtual void Bind(string dn, string passwd, LdapConstraints cons)
		{
			this.Bind(3, dn, passwd, cons);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006804 File Offset: 0x00004A04
		public virtual void Bind(int version, string dn, string passwd, LdapConstraints cons)
		{
			sbyte[] array = null;
			if (passwd != null)
			{
				try
				{
					array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(passwd));
					passwd = null;
				}
				catch (IOException ex)
				{
					passwd = null;
					throw new SystemException(ex.ToString());
				}
			}
			this.Bind(version, dn, array, cons);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000685C File Offset: 0x00004A5C
		[CLSCompliant(false)]
		public virtual void Bind(int version, string dn, sbyte[] passwd)
		{
			this.Bind(version, dn, passwd, this.defSearchCons);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006870 File Offset: 0x00004A70
		[CLSCompliant(false)]
		public virtual void Bind(int version, string dn, sbyte[] passwd, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Bind(version, dn, passwd, null, cons, null);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			if (ldapResponse != null)
			{
				object obj = this.responseCtlSemaphore;
				lock (obj)
				{
					this.responseCtls = ldapResponse.Controls;
				}
				this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000068E0 File Offset: 0x00004AE0
		[CLSCompliant(false)]
		public virtual LdapResponseQueue Bind(int version, string dn, sbyte[] passwd, LdapResponseQueue queue)
		{
			return this.Bind(version, dn, passwd, queue, this.defSearchCons, null);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000068F4 File Offset: 0x00004AF4
		[CLSCompliant(false)]
		public virtual LdapResponseQueue Bind(int version, string dn, sbyte[] passwd, LdapResponseQueue queue, LdapConstraints cons, string mech)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			if (dn == null)
			{
				dn = "";
			}
			else
			{
				dn = dn.Trim();
			}
			if (passwd == null)
			{
				passwd = new sbyte[0];
			}
			bool flag = false;
			if (passwd.Length == 0)
			{
				flag = true;
				dn = "";
			}
			LdapMessage ldapMessage = new LdapBindRequest(version, dn, passwd, cons.getControls());
			int messageID = ldapMessage.MessageID;
			BindProperties bindProperties = new BindProperties(version, dn, "simple", flag, null, null);
			if (!this.conn.Connected)
			{
				if (this.conn.Host == null)
				{
					throw new LdapException("CONNECTION_IMPOSSIBLE", 91, null);
				}
				this.conn.connect(this.conn.Host, this.conn.Port);
			}
			this.conn.acquireWriteSemaphore(messageID);
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, bindProperties);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000069CB File Offset: 0x00004BCB
		public virtual bool Compare(string dn, LdapAttribute attr)
		{
			return this.Compare(dn, attr, this.defSearchCons);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000069DC File Offset: 0x00004BDC
		public virtual bool Compare(string dn, LdapAttribute attr, LdapConstraints cons)
		{
			bool flag = false;
			LdapResponseQueue ldapResponseQueue = this.Compare(dn, attr, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			if (ldapResponse.ResultCode == 6)
			{
				flag = true;
			}
			else if (ldapResponse.ResultCode == 5)
			{
				flag = false;
			}
			else
			{
				this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
			}
			return flag;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006A64 File Offset: 0x00004C64
		public virtual LdapResponseQueue Compare(string dn, LdapAttribute attr, LdapResponseQueue queue)
		{
			return this.Compare(dn, attr, queue, this.defSearchCons);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006A78 File Offset: 0x00004C78
		public virtual LdapResponseQueue Compare(string dn, LdapAttribute attr, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (attr.size() != 1)
			{
				throw new ArgumentException("compare: Exactly one value must be present in the LdapAttribute");
			}
			if (dn == null)
			{
				throw new ArgumentException("compare: DN cannot be null");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = new LdapCompareRequest(dn, attr.Name, attr.ByteValue, cons.getControls());
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006AE0 File Offset: 0x00004CE0
		public virtual void Connect(string host, int port)
		{
			SupportClass.Tokenizer tokenizer = new SupportClass.Tokenizer(host, " ");
			string text = null;
			while (tokenizer.HasMoreTokens())
			{
				try
				{
					int num = port;
					text = tokenizer.NextToken();
					int num2 = text.IndexOf(':');
					if (num2 != -1 && num2 + 1 != text.Length)
					{
						try
						{
							num = int.Parse(text.Substring(num2 + 1));
							text = text.Substring(0, num2);
						}
						catch (Exception)
						{
							throw new ArgumentException("INVALID_ADDRESS");
						}
					}
					this.conn = this.conn.destroyClone(true);
					this.conn.connect(text, num);
					break;
				}
				catch (LdapException ex)
				{
					if (!tokenizer.HasMoreTokens())
					{
						throw ex;
					}
				}
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006B9C File Offset: 0x00004D9C
		public virtual void Delete(string dn)
		{
			this.Delete(dn, this.defSearchCons);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006BAC File Offset: 0x00004DAC
		public virtual void Delete(string dn, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Delete(dn, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006C14 File Offset: 0x00004E14
		public virtual LdapResponseQueue Delete(string dn, LdapResponseQueue queue)
		{
			return this.Delete(dn, queue, this.defSearchCons);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006C24 File Offset: 0x00004E24
		public virtual LdapResponseQueue Delete(string dn, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (dn == null)
			{
				throw new ArgumentException("DN_PARAM_ERROR");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = new LdapDeleteRequest(dn, cons.getControls());
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00006C66 File Offset: 0x00004E66
		public virtual void Disconnect()
		{
			this.Disconnect(this.defSearchCons, true);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006C75 File Offset: 0x00004E75
		public virtual void Disconnect(LdapConstraints cons)
		{
			this.Disconnect(cons, true);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006C7F File Offset: 0x00004E7F
		private void Disconnect(LdapConstraints cons, bool how)
		{
			this.conn = this.conn.destroyClone(how);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006C93 File Offset: 0x00004E93
		public virtual LdapExtendedResponse ExtendedOperation(LdapExtendedOperation op)
		{
			return this.ExtendedOperation(op, this.defSearchCons);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public virtual LdapExtendedResponse ExtendedOperation(LdapExtendedOperation op, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.ExtendedOperation(op, cons, null);
			LdapExtendedResponse ldapExtendedResponse = (LdapExtendedResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapExtendedResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapExtendedResponse);
			return ldapExtendedResponse;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006D0C File Offset: 0x00004F0C
		public virtual LdapResponseQueue ExtendedOperation(LdapExtendedOperation op, LdapResponseQueue queue)
		{
			return this.ExtendedOperation(op, this.defSearchCons, queue);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006D1C File Offset: 0x00004F1C
		public virtual LdapResponseQueue ExtendedOperation(LdapExtendedOperation op, LdapConstraints cons, LdapResponseQueue queue)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = this.MakeExtendedOperation(op, cons);
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006D4C File Offset: 0x00004F4C
		protected internal virtual LdapMessage MakeExtendedOperation(LdapExtendedOperation op, LdapConstraints cons)
		{
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			if (op.getID() == null)
			{
				throw new ArgumentException("OP_PARAM_ERROR");
			}
			return new LdapExtendedRequest(op, cons.getControls());
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006D78 File Offset: 0x00004F78
		public virtual void Modify(string dn, LdapModification mod)
		{
			this.Modify(dn, mod, this.defSearchCons);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006D88 File Offset: 0x00004F88
		public virtual void Modify(string dn, LdapModification mod, LdapConstraints cons)
		{
			this.Modify(dn, new LdapModification[] { mod }, cons);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006DA9 File Offset: 0x00004FA9
		public virtual void Modify(string dn, LdapModification[] mods)
		{
			this.Modify(dn, mods, this.defSearchCons);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006DBC File Offset: 0x00004FBC
		public virtual void Modify(string dn, LdapModification[] mods, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Modify(dn, mods, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006E24 File Offset: 0x00005024
		public virtual LdapResponseQueue Modify(string dn, LdapModification mod, LdapResponseQueue queue)
		{
			return this.Modify(dn, mod, queue, this.defSearchCons);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006E38 File Offset: 0x00005038
		public virtual LdapResponseQueue Modify(string dn, LdapModification mod, LdapResponseQueue queue, LdapConstraints cons)
		{
			return this.Modify(dn, new LdapModification[] { mod }, queue, cons);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006E5B File Offset: 0x0000505B
		public virtual LdapResponseQueue Modify(string dn, LdapModification[] mods, LdapResponseQueue queue)
		{
			return this.Modify(dn, mods, queue, this.defSearchCons);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006E6C File Offset: 0x0000506C
		public virtual LdapResponseQueue Modify(string dn, LdapModification[] mods, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (dn == null)
			{
				throw new ArgumentException("DN_PARAM_ERROR");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = new LdapModifyRequest(dn, mods, cons.getControls());
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006EB2 File Offset: 0x000050B2
		public virtual LdapEntry Read(string dn)
		{
			return this.Read(dn, this.defSearchCons);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006EC1 File Offset: 0x000050C1
		public virtual LdapEntry Read(string dn, LdapSearchConstraints cons)
		{
			return this.Read(dn, null, cons);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006ECC File Offset: 0x000050CC
		public virtual LdapEntry Read(string dn, string[] attrs)
		{
			return this.Read(dn, attrs, this.defSearchCons);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006EDC File Offset: 0x000050DC
		public virtual LdapEntry Read(string dn, string[] attrs, LdapSearchConstraints cons)
		{
			LdapSearchResults ldapSearchResults = this.Search(dn, 0, null, attrs, false, cons);
			LdapEntry ldapEntry = null;
			if (ldapSearchResults.hasMore())
			{
				ldapEntry = ldapSearchResults.next();
				if (ldapSearchResults.hasMore())
				{
					throw new LdapLocalException("READ_MULTIPLE", 101);
				}
			}
			return ldapEntry;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006F20 File Offset: 0x00005120
		public static LdapEntry Read(LdapUrl toGet)
		{
			LdapConnection ldapConnection = new LdapConnection();
			ldapConnection.Connect(toGet.Host, toGet.Port);
			LdapEntry ldapEntry = ldapConnection.Read(toGet.getDN(), toGet.AttributeArray);
			ldapConnection.Disconnect();
			return ldapEntry;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006F60 File Offset: 0x00005160
		public static LdapEntry Read(LdapUrl toGet, LdapSearchConstraints cons)
		{
			LdapConnection ldapConnection = new LdapConnection();
			ldapConnection.Connect(toGet.Host, toGet.Port);
			LdapEntry ldapEntry = ldapConnection.Read(toGet.getDN(), toGet.AttributeArray, cons);
			ldapConnection.Disconnect();
			return ldapEntry;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006F9E File Offset: 0x0000519E
		public virtual void Rename(string dn, string newRdn, bool deleteOldRdn)
		{
			this.Rename(dn, newRdn, deleteOldRdn, this.defSearchCons);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006FAF File Offset: 0x000051AF
		public virtual void Rename(string dn, string newRdn, bool deleteOldRdn, LdapConstraints cons)
		{
			this.Rename(dn, newRdn, null, deleteOldRdn, cons);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006FBD File Offset: 0x000051BD
		public virtual void Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn)
		{
			this.Rename(dn, newRdn, newParentdn, deleteOldRdn, this.defSearchCons);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006FD0 File Offset: 0x000051D0
		public virtual void Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapConstraints cons)
		{
			LdapResponseQueue ldapResponseQueue = this.Rename(dn, newRdn, newParentdn, deleteOldRdn, null, cons);
			LdapResponse ldapResponse = (LdapResponse)ldapResponseQueue.getResponse();
			object obj = this.responseCtlSemaphore;
			lock (obj)
			{
				this.responseCtls = ldapResponse.Controls;
			}
			this.chkResultCode(ldapResponseQueue, cons, ldapResponse);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000703C File Offset: 0x0000523C
		public virtual LdapResponseQueue Rename(string dn, string newRdn, bool deleteOldRdn, LdapResponseQueue queue)
		{
			return this.Rename(dn, newRdn, deleteOldRdn, queue, this.defSearchCons);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000704F File Offset: 0x0000524F
		public virtual LdapResponseQueue Rename(string dn, string newRdn, bool deleteOldRdn, LdapResponseQueue queue, LdapConstraints cons)
		{
			return this.Rename(dn, newRdn, null, deleteOldRdn, queue, cons);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000705F File Offset: 0x0000525F
		public virtual LdapResponseQueue Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapResponseQueue queue)
		{
			return this.Rename(dn, newRdn, newParentdn, deleteOldRdn, queue, this.defSearchCons);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007074 File Offset: 0x00005274
		public virtual LdapResponseQueue Rename(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapResponseQueue queue, LdapConstraints cons)
		{
			if (dn == null || newRdn == null)
			{
				throw new ArgumentException("RDN_PARAM_ERROR");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = new LdapModifyDNRequest(dn, newRdn, newParentdn, deleteOldRdn, cons.getControls());
			return this.SendRequestToServer(ldapMessage, cons.TimeLimit, queue, null);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000070C1 File Offset: 0x000052C1
		public virtual LdapSearchResults Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly)
		{
			return this.Search(base_Renamed, scope, filter, attrs, typesOnly, this.defSearchCons);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000070D8 File Offset: 0x000052D8
		public virtual LdapSearchResults Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchConstraints cons)
		{
			LdapSearchQueue ldapSearchQueue = this.Search(base_Renamed, scope, filter, attrs, typesOnly, null, cons);
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			return new LdapSearchResults(this, ldapSearchQueue, cons);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000710B File Offset: 0x0000530B
		public virtual LdapSearchQueue Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchQueue queue)
		{
			return this.Search(base_Renamed, scope, filter, attrs, typesOnly, queue, this.defSearchCons);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007124 File Offset: 0x00005324
		public virtual LdapSearchQueue Search(string base_Renamed, int scope, string filter, string[] attrs, bool typesOnly, LdapSearchQueue queue, LdapSearchConstraints cons)
		{
			if (filter == null)
			{
				filter = "objectclass=*";
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessage ldapMessage = new LdapSearchRequest(base_Renamed, scope, filter, attrs, cons.Dereference, cons.MaxResults, cons.ServerTimeLimit, typesOnly, cons.getControls());
			LdapSearchQueue ldapSearchQueue = queue;
			MessageAgent messageAgent;
			if (ldapSearchQueue == null)
			{
				messageAgent = new MessageAgent();
				ldapSearchQueue = new LdapSearchQueue(messageAgent);
			}
			else
			{
				messageAgent = queue.MessageAgent;
			}
			try
			{
				messageAgent.sendMessage(this.conn, ldapMessage, cons.TimeLimit, ldapSearchQueue, null);
			}
			catch (LdapException ex)
			{
				throw ex;
			}
			return ldapSearchQueue;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000071B8 File Offset: 0x000053B8
		public static LdapSearchResults Search(LdapUrl toGet)
		{
			return LdapConnection.Search(toGet, null);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000071C4 File Offset: 0x000053C4
		public static LdapSearchResults Search(LdapUrl toGet, LdapSearchConstraints cons)
		{
			LdapConnection ldapConnection = new LdapConnection();
			ldapConnection.Connect(toGet.Host, toGet.Port);
			if (cons == null)
			{
				cons = ldapConnection.SearchConstraints;
			}
			else
			{
				cons = (LdapSearchConstraints)cons.Clone();
			}
			cons.BatchSize = 0;
			LdapSearchResults ldapSearchResults = ldapConnection.Search(toGet.getDN(), toGet.Scope, toGet.Filter, toGet.AttributeArray, false, cons);
			ldapConnection.Disconnect();
			return ldapSearchResults;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00007230 File Offset: 0x00005430
		public virtual LdapMessageQueue SendRequest(LdapMessage request, LdapMessageQueue queue)
		{
			return this.SendRequest(request, queue, null);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000723C File Offset: 0x0000543C
		public virtual LdapMessageQueue SendRequest(LdapMessage request, LdapMessageQueue queue, LdapConstraints cons)
		{
			if (!request.Request)
			{
				throw new SystemException("Object is not a request message");
			}
			if (cons == null)
			{
				cons = this.defSearchCons;
			}
			LdapMessageQueue ldapMessageQueue = queue;
			MessageAgent messageAgent;
			if (ldapMessageQueue == null)
			{
				messageAgent = new MessageAgent();
				if (request.Type == 3)
				{
					ldapMessageQueue = new LdapSearchQueue(messageAgent);
				}
				else
				{
					ldapMessageQueue = new LdapResponseQueue(messageAgent);
				}
			}
			else if (request.Type == 3)
			{
				messageAgent = queue.MessageAgent;
			}
			else
			{
				messageAgent = queue.MessageAgent;
			}
			try
			{
				messageAgent.sendMessage(this.conn, request, cons.TimeLimit, ldapMessageQueue, null);
			}
			catch (LdapException ex)
			{
				throw ex;
			}
			return ldapMessageQueue;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000072D0 File Offset: 0x000054D0
		private LdapResponseQueue SendRequestToServer(LdapMessage msg, int timeout, LdapResponseQueue queue, BindProperties bindProps)
		{
			MessageAgent messageAgent;
			if (queue == null)
			{
				messageAgent = new MessageAgent();
				queue = new LdapResponseQueue(messageAgent);
			}
			else
			{
				messageAgent = queue.MessageAgent;
			}
			messageAgent.sendMessage(this.conn, msg, timeout, queue, bindProps);
			return queue;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000730C File Offset: 0x0000550C
		private ReferralInfo getReferralConnection(string[] referrals)
		{
			ReferralInfo referralInfo = null;
			Exception ex = null;
			LdapConnection ldapConnection = null;
			LdapReferralHandler referralHandler = this.defSearchCons.getReferralHandler();
			int i = 0;
			if (referralHandler == null || referralHandler is LdapAuthHandler)
			{
				for (i = 0; i < referrals.Length; i++)
				{
					string text = null;
					sbyte[] array = null;
					try
					{
						ldapConnection = new LdapConnection();
						ldapConnection.Constraints = this.defSearchCons;
						LdapUrl ldapUrl = new LdapUrl(referrals[i]);
						ldapConnection.Connect(ldapUrl.Host, ldapUrl.Port);
						if (referralHandler != null && referralHandler is LdapAuthHandler)
						{
							LdapAuthProvider authProvider = ((LdapAuthHandler)referralHandler).getAuthProvider(ldapUrl.Host, ldapUrl.Port);
							text = authProvider.DN;
							array = authProvider.Password;
						}
						ldapConnection.Bind(3, text, array);
						ex = null;
						referralInfo = new ReferralInfo(ldapConnection, referrals, ldapUrl);
						ldapConnection.Connection.ActiveReferral = referralInfo;
						break;
					}
					catch (Exception ex2)
					{
						if (ldapConnection != null)
						{
							try
							{
								ldapConnection.Disconnect();
								ldapConnection = null;
								ex = ex2;
							}
							catch (LdapException)
							{
							}
						}
					}
				}
			}
			else
			{
				try
				{
					ldapConnection = ((LdapBindHandler)referralHandler).Bind(referrals, this);
					if (ldapConnection == null)
					{
						LdapReferralException ex3 = new LdapReferralException("REFERRAL_ERROR");
						ex3.setReferrals(referrals);
						throw ex3;
					}
					for (int j = 0; j < referrals.Length; j++)
					{
						try
						{
							LdapUrl ldapUrl2 = new LdapUrl(referrals[j]);
							if (ldapUrl2.Host.ToUpper().Equals(ldapConnection.Host.ToUpper()) && ldapUrl2.Port == ldapConnection.Port)
							{
								referralInfo = new ReferralInfo(ldapConnection, referrals, ldapUrl2);
								break;
							}
						}
						catch (Exception)
						{
						}
					}
					if (referralInfo == null)
					{
						ex = new LdapLocalException("REFERRAL_BIND_MATCH", 91);
					}
				}
				catch (Exception ex4)
				{
					ldapConnection = null;
					ex = ex4;
				}
			}
			if (ex == null)
			{
				return referralInfo;
			}
			if (ex is LdapReferralException)
			{
				throw (LdapReferralException)ex;
			}
			LdapException ex5;
			if (ex is LdapException)
			{
				ex5 = (LdapException)ex;
			}
			else
			{
				ex5 = new LdapLocalException("SERVER_CONNECT_ERROR", new object[] { this.conn.Host }, 91, ex);
			}
			LdapReferralException ex6 = new LdapReferralException("REFERRAL_ERROR", ex5);
			ex6.setReferrals(referrals);
			ex6.FailedReferral = referrals[referrals.Length - 1];
			throw ex6;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00007538 File Offset: 0x00005738
		private void chkResultCode(LdapMessageQueue queue, LdapConstraints cons, LdapResponse response)
		{
			if (response.ResultCode == 10 && cons.ReferralFollowing)
			{
				ArrayList arrayList = null;
				try
				{
					this.chaseReferral(queue, cons, response, response.Referrals, 0, false, null);
					return;
				}
				finally
				{
					this.releaseReferralConnections(arrayList);
				}
			}
			response.chkResultCode();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000758C File Offset: 0x0000578C
		internal virtual ArrayList chaseReferral(LdapMessageQueue queue, LdapConstraints cons, LdapMessage msg, string[] initialReferrals, int hopCount, bool searchReference, ArrayList connectionList)
		{
			ArrayList arrayList = connectionList;
			LdapConnection ldapConnection = null;
			ReferralInfo referralInfo = null;
			if (arrayList == null)
			{
				arrayList = new ArrayList(cons.HopLimit);
			}
			string[] array;
			LdapMessage ldapMessage;
			if (initialReferrals != null)
			{
				array = initialReferrals;
				ldapMessage = msg.RequestingMessage;
			}
			else
			{
				LdapResponse ldapResponse = (LdapResponse)queue.getResponse();
				if (ldapResponse.ResultCode != 10)
				{
					ldapResponse.chkResultCode();
					return arrayList;
				}
				array = ldapResponse.Referrals;
				ldapMessage = ldapResponse.RequestingMessage;
			}
			try
			{
				if (hopCount++ > cons.HopLimit)
				{
					throw new LdapLocalException("Max hops exceeded", 97);
				}
				referralInfo = this.getReferralConnection(array);
				ldapConnection = referralInfo.ReferralConnection;
				LdapUrl referralUrl = referralInfo.ReferralUrl;
				arrayList.Add(ldapConnection);
				LdapMessage ldapMessage2 = this.rebuildRequest(ldapMessage, referralUrl, searchReference);
				try
				{
					MessageAgent messageAgent;
					if (queue is LdapResponseQueue)
					{
						messageAgent = queue.MessageAgent;
					}
					else
					{
						messageAgent = queue.MessageAgent;
					}
					messageAgent.sendMessage(ldapConnection.Connection, ldapMessage2, this.defSearchCons.TimeLimit, queue, null);
				}
				catch (InterThreadException ex)
				{
					LdapReferralException ex2 = new LdapReferralException("REFERRAL_SEND", 91, null, ex);
					ex2.setReferrals(initialReferrals);
					ReferralInfo activeReferral = ldapConnection.Connection.ActiveReferral;
					ex2.FailedReferral = activeReferral.ReferralUrl.ToString();
					throw ex2;
				}
				if (initialReferrals != null)
				{
					return arrayList;
				}
				arrayList = this.chaseReferral(queue, cons, null, null, hopCount, false, arrayList);
			}
			catch (Exception ex3)
			{
				if (ex3 is LdapReferralException)
				{
					throw (LdapReferralException)ex3;
				}
				LdapReferralException ex4 = new LdapReferralException("REFERRAL_ERROR", ex3);
				ex4.setReferrals(array);
				if (referralInfo != null)
				{
					ex4.FailedReferral = referralInfo.ReferralUrl.ToString();
				}
				else
				{
					ex4.FailedReferral = array[array.Length - 1];
				}
				throw ex4;
			}
			return arrayList;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007740 File Offset: 0x00005940
		private LdapMessage rebuildRequest(LdapMessage msg, LdapUrl url, bool reference)
		{
			string dn = url.getDN();
			string text = null;
			int type = msg.Type;
			switch (type)
			{
			case 0:
			case 6:
			case 8:
			case 10:
			case 12:
			case 14:
				goto IL_008E;
			case 1:
			case 2:
			case 4:
			case 5:
			case 7:
			case 9:
			case 11:
			case 13:
			case 15:
			case 16:
				break;
			case 3:
				if (reference)
				{
					text = url.Filter;
					goto IL_008E;
				}
				goto IL_008E;
			default:
				if (type == 23)
				{
					goto IL_008E;
				}
				break;
			}
			throw new LdapLocalException("IMPROPER_REFERRAL", new object[] { msg.Type }, 82);
			IL_008E:
			return msg.Clone(dn, text, reference);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000077E4 File Offset: 0x000059E4
		internal virtual void releaseReferralConnections(ArrayList list)
		{
			if (list == null)
			{
				return;
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				try
				{
					LdapConnection ldapConnection = (LdapConnection)list[i];
					list.RemoveAt(i);
					ldapConnection.Disconnect();
				}
				catch (IndexOutOfRangeException)
				{
				}
				catch (LdapException)
				{
				}
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007848 File Offset: 0x00005A48
		public virtual LdapSchema FetchSchema(string schemaDN)
		{
			return new LdapSchema(this.Read(schemaDN, LdapSchema.schemaTypeNames));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000785B File Offset: 0x00005A5B
		public virtual string GetSchemaDN()
		{
			return this.GetSchemaDN("");
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007868 File Offset: 0x00005A68
		public virtual string GetSchemaDN(string dn)
		{
			string[] array = new string[] { "subschemaSubentry" };
			string[] stringValueArray = this.Read(dn, array).getAttribute(array[0]).StringValueArray;
			if (stringValueArray == null || stringValueArray.Length < 1)
			{
				throw new LdapLocalException("NO_SCHEMA", new object[] { dn }, 94);
			}
			if (stringValueArray.Length > 1)
			{
				throw new LdapLocalException("MULTIPLE_SCHEMA", new object[] { dn }, 19);
			}
			return stringValueArray[0];
		}

		// Token: 0x04000099 RID: 153
		private LdapSearchConstraints defSearchCons;

		// Token: 0x0400009A RID: 154
		private LdapControl[] responseCtls;

		// Token: 0x0400009B RID: 155
		private object responseCtlSemaphore;

		// Token: 0x0400009C RID: 156
		private Connection conn;

		// Token: 0x0400009D RID: 157
		private static object nameLock;

		// Token: 0x0400009E RID: 158
		private static int lConnNum;

		// Token: 0x0400009F RID: 159
		private string name;

		// Token: 0x040000A0 RID: 160
		public const int SCOPE_BASE = 0;

		// Token: 0x040000A1 RID: 161
		public const int SCOPE_ONE = 1;

		// Token: 0x040000A2 RID: 162
		public const int SCOPE_SUB = 2;

		// Token: 0x040000A3 RID: 163
		public const string NO_ATTRS = "1.1";

		// Token: 0x040000A4 RID: 164
		public const string ALL_USER_ATTRS = "*";

		// Token: 0x040000A5 RID: 165
		public const int Ldap_V3 = 3;

		// Token: 0x040000A6 RID: 166
		public const int DEFAULT_PORT = 389;

		// Token: 0x040000A7 RID: 167
		public const int DEFAULT_SSL_PORT = 636;

		// Token: 0x040000A8 RID: 168
		public const string Ldap_PROPERTY_SDK = "version.sdk";

		// Token: 0x040000A9 RID: 169
		public const string Ldap_PROPERTY_PROTOCOL = "version.protocol";

		// Token: 0x040000AA RID: 170
		public const string Ldap_PROPERTY_SECURITY = "version.security";

		// Token: 0x040000AB RID: 171
		public const string SERVER_SHUTDOWN_OID = "1.3.6.1.4.1.1466.20036";

		// Token: 0x040000AC RID: 172
		private const string START_TLS_OID = "1.3.6.1.4.1.1466.20037";
	}
}
