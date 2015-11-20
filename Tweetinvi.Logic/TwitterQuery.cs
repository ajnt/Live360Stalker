﻿using System;
using System.Collections.Generic;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces.Models;
using Tweetinvi.Core.Interfaces.WebLogic;

namespace Tweetinvi.Logic
{
    public class TwitterQuery : ITwitterQuery
    {
        public TwitterQuery(string queryURL, HttpMethod httpMethod)
        {
            QueryURL = queryURL;
            HttpMethod = httpMethod;
        }

        public string QueryURL { get; set; }
        public HttpMethod HttpMethod { get; set; }

        public string Proxy { get; set; }

        private TimeSpan _timeout;
        public TimeSpan Timeout
        {
            get { return _timeout; }
            set
            {
                if (value.TotalMilliseconds <= 0)
                {
                    _timeout = TimeSpan.FromSeconds(10);
                    return;
                }

                _timeout = value;
            }
        }

        public ITwitterCredentials TwitterCredentials { get; set; }
        public IEnumerable<IOAuthQueryParameter> QueryParameters { get; set; }

        /// <summary>
        /// Date at which the Twitter query will be ready to be executed
        /// </summary>
        public DateTime? DateWhenCredentialsWillHaveRequiredRateLimits { get; set; }

        public int? TimeToWaitBeforeExecutingTheQueryInMilliSeconds
        {
            get
            {
                if (DateWhenCredentialsWillHaveRequiredRateLimits == null)
                {
                    return null;
                }

                var timeToWait = DateWhenCredentialsWillHaveRequiredRateLimits.Value.Subtract(DateTime.Now).TotalMilliseconds;
                return (int)Math.Max(0, timeToWait);
            }
        }

        public ITwitterQuery Clone()
        {
            var clone = new TwitterQuery(QueryURL, HttpMethod)
            {
                TwitterCredentials = TwitterCredentials, 
                QueryParameters = QueryParameters
            };

            return clone;
        }
    }
}