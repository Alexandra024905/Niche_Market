using NUnit.Framework;
using System;
using System.Reflection;

namespace NicheMarket.Services.Tests
{
    static class TestsMessages
    {

        internal static string MappingErrorMessage(string methodName, string property)
        {
            return $"{methodName} method does not map {property} property correctly.";
        }

        internal static string ResultErrorMessage(string methodName)
        {
            return $"{methodName} method should return truthy value with valid data.";
        }

        internal static string InvalidValueErrorMessage(string methodName)
        {
            return $"{methodName} method should return truthy value with valid data.";
        }

        internal static string SetPropertyIncorrectlyErrorMessage(string methodName, string property)
        {
            return $"{methodName} method does not set {property} property correctly.";
        }  
        internal static string DoesNotCalculateCorrectlyErrorMessage(string methodName, string property)
        {
            return $"{methodName}method does not calculate the value of {property} property correctly.";
        }   
        
        internal static string ReturnsTrueWhenFalseIsExpected(string methodName)
        {
            return $"{methodName} method should return \"false\" with invalid data.";
        }
    }
}