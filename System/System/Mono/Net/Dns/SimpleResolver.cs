using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace Mono.Net.Dns
{
	// Token: 0x02000095 RID: 149
	internal sealed class SimpleResolver : IDisposable
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00008F74 File Offset: 0x00007174
		public SimpleResolver()
		{
			this.queries = new Dictionary<int, SimpleResolverEventArgs>();
			this.receive_cb = new AsyncCallback(this.OnReceive);
			this.timeout_cb = new TimerCallback(this.OnTimeout);
			this.InitFromSystem();
			this.InitSocket();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008FC2 File Offset: 0x000071C2
		void IDisposable.Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				if (this.client != null)
				{
					this.client.Close();
					this.client = null;
				}
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00008FF0 File Offset: 0x000071F0
		private void GetLocalHost(SimpleResolverEventArgs args)
		{
			IPHostEntry iphostEntry = new IPHostEntry();
			iphostEntry.HostName = "localhost";
			iphostEntry.AddressList = new IPAddress[] { IPAddress.Loopback };
			iphostEntry.Aliases = SimpleResolver.EmptyStrings;
			args.ResolverError = ResolverError.NoError;
			args.HostEntry = iphostEntry;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000903C File Offset: 0x0000723C
		public bool GetHostAddressesAsync(SimpleResolverEventArgs args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			if (args.HostName == null)
			{
				throw new ArgumentNullException("args.HostName is null");
			}
			if (args.HostName.Length > 255)
			{
				throw new ArgumentException("args.HostName is too long");
			}
			args.Reset(ResolverAsyncOperation.GetHostAddresses);
			string hostName = args.HostName;
			if (hostName == "")
			{
				this.GetLocalHost(args);
				return false;
			}
			IPAddress ipaddress;
			if (IPAddress.TryParse(hostName, out ipaddress))
			{
				args.HostEntry = new IPHostEntry
				{
					HostName = hostName,
					Aliases = SimpleResolver.EmptyStrings,
					AddressList = new IPAddress[] { ipaddress }
				};
				return false;
			}
			this.SendAQuery(args, true);
			return true;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000090F0 File Offset: 0x000072F0
		private bool AddQuery(DnsQuery query, SimpleResolverEventArgs args)
		{
			Dictionary<int, SimpleResolverEventArgs> dictionary = this.queries;
			lock (dictionary)
			{
				if (this.queries.ContainsKey((int)query.Header.ID))
				{
					return false;
				}
				this.queries[(int)query.Header.ID] = args;
			}
			return true;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009160 File Offset: 0x00007360
		private static DnsQuery GetQuery(string host, DnsQType q, DnsQClass c)
		{
			return new DnsQuery(host, q, c);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000916A File Offset: 0x0000736A
		private void SendAQuery(SimpleResolverEventArgs args, bool add_it)
		{
			this.SendAQuery(args, args.HostName, add_it);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000917C File Offset: 0x0000737C
		private void SendAQuery(SimpleResolverEventArgs args, string host, bool add_it)
		{
			DnsQuery query = SimpleResolver.GetQuery(host, DnsQType.A, DnsQClass.Internet);
			this.SendQuery(args, query, add_it);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000919C File Offset: 0x0000739C
		private void SendQuery(SimpleResolverEventArgs args, DnsQuery query, bool add_it)
		{
			int num = 0;
			if (add_it)
			{
				for (;;)
				{
					query.Header.ID = (ushort)new Random().Next(1, 65534);
					if (num > 500)
					{
						break;
					}
					if (this.AddQuery(query, args))
					{
						goto Block_2;
					}
				}
				throw new InvalidOperationException("Too many pending queries (or really bad luck)");
				Block_2:
				args.QueryID = query.Header.ID;
			}
			else
			{
				query.Header.ID = args.QueryID;
			}
			if (args.Timer == null)
			{
				args.Timer = new Timer(this.timeout_cb, args, 5000, -1);
			}
			else
			{
				args.Timer.Change(5000, -1);
			}
			this.client.BeginSend(query.Packet, 0, query.Length, SocketFlags.None, null, null);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000925B File Offset: 0x0000745B
		private byte[] GetFreshBuffer()
		{
			return new byte[512];
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00002FA0 File Offset: 0x000011A0
		private void FreeBuffer(byte[] buffer)
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009268 File Offset: 0x00007468
		private void InitSocket()
		{
			this.client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			this.client.Blocking = true;
			this.client.Bind(new IPEndPoint(IPAddress.Any, 0));
			this.client.Connect(this.endpoints[0]);
			this.BeginReceive();
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000092C0 File Offset: 0x000074C0
		private void BeginReceive()
		{
			byte[] freshBuffer = this.GetFreshBuffer();
			this.client.BeginReceive(freshBuffer, 0, freshBuffer.Length, SocketFlags.None, this.receive_cb, freshBuffer);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000092F0 File Offset: 0x000074F0
		private void OnTimeout(object obj)
		{
			SimpleResolverEventArgs simpleResolverEventArgs = (SimpleResolverEventArgs)obj;
			Dictionary<int, SimpleResolverEventArgs> dictionary = this.queries;
			lock (dictionary)
			{
				SimpleResolverEventArgs simpleResolverEventArgs2;
				if (this.queries.TryGetValue((int)simpleResolverEventArgs.QueryID, out simpleResolverEventArgs2))
				{
					if (simpleResolverEventArgs != simpleResolverEventArgs2)
					{
						throw new Exception("Should not happen: args != args2");
					}
					SimpleResolverEventArgs simpleResolverEventArgs3 = simpleResolverEventArgs;
					simpleResolverEventArgs3.Retries += 1;
					if (simpleResolverEventArgs.Retries > 1)
					{
						simpleResolverEventArgs.ResolverError = ResolverError.Timeout;
						simpleResolverEventArgs.OnCompleted(this);
					}
					else
					{
						this.SendAQuery(simpleResolverEventArgs, false);
					}
				}
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009388 File Offset: 0x00007588
		private void OnReceive(IAsyncResult ares)
		{
			if (this.disposed)
			{
				return;
			}
			int num = 0;
			EndPoint remoteEndPoint = this.client.RemoteEndPoint;
			try
			{
				num = this.client.EndReceive(ares);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex);
			}
			this.BeginReceive();
			byte[] array = (byte[])ares.AsyncState;
			if (num > 12)
			{
				DnsResponse dnsResponse = new DnsResponse(array, num);
				int id = (int)dnsResponse.Header.ID;
				SimpleResolverEventArgs simpleResolverEventArgs = null;
				Dictionary<int, SimpleResolverEventArgs> dictionary = this.queries;
				lock (dictionary)
				{
					if (this.queries.TryGetValue(id, out simpleResolverEventArgs))
					{
						this.queries.Remove(id);
					}
				}
				if (simpleResolverEventArgs != null)
				{
					Timer timer = simpleResolverEventArgs.Timer;
					if (timer != null)
					{
						timer.Change(-1, -1);
					}
					try
					{
						this.ProcessResponse(simpleResolverEventArgs, dnsResponse, remoteEndPoint);
					}
					catch (Exception ex2)
					{
						simpleResolverEventArgs.ResolverError = (ResolverError)(-1);
						simpleResolverEventArgs.ErrorMessage = ex2.Message;
					}
					IPHostEntry hostEntry = simpleResolverEventArgs.HostEntry;
					if (simpleResolverEventArgs.ResolverError != ResolverError.NoError && simpleResolverEventArgs.PTRAddress != null && hostEntry != null && hostEntry.HostName != null)
					{
						simpleResolverEventArgs.PTRAddress = null;
						this.SendAQuery(simpleResolverEventArgs, hostEntry.HostName, true);
						simpleResolverEventArgs.Timer.Change(5000, -1);
					}
					else
					{
						simpleResolverEventArgs.OnCompleted(this);
					}
				}
			}
			this.FreeBuffer(array);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009510 File Offset: 0x00007710
		private void ProcessResponse(SimpleResolverEventArgs args, DnsResponse response, EndPoint server_ep)
		{
			DnsRCode rcode = response.Header.RCode;
			if (rcode != DnsRCode.NoError)
			{
				if (args.PTRAddress != null)
				{
					return;
				}
				args.ResolverError = (ResolverError)rcode;
				return;
			}
			else
			{
				if (((IPEndPoint)server_ep).Port != 53)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "Port";
					return;
				}
				DnsHeader header = response.Header;
				if (!header.IsQuery)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "IsQuery";
					return;
				}
				if (header.QuestionCount > 1)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QuestionCount";
					return;
				}
				ReadOnlyCollection<DnsQuestion> questions = response.GetQuestions();
				if (questions.Count != 1)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QuestionCount 2";
					return;
				}
				DnsQuestion dnsQuestion = questions[0];
				DnsQType type = dnsQuestion.Type;
				if (type != DnsQType.A && type != DnsQType.AAAA && type != DnsQType.PTR)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QType " + dnsQuestion.Type.ToString();
					return;
				}
				if (dnsQuestion.Class != DnsQClass.Internet)
				{
					args.ResolverError = ResolverError.ResponseHeaderError;
					args.ErrorMessage = "QClass " + dnsQuestion.Class.ToString();
					return;
				}
				ReadOnlyCollection<DnsResourceRecord> answers = response.GetAnswers();
				if (answers.Count != 0)
				{
					List<string> list = null;
					List<IPAddress> list2 = null;
					foreach (DnsResourceRecord dnsResourceRecord in answers)
					{
						if (dnsResourceRecord.Class == DnsClass.Internet)
						{
							if (dnsResourceRecord.Type == DnsType.A || dnsResourceRecord.Type == DnsType.AAAA)
							{
								if (list2 == null)
								{
									list2 = new List<IPAddress>();
								}
								list2.Add(((DnsResourceRecordIPAddress)dnsResourceRecord).Address);
							}
							else if (dnsResourceRecord.Type == DnsType.CNAME)
							{
								if (list == null)
								{
									list = new List<string>();
								}
								list.Add(((DnsResourceRecordCName)dnsResourceRecord).CName);
							}
							else if (dnsResourceRecord.Type == DnsType.PTR)
							{
								args.HostEntry.HostName = ((DnsResourceRecordPTR)dnsResourceRecord).DName;
								args.HostEntry.Aliases = ((list == null) ? SimpleResolver.EmptyStrings : list.ToArray());
								args.HostEntry.AddressList = SimpleResolver.EmptyAddresses;
								return;
							}
						}
					}
					IPHostEntry iphostEntry = args.HostEntry ?? new IPHostEntry();
					if (iphostEntry.HostName == null && list != null && list.Count > 0)
					{
						iphostEntry.HostName = list[0];
						list.RemoveAt(0);
					}
					iphostEntry.Aliases = ((list == null) ? SimpleResolver.EmptyStrings : list.ToArray());
					iphostEntry.AddressList = ((list2 == null) ? SimpleResolver.EmptyAddresses : list2.ToArray());
					args.HostEntry = iphostEntry;
					if ((dnsQuestion.Type == DnsQType.A || dnsQuestion.Type == DnsQType.AAAA) && iphostEntry.AddressList == SimpleResolver.EmptyAddresses)
					{
						args.ResolverError = ResolverError.NameError;
						args.ErrorMessage = "No addresses in response";
						return;
					}
					if (dnsQuestion.Type == DnsQType.PTR && iphostEntry.HostName == null)
					{
						args.ResolverError = ResolverError.NameError;
						args.ErrorMessage = "No PTR in response";
					}
					return;
				}
				if (args.PTRAddress != null)
				{
					return;
				}
				args.ResolverError = ResolverError.NameError;
				args.ErrorMessage = "NoAnswers";
				return;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000984C File Offset: 0x00007A4C
		private void InitFromSystem()
		{
			List<IPEndPoint> list = new List<IPEndPoint>();
			foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (NetworkInterfaceType.Loopback != networkInterface.NetworkInterfaceType)
				{
					foreach (IPAddress ipaddress in networkInterface.GetIPProperties().DnsAddresses)
					{
						if (AddressFamily.InterNetworkV6 != ipaddress.AddressFamily)
						{
							IPEndPoint ipendPoint = new IPEndPoint(ipaddress, 53);
							if (!list.Contains(ipendPoint))
							{
								list.Add(ipendPoint);
							}
						}
					}
				}
			}
			this.endpoints = list.ToArray();
		}

		// Token: 0x04000259 RID: 601
		private static string[] EmptyStrings = new string[0];

		// Token: 0x0400025A RID: 602
		private static IPAddress[] EmptyAddresses = new IPAddress[0];

		// Token: 0x0400025B RID: 603
		private IPEndPoint[] endpoints;

		// Token: 0x0400025C RID: 604
		private Socket client;

		// Token: 0x0400025D RID: 605
		private Dictionary<int, SimpleResolverEventArgs> queries;

		// Token: 0x0400025E RID: 606
		private AsyncCallback receive_cb;

		// Token: 0x0400025F RID: 607
		private TimerCallback timeout_cb;

		// Token: 0x04000260 RID: 608
		private bool disposed;
	}
}
