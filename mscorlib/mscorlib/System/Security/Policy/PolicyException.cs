using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Policy
{
	/// <summary>The exception that is thrown when policy forbids code to run.</summary>
	// Token: 0x02000347 RID: 839
	[ComVisible(true)]
	[Serializable]
	public class PolicyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with default properties.</summary>
		// Token: 0x06001D8C RID: 7564 RVA: 0x000742AB File Offset: 0x000724AB
		public PolicyException()
			: base(Locale.GetText("Cannot run because of policy."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with a specified error message.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		// Token: 0x06001D8D RID: 7565 RVA: 0x000536B7 File Offset: 0x000518B7
		public PolicyException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.PolicyException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		// Token: 0x06001D8E RID: 7566 RVA: 0x00018324 File Offset: 0x00016524
		protected PolicyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
