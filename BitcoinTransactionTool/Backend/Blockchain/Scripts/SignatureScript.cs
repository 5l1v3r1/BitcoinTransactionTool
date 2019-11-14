﻿// Bitcoin Transaction Tool
// Copyright (c) 2017 Coding Enthusiast
// Distributed under the MIT software license, see the accompanying
// file LICENCE or http://www.opensource.org/licenses/mit-license.php.

using BitcoinTransactionTool.Backend.Blockchain.Scripts.Operations;
using System;

namespace BitcoinTransactionTool.Backend.Blockchain.Scripts
{
    public class SignatureScript : Script, ISignatureScript
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SignatureScript"/> using given parameters of the <see cref="ICoin"/>.
        /// </summary>
        public SignatureScript()
        {
            IsWitness = false;
            OperationList = new IOperation[0];
            ScriptType = ScriptType.ScriptSig;

            redeemBuilder = new RedeemScript();
            maxLenOrCount = 10000; // TODO: set this to a real value and from ICoin
        }



        private RedeemScript redeemBuilder;



        ///// <summary>
        ///// Sets this script to a single signature used for spending a "pay to pubkey" output,
        ///// using the given <see cref="Signature"/>.
        ///// </summary>
        ///// <exception cref="ArgumentNullException"/>
        ///// <param name="sig">Signature to use (must have its <see cref="SigHashType"/> set)</param>
        //public void SetToP2PK(Signature sig)
        //{
        //    if (sig == null)
        //        throw new ArgumentNullException(nameof(sig), "Signature can not be null.");


        //    byte[] sigBa = sig.EncodeWithSigHashType();

        //    OperationList = new IOperation[]
        //    {
        //        new PushDataOp(sigBa),
        //    };
        //}


        ///// <summary>
        ///// Sets this script to a single signature used for spending a "pay to pubkey hash" output,
        ///// using the given <see cref="Signature"/> and the <see cref="PublicKey"/> with its compression type.
        ///// </summary>
        ///// <exception cref="ArgumentNullException"/>
        ///// <param name="sig">Signature to use (must have its <see cref="SigHashType"/> set)</param>
        ///// <param name="pubKey">Public key to use </param>
        ///// <param name="useCompressed">Indicates whether to use the compressed or uncompressed public key</param>
        //public void SetToP2PKH(Signature sig, PublicKey pubKey, bool useCompressed)
        //{
        //    if (sig == null)
        //        throw new ArgumentNullException(nameof(sig), "Signature can not be null.");
        //    if (pubKey == null)
        //        throw new ArgumentNullException(nameof(pubKey), "Public key can not be null.");


        //    byte[] sigBa = sig.EncodeWithSigHashType();
        //    byte[] pubkBa = pubKey.ToByteArray(useCompressed);

        //    OperationList = new IOperation[]
        //    {
        //        new PushDataOp(sigBa),
        //        new PushDataOp(pubkBa)
        //    };
        //}


        ///// <summary>
        ///// Initializes this instance with the given <see cref="RedeemScriptType.MultiSig"/> type <see cref="RedeemScript"/>
        ///// to be ready for addition of signatures individually.
        ///// </summary>
        ///// <exception cref="ArgumentNullException"/>
        ///// <param name="redeem"><see cref="RedeemScript"/> to use</param>
        //public void InitializeForMultiSig(RedeemScript redeem)
        //{
        //    if (redeem == null)
        //        throw new ArgumentNullException(nameof(redeem), "Redeem script can not be null.");
        //    if (redeem.GetRedeemScriptType() != RedeemScriptType.MultiSig)
        //        throw new ArgumentException("Invalid redeem script type.");

        //    // OP_m | pub1 | pub2 | ... | pub(n) | OP_n | OP_CheckMultiSig
        //    // TODO: set a field for isStrict
        //    if (!((PushDataOp)redeem.OperationList[0]).TryGetNumber(true, out long m, out string err))
        //    {
        //        throw new ArgumentException("Invalid push operation at index 0 of redeem script.");
        //    }

        //    if (!((PushDataOp)redeem.OperationList[OperationList.Length - 2]).TryGetNumber(true, out long n, out err))
        //    {
        //        throw new ArgumentException("Invalid push operation at index 0 of redeem script.");
        //    }

        //    // TODO: check n and m to be in range

        //    // OP_0 | Sig1 | sig2 | ... | sig(m) | redeemScript
        //    OperationList = new IOperation[m + 2];
        //    OperationList[0] = new PushDataOp(OP._0);
        //    OperationList[OperationList.Length - 1] = new PushDataOp(redeem.ToByteArray());
        //}

        //public void SetMultiSig_Signature(Signature sig, int sigRank)
        //{
        //    if (sig == null)
        //        throw new ArgumentNullException(nameof(sig), "Signature can not be null.");
        //    if (OperationList == null || OperationList.Length == 0)
        //        throw new ArgumentException("Script must first be initialized using the redeem script.");
        //    if (sigRank < 1 || sigRank > OperationList.Length - 2)
        //        throw new IndexOutOfRangeException(
        //            $"Signature rank must be between 1 (first sig) and {OperationList.Length - 2} (last sig).");


        //    byte[] data = sig.EncodeWithSigHashType();
        //    // Due to a bug in bitcoin-core's implementation of OP_CheckMultiSig, the first operation is always OP_0
        //    OperationList[sigRank] = new PushDataOp(data);
        //}


        //public void SetToMultiSig(Signature[] sigs, RedeemScript redeem)
        //{
        //    InitializeForMultiSig(redeem);
        //    if (sigs.Length != OperationList.Length - 2)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(sigs), "Invalid number of signatures were provided.");
        //    }

        //    for (int i = 0; i < sigs.Length; i++)
        //    {
        //        OperationList[i + 1] = new PushDataOp(sigs[i].EncodeWithSigHashType());
        //    }
        //}


        public void SetToP2SH_P2WPKH(RedeemScript redeem)
        {
            if (redeem == null)
                throw new ArgumentNullException(nameof(redeem), "Redeem script can not be null.");
            if (redeem.GetRedeemScriptType() != RedeemScriptType.P2SH_P2WPKH)
                throw new ArgumentException("Invalid redeem script type.");


            OperationList = new IOperation[]
            {
                new PushDataOp(redeem.ToByteArray())
            };
        }

        //public void SetToP2SH_P2WPKH(PublicKey pubKey)
        //{
        //    if (pubKey == null)
        //        throw new ArgumentNullException(nameof(pubKey), "Public key can not be null.");

        //    redeemBuilder.SetToP2SH_P2WPKH(pubKey);
        //    SetToP2SH_P2WPKH(redeemBuilder);
        //}


        public void SetToP2SH_P2WSH(RedeemScript redeem)
        {
            if (redeem == null)
                throw new ArgumentNullException(nameof(redeem), "Redeem script can not be null.");
            if (redeem.GetRedeemScriptType() != RedeemScriptType.P2SH_P2WSH)
                throw new ArgumentException("Invalid redeem script type.");


            OperationList = new IOperation[]
            {
                new PushDataOp(redeem.ToByteArray())
            };
        }


    }
}
