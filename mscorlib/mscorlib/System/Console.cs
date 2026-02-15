using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	/// <summary>Represents the standard input, output, and error streams for console applications. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001C4 RID: 452
	public static class Console
	{
		// Token: 0x060011B5 RID: 4533 RVA: 0x0004861C File Offset: 0x0004681C
		static Console()
		{
			if (Environment.IsRunningOnWindows)
			{
				try
				{
					Console.inputEncoding = Encoding.GetEncoding(Console.WindowsConsole.GetInputCodePage());
					Console.outputEncoding = Encoding.GetEncoding(Console.WindowsConsole.GetOutputCodePage());
					goto IL_009B;
				}
				catch
				{
					Console.inputEncoding = (Console.outputEncoding = Encoding.Default);
					goto IL_009B;
				}
			}
			int num = 0;
			EncodingHelper.InternalCodePage(ref num);
			if (num != -1 && ((num & 268435455) == 3 || (num & 268435456) != 0))
			{
				Console.inputEncoding = (Console.outputEncoding = EncodingHelper.UTF8Unmarked);
			}
			else
			{
				Console.inputEncoding = (Console.outputEncoding = Encoding.Default);
			}
			IL_009B:
			Console.SetupStreams(Console.inputEncoding, Console.outputEncoding);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000486E4 File Offset: 0x000468E4
		private static void SetupStreams(Encoding inputEncoding, Encoding outputEncoding)
		{
			if (!Environment.IsRunningOnWindows && ConsoleDriver.IsConsole)
			{
				Console.stdin = new CStreamReader(Console.OpenStandardInput(0), inputEncoding);
				Console.stdout = TextWriter.Synchronized(new CStreamWriter(Console.OpenStandardOutput(0), outputEncoding, true)
				{
					AutoFlush = true
				});
				Console.stderr = TextWriter.Synchronized(new CStreamWriter(Console.OpenStandardError(0), outputEncoding, true)
				{
					AutoFlush = true
				});
			}
			else
			{
				Console.stdin = TextReader.Synchronized(new UnexceptionalStreamReader(Console.OpenStandardInput(0), inputEncoding));
				Console.stdout = TextWriter.Synchronized(new UnexceptionalStreamWriter(Console.OpenStandardOutput(0), outputEncoding)
				{
					AutoFlush = true
				});
				Console.stderr = TextWriter.Synchronized(new UnexceptionalStreamWriter(Console.OpenStandardError(0), outputEncoding)
				{
					AutoFlush = true
				});
			}
			GC.SuppressFinalize(Console.stdout);
			GC.SuppressFinalize(Console.stderr);
			GC.SuppressFinalize(Console.stdin);
		}

		/// <summary>Gets the standard error output stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the standard error output stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x000487BC File Offset: 0x000469BC
		public static TextWriter Error
		{
			get
			{
				return Console.stderr;
			}
		}

		/// <summary>Gets the standard output stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the standard output stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x000487C3 File Offset: 0x000469C3
		public static TextWriter Out
		{
			get
			{
				return Console.stdout;
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000487CC File Offset: 0x000469CC
		private static Stream Open(IntPtr handle, FileAccess access, int bufferSize)
		{
			Stream stream;
			try
			{
				FileStream fileStream = new FileStream(handle, access, false, bufferSize, false, true);
				GC.SuppressFinalize(fileStream);
				stream = fileStream;
			}
			catch (IOException)
			{
				stream = Stream.Null;
			}
			return stream;
		}

		/// <summary>Acquires the standard error stream, which is set to a specified buffer size.</summary>
		/// <returns>The standard error stream.</returns>
		/// <param name="bufferSize">The internal stream buffer size. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011BA RID: 4538 RVA: 0x00048808 File Offset: 0x00046A08
		public static Stream OpenStandardError(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleError, FileAccess.Write, bufferSize);
		}

		/// <summary>Acquires the standard input stream, which is set to a specified buffer size.</summary>
		/// <returns>The standard input stream.</returns>
		/// <param name="bufferSize">The internal stream buffer size. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011BB RID: 4539 RVA: 0x00048816 File Offset: 0x00046A16
		public static Stream OpenStandardInput(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleInput, FileAccess.Read, bufferSize);
		}

		/// <summary>Acquires the standard output stream, which is set to a specified buffer size.</summary>
		/// <returns>The standard output stream.</returns>
		/// <param name="bufferSize">The internal stream buffer size. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011BC RID: 4540 RVA: 0x00048824 File Offset: 0x00046A24
		public static Stream OpenStandardOutput(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleOutput, FileAccess.Write, bufferSize);
		}

		/// <summary>Sets the <see cref="P:System.Console.Error" /> property to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="newError">A stream that is the new standard error output. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newError" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060011BD RID: 4541 RVA: 0x00048832 File Offset: 0x00046A32
		public static void SetError(TextWriter newError)
		{
			if (newError == null)
			{
				throw new ArgumentNullException("newError");
			}
			Console.stderr = TextWriter.Synchronized(newError);
		}

		/// <summary>Sets the <see cref="P:System.Console.Out" /> property to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="newOut">A stream that is the new standard output. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newOut" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060011BE RID: 4542 RVA: 0x0004884D File Offset: 0x00046A4D
		public static void SetOut(TextWriter newOut)
		{
			if (newOut == null)
			{
				throw new ArgumentNullException("newOut");
			}
			Console.stdout = TextWriter.Synchronized(newOut);
		}

		/// <summary>Writes the specified string value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011BF RID: 4543 RVA: 0x00048868 File Offset: 0x00046A68
		public static void Write(string value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified object to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">An object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011C0 RID: 4544 RVA: 0x00048875 File Offset: 0x00046A75
		public static void Write(string format, object arg0)
		{
			Console.stdout.Write(format, arg0);
		}

		/// <summary>Writes the current line terminator to the standard output stream.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011C1 RID: 4545 RVA: 0x00048883 File Offset: 0x00046A83
		public static void WriteLine()
		{
			Console.stdout.WriteLine();
		}

		/// <summary>Writes the text representation of the specified Boolean value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011C2 RID: 4546 RVA: 0x0004888F File Offset: 0x00046A8F
		public static void WriteLine(bool value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the specified Unicode character, followed by the current line terminator, value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011C3 RID: 4547 RVA: 0x0004889C File Offset: 0x00046A9C
		public static void WriteLine(char value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the specified array of Unicode characters, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="buffer">A Unicode character array. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011C4 RID: 4548 RVA: 0x000488A9 File Offset: 0x00046AA9
		public static void WriteLine(char[] buffer)
		{
			Console.stdout.WriteLine(buffer);
		}

		/// <summary>Writes the text representation of the specified <see cref="T:System.Decimal" /> value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011C5 RID: 4549 RVA: 0x000488B6 File Offset: 0x00046AB6
		public static void WriteLine(decimal value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified double-precision floating-point value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011C6 RID: 4550 RVA: 0x000488C3 File Offset: 0x00046AC3
		public static void WriteLine(double value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit signed integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011C7 RID: 4551 RVA: 0x000488D0 File Offset: 0x00046AD0
		public static void WriteLine(int value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit signed integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011C8 RID: 4552 RVA: 0x000488DD File Offset: 0x00046ADD
		public static void WriteLine(long value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011C9 RID: 4553 RVA: 0x000488EA File Offset: 0x00046AEA
		public static void WriteLine(object value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified single-precision floating-point value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011CA RID: 4554 RVA: 0x000488F7 File Offset: 0x00046AF7
		public static void WriteLine(float value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the specified string value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011CB RID: 4555 RVA: 0x00048904 File Offset: 0x00046B04
		public static void WriteLine(string value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit unsigned integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011CC RID: 4556 RVA: 0x00048911 File Offset: 0x00046B11
		[CLSCompliant(false)]
		public static void WriteLine(uint value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit unsigned integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011CD RID: 4557 RVA: 0x0004891E File Offset: 0x00046B1E
		[CLSCompliant(false)]
		public static void WriteLine(ulong value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">An object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011CE RID: 4558 RVA: 0x0004892B File Offset: 0x00046B2B
		public static void WriteLine(string format, object arg0)
		{
			Console.stdout.WriteLine(format, arg0);
		}

		/// <summary>Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg">An array of objects to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="arg" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011CF RID: 4559 RVA: 0x00048939 File Offset: 0x00046B39
		public static void WriteLine(string format, params object[] arg)
		{
			if (arg == null)
			{
				Console.stdout.WriteLine(format);
				return;
			}
			Console.stdout.WriteLine(format, arg);
		}

		/// <summary>Writes the specified subarray of Unicode characters, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="buffer">An array of Unicode characters. </param>
		/// <param name="index">The starting position in <paramref name="buffer" />. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus <paramref name="count" /> specify a position that is not within <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D0 RID: 4560 RVA: 0x00048956 File Offset: 0x00046B56
		public static void WriteLine(char[] buffer, int index, int count)
		{
			Console.stdout.WriteLine(buffer, index, count);
		}

		/// <summary>Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D1 RID: 4561 RVA: 0x00048965 File Offset: 0x00046B65
		public static void WriteLine(string format, object arg0, object arg1)
		{
			Console.stdout.WriteLine(format, arg0, arg1);
		}

		/// <summary>Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <param name="arg2">The third object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D2 RID: 4562 RVA: 0x00048974 File Offset: 0x00046B74
		public static void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			Console.stdout.WriteLine(format, arg0, arg1, arg2);
		}

		/// <summary>Writes the text representation of the specified objects and variable-length parameter list, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks).</param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <param name="arg2">The third object to write using <paramref name="format" />. </param>
		/// <param name="arg3">The fourth object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D3 RID: 4563 RVA: 0x00048984 File Offset: 0x00046B84
		[CLSCompliant(false)]
		public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int remainingCount = argIterator.GetRemainingCount();
			object[] array = new object[remainingCount + 4];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 0; i < remainingCount; i++)
			{
				TypedReference nextArg = argIterator.GetNextArg();
				array[i + 4] = TypedReference.ToObject(nextArg);
			}
			Console.stdout.WriteLine(string.Format(format, array));
		}

		/// <summary>Gets or sets the encoding the console uses to read input. </summary>
		/// <returns>The encoding used to read console input.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value in a set operation is null.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">Your application does not have permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x000489EE File Offset: 0x00046BEE
		public static Encoding InputEncoding
		{
			get
			{
				return Console.inputEncoding;
			}
		}

		/// <summary>Gets or sets the encoding the console uses to write output. </summary>
		/// <returns>The encoding used to write console output.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value in a set operation is null.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">Your application does not have permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x000489F5 File Offset: 0x00046BF5
		public static Encoding OutputEncoding
		{
			get
			{
				return Console.outputEncoding;
			}
		}

		/// <summary>Gets or sets the background color of the console.</summary>
		/// <returns>A value that specifies the background color of the console; that is, the color that appears behind each character. The default is black.</returns>
		/// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x170001CA RID: 458
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x000489FC File Offset: 0x00046BFC
		public static ConsoleColor BackgroundColor
		{
			set
			{
				ConsoleDriver.BackgroundColor = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the console.</summary>
		/// <returns>A <see cref="T:System.ConsoleColor" /> that specifies the foreground color of the console; that is, the color of each character that is displayed. The default is gray.</returns>
		/// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x170001CB RID: 459
		// (set) Token: 0x060011D7 RID: 4567 RVA: 0x00048A04 File Offset: 0x00046C04
		public static ConsoleColor ForegroundColor
		{
			set
			{
				ConsoleDriver.ForegroundColor = value;
			}
		}

		/// <summary>Obtains the next character or function key pressed by the user. The pressed key is displayed in the console window.</summary>
		/// <returns>A <see cref="T:System.ConsoleKeyInfo" /> object that describes the <see cref="T:System.ConsoleKey" /> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo" /> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values, whether one or more Shift, Alt, or Ctrl modifier keys was pressed simultaneously with the console key.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Console.In" /> property is redirected from some stream other than the console.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D8 RID: 4568 RVA: 0x00048A0C File Offset: 0x00046C0C
		public static ConsoleKeyInfo ReadKey()
		{
			return Console.ReadKey(false);
		}

		/// <summary>Obtains the next character or function key pressed by the user. The pressed key is optionally displayed in the console window.</summary>
		/// <returns>A <see cref="T:System.ConsoleKeyInfo" /> object that describes the <see cref="T:System.ConsoleKey" /> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo" /> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values, whether one or more Shift, Alt, or Ctrl modifier keys was pressed simultaneously with the console key.</returns>
		/// <param name="intercept">Determines whether to display the pressed key in the console window. true to not display the pressed key; otherwise, false. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Console.In" /> property is redirected from some stream other than the console.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D9 RID: 4569 RVA: 0x00048A14 File Offset: 0x00046C14
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			return ConsoleDriver.ReadKey(intercept);
		}

		/// <summary>Sets the foreground and background console colors to their defaults.</summary>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x060011DA RID: 4570 RVA: 0x00048A1C File Offset: 0x00046C1C
		public static void ResetColor()
		{
			ConsoleDriver.ResetColor();
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00048A24 File Offset: 0x00046C24
		private static void DoConsoleCancelEvent()
		{
			bool flag = true;
			if (Console.cancel_event != null)
			{
				ConsoleCancelEventArgs consoleCancelEventArgs = new ConsoleCancelEventArgs(ConsoleSpecialKey.ControlC);
				foreach (ConsoleCancelEventHandler consoleCancelEventHandler in Console.cancel_event.GetInvocationList())
				{
					try
					{
						consoleCancelEventHandler(null, consoleCancelEventArgs);
					}
					catch
					{
					}
				}
				flag = !consoleCancelEventArgs.Cancel;
			}
			if (flag)
			{
				Environment.Exit(58);
			}
		}

		// Token: 0x04000734 RID: 1844
		internal static TextWriter stdout;

		// Token: 0x04000735 RID: 1845
		private static TextWriter stderr;

		// Token: 0x04000736 RID: 1846
		private static TextReader stdin;

		// Token: 0x04000737 RID: 1847
		internal static bool IsRunningOnAndroid = File.Exists("/system/lib/liblog.so") || File.Exists("/system/lib64/liblog.so");

		// Token: 0x04000738 RID: 1848
		private static Encoding inputEncoding;

		// Token: 0x04000739 RID: 1849
		private static Encoding outputEncoding;

		// Token: 0x0400073A RID: 1850
		private static ConsoleCancelEventHandler cancel_event;

		// Token: 0x020001C5 RID: 453
		private class WindowsConsole
		{
			// Token: 0x060011DC RID: 4572
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			private static extern int GetConsoleCP();

			// Token: 0x060011DD RID: 4573
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			private static extern int GetConsoleOutputCP();

			// Token: 0x060011DE RID: 4574 RVA: 0x00048A98 File Offset: 0x00046C98
			private static bool DoWindowsConsoleCancelEvent(int keyCode)
			{
				if (keyCode == 0)
				{
					Console.DoConsoleCancelEvent();
				}
				return keyCode == 0;
			}

			// Token: 0x060011DF RID: 4575 RVA: 0x00048AA6 File Offset: 0x00046CA6
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static int GetInputCodePage()
			{
				return Console.WindowsConsole.GetConsoleCP();
			}

			// Token: 0x060011E0 RID: 4576 RVA: 0x00048AAD File Offset: 0x00046CAD
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static int GetOutputCodePage()
			{
				return Console.WindowsConsole.GetConsoleOutputCP();
			}

			// Token: 0x0400073B RID: 1851
			public static bool ctrlHandlerAdded = false;

			// Token: 0x0400073C RID: 1852
			private static Console.WindowsConsole.WindowsCancelHandler cancelHandler = new Console.WindowsConsole.WindowsCancelHandler(Console.WindowsConsole.DoWindowsConsoleCancelEvent);

			// Token: 0x020001C6 RID: 454
			// (Invoke) Token: 0x060011E3 RID: 4579
			private delegate bool WindowsCancelHandler(int keyCode);
		}
	}
}
