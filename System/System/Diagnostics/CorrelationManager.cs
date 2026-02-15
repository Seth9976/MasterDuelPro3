using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Diagnostics
{
	/// <summary>Correlates traces that are part of a logical transaction.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200014C RID: 332
	public class CorrelationManager
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x000026E5 File Offset: 0x000008E5
		internal CorrelationManager()
		{
		}

		/// <summary>Gets or sets the identity for a global activity.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that identifies the global activity.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0002B344 File Offset: 0x00029544
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0002B36B File Offset: 0x0002956B
		public Guid ActivityId
		{
			get
			{
				object obj = CallContext.LogicalGetData("E2ETrace.ActivityID");
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				CallContext.LogicalSetData("E2ETrace.ActivityID", value);
			}
		}

		/// <summary>Gets the logical operation stack from the call context.</summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> object that represents the logical operation stack for the call context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0002B37D File Offset: 0x0002957D
		public Stack LogicalOperationStack
		{
			get
			{
				return this.GetLogicalOperationStack();
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0002B388 File Offset: 0x00029588
		private Stack GetLogicalOperationStack()
		{
			Stack stack = CallContext.LogicalGetData("System.Diagnostics.Trace.CorrelationManagerSlot") as Stack;
			if (stack == null)
			{
				stack = new Stack();
				CallContext.LogicalSetData("System.Diagnostics.Trace.CorrelationManagerSlot", stack);
			}
			return stack;
		}
	}
}
