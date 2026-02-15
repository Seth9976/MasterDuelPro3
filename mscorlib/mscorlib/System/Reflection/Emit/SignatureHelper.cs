using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Provides methods for building signatures.</summary>
	// Token: 0x0200067E RID: 1662
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_SignatureHelper))]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class SignatureHelper : _SignatureHelper
	{
		// Token: 0x06003383 RID: 13187 RVA: 0x000C1F1D File Offset: 0x000C011D
		internal SignatureHelper(ModuleBuilder module, SignatureHelper.SignatureHelperType type)
		{
			this.type = type;
			this.module = module;
		}

		/// <summary>Returns a signature helper for a local variable.</summary>
		/// <returns>The SignatureHelper object for a local variable.</returns>
		/// <param name="mod">The dynamic module that contains the local variable for which the SignatureHelper is requested. </param>
		// Token: 0x06003384 RID: 13188 RVA: 0x000C1F33 File Offset: 0x000C0133
		public static SignatureHelper GetLocalVarSigHelper(Module mod)
		{
			if (mod != null && !(mod is ModuleBuilder))
			{
				throw new ArgumentException("ModuleBuilder is expected");
			}
			return new SignatureHelper((ModuleBuilder)mod, SignatureHelper.SignatureHelperType.HELPER_LOCAL);
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000C1F60 File Offset: 0x000C0160
		private static int AppendArray(ref Type[] array, Type t)
		{
			if (array != null)
			{
				Type[] array2 = new Type[array.Length + 1];
				Array.Copy(array, array2, array.Length);
				array2[array.Length] = t;
				array = array2;
				return array.Length - 1;
			}
			array = new Type[1];
			array[0] = t;
			return 0;
		}

		/// <summary>Adds an argument to the signature.</summary>
		/// <param name="clsArgument">The type of the argument. </param>
		/// <exception cref="T:System.ArgumentException">The signature has already been finished. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="clsArgument" /> is null.</exception>
		// Token: 0x06003386 RID: 13190 RVA: 0x000C1FA8 File Offset: 0x000C01A8
		public void AddArgument(Type clsArgument)
		{
			if (clsArgument == null)
			{
				throw new ArgumentNullException("clsArgument");
			}
			SignatureHelper.AppendArray(ref this.arguments, clsArgument);
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000C1FCC File Offset: 0x000C01CC
		private static bool CompareOK(Type[][] one, Type[][] two)
		{
			if (one == null)
			{
				return two == null;
			}
			if (two == null)
			{
				return false;
			}
			if (one.Length != two.Length)
			{
				return false;
			}
			int i = 0;
			while (i < one.Length)
			{
				Type[] array = one[i];
				Type[] array2 = two[i];
				if (array == null)
				{
					if (array2 != null)
					{
						goto IL_0032;
					}
				}
				else
				{
					if (array2 == null)
					{
						return false;
					}
					goto IL_0032;
				}
				IL_0083:
				i++;
				continue;
				IL_0032:
				if (array.Length != array2.Length)
				{
					return false;
				}
				for (int j = 0; j < array.Length; j++)
				{
					Type type = array[j];
					Type type2 = array2[j];
					if (type == null)
					{
						if (!(type2 == null))
						{
							return false;
						}
					}
					else
					{
						if (type2 == null)
						{
							return false;
						}
						if (!type.Equals(type2))
						{
							return false;
						}
					}
				}
				goto IL_0083;
			}
			return true;
		}

		/// <summary>Checks if this instance is equal to the given object.</summary>
		/// <returns>true if the given object is a SignatureHelper and represents the same signature; otherwise, false.</returns>
		/// <param name="obj">The object with which this instance should be compared. </param>
		// Token: 0x06003388 RID: 13192 RVA: 0x000C2068 File Offset: 0x000C0268
		public override bool Equals(object obj)
		{
			SignatureHelper signatureHelper = obj as SignatureHelper;
			if (signatureHelper == null)
			{
				return false;
			}
			if (signatureHelper.module != this.module || signatureHelper.returnType != this.returnType || signatureHelper.callConv != this.callConv || signatureHelper.unmanagedCallConv != this.unmanagedCallConv)
			{
				return false;
			}
			if (this.arguments != null)
			{
				if (signatureHelper.arguments == null)
				{
					return false;
				}
				if (this.arguments.Length != signatureHelper.arguments.Length)
				{
					return false;
				}
				for (int i = 0; i < this.arguments.Length; i++)
				{
					if (!signatureHelper.arguments[i].Equals(this.arguments[i]))
					{
						return false;
					}
				}
			}
			else if (signatureHelper.arguments != null)
			{
				return false;
			}
			return SignatureHelper.CompareOK(signatureHelper.modreqs, this.modreqs) && SignatureHelper.CompareOK(signatureHelper.modopts, this.modopts);
		}

		/// <summary>Creates and returns a hash code for this instance.</summary>
		/// <returns>Returns the hash code based on the name.</returns>
		// Token: 0x06003389 RID: 13193 RVA: 0x00033991 File Offset: 0x00031B91
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x0600338A RID: 13194
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern byte[] get_signature_local();

		// Token: 0x0600338B RID: 13195
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern byte[] get_signature_field();

		/// <summary>Adds the end token to the signature and marks the signature as finished, so no further tokens can be added.</summary>
		/// <returns>Returns a byte array made up of the full signature.</returns>
		// Token: 0x0600338C RID: 13196 RVA: 0x000C214C File Offset: 0x000C034C
		public byte[] GetSignature()
		{
			TypeBuilder.ResolveUserTypes(this.arguments);
			SignatureHelper.SignatureHelperType signatureHelperType = this.type;
			if (signatureHelperType == SignatureHelper.SignatureHelperType.HELPER_FIELD)
			{
				return this.get_signature_field();
			}
			if (signatureHelperType == SignatureHelper.SignatureHelperType.HELPER_LOCAL)
			{
				return this.get_signature_local();
			}
			throw new NotImplementedException();
		}

		/// <summary>Returns a string representing the signature arguments.</summary>
		/// <returns>Returns a string representing the arguments of this signature.</returns>
		// Token: 0x0600338D RID: 13197 RVA: 0x000C2185 File Offset: 0x000C0385
		public override string ToString()
		{
			return "SignatureHelper";
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000C218C File Offset: 0x000C038C
		internal static SignatureHelper GetMethodSigHelper(Module mod, CallingConventions callingConvention, CallingConvention unmanagedCallingConvention, Type returnType, Type[] parameters)
		{
			if (mod != null && !(mod is ModuleBuilder))
			{
				throw new ArgumentException("ModuleBuilder is expected");
			}
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			if (returnType.IsUserType)
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			if (parameters != null)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					if (parameters[i].IsUserType)
					{
						throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
					}
				}
			}
			SignatureHelper signatureHelper = new SignatureHelper((ModuleBuilder)mod, SignatureHelper.SignatureHelperType.HELPER_METHOD);
			signatureHelper.returnType = returnType;
			signatureHelper.callConv = callingConvention;
			signatureHelper.unmanagedCallConv = unmanagedCallingConvention;
			if (parameters != null)
			{
				signatureHelper.arguments = new Type[parameters.Length];
				for (int j = 0; j < parameters.Length; j++)
				{
					signatureHelper.arguments[j] = parameters[j];
				}
			}
			return signatureHelper;
		}

		// Token: 0x04001AF3 RID: 6899
		private ModuleBuilder module;

		// Token: 0x04001AF4 RID: 6900
		private Type[] arguments;

		// Token: 0x04001AF5 RID: 6901
		private SignatureHelper.SignatureHelperType type;

		// Token: 0x04001AF6 RID: 6902
		private Type returnType;

		// Token: 0x04001AF7 RID: 6903
		private CallingConventions callConv;

		// Token: 0x04001AF8 RID: 6904
		private CallingConvention unmanagedCallConv;

		// Token: 0x04001AF9 RID: 6905
		private Type[][] modreqs;

		// Token: 0x04001AFA RID: 6906
		private Type[][] modopts;

		// Token: 0x0200067F RID: 1663
		internal enum SignatureHelperType
		{
			// Token: 0x04001AFC RID: 6908
			HELPER_FIELD,
			// Token: 0x04001AFD RID: 6909
			HELPER_LOCAL,
			// Token: 0x04001AFE RID: 6910
			HELPER_METHOD,
			// Token: 0x04001AFF RID: 6911
			HELPER_PROPERTY
		}
	}
}
