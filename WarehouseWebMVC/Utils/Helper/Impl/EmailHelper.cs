using MimeKit;
using WarehouseWebMVC.Utils.Helper;

namespace WarehouseWebMVC.Utils.Helper.Impl
{
    public class EmailHelper : IEmailHelper
    {
        public string RenderBodyResetPassword(string resetLink)
        {
            string bodyEmail = $@"
            <!DOCTYPE html>
            <html lang='en-US'>

                <head>
                    <meta content='text/html; charset=utf-8' http-equiv='Content-Type' />
                    <title>Reset Password</title>
                    <meta name='description' content='Reset Password Email.'>
                    <style type='text/css'>
                        a:hover {{text-decoration: underline !important;}}
                    </style>
                </head>

                <body marginheight='0' topmargin='0' marginwidth='0' style='margin: 0px; background-color: #f2f3f8;' leftmargin='0'>
                    <table cellspacing='0' border='0' cellpadding='0' width='100%' bgcolor='#f2f3f8'
                            style='@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;'>
                        <tr>
                            <td>
                                <table style='background-color: #f2f3f8; max-width:670px;  margin:0 auto;' width='100%' border='0'
                                        align='center' cellpadding='0' cellspacing='0'>
                                    <tr>
                                    <td style='height:80px;'>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style='text-align:center;'>
                                            <a href='https://localhost:7051/' title='logo' target='_blank'>
                                            <img width='200' src='https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/logo%2Fgdupa-high-resolution-logo-transparent.png?alt=media&token=c438141e-e081-48e3-8b9d-b270bd160fde' title='logo' alt='logo'>
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='height:20px;'>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width='95%' border='0' align='center' cellpadding='0' cellspacing='0'
                                                    style='max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);'>
                                                <tr>
                                                    <td style='height:40px;'>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style='padding:0 35px;'>
                                                        <h1 style='color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;'>You have
                                                            requested to reset your password</h1>
                                                        <span style='display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;'></span>
                                                        <p style='color:#455056; font-size:15px;line-height:24px; margin:0;'>
                                                            We cannot simply send you your old password. A unique link to reset your
                                                            password has been generated for you. To reset your password, click the
                                                            following link and follow the instructions.
                                                        </p>
                                                        <a href='{resetLink}'
                                                            style='background:#87CEFA;text-decoration:none !important; font-weight:500; margin-top:35px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;'>Reset
                                                            Password
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style='height:40px;'>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    <tr>
                                        <td style='height:20px;'>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style='height:80px;'>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </body>
            </html>";

            return bodyEmail;
        }

        public string RenderBodyActive(string email, string link)
        {
            string bodyEmail = $@"
            <!DOCTYPE html>
            <html lang='en-US'>

                <head>
                    <meta content='text/html; charset=utf-8' http-equiv='Content-Type' />
                    <title>Reset Password</title>
                    <meta name='description' content='Active Account Email.'>
                    <style type='text/css'>
                        a:hover {{text-decoration: underline !important;}}
                    </style>
                </head>

                <body marginheight='0' topmargin='0' marginwidth='0' style='margin: 0px; background-color: #f2f3f8;' leftmargin='0'>
                    <table cellspacing='0' border='0' cellpadding='0' width='100%' bgcolor='#f2f3f8'
                            style='@import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: 'Open Sans', sans-serif;'>
                        <tr>
                            <td>
                                <table style='background-color: #f2f3f8; max-width:670px;  margin:0 auto;' width='100%' border='0'
                                        align='center' cellpadding='0' cellspacing='0'>
                                    <tr>
                                    <td style='height:80px;'>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style='text-align:center;'>
                                            <a href='https://localhost:7051/' title='logo' target='_blank'>
                                            <img width='200' src='https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/logo%2Fgdupa-high-resolution-logo-transparent.png?alt=media&token=c438141e-e081-48e3-8b9d-b270bd160fde' title='logo' alt='logo'>
                                            </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style='height:20px;'>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width='95%' border='0' align='center' cellpadding='0' cellspacing='0'
                                                    style='max-width:670px;background:#fff; border-radius:3px; text-align:center;-webkit-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);-moz-box-shadow:0 6px 18px 0 rgba(0,0,0,.06);box-shadow:0 6px 18px 0 rgba(0,0,0,.06);'>
                                                <tr>
                                                    <td style='height:40px;'>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style='padding:0 35px;'>
                                                        <h1 style='color:#1e1e2d; font-weight:500; margin:0;font-size:32px;font-family:'Rubik',sans-serif;'>You have
                                                            requested to active your account</h1>
                                                        <span style='display:inline-block; vertical-align:middle; margin:29px 0 26px; border-bottom:1px solid #cecece; width:100px;'></span>
                                                        <p style='color:#455056; font-size:15px;line-height:24px; margin:0;'>
                                                            To access the system please activate your account by clicking the button below. <br/>
                                                            If you do not activate, your account will be automatically deleted after 5 days.
                                                        </p>
                                                        <a href='{link}'
                                                            style='background:#87CEFA;text-decoration:none !important; font-weight:500; margin-top:35px; color:#fff;text-transform:uppercase; font-size:14px;padding:10px 24px;display:inline-block;border-radius:50px;'>Active
                                                            Account
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style='height:40px;'>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    <tr>
                                        <td style='height:20px;'>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style='height:80px;'>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </body>
            </html>";

            return bodyEmail;
        }
    }
}
