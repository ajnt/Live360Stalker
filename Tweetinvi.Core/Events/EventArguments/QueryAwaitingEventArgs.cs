﻿using System;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Interfaces.Credentials;
using Tweetinvi.Core.Interfaces.WebLogic;

namespace Tweetinvi.Core.Events.EventArguments
{
    public class QueryAwaitingEventArgs : EventArgs
    {
        private readonly string _query;
        private readonly ITokenRateLimit _queryRateLimit;
        private readonly ITwitterCredentials _twitterCredentials;

        public QueryAwaitingEventArgs(
            string query,
            ITokenRateLimit queryRateLimit,
            ITwitterCredentials twitterCredentials)
        {
            _query = query;
            _queryRateLimit = queryRateLimit;
            _twitterCredentials = twitterCredentials;
        }

        public string Query { get { return _query; } }
        public ITokenRateLimit QueryRateLimit { get { return _queryRateLimit; } }
        public ITwitterCredentials Credentials { get { return _twitterCredentials; } }

        public DateTime ResetDateTime { get { return _queryRateLimit.ResetDateTime; } }
        public int ResetInMilliseconds { get { return ((int)_queryRateLimit.ResetDateTimeInMilliseconds); } }
    }
}