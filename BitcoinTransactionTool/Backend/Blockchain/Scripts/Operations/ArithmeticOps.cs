﻿// Bitcoin Transaction Tool
// Copyright (c) 2017 Coding Enthusiast
// Distributed under the MIT software license, see the accompanying
// file LICENCE or http://www.opensource.org/licenses/mit-license.php.

namespace BitcoinTransactionTool.Backend.Blockchain.Scripts.Operations
{
    /// <summary>
    /// Base (abstract) class for arithmetic operations that only pop one item from stack and use it as an integer.
    /// </summary>
    public abstract class SingleNumArithmeticOpsBase : BaseOperation
    {
        /// <summary>
        /// The popped item's integer value
        /// </summary>
        protected long a;

        protected bool TrySetValue(IOpData opData, out string error)
        {
            if (opData.ItemCount < 1)
            {
                error = "Not enough items left in the stack.";
                return false;
            }

            if (!OpHelper.TryConvertByteArrayToInt(opData.Pop(), out a, true))
            {
                error = "Invalid number format.";
                return false;
            }

            error = null;
            return true;
        }
    }

    /// <summary>
    /// Base (abstract) class for arithmetic operations that pop two items from stack and use them as an integers.
    /// </summary>
    public abstract class DoubleNumArithmeticOpsBase : BaseOperation
    {
        /// <summary>
        /// The popped items' integer value
        /// </summary>
        protected long a, b;

        protected bool TrySetValues(IOpData opData, out string error)
        {
            if (opData.ItemCount < 2)
            {
                error = "Not enough items left in the stack.";
                return false;
            }

            // Stack is: a then b so we pop b first
            if (!OpHelper.TryConvertByteArrayToInt(opData.Pop(), out b, true))
            {
                error = "Invalid number format.";
                return false;
            }

            if (!OpHelper.TryConvertByteArrayToInt(opData.Pop(), out a, true))
            {
                error = "Invalid number format.";
                return false;
            }

            error = null;
            return true;
        }
    }



    /// <summary>
    /// Operation to add 1 to the top stack item.
    /// </summary>
    public class ADD1Op : SingleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.ADD1;

        /// <summary>
        /// Adds 1 to the top stack item interpreted as a <see cref="long"/>. Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValue(opData, out error))
            {
                return false;
            }

            a += 1;
            opData.Push(OpHelper.IntToByteArray(a));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to subtract 1 from the top stack item.
    /// </summary>
    public class SUB1Op : SingleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.SUB1;

        /// <summary>
        /// Subtracts 1 from the top stack item interpreted as a <see cref="long"/>. Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValue(opData, out error))
            {
                return false;
            }

            a -= 1;
            opData.Push(OpHelper.IntToByteArray(a));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to flip sign of the top stack item.
    /// </summary>
    public class NEGATEOp : SingleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.NEGATE;

        /// <summary>
        /// Flips the sign of the top stack item interpreted as a <see cref="long"/>. Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValue(opData, out error))
            {
                return false;
            }

            a = -a;
            opData.Push(OpHelper.IntToByteArray(a));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to remove negative sign from the top stack item.
    /// </summary>
    public class ABSOp : SingleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.ABS;

        /// <summary>
        /// Replaces top stack item interpreted as a <see cref="long"/> with its absolute value. Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValue(opData, out error))
            {
                return false;
            }

            if (a < 0)
            {
                a = -a;
            }
            opData.Push(OpHelper.IntToByteArray(a));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to replace the top stack item with 0 or 1.
    /// </summary>
    public class NOTOp : SingleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.NOT;

        /// <summary>
        /// Replaces the top stack item interpreted as <see cref="long"/> with 1 if it was 0 otherwise with 0.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValue(opData, out error))
            {
                return false;
            }

            a = (a == 0) ? 1 : 0;
            opData.Push(OpHelper.IntToByteArray(a));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to replace the top stack item with 0 or 1.
    /// </summary>
    public class NotEqual0Op : SingleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.NotEqual0;

        /// <summary>
        /// Replaces the top stack item interpreted as <see cref="long"/> with 0 if it was 0 otherwise with 1.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValue(opData, out error))
            {
                return false;
            }

