using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x0200042D RID: 1069
	internal class ServicePointScheduler
	{
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00075642 File Offset: 0x00073842
		// (set) Token: 0x06001B0A RID: 6922 RVA: 0x0007564A File Offset: 0x0007384A
		private ServicePoint ServicePoint { get; set; }

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x00075653 File Offset: 0x00073853
		public int MaxIdleTime
		{
			get
			{
				return this.maxIdleTime;
			}
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0007565C File Offset: 0x0007385C
		public ServicePointScheduler(ServicePoint servicePoint, int connectionLimit, int maxIdleTime)
		{
			this.ServicePoint = servicePoint;
			this.connectionLimit = connectionLimit;
			this.maxIdleTime = maxIdleTime;
			this.schedulerEvent = new ServicePointScheduler.AsyncManualResetEvent(false);
			this.defaultGroup = new ServicePointScheduler.ConnectionGroup(this, string.Empty);
			this.operations = new LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>>();
			this.idleConnections = new LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>>();
			this.idleSince = DateTime.UtcNow;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x000756E0 File Offset: 0x000738E0
		public void Run()
		{
			if (Interlocked.CompareExchange(ref this.running, 1, 0) == 0)
			{
				Task.Run(() => this.RunScheduler());
			}
			this.schedulerEvent.Set();
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x00075710 File Offset: 0x00073910
		private async Task RunScheduler()
		{
			this.idleSince = DateTime.UtcNow + TimeSpan.FromDays(3650.0);
			for (;;)
			{
				List<Task> taskList = new List<Task>();
				bool finalCleanup = false;
				ServicePoint servicePoint = this.ServicePoint;
				ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>[] operationArray;
				ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>[] idleArray;
				Task<bool> schedulerTask;
				lock (servicePoint)
				{
					this.Cleanup();
					operationArray = new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>[this.operations.Count];
					this.operations.CopyTo(operationArray, 0);
					idleArray = new ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>[this.idleConnections.Count];
					this.idleConnections.CopyTo(idleArray, 0);
					schedulerTask = this.schedulerEvent.WaitAsync(this.maxIdleTime);
					taskList.Add(schedulerTask);
					if (this.groups == null && this.defaultGroup.IsEmpty() && this.operations.Count == 0 && this.idleConnections.Count == 0)
					{
						this.idleSince = DateTime.UtcNow;
						finalCleanup = true;
					}
					else
					{
						foreach (ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation> valueTuple in operationArray)
						{
							taskList.Add(valueTuple.Item2.Finished.Task);
						}
						foreach (ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task> valueTuple2 in idleArray)
						{
							taskList.Add(valueTuple2.Item3);
						}
					}
				}
				Task task = await Task.WhenAny(taskList).ConfigureAwait(false);
				servicePoint = this.ServicePoint;
				lock (servicePoint)
				{
					bool flag2 = false;
					if (finalCleanup)
					{
						if (!schedulerTask.Result)
						{
							this.FinalCleanup();
							break;
						}
						flag2 = true;
					}
					else if (task == taskList[0])
					{
						flag2 = true;
					}
					foreach (ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation> valueTuple3 in operationArray)
					{
						if (valueTuple3.Item2.Finished.CurrentResult != null)
						{
							this.operations.Remove(valueTuple3);
							flag2 |= this.OperationCompleted(valueTuple3.Item1, valueTuple3.Item2);
						}
					}
					if (flag2)
					{
						this.RunSchedulerIteration();
					}
					int num = -1;
					for (int k = 0; k < idleArray.Length; k++)
					{
						if (task == taskList[k + 1 + operationArray.Length])
						{
							num = k;
							break;
						}
					}
					if (num >= 0)
					{
						ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task> valueTuple4 = idleArray[num];
						this.idleConnections.Remove(valueTuple4);
						this.CloseIdleConnection(valueTuple4.Item1, valueTuple4.Item2);
					}
				}
				operationArray = null;
				idleArray = null;
				taskList = null;
				schedulerTask = null;
			}
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00075754 File Offset: 0x00073954
		private void Cleanup()
		{
			if (this.groups != null)
			{
				string[] array = new string[this.groups.Count];
				this.groups.Keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					if (this.groups.ContainsKey(text) && this.groups[text].IsEmpty())
					{
						this.groups.Remove(text);
					}
				}
				if (this.groups.Count == 0)
				{
					this.groups = null;
				}
			}
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000757E4 File Offset: 0x000739E4
		private void RunSchedulerIteration()
		{
			this.schedulerEvent.Reset();
			bool flag;
			do
			{
				flag = this.SchedulerIteration(this.defaultGroup);
				if (this.groups != null)
				{
					foreach (KeyValuePair<string, ServicePointScheduler.ConnectionGroup> keyValuePair in this.groups)
					{
						flag |= this.SchedulerIteration(keyValuePair.Value);
					}
				}
			}
			while (flag);
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x00075864 File Offset: 0x00073A64
		private bool OperationCompleted(ServicePointScheduler.ConnectionGroup group, WebOperation operation)
		{
			WebCompletionSource<ValueTuple<bool, WebOperation>>.Result currentResult = operation.Finished.CurrentResult;
			bool flag;
			WebOperation webOperation;
			if (!currentResult.Success)
			{
				flag = false;
				webOperation = null;
			}
			else
			{
				ValueTuple<bool, WebOperation> argument = currentResult.Argument;
				flag = argument.Item1;
				webOperation = argument.Item2;
			}
			if (!flag || !operation.Connection.Continue(webOperation))
			{
				group.RemoveConnection(operation.Connection);
				if (webOperation == null)
				{
					return true;
				}
				flag = false;
			}
			if (webOperation == null)
			{
				if (flag)
				{
					Task task = Task.Delay(this.MaxIdleTime);
					this.idleConnections.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>(group, operation.Connection, task));
				}
				return true;
			}
			this.operations.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>(group, webOperation));
			if (flag)
			{
				this.RemoveIdleConnection(operation.Connection);
				return false;
			}
			group.Cleanup();
			group.CreateOrReuseConnection(webOperation, true);
			return false;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x00075927 File Offset: 0x00073B27
		private void CloseIdleConnection(ServicePointScheduler.ConnectionGroup group, WebConnection connection)
		{
			group.RemoveConnection(connection);
			this.RemoveIdleConnection(connection);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x00075938 File Offset: 0x00073B38
		private bool SchedulerIteration(ServicePointScheduler.ConnectionGroup group)
		{
			group.Cleanup();
			WebOperation nextOperation = group.GetNextOperation();
			if (nextOperation == null)
			{
				return false;
			}
			WebConnection item = group.CreateOrReuseConnection(nextOperation, false).Item1;
			if (item == null)
			{
				return false;
			}
			this.operations.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>(group, nextOperation));
			this.RemoveIdleConnection(item);
			return true;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x00075988 File Offset: 0x00073B88
		private void RemoveOperation(WebOperation operation)
		{
			LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> linkedListNode = this.operations.First;
			while (linkedListNode != null)
			{
				LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> linkedListNode2 = linkedListNode;
				linkedListNode = linkedListNode.Next;
				if (linkedListNode2.Value.Item2 == operation)
				{
					this.operations.Remove(linkedListNode2);
				}
			}
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000759CC File Offset: 0x00073BCC
		private void RemoveIdleConnection(WebConnection connection)
		{
			LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> linkedListNode = this.idleConnections.First;
			while (linkedListNode != null)
			{
				LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> linkedListNode2 = linkedListNode;
				linkedListNode = linkedListNode.Next;
				if (linkedListNode2.Value.Item2 == connection)
				{
					this.idleConnections.Remove(linkedListNode2);
				}
			}
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00075A0D File Offset: 0x00073C0D
		private void FinalCleanup()
		{
			this.groups = null;
			this.operations = null;
			this.idleConnections = null;
			this.defaultGroup = null;
			this.ServicePoint.FreeServicePoint();
			ServicePointManager.RemoveServicePoint(this.ServicePoint);
			this.ServicePoint = null;
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00075A48 File Offset: 0x00073C48
		public void SendRequest(WebOperation operation, string groupName)
		{
			ServicePoint servicePoint = this.ServicePoint;
			lock (servicePoint)
			{
				this.GetConnectionGroup(groupName).EnqueueOperation(operation);
				this.Run();
			}
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00075A98 File Offset: 0x00073C98
		private ServicePointScheduler.ConnectionGroup GetConnectionGroup(string name)
		{
			ServicePoint servicePoint = this.ServicePoint;
			ServicePointScheduler.ConnectionGroup connectionGroup;
			lock (servicePoint)
			{
				if (string.IsNullOrEmpty(name))
				{
					connectionGroup = this.defaultGroup;
				}
				else
				{
					if (this.groups == null)
					{
						this.groups = new Dictionary<string, ServicePointScheduler.ConnectionGroup>();
					}
					ServicePointScheduler.ConnectionGroup connectionGroup2;
					if (this.groups.TryGetValue(name, out connectionGroup2))
					{
						connectionGroup = connectionGroup2;
					}
					else
					{
						connectionGroup2 = new ServicePointScheduler.ConnectionGroup(this, name);
						this.groups.Add(name, connectionGroup2);
						connectionGroup = connectionGroup2;
					}
				}
			}
			return connectionGroup;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00075B24 File Offset: 0x00073D24
		private void OnConnectionCreated(WebConnection connection)
		{
			Interlocked.Increment(ref this.currentConnections);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00075B32 File Offset: 0x00073D32
		private void OnConnectionClosed(WebConnection connection)
		{
			this.RemoveIdleConnection(connection);
			Interlocked.Decrement(ref this.currentConnections);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00075B48 File Offset: 0x00073D48
		public static async Task<bool> WaitAsync(Task workerTask, int millisecondTimeout)
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			bool flag;
			try
			{
				Task timeoutTask = Task.Delay(millisecondTimeout, cts.Token);
				ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.WhenAny(new Task[] { workerTask, timeoutTask }).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
				}
				flag = configuredTaskAwaiter.GetResult() != timeoutTask;
			}
			finally
			{
				cts.Cancel();
				cts.Dispose();
			}
			return flag;
		}

		// Token: 0x0400119F RID: 4511
		private int running;

		// Token: 0x040011A0 RID: 4512
		private int maxIdleTime = 100000;

		// Token: 0x040011A1 RID: 4513
		private ServicePointScheduler.AsyncManualResetEvent schedulerEvent;

		// Token: 0x040011A2 RID: 4514
		private ServicePointScheduler.ConnectionGroup defaultGroup;

		// Token: 0x040011A3 RID: 4515
		private Dictionary<string, ServicePointScheduler.ConnectionGroup> groups;

		// Token: 0x040011A4 RID: 4516
		private LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> operations;

		// Token: 0x040011A5 RID: 4517
		private LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> idleConnections;

		// Token: 0x040011A6 RID: 4518
		private int currentConnections;

		// Token: 0x040011A7 RID: 4519
		private int connectionLimit;

		// Token: 0x040011A8 RID: 4520
		private DateTime idleSince;

		// Token: 0x040011A9 RID: 4521
		private static int nextId;

		// Token: 0x040011AA RID: 4522
		public readonly int ID = ++ServicePointScheduler.nextId;

		// Token: 0x0200042E RID: 1070
		private class ConnectionGroup
		{
			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x06001B1D RID: 6941 RVA: 0x00075B9B File Offset: 0x00073D9B
			public ServicePointScheduler Scheduler { get; }

			// Token: 0x06001B1E RID: 6942 RVA: 0x00075BA3 File Offset: 0x00073DA3
			public ConnectionGroup(ServicePointScheduler scheduler, string name)
			{
				this.Scheduler = scheduler;
				this.<Name>k__BackingField = name;
				this.connections = new LinkedList<WebConnection>();
				this.queue = new LinkedList<WebOperation>();
			}

			// Token: 0x06001B1F RID: 6943 RVA: 0x00075BE2 File Offset: 0x00073DE2
			public bool IsEmpty()
			{
				return this.connections.Count == 0 && this.queue.Count == 0;
			}

			// Token: 0x06001B20 RID: 6944 RVA: 0x00075C01 File Offset: 0x00073E01
			public void RemoveConnection(WebConnection connection)
			{
				this.connections.Remove(connection);
				connection.Dispose();
				this.Scheduler.OnConnectionClosed(connection);
			}

			// Token: 0x06001B21 RID: 6945 RVA: 0x00075C24 File Offset: 0x00073E24
			public void Cleanup()
			{
				LinkedListNode<WebConnection> linkedListNode = this.connections.First;
				while (linkedListNode != null)
				{
					WebConnection value = linkedListNode.Value;
					LinkedListNode<WebConnection> linkedListNode2 = linkedListNode;
					linkedListNode = linkedListNode.Next;
					if (value.Closed)
					{
						this.connections.Remove(linkedListNode2);
						this.Scheduler.OnConnectionClosed(value);
					}
				}
			}

			// Token: 0x06001B22 RID: 6946 RVA: 0x00075C72 File Offset: 0x00073E72
			public void EnqueueOperation(WebOperation operation)
			{
				this.queue.AddLast(operation);
			}

			// Token: 0x06001B23 RID: 6947 RVA: 0x00075C84 File Offset: 0x00073E84
			public WebOperation GetNextOperation()
			{
				LinkedListNode<WebOperation> linkedListNode = this.queue.First;
				while (linkedListNode != null)
				{
					WebOperation value = linkedListNode.Value;
					LinkedListNode<WebOperation> linkedListNode2 = linkedListNode;
					linkedListNode = linkedListNode.Next;
					if (!value.Aborted)
					{
						return value;
					}
					this.queue.Remove(linkedListNode2);
					this.Scheduler.RemoveOperation(value);
				}
				return null;
			}

			// Token: 0x06001B24 RID: 6948 RVA: 0x00075CD8 File Offset: 0x00073ED8
			public WebConnection FindIdleConnection(WebOperation operation)
			{
				WebConnection webConnection = null;
				foreach (WebConnection webConnection2 in this.connections)
				{
					if (webConnection2.CanReuseConnection(operation) && (webConnection == null || webConnection2.IdleSince > webConnection.IdleSince))
					{
						webConnection = webConnection2;
					}
				}
				if (webConnection != null && webConnection.StartOperation(operation, true))
				{
					this.queue.Remove(operation);
					return webConnection;
				}
				foreach (WebConnection webConnection3 in this.connections)
				{
					if (webConnection3.StartOperation(operation, true))
					{
						this.queue.Remove(operation);
						return webConnection3;
					}
				}
				return null;
			}

			// Token: 0x06001B25 RID: 6949 RVA: 0x00075DC0 File Offset: 0x00073FC0
			[return: TupleElementNames(new string[] { "connection", "created" })]
			public ValueTuple<WebConnection, bool> CreateOrReuseConnection(WebOperation operation, bool force)
			{
				WebConnection webConnection = this.FindIdleConnection(operation);
				if (webConnection != null)
				{
					return new ValueTuple<WebConnection, bool>(webConnection, false);
				}
				if (force || this.Scheduler.ServicePoint.ConnectionLimit > this.connections.Count || this.connections.Count == 0)
				{
					webConnection = new WebConnection(this.Scheduler.ServicePoint);
					webConnection.StartOperation(operation, false);
					this.connections.AddFirst(webConnection);
					this.Scheduler.OnConnectionCreated(webConnection);
					this.queue.Remove(operation);
					return new ValueTuple<WebConnection, bool>(webConnection, true);
				}
				return new ValueTuple<WebConnection, bool>(null, false);
			}

			// Token: 0x040011AD RID: 4525
			private static int nextId;

			// Token: 0x040011AE RID: 4526
			public readonly int ID = ++ServicePointScheduler.ConnectionGroup.nextId;

			// Token: 0x040011AF RID: 4527
			private LinkedList<WebConnection> connections;

			// Token: 0x040011B0 RID: 4528
			private LinkedList<WebOperation> queue;
		}

		// Token: 0x0200042F RID: 1071
		private class AsyncManualResetEvent
		{
			// Token: 0x06001B26 RID: 6950 RVA: 0x00075E5C File Offset: 0x0007405C
			public Task<bool> WaitAsync(int millisecondTimeout)
			{
				return ServicePointScheduler.WaitAsync(this.m_tcs.Task, millisecondTimeout);
			}

			// Token: 0x06001B27 RID: 6951 RVA: 0x00075E74 File Offset: 0x00074074
			public void Set()
			{
				TaskCompletionSource<bool> tcs = this.m_tcs;
				Task.Factory.StartNew<bool>((object s) => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
				tcs.Task.Wait();
			}

			// Token: 0x06001B28 RID: 6952 RVA: 0x00075ECC File Offset: 0x000740CC
			public void Reset()
			{
				TaskCompletionSource<bool> tcs;
				do
				{
					tcs = this.m_tcs;
				}
				while (tcs.Task.IsCompleted && Interlocked.CompareExchange<TaskCompletionSource<bool>>(ref this.m_tcs, new TaskCompletionSource<bool>(), tcs) != tcs);
			}

			// Token: 0x06001B29 RID: 6953 RVA: 0x00075F03 File Offset: 0x00074103
			public AsyncManualResetEvent(bool state)
			{
				if (state)
				{
					this.Set();
				}
			}

			// Token: 0x040011B1 RID: 4529
			private volatile TaskCompletionSource<bool> m_tcs = new TaskCompletionSource<bool>();
		}
	}
}
