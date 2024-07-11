using System;
using System.Net.Mail;

public class Email
{
    public void Mail(string subject, string content, string emailid, string cc, string bcc)
    {
        try
        {

            string userName = "api@hansaresearch.com";
            string password = "SOyyy959";//Dc 28-03-20 "H2nsa#1814";

            //DC 12-06-2019
            // string userName = "nobby.mendonca@hansaresearch.com";
            //string password = "C1r1t@123";
            MailMessage msg = new MailMessage();
            msg.To.Add(emailid);
            msg.From = new MailAddress(userName);
            msg.Subject = subject;
            msg.Body = content;
            //msg.Body = "Tocontinue discussion click <a href=\"" + "~/emailers/resetpassword.html"+ "\">here</ID></a>";

            msg.IsBodyHtml = true;

            if (cc != "")
            {
                foreach (var address in cc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    msg.CC.Add(address);
                }
            }

            if (bcc != "")
            {
                foreach (var bccaddress in bcc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    msg.Bcc.Add(bccaddress);
                }
            }
        //SmtpClient client = new SmtpClient();
        //client.Host = "smtp.office365.com";
        //client.Credentials = new System.Net.NetworkCredential(userName, password);
        //client.Port = 587; // You can use Port 25 if 587 is blocked
        //client.EnableSsl = true;
        //client.Send(msg);


    test:
            {

                try
                {
                    //SmtpClient client = new SmtpClient();

                    //client.UseDefaultCredentials = false;
                    //client.Credentials = new System.Net.NetworkCredential(userName, password);
                    //client.Port = 587; // You can use Port 25 if 587 is blocked                        
                    //client.Host = "smtp.office365.com";
                    //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //client.EnableSsl = true;
                    //client.ServicePoint.MaxIdleTime = 1;
                    //client.Send(msg);
                    //msg.Dispose();


                    SmtpClient client = new SmtpClient();

                    client.UseDefaultCredentials = false;
                    //DC 05-02-2022  client.Credentials = new System.Net.NetworkCredential(userName, password);
                    client.Credentials = new System.Net.NetworkCredential("hansaresearchpep", "Hrg#1814");
                    client.Port = 587; // You can use Port 25 if 587 is blocked                        
                    //DC 05-02-2022 client.Host = "smtp.office365.com";
                    client.Host = "smtp.pepipost.com";
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = false;
                    client.ServicePoint.MaxIdleTime = 1;
                    client.Send(msg);
                    msg.Dispose();
                }
                catch (Exception ex)
                {
                    goto test;

                }
            }

        }
        catch (Exception ex)
        {

            //  throw;
        }
    }
}