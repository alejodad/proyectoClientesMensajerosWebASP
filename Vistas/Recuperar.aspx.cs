﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vistas.Models;
using SimpleCrypto;
using System.Configuration;
using System.Net.Mail;
using System.Net;
namespace Vistas
{
    
    public partial class Recuperar : System.Web.UI.Page
    {
        AplicacionDataContext instanciBD = new AplicacionDataContext();
        ICryptoService encriptado = new PBKDF2();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text;
            var persona = instanciBD.tbl_persona.Where(x =>x.usuarioPersona == user).FirstOrDefault();
            if (persona != null)
            {
                string newPasEncriptada = RandomPassword.Generate(10, PasswordGroup.Lowercase, PasswordGroup.Uppercase, PasswordGroup.Numeric, PasswordGroup.Special); //la cantidad de argumentos, minusculas, mayusculas/ numeros y caracteres especiales

                string salt = encriptado.GenerateSalt();
                string contrasenaEncriptada = encriptado.Compute(newPasEncriptada);
                try
                {
                    persona.salt = salt;
                    persona.contrasenaPersona = contrasenaEncriptada;
                    instanciBD.SubmitChanges();
                    EnviarCorreo(persona.emailPersona, newPasEncriptada);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "aletarUsuario", "window.onload = function(){ alert ('Usuario No esta');};", true);
            }

        }

