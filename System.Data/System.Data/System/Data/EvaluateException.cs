using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the exception that is thrown when the <see cref="P:System.Data.DataColumn.Expression" /> property of a <see cref="T:System.Data.DataColumn" /> cannot be evaluated.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200006F RID: 111
	[Serializable]
	public class EvaluateException : InvalidExpressionException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class with the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and the <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object. </param>
		/// <param name="context">The source and destination of a particular serialized stream. </param>
		// Token: 0x06000629 RID: 1577 RVA: 0x0001F74C File Offset: 0x0001D94C
		protected EvaluateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class.</summary>
		// Token: 0x0600062A RID: 1578 RVA: 0x0001F756 File Offset: 0x0001D956
		public EvaluateException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.EvaluateException" /> class with the specified string.</summary>
		/// <param name="s">The string to display when the exception is thrown. </param>
		// Token: 0x0600062B RID: 1579 RVA: 0x0001F75E File Offset: 0x0001D95E
		public EvaluateException(string s)
			: base(s)
		{
		}
	}
}
