using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace eimprovement.WebApplication.Client
{
    public class PetStoreApiException : Exception
    {
        public PetStoreApiException()
        {
        }

        public PetStoreApiException(string message) : base(message)
        {
        }

        public PetStoreApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}