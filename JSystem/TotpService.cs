using Azure.Core;
using OtpNet;
using QRCoder;
using System;

namespace JustSupportSystem.JSystem
{
    //var secretKey = _totpService.GenerateSecretKey();
    //var uri = _totpService.GenerateQrCodeUrl("chinthakapb@gmail.com", secretKey);
    //var qrCodeImage = _totpService.GenerateQRCode(uri);

    //bool result = _totpService.ValidateOTP(request.SecretKey, request.Code);
    public class TotpService
    {
        public string GenerateSecretKey()
        {
            var secretKey = KeyGeneration.GenerateRandomKey(20);
            return Base32Encoding.ToString(secretKey);
        }

        public byte[] GenerateQRCode(string email, string secretKey)
        {
            return GenerateQRCode(GenerateQrCodeUrl(email, secretKey));
        }

        public string GenerateQrCodeUrl(string email, string secretKey)
        {
            var issuer = Uri.EscapeDataString("HelixSupport");
            var userEmail = Uri.EscapeDataString(email);

            return $"otpauth://totp/{issuer}:{userEmail}?secret={secretKey}&issuer={issuer}&algorithm=SHA1&digits=6&period=30";
        }

        public byte[] GenerateQRCode(string uri)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(uri, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);

            return qrCode.GetGraphic(20);
        }

        public bool ValidateOTP(string secretKey, string otp)
        {
            var totp = new Totp(Base32Encoding.ToBytes(secretKey));
            return totp.VerifyTotp(otp, out _, VerificationWindow.RfcSpecifiedNetworkDelay);
        }
    }
}
