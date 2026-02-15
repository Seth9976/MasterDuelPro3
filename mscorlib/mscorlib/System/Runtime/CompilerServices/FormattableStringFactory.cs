using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000588 RID: 1416
	public static class FormattableStringFactory
	{
		// Token: 0x06002AF3 RID: 10995 RVA: 0x000AAB77 File Offset: 0x000A8D77
		public static FormattableString Create(string format, params object[] arguments)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			return new FormattableStringFactory.ConcreteFormattableString(format, arguments);
		}

		// Token: 0x02000589 RID: 1417
		private sealed class ConcreteFormattableString : FormattableString
		{
			// Token: 0x06002AF4 RID: 10996 RVA: 0x000AAB9C File Offset: 0x000A8D9C
			internal ConcreteFormattableString(string format, object[] arguments)
			{
				this._format = format;
				this._arguments = arguments;
			}

			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000AABB2 File Offset: 0x000A8DB2
			public override string Format
			{
				get
				{
					return this._format;
				}
			}

			// Token: 0x06002AF6 RID: 10998 RVA: 0x000AABBA File Offset: 0x000A8DBA
			public override object[] GetArguments()
			{
				return this._arguments;
			}

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x000AABC2 File Offset: 0x000A8DC2
			public override int ArgumentCount
			{
				get
				{
					return this._arguments.Length;
				}
			}

			// Token: 0x06002AF8 RID: 11000 RVA: 0x000AABCC File Offset: 0x000A8DCC
			public override object GetArgument(int index)
			{
				return this._arguments[index];
			}

			// Token: 0x06002AF9 RID: 11001 RVA: 0x000AABD6 File Offset: 0x000A8DD6
			public override string ToString(IFormatProvider formatProvider)
			{
				return string.Format(formatProvider, this._format, this._arguments);
			}

			// Token: 0x040015D1 RID: 5585
			private readonly string _format;

			// Token: 0x040015D2 RID: 5586
			private readonly object[] _arguments;
		}
	}
}
