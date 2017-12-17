﻿using RaiBlocks.Actions;
using RaiBlocks.Results;
using RaiBlocks.ValueObjects;
using System;
using System.Threading.Tasks;

namespace RaiBlocks
{
    /// <summary>
    /// .NET wrapper for RaiBlocks RPC Protocol.
    /// <see cref="https://github.com/clemahieu/raiblocks/wiki/RPC-protocol"/>
    /// </summary>
    public class RaiBlocksRpc
    {
        Uri _node;

        /// <param name="node">The URI of your node. http://localhost:7076/ by default.</param>
        public RaiBlocksRpc(Uri node)
        {
            _node = node ?? throw new ArgumentNullException(nameof(node));
        }

        /// <param name="url">The URI of your node. http://localhost:7076/ by default.</param>
        public RaiBlocksRpc(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            _node = new Uri(url);
        }

        #region Actions

        /// <summary>
        /// Returns how many XRB (10^30 RAW) is owned and how many have not yet been received by account.
        /// </summary>
        /// <param name="acc">The account to get balance for.</param>
        /// <returns>Balance of <paramref name="acc"/> in XRB. (10^30 RAW).</returns>
        public async Task<BalanceResult> GetBalanceAsync(RaiAddress acc)
        {
            var action = new GetBalance(acc);
            var handler = new ActionHandler<GetBalance, BalanceResult>(_node);
            return await handler.Handle(action);
        }

        /// <summary>
        /// Get number of blocks for a specific account
        /// </summary>
        /// <param name="acc">The account to get block count for.</param>
        /// <returns>Number of blocks on account.</returns>
        public async Task<AccountBlockCountResult> GetAccountBlockCountAsync(RaiAddress acc)
        {
            var action = new GetAccountBlockCount(acc);
            var handler = new ActionHandler<GetAccountBlockCount, AccountBlockCountResult>(_node);
            return await handler.Handle(action);
        }

        /// <summary>
        /// Returns frontier, open block, change representative block, balance, last modified timestamp from local database & block count for account
        /// </summary>
        public async Task<AccountInformationResult> GetAccountInformationAsync(RaiAddress acc)
        {
            var action = new GetAccountInformation(acc);
            var handler = new ActionHandler<GetAccountInformation, AccountInformationResult>(_node);
            return await handler.Handle(action);
        }

        /// <summary>
        /// enable_control required
        // Creates a new account, insert next deterministic key in wallet
        public async Task<CreateAccountResult> CreateAccountAsync(string wallet)
        {
            var action = new CreateAccount(wallet);
            var handler = new ActionHandler<CreateAccount, CreateAccountResult>(_node);
            return await handler.Handle(action);
        }

        /// <summary>
        /// Get account number for the public key
        /// </summary>>
        public async Task<GetAccountByPublicKeyResult> GetAccountByPublicKeyAsync(string key)
        {
            var action = new GetAccountByPublicKey(key);
            var handler = new ActionHandler<GetAccountByPublicKey, GetAccountByPublicKeyResult>(_node);
            return await handler.Handle(action);
        }
        
        #endregion
    }
}