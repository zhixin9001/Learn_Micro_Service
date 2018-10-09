using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;

namespace _9_JWTTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Encrpty-----------------");
            var payload = new Dictionary<string, object>
            {
                { "UserId", 123 },
                { "UserName", "admin" },
                { "exp","123456"}
            };
            var secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
            //不要泄露
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            var token = encoder.Encode(payload, secret);
            Console.WriteLine(token);

            Console.WriteLine("Decrpty-----------------");
            var secret1 = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
            try
            {
                IJsonSerializer serializer1 = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer1, provider);
                IBase64UrlEncoder urlEncoder1 = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer1, validator, urlEncoder1);
                var json = decoder.Decode(token, secret1, verify: true);
                Console.WriteLine(json);

                var json1 = decoder.Decode(token);
                Console.WriteLine(json1);
            }
            catch (FormatException)
            {
                Console.WriteLine("Token format invalid");
            }
            catch (TokenExpiredException)
            {
                Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
                Console.WriteLine("Token has invalid signature");
            }
            Console.ReadKey();
        }
    }
}