        public void EnviarCorreo(string enviarA, string contraseñaRecuperar)
        {
            string correAdmin = ConfigurationManager.AppSettings["correoElectronico"].ToString();
            string contrAdmin = ConfigurationManager.AppSettings["contrasena"].ToString();

            string asunto = "Recuperacion de Contraseña ";
            string body = "Su nueva contraseña es: <strong>"+contraseñaRecuperar+"</strong>";
            body += "<!doctype html><html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'><head> <title>Say hello to RealEstate</title> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'> <meta name='viewport' content='width=device-width, initial-scale=1.0'> <style type='text/css'> #outlook a{padding: 0;}.ReadMsgBody{width: 100%;}.ExternalClass{width: 100%;}.ExternalClass *{line-height: 100%;}body{margin: 0; padding: 0; -webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%;}table, td{border-collapse: collapse; mso-table-lspace: 0pt; mso-table-rspace: 0pt;}img{border: 0; height: auto; line-height: 100%; outline: none; text-decoration: none; -ms-interpolation-mode: bicubic;}p{display: block; margin: 13px 0;}</style> <style type='text/css'> @media only screen and (max-width:480px){@-ms-viewport{width: 320px;}@viewport{width: 320px;}}</style><!--[if mso]><xml> <o:OfficeDocumentSettings> <o:AllowPNG/> <o:PixelsPerInch>96</o:PixelsPerInch> </o:OfficeDocumentSettings></xml><![endif]--><!--[if lte mso 11]><style type='text/css'> .outlook-group-fix{width:100% !important;}</style><![endif]--> <link href='https://fonts.googleapis.com/css?family=Roboto:400,700' rel='stylesheet' type='text/css'> <link href='https://fonts.googleapis.com/css?family=Alice' rel='stylesheet' type='text/css'> <style type='text/css'> @import url(https://fonts.googleapis.com/css?family=Roboto:400,700); @import url(https://fonts.googleapis.com/css?family=Alice); </style> <style type='text/css'> @media only screen and (min-width:480px){.mj-column-per-100{width: 100%!important;}.mj-column-per-60{width: 60%!important;}.mj-column-per-40{width: 40%!important;}.mj-column-per-55{width: 55%!important;}.mj-column-per-45{width: 45%!important;}.mj-column-per-33{width: 33%!important;}.mj-column-per-34{width: 34%!important;}.mj-column-px-570{width: 570px!important;}.mj-column-px-300{width: 300px!important;}.mj-column-px-260{width: 260px!important;}}</style></head><body style='background: #EAE8E5;'> <div class='mj-container' style='background-color:#EAE8E5;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:20px 0px 0px 0px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:600px;'><![endif]--> <div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='center' border='0'> <tbody> <tr> <td style='width:600px;'><img alt='' title='' height='auto' src='http://nimus.de/share/tpl-realestate/box-head.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='600'></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:#FFFFFF;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#FFFFFF;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:0px;padding-top:40px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:570px;'><![endif]--> <div class='mj-column-px-570 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px 20px;' align='left'> <div style='cursor:auto;color:#1D83A8;font-family:Alice, Helvetica, Arial, sans-serif;font-size:36px;font-weight:400;line-height:40px;text-align:left;'>Real Estate</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:0px 20px;' align='left'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:14px;font-weight:400;line-height:21px;text-align:left;'><strong><span style='font-size: 40px;'>⚊</span></strong><br><br></div></td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:#FFFFFF;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#FFFFFF;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:0px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:300px;'><![endif]--> <div class='mj-column-px-300 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px 20px 30px 20px;' align='left'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:14px;font-weight:400;line-height:21px;text-align:left;'><strong>Aliquam lorem ante, dapibus in hasellus viverra nulla</strong> ut metus varius laoreet. Quisque rutrum lorem dellorus. Aenean imperdiet.</div></td></tr></tbody> </table> </div><!--[if mso | IE]> </td><td style='vertical-align:top;width:260px;'><![endif]--> <div class='mj-column-px-260 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;padding-bottom:30px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='center' border='0'> <tbody> <tr> <td style='width:260px;'><img alt='' title='' height='auto' src='http://nimus.de/share/tpl-realestate/icon-1.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='260'></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:url(http://nimus.de/share/tpl-realestate/img-1.png) top center / cover no-repeat;'><!--[if mso | IE]> <v:rect xmlns:v='urn:schemas-microsoft-com:vml' fill='true' stroke='false' style='width:600px;'> <v:fill origin='0.5, 0' position='0.5,0' type='tile' src='http://nimus.de/share/tpl-realestate/img-1.png'/> <v:textbox style='mso-fit-shape-to-text:true' inset='0,0,0,0'><![endif]--> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:url(http://nimus.de/share/tpl-realestate/img-1.png) top center / cover no-repeat;' align='center' border='0' background='http://nimus.de/share/tpl-realestate/img-1.png'> <tbody> <tr> <td style='text-align:center;vertical-align:middle;direction:ltr;font-size:0px;padding:30px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:middle;width:360px;'><![endif]--> <div class='mj-column-per-60 outlook-group-fix' style='vertical-align:middle;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' style='vertical-align:middle;' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:40px 30px;' align='center'> <div style='cursor:auto;color:#FFFFFF;font-family:Alice, Helvetica, Arial, sans-serif;font-size:42px;font-weight:400;line-height:48px;text-align:center;'>Villa Semperin</div></td></tr></tbody> </table> </div><!--[if mso | IE]> </td><td style='vertical-align:middle;width:240px;'><![endif]--> <div class='mj-column-per-40 outlook-group-fix' style='vertical-align:middle;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' style='background:#FFFFFF;vertical-align:middle;' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='center' border='0'> <tbody> <tr> <td style='width:220px;'><img alt='' title='' height='auto' src='http://nimus.de/share/tpl-realestate/box-top.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='220'></td></tr></tbody> </table> </td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:20px 20px 0px 20px;' align='center'> <div style='cursor:auto;color:#1D83A8;font-family:Alice, Helvetica, Arial, sans-serif;font-size:26px;font-weight:400;line-height:30px;text-align:center;'>– first offer –</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:10px 20px;' align='center'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:22px;font-weight:400;line-height:30px;text-align:center;'>340,000 $</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:0px 20px;' align='center'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:14px;font-weight:400;line-height:21px;text-align:center;'>Nascetur ridiculus mus. Donec quam felis, ultricies nec</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:20px 20px 30px 20px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:separate;' align='center' border='0'> <tbody> <tr> <td style='border:none;border-radius:20px;color:white;cursor:auto;padding:10px 25px;' align='center' valign='middle' bgcolor='#F44E3C'> <p style='text-decoration:none;background:#F44E3C;color:white;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:13px;font-weight:normal;line-height:120%;text-transform:none;margin:0px;'>view details</p></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table><!--[if mso | IE]> </v:textbox> </v:rect><![endif]--> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:#FFFFFF;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#FFFFFF;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:30px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:360px;'><![endif]--> <div class='mj-column-per-60 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;padding-bottom:20px;' align='center'> <div style='cursor:auto;color:#000000;font-family:Alice, Helvetica, Arial, sans-serif;font-size:26px;font-weight:400;line-height:30px;text-align:center;'>Lorem Ipsum</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:0px 40px;' align='center'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:14px;font-weight:400;line-height:21px;text-align:center;'>Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Lorem Ipsum</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:30px 0px 10px 0px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='center' border='0'> <tbody> <tr> <td style='width:50px;'><img alt='' title='' height='auto' src='http://nimus.de/share/tpl-realestate/icon-2.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='50'></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td><td style='vertical-align:top;width:240px;'><![endif]--> <div class='mj-column-per-40 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' style='background:#FFFFFF;' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='center' border='0'> <tbody> <tr> <td style='width:216px;'><img alt='' title='' height='auto' src='http://nimus.de/share/tpl-realestate/img-2.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='216'></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:#FFFFFF;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#FFFFFF;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:20px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:600px;'><![endif]--> <div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;' align='left'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='left' border='0'> <tbody> <tr> <td style='width:292px;'><img alt='' title='' height='auto' src='http://nimus.de/share/tpl-realestate/symbol-1.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='292'></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:#FFFFFF;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#FFFFFF;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:0px 20px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:600px;'><![endif]--> <div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;' align='center'> <div style='cursor:auto;color:#000000;font-family:Alice, Helvetica, Arial, sans-serif;font-size:26px;font-weight:400;line-height:30px;text-align:center;'>Lorem Ipsum</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:20px 0px;'> <p style='font-size:1px;margin:0px auto;border-top:2px solid #000000;width:20px;'></p></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:0px 40px;' align='center'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:14px;font-weight:400;line-height:21px;text-align:center;'>Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Lorem Ipsum</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:20px 20px 10px 20px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:separate;' align='center' border='0'> <tbody> <tr> <td style='border:none;border-radius:20px;color:white;cursor:auto;padding:10px 25px;' align='center' valign='middle' bgcolor='#F44E3C'> <p style='text-decoration:none;background:#F44E3C;color:white;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:13px;font-weight:normal;line-height:120%;text-transform:none;margin:0px;'>Call to action</p></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:#FFFFFF;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#FFFFFF;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:20px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:600px;'><![endif]--> <div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;' align='right'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='right' border='0'> <tbody> <tr> <td style='width:213px;'><img alt='' title='' height='auto' src='http://nimus.de/share/tpl-realestate/symbol-2.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='213'></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:#FFFFFF;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#FFFFFF;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:0px 30px 30px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:330px;'><![endif]--> <div class='mj-column-per-55 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;padding-bottom:20px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='center' border='0'> <tbody> <tr> <td style='width:280px;'><img alt='' title='' height='auto' src='http://nimus.de/share/tpl-realestate/img-3.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='280'></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td><td style='vertical-align:top;width:270px;'><![endif]--> <div class='mj-column-per-45 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' style='background:#FFFFFF;' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;padding-bottom:20px;' align='center'> <div style='cursor:auto;color:#000000;font-family:Alice, Helvetica, Arial, sans-serif;font-size:26px;font-weight:400;line-height:30px;text-align:center;'>Lorem Ipsum</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:0px 40px;' align='center'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:14px;font-weight:400;line-height:21px;text-align:center;'>Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum.</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:30px 0px 10px 0px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='center' border='0'> <tbody> <tr> <td style='width:50px;'><img alt='' title='' height='auto' src='http://nimus.de/share/tpl-realestate/icon-3.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='50'></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:#FFFFFF;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#FFFFFF;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:0px 20px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:600px;'><![endif]--> <div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;' align='center'> <div style='cursor:auto;color:#000000;font-family:Alice, Helvetica, Arial, sans-serif;font-size:26px;font-weight:400;line-height:30px;text-align:center;'>Nullam dictum felis eu pede</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:20px 0px;'> <p style='font-size:1px;margin:0px auto;border-top:2px solid #000000;width:20px;'></p></td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:#FFFFFF;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#FFFFFF;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:0px 20px 30px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:198px;'><![endif]--> <div class='mj-column-per-33 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px 15px 15px;' align='left'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:14px;font-weight:400;line-height:21px;text-align:left;'><strong>Aliquam lorem ante,</strong> dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Lorem Ipsum</div></td></tr></tbody> </table> </div><!--[if mso | IE]> </td><td style='vertical-align:top;width:204px;'><![endif]--> <div class='mj-column-per-34 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px 15px 15px;' align='left'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:14px;font-weight:400;line-height:21px;text-align:left;'><strong>Phasellus viverra null aliquam lorem ante</strong>, dapibus in, viverra quis, feugiat a, tellus ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Lorem Ipsum</div></td></tr></tbody> </table> </div><!--[if mso | IE]> </td><td style='vertical-align:top;width:198px;'><![endif]--> <div class='mj-column-per-33 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px 15px 15px;' align='left'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:14px;font-weight:400;line-height:21px;text-align:left;'><strong>Quisque rutrum.</strong> Aenean imperdiet viverra nulla ut metus varius laoreet. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Lorem Ipsum</div></td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:url(http://nimus.de/share/tpl-realestate/img-4.png) top center / cover no-repeat;'><!--[if mso | IE]> <v:rect xmlns:v='urn:schemas-microsoft-com:vml' fill='true' stroke='false' style='width:600px;'> <v:fill origin='0.5, 0' position='0.5,0' type='tile' src='http://nimus.de/share/tpl-realestate/img-4.png'/> <v:textbox style='mso-fit-shape-to-text:true' inset='0,0,0,0'><![endif]--> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:url(http://nimus.de/share/tpl-realestate/img-4.png) top center / cover no-repeat;' align='center' border='0' background='http://nimus.de/share/tpl-realestate/img-4.png'> <tbody> <tr> <td style='text-align:center;vertical-align:middle;direction:ltr;font-size:0px;padding:30px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:middle;width:240px;'><![endif]--> <div class='mj-column-per-40 outlook-group-fix' style='vertical-align:middle;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' style='background:#FFFFFF;vertical-align:middle;' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='center' border='0'> <tbody> <tr> <td style='width:220px;'><img alt='' title='' height='auto' src='http://nimus.de/share/tpl-realestate/box-top.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='220'></td></tr></tbody> </table> </td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:20px 20px 0px 20px;' align='center'> <div style='cursor:auto;color:#1D83A8;font-family:Alice, Helvetica, Arial, sans-serif;font-size:26px;font-weight:400;line-height:30px;text-align:center;'>– 2. offer –</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:10px 20px;' align='center'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:22px;font-weight:400;line-height:30px;text-align:center;'>198,700 $</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:0px 20px;' align='center'> <div style='cursor:auto;color:#000000;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:14px;font-weight:400;line-height:21px;text-align:center;'>Donec quam felis, ultricies Nascetur ridiculus mus.</div></td></tr><tr> <td style='word-wrap:break-word;font-size:0px;padding:20px 20px 30px 20px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:separate;' align='center' border='0'> <tbody> <tr> <td style='border:none;border-radius:20px;color:white;cursor:auto;padding:10px 25px;' align='center' valign='middle' bgcolor='#F44E3C'> <p style='text-decoration:none;background:#F44E3C;color:white;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:13px;font-weight:normal;line-height:120%;text-transform:none;margin:0px;'>view details</p></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td><td style='vertical-align:middle;width:360px;'><![endif]--> <div class='mj-column-per-60 outlook-group-fix' style='vertical-align:middle;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' style='vertical-align:middle;' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:40px 30px;' align='center'> <div style='cursor:auto;color:#FFFFFF;font-family:Alice, Helvetica, Arial, sans-serif;font-size:42px;font-weight:400;line-height:48px;text-align:center;'>Window House 23</div></td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table><!--[if mso | IE]> </v:textbox> </v:rect><![endif]--> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;background:#FFFFFF;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;background:#FFFFFF;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:40px 20px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:600px;'><![endif]--> <div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;' align='center'> <table role='presentation' cellpadding='0' cellspacing='0' style='border-collapse:collapse;border-spacing:0px;' align='center' border='0'> <tbody> <tr> <td style='width:130px;'><img alt='' title='' height='auto' src='http://nimus.de/share/tpl-realestate/icon-1.png' style='border:none;border-radius:0px;display:block;font-size:13px;outline:none;text-decoration:none;width:100%;height:auto;' width='130'></td></tr></tbody> </table> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0' width='600' align='center' style='width:600px;'> <tr> <td style='line-height:0px;font-size:0px;mso-line-height-rule:exactly;'><![endif]--> <div style='margin:0px auto;max-width:600px;'> <table role='presentation' cellpadding='0' cellspacing='0' style='font-size:0px;width:100%;' align='center' border='0'> <tbody> <tr> <td style='text-align:center;vertical-align:top;direction:ltr;font-size:0px;padding:10px 0px 20px 0px;'><!--[if mso | IE]> <table role='presentation' border='0' cellpadding='0' cellspacing='0'> <tr> <td style='vertical-align:top;width:600px;'><![endif]--> <div class='mj-column-per-100 outlook-group-fix' style='vertical-align:top;display:inline-block;direction:ltr;font-size:13px;text-align:left;width:100%;'> <table role='presentation' cellpadding='0' cellspacing='0' width='100%' border='0'> <tbody> <tr> <td style='word-wrap:break-word;font-size:0px;padding:0px;' align='center'> <div style='cursor:auto;color:#9B9B9B;font-family:Roboto, Helvetica, Arial, sans-serif;font-size:11px;font-weight:400;line-height:21px;text-align:center;'><a href='#' style='color: #9B9B9B;'>Unsubscribe</a> from this newsletter<br>Icon made by Freepik from <a href='http://www.flaticon.com' target='_blank' style='color: #9B9B9B; text-decoration: none;'>www.flaticon.com</a><br><a href='http://svenhaustein.de' style='color: #9B9B9B; text-decoration:none;'>Made by svenhaustein.de</a></div></td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </td></tr></tbody> </table> </div><!--[if mso | IE]> </td></tr></table><![endif]--> </div></body></html>";

            MailMessage mail = new MailMessage(correAdmin, enviarA, asunto, body);
            mail.IsBodyHtml = true;

            //configuraciones smtp
            var smtp = new SmtpClient();
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;// si es red sin proxy el port es 587 si es con proxy es el 25
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network; //desde que red se envian los correos
                smtp.Credentials = new NetworkCredential(correAdmin,contrAdmin);
                smtp.Timeout = 20000;
            }

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex )
            {

                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "aletaFailMensaje", "window.onload = function(){ alert ('El correo no se envio ');};", true);
            }
        }
    }
}