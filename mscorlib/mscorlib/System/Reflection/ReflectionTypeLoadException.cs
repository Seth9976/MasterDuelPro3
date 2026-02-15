using System;
using System.Runtime.Serialization;
using System.Text;

namespace System.Reflection
{
	/// <summary>The exception that is thrown by the <see cref="M:System.Reflection.Module.GetTypes" /> method if any of the classes in a module cannot be loaded. This class cannot be inherited.</summary>
	// Token: 0x02000619 RID: 1561
	[Serializable]
	public sealed class ReflectionTypeLoadException : SystemException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ReflectionTypeLoadException" /> class with the given classes and their associated exceptions.</summary>
		/// <param name="classes">An array of type Type containing the classes that were defined in the module and loaded. This array can contain null reference (Nothing in Visual Basic) values. </param>
		/// <param name="exceptions">An array of type Exception containing the exceptions that were thrown by the class loader. The null reference (Nothing in Visual Basic) values in the <paramref name="classes" /> array line up with the exceptions in this <paramref name="exceptions" /> array. </param>
		// Token: 0x06002D7E RID: 11646 RVA: 0x000B2631 File Offset: 0x000B0831
		public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions)
			: base(null)
		{
			this.Types = classes;
			this.LoaderExceptions = exceptions;
			base.HResult = -2146232830;
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000B2653 File Offset: 0x000B0853
		private ReflectionTypeLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.LoaderExceptions = (Exception[])info.GetValue("Exceptions", typeof(Exception[]));
		}

		/// <summary>Provides an <see cref="T:System.Runtime.Serialization.ISerializable" /> implementation for serialized objects.</summary>
		/// <param name="info">The information and data needed to serialize or deserialize an object. </param>
		/// <param name="context">The context for the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">info is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06002D80 RID: 11648 RVA: 0x000B267D File Offset: 0x000B087D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("Types", null, typeof(Type[]));
			info.AddValue("Exceptions", this.LoaderExceptions, typeof(Exception[]));
		}

		/// <summary>Gets the array of classes that were defined in the module and loaded.</summary>
		/// <returns>An array of type Type containing the classes that were defined in the module and loaded. This array can contain some null values.</returns>
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x000B26B8 File Offset: 0x000B08B8
		public Type[] Types { get; }

		/// <summary>Gets the array of exceptions thrown by the class loader.</summary>
		/// <returns>An array of type Exception containing the exceptions thrown by the class loader. The null values in the <paramref name="classes" /> array of this instance line up with the exceptions in this array.</returns>
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x000B26C0 File Offset: 0x000B08C0
		public Exception[] LoaderExceptions { get; }

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x000B26C8 File Offset: 0x000B08C8
		public override string Message
		{
			get
			{
				return this.CreateString(true);
			}
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000B26D1 File Offset: 0x000B08D1
		public override string ToString()
		{
			return this.CreateString(false);
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000B26DC File Offset: 0x000B08DC
		private string CreateString(bool isMessage)
		{
			string text = (isMessage ? base.Message : base.ToString());
			Exception[] loaderExceptions = this.LoaderExceptions;
			if (loaderExceptions == null || loaderExceptions.Length == 0)
			{
				return text;
			}
			StringBuilder stringBuilder = new StringBuilder(text);
			foreach (Exception ex in loaderExceptions)
			{
				if (ex != null)
				{
					stringBuilder.AppendLine();
					stringBuilder.Append(isMessage ? ex.Message : ex.ToString());
				}
			}
			return stringBuilder.ToString();
		}
	}
}
