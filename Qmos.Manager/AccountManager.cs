using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Utilities;
using Qmos.Data.Repositories.Abstractions;
using Qmos.Entities;
using Qmos.Manager.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Qmos.Manager
{
    public class AccountManager : ManagerBase, IAccountManager
    {
        public IConfiguration Configuration { get; }
        public IEmailSender EmailSender { get; }
        private readonly Security_ModuleContext DB;
        private string BaseUrl;

        public AccountManager( IConfiguration configuration,
                        IEmailSender emailSender, Security_ModuleContext _DB,
                          ILoggerErrorManager loggerErrorManager,
                          ILoggerActionsManager logger_ActionsManager, IUserManager userManager) : base(loggerErrorManager, logger_ActionsManager, userManager)
        {
            Configuration = configuration;
            EmailSender = emailSender;
            DB = _DB;
            BaseUrl = Configuration.GetSection("RecoveryUrl").Value;

        }

        public async Task<bool> ForgotPasswordNotification(string email)
        {
            var user = DB.SM_USER.SingleOrDefault(x => x.TX_Email.Equals(email));

            if (user != null)
            {
                var idUserEncriptado = Encriptar(user.ID_User.ToString());
                var idUserDesencriptado = Desencriptar(idUserEncriptado);
                string Enlace = $"{(string.IsNullOrEmpty(BaseUrl) ? "" : $"{BaseUrl}")}ForgotPassword/Index?i={idUserEncriptado}";
                user.DT_ValidDatePasswordRecoveryLink = DateTime.Now.AddHours(24);
                DB.SaveChanges();
                await EmailSender.SendEmailAsync(user.TX_Email, $" Restablecimiento de contraseña de la cuenta Linea Comercial",
                    "<html><head><style>a {color: white;text-decoration: none;}a:active {color: white;text-decoration: none;}a:hover {color: white;text-decoration: none;}</style></head>" +
$@"<body style='font-family:sans-serif;width: 100%;text-align: -webkit-center;'>
    <div>
        <img src='cid:Header' width='400px'>
    </div>
    <div
        style='width: 398px;margin-top:-5px;padding-top:20px;background-color:white;border-width:1px;border-style:solid;border-color:white;text-align:center;margin-top:-15px;'>
        <div><img height='80px' src='cid:Icon'></div>
        <p style='font-size: 14px;'><strong>Estimado</strong>, {user.TX_FirstName} {user.TX_LastName}</p>
        <p style='font-size: 14px;text-align: center;'>El siguiente enlace le permitir&aacute; restablecer la contrase&ntilde;a para ingresar nuevamente al portal de
            <br /><strong>Lince Comercial</strong>.
            <br />
            <br />
        </p>
        <div>
            <button
                style='background-color: lightseagreen;color: white; border-radius: 25px; width: 200px; border-color: lightseagreen;height: 40px;'><a style='color: white; text-decoration: none;' href='{Enlace}' target='_blank'> Restablecer Contrase&ntilde;a</a></button>
        </div>
        <p></p>
        <p>Este enlace tendrá una vigencia de <strong>24 horas</strong></p>
        <p></p>
        <p style='font-size: 11px;'>Si no visualiza el enlace puede ingresar esta url en su explorador: {Enlace} </p>
        <p style='font-size: 12px;'>Este mensaje ha sido enviado autom&aacute;ticamente por el sistema</p>
    </div>
    <div>
        <img src='cid:Footer' width='400px'>
    </div>
</body>

</html>", true);
                return true;
            }
            else {
                return false;
            }
        }

        public string FindIdUser(string iduser) {
            string idUser = Desencriptar(iduser);
            return idUser;
        }

        public static string Encriptar(string texto)
        {
            try
            {

                string key = "qualityinfosolutions"; //llave para encriptar datos

                byte[] keyArray;

                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);

                //Se utilizan las clases de encriptación MD5

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();

                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);

                tdes.Clear();

                //se regresa el resultado en forma de una cadena
                texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);

            }
            catch (Exception)
            {

            }
            return texto;
        }

        public static string Desencriptar(string textoEncriptado)
        {
            try
            {
                string key = "qualityinfosolutions";
                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);

                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                hashmd5.Clear();

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);

                tdes.Clear();
                textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray);

            }
            catch (Exception)
            {

            }
            return textoEncriptado;
        }

        //public Task<bool> IsClientActive(int Id_Customer)
        //{
        //    try
        //    {
        //        return CustomerRepository.IsActive(Id_Customer);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
