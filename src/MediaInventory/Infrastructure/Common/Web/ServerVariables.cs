using System;
using System.Collections.Generic;
using System.Web;

namespace MediaInventory.Infrastructure.Common.Web
{
    public interface IServerVariables : IDictionary<string, string>
    {
        string AllHttp { get; }
        string AllRaw { get; }
        string AppPoolId { get; }
        string ApplicationMetadataPath { get; }
        string ApplicationPhysicalPath { get; }
        string AuthenticationPassword { get; }
        string AuthenticationType { get; }
        string AuthenticationUser { get; }
        string CacheUrl { get; }
        string CertificateCookie { get; }
        string CertificateFlags { get; }
        string CertificateIssuer { get; }
        string CertificateKeySize { get; }
        string CertificateSecretKeySize { get; }
        string CertificateSerialNumber { get; }
        string CertificateServerIssuer { get; }
        string CertificateServerSubject { get; }
        string CertificateSubject { get; }
        string ContentLength { get; }
        string ContentType { get; }
        string GatewayInterface { get; }
        string HttpAccept { get; }
        string HttpEncoding { get; }
        string HttpAcceptLanguage { get; }
        string HttpConnection { get; }
        string HttpCookie { get; }
        string HttpHost { get; }
        string HttpMethod { get; }
        string HttpReferrer { get; }
        string HttpUrl { get; }
        string HttpUserAgent { get; }
        string HttpVersion { get; }
        string Https { get; }
        string HttpsKeySize { get; }
        string HttpsSecretKeySize { get; }
        string HttpsServerIssuer { get; }
        string HttpsServerSubject { get; }
        string InstanceId { get; }
        string InstanceMetadataPath { get; }
        string LocalAddress { get; }
        string LogonUser { get; }
        string PathInfo { get; }
        string PathTranslated { get; }
        string QueryString { get; }
        string RemoteAddress { get; }
        string RemoteHost { get; }
        string RemotePort { get; }
        string RemoteUser { get; }
        string RequestMethod { get; }
        string ScriptName { get; }
        string ScriptTranslated { get; }
        string ServerName { get; }
        string ServerPort { get; }
        string ServerPortSecure { get; }
        string ServerProtocol { get; }
        string ServerSoftware { get; }
        string SsiExecDisabled { get; }
        string UnencodedUrl { get; }
        string UnmappedRemoteUser { get; }
        string Url { get; }
        string GetUnicodeServerVariableName(string name);

        string Header(string name);
    }

    public class ServerVariables : NameValueCollectionAdapterBase, IServerVariables
    {
        public ServerVariables()
            : base(() => HttpContext.Current != null ? HttpContext.Current.Request.ServerVariables : null) { }

        public const string AllHttpHeader = "ALL_HTTP";
        public const string AllRawHeader = "ALL_RAW";
        public const string AppPoolIdHeader = "APP_POOL_ID";
        public const string ApplicationMetadataPathHeader = "APPL_MD_PATH";
        public const string ApplicationPhysicalPathHeader = "APPL_PHYSICAL_PATH";
        public const string AuthenticationPasswordHeader = "AUTH_PASSWORD";
        public const string AuthenticationTypeHeader = "AUTH_TYPE";
        public const string AuthenticationUserHeader = "AUTH_USER";
        public const string CacheUrlHeader = "CACHE_URL";
        public const string CertificateCookieHeader = "CERT_COOKIE";
        public const string CertificateFlagsHeader = "CERT_FLAGS";
        public const string CertificateIssuerHeader = "CERT_ISSUER";
        public const string CertificateKeySizeHeader = "CERT_KEYSIZE";
        public const string CertificateSecretKeySizeHeader = "CERT_SECRETKEYSIZE";
        public const string CertificateSerialNumberHeader = "CERT_SERIALNUMBER";
        public const string CertificateServerIssuerHeader = "CERT_SERVER_ISSUER";
        public const string CertificateServerSubjectHeader = "CERT_SERVER_SUBJECT";
        public const string CertificateSubjectHeader = "CERT_SUBJECT";
        public const string ContentLengthHeader = "CONTENT_LENGTH";
        public const string ContentTypeHeader = "CONTENT_TYPE";
        public const string GatewayInterfaceHeader = "GATEWAY_INTERFACE";
        public const string HeaderPrefix = "HEADER_";
        public const string HttpAcceptHeader = "HTTP_ACCEPT";
        public const string HttpEncodingHeader = "HTTP_ACCEPT_ENCODING";
        public const string HttpAcceptLanguageHeader = "HTTP_ACCEPT_LANGUAGE";
        public const string HttpConnectionHeader = "HTTP_CONNECTION";
        public const string HttpCookieHeader = "HTTP_COOKIE";
        public const string HttpHostHeader = "HTTP_HOST";
        public const string HttpMethodHeader = "HTTP_METHOD";
        public const string HttpReferrerHeader = "HTTP_REFERER";
        public const string HttpUrlHeader = "HTTP_URL";
        public const string HttpUserAgentHeader = "HTTP_USER_AGENT";
        public const string HttpVersionHeader = "HTTP_VERSION";
        public const string HttpsHeader = "HTTPS";
        public const string HttpsKeySizeHeader = "HTTPS_KEYSIZE";
        public const string HttpsSecretKeySizeHeader = "HTTPS_SECRETKEYSIZE";
        public const string HttpsServerIssuerHeader = "HTTPS_SERVER_ISSUER";
        public const string HttpsServerSubjectHeader = "HTTPS_SERVER_SUBJECT";
        public const string InstanceIdHeader = "INSTANCE_ID";
        public const string InstanceMetadataPathHeader = "INSTANCE_META_PATH";
        public const string LocalAddressHeader = "LOCAL_ADDR";
        public const string LogonUserHeader = "LOGON_USER";
        public const string PathInfoHeader = "PATH_INFO";
        public const string PathTranslatedHeader = "PATH_TRANSLATED";
        public const string QueryStringHeader = "QUERY_STRING";
        public const string RemoteAddressHeader = "REMOTE_ADDR";
        public const string RemoteHostHeader = "REMOTE_HOST";
        public const string RemotePortHeader = "REMOTE_PORT";
        public const string RemoteUserHeader = "REMOTE_USER";
        public const string RequestMethodHeader = "REQUEST_METHOD";
        public const string ScriptNameHeader = "SCRIPT_NAME";
        public const string ScriptTranslatedHeader = "SCRIPT_TRANSLATED";
        public const string ServerNameHeader = "SERVER_NAME";
        public const string ServerPortHeader = "SERVER_PORT";
        public const string ServerPortSecureHeader = "SERVER_PORT_SECURE";
        public const string ServerProtocolHeader = "SERVER_PROTOCOL";
        public const string ServerSoftwareHeader = "SERVER_SOFTWARE";
        public const string SsiExecDisabledHeader = "SSI_EXEC_DISABLED";
        public const string UnencodedUrlHeader = "UNENCODED_URL";
        public const string UnicodePrefix = "UNICODE_";
        public const string UnmappedRemoteUserHeader = "UNMAPPED_REMOTE_USER";
        public const string UrlHeader = "URL";

