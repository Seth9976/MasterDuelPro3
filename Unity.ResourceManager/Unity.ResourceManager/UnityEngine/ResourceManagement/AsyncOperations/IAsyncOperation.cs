using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnityEngine.ResourceManagement.AsyncOperations
{
	// Token: 0x02000077 RID: 119
	internal interface IAsyncOperation
	{
		// Token: 0x060002A2 RID: 674
		object GetResultAsObject();

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002A3 RID: 675
		Type ResultType { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002A4 RID: 676
		int Version { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002A5 RID: 677
		string DebugName { get; }

		// Token: 0x060002A6 RID: 678
		void DecrementReferenceCount();

		// Token: 0x060002A7 RID: 679
		void IncrementReferenceCount();

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002A8 RID: 680
		int ReferenceCount { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002A9 RID: 681
		float PercentComplete { get; }

		// Token: 0x060002AA RID: 682
		DownloadStatus GetDownloadStatus(HashSet<object> visited);

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002AB RID: 683
		AsyncOperationStatus Status { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002AC RID: 684
		Exception OperationException { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002AD RID: 685
		bool IsDone { get; }

		// Token: 0x17000088 RID: 136
		// (set) Token: 0x060002AE RID: 686
		Action<IAsyncOperation> OnDestroy { set; }

		// Token: 0x060002AF RID: 687
		void GetDependencies(List<AsyncOperationHandle> deps);

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002B0 RID: 688
		bool IsRunning { get; }

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060002B1 RID: 689
		// (remove) Token: 0x060002B2 RID: 690
		event Action<AsyncOperationHandle> CompletedTypeless;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060002B3 RID: 691
		// (remove) Token: 0x060002B4 RID: 692
		event Action<AsyncOperationHandle> Destroyed;

		// Token: 0x060002B5 RID: 693
		void InvokeCompletionEvent();

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002B6 RID: 694
		Task<object> Task { get; }

		// Token: 0x060002B7 RID: 695
		void Start(ResourceManager rm, AsyncOperationHandle dependency, DelegateList<float> updateCallbacks);

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002B8 RID: 696
		AsyncOperationHandle Handle { get; }

		// Token: 0x060002B9 RID: 697
		void WaitForCompletion();
	}
}
