using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	/// <summary>Represents a base implementation of the <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> interface that uses the <see cref="T:System.Convert" /> class and the <see cref="T:System.IConvertible" /> interface.</summary>
	// Token: 0x020004AD RID: 1197
	public class FormatterConverter : IFormatterConverter
	{
		/// <summary>Converts a value to the given <see cref="T:System.Type" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <param name="type">The <see cref="T:System.Type" /> into which <paramref name="value" /> is converted. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x06002641 RID: 9793 RVA: 0x0009A8CE File Offset: 0x00098ACE
		public object Convert(object value, Type type)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x06002642 RID: 9794 RVA: 0x0009A8E4 File Offset: 0x00098AE4
		public bool ToBoolean(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 32-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x06002643 RID: 9795 RVA: 0x0009A8F9 File Offset: 0x00098AF9
		public int ToInt32(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 64-bit signed integer.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x06002644 RID: 9796 RVA: 0x0009A90E File Offset: 0x00098B0E
		public long ToInt64(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToInt64(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a single-precision floating-point number.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x06002645 RID: 9797 RVA: 0x0009A923 File Offset: 0x00098B23
		public float ToSingle(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToSingle(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the specified object to a <see cref="T:System.String" />.</summary>
		/// <returns>The converted <paramref name="value" /> or null if the <paramref name="type" /> parameter is null.</returns>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x06002646 RID: 9798 RVA: 0x0009A938 File Offset: 0x00098B38
		public string ToString(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return global::System.Convert.ToString(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0009A94D File Offset: 0x00098B4D
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowValueNullException()
		{
			throw new ArgumentNullException("value");
		}
	}
}
