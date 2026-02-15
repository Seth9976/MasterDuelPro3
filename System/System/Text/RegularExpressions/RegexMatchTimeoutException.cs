using System;
using System.Runtime.Serialization;

namespace System.Text.RegularExpressions
{
	/// <summary>The exception that is thrown when the execution time of a regular expression pattern-matching method exceeds its time-out interval.</summary>
	// Token: 0x02000140 RID: 320
	[Serializable]
	public class RegexMatchTimeoutException : TimeoutException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with information about the regular expression pattern, the input text, and the time-out interval.</summary>
		/// <param name="regexInput">The input text processed by the regular expression engine when the time-out occurred.</param>
		/// <param name="regexPattern">The pattern used by the regular expression engine when the time-out occurred.</param>
		/// <param name="matchTimeout">The time-out interval.</param>
		// Token: 0x06000714 RID: 1812 RVA: 0x000268CC File Offset: 0x00024ACC
		public RegexMatchTimeoutException(string regexInput, string regexPattern, TimeSpan matchTimeout)
			: base("The RegEx engine has timed out while trying to match a pattern to an input string. This can occur for many reasons, including very large inputs or excessive backtracking caused by nested quantifiers, back-references and other factors.")
		{
			this.Input = regexInput;
			this.Pattern = regexPattern;
			this.MatchTimeout = matchTimeout;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with a system-supplied message.</summary>
		// Token: 0x06000715 RID: 1813 RVA: 0x0002691C File Offset: 0x00024B1C
		public RegexMatchTimeoutException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with serialized data.</summary>
		/// <param name="info">The object that contains the serialized data.</param>
		/// <param name="context">The stream that contains the serialized data.</param>
		// Token: 0x06000716 RID: 1814 RVA: 0x00026948 File Offset: 0x00024B48
		protected RegexMatchTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Input = info.GetString("regexInput");
			this.Pattern = info.GetString("regexPattern");
			this.MatchTimeout = new TimeSpan(info.GetInt64("timeoutTicks"));
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize a <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> object.</summary>
		/// <param name="si">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x06000717 RID: 1815 RVA: 0x000269B8 File Offset: 0x00024BB8
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("regexInput", this.Input);
			info.AddValue("regexPattern", this.Pattern);
			info.AddValue("timeoutTicks", this.MatchTimeout.Ticks);
		}

		/// <summary>Gets the input text that the regular expression engine was processing when the time-out occurred.</summary>
		/// <returns>The regular expression input text.</returns>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00026A08 File Offset: 0x00024C08
		public string Input { get; } = string.Empty;

		/// <summary>Gets the regular expression pattern that was used in the matching operation when the time-out occurred.</summary>
		/// <returns>The regular expression pattern.</returns>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00026A10 File Offset: 0x00024C10
		public string Pattern { get; } = string.Empty;

		/// <summary>Gets the time-out interval for a regular expression match.</summary>
		/// <returns>The time-out interval.</returns>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00026A18 File Offset: 0x00024C18
		public TimeSpan MatchTimeout { get; } = TimeSpan.FromTicks(-1L);
	}
}
