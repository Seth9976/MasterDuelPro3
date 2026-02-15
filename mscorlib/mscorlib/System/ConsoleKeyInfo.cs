using System;

namespace System
{
	/// <summary>Describes the console key that was pressed, including the character represented by the console key and the state of the SHIFT, ALT, and CTRL modifier keys.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000175 RID: 373
	[Serializable]
	public readonly struct ConsoleKeyInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ConsoleKeyInfo" /> structure using the specified character, console key, and modifier keys.</summary>
		/// <param name="keyChar">The Unicode character that corresponds to the <paramref name="key" /> parameter. </param>
		/// <param name="key">The console key that corresponds to the <paramref name="keyChar" /> parameter. </param>
		/// <param name="shift">true to indicate that a SHIFT key was pressed; otherwise, false. </param>
		/// <param name="alt">true to indicate that an ALT key was pressed; otherwise, false. </param>
		/// <param name="control">true to indicate that a CTRL key was pressed; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The numeric value of the <paramref name="key" /> parameter is less than 0 or greater than 255.</exception>
		// Token: 0x06000D68 RID: 3432 RVA: 0x00039718 File Offset: 0x00037918
		public ConsoleKeyInfo(char keyChar, ConsoleKey key, bool shift, bool alt, bool control)
		{
			if (key < (ConsoleKey)0 || key > (ConsoleKey)255)
			{
				throw new ArgumentOutOfRangeException("key", "Console key values must be between 0 and 255 inclusive.");
			}
			this._keyChar = keyChar;
			this._key = key;
			this._mods = (ConsoleModifiers)0;
			if (shift)
			{
				this._mods |= ConsoleModifiers.Shift;
			}
			if (alt)
			{
				this._mods |= ConsoleModifiers.Alt;
			}
			if (control)
			{
				this._mods |= ConsoleModifiers.Control;
			}
		}

		/// <summary>Gets the Unicode character represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>An object that corresponds to the console key represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0003978B File Offset: 0x0003798B
		public char KeyChar
		{
			get
			{
				return this._keyChar;
			}
		}

		/// <summary>Gets the console key represented by the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>A value that identifies the console key that was pressed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00039793 File Offset: 0x00037993
		public ConsoleKey Key
		{
			get
			{
				return this._key;
			}
		}

		/// <summary>Gets a value indicating whether the specified object is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>true if <paramref name="value" /> is a <see cref="T:System.ConsoleKeyInfo" /> object and is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object; otherwise, false.</returns>
		/// <param name="value">An object to compare to the current <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		// Token: 0x06000D6B RID: 3435 RVA: 0x0003979B File Offset: 0x0003799B
		public override bool Equals(object value)
		{
			return value is ConsoleKeyInfo && this.Equals((ConsoleKeyInfo)value);
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.ConsoleKeyInfo" /> object is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the current <see cref="T:System.ConsoleKeyInfo" /> object; otherwise, false.</returns>
		/// <param name="obj">An object to compare to the current <see cref="T:System.ConsoleKeyInfo" /> object.</param>
		// Token: 0x06000D6C RID: 3436 RVA: 0x000397B3 File Offset: 0x000379B3
		public bool Equals(ConsoleKeyInfo obj)
		{
			return obj._keyChar == this._keyChar && obj._key == this._key && obj._mods == this._mods;
		}

		/// <summary>Returns the hash code for the current <see cref="T:System.ConsoleKeyInfo" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000D6D RID: 3437 RVA: 0x000397E1 File Offset: 0x000379E1
		public override int GetHashCode()
		{
			return (int)((ConsoleKey)this._keyChar | ((int)this._key << 16) | (ConsoleKey)((int)this._mods << 24));
		}

		// Token: 0x0400058F RID: 1423
		private readonly char _keyChar;

		// Token: 0x04000590 RID: 1424
		private readonly ConsoleKey _key;

		// Token: 0x04000591 RID: 1425
		private readonly ConsoleModifiers _mods;
	}
}
