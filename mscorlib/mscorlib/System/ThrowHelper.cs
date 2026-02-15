using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000178 RID: 376
	[StackTraceHidden]
	internal static class ThrowHelper
	{
		// Token: 0x06000D6E RID: 3438 RVA: 0x000397FD File Offset: 0x000379FD
		internal static void ThrowArgumentNullException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentNullException(argument);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00039805 File Offset: 0x00037A05
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(argument.ToString());
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x00039819 File Offset: 0x00037A19
		internal static void ThrowArrayTypeMismatchException()
		{
			throw ThrowHelper.CreateArrayTypeMismatchException();
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00039820 File Offset: 0x00037A20
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArrayTypeMismatchException()
		{
			return new ArrayTypeMismatchException();
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00039827 File Offset: 0x00037A27
		internal static void ThrowArgumentException_DestinationTooShort()
		{
			throw ThrowHelper.CreateArgumentException_DestinationTooShort();
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0003982E File Offset: 0x00037A2E
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentException_DestinationTooShort()
		{
			return new ArgumentException("Destination is too short.");
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0003983A File Offset: 0x00037A3A
		internal static void ThrowIndexOutOfRangeException()
		{
			throw ThrowHelper.CreateIndexOutOfRangeException();
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00039841 File Offset: 0x00037A41
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateIndexOutOfRangeException()
		{
			return new IndexOutOfRangeException();
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00039848 File Offset: 0x00037A48
		internal static void ThrowArgumentOutOfRangeException()
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException();
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0003984F File Offset: 0x00037A4F
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException()
		{
			return new ArgumentOutOfRangeException();
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00039856 File Offset: 0x00037A56
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument)
		{
			throw ThrowHelper.CreateArgumentOutOfRangeException(argument);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0003985E File Offset: 0x00037A5E
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateArgumentOutOfRangeException(ExceptionArgument argument)
		{
			return new ArgumentOutOfRangeException(argument.ToString());
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00039872 File Offset: 0x00037A72
		internal static void ThrowObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			throw ThrowHelper.CreateObjectDisposedException_ArrayMemoryPoolBuffer();
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00039879 File Offset: 0x00037A79
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateObjectDisposedException_ArrayMemoryPoolBuffer()
		{
			return new ObjectDisposedException("ArrayMemoryPoolBuffer");
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00039885 File Offset: 0x00037A85
		internal static void ThrowNotSupportedException()
		{
			throw ThrowHelper.CreateThrowNotSupportedException();
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0003988C File Offset: 0x00037A8C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Exception CreateThrowNotSupportedException()
		{
			return new NotSupportedException();
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00039893 File Offset: 0x00037A93
		internal static void ThrowWrongKeyTypeArgumentException(object key, Type targetType)
		{
			throw new ArgumentException(Environment.GetResourceString("The value \"{0}\" is not of type \"{1}\" and cannot be used in this generic collection.", new object[] { key, targetType }), "key");
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000398B7 File Offset: 0x00037AB7
		internal static void ThrowWrongValueTypeArgumentException(object value, Type targetType)
		{
			throw new ArgumentException(Environment.GetResourceString("The value \"{0}\" is not of type \"{1}\" and cannot be used in this generic collection.", new object[] { value, targetType }), "value");
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000398DB File Offset: 0x00037ADB
		internal static void ThrowArgumentException(ExceptionResource resource)
		{
			throw new ArgumentException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000398ED File Offset: 0x00037AED
		internal static void ThrowArgumentException(ExceptionResource resource, ExceptionArgument argument)
		{
			throw new ArgumentException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)), ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00039905 File Offset: 0x00037B05
		internal static void ThrowArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
		{
			if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), string.Empty);
			}
			throw new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00039935 File Offset: 0x00037B35
		internal static void ThrowInvalidOperationException(ExceptionResource resource)
		{
			throw new InvalidOperationException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00039947 File Offset: 0x00037B47
		internal static void ThrowSerializationException(ExceptionResource resource)
		{
			throw new SerializationException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00039959 File Offset: 0x00037B59
		internal static void ThrowNotSupportedException(ExceptionResource resource)
		{
			throw new NotSupportedException(Environment.GetResourceString(ThrowHelper.GetResourceName(resource)));
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0003996B File Offset: 0x00037B6B
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion()
		{
			throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00039977 File Offset: 0x00037B77
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen()
		{
			throw new InvalidOperationException("Enumeration has either not started or has already finished.");
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00039983 File Offset: 0x00037B83
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumNotStarted()
		{
			throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0003998F File Offset: 0x00037B8F
		internal static void ThrowInvalidOperationException_InvalidOperation_EnumEnded()
		{
			throw new InvalidOperationException("Enumeration already finished.");
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0003999B File Offset: 0x00037B9B
		internal static void ThrowInvalidOperationException_InvalidOperation_NoValue()
		{
			throw new InvalidOperationException("Nullable object must have a value.");
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x000399A7 File Offset: 0x00037BA7
		private static ArgumentOutOfRangeException GetArgumentOutOfRangeException(ExceptionArgument argument, string resource)
		{
			return new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), resource);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000399B5 File Offset: 0x00037BB5
		internal static void ThrowArgumentOutOfRange_IndexException()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.index, "Index was out of range. Must be non-negative and less than the size of the collection.");
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x000399C3 File Offset: 0x00037BC3
		internal static void ThrowIndexArgumentOutOfRange_NeedNonNegNumException()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.index, "Non-negative number required.");
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000399D1 File Offset: 0x00037BD1
		internal static void ThrowArgumentException_Argument_InvalidArrayType()
		{
			throw new ArgumentException("Target array type is not compatible with the type of items in the collection.");
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000399DD File Offset: 0x00037BDD
		private static ArgumentException GetAddingDuplicateWithKeyArgumentException(object key)
		{
			return new ArgumentException(SR.Format("An item with the same key has already been added. Key: {0}", key));
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000399EF File Offset: 0x00037BEF
		internal static void ThrowAddingDuplicateWithKeyArgumentException(object key)
		{
			throw ThrowHelper.GetAddingDuplicateWithKeyArgumentException(key);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000399F7 File Offset: 0x00037BF7
		private static KeyNotFoundException GetKeyNotFoundException(object key)
		{
			throw new KeyNotFoundException(SR.Format("The given key '{0}' was not present in the dictionary.", key.ToString()));
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00039A0E File Offset: 0x00037C0E
		internal static void ThrowKeyNotFoundException(object key)
		{
			throw ThrowHelper.GetKeyNotFoundException(key);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x00039A16 File Offset: 0x00037C16
		internal static void ThrowInvalidTypeWithPointersNotSupported(Type targetType)
		{
			throw new ArgumentException(SR.Format("Cannot use type '{0}'. Only value types without pointers or references are supported.", targetType));
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x00039A28 File Offset: 0x00037C28
		internal static void ThrowInvalidOperationException_ConcurrentOperationsNotSupported()
		{
			throw ThrowHelper.GetInvalidOperationException("Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.");
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00039A34 File Offset: 0x00037C34
		internal static InvalidOperationException GetInvalidOperationException(string str)
		{
			return new InvalidOperationException(str);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00039A3C File Offset: 0x00037C3C
		internal static void ThrowArraySegmentCtorValidationFailedExceptions(Array array, int offset, int count)
		{
			throw ThrowHelper.GetArraySegmentCtorValidationFailedException(array, offset, count);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00039A46 File Offset: 0x00037C46
		private static Exception GetArraySegmentCtorValidationFailedException(Array array, int offset, int count)
		{
			if (array == null)
			{
				return ThrowHelper.GetArgumentNullException(ExceptionArgument.array);
			}
			if (offset < 0)
			{
				return ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.offset, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				return ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			return ThrowHelper.GetArgumentException(ExceptionResource.Argument_InvalidOffLen);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x00039A73 File Offset: 0x00037C73
		private static ArgumentException GetArgumentException(ExceptionResource resource)
		{
			return new ArgumentException(resource.ToString());
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00039A87 File Offset: 0x00037C87
		private static ArgumentNullException GetArgumentNullException(ExceptionArgument argument)
		{
			return new ArgumentNullException(ThrowHelper.GetArgumentName(argument));
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00039A94 File Offset: 0x00037C94
		internal static void IfNullAndNullsAreIllegalThenThrow<T>(object value, ExceptionArgument argName)
		{
			if (value == null && default(T) != null)
			{
				ThrowHelper.ThrowArgumentNullException(argName);
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00039ABC File Offset: 0x00037CBC
		internal static string GetArgumentName(ExceptionArgument argument)
		{
			string text;
			switch (argument)
			{
			case ExceptionArgument.obj:
				text = "obj";
				break;
			case ExceptionArgument.dictionary:
				text = "dictionary";
				break;
			case ExceptionArgument.dictionaryCreationThreshold:
				text = "dictionaryCreationThreshold";
				break;
			case ExceptionArgument.array:
				text = "array";
				break;
			case ExceptionArgument.info:
				text = "info";
				break;
			case ExceptionArgument.key:
				text = "key";
				break;
			case ExceptionArgument.collection:
				text = "collection";
				break;
			case ExceptionArgument.list:
				text = "list";
				break;
			case ExceptionArgument.match:
				text = "match";
				break;
			case ExceptionArgument.converter:
				text = "converter";
				break;
			case ExceptionArgument.queue:
				text = "queue";
				break;
			case ExceptionArgument.stack:
				text = "stack";
				break;
			case ExceptionArgument.capacity:
				text = "capacity";
				break;
			case ExceptionArgument.index:
				text = "index";
				break;
			case ExceptionArgument.startIndex:
				text = "startIndex";
				break;
			case ExceptionArgument.value:
				text = "value";
				break;
			case ExceptionArgument.count:
				text = "count";
				break;
			case ExceptionArgument.arrayIndex:
				text = "arrayIndex";
				break;
			case ExceptionArgument.name:
				text = "name";
				break;
			case ExceptionArgument.mode:
				text = "mode";
				break;
			case ExceptionArgument.item:
				text = "item";
				break;
			case ExceptionArgument.options:
				text = "options";
				break;
			case ExceptionArgument.view:
				text = "view";
				break;
			case ExceptionArgument.sourceBytesToCopy:
				text = "sourceBytesToCopy";
				break;
			default:
				return string.Empty;
			}
			return text;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00039C15 File Offset: 0x00037E15
		private static ArgumentOutOfRangeException GetArgumentOutOfRangeException(ExceptionArgument argument, ExceptionResource resource)
		{
			return new ArgumentOutOfRangeException(ThrowHelper.GetArgumentName(argument), resource.ToString());
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00039C2F File Offset: 0x00037E2F
		internal static void ThrowStartIndexArgumentOutOfRange_ArgumentOutOfRange_Index()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00039C3A File Offset: 0x00037E3A
		internal static void ThrowCountArgumentOutOfRange_ArgumentOutOfRange_Count()
		{
			throw ThrowHelper.GetArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_Count);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00039C48 File Offset: 0x00037E48
		internal static string GetResourceName(ExceptionResource resource)
		{
			string text;
			switch (resource)
			{
			case ExceptionResource.Argument_ImplementIComparable:
				text = "At least one object must implement IComparable.";
				break;
			case ExceptionResource.Argument_InvalidType:
				text = "The type of arguments passed into generic comparer methods is invalid.";
				break;
			case ExceptionResource.Argument_InvalidArgumentForComparison:
				text = "Type of argument is not compatible with the generic comparer.";
				break;
			case ExceptionResource.Argument_InvalidRegistryKeyPermissionCheck:
				text = "The specified RegistryKeyPermissionCheck value is invalid.";
				break;
			case ExceptionResource.ArgumentOutOfRange_NeedNonNegNum:
				text = "Non-negative number required.";
				break;
			case ExceptionResource.Arg_ArrayPlusOffTooSmall:
				text = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";
				break;
			case ExceptionResource.Arg_NonZeroLowerBound:
				text = "The lower bound of target array must be zero.";
				break;
			case ExceptionResource.Arg_RankMultiDimNotSupported:
				text = "Only single dimensional arrays are supported for the requested action.";
				break;
			case ExceptionResource.Arg_RegKeyDelHive:
				text = "Cannot delete a registry hive's subtree.";
				break;
			case ExceptionResource.Arg_RegKeyStrLenBug:
				text = "Registry key names should not be greater than 255 characters.";
				break;
			case ExceptionResource.Arg_RegSetStrArrNull:
				text = "RegistryKey.SetValue does not allow a String[] that contains a null String reference.";
				break;
			case ExceptionResource.Arg_RegSetMismatchedKind:
				text = "The type of the value object did not match the specified RegistryValueKind or the object could not be properly converted.";
				break;
			case ExceptionResource.Arg_RegSubKeyAbsent:
				text = "Cannot delete a subkey tree because the subkey does not exist.";
				break;
			case ExceptionResource.Arg_RegSubKeyValueAbsent:
				text = "No value exists with that name.";
				break;
			case ExceptionResource.Argument_AddingDuplicate:
				text = "An item with the same key has already been added.";
				break;
			case ExceptionResource.Serialization_InvalidOnDeser:
				text = "OnDeserialization method was called while the object was not being deserialized.";
				break;
			case ExceptionResource.Serialization_MissingKeys:
				text = "The Keys for this Hashtable are missing.";
				break;
			case ExceptionResource.Serialization_NullKey:
				text = "One of the serialized keys is null.";
				break;
			case ExceptionResource.Argument_InvalidArrayType:
				text = "Target array type is not compatible with the type of items in the collection.";
				break;
			case ExceptionResource.NotSupported_KeyCollectionSet:
				text = "Mutating a key collection derived from a dictionary is not allowed.";
				break;
			case ExceptionResource.NotSupported_ValueCollectionSet:
				text = "Mutating a value collection derived from a dictionary is not allowed.";
				break;
			case ExceptionResource.ArgumentOutOfRange_SmallCapacity:
				text = "capacity was less than the current size.";
				break;
			case ExceptionResource.ArgumentOutOfRange_Index:
				text = "Index was out of range. Must be non-negative and less than the size of the collection.";
				break;
			case ExceptionResource.Argument_InvalidOffLen:
				text = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";
				break;
			case ExceptionResource.Argument_ItemNotExist:
				text = "The specified item does not exist in this KeyedCollection.";
				break;
			case ExceptionResource.ArgumentOutOfRange_Count:
				text = "Count must be positive and count must refer to a location within the string/array/collection.";
				break;
			case ExceptionResource.ArgumentOutOfRange_InvalidThreshold:
				text = "The specified threshold for creating dictionary is out of range.";
				break;
			case ExceptionResource.ArgumentOutOfRange_ListInsert:
				text = "Index must be within the bounds of the List.";
				break;
			case ExceptionResource.NotSupported_ReadOnlyCollection:
				text = "Collection is read-only.";
				break;
			case ExceptionResource.InvalidOperation_CannotRemoveFromStackOrQueue:
				text = "Removal is an invalid operation for Stack or Queue.";
				break;
			case ExceptionResource.InvalidOperation_EmptyQueue:
				text = "Queue empty.";
				break;
			case ExceptionResource.InvalidOperation_EnumOpCantHappen:
				text = "Enumeration has either not started or has already finished.";
				break;
			case ExceptionResource.InvalidOperation_EnumFailedVersion:
				text = "Collection was modified; enumeration operation may not execute.";
				break;
			case ExceptionResource.InvalidOperation_EmptyStack:
				text = "Stack empty.";
				break;
			case ExceptionResource.ArgumentOutOfRange_BiggerThanCollection:
				text = "Larger than collection size.";
				break;
			case ExceptionResource.InvalidOperation_EnumNotStarted:
				text = "Enumeration has not started. Call MoveNext.";
				break;
			case ExceptionResource.InvalidOperation_EnumEnded:
				text = "Enumeration already finished.";
				break;
			case ExceptionResource.NotSupported_SortedListNestedWrite:
				text = "This operation is not supported on SortedList nested types because they require modifying the original SortedList.";
				break;
			case ExceptionResource.InvalidOperation_NoValue:
				text = "Nullable object must have a value.";
				break;
			case ExceptionResource.InvalidOperation_RegRemoveSubKey:
				text = "Registry key has subkeys and recursive removes are not supported by this method.";
				break;
			case ExceptionResource.Security_RegistryPermission:
				text = "Requested registry access is not allowed.";
				break;
			case ExceptionResource.UnauthorizedAccess_RegistryNoWrite:
				text = "Cannot write to the registry key.";
				break;
			case ExceptionResource.ObjectDisposed_RegKeyClosed:
				text = "Cannot access a closed registry key.";
				break;
			case ExceptionResource.NotSupported_InComparableType:
				text = "A type must implement IComparable<T> or IComparable to support comparison.";
				break;
			case ExceptionResource.Argument_InvalidRegistryOptionsCheck:
				text = "The specified RegistryOptions value is invalid.";
				break;
			case ExceptionResource.Argument_InvalidRegistryViewCheck:
				text = "The specified RegistryView value is invalid.";
				break;
			default:
				return string.Empty;
			}
			return text;
		}
	}
}
