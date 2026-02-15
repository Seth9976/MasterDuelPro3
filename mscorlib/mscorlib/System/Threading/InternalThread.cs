using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x02000285 RID: 645
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class InternalThread : CriticalFinalizerObject
	{
		// Token: 0x06001809 RID: 6153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Thread_free_internal();

		// Token: 0x0600180A RID: 6154 RVA: 0x0005CBD0 File Offset: 0x0005ADD0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~InternalThread()
		{
			this.Thread_free_internal();
		}

		// Token: 0x04000B50 RID: 2896
		private int lock_thread_id;

		// Token: 0x04000B51 RID: 2897
		private IntPtr handle;

		// Token: 0x04000B52 RID: 2898
		private IntPtr native_handle;

		// Token: 0x04000B53 RID: 2899
		private IntPtr name_chars;

		// Token: 0x04000B54 RID: 2900
		private int name_free;

		// Token: 0x04000B55 RID: 2901
		private int name_length;

		// Token: 0x04000B56 RID: 2902
		private ThreadState state;

		// Token: 0x04000B57 RID: 2903
		private object abort_exc;

		// Token: 0x04000B58 RID: 2904
		private int abort_state_handle;

		// Token: 0x04000B59 RID: 2905
		internal long thread_id;

		// Token: 0x04000B5A RID: 2906
		private IntPtr debugger_thread;

		// Token: 0x04000B5B RID: 2907
		private UIntPtr static_data;

		// Token: 0x04000B5C RID: 2908
		private IntPtr runtime_thread_info;

		// Token: 0x04000B5D RID: 2909
		private object current_appcontext;

		// Token: 0x04000B5E RID: 2910
		private object root_domain_thread;

		// Token: 0x04000B5F RID: 2911
		internal byte[] _serialized_principal;

		// Token: 0x04000B60 RID: 2912
		internal int _serialized_principal_version;

		// Token: 0x04000B61 RID: 2913
		private IntPtr appdomain_refs;

		// Token: 0x04000B62 RID: 2914
		private int interruption_requested;

		// Token: 0x04000B63 RID: 2915
		private IntPtr longlived;

		// Token: 0x04000B64 RID: 2916
		internal bool threadpool_thread;

		// Token: 0x04000B65 RID: 2917
		private bool thread_interrupt_requested;

		// Token: 0x04000B66 RID: 2918
		internal int stack_size;

		// Token: 0x04000B67 RID: 2919
		internal byte apartment_state;

		// Token: 0x04000B68 RID: 2920
		internal volatile int critical_region_level;

		// Token: 0x04000B69 RID: 2921
		internal int managed_id;

		// Token: 0x04000B6A RID: 2922
		private int small_id;

		// Token: 0x04000B6B RID: 2923
		private IntPtr manage_callback;

		// Token: 0x04000B6C RID: 2924
		private IntPtr flags;

		// Token: 0x04000B6D RID: 2925
		private IntPtr thread_pinning_ref;

		// Token: 0x04000B6E RID: 2926
		private IntPtr abort_protected_block_count;

		// Token: 0x04000B6F RID: 2927
		private int priority = 2;

		// Token: 0x04000B70 RID: 2928
		private IntPtr owned_mutex;

		// Token: 0x04000B71 RID: 2929
		private IntPtr suspended_event;

		// Token: 0x04000B72 RID: 2930
		private int self_suspended;

		// Token: 0x04000B73 RID: 2931
		private IntPtr thread_state;

		// Token: 0x04000B74 RID: 2932
		private IntPtr netcore0;

		// Token: 0x04000B75 RID: 2933
		private IntPtr netcore1;

		// Token: 0x04000B76 RID: 2934
		private IntPtr netcore2;

		// Token: 0x04000B77 RID: 2935
		private IntPtr last;
	}
}
