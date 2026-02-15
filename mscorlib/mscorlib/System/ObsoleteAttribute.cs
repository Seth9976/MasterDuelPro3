using System;

namespace System
{
	/// <summary>Marks the program elements that are no longer in use. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200012F RID: 303
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[Serializable]
	public sealed class ObsoleteAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with default properties.</summary>
		// Token: 0x06000A34 RID: 2612 RVA: 0x0002E59D File Offset: 0x0002C79D
		public ObsoleteAttribute()
		{
			this._message = null;
			this._error = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a specified workaround message.</summary>
		/// <param name="message">The text string that describes alternative workarounds. </param>
		// Token: 0x06000A35 RID: 2613 RVA: 0x0002E5B3 File Offset: 0x0002C7B3
		public ObsoleteAttribute(string message)
		{
			this._message = message;
			this._error = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a workaround message and a Boolean value indicating whether the obsolete element usage is considered an error.</summary>
		/// <param name="message">The text string that describes alternative workarounds. </param>
		/// <param name="error">The Boolean value that indicates whether the obsolete element usage is considered an error. </param>
		// Token: 0x06000A36 RID: 2614 RVA: 0x0002E5C9 File Offset: 0x0002C7C9
		public ObsoleteAttribute(string message, bool error)
		{
			this._message = message;
			this._error = error;
		}

		/// <summary>Gets the workaround message, including a description of the alternative program elements.</summary>
		/// <returns>The workaround text string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002E5DF File Offset: 0x0002C7DF
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		/// <summary>Gets a Boolean value indicating whether the compiler will treat usage of the obsolete program element as an error.</summary>
		/// <returns>true if the obsolete element usage is considered an error; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0002E5E7 File Offset: 0x0002C7E7
		public bool IsError
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x04000463 RID: 1123
		private string _message;

		// Token: 0x04000464 RID: 1124
		private bool _error;
	}
}