            a = (a == 0) ? 0 : 1;
            opData.Push(OpHelper.IntToByteArray(a));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to add top two stack items together.
    /// </summary>
    public class AddOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.ADD;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with their sum. Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            opData.Push(OpHelper.IntToByteArray(a + b));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to subtract top two stack items from each other.
    /// </summary>
    public class SUBOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.SUB;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with their subtract result. Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            opData.Push(OpHelper.IntToByteArray(a - b));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to check that top two stack items are not 0.
    /// </summary>
    public class BoolAndOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.BoolAnd;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with 1 if they are both not 0, otherwise with 0.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            int c = (a != 0 && b != 0) ? 1 : 0;
            opData.Push(OpHelper.IntToByteArray(c));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to check that either one of the top two stack items are not 0.
    /// </summary>
    public class BoolOrOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.BoolOr;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with 1 if either one is not 0, otherwise with 0.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            int c = (a != 0 || b != 0) ? 1 : 0;
            opData.Push(OpHelper.IntToByteArray(c));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to check equality of top two stack items.
    /// </summary>
    public class NumEqualOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.NumEqual;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with 1 if they are equal, otherwise with 0.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            int c = (a == b) ? 1 : 0;
            opData.Push(OpHelper.IntToByteArray(c));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to check and verify equality of top two stack items.
    /// </summary>
    public class NumEqualVerifyOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.NumEqualVerify;

        /// <summary>
        /// Removes top two stack items and checks their equality interpreting them as <see cref="long"/>s. 
        /// Passes if they are equal, fails otherwise.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            if (a == b)
            {
                error = null;
                return true;
            }
            else
            {
                error = "Numbers are not equal.";
                return false;
            }
        }
    }


    /// <summary>
    /// Operation to check unequality of top two stack items.
    /// </summary>
    public class NumNotEqualOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.NumNotEqual;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with 1 if they are not equal, otherwise with 0.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            int c = (a != b) ? 1 : 0;
            opData.Push(OpHelper.IntToByteArray(c));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to compare top two stack items.
    /// </summary>
    public class LessThanOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.LessThan;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with 1 if first one was smaller, otherwise with 0.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            int c = (a < b) ? 1 : 0;
            opData.Push(OpHelper.IntToByteArray(c));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to compare top two stack items.
    /// </summary>
    public class GreaterThanOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.GreaterThan;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with 1 if first one was bigger, otherwise with 0.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            int c = (a > b) ? 1 : 0;
            opData.Push(OpHelper.IntToByteArray(c));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to compare top two stack items.
    /// </summary>
    public class LessThanOrEqualOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.LessThanOrEqual;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with 1 if first one was smaller or equal, otherwise with 0.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            int c = (a <= b) ? 1 : 0;
            opData.Push(OpHelper.IntToByteArray(c));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to compare top two stack items.
    /// </summary>
    public class GreaterThanOrEqualOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.GreaterThanOrEqual;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with 1 if first one was bigger or equal, otherwise with 0.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            int c = (a >= b) ? 1 : 0;
            opData.Push(OpHelper.IntToByteArray(c));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to find smaller value between two top stack items.
    /// </summary>
    public class MINOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.MIN;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with the smaller of the two.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            long c = (a < b) ? a : b;
            opData.Push(OpHelper.IntToByteArray(c));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to find bigger value between two top stack items.
    /// </summary>
    public class MAXOp : DoubleNumArithmeticOpsBase
    {
        public override OP OpValue => OP.MAX;

        /// <summary>
        /// Replaces top two stack items interpreted as <see cref="long"/>s with the bigger of the two.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (!TrySetValues(opData, out error))
            {
                return false;
            }

            long c = (a > b) ? a : b;
            opData.Push(OpHelper.IntToByteArray(c));

            error = null;
            return true;
        }
    }


    /// <summary>
    /// Operation to check if the number is within the specified range.
    /// </summary>
    public class WITHINOp : BaseOperation
    {
        public override OP OpValue => OP.WITHIN;

        /// <summary>
        /// Removes top three stack items and converts them to <see cref="long"/>s: value, Min, Max.
        /// Pushes 1 if value was in range, otherwise 0.
        /// Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (opData.ItemCount < 3)
            {
                error = "Invalid number of elements in stack.";
                return false;
            }

            // Stack is: x then min then max so we pop max first
            if (!OpHelper.TryConvertByteArrayToInt(opData.Pop(), out long max, true))
            {
                error = "Couldn't convert to number.";
                return false;
            }
            if (!OpHelper.TryConvertByteArrayToInt(opData.Pop(), out long min, true))
            {
                error = "Couldn't convert to number.";
                return false;
            }
            if (!OpHelper.TryConvertByteArrayToInt(opData.Pop(), out long x, true))
            {
                error = "Couldn't convert to number.";
                return false;
            }

            int c = (x >= min && x < max) ? 1 : 0;
            opData.Push(OpHelper.IntToByteArray(c));

            error = null;
            return true;
        }
    }
}
