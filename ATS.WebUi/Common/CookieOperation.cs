using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATS.BusinessEntity;

namespace ATS.WebUi.Common
{
    public static class CookieOperation
    {
        /// <summary>
        /// Creates a new cookie and adds it to the response stream
        /// </summary>
        /// <param name="key">Key identifying the cookie</param>
        /// <param name="data">Text to be added to the cookie</param>
        /// <param name="days">Number of days to persist the cookie.
        /// Set to 0 to create a session cookie</param>
        /// <param name="response">HTTPResponse object</param>
        /// <param name="request">HttpRequest Object</param>
        /// <param name="RequestType">RequestType Object</param>
        public static void CookieBasedOnRequest(string key, string data, int minutes, HttpResponseBase response, HttpRequestBase request)
        {
            if (CookieExists(key, request))
            {
                RemoveCookie(key, response, request);
            }
            CreateCookie(key, data, minutes, response);
        }

        /// <summary>
        /// Creates a new cookie and adds it to the response stream
        /// </summary>
        /// <param name="key">Key identifying the cookie</param>
        /// <param name="minutes">Number of days to persist the cookie.
        /// Set to 0 to create a session cookie</param>
        /// <param name="request">HttpRequest Object</param>
        private static void RenewCookieExpire(string key, int minutes, HttpRequestBase request)
        {
            //Renew Cookie
            if (minutes > 0)
            {
                request.Cookies[key].Expires = DateTime.Now.AddMinutes(minutes);
            }
        }

        /// <summary>
        /// Creates a new cookie and adds it to the response stream
        /// </summary>
        /// <param name="key">Key identifying the cookie</param>
        /// <param name="data">Text to be added to the cookie</param>
        /// <param name="minutes">Number of days to persist the cookie.
        /// Set to 0 to create a session cookie</param>
        /// <param name="response">HTTPResponse object</param>
        private static void CreateCookie(string key, string data, int minutes, HttpResponseBase response)
        {
            //check that the response object is valid
            if (response == null) return;
            //check key is valid
            if (string.IsNullOrEmpty(key)) return;
            //create new cookie object
            HttpCookie cookie = new HttpCookie(key);
            //if days is 0 then a session cookie is created
            //which will be discarded when the browser closes
            if (minutes > 0)
            {
                //set the expiry date to x days in the future
                //this could be changed to minutes for more accuracy
                cookie.Expires = DateTime.Now.AddMinutes(minutes);
            }
            //set the data on the cookie
            cookie.Value = ATS.HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(data);
            //update response with new cookie
            response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Removes a cookie from the cookies collection
        /// </summary>
        /// <param name="key">Key identifying the cookie</param>
        /// <param name="response">HTTPRepsone Object</param>
        /// <param name="request">HttpRequest Object</param>
        public static void RemoveCookie(string key, HttpResponseBase response, HttpRequestBase request)
        {
            //check that the request object is valid
            if (request == null) return;
            //check that the response object is valid
            if (response == null) return;
            //check key is passed in
            if (string.IsNullOrEmpty(key)) return;

            //check if the cookie exists
            if (request.Cookies[key] != null)
            {
                //create a new cookie to replace the current cookie
                HttpCookie newCookie = new HttpCookie(key);
                //set the new cookie to expire 1 day ago
                newCookie.Expires = DateTime.Now.AddDays(-1d);
                //update the cookies collection on the response
                response.Cookies.Add(newCookie);
            }
        }


        /// <summary>
        /// Checks to see if a cookie exists
        /// </summary>
        /// <param name="key">Key identifying the cookie</param>
        /// <param name="request">HttpRequest Object</param>
        /// <returns>True if the key exists in the cookies collection</returns>
        public static bool CookieExists(string key, HttpRequestBase request)
        {
            //check that the request object is valid
            if (request == null) return false;
            //check if key added
            if (string.IsNullOrEmpty(key)) return false;
            //check to see if key exists in cookies collection
            return request.Cookies[key] != null;
        }

        /// <summary>
        /// Gets the text stored in a cookie
        /// </summary>
        /// <param name="key">Key identifying the cookie</param>
        /// <param name="request">HttpRequest Object</param>
        /// <returns>Text stored in cookied</returns>
        public static string ReadCookie(string key, HttpRequestBase request)
        {
            //check that the request object is valid
            if (request == null) return "";
            //check if key is valid
            if (string.IsNullOrEmpty(key)) return null;
            //retrieve cookie from request
            HttpCookie cookie = request.Cookies[key];
            //check if a cookie was retrieved
            if (cookie == null) return null;
            //extract cookie data
            return ATS.HelperLibrary.Encryption.EncryptionAlgo.DecryptData(cookie.Value);
        }

    }

}