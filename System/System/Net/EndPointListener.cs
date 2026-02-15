using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000406 RID: 1030
	internal sealed class EndPointListener
	{
		// Token: 0x0600198A RID: 6538 RVA: 0x0006D328 File Offset: 0x0006B528
		public EndPointListener(HttpListener listener, IPAddress addr, int port, bool secure)
		{
			this.listener = listener;
			if (secure)
			{
				this.secure = secure;
				this.cert = listener.LoadCertificateAndKey(addr, port);
			}
			this.endpoint = new IPEndPoint(addr, port);
			this.sock = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			this.sock.Bind(this.endpoint);
			this.sock.Listen(500);
			SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
			socketAsyncEventArgs.UserToken = this;
			socketAsyncEventArgs.Completed += EndPointListener.OnAccept;
			Socket socket = null;
			EndPointListener.Accept(this.sock, socketAsyncEventArgs, ref socket);
			this.prefixes = new Hashtable();
			this.unregistered = new Dictionary<HttpConnection, HttpConnection>();
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600198B RID: 6539 RVA: 0x0006D3E2 File Offset: 0x0006B5E2
		internal HttpListener Listener
		{
			get
			{
				return this.listener;
			}
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0006D3EC File Offset: 0x0006B5EC
		private static void Accept(Socket socket, SocketAsyncEventArgs e, ref Socket accepted)
		{
			e.AcceptSocket = null;
			bool flag;
			try
			{
				flag = socket.AcceptAsync(e);
			}
			catch
			{
				if (accepted != null)
				{
					try
					{
						accepted.Close();
					}
					catch
					{
					}
					accepted = null;
				}
				return;
			}
			if (!flag)
			{
				EndPointListener.ProcessAccept(e);
			}
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0006D448 File Offset: 0x0006B648
		private static void ProcessAccept(SocketAsyncEventArgs args)
		{
			Socket socket = null;
			if (args.SocketError == SocketError.Success)
			{
				socket = args.AcceptSocket;
			}
			EndPointListener endPointListener = (EndPointListener)args.UserToken;
			EndPointListener.Accept(endPointListener.sock, args, ref socket);
			if (socket == null)
			{
				return;
			}
			if (endPointListener.secure && endPointListener.cert == null)
			{
				socket.Close();
				return;
			}
			HttpConnection httpConnection;
			try
			{
				httpConnection = new HttpConnection(socket, endPointListener, endPointListener.secure, endPointListener.cert);
			}
			catch
			{
				socket.Close();
				return;
			}
			Dictionary<HttpConnection, HttpConnection> dictionary = endPointListener.unregistered;
			lock (dictionary)
			{
				endPointListener.unregistered[httpConnection] = httpConnection;
			}
			httpConnection.BeginReadRequest();
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0006D50C File Offset: 0x0006B70C
		private static void OnAccept(object sender, SocketAsyncEventArgs e)
		{
			EndPointListener.ProcessAccept(e);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0006D514 File Offset: 0x0006B714
		internal void RemoveConnection(HttpConnection conn)
		{
			Dictionary<HttpConnection, HttpConnection> dictionary = this.unregistered;
			lock (dictionary)
			{
				this.unregistered.Remove(conn);
			}
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0006D55C File Offset: 0x0006B75C
		public bool BindContext(HttpListenerContext context)
		{
			HttpListenerRequest request = context.Request;
			ListenerPrefix listenerPrefix;
			HttpListener httpListener = this.SearchListener(request.Url, out listenerPrefix);
			if (httpListener == null)
			{
				return false;
			}
			context.Listener = httpListener;
			context.Connection.Prefix = listenerPrefix;
			return true;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0006D598 File Offset: 0x0006B798
		public void UnbindContext(HttpListenerContext context)
		{
			if (context == null || context.Request == null)
			{
				return;
			}
			context.Listener.UnregisterContext(context);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0006D5B4 File Offset: 0x0006B7B4
		private HttpListener SearchListener(Uri uri, out ListenerPrefix prefix)
		{
			prefix = null;
			if (uri == null)
			{
				return null;
			}
			string host = uri.Host;
			int port = uri.Port;
			string text = WebUtility.UrlDecode(uri.AbsolutePath);
			string text2 = ((text[text.Length - 1] == '/') ? text : (text + "/"));
			HttpListener httpListener = null;
			int num = -1;
			if (host != null && host != "")
			{
				Hashtable hashtable = this.prefixes;
				foreach (object obj in hashtable.Keys)
				{
					ListenerPrefix listenerPrefix = (ListenerPrefix)obj;
					string path = listenerPrefix.Path;
					if (path.Length >= num && !(listenerPrefix.Host != host) && listenerPrefix.Port == port && (text.StartsWith(path) || text2.StartsWith(path)))
					{
						num = path.Length;
						httpListener = (HttpListener)hashtable[listenerPrefix];
						prefix = listenerPrefix;
					}
				}
				if (num != -1)
				{
					return httpListener;
				}
			}
			ArrayList arrayList = this.unhandled;
			httpListener = this.MatchFromList(host, text, arrayList, out prefix);
			if (text != text2 && httpListener == null)
			{
				httpListener = this.MatchFromList(host, text2, arrayList, out prefix);
			}
			if (httpListener != null)
			{
				return httpListener;
			}
			arrayList = this.all;
			httpListener = this.MatchFromList(host, text, arrayList, out prefix);
			if (text != text2 && httpListener == null)
			{
				httpListener = this.MatchFromList(host, text2, arrayList, out prefix);
			}
			if (httpListener != null)
			{
				return httpListener;
			}
			return null;
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0006D750 File Offset: 0x0006B950
		private HttpListener MatchFromList(string host, string path, ArrayList list, out ListenerPrefix prefix)
		{
			prefix = null;
			if (list == null)
			{
				return null;
			}
			HttpListener httpListener = null;
			int num = -1;
			foreach (object obj in list)
			{
				ListenerPrefix listenerPrefix = (ListenerPrefix)obj;
				string path2 = listenerPrefix.Path;
				if (path2.Length >= num && path.StartsWith(path2))
				{
					num = path2.Length;
					httpListener = listenerPrefix.Listener;
					prefix = listenerPrefix;
				}
			}
			return httpListener;
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0006D7E0 File Offset: 0x0006B9E0
		private void AddSpecial(ArrayList coll, ListenerPrefix prefix)
		{
			if (coll == null)
			{
				return;
			}
			using (IEnumerator enumerator = coll.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((ListenerPrefix)enumerator.Current).Path == prefix.Path)
					{
						throw new HttpListenerException(400, "Prefix already in use.");
					}
				}
			}
			coll.Add(prefix);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0006D85C File Offset: 0x0006BA5C
		private bool RemoveSpecial(ArrayList coll, ListenerPrefix prefix)
		{
			if (coll == null)
			{
				return false;
			}
			int count = coll.Count;
			for (int i = 0; i < count; i++)
			{
				if (((ListenerPrefix)coll[i]).Path == prefix.Path)
				{
					coll.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0006D8AC File Offset: 0x0006BAAC
		private void CheckIfRemove()
		{
			if (this.prefixes.Count > 0)
			{
				return;
			}
			ArrayList arrayList = this.unhandled;
			if (arrayList != null && arrayList.Count > 0)
			{
				return;
			}
			arrayList = this.all;
			if (arrayList != null && arrayList.Count > 0)
			{
				return;
			}
			EndPointManager.RemoveEndPoint(this, this.endpoint);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0006D8FC File Offset: 0x0006BAFC
		public void Close()
		{
			this.sock.Close();
			Dictionary<HttpConnection, HttpConnection> dictionary = this.unregistered;
			lock (dictionary)
			{
				foreach (HttpConnection httpConnection in new List<HttpConnection>(this.unregistered.Keys))
				{
					httpConnection.Close(true);
				}
				this.unregistered.Clear();
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0006D998 File Offset: 0x0006BB98
		public void AddPrefix(ListenerPrefix prefix, HttpListener listener)
		{
			if (prefix.Host == "*")
			{
				ArrayList arrayList;
				ArrayList arrayList2;
				do
				{
					arrayList = this.unhandled;
					arrayList2 = ((arrayList != null) ? ((ArrayList)arrayList.Clone()) : new ArrayList());
					prefix.Listener = listener;
					this.AddSpecial(arrayList2, prefix);
				}
				while (Interlocked.CompareExchange<ArrayList>(ref this.unhandled, arrayList2, arrayList) != arrayList);
				return;
			}
			if (prefix.Host == "+")
			{
				ArrayList arrayList;
				ArrayList arrayList2;
				do
				{
					arrayList = this.all;
					arrayList2 = ((arrayList != null) ? ((ArrayList)arrayList.Clone()) : new ArrayList());
					prefix.Listener = listener;
					this.AddSpecial(arrayList2, prefix);
				}
				while (Interlocked.CompareExchange<ArrayList>(ref this.all, arrayList2, arrayList) != arrayList);
				return;
			}
			Hashtable hashtable;
			for (;;)
			{
				hashtable = this.prefixes;
				if (hashtable.ContainsKey(prefix))
				{
					break;
				}
				Hashtable hashtable2 = (Hashtable)hashtable.Clone();
				hashtable2[prefix] = listener;
				if (Interlocked.CompareExchange<Hashtable>(ref this.prefixes, hashtable2, hashtable) == hashtable)
				{
					return;
				}
			}
			if ((HttpListener)hashtable[prefix] != listener)
			{
				throw new HttpListenerException(400, "There's another listener for " + ((prefix != null) ? prefix.ToString() : null));
			}
			return;
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0006DAAC File Offset: 0x0006BCAC
		public void RemovePrefix(ListenerPrefix prefix, HttpListener listener)
		{
			if (prefix.Host == "*")
			{
				ArrayList arrayList;
				ArrayList arrayList2;
				do
				{
					arrayList = this.unhandled;
					arrayList2 = ((arrayList != null) ? ((ArrayList)arrayList.Clone()) : new ArrayList());
				}
				while (this.RemoveSpecial(arrayList2, prefix) && Interlocked.CompareExchange<ArrayList>(ref this.unhandled, arrayList2, arrayList) != arrayList);
				this.CheckIfRemove();
				return;
			}
			if (prefix.Host == "+")
			{
				ArrayList arrayList;
				ArrayList arrayList2;
				do
				{
					arrayList = this.all;
					arrayList2 = ((arrayList != null) ? ((ArrayList)arrayList.Clone()) : new ArrayList());
				}
				while (this.RemoveSpecial(arrayList2, prefix) && Interlocked.CompareExchange<ArrayList>(ref this.all, arrayList2, arrayList) != arrayList);
				this.CheckIfRemove();
				return;
			}
			Hashtable hashtable;
			Hashtable hashtable2;
			do
			{
				hashtable = this.prefixes;
				if (!hashtable.ContainsKey(prefix))
				{
					break;
				}
				hashtable2 = (Hashtable)hashtable.Clone();
				hashtable2.Remove(prefix);
			}
			while (Interlocked.CompareExchange<Hashtable>(ref this.prefixes, hashtable2, hashtable) != hashtable);
			this.CheckIfRemove();
		}

		// Token: 0x0400103B RID: 4155
		private HttpListener listener;

		// Token: 0x0400103C RID: 4156
		private IPEndPoint endpoint;

		// Token: 0x0400103D RID: 4157
		private Socket sock;

		// Token: 0x0400103E RID: 4158
		private Hashtable prefixes;

		// Token: 0x0400103F RID: 4159
		private ArrayList unhandled;

		// Token: 0x04001040 RID: 4160
		private ArrayList all;

		// Token: 0x04001041 RID: 4161
		private X509Certificate cert;

		// Token: 0x04001042 RID: 4162
		private bool secure;

		// Token: 0x04001043 RID: 4163
		private Dictionary<HttpConnection, HttpConnection> unregistered;
	}
}