        public string AllHttp => this[AllHttpHeader];
        public string AllRaw => this[AllRawHeader];
        public string AppPoolId => this[AppPoolIdHeader];
        public string ApplicationMetadataPath => this[ApplicationMetadataPathHeader];
        public string ApplicationPhysicalPath => this[ApplicationPhysicalPathHeader];
        public string AuthenticationPassword => this[AuthenticationPasswordHeader];
        public string AuthenticationType => this[AuthenticationTypeHeader];
        public string AuthenticationUser => this[AuthenticationUserHeader];
        public string CacheUrl => this[CacheUrlHeader];
        public string CertificateCookie => this[CertificateCookieHeader];
        public string CertificateFlags => this[CertificateFlagsHeader];
        public string CertificateIssuer => this[CertificateIssuerHeader];
        public string CertificateKeySize => this[CertificateKeySizeHeader];
        public string CertificateSecretKeySize => this[CertificateSecretKeySizeHeader];
        public string CertificateSerialNumber => this[CertificateSerialNumberHeader];
        public string CertificateServerIssuer => this[CertificateServerIssuerHeader];
        public string CertificateServerSubject => this[CertificateServerSubjectHeader];
        public string CertificateSubject => this[CertificateSubjectHeader];
        public string ContentLength => this[ContentLengthHeader];
        public string ContentType => this[ContentTypeHeader];
        public string GatewayInterface => this[GatewayInterfaceHeader];
        public string HttpAccept => this[HttpAcceptHeader];
        public string HttpEncoding => this[HttpEncodingHeader];
        public string HttpAcceptLanguage => this[HttpAcceptLanguageHeader];
        public string HttpConnection => this[HttpConnectionHeader];
        public string HttpCookie => this[HttpCookieHeader];
        public string HttpHost => this[HttpHostHeader];
        public string HttpMethod => this[HttpMethodHeader];
        public string HttpReferrer => this[HttpReferrerHeader];
        public string HttpUrl => this[HttpUrlHeader];
        public string HttpUserAgent => this[HttpUserAgentHeader];
        public string HttpVersion => this[HttpVersionHeader];
        public string Https => this[HttpsHeader];
        public string HttpsKeySize => this[HttpsKeySizeHeader];
        public string HttpsSecretKeySize => this[HttpsSecretKeySizeHeader];
        public string HttpsServerIssuer => this[HttpsServerIssuerHeader];
        public string HttpsServerSubject => this[HttpsServerSubjectHeader];
        public string InstanceId => this[InstanceIdHeader];
        public string InstanceMetadataPath => this[InstanceMetadataPathHeader];
        public string LocalAddress => this[LocalAddressHeader];
        public string LogonUser => this[LogonUserHeader];
        public string PathInfo => this[PathInfoHeader];
        public string PathTranslated => this[PathTranslatedHeader];
        public string QueryString => this[QueryStringHeader];
        public string RemoteAddress => this[RemoteAddressHeader];
        public string RemoteHost => this[RemoteHostHeader];
        public string RemotePort => this[RemotePortHeader];
        public string RemoteUser => this[RemoteUserHeader];
        public string RequestMethod => this[RequestMethodHeader];
        public string ScriptName => this[ScriptNameHeader];
        public string ScriptTranslated => this[ScriptTranslatedHeader];
        public string ServerName => this[ServerNameHeader];
        public string ServerPort => this[ServerPortHeader];
        public string ServerPortSecure => this[ServerPortSecureHeader];
        public string ServerProtocol => this[ServerProtocolHeader];
        public string ServerSoftware => this[ServerSoftwareHeader];
        public string SsiExecDisabled => this[SsiExecDisabledHeader];
        public string UnencodedUrl => this[UnencodedUrlHeader];
        public string GetUnicodeServerVariableName(string name) { return this[UnicodePrefix + name]; }
        public string UnmappedRemoteUser => this[UnmappedRemoteUserHeader];
        public string Url => this[UrlHeader];

        public string Header(string name)
        {
            return this[HeaderPrefix + name];
        }

        public bool IsGet()
        {
            return HttpMethod.Equals("get", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsPost()
        {
            return HttpMethod.Equals("post", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsPut()
        {
            return HttpMethod.Equals("put", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsDelete()
        {
            return HttpMethod.Equals("delete", StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return this.ToKeyValueString();
        }
    }
}
