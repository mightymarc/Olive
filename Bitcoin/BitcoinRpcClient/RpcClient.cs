// -----------------------------------------------------------------------
// <copyright file="RpcClient.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Olive.Bitcoin
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// RPC client to access the Bitcoin Daemon JSON RPC interface.
    /// </summary>
    public class RpcClient : IRpcClient
    {
        /// <summary>
        /// Gets or sets the port number used to connect to the service.
        /// </summary>
        /// <value>
        /// The port number.
        /// </value>
        public int PortNumber { get; set; }

        /// <summary>
        /// Gets or sets the credentials used to connect to the service.
        /// </summary>
        /// <value>
        /// The credential.
        /// </value>
        public NetworkCredential Credential { get; set; }

        /// <summary>
        /// Gets or sets the hostname used to connect to the service.
        /// </summary>
        /// <value>
        /// The hostname.
        /// </value>
        public string Hostname { get; set; }

        public JToken InvokeMethod(string methodName, params object[] methodParameters)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(methodName), "methodName");

            var uri = new Uri("http://" + this.Hostname + ":" + this.PortNumber);

            var webRequest = (HttpWebRequest)WebRequest.Create(uri);
            webRequest.Credentials = this.Credential;
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";

            var requestDataBytes = this.GetRequestDataBytes(methodName, methodParameters);
            webRequest.ContentLength = requestDataBytes.Length;

            // Write the request data.
            using (var dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(requestDataBytes, 0, requestDataBytes.Length);
            }

            // Get the response and read read the response data.
            JObject responseJson = null;

            using (var webResponse = webRequest.GetResponse())
            {
                using (var responseStream = webResponse.GetResponseStream())
                {
                    using (var responseStreamReader = new StreamReader(responseStream))
                    {
                        var responseRaw = responseStreamReader.ReadToEnd();
                        responseJson = JsonConvert.DeserializeObject<JObject>(responseRaw);
                    }
                }
            }

            if (responseJson == null)
            {
                throw new Exception("No JSON object was returned.");
            }

            if (responseJson.Root == null)
            {
                throw new Exception("The response JSON object does not contain a root token.");
            }

            if (responseJson.Root["error"] == null)
            {
                throw new Exception("The response JSON object's root token does not contain an error token.");
            }

            if (responseJson.Root["result"] == null)
            {
                throw new Exception("The response JSON object's root token does not contain a result token.");
            }

            if (responseJson.Root["error"].HasValues)
            {
                // There was an error.
                // TODO: Not sure how to parse these errors.
                throw new Exception("The RPC server reported an error: " + responseJson.Root["error"]);
            }

            return responseJson["result"];
        }

        private byte[] GetRequestDataBytes(string a_sMethod, object[] a_params)
        {
            var requestData = this.CreateRequestJsonData(a_sMethod, a_params);
            var requestDataString = JsonConvert.SerializeObject(requestData);
            return Encoding.UTF8.GetBytes(requestDataString);
        }

        protected JObject CreateRequestJsonData(string methodName, object[] methodArguments)
        {
            var joe = new JObject();
            joe["jsonrpc"] = "1.0";
            joe["id"] = "1";
            joe["method"] = methodName;

            if (methodArguments != null)
            {
                if (methodArguments.Length > 0)
                {
                    var props = new JArray();

                    foreach (var p in methodArguments)
                    {
                        props.Add(p);
                    }

                    joe.Add(new JProperty("params", props));
                }
            }
            return joe;
        }

        public decimal GetAccountBalance(string accountId)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(accountId), "accountId");
            Contract.Ensures(Contract.Result<decimal>() >= 0);

           throw new NotImplementedException();
        }

        public string GetNewAddress(string accountId)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(accountId), "accountId");
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void GetInfo()
        {
            throw new NotImplementedException();
        }

        public bool GetIsOnTestNetwork()
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransactions(string accountId = null, int count = 10, int skip = 0)
        {
            var result = (JArray)this.InvokeMethod("listtransactions", accountId ?? "*", count, skip);

            return (from t in result
                    select
                        new Transaction
                            {
                                Account = t.Value<string>("account"),
                                Address = t.Value<string>("address"),
                                Amount = decimal.Parse(t.Value<string>("amount"), CultureInfo.InvariantCulture),
                                Category = t.Value<string>("category"),
                                Confirmations = int.Parse(t.Value<string>("confirmations"), CultureInfo.InvariantCulture),
                                Time =
                                    new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(
                                        int.Parse(t.Value<string>("time"), CultureInfo.InvariantCulture)),
                                TransactionId = t.Value<string>("txid")
                            }).ToList();
        }

        public void Move(string fromAccountId, string toAccountId, decimal amount)
        {
            throw new NotImplementedException();
        }

        public List<AccountWithBalance> GetAcccounts(int minConfirmations = 1)
        {
            Contract.Ensures(Contract.Result<List<AccountWithBalance>>() != null);

            var result = (JObject)this.InvokeMethod("listaccounts", minConfirmations);

            return (from account in result.Cast<JProperty>()
                    select
                        new AccountWithBalance()
                            {
                                Name = account.Name,
                                Balance = decimal.Parse(account.Value.ToString(), CultureInfo.InvariantCulture)
                            }).ToList(
                                );
        }
    }

    public class Transaction
    {
        public string Account { get; set; }

        public string Address { get; set; }

        public string Category { get; set; }

        public decimal Amount { get; set; }

        public int Confirmations { get; set; }

        public string TransactionId { get; set; }

        public DateTime Time { get; set; }
    }

    public class AccountWithBalance
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }
    }
}
