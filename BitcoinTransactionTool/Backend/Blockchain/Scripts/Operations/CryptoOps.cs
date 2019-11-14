﻿// Bitcoin Transaction Tool
// Copyright (c) 2017 Coding Enthusiast
// Distributed under the MIT software license, see the accompanying
// file LICENCE or http://www.opensource.org/licenses/mit-license.php.

using BitcoinTransactionTool.Backend.Cryptography.Hashing;

namespace BitcoinTransactionTool.Backend.Blockchain.Scripts.Operations
{
    /// <summary>
    /// Base (abstract) class for cryptography operations. Implements <see cref="BaseOperation"/> and 
    /// <see cref="IOperation.Run(IOpData, out string)"/>. Derived classes must set the <see cref="Hash"/> property.
    /// </summary>
    public abstract class CryptoOp : BaseOperation
    {
        protected abstract IHashFunction Hash { get; }

        /// <summary>
        /// Replaces top stack item with its hash digest. Return value indicates success.
        /// </summary>
        /// <param name="opData">Data to use</param>
        /// <param name="error">Error message (null if sucessful, otherwise will contain information about the failure)</param>
        /// <returns>True if operation was successful, false if otherwise</returns>
        public override bool Run(IOpData opData, out string error)
        {
            if (opData.ItemCount < 1)
            {
                error = "Not enough items in stack.";
                return false;
            }

            opData.Push(Hash.ComputeHash(opData.Pop()));

            error = null;
            return true;
        }
    }



    /// <summary>
    /// Operation to perform RIPEMD-160 hash on top stack item.
    /// </summary>
    public class RipeMd160Op : CryptoOp
    {
        public sealed override OP OpValue => OP.RIPEMD160;
        protected sealed override IHashFunction Hash => new Ripemd160();
    }


    /// <summary>
    /// Operation to perform SHA-1 hash on top stack item.
    /// </summary>
    public class Sha1Op : CryptoOp
    {
        public sealed override OP OpValue => OP.SHA1;
        protected sealed override IHashFunction Hash => new Sha1();
    }


    /// <summary>
    /// Operation to perform SHA-256 hash on top stack item.
    /// </summary>
    public class Sha256Op : CryptoOp
    {
        public sealed override OP OpValue => OP.SHA256;
        protected sealed override IHashFunction Hash => new Sha256(false);
    }


    /// <summary>
    /// Operation to perform SHA-256 then RIPEMD-160 hash on top stack item.
    /// </summary>
    public class Hash160Op : CryptoOp
    {
        public sealed override OP OpValue => OP.HASH160;
        protected sealed override IHashFunction Hash => new Ripemd160Sha256();
    }


    /// <summary>
    /// Operation to perform SHA-256 hash twice on top stack item.
    /// </summary>
    public class Hash256Op : CryptoOp
    {
        public sealed override OP OpValue => OP.HASH256;
        protected sealed override IHashFunction Hash => new Sha256(true);
    }
}
